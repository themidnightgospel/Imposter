namespace Imposter.Abstractions;

/// <summary>
/// Exception thrown when a mock behaviour verification fails.
/// </summary>
public class VerificationFailedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VerificationFailedException"/> class with expected and actual invocation counts.
    /// </summary>
    /// <param name="expectedCount">The expected number of invocations.</param>
    /// <param name="actualCount">The actual number of invocations that occurred.</param>
    public VerificationFailedException(Count expectedCount, int actualCount)
        : base($"Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.")
    { }
}