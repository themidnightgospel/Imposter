namespace Imposter.Ideation;

public class VerificationFailedException : Exception
{
    public VerificationFailedException(string message) : base(message)
    { }

    public VerificationFailedException(string message, Exception innerException) : base(message, innerException)
    { }
}