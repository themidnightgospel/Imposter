namespace Imposter.Ideation;

public class AddMethodInvocationBehaviour(Func<int, int, bool> parameterCriteria)
{
    public int InvocationCounter;

    private readonly List<Func<int, int, int>> _returns = new();
    private Action<int, int>? _callbackBeforeReturn;
    private Action<int, int>? _callbackAfterReturn;

    internal bool Matches(int left, int right) => parameterCriteria(left, right);

    public AddMethodInvocationBehaviour Returns(Func<int, int, int> returns)
    {
        _returns.Add(returns);
        return this;
    }

    public AddMethodInvocationBehaviour Throws<TException>()
        where TException : Exception, new()
    {
        _returns.Add((_, _) => throw new TException());
        return this;
    }

    public AddMethodInvocationBehaviour Throws(Exception exception)
    {
        _returns.Add((_, _) => throw exception);
        return this;
    }

    public AddMethodInvocationBehaviour ThrowsSequence(IEnumerable<Exception> exceptions)
    {
        foreach (var exception in exceptions)
        {
            _returns.Add((_, _) => throw exception);
        }

        return this;
    }

    public AddMethodInvocationBehaviour Returns(int result)
    {
        _returns.Add((_, _) => result);
        return this;
    }

    public AddMethodInvocationBehaviour ReturnsSequence(IEnumerable<int> results)
    {
        foreach (var result in results)
        {
            _returns.Add((_, _) => result);
        }

        return this;
    }

    public AddMethodInvocationBehaviour CallBeforeReturn(Action<int, int> callbackBeforeReturn)
    {
        _callbackBeforeReturn = callbackBeforeReturn;
        return this;
    }

    public AddMethodInvocationBehaviour CallAfterReturn(Action<int, int> callbackAfterReturn)
    {
        _callbackAfterReturn = callbackAfterReturn;
        return this;
    }

    public int Invoke(int left, int right)
    {
        ++InvocationCounter;

        if (_callbackBeforeReturn is not null)
        {
            _callbackBeforeReturn.Invoke(left, right);
        }

        // TODO If exception happens
        var result = _returns.Count >= InvocationCounter
            ? _returns[InvocationCounter - 1].Invoke(left, right)
            : _returns.Last().Invoke(left, right);

        if (_callbackAfterReturn is not null)
        {
            _callbackAfterReturn.Invoke(left, right);
        }

        return result;
    }
}