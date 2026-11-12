namespace Imposter.Abstractions;

/// <summary>A generated verification builder that can participate in ordered verification.</summary>
public interface IInvocationStep
{
    /// <summary>The imposter whose interactions this step verifies.</summary>
    IImposter Imposter { get; }

    /// <summary>A description of this step and its argument matchers.</summary>
    string Description { get; }

    /// <summary>Captures the earliest matching entry after a sequence without changing coverage.</summary>
    /// <param name="sequence">The exclusive lower entry-stamp bound.</param>
    /// <param name="match">The captured selection, or the default value when no entry matches.</param>
    /// <remarks>Capture the history head once. Evaluate matchers only when this method is called.</remarks>
    bool TryFindAfter(long sequence, out InvocationMatch match);
}
