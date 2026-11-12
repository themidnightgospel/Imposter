using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Aggregates member findings for generated imposter verification.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ImposterVerification
{
    /// <summary>Formats recorded interactions, optionally excluding those already covered.</summary>
    /// <param name="invocations">The recorded invocation snapshot.</param>
    /// <param name="uncoveredOnly">Whether to omit covered invocations.</param>
    public static IEnumerable<string> Describe(
        IReadOnlyList<IImposterInvocation> invocations,
        bool uncoveredOnly
    )
    {
        for (var index = 0; index < invocations.Count; index++)
        {
            var invocation = invocations[index];
            if (!uncoveredOnly || invocation is not IVerifiableInvocation { IsVerified: true })
                yield return invocation.ToString() ?? invocation.Member;
        }
    }

    /// <summary>Throws when any supplied member has uncovered interactions.</summary>
    /// <param name="sources">The imposter's member verification sources.</param>
    public static void VerifyNoOtherCalls(params IImposterVerificationSource[] sources) =>
        Verify(
            sources,
            source => source.UncoveredInteractions(),
            "No uncovered interactions",
            "Uncovered interactions"
        );

    /// <summary>Throws when any supplied member has recorded interactions.</summary>
    /// <param name="sources">The imposter's member verification sources.</param>
    public static void VerifyNoInteractions(params IImposterVerificationSource[] sources) =>
        Verify(sources, source => source.Interactions(), "No interactions", "Interactions");

    /// <summary>Throws when any supplied member has an unused configured setup.</summary>
    /// <param name="sources">The imposter's member verification sources.</param>
    public static void VerifyAllSetupsUsed(params IImposterVerificationSource[] sources) =>
        Verify(
            sources,
            source => source.UnusedSetups(),
            "All configured setups used",
            "Unused setups"
        );

    private static void Verify(
        IImposterVerificationSource[] sources,
        Func<IImposterVerificationSource, IEnumerable<string>> select,
        string expected,
        string findingsHeading
    )
    {
        var findings = sources.SelectMany(select).ToArray();
        if (findings.Length != 0)
            throw new VerificationFailedException(
                expected,
                string.Join(Environment.NewLine, findings),
                findingsHeading
            );
    }
}
