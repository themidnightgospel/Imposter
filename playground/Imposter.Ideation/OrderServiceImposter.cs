using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod;

#pragma warning disable nullable
namespace Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod
{
    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public delegate TOut ConvertToDelegate<TIn, TOut>(TIn input);

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public delegate void ConvertToCallbackDelegate<TIn, TOut>(TIn input);

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public delegate System.Exception ConvertToExceptionGeneratorDelegate<TIn, TOut>(TIn input);

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public class ConvertToArgumentsCriteria<TIn>
    {
        public Arg<TIn> input { get; }

        public ConvertToArgumentsCriteria(Arg<TIn> input)
        {
            this.input = input;
        }

        public bool Matches(ConvertToArguments<TIn> arguments)
        {
            return input.Matches(arguments.input);
        }

        public ConvertToArgumentsCriteria<TInResult> Convert<TInResult>()
        {
            return new ConvertToArgumentsCriteria<TInResult>(
                Arg<TInResult>.Is(it => it is TIn tin && input.Matches(tin))
            );
        }
    }

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public class ConvertToMethodInvocationHistory<TIn, TOut>
    {
        public ConvertToArguments<TIn> Arguments { get; }
        public TOut? Result { get; }
        public System.Exception? Exception { get; }

        public ConvertToMethodInvocationHistory(ConvertToArguments<TIn> arguments, TOut? result = default(TOut?), System.Exception? exception = default(System.Exception?))
        {
            Arguments = arguments;
            Result = result;
            Exception = exception;
        }
    }

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ConvertToMethodInvocationsSetup<TIn, TOut> : IConvertToMethodInvocationsSetup<TIn, TOut>
    {
        internal static ConvertToMethodInvocationsSetup<TIn, TOut> DefaultInvocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(new ConvertToArgumentsCriteria<TIn>(Arg<TIn>.Any));
        internal ConvertToArgumentsCriteria<TIn> ArgumentsCriteria { get; }

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

        internal static TOut DefaultResultGenerator(TIn input)
        {
            return default(TOut);
        }

        public ConvertToMethodInvocationsSetup(ConvertToArgumentsCriteria<TIn> argumentsCriteria)
        {
            ArgumentsCriteria = argumentsCriteria;
            GetMethodCallSetup(it => true);
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Returns(ConvertToDelegate<TIn, TOut> resultGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Returns(TOut value)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) => { return value; };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) => { throw new TException(); };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) => { throw ex; };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws(ConvertToExceptionGeneratorDelegate<TIn, TOut> exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) => { throw exceptionGenerator(input); };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> CallBefore(ConvertToCallbackDelegate<TIn, TOut> callback)
        {
            GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> CallAfter(ConvertToCallbackDelegate<TIn, TOut> callback)
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

        public TOut Invoke(TIn input)
        {
            var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
            if (nextSetup.CallBefore is not null)
            {
                nextSetup.CallBefore(input);
            }

            if (nextSetup.ResultGenerator is null)
            {
                nextSetup.ResultGenerator = DefaultResultGenerator;
            }

            var result = nextSetup.ResultGenerator.Invoke(input);
            if (nextSetup.CallAfter is not null)
            {
                nextSetup.CallAfter(input);
            }

            return result;
        }

        internal class MethodInvocationSetup
        {
            internal ConvertToDelegate<TIn, TOut> ResultGenerator { get; set; }
            internal ConvertToCallbackDelegate<TIn, TOut> CallBefore { get; set; }
            internal ConvertToCallbackDelegate<TIn, TOut> CallAfter { get; set; }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public interface IConvertToMethodInvocationsSetup<TIn, TOut>
    {
        public IConvertToMethodInvocationsSetup<TIn, TOut> Returns(ConvertToDelegate<TIn, TOut> resultGenerator);
        public IConvertToMethodInvocationsSetup<TIn, TOut> Returns(TOut value);

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws<TException>()
            where TException : Exception, new();

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws(System.Exception ex);
        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws(ConvertToExceptionGeneratorDelegate<TIn, TOut> exceptionGenerator);
        public IConvertToMethodInvocationsSetup<TIn, TOut> CallBefore(ConvertToCallbackDelegate<TIn, TOut> callback);
        public IConvertToMethodInvocationsSetup<TIn, TOut> CallAfter(ConvertToCallbackDelegate<TIn, TOut> callback);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public interface ConvertToMethodInvocationVerifier<TIn, TOut>
    {
        void Called(Count count);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public interface IConvertToMethodImposterBuilder<TIn, TOut> : IConvertToMethodInvocationsSetup<TIn, TOut>, ConvertToMethodInvocationVerifier<TIn, TOut>
    {
    }

    internal interface IConvertToGenericMethodImposter
    {
        bool Matches(ConvertToTypeArgumentsCriteria criteria);

        IConvertToMethodImposter<TIn, TOut> Convert<TIn, TOut>();
    }

    internal interface IConvertToMethodImposter<TIn, TOut>
    {
        TOut Invoke(TIn input);

        int CountInvocations(ConvertToArgumentsCriteria<TIn> criteria);

        bool HasMatchingSetup(ConvertToArguments<TIn> arguments);
    }

    class ConvertToMethodImposterAdapter<TInTo, TOutTo, TIn, TOut> : IConvertToMethodImposter<TInTo, TOutTo>
    {
        private readonly IConvertToMethodImposter<TIn, TOut> _inner;

        public ConvertToMethodImposterAdapter(ConvertToMethodImposter<TIn, TOut> inner)
        {
            _inner = inner;
        }

        public TOutTo Invoke(TInTo input)
        {
            if (input is TIn inputAsTIn && typeof(TOut).IsAssignableTo(typeof(TOutTo)))
            {
                var result = _inner.Invoke(inputAsTIn);

                if (result is TOutTo resultAsTOutTo)
                {
                    return resultAsTOutTo;
                }

                throw new InvalidOperationException("Test");
            }

            throw new InvalidOperationException("Test");
        }

        public int CountInvocations(ConvertToArgumentsCriteria<TInTo> criteria)
        {
            return _inner.CountInvocations(criteria.Convert<TIn>());
        }

        public bool HasMatchingSetup(ConvertToArguments<TInTo> arguments)
        {
            return _inner.HasMatchingSetup(arguments.Convert<TIn>());
        }
    }

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ConvertToMethodImposter<TIn, TOut> : IConvertToGenericMethodImposter, IConvertToMethodImposter<TIn, TOut>
    {
        private readonly ConcurrentStack<ConvertToMethodInvocationsSetup<TIn, TOut>> _invocationSetups = new ConcurrentStack<ConvertToMethodInvocationsSetup<TIn, TOut>>();
        private readonly List<ConvertToMethodInvocationHistory<TIn, TOut>> _invocationHistory = new List<ConvertToMethodInvocationHistory<TIn, TOut>>();

        bool IConvertToGenericMethodImposter.Matches(ConvertToTypeArgumentsCriteria criteria)
        {
            return criteria.Matches(typeof(TIn), typeof(TOut));
        }

        public IConvertToMethodImposter<TInTo, TOutTo> Convert<TInTo, TOutTo>()
        {
            return new ConvertToMethodImposterAdapter<TInTo, TOutTo, TIn, TOut>(this);
        }

        int IConvertToMethodImposter<TIn, TOut>.CountInvocations(ConvertToArgumentsCriteria<TIn> criteria)
        {
            return _invocationHistory.Count(it => criteria.Matches(it.Arguments));
        }

        bool IConvertToMethodImposter<TIn, TOut>.HasMatchingSetup(ConvertToArguments<TIn> arguments)
        {
            return FindMatchingSetup(arguments) is not null;
        }

        ConvertToMethodInvocationsSetup<TIn, TOut>? FindMatchingSetup(ConvertToArguments<TIn> arguments)
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

        TOut IConvertToMethodImposter<TIn, TOut>.Invoke(TIn input)
        {
            var arguments = new ConvertToArguments<TIn>(input);
            var matchingSetup = FindMatchingSetup(arguments) ?? ConvertToMethodInvocationsSetup<TIn, TOut>.DefaultInvocationSetup;

            try
            {
                var result = matchingSetup.Invoke(input);
                _invocationHistory.Add(new ConvertToMethodInvocationHistory<TIn, TOut>(arguments, result));
                return result;
            }
            catch (Exception ex)
            {
                _invocationHistory.Add(new ConvertToMethodInvocationHistory<TIn, TOut>(arguments, default, ex));
                throw;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : IConvertToMethodImposterBuilder<TIn, TOut>
        {
            private ConvertToMethodImposterCollection _impostersCollection;
            private ConvertToArgumentsCriteria<TIn> _argumentsCriteria;

            public Builder(
                ConvertToMethodImposterCollection convertToMethodImposterCollection,
                ConvertToArgumentsCriteria<TIn> _argumentsCriteria)
            {
                this._impostersCollection = convertToMethodImposterCollection;
                this._argumentsCriteria = _argumentsCriteria;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Returns(ConvertToDelegate<TIn, TOut> resultGenerator)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Returns(resultGenerator);
                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Returns(TOut value)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Returns(value);

                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Throws<TException>()
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Throws<TException>();
                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Throws(System.Exception ex)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Throws(ex);
                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Throws(ConvertToExceptionGeneratorDelegate<TIn, TOut> exceptionGenerator)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Throws(exceptionGenerator);
                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.CallBefore(ConvertToCallbackDelegate<TIn, TOut> callback)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.CallBefore(callback);
                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.CallAfter(ConvertToCallbackDelegate<TIn, TOut> callback)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.CallAfter(callback);
                _impostersCollection.AddNew<TIn, TOut>()._invocationSetups.Push(invocationSetup);
                return invocationSetup;
            }

            void ConvertToMethodInvocationVerifier<TIn, TOut>.Called(Count count)
            {
                var matchingMethodImposters = _impostersCollection.Find<TIn, TOut>();

                var invocationCount = matchingMethodImposters
                    .Sum(it => it.Convert<TIn, TOut>().CountInvocations(_argumentsCriteria));

                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException("TODO");
                }
            }
        }
    }
}

internal class ConvertToMethodImposterCollection
{
    private readonly ConcurrentStack<IConvertToGenericMethodImposter> _imposters = new();

    public ConvertToMethodImposter<TIn, TOut> AddNew<TIn, TOut>()
    {
        var imposter = new ConvertToMethodImposter<TIn, TOut>();
        _imposters.Push(imposter);

        return imposter;
    }

    public IReadOnlyCollection<IConvertToGenericMethodImposter> Find<TIn, TOut>()
    {
        var criteria = new ConvertToTypeArgumentsCriteria(
            TypeCriteria.Create<TIn>(),
            TypeCriteria.Create<TOut>()
        );

        return _imposters.Where(it => it.Matches(criteria)).ToList();
    }
}

namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ISutWithGenericMethodImposter : IHaveImposterInstance<global::Imposter.Ideation.ISutWithGenericMethod>
    {
        // private ConvertToMethodImposter<TIn, TOut> _convertToMethodImposter;
        private ConvertToMethodImposterCollection _convertToMethodImposterCollection;

        public IConvertToMethodImposterBuilder<TIn, TOut> ConvertTo<TIn, TOut>(Arg<TIn> input)
        {
            return new ConvertToMethodImposter<TIn, TOut>.Builder(_convertToMethodImposterCollection, new ConvertToArgumentsCriteria<TIn>(input));
        }

        private ImposterTargetInstance _imposterInstance;

        public ISutWithGenericMethodImposter()
        {
            this._convertToMethodImposterCollection = new ConvertToMethodImposterCollection();
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

            public TOut ConvertTo<TIn, TOut>(TIn input)
            {
                var arguments = new ConvertToArguments<TIn>(input);
                var methodImposter = _imposter._convertToMethodImposterCollection.Find<TIn, TOut>().Select(it => it.Convert<TIn, TOut>());

                var imposterWithMatchingSetup = methodImposter.FirstOrDefault(it => it.HasMatchingSetup(arguments))
                                                ?? _imposter._convertToMethodImposterCollection.AddNew<TIn, TOut>();

                return imposterWithMatchingSetup.Invoke(input);
            }
        }

        global::Imposter.Ideation.ISutWithGenericMethod IHaveImposterInstance<global::Imposter.Ideation.ISutWithGenericMethod>.Instance()
        {
            return _imposterInstance;
        }
    }
}

public class ConvertToArguments<TIn>
{
    public TIn input { get; }

    public ConvertToArguments(TIn input)
    {
        this.input = input;
    }

    public static TOut Convert<TOut>(TIn input)
    {
        var matches = typeof(TOut) switch
        {
            var t when t == typeof(ArgType.Any) => new ArgType.Any(input),
            var t when t == typeof(ArgType.IsAssignableTo<>) => ArgType.IsAssignableTo<TArg>.Matches,
            var t when t == typeof(ArgType.Is<TArg>) => ArgType.Is<TArg>.Matches,
            var t when t == typeof(ArgType.IsSubclassOf<TArg>) => ArgType.IsSubclassOf<TArg>.Matches,
            var t when typeof(IArgType).IsAssignableFrom(t) => type => (bool)GetMatchesMethod(t).Invoke(null, new[] { type }),
            _ => _ => true
        };
    }
    
    public ConvertToArguments<TInResult> Convert<TInResult>()
    {
        if (typeof(IArgType).IsAssignableFrom(typeof(TInResult)))
        {
            
        }
        
        if (input is TInResult inputAsTInResult)
        {
            return new ConvertToArguments<TInResult>(inputAsTInResult);
        }

        throw new InvalidOperationException("Test");
    }
}

public class ConvertToTypeArgumentsCriteria
{
    public ConvertToTypeArgumentsCriteria(TypeCriteria inCriteria, TypeCriteria outCriteria)
    {
        TInCriteria = inCriteria;
        TOutCriteria = outCriteria;
    }

    public TypeCriteria TInCriteria { get; }

    public TypeCriteria TOutCriteria { get; }

    public bool Matches(Type tInType, Type tOutType)
    {
        return TInCriteria.Matches(tInType) && TOutCriteria.Matches(tOutType);
    }
}