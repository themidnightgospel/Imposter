namespace Imposter.Ideation;

public interface IOrderService
{
    int GetOrderCount(int filter);
}

public class GetOrderCountMethodInvocationHistory
{
    public int Result { get; }
    public System.Exception Exception { get; }

    public GetOrderCountMethodInvocationHistory(int result, System.Exception exception)
    {
        Result = result;
        Exception = exception;
    }
}

public class MethodWithOutParameterArguments
{
    public int filter { get; }
    
    public MethodWithOutParameterArguments(int filter)
    {
        this.filter = filter;
    }
}

// int WhenSettingUpMethodReturnValue.ISut.NoParametersWithReturnType()
public delegate int NoParametersWithReturnTypeDelegate();

public class NoParametersWithReturnTypeArgArguments
{
    public NoParametersWithReturnTypeArgArguments()
    {
    }

    public bool Matches(MethodWithOutParameterArguments arguments)
    {
        return true;
    }
}

public delegate void NoParametersWithReturnTypeCallbackDelegate();

public delegate System.Exception NoParametersWithReturnTypeExceptionGeneratorDelegate();

public interface INoParametersWithReturnTypeMethodInvocationsSetupBuilder
{
    INoParametersWithReturnTypeMethodInvocationsSetupBuilder Returns(int value);
}

public interface INoParametersWithReturnTypeMethodInvocationVerifier
{
    bool WasInvoked(int a);
}

public class NoParametersWithReturnTypeMethodInvocationsSetupBuilder : INoParametersWithReturnTypeMethodInvocationsSetupBuilder, INoParametersWithReturnTypeMethodInvocationVerifier
{
    internal static int DefaultResultGenerator()
    {
        return default(int);
    }

    public NoParametersWithReturnTypeMethodInvocationsSetupBuilder(TestArg<int> aArg)
    {
        ArgArguments = new NoParametersWithReturnTypeArgArguments();
    }

    internal NoParametersWithReturnTypeArgArguments ArgArguments { get; }

    private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
    private MethodInvocationSetup? _currentlySetupCall;

    private MethodInvocationSetup GetMethodCallSetup(Func<MethodInvocationSetup, bool> addNew)
    {
        if (_currentlySetupCall is null || addNew(_currentlySetupCall))
        {
            _currentlySetupCall = new MethodInvocationSetup();
            _callSetups.Enqueue(_currentlySetupCall);
        }

        return _currentlySetupCall;
    }

    public INoParametersWithReturnTypeMethodInvocationsSetupBuilder Returns(NoParametersWithReturnTypeDelegate resultGenerator)
    {
        GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
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

    public int Invoke()
    {
        var nextMethodCallSetupToInvoke = GetNextMethodCallSetupToInvoke();
        if (nextMethodCallSetupToInvoke is null)
        {
            return default(int);
        }

        if (nextMethodCallSetupToInvoke.CallBefore is not null)
        {
            nextMethodCallSetupToInvoke.CallBefore();
        }

        var result = nextMethodCallSetupToInvoke.ResultGenerator?.Invoke() ?? default(int);
        if (nextMethodCallSetupToInvoke.CallAfter is not null)
        {
            nextMethodCallSetupToInvoke.CallAfter();
        }

        return result;
    }

    internal class MethodInvocationSetup
    {
        internal NoParametersWithReturnTypeDelegate ResultGenerator { get; set; }
        internal NoParametersWithReturnTypeCallbackDelegate CallBefore { get; set; }
        internal NoParametersWithReturnTypeCallbackDelegate CallAfter { get; set; }
    }
}

public interface INoParametersWithReturnTypeMethodImposter : INoParametersWithReturnTypeMethodInvocationsSetupBuilder, INoParametersWithReturnTypeMethodInvocationVerifier
{
}

internal class NoParametersWithReturnTypeMethodImposter
{
    private readonly List<NoParametersWithReturnTypeMethodInvocationsSetupBuilder> _invocationSetups = new List<NoParametersWithReturnTypeMethodInvocationsSetupBuilder>();
    private readonly List<GetOrderCountMethodInvocationHistory> _invocationHistory = new List<GetOrderCountMethodInvocationHistory>();

    internal class Builder : INoParametersWithReturnTypeMethodImposter
    {
        private readonly NoParametersWithReturnTypeMethodImposter _imposter;
        private readonly TestArg<int> aArg;

        public Builder(
            NoParametersWithReturnTypeMethodImposter imposter,
            TestArg<int> aArg)
        {
            _imposter = imposter;
            this.aArg = aArg;
        }

        public INoParametersWithReturnTypeMethodInvocationsSetupBuilder Returns(int value)
        {
            var setupB = new NoParametersWithReturnTypeMethodInvocationsSetupBuilder(aArg);
            _imposter._invocationSetups.Add(setupB);
            return setupB.Returns(() => value);
        }

        public bool WasInvoked(int a)
        {
            return _imposter.WasInvoked(a);
        }
    }


    private int Invoke()
    {
        NoParametersWithReturnTypeMethodInvocationsSetupBuilder? matchingSetup = null;
        for (int i = _invocationSetups.Count - 1; i >= 0; i--)
        {
            var setup = _invocationSetups[i];
            if (setup.ArgArguments.Matches(arguments))
            {
                matchingSetup = setup;
                break;
            }
        }

        if (matchingSetup == null)
        {
            matchingSetup = NoParametersWithReturnTypeMethodInvocationsSetupBuilder.DefaultInvocationSetup.Value;
        }

        try
        {
            var result = matchingSetup.Invoke();
            _invocationHistory.Add(new GetOrderCountMethodInvocationHistory(arguments, result));
        }
        catch (Exception ex)
        {
            _invocationHistory.Add(new GetOrderCountMethodInvocationHistory(arguments, result, ex));
            throw;
        }
    }

    public bool WasInvoked(int a)
    {
        throw new NotImplementedException();
    }
}

public class TestSut
{
    private readonly NoParametersWithReturnTypeMethodImposter _noParametersWithReturnTypeMethodImposter;
    private readonly ISutImposterInstance _imposterInstance;

    public TestSut()
    {
        _noParametersWithReturnTypeMethodImposter = new();
        _imposterInstance = new ISutImposterInstance(this);
    }

    public INoParametersWithReturnTypeMethodImposter NoParametersWithReturnType(TestArg<int> aArg)
    {
        return new NoParametersWithReturnTypeMethodImposter.Builder(_noParametersWithReturnTypeMethodImposter, aArg);
    }

    public static implicit operator ISutImposterInstance(TestSut sut) => sut._imposterInstance;
}

public class ISutImposterInstance : global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut
{
    private readonly TestSut _imposter;

    public ISutImposterInstance(TestSut imposter)
    {
        _imposter = imposter;
    }

    public global::System.Int32 NoParametersWithReturnType()
    {
        return _imposter._noParametersWithReturnTypeMethodImposter.Invoke();
    }
}

static class Test
{
    static void a()
    {
        new TestSut()
            .NoParametersWithReturnType(TestArg<int>.IsAny())
            .WasInvoked(Int32.MaxValue);
    }
}