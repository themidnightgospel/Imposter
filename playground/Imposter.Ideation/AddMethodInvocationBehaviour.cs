using System.Collections.Concurrent;

namespace Imposter.Ideation;

public class AddMethodInvocationArgArguments
{
    public AddMethodInvocationArgArguments(TestArg<int> left, TestArg<int> right)
    {
        this.left = left;
        this.right = right;
    }

    public TestArg<int> left { get; set; }

    public TestArg<int> right { get; set; }

    public bool Matches(AddMethodInvocationArguments arguments)
    {
        return left.Predicate(arguments.left) && right.Predicate(arguments.right);
    }
}

public class AddMethodInvocationArguments
{
    public AddMethodInvocationArguments(int left, int right)
    {
        this.left = left;
        this.right = right;
    }

    public int left { get; set; }

    public int right { get; set; }
}

public class AddMethodInvocationSetupBuilder(TestArg<int> left, TestArg<int> right)
{
    /*
    internal static Lazy<AddMethodInvocationSetupBuilder> Default = new Lazy<AddMethodInvocationSetupBuilder>(
        new AddMethodInvocationSetupBuilder(TestArg<int>.IsAny(), TestArg<int>.IsAny())
            .Returns((int a, out int b) =>
            {
                return default(T);
            })
        );
        */
    
    private readonly ConcurrentQueue<MethodInvocationSetup> _callSetups = new();
    private MethodInvocationSetup? _currentlySetupCall;
    private AddMethodInvocationArgArguments _argArguments = new(left, right);

    internal bool Matches(AddMethodInvocationArguments arguments) => _argArguments.Matches(arguments);

    private MethodInvocationSetup GetMethodCallSetup(Func<MethodInvocationSetup, bool> addNew)
    {
        if (_currentlySetupCall is null || addNew(_currentlySetupCall))
        {
            _currentlySetupCall = new MethodInvocationSetup();
            _callSetups.Enqueue(_currentlySetupCall);
        }

        return _currentlySetupCall;
    }

    public AddMethodInvocationSetupBuilder Returns(Func<int, int, int> returns)
    {
        GetMethodCallSetup(it => it.ResultGenerator is not null).ResultGenerator = returns;
        return this;
    }

    public AddMethodInvocationSetupBuilder Throws<TException>()
        where TException : Exception, new()
    {
        GetMethodCallSetup(it => it.ResultGenerator is not null)
            .ResultGenerator = (_, _) => throw new TException();
        return this;
    }

    public AddMethodInvocationSetupBuilder Throws(Exception exception)
    {
        GetMethodCallSetup(it => it.ResultGenerator is not null)
            .ResultGenerator = (_, _) => throw exception;
        return this;
    }

    public AddMethodInvocationSetupBuilder Returns(int result)
    {
        GetMethodCallSetup(it => it.ResultGenerator is not null)
            .ResultGenerator = (_, _) => result;
        return this;
    }

    public AddMethodInvocationSetupBuilder CallBeforeReturn(Action<int, int> callbackBeforeReturn)
    {
        GetMethodCallSetup(it => false).CallBefore = callbackBeforeReturn;
        return this;
    }

    public AddMethodInvocationSetupBuilder CallAfterReturn(Action<int, int> callbackAfterReturn)
    {
        GetMethodCallSetup(it => false).CallAfter = callbackAfterReturn;
        return this;
    }

    private MethodInvocationSetup? _nextMethodCallSetupToInvoke;

    private MethodInvocationSetup? GetNextMethodCallSetupToInvoke()
    {
        if (_callSetups.TryDequeue(out var callSetup))
        {
            _nextMethodCallSetupToInvoke = callSetup;
        }

        return _nextMethodCallSetupToInvoke;
    }

    public int Invoke(int a, int b)
    {
        var nextMethodCallSetupToInvoke = GetNextMethodCallSetupToInvoke();

        if (nextMethodCallSetupToInvoke is null)
        {
            return default(int);
        }

        if (nextMethodCallSetupToInvoke.CallBefore is not null)
        {
            nextMethodCallSetupToInvoke.CallBefore(a, b);
        }

        var result = nextMethodCallSetupToInvoke.ResultGenerator?.Invoke(a, b) ?? default(global::System.Int32);

        if (nextMethodCallSetupToInvoke.CallAfter is not null)
        {
            nextMethodCallSetupToInvoke.CallAfter(a, b);
        }

        return result;
    }

    private class MethodInvocationSetup
    {
        internal Func<int, int, int>? ResultGenerator { get; set; }
        internal Action<int, int>? CallBefore { get; set; }
        internal Action<int, int>? CallAfter { get; set; }
    }
}