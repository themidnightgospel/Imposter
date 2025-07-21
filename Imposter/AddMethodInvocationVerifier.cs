namespace Imposter;

public class AddMethodInvocationVerifier(
    AddMethodBehaviour addMethodBehaviour,
    Arg<int> leftCriteria,
    Arg<int> rightCriteria)
{
    public void WasInvoked(InvocationCount count)
    {
        var invocationCount = addMethodBehaviour.CountInvocation(leftCriteria, rightCriteria);

        if (!count.Matches(invocationCount))
        {
            throw new VerificationFailedException("TODO");
        }
    }
}