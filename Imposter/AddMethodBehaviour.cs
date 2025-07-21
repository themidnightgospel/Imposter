namespace Imposter;

public class AddMethodBehaviour
{
    public List<AddMethodInvocation> Invocations { get; } = new();

    // Support multiple return values,
    public List<AddMethodInvocationBehaviour> InvocationBehaviours { get; } = new();

    public int Invoke(int left, int right)
    {
        // Most recently setup behaviour is invoked first
        // Imagine if you return a mock from a method but you want to override it's setup, this will allow it
        var invocationBehaviour = Enumerable.Reverse(InvocationBehaviours).FirstOrDefault(it => it.Matches(left, right));
        int result = default;

        try
        {
            result = invocationBehaviour?.Invoke(left, right) ?? default;
            Invocations.Add(new AddMethodInvocation((left, right), result, null));
        }
        catch (Exception ex)
        {
            Invocations.Add(new AddMethodInvocation((left, right), result, ex));
            throw;
        }

        return result;
    }

    public int CountInvocation(Arg<int> leftCriteria, Arg<int> rightCriteria)
    {
        return Invocations
            .Count(it => leftCriteria.Predicate(it.Arguments.left) && rightCriteria.Predicate(it.Arguments.right));
    }
}