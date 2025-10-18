using System.Collections.Concurrent;
using Imposter.Abstractions;

namespace Imposter.Ideation.GenericMethod;
#pragma warning disable nullable
// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public delegate TResult PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);

// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public delegate void PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);

// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public delegate System.Exception PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);

public class PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>
{
    public TOrdinaryParam ordinaryParam { get; }
    public TInParam inParam { get; }
    public TRefParam refParam { get; }
    public TParamsParam[] paramsParam { get; }

    public PrintArguments(TOrdinaryParam ordinaryParam, TInParam inParam, TRefParam refParam, TParamsParam[] paramsParam)
    {
        this.ordinaryParam = ordinaryParam;
        this.inParam = inParam;
        this.refParam = refParam;
        this.paramsParam = paramsParam;
    }

    public PrintArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget>
        As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget>()
    {
        return new PrintArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget>(
            TypeCaster.Cast<TOrdinaryParam, TOrdinaryParamTarget>(ordinaryParam),
            TypeCaster.Cast<TInParam, TInParamTarget>(inParam),
            TypeCaster.Cast<TRefParam, TRefParamTarget>(refParam),
            TypeCaster.Cast<TParamsParam[], TParamsParamTarget[]>(paramsParam)
        );
    }
}

// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public class PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>
{
    public Arg<TOrdinaryParam> ordinaryParam { get; }
    public Arg<TInParam> inParam { get; }
    public Arg<TRefParam> refParam { get; }
    public Arg<TParamsParam[]> paramsParam { get; }

    public PrintArgumentsCriteria(Arg<TOrdinaryParam> ordinaryParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
    {
        this.ordinaryParam = ordinaryParam;
        this.inParam = inParam;
        this.refParam = refParam;
        this.paramsParam = paramsParam;
    }

    public bool Matches(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> arguments)
    {
        return ordinaryParam.Matches(arguments.ordinaryParam)
               // && outParam.Matches(arguments.outParam)
               && inParam.Matches(arguments.inParam)
               && refParam.Matches(arguments.refParam)
               && paramsParam.Matches(arguments.paramsParam);
    }

    public PrintArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget>
        As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget>()
    {
        return new PrintArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget>(
            Arg<TOrdinaryParamTarget>.Is(it =>
                TypeCaster.TryCast<TOrdinaryParamTarget, TOrdinaryParam>(it, out var ordinaryParamTarget) && ordinaryParam.Matches(ordinaryParamTarget)),
            Arg<TInParamTarget>.Is(it => TypeCaster.TryCast<TInParamTarget, TInParam>(it, out var inParamTarget) && inParam.Matches(inParamTarget)),
            Arg<TRefParamTarget>.Is(it => TypeCaster.TryCast<TRefParamTarget, TRefParam>(it, out var refParamTarget) && refParam.Matches(refParamTarget)),
            Arg<TParamsParamTarget[]>.Is(it => TypeCaster.TryCast<TParamsParamTarget[], TParamsParam[]>(it, out var paramsParamTarget) && paramsParam.Matches(paramsParamTarget))
        );
    }
}

public interface IPrintMethodInvocationHistory
{
    bool Matches<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>(PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> criteria);
}

// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public class PrintMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodInvocationHistory
{
    public PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> Arguments { get; }
    public TResult? Result { get; }
    public System.Exception? Exception { get; }

    public PrintMethodInvocationHistory(
        PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> arguments,
        TResult? result = default(TResult?), System.Exception? exception = default(System.Exception?))
    {
        Arguments = arguments;
        Result = result;
        Exception = exception;
    }

    public bool Matches<TOrdinaryParamCriteria, TOutParamCriteria, TInParamCriteria, TRefParamCriteria, TParamsParamCriteria>(
        PrintArgumentsCriteria<TOrdinaryParamCriteria, TOutParamCriteria, TInParamCriteria, TRefParamCriteria, TParamsParamCriteria> criteria)
    {
        // Not Return type TResult
        return criteria.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>().Matches(Arguments);
    }
}

internal class PrintMethodInvocationHistoryCollection
{
    private readonly ConcurrentStack<IPrintMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IPrintMethodInvocationHistory>();

    internal void Add(IPrintMethodInvocationHistory invocationHistory)
    {
        _invocationHistory.Push(invocationHistory);
    }

    internal int Count<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>(PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> argumentsCriteria)
    {
        return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
    }
}

// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
[global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
class PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
{
    internal static PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> DefaultInvocationSetup = new PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(
        new PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>(Arg<TOrdinaryParam>.Any, Arg<TInParam>.Any, Arg<TRefParam>.Any, Arg<TParamsParam[]>.Any));

    internal PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> ArgumentsCriteria { get; }

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

    internal static TResult DefaultResultGenerator(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
    {
        InitializeOutParametersWithDefaultValues(out outParam);
        return default(TResult);
    }

    public PrintMethodInvocationsSetup(PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> argumentsCriteria)
    {
        ArgumentsCriteria = argumentsCriteria;
        GetMethodCallSetup(it => true);
    }

    private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
    {
        outParam = default(TOutParam);
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
    {
        GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
        return this;
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value)
    {
        GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
        {
            InitializeOutParametersWithDefaultValues(out outParam);
            return value;
        };
        return this;
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
        where TException : Exception, new()
    {
        GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
        {
            InitializeOutParametersWithDefaultValues(out outParam);
            throw new TException();
        };
        return this;
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex)
    {
        GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
        {
            InitializeOutParametersWithDefaultValues(out outParam);
            throw ex;
        };
        return this;
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
    {
        GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) => { throw exceptionGenerator(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam); };
        return this;
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
    {
        GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
        return this;
    }

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
    {
        GetMethodCallSetup(it => it.CallAfter != null).CallAfter = callback;
        return this;
    }

    private MethodInvocationSetup? _nextSetup;

    private MethodInvocationSetup? GetNextSetup()
    {
        if (_callSetups.TryDequeue(out var callSetup))
        {
            _nextSetup = callSetup;
        }

        return _nextSetup;
    }

    public TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
    {
        var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
        if (nextSetup.CallBefore is not null)
        {
            nextSetup.CallBefore(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
        }

        if (nextSetup.ResultGenerator is null)
        {
            nextSetup.ResultGenerator = DefaultResultGenerator;
        }

        var result = nextSetup.ResultGenerator.Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
        if (nextSetup.CallAfter is not null)
        {
            nextSetup.CallAfter(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
        }

        return result;
    }

    internal class MethodInvocationSetup
    {
        internal PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> ResultGenerator { get; set; }
        internal PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore { get; set; }
        internal PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter { get; set; }
    }
}

[global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
public interface IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
{
    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator);
    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value);

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
        where TException : Exception, new();

    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex);
    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator);
    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
    public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
}

[global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public interface PrintMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
{
    void Called(Count count);
}

[global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
// TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
public interface IPrintMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>, PrintMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
{
}

public interface IPrintMethodImposter
{
    IPrintMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>?
        As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>();
}

public interface IPrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodImposter
{
    TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);

    bool HasMatchingSetup(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> arguments);
}

internal class PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
{
    private readonly ConcurrentStack<PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>> _invocationSetups
        = new ConcurrentStack<PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>>();

    private readonly PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;

    public PrintMethodImposter(PrintMethodInvocationHistoryCollection printMethodInvocationHistoryCollection)
    {
        _printMethodInvocationHistoryCollection = printMethodInvocationHistoryCollection;
    }

    private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
    {
        outParam = default(TOutParam);
    }

    IPrintMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? IPrintMethodImposter.As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
    {
        if (typeof(TOrdinaryParamTarget).IsAssignableTo(typeof(TOrdinaryParam))
            && typeof(TOutParam).IsAssignableTo(typeof(TOutParamTarget))
            && typeof(TInParamTarget).IsAssignableTo(typeof(TInParam))
            && typeof(TRefParamTarget) == typeof(TRefParam)
            && typeof(TResult).IsAssignableTo(typeof(TResultTarget))
           )
        {
            return new Adapter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(this);
        }

        return null;
    }

    private class Adapter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>
        : IPrintMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>
    {
        private readonly PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _target;

        public Adapter(PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> target)
        {
            _target = target;
        }

        public TResultTarget Invoke(TOrdinaryParamTarget ordinaryParam, out TOutParamTarget outParam, in TInParamTarget inParam, ref TRefParamTarget refParam, TParamsParamTarget[] paramsParam)
        {
            // TODO Adapted might not be unique
            var refParamAdapted = TypeCaster.Cast<TRefParamTarget, TRefParam>(refParam);
            var inParamAdapted = TypeCaster.Cast<TInParamTarget, TInParam>(inParam);
            var result = _target.Invoke(
                TypeCaster.Cast<TOrdinaryParamTarget, TOrdinaryParam>(ordinaryParam),
                out var outParamAdapted,
                in inParamAdapted,
                ref refParamAdapted,
                TypeCaster.Cast<TParamsParamTarget[], TParamsParam[]>(paramsParam)
            );

            outParam = TypeCaster.Cast<TOutParam, TOutParamTarget>(outParamAdapted);
            refParam = TypeCaster.Cast<TRefParam, TRefParamTarget>(refParamAdapted);

            return TypeCaster.Cast<TResult, TResultTarget>(result);
        }

        public bool HasMatchingSetup(PrintArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TParamsParamTarget> arguments)
        {
            return _target.HasMatchingSetup(arguments.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>());
        }

        public IPrintMethodImposter<TOrdinaryParamTarget1, TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1> As<TOrdinaryParamTarget1, TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>()
        {
            throw new NotImplementedException();
        }
    }

    public bool HasMatchingSetup(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> arguments)
    {
        return FindMatchingSetup(arguments) is not null;
    }

    PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? FindMatchingSetup(
        PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> arguments)
    {
        foreach (var setup in _invocationSetups)
        {
            if (setup.ArgumentsCriteria.Matches(arguments))
            {
                return setup;
            }
        }

        return null;
    }

    public TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
    {
        var arguments = new PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam);

        try
        {
            var matchingSetup = FindMatchingSetup(arguments) ?? PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.DefaultInvocationSetup;

            var result = matchingSetup.Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);

            _printMethodInvocationHistoryCollection.Add(new PrintMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, result));

            return result;
        }
        catch (Exception ex)
        {
            _printMethodInvocationHistoryCollection.Add(new PrintMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, default, ex));
            throw;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    internal class Builder : IPrintMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
    {
        private readonly PrintMethodImposterCollection _printMethodImposterCollection;
        private readonly PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;
        private readonly PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> _argumentsCriteria;

        private PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? existingInvocationSetup;

        public Builder(
            PrintMethodImposterCollection printMethodImposterCollection,
            PrintMethodInvocationHistoryCollection printMethodInvocationHistoryCollection,
            PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> argumentsCriteria)
        {
            this._printMethodImposterCollection = printMethodImposterCollection;
            this._printMethodInvocationHistoryCollection = printMethodInvocationHistoryCollection;
            this._argumentsCriteria = argumentsCriteria;
        }

        private PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetOrAddInvocationSetup()
        {
            if (existingInvocationSetup is null)
            {
                existingInvocationSetup = new PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);

                // If method is generic
                _printMethodImposterCollection.AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()._invocationSetups.Push(existingInvocationSetup);

                // If not then there will be _imposter variable and we should do instead
                // _imposter._invocationSetups.Push(existingInvocationSetup)
            }

            return existingInvocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.Returns(resultGenerator);

            return invocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(TResult value)
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.Returns(value);

            return invocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws<TException>()
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.Throws<TException>();

            return invocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(System.Exception ex)
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.Throws(ex);

            return invocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.Throws(exceptionGenerator);

            return invocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallBefore(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.CallBefore(callback);

            return invocationSetup;
        }

        IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallAfter(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
        {
            var invocationSetup = GetOrAddInvocationSetup();
            invocationSetup.CallAfter(callback);

            return invocationSetup;
        }

        void PrintMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Called(Count count)
        {
            var invocationCount = _printMethodInvocationHistoryCollection.Count(_argumentsCriteria);

            if (!count.Matches(invocationCount))
            {
                throw new VerificationFailedException("TODO");
            }
        }
    }
}

internal class PrintMethodImposterCollection
{
    private readonly PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;
    private readonly ConcurrentStack<IPrintMethodImposter> _imposters = new();

    public PrintMethodImposterCollection(PrintMethodInvocationHistoryCollection printMethodInvocationHistoryCollection)
    {
        _printMethodInvocationHistoryCollection = printMethodInvocationHistoryCollection;
    }

    public PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()
    {
        var imposter = new PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_printMethodInvocationHistoryCollection);
        _imposters.Push(imposter);

        return imposter;
    }

    public IPrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetImposterWithMatchingSetup
        <TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(
            PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam> arguments)
    {
        // TODO Not thread safe
        return _imposters
                   .Select(it => it.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>())
                   .Where(it => it is not null)
                   .Select(it => it!)
                   .FirstOrDefault(it => it.HasMatchingSetup(arguments))
               ?? AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()
            ;
    }
}

[global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
public class GenericMethodPoc : IHaveImposterInstance<ISutWithGenericMethod>
{
    private readonly PrintMethodImposterCollection _printMethodImposterCollection;
    private readonly PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;

    public IPrintMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(
        Arg<TOrdinaryParam> ordinaryParam, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
    {
        return new PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Builder(
            _printMethodImposterCollection,
            _printMethodInvocationHistoryCollection,
            new PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam));
    }

    private ImposterTargetInstance _imposterInstance;

    public GenericMethodPoc()
    {
        this._printMethodInvocationHistoryCollection = new PrintMethodInvocationHistoryCollection();
        this._printMethodImposterCollection = new PrintMethodImposterCollection(_printMethodInvocationHistoryCollection);
        this._imposterInstance = new ImposterTargetInstance(this);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ImposterTargetInstance : global::Imposter.Ideation.GenericMethod.ISutWithGenericMethod
    {
        GenericMethodPoc _imposter;

        public ImposterTargetInstance(GenericMethodPoc _imposter)
        {
            this._imposter = _imposter;
        }

        public TResult Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(
            TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        {
            return _imposter
                ._printMethodImposterCollection
                .GetImposterWithMatchingSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
                    (new PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam))
                .Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
        }
    }

    global::Imposter.Ideation.GenericMethod.ISutWithGenericMethod IHaveImposterInstance<global::Imposter.Ideation.GenericMethod.ISutWithGenericMethod>.Instance()
    {
        return _imposterInstance;
    }
}

public interface ISutWithGenericMethod
{
    TResult Print<
        TOrdinaryParam,
        TOutParam,
        TInParam,
        TRefParam,
        TResult,
        TParamsParam
    >(
        TOrdinaryParam ordinaryParam,
        out TOutParam outParam,
        in TInParam inParam,
        ref TRefParam refParam,
        TParamsParam[] paramsParam
    );
}