namespace Imposter.Ideation;

public class AddMethodInvocation
{
    public AddMethodInvocation((int left, int right) arguments, int result, Exception? exception)
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }

    public (int left, int right) Arguments { get; }

    public int? Result { get; }

    public Exception? Exception { get; }
}