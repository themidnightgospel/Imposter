using System.Text;

namespace Imposter.Abstractions;

/// <summary>Verifies a subsequence of interactions, including interactions across imposters.</summary>
public sealed class InvocationOrder
{
    private readonly List<IInvocationStep> _steps = new();
    private InvocationMatch[] _selected = Array.Empty<InvocationMatch>();
    private bool _verifying;

    internal InvocationOrder() { }

    /// <summary>Adds the next expected interaction without configuring an outcome.</summary>
    /// <param name="step">The generated member verification builder.</param>
    /// <returns>This ordered verification.</returns>
    public InvocationOrder Then(IInvocationStep step)
    {
        if (step is null)
            throw new ArgumentNullException(nameof(step));
        lock (_steps)
        {
            if (_verifying)
                throw new InvalidOperationException("An order cannot change during verification.");
            if (_selected.Length == _steps.Count)
                Array.Resize(ref _selected, Math.Max(4, _steps.Count * 2));
            _steps.Add(step);
        }
        return this;
    }

    /// <summary>Verifies the configured steps and covers only the selected invocations on success.</summary>
    /// <remarks>
    /// Each step selects the earliest matching invocation after the previous step. Unrelated calls may occur
    /// between steps. A failed verification adds no coverage. Coverage from earlier verifications is retained.
    /// </remarks>
    public void Verify()
    {
        lock (_steps)
        {
            if (_verifying)
                throw new InvalidOperationException("Ordered verification cannot be reentered.");
            if (_steps.Count == 0)
                throw new InvalidOperationException(
                    "Ordered verification requires at least one step."
                );
            _verifying = true;
            try
            {
                var previous = long.MinValue;
                for (var index = 0; index < _steps.Count; index++)
                {
                    if (!_steps[index].TryFindAfter(previous, out var match))
                        throw Failure(index);
                    if (!match.IsValid || match.Sequence <= previous)
                        throw new InvalidOperationException(
                            "An ordered step returned an invalid selection."
                        );
                    _selected[index] = match;
                    previous = match.Sequence;
                }
                for (var index = 0; index < _steps.Count; index++)
                    _selected[index].MarkVerified();
            }
            finally
            {
                // An order stores builders, but must not retain discarded history through old selections.
                Array.Clear(_selected, 0, _steps.Count);
                _verifying = false;
            }
        }
    }

    private VerificationFailedException Failure(int failedIndex)
    {
        var expected = new StringBuilder("Interactions in order:");
        for (var index = 0; index < _steps.Count; index++)
            expected
                .AppendLine()
                .Append(index == failedIndex ? "  -> " : "     ")
                .Append(index + 1)
                .Append(". ")
                .Append(_steps[index].Description);

        var owners = new List<IImposter>();
        var unavailableOwners = new List<string>();
        var interactions = new List<(IImposterInvocation Invocation, string Owner)>();
        for (var index = 0; index < _steps.Count; index++)
        {
            var owner = _steps[index].Imposter;
            if (owner is null)
            {
                unavailableOwners.Add("Step " + (index + 1) + ": owning imposter unavailable.");
                continue;
            }
            if (owners.Any(existing => ReferenceEquals(existing, owner)))
                continue;
            owners.Add(owner);
            var ownerName = owner.GetType().Name + " #" + owners.Count;
            foreach (var invocation in owner.Invocations())
                interactions.Add((invocation, ownerName));
        }
        interactions.Sort(
            (left, right) => left.Invocation.Sequence.CompareTo(right.Invocation.Sequence)
        );
        var performed = string.Join(
            Environment.NewLine,
            unavailableOwners.Concat(
                interactions.Select(item => item.Owner + ": " + item.Invocation)
            )
        );
        return new VerificationFailedException(
            expected.ToString(),
            string.IsNullOrEmpty(performed) ? "none" : performed,
            "Recorded interactions"
        );
    }
}
