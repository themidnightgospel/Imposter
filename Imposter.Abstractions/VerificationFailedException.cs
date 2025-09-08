namespace Imposter.Abstractions;

/// <summary>
/// TODO add docs
/// </summary>
public class VerificationFailedException : Exception
{
    public VerificationFailedException(string message) : base(message)
    { }

    public VerificationFailedException(string message, Exception innerException) : base(message, innerException)
    { }
}