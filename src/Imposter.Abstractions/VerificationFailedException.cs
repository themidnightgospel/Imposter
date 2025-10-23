namespace Imposter.Abstractions;

/// <summary>
/// TODO add docs
/// </summary>
public class VerificationFailedException : Exception
{
    public VerificationFailedException(Count expectedCount, int actualCount)
        : base($"Invocation was expected to be performed {expectedCount} but instead was performed {actualCount} times.")
    { }

    public VerificationFailedException(string message, Exception innerException) : base(message, innerException)
    { }
}