namespace Imposter;

public class AddMethodInvocationVerifier(
    AddMethodBehaviour addMethodBehaviour,
    ParameterCriteria<int> leftCriteria,
    ParameterCriteria<int> rightCriteria)
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