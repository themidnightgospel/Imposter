namespace Imposter.Abstractions;

/// <summary>
/// Exception thrown when invocation or setup verification fails.
/// </summary>
/// <remarks>
/// Typically thrown by verification helpers such as <c>.Called(Count.Exactly(n))</c> when the actual invocation
/// count does not satisfy the specified <see cref="Count"/> constraint.
/// </remarks>
public class VerificationFailedException : Exception
{
    internal VerificationFailedException(string expected, string findings, string findingsHeading)
        : base(
            "Expected "
                + expected
                + Environment.NewLine
                + findingsHeading
                + ":"
                + Environment.NewLine
                + findings
        )
    {
        PerformedInvocations = findings;
    }

    /// <summary>
    /// Gets the findings explaining the failure, such as interactions or unused setups.
    /// </summary>
    public string? PerformedInvocations { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="expectedCount">The expected number of invocations.</param>
    /// <param name="actualCount">The actual number of invocations that occurred.</param>
    public VerificationFailedException(Count expectedCount, int actualCount)
        : this(expectedCount, actualCount, null) { }

    /// <summary>
    /// Constructor
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
