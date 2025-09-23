namespace Imposter.Ideation;

public class NoParametersWithReturnTypeMethodInvocationHistory
{
    public int Result { get; }
    public System.Exception Exception { get; }

    public NoParametersWithReturnTypeMethodInvocationHistory(int result, System.Exception exception)
    {
        Result = result;
        Exception = exception;
    }
}

public class MethodWithOutParameterArguments
{
    public MethodWithOutParameterArguments()
    {
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
    NoParametersWithReturnTypeMethodInvocationsSetupBuilder Returns(int value);
}

public interface INoParametersWithReturnTypeMethodInvocationVerifier
{
    bool WasInvoked(int a);
}

public class NoParametersWithReturnTypeMethodInvocationsSetupBuilder : INoParametersWithReturnTypeMethodInvocationsSetupBuilder, INoParametersWithReturnTypeMethodInvocationVerifier
{
    internal static Lazy<NoParametersWithReturnTypeMethodInvocationsSetupBuilder> DefaultInvocationSetup =
        new(new NoParametersWithReturnTypeMethodInvocationsSetupBuilder().Returns(DefaultResultGenerator));

    internal static int DefaultResultGenerator()
    {
        return default(int);
    }

    public NoParametersWithReturnTypeMethodInvocationsSetupBuilder()
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

    public NoParametersWithReturnTypeMethodInvocationsSetupBuilder Returns(NoParametersWithReturnTypeDelegate resultGenerator)
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

public class NoParametersWithReturnTypeMethodImposter : INoParametersWithReturnTypeMethodInvocationsSetupBuilder, INoParametersWithReturnTypeMethodInvocationVerifier
{
    private readonly List<NoParametersWithReturnTypeMethodInvocationsSetupBuilder> _invocationSetups = new List<NoParametersWithReturnTypeMethodInvocationsSetupBuilder>();
    private readonly List<NoParametersWithReturnTypeMethodInvocationHistory> _invocationHistory = new List<NoParametersWithReturnTypeMethodInvocationHistory>();

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
            _invocationHistory.Add(new NoParametersWithReturnTypeMethodInvocationHistory(arguments, result));
        }
        catch (Exception ex)
        {
            _invocationHistory.Add(new NoParametersWithReturnTypeMethodInvocationHistory(arguments, result, ex));
            throw;
        }
    }

    public INoParametersWithReturnTypeMethodInvocationsSetupBuilder Returns(int value)
    {
        var builder = new NoParametersWithReturnTypeMethodInvocationsSetupBuilder();
        _invocationSetups.Add(builder);
        return builder;
    }

    public bool WasInvoked(int a)
    {
        throw new NotImplementedException();
    }
}

public class ISutImposter
{
    private readonly NoParametersWithReturnTypeMethodImposter _noParametersWithReturnTypeMethodImposter;
    private readonly global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut _imposterInstance;

    public ISutImposter()
    {
        _noParametersWithReturnTypeMethodImposter = new();
        _imposterInstance = new ISutImposterInstance(this);
    }

    global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut>.Instance() => _imposterInstance;

    ISutImposterVerifier IHaveImposterVerifier<ISutImposterVerifier>.Verify() => _verifier;

    public NoParametersWithReturnTypeMethodInvocationsSetupBuilder NoParametersWithReturnType()
    {
        var invocationBehaviour = new NoParametersWithReturnTypeMethodInvocationsSetupBuilder();
        _noParametersWithReturnTypeMethodImposter.Behaviours.Add(invocationBehaviour);
        return invocationBehaviour;
    }
}

public class ISutImposterInstance : global::Imposter.CodeGenerator.Tests.Setup.WhenSettingUpMethodReturnValue.ISut
{
    private readonly ISutImposter _imposter;

    public ISutImposterInstance(ISutImposter imposter)
    {
        _imposter = imposter;
    }

    public global::System.Int32 NoParametersWithReturnType()
    {
        return _imposter._noParametersWithReturnTypeMethodImposter.Invoke();
    }
}