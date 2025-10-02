using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Setup.Methods.Generics;
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
    public class ConvertToArguments<TIn, TOut>
    {
        public TIn input { get; }

        public ConvertToArguments(TIn input)
        {
            this.input = input;
        }
    }

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public class ConvertToArgumentsCriteria<TIn, TOut>
    {
        public Arg<TIn> input { get; }

        public ConvertToArgumentsCriteria(Arg<TIn> input)
        {
            this.input = input;
        }

        public bool Matches(ConvertToArguments<TIn, TOut> arguments)
        {
            return input.Matches(arguments.input);
        }
    }

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    public class ConvertToMethodInvocationHistory<TIn, TOut>
    {
        public ConvertToArguments<TIn, TOut> Arguments { get; }
        public TOut? Result { get; }
        public System.Exception? Exception { get; }

        public ConvertToMethodInvocationHistory(ConvertToArguments<TIn, TOut> arguments, TOut? result = default(TOut? ), System.Exception? exception = default(System.Exception? ))
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
        internal static ConvertToMethodInvocationsSetup<TIn, TOut> DefaultInvocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(new ConvertToArgumentsCriteria<TIn, TOut>(Arg<TIn>.Any));
        internal ConvertToArgumentsCriteria<TIn, TOut> ArgumentsCriteria { get; }

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

        public ConvertToMethodInvocationsSetup(ConvertToArgumentsCriteria<TIn, TOut> argumentsCriteria)
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
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) =>
            {
                return value;
            };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) =>
            {
                throw new TException();
            };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) =>
            {
                throw ex;
            };
            return this;
        }

        public IConvertToMethodInvocationsSetup<TIn, TOut> Throws(ConvertToExceptionGeneratorDelegate<TIn, TOut> exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (TIn input) =>
            {
                throw exceptionGenerator(input);
            };
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

    // TOut ISutWithGenericMethod.ConvertTo<TIn, TOut>(TIn input)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ConvertToMethodImposter<TIn, TOut>
    {
        private readonly List<ConvertToMethodInvocationsSetup<TIn, TOut>> _invocationSetups = new List<ConvertToMethodInvocationsSetup<TIn, TOut>>();
        private readonly List<ConvertToMethodInvocationHistory<TIn, TOut>> _invocationHistory = new List<ConvertToMethodInvocationHistory<TIn, TOut>>();
        internal TOut Invoke(TIn input)
        {
            ConvertToArguments<TIn, TOut> arguments = new ConvertToArguments<TIn, TOut>(input);
            ConvertToMethodInvocationsSetup<TIn, TOut>? matchingSetup = null;
            for (int i = _invocationSetups.Count - 1; i >= 0; i--)
            {
                var setup = _invocationSetups[i];
                if (setup.ArgumentsCriteria.Matches(arguments))
                {
                    matchingSetup = setup;
                    break;
                }
            }

            if (matchingSetup == null)
            {
                matchingSetup = ConvertToMethodInvocationsSetup<TIn, TOut>.DefaultInvocationSetup;
            }

            try
            {
                var result = matchingSetup.Invoke(input);
                _invocationHistory.Add(new ConvertToMethodInvocationHistory<TIn, TOut>(arguments, result));
                return result;
            }
            catch (Exception ex)
            {
                _invocationHistory.Add(new ConvertToMethodInvocationHistory<TIn, TOut>(arguments, null, ex));
                throw;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : IConvertToMethodImposterBuilder<TIn, TOut>
        {
            private ConvertToMethodImposter<TIn, TOut> _imposter;
            private ConvertToArgumentsCriteria<TIn, TOut> _argumentsCriteria;
            public Builder(ConvertToMethodImposter<TIn, TOut> _imposter, ConvertToArgumentsCriteria<TIn, TOut> _argumentsCriteria)
            {
                this._imposter = _imposter;
                this._argumentsCriteria = _argumentsCriteria;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Returns(ConvertToDelegate<TIn, TOut> resultGenerator)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Returns(resultGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Returns(TOut value)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Returns(value);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Throws<TException>()
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Throws<TException>();
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Throws(System.Exception ex)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Throws(ex);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.Throws(ConvertToExceptionGeneratorDelegate<TIn, TOut> exceptionGenerator)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.Throws(exceptionGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.CallBefore(ConvertToCallbackDelegate<TIn, TOut> callback)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.CallBefore(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IConvertToMethodInvocationsSetup<TIn, TOut> IConvertToMethodInvocationsSetup<TIn, TOut>.CallAfter(ConvertToCallbackDelegate<TIn, TOut> callback)
            {
                var invocationSetup = new ConvertToMethodInvocationsSetup<TIn, TOut>(_argumentsCriteria);
                invocationSetup.CallAfter(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            void ConvertToMethodInvocationVerifier<TIn, TOut>.Called(Count count)
            {
                var invocationCount = _imposter._invocationHistory.Count(it => _argumentsCriteria.Matches(it.Arguments));
                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException("TODO");
                }
            }
        }
    }
}

namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ISutWithGenericMethodImposter : IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod>
    {
        private ConvertToMethodImposter<TIn, TOut> _convertToMethodImposter;
        public IConvertToMethodImposterBuilder<TIn, TOut> ConvertTo<TIn, TOut>(Arg<TIn> input)
        {
            return new ConvertToMethodImposter.Builder(_convertToMethodImposter, new ConvertToArgumentsCriteria<TIn, TOut>(input));
        }

        private ImposterTargetInstance _imposterInstance;
        public ISutWithGenericMethodImposter()
        {
            this._convertToMethodImposter = new ConvertToMethodImposter<TIn, TOut>();
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod
        {
            ISutWithGenericMethodImposter _imposter;
            public ImposterTargetInstance(ISutWithGenericMethodImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public TOut ConvertTo(TIn input)
            {
                return _imposter._convertToMethodImposter.Invoke(input);
            }
        }

        global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.ISutWithGenericMethod>.Instance()
        {
            return _imposterInstance;
        }
    }
}