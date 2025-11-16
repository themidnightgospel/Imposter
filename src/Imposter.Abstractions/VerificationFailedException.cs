namespace Imposter.Abstractions;

/// <summary>
/// Exception thrown when verification of an expected invocation count fails.
/// </summary>
/// <remarks>
/// Typically thrown by verification helpers such as <c>.Called(Count.Exactly(n))</c> when the actual invocation
/// count does not satisfy the specified <see cref="Count"/> constraint.
/// </remarks>
public class VerificationFailedException : Exception
{
    /// <summary>
    /// Gets the textual representation of the invocations that were performed prior to the verification failure.
    /// </summary>
    public string? PerformedInvocations { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VerificationFailedException"/> class with expected and actual invocation counts.
    /// </summary>
    /// <param name="expectedCount">The expected number of invocations.</param>
    /// <param name="actualCount">The actual number of invocations that occurred.</param>
    public VerificationFailedException(Count expectedCount, int actualCount)
        : this(expectedCount, actualCount, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="VerificationFailedException"/> class with the performed invocations.
    /// </summary>
    /// <param name="expectedCount">The expected number of invocations.</param>
    /// <param name="actualCount">The actual number of invocations that occurred.</param>
    /// <param name="performedInvocations">The textual representation of the invocations that were performed.</param>
    public VerificationFailedException(
        Count expectedCount,
        int actualCount,
        string? performedInvocations
    )
        : base(BuildMessage(expectedCount, actualCount, performedInvocations))
    {
        PerformedInvocations = performedInvocations;
    }

    private static string BuildMessage(
        Count expectedCount,
        int actualCount,
        string? performedInvocations
    )
    {
        var message =
            $"Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.";

        if (!string.IsNullOrWhiteSpace(performedInvocations))
        {
            return $"{message}{Environment.NewLine}Performed invocations:{Environment.NewLine}{performedInvocations}";
        }

        return message;
    }
}
