using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod;

#pragma warning disable nullable
namespace Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod
{
    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public delegate int PrintDelegate<TInput>(TInput input, int age);

    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public delegate void PrintCallbackDelegate<TInput>(TInput input, int age);

    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public delegate System.Exception PrintExceptionGeneratorDelegate<TInput>(TInput input, int age);

    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public class PrintArgumentsCriteria<TInput>
    {
        public Arg<TInput> input { get; }

        public Arg<int> age { get; }

        public PrintArgumentsCriteria(Arg<TInput> input, Arg<int> age)
        {
            this.input = input;
            this.age = age;
        }

        public bool Matches(PrintArguments<TInput> arguments)
        {
            return input.Matches(arguments.input);
        }

        public PrintArgumentsCriteria<TInResult> Convert<TInResult>()
        {
            return new PrintArgumentsCriteria<TInResult>(
                // TODO
                Arg<TInResult>.Is(it => it is TInput tin && input.Matches(tin)),
                age
            );
        }
    }

    public interface IPrintMethodInvocationHistory
    {
        bool Matches<TInputCriteria>(PrintArgumentsCriteria<TInputCriteria> criteria);
    }

    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public class PrintMethodInvocationHistory<TInput> : IPrintMethodInvocationHistory
    {
        public PrintArguments<TInput> Arguments { get; }

        public int? Result { get; }

        public System.Exception? Exception { get; }

        public bool Matches<TInputCriteria>(PrintArgumentsCriteria<TInputCriteria> criteria)
        {
            var typeCriteria = new PrintTypeArgumentsCriteria(
                TypeCriteria.Create<TInputCriteria>()
            );

            return typeCriteria.Matches(typeof(TInput)) && criteria.Matches(Arguments.Convert<TInputCriteria>());
        }

        public PrintMethodInvocationHistory(
            PrintArguments<TInput> arguments,
            int? result = default(int?),
            System.Exception? exception = default(System.Exception?))
        {
            Arguments = arguments;
            Result = result;
            Exception = exception;
        }
    }

    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class PrintMethodInvocationsSetup<TInput> : IPrintMethodInvocationsSetup<TInput>
    {
        internal static PrintMethodInvocationsSetup<TInput> DefaultInvocationSetup = new PrintMethodInvocationsSetup<TInput>(new PrintArgumentsCriteria<TInput>(Arg<TInput>.Any, Arg<int>.Any));
        internal PrintArgumentsCriteria<TInput> ArgumentsCriteria { get; }

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

        internal static int DefaultResultGenerator(TInput input, int age)
        {
            return default(int);
        }

        public PrintMethodInvocationsSetup(PrintArgumentsCriteria<TInput> argumentsCriteria)
        {
            ArgumentsCriteria = argumentsCriteria;
            GetMethodCallSetup(it => true);
        }

        public IPrintMethodInvocationsSetup<TInput> Returns(PrintDelegate<TInput> resultGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
            return this;
        }

        public IPrintMethodInvocationsSetup<TInput> Returns(int value)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TInput input, int age) => { return value; };
            return this;
        }

        public IPrintMethodInvocationsSetup<TInput> Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TInput input, int age) => { throw new TException(); };
            return this;
        }

        public IPrintMethodInvocationsSetup<TInput> Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TInput input, int age) => { throw ex; };
            return this;
        }

        public IPrintMethodInvocationsSetup<TInput> Throws(PrintExceptionGeneratorDelegate<TInput> exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TInput input, int age) => { throw exceptionGenerator(input, age); };
            return this;
        }

        public IPrintMethodInvocationsSetup<TInput> CallBefore(PrintCallbackDelegate<TInput> callback)
        {
            GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
            return this;
        }

        public IPrintMethodInvocationsSetup<TInput> CallAfter(PrintCallbackDelegate<TInput> callback)
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

        public int Invoke(TInput input, int age)
        {
            var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
            if (nextSetup.CallBefore is not null)
            {
                nextSetup.CallBefore(input, age);
            }

            if (nextSetup.ResultGenerator is null)
            {
                nextSetup.ResultGenerator = DefaultResultGenerator;
            }

            var result = nextSetup.ResultGenerator.Invoke(input, age);
            if (nextSetup.CallAfter is not null)
            {
                nextSetup.CallAfter(input, age);
            }

            return result;
        }

        internal class MethodInvocationSetup
        {
            internal PrintDelegate<TInput> ResultGenerator { get; set; }
            internal PrintCallbackDelegate<TInput> CallBefore { get; set; }
            internal PrintCallbackDelegate<TInput> CallAfter { get; set; }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public interface IPrintMethodInvocationsSetup<TInput>
    {
        public IPrintMethodInvocationsSetup<TInput> Returns(PrintDelegate<TInput> resultGenerator);
        public IPrintMethodInvocationsSetup<TInput> Returns(int value);

        public IPrintMethodInvocationsSetup<TInput> Throws<TException>()
            where TException : Exception, new();

        public IPrintMethodInvocationsSetup<TInput> Throws(System.Exception ex);
        public IPrintMethodInvocationsSetup<TInput> Throws(PrintExceptionGeneratorDelegate<TInput> exceptionGenerator);
        public IPrintMethodInvocationsSetup<TInput> CallBefore(PrintCallbackDelegate<TInput> callback);
        public IPrintMethodInvocationsSetup<TInput> CallAfter(PrintCallbackDelegate<TInput> callback);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public interface PrintMethodInvocationVerifier<TInput>
    {
        void Called(Count count);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    public interface IPrintMethodImposterBuilder<TInput> : IPrintMethodInvocationsSetup<TInput>, PrintMethodInvocationVerifier<TInput>
    {
    }

    internal interface IPrintGenericMethodImposter
    {
        bool MatchesTypeCriteria<TInputCriteria>();

        IPrintMethodImposter<TInput> Convert<TInput>();

        bool MatchesTypeArguments<TInputCriteria>();
    }

    internal interface IPrintMethodImposter<TInput>
    {
        int Invoke(TInput input, int age);

        bool HasMatchingSetup(PrintArguments<TInput> arguments);
    }

    class PrintMethodImposterAdapter<TSource, TTarget> : IPrintMethodImposter<TSource>
    {
        private readonly IPrintMethodImposter<TTarget> _target;

        public PrintMethodImposterAdapter(PrintMethodImposter<TTarget> target)
        {
            _target = target;
        }

        public int Invoke(TSource input, int age)
        {
            return _target.Invoke(
                ValueConverter.ConvertTo<TTarget>(input),
                age);
        }

        public bool HasMatchingSetup(PrintArguments<TSource> arguments)
        {
            return _target.HasMatchingSetup(arguments.Convert<TTarget>());
        }
    }

    // TOut ISutWithGenericMethod.Print<TIn, TOut>(TIn input)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class PrintMethodImposter<TInput> : IPrintGenericMethodImposter, IPrintMethodImposter<TInput>
    {
        private readonly ConcurrentStack<PrintMethodInvocationsSetup<TInput>> _invocationSetups = new ConcurrentStack<PrintMethodInvocationsSetup<TInput>>();

        bool IPrintGenericMethodImposter.MatchesTypeCriteria<TInputCriteria>()
        {
            // TODO Do it once
            var criteria = new PrintTypeArgumentsCriteria(
                TypeCriteria.Create<TInputCriteria>()
            );

            return criteria.Matches(typeof(TInput));
        }

        bool IPrintGenericMethodImposter.MatchesTypeArguments<TInputCriteria>()
        {
            // TODO Do it once
            var criteria = new PrintTypeArgumentsCriteria(
                TypeCriteria.Create<TInput>()
            );

            return criteria.Matches(typeof(TInputCriteria));
        }

        public IPrintMethodImposter<TInTo> Convert<TInTo>()
        {
            return new PrintMethodImposterAdapter<TInTo, TInput>(this);
        }

        bool IPrintMethodImposter<TInput>.HasMatchingSetup(PrintArguments<TInput> arguments)
        {
            return FindMatchingSetup(arguments) is not null;
        }

        PrintMethodInvocationsSetup<TInput>? FindMatchingSetup(PrintArguments<TInput> arguments)
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

        int IPrintMethodImposter<TInput>.Invoke(TInput input, int age)
        {
            var arguments = new PrintArguments<TInput>(input, age);
            var matchingSetup = FindMatchingSetup(arguments) ?? PrintMethodInvocationsSetup<TInput>.DefaultInvocationSetup;

            return matchingSetup.Invoke(input, age);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : IPrintMethodImposterBuilder<TInput>
        {
            private PrintMethodImposterCollection _impostersCollection;
            private PrintMethodInvocationsCollection _printMethodInvocationsCollection;
            private PrintArgumentsCriteria<TInput> _argumentsCriteria;

            public Builder(
                PrintMethodImposterCollection printMethodImposterCollection,
                PrintArgumentsCriteria<TInput> _argumentsCriteria,
                PrintMethodInvocationsCollection printMethodInvocationsCollection)
            {
                this._impostersCollection = printMethodImposterCollection;
                this._argumentsCriteria = _argumentsCriteria;
                _printMethodInvocationsCollection = printMethodInvocationsCollection;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.Returns(PrintDelegate<TInput> resultGenerator)
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.Returns(resultGenerator);
                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.Returns(int value)
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.Returns(value);

                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.Throws<TException>()
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.Throws<TException>();
                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.Throws(System.Exception ex)
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.Throws(ex);
                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.Throws(PrintExceptionGeneratorDelegate<TInput> exceptionGenerator)
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.Throws(exceptionGenerator);
                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.CallBefore(PrintCallbackDelegate<TInput> callback)
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.CallBefore(callback);
                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IPrintMethodInvocationsSetup<TInput> IPrintMethodInvocationsSetup<TInput>.CallAfter(PrintCallbackDelegate<TInput> callback)
            {
                var invocationSetup = new PrintMethodInvocationsSetup<TInput>(_argumentsCriteria);
                invocationSetup.CallAfter(callback);
                _impostersCollection.AddNew<TInput>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            void PrintMethodInvocationVerifier<TInput>.Called(Count count)
            {
                var invocationCount = _printMethodInvocationsCollection.Count<TInput>(_argumentsCriteria);

                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException($"Expected {count} invocations, but was {invocationCount}");
                }
            }
        }
    }
}

internal class PrintMethodImposterCollection
{
    private readonly ConcurrentStack<IPrintGenericMethodImposter> _imposters = new();

    public PrintMethodImposter<TInput> AddNew<TInput>()
    {
        var imposter = new PrintMethodImposter<TInput>();
        _imposters.Push(imposter);

        return imposter;
    }

    public IReadOnlyCollection<IPrintGenericMethodImposter> Find<TInputCriteria>()
    {
        return _imposters.Where(it => it.MatchesTypeArguments<TInputCriteria>()).ToList();
    }

    public IReadOnlyCollection<IPrintGenericMethodImposter> FindByCriteria<TInputCriteria>()
    {
        return _imposters.Where(it => it.MatchesTypeCriteria<TInputCriteria>()).ToList();
    }
}

// TODO move to shared
internal class PrintMethodInvocationsCollection<TInvocationHistory>
{
}

internal class PrintMethodInvocationsCollection
{
    private readonly ConcurrentStack<IPrintMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IPrintMethodInvocationHistory>();

    internal void Add(IPrintMethodInvocationHistory invocationHistory)
    {
        _invocationHistory.Push(invocationHistory);
    }

    internal int Count<TInput>(PrintArgumentsCriteria<TInput> argumentsCriteria)
    {
        return _invocationHistory.Count(it => it.Matches<TInput>(argumentsCriteria));
    }
}

namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ISutWithGenericMethodImposter : IHaveImposterInstance<global::Imposter.Ideation.ISutWithGenericMethod>
    {
        private PrintMethodImposterCollection _printMethodImposterCollection;
        private PrintMethodInvocationsCollection _printMethodInvocationsCollection;

        public IPrintMethodImposterBuilder<TInput> Print<TInput>(Arg<TInput> input, Arg<int> age)
        {
            return new PrintMethodImposter<TInput>.Builder(_printMethodImposterCollection, new PrintArgumentsCriteria<TInput>(input, age), _printMethodInvocationsCollection);
        }

        private ImposterTargetInstance _imposterInstance;

        public ISutWithGenericMethodImposter()
        {
            this._printMethodInvocationsCollection = new PrintMethodInvocationsCollection();
            this._printMethodImposterCollection = new PrintMethodImposterCollection();
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Ideation.ISutWithGenericMethod
        {
            ISutWithGenericMethodImposter _imposter;

            public ImposterTargetInstance(ISutWithGenericMethodImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Print<TInput>(TInput input, int age)
            {
                var methodImposter = _imposter._printMethodImposterCollection.Find<TInput>().Select(it => it.Convert<TInput>());

                var arguments = new PrintArguments<TInput>(input, age);
                var imposterWithMatchingSetup = methodImposter.FirstOrDefault(it => it.HasMatchingSetup(arguments))
                                                ?? _imposter._printMethodImposterCollection.AddNew<TInput>();

                try
                {
                    var result = imposterWithMatchingSetup.Invoke(input, age);
                    _imposter._printMethodInvocationsCollection.Add(new PrintMethodInvocationHistory<TInput>(new PrintArguments<TInput>(input, age), result));

                    return result;
                }
                catch (Exception ex)
                {
                    _imposter._printMethodInvocationsCollection.Add(new PrintMethodInvocationHistory<TInput>(new PrintArguments<TInput>(input, age), null, ex));
                    throw;
                }
            }
        }

        global::Imposter.Ideation.ISutWithGenericMethod IHaveImposterInstance<global::Imposter.Ideation.ISutWithGenericMethod>.Instance()
        {
            return _imposterInstance;
        }
    }
}

public class PrintArguments<TInput>
{
    public TInput input { get; }

    public int age { get; }

    public PrintArguments(TInput input, int age)
    {
        this.input = input;
        this.age = age;
    }

    public PrintArguments<TInResult> Convert<TInResult>()
    {
        return new PrintArguments<TInResult>(
            ValueConverter.ConvertTo<TInResult>(input),
            age
        );
    }
}

public class PrintTypeArgumentsCriteria
{
    public PrintTypeArgumentsCriteria(TypeCriteria inCriteria)
    {
        TInCriteria = inCriteria;
    }

    public TypeCriteria TInCriteria { get; }

    public bool Matches(Type tInType)
    {
        return TInCriteria.Matches(tInType);
    }
}