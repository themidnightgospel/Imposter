namespace Imposter.Ideation;

public class AddMethodBehaviour
{
    public List<AddMethodInvocation> Invocations { get; } = new();

    // Support multiple return values,
    public List<AddMethodInvocationSetupBuilder> InvocationBehaviours { get; } = new();

    public int Invoke(int left, int right)
    {
        var methodCallSetup = Enumerable
            .Reverse(InvocationBehaviours)
            .Where(it => it.Matches(left, right));
        
        int result = default;

        try
        {
            /*
            result = methodCallSetup?.Invoke(left, right) ?? default;
            */
            Invocations.Add(new AddMethodInvocation((left, right), result, null));
        }
        catch (Exception ex)
        {
            Invocations.Add(new AddMethodInvocation((left, right), result, ex));
            throw;
        }

        return result;
    }

    public int CountInvocation(TestArg<int> leftCriteria, TestArg<int> rightCriteria)
    {
        return Invocations
            .Count(it => leftCriteria.Predicate(it.Arguments.left) && rightCriteria.Predicate(it.Arguments.right));
    }
}