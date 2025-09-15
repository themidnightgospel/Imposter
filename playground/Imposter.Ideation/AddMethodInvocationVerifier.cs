namespace Imposter.Ideation;

public class AddMethodInvocationVerifier(
    AddMethodBehaviour addMethodBehaviour,
    TestArg<int> leftCriteria,
    TestArg<int> rightCriteria)
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