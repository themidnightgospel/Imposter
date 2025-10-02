using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Setup.Methods;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups;

#pragma warning disable nullable
namespace Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups
{
    // void ISutForMethodSetups.NoParamsNoReturn()
    public delegate void NoParamsNoReturnDelegate();
    // void ISutForMethodSetups.NoParamsNoReturn()
    public delegate void NoParamsNoReturnCallbackDelegate();
    // void ISutForMethodSetups.NoParamsNoReturn()
    public delegate System.Exception NoParamsNoReturnExceptionGeneratorDelegate();
    // void ISutForMethodSetups.NoParamsNoReturn()
    public class NoParamsNoReturnMethodInvocationHistory
    {
        public System.Exception? Exception { get; }

        public NoParamsNoReturnMethodInvocationHistory(System.Exception? exception = default(System.Exception? ))
        {
            Exception = exception;
        }
    }

    // void ISutForMethodSetups.NoParamsNoReturn()
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class NoParamsNoReturnMethodInvocationsSetup : INoParamsNoReturnMethodInvocationsSetup
    {
        internal static NoParamsNoReturnMethodInvocationsSetup DefaultInvocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
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

        internal static void DefaultResultGenerator()
        {
        }

        public NoParamsNoReturnMethodInvocationsSetup()
        {
            GetMethodCallSetup(it => true);
        }

        public INoParamsNoReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                throw new TException();
            };
            return this;
        }

        public INoParamsNoReturnMethodInvocationsSetup Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                throw ex;
            };
            return this;
        }

        public INoParamsNoReturnMethodInvocationsSetup Throws(NoParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                throw exceptionGenerator();
            };
            return this;
        }

        public INoParamsNoReturnMethodInvocationsSetup CallBefore(NoParamsNoReturnCallbackDelegate callback)
        {
            GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
            return this;
        }

        public INoParamsNoReturnMethodInvocationsSetup CallAfter(NoParamsNoReturnCallbackDelegate callback)
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

        public void Invoke()
        {
            var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
            if (nextSetup.CallBefore is not null)
            {
                nextSetup.CallBefore();
            }

            if (nextSetup.ResultGenerator is null)
            {
                nextSetup.ResultGenerator = DefaultResultGenerator;
            }

            nextSetup.ResultGenerator.Invoke();
            if (nextSetup.CallAfter is not null)
            {
                nextSetup.CallAfter();
            };
        }

        internal class MethodInvocationSetup
        {
            internal NoParamsNoReturnDelegate ResultGenerator { get; set; }
            internal NoParamsNoReturnCallbackDelegate CallBefore { get; set; }
            internal NoParamsNoReturnCallbackDelegate CallAfter { get; set; }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public interface INoParamsNoReturnMethodInvocationsSetup
    {
        public INoParamsNoReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new();
        public INoParamsNoReturnMethodInvocationsSetup Throws(System.Exception ex);
        public INoParamsNoReturnMethodInvocationsSetup Throws(NoParamsNoReturnExceptionGeneratorDelegate exceptionGenerator);
        public INoParamsNoReturnMethodInvocationsSetup CallBefore(NoParamsNoReturnCallbackDelegate callback);
        public INoParamsNoReturnMethodInvocationsSetup CallAfter(NoParamsNoReturnCallbackDelegate callback);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // void ISutForMethodSetups.NoParamsNoReturn()
    public interface NoParamsNoReturnMethodInvocationVerifier
    {
        void Called(Count count);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // void ISutForMethodSetups.NoParamsNoReturn()
    public interface INoParamsNoReturnMethodImposterBuilder : INoParamsNoReturnMethodInvocationsSetup, NoParamsNoReturnMethodInvocationVerifier
    {
    }

    // void ISutForMethodSetups.NoParamsNoReturn()
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class NoParamsNoReturnMethodImposter
    {
        private readonly List<NoParamsNoReturnMethodInvocationsSetup> _invocationSetups = new List<NoParamsNoReturnMethodInvocationsSetup>();
        private readonly List<NoParamsNoReturnMethodInvocationHistory> _invocationHistory = new List<NoParamsNoReturnMethodInvocationHistory>();
        internal void Invoke()
        {
            NoParamsNoReturnMethodInvocationsSetup? matchingSetup = _invocationSetups.Count == 0 ? null : _invocationSetups[_invocationSetups.Count - 1];
            if (matchingSetup == null)
            {
                matchingSetup = NoParamsNoReturnMethodInvocationsSetup.DefaultInvocationSetup;
            }

            try
            {
                matchingSetup.Invoke();
                _invocationHistory.Add(new NoParamsNoReturnMethodInvocationHistory());
            }
            catch (Exception ex)
            {
                _invocationHistory.Add(new NoParamsNoReturnMethodInvocationHistory(ex));
                throw;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : INoParamsNoReturnMethodImposterBuilder
        {
            private NoParamsNoReturnMethodImposter _imposter;
            public Builder(NoParamsNoReturnMethodImposter _imposter)
            {
                this._imposter = _imposter;
            }

            INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.Throws<TException>()
            {
                var invocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
                invocationSetup.Throws<TException>();
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.Throws(System.Exception ex)
            {
                var invocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
                invocationSetup.Throws(ex);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.Throws(NoParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                var invocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
                invocationSetup.Throws(exceptionGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.CallBefore(NoParamsNoReturnCallbackDelegate callback)
            {
                var invocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
                invocationSetup.CallBefore(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.CallAfter(NoParamsNoReturnCallbackDelegate callback)
            {
                var invocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
                invocationSetup.CallAfter(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            void NoParamsNoReturnMethodInvocationVerifier.Called(Count count)
            {
                var invocationCount = _imposter._invocationHistory.Count;
                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException("TODO");
                }
            }
        }
    }

    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public delegate void ParamsNoReturnDelegate(int value);
    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public delegate void ParamsNoReturnCallbackDelegate(int value);
    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public delegate System.Exception ParamsNoReturnExceptionGeneratorDelegate(int value);
    public class ParamsNoReturnArguments
    {
        public int value { get; }

        public ParamsNoReturnArguments(int value)
        {
            this.value = value;
        }
    }

    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public class ParamsNoReturnArgumentsCriteria
    {
        public Arg<int> value { get; }

        public ParamsNoReturnArgumentsCriteria(Arg<int> value)
        {
            this.value = value;
        }

        public bool Matches(ParamsNoReturnArguments arguments)
        {
            return value.Matches(arguments.value);
        }
    }

    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public class ParamsNoReturnMethodInvocationHistory
    {
        public ParamsNoReturnArguments Arguments { get; }
        public System.Exception? Exception { get; }

        public ParamsNoReturnMethodInvocationHistory(ParamsNoReturnArguments arguments, System.Exception? exception = default(System.Exception? ))
        {
            Arguments = arguments;
            Exception = exception;
        }
    }

    // void ISutForMethodSetups.ParamsNoReturn(int value)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ParamsNoReturnMethodInvocationsSetup : IParamsNoReturnMethodInvocationsSetup
    {
        internal static ParamsNoReturnMethodInvocationsSetup DefaultInvocationSetup = new ParamsNoReturnMethodInvocationsSetup(new ParamsNoReturnArgumentsCriteria(Arg<int>.Any));
        internal ParamsNoReturnArgumentsCriteria ArgumentsCriteria { get; }

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

        internal static void DefaultResultGenerator(int value)
        {
        }

        public ParamsNoReturnMethodInvocationsSetup(ParamsNoReturnArgumentsCriteria argumentsCriteria)
        {
            ArgumentsCriteria = argumentsCriteria;
            GetMethodCallSetup(it => true);
        }

        public IParamsNoReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                throw new TException();
            };
            return this;
        }

        public IParamsNoReturnMethodInvocationsSetup Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                throw ex;
            };
            return this;
        }

        public IParamsNoReturnMethodInvocationsSetup Throws(ParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                throw exceptionGenerator(value);
            };
            return this;
        }

        public IParamsNoReturnMethodInvocationsSetup CallBefore(ParamsNoReturnCallbackDelegate callback)
        {
            GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
            return this;
        }

        public IParamsNoReturnMethodInvocationsSetup CallAfter(ParamsNoReturnCallbackDelegate callback)
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

        public void Invoke(int value)
        {
            var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
            if (nextSetup.CallBefore is not null)
            {
                nextSetup.CallBefore(value);
            }

            if (nextSetup.ResultGenerator is null)
            {
                nextSetup.ResultGenerator = DefaultResultGenerator;
            }

            nextSetup.ResultGenerator.Invoke(value);
            if (nextSetup.CallAfter is not null)
            {
                nextSetup.CallAfter(value);
            };
        }

        internal class MethodInvocationSetup
        {
            internal ParamsNoReturnDelegate ResultGenerator { get; set; }
            internal ParamsNoReturnCallbackDelegate CallBefore { get; set; }
            internal ParamsNoReturnCallbackDelegate CallAfter { get; set; }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public interface IParamsNoReturnMethodInvocationsSetup
    {
        public IParamsNoReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new();
        public IParamsNoReturnMethodInvocationsSetup Throws(System.Exception ex);
        public IParamsNoReturnMethodInvocationsSetup Throws(ParamsNoReturnExceptionGeneratorDelegate exceptionGenerator);
        public IParamsNoReturnMethodInvocationsSetup CallBefore(ParamsNoReturnCallbackDelegate callback);
        public IParamsNoReturnMethodInvocationsSetup CallAfter(ParamsNoReturnCallbackDelegate callback);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public interface ParamsNoReturnMethodInvocationVerifier
    {
        void Called(Count count);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // void ISutForMethodSetups.ParamsNoReturn(int value)
    public interface IParamsNoReturnMethodImposterBuilder : IParamsNoReturnMethodInvocationsSetup, ParamsNoReturnMethodInvocationVerifier
    {
    }

    // void ISutForMethodSetups.ParamsNoReturn(int value)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ParamsNoReturnMethodImposter
    {
        private readonly List<ParamsNoReturnMethodInvocationsSetup> _invocationSetups = new List<ParamsNoReturnMethodInvocationsSetup>();
        private readonly List<ParamsNoReturnMethodInvocationHistory> _invocationHistory = new List<ParamsNoReturnMethodInvocationHistory>();
        internal void Invoke(int value)
        {
            ParamsNoReturnArguments arguments = new ParamsNoReturnArguments(value);
            ParamsNoReturnMethodInvocationsSetup? matchingSetup = null;
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
                matchingSetup = ParamsNoReturnMethodInvocationsSetup.DefaultInvocationSetup;
            }

            try
            {
                matchingSetup.Invoke(value);
                _invocationHistory.Add(new ParamsNoReturnMethodInvocationHistory(arguments));
            }
            catch (Exception ex)
            {
                _invocationHistory.Add(new ParamsNoReturnMethodInvocationHistory(arguments, ex));
                throw;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : IParamsNoReturnMethodImposterBuilder
        {
            private ParamsNoReturnMethodImposter _imposter;
            private ParamsNoReturnArgumentsCriteria _argumentsCriteria;
            public Builder(ParamsNoReturnMethodImposter _imposter, ParamsNoReturnArgumentsCriteria _argumentsCriteria)
            {
                this._imposter = _imposter;
                this._argumentsCriteria = _argumentsCriteria;
            }

            IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.Throws<TException>()
            {
                var invocationSetup = new ParamsNoReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Throws<TException>();
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.Throws(System.Exception ex)
            {
                var invocationSetup = new ParamsNoReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Throws(ex);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.Throws(ParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                var invocationSetup = new ParamsNoReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Throws(exceptionGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.CallBefore(ParamsNoReturnCallbackDelegate callback)
            {
                var invocationSetup = new ParamsNoReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.CallBefore(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.CallAfter(ParamsNoReturnCallbackDelegate callback)
            {
                var invocationSetup = new ParamsNoReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.CallAfter(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            void ParamsNoReturnMethodInvocationVerifier.Called(Count count)
            {
                var invocationCount = _imposter._invocationHistory.Count(it => _argumentsCriteria.Matches(it.Arguments));
                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException("TODO");
                }
            }
        }
    }

    // int ISutForMethodSetups.NoParamsReturn()
    public delegate int NoParamsReturnDelegate();
    // int ISutForMethodSetups.NoParamsReturn()
    public delegate void NoParamsReturnCallbackDelegate();
    // int ISutForMethodSetups.NoParamsReturn()
    public delegate System.Exception NoParamsReturnExceptionGeneratorDelegate();
    // int ISutForMethodSetups.NoParamsReturn()
    public class NoParamsReturnMethodInvocationHistory
    {
        public int? Result { get; }
        public System.Exception? Exception { get; }

        public NoParamsReturnMethodInvocationHistory(int? result = default(int? ), System.Exception? exception = default(System.Exception? ))
        {
            Result = result;
            Exception = exception;
        }
    }

    // int ISutForMethodSetups.NoParamsReturn()
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class NoParamsReturnMethodInvocationsSetup : INoParamsReturnMethodInvocationsSetup
    {
        internal static NoParamsReturnMethodInvocationsSetup DefaultInvocationSetup = new NoParamsReturnMethodInvocationsSetup();
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

        internal static int DefaultResultGenerator()
        {
            return default(int);
        }

        public NoParamsReturnMethodInvocationsSetup()
        {
            GetMethodCallSetup(it => true);
        }

        public INoParamsReturnMethodInvocationsSetup Returns(NoParamsReturnDelegate resultGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
            return this;
        }

        public INoParamsReturnMethodInvocationsSetup Returns(int value)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                return value;
            };
            return this;
        }

        public INoParamsReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                throw new TException();
            };
            return this;
        }

        public INoParamsReturnMethodInvocationsSetup Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                throw ex;
            };
            return this;
        }

        public INoParamsReturnMethodInvocationsSetup Throws(NoParamsReturnExceptionGeneratorDelegate exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
            {
                throw exceptionGenerator();
            };
            return this;
        }

        public INoParamsReturnMethodInvocationsSetup CallBefore(NoParamsReturnCallbackDelegate callback)
        {
            GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
            return this;
        }

        public INoParamsReturnMethodInvocationsSetup CallAfter(NoParamsReturnCallbackDelegate callback)
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

        public int Invoke()
        {
            var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
            if (nextSetup.CallBefore is not null)
            {
                nextSetup.CallBefore();
            }

            if (nextSetup.ResultGenerator is null)
            {
                nextSetup.ResultGenerator = DefaultResultGenerator;
            }

            var result = nextSetup.ResultGenerator.Invoke();
            if (nextSetup.CallAfter is not null)
            {
                nextSetup.CallAfter();
            }

            return result;
        }

        internal class MethodInvocationSetup
        {
            internal NoParamsReturnDelegate ResultGenerator { get; set; }
            internal NoParamsReturnCallbackDelegate CallBefore { get; set; }
            internal NoParamsReturnCallbackDelegate CallAfter { get; set; }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public interface INoParamsReturnMethodInvocationsSetup
    {
        public INoParamsReturnMethodInvocationsSetup Returns(NoParamsReturnDelegate resultGenerator);
        public INoParamsReturnMethodInvocationsSetup Returns(int value);
        public INoParamsReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new();
        public INoParamsReturnMethodInvocationsSetup Throws(System.Exception ex);
        public INoParamsReturnMethodInvocationsSetup Throws(NoParamsReturnExceptionGeneratorDelegate exceptionGenerator);
        public INoParamsReturnMethodInvocationsSetup CallBefore(NoParamsReturnCallbackDelegate callback);
        public INoParamsReturnMethodInvocationsSetup CallAfter(NoParamsReturnCallbackDelegate callback);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // int ISutForMethodSetups.NoParamsReturn()
    public interface NoParamsReturnMethodInvocationVerifier
    {
        void Called(Count count);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // int ISutForMethodSetups.NoParamsReturn()
    public interface INoParamsReturnMethodImposterBuilder : INoParamsReturnMethodInvocationsSetup, NoParamsReturnMethodInvocationVerifier
    {
    }

    // int ISutForMethodSetups.NoParamsReturn()
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class NoParamsReturnMethodImposter
    {
        private readonly List<NoParamsReturnMethodInvocationsSetup> _invocationSetups = new List<NoParamsReturnMethodInvocationsSetup>();
        private readonly List<NoParamsReturnMethodInvocationHistory> _invocationHistory = new List<NoParamsReturnMethodInvocationHistory>();
        internal int Invoke()
        {
            NoParamsReturnMethodInvocationsSetup? matchingSetup = _invocationSetups.Count == 0 ? null : _invocationSetups[_invocationSetups.Count - 1];
            if (matchingSetup == null)
            {
                matchingSetup = NoParamsReturnMethodInvocationsSetup.DefaultInvocationSetup;
            }

            try
            {
                var result = matchingSetup.Invoke();
                _invocationHistory.Add(new NoParamsReturnMethodInvocationHistory(result));
                return result;
            }
            catch (Exception ex)
            {
                _invocationHistory.Add(new NoParamsReturnMethodInvocationHistory(null, ex));
                throw;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : INoParamsReturnMethodImposterBuilder
        {
            private NoParamsReturnMethodImposter _imposter;
            public Builder(NoParamsReturnMethodImposter _imposter)
            {
                this._imposter = _imposter;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Returns(NoParamsReturnDelegate resultGenerator)
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.Returns(resultGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Returns(int value)
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.Returns(value);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Throws<TException>()
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.Throws<TException>();
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Throws(System.Exception ex)
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.Throws(ex);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Throws(NoParamsReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.Throws(exceptionGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.CallBefore(NoParamsReturnCallbackDelegate callback)
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.CallBefore(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.CallAfter(NoParamsReturnCallbackDelegate callback)
            {
                var invocationSetup = new NoParamsReturnMethodInvocationsSetup();
                invocationSetup.CallAfter(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            void NoParamsReturnMethodInvocationVerifier.Called(Count count)
            {
                var invocationCount = _imposter._invocationHistory.Count;
                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException("TODO");
                }
            }
        }
    }

    // int ISutForMethodSetups.ParamsReturn(int value)
    public delegate int ParamsReturnDelegate(int value);
    // int ISutForMethodSetups.ParamsReturn(int value)
    public delegate void ParamsReturnCallbackDelegate(int value);
    // int ISutForMethodSetups.ParamsReturn(int value)
    public delegate System.Exception ParamsReturnExceptionGeneratorDelegate(int value);
    public class ParamsReturnArguments
    {
        public int value { get; }

        public ParamsReturnArguments(int value)
        {
            this.value = value;
        }
    }

    // int ISutForMethodSetups.ParamsReturn(int value)
    public class ParamsReturnArgumentsCriteria
    {
        public Arg<int> value { get; }

        public ParamsReturnArgumentsCriteria(Arg<int> value)
        {
            this.value = value;
        }

        public bool Matches(ParamsReturnArguments arguments)
        {
            return value.Matches(arguments.value);
        }
    }

    // int ISutForMethodSetups.ParamsReturn(int value)
    public class ParamsReturnMethodInvocationHistory
    {
        public ParamsReturnArguments Arguments { get; }
        public int? Result { get; }
        public System.Exception? Exception { get; }

        public ParamsReturnMethodInvocationHistory(ParamsReturnArguments arguments, int? result = default(int? ), System.Exception? exception = default(System.Exception? ))
        {
            Arguments = arguments;
            Result = result;
            Exception = exception;
        }
    }

    // int ISutForMethodSetups.ParamsReturn(int value)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ParamsReturnMethodInvocationsSetup : IParamsReturnMethodInvocationsSetup
    {
        internal static ParamsReturnMethodInvocationsSetup DefaultInvocationSetup = new ParamsReturnMethodInvocationsSetup(new ParamsReturnArgumentsCriteria(Arg<int>.Any));
        internal ParamsReturnArgumentsCriteria ArgumentsCriteria { get; }

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

        internal static int DefaultResultGenerator(int value)
        {
            return default(int);
        }

        public ParamsReturnMethodInvocationsSetup(ParamsReturnArgumentsCriteria argumentsCriteria)
        {
            ArgumentsCriteria = argumentsCriteria;
            GetMethodCallSetup(it => true);
        }

        public IParamsReturnMethodInvocationsSetup Returns(ParamsReturnDelegate resultGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
            return this;
        }

        public IParamsReturnMethodInvocationsSetup Returns(int value)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                return value;
            };
            return this;
        }

        public IParamsReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new()
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                throw new TException();
            };
            return this;
        }

        public IParamsReturnMethodInvocationsSetup Throws(System.Exception ex)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                throw ex;
            };
            return this;
        }

        public IParamsReturnMethodInvocationsSetup Throws(ParamsReturnExceptionGeneratorDelegate exceptionGenerator)
        {
            GetMethodCallSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
            {
                throw exceptionGenerator(value);
            };
            return this;
        }

        public IParamsReturnMethodInvocationsSetup CallBefore(ParamsReturnCallbackDelegate callback)
        {
            GetMethodCallSetup(it => it.CallBefore != null).CallBefore = callback;
            return this;
        }

        public IParamsReturnMethodInvocationsSetup CallAfter(ParamsReturnCallbackDelegate callback)
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

        public int Invoke(int value)
        {
            var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
            if (nextSetup.CallBefore is not null)
            {
                nextSetup.CallBefore(value);
            }

            if (nextSetup.ResultGenerator is null)
            {
                nextSetup.ResultGenerator = DefaultResultGenerator;
            }

            var result = nextSetup.ResultGenerator.Invoke(value);
            if (nextSetup.CallAfter is not null)
            {
                nextSetup.CallAfter(value);
            }

            return result;
        }

        internal class MethodInvocationSetup
        {
            internal ParamsReturnDelegate ResultGenerator { get; set; }
            internal ParamsReturnCallbackDelegate CallBefore { get; set; }
            internal ParamsReturnCallbackDelegate CallAfter { get; set; }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public interface IParamsReturnMethodInvocationsSetup
    {
        public IParamsReturnMethodInvocationsSetup Returns(ParamsReturnDelegate resultGenerator);
        public IParamsReturnMethodInvocationsSetup Returns(int value);
        public IParamsReturnMethodInvocationsSetup Throws<TException>()
            where TException : Exception, new();
        public IParamsReturnMethodInvocationsSetup Throws(System.Exception ex);
        public IParamsReturnMethodInvocationsSetup Throws(ParamsReturnExceptionGeneratorDelegate exceptionGenerator);
        public IParamsReturnMethodInvocationsSetup CallBefore(ParamsReturnCallbackDelegate callback);
        public IParamsReturnMethodInvocationsSetup CallAfter(ParamsReturnCallbackDelegate callback);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // int ISutForMethodSetups.ParamsReturn(int value)
    public interface ParamsReturnMethodInvocationVerifier
    {
        void Called(Count count);
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    // int ISutForMethodSetups.ParamsReturn(int value)
    public interface IParamsReturnMethodImposterBuilder : IParamsReturnMethodInvocationsSetup, ParamsReturnMethodInvocationVerifier
    {
    }

    // int ISutForMethodSetups.ParamsReturn(int value)
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    class ParamsReturnMethodImposter
    {
        private readonly List<ParamsReturnMethodInvocationsSetup> _invocationSetups = new List<ParamsReturnMethodInvocationsSetup>();
        private readonly List<ParamsReturnMethodInvocationHistory> _invocationHistory = new List<ParamsReturnMethodInvocationHistory>();
        internal int Invoke(int value)
        {
            ParamsReturnArguments arguments = new ParamsReturnArguments(value);
            ParamsReturnMethodInvocationsSetup? matchingSetup = null;
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
                matchingSetup = ParamsReturnMethodInvocationsSetup.DefaultInvocationSetup;
            }

            try
            {
                var result = matchingSetup.Invoke(value);
                _invocationHistory.Add(new ParamsReturnMethodInvocationHistory(arguments, result));
                return result;
            }
            catch (Exception ex)
            {
                _invocationHistory.Add(new ParamsReturnMethodInvocationHistory(arguments, null, ex));
                throw;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Builder : IParamsReturnMethodImposterBuilder
        {
            private ParamsReturnMethodImposter _imposter;
            private ParamsReturnArgumentsCriteria _argumentsCriteria;
            public Builder(ParamsReturnMethodImposter _imposter, ParamsReturnArgumentsCriteria _argumentsCriteria)
            {
                this._imposter = _imposter;
                this._argumentsCriteria = _argumentsCriteria;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Returns(ParamsReturnDelegate resultGenerator)
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Returns(resultGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Returns(int value)
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Returns(value);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Throws<TException>()
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Throws<TException>();
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Throws(System.Exception ex)
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Throws(ex);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Throws(ParamsReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.Throws(exceptionGenerator);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.CallBefore(ParamsReturnCallbackDelegate callback)
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.CallBefore(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.CallAfter(ParamsReturnCallbackDelegate callback)
            {
                var invocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                invocationSetup.CallAfter(callback);
                _imposter._invocationSetups.Add(invocationSetup);
                return invocationSetup;
            }

            void ParamsReturnMethodInvocationVerifier.Called(Count count)
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

namespace Imposter.CodeGenerator.Tests.Setup.Methods
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ISutForMethodSetupsImposter : IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups>
    {
        private NoParamsNoReturnMethodImposter _noParamsNoReturnMethodImposter;
        private ParamsNoReturnMethodImposter _paramsNoReturnMethodImposter;
        private NoParamsReturnMethodImposter _noParamsReturnMethodImposter;
        private ParamsReturnMethodImposter _paramsReturnMethodImposter;
        public INoParamsNoReturnMethodImposterBuilder NoParamsNoReturn()
        {
            return new NoParamsNoReturnMethodImposter.Builder(_noParamsNoReturnMethodImposter);
        }

        public IParamsNoReturnMethodImposterBuilder ParamsNoReturn(Arg<int> value)
        {
            return new ParamsNoReturnMethodImposter.Builder(_paramsNoReturnMethodImposter, new ParamsNoReturnArgumentsCriteria(value));
        }

        public INoParamsReturnMethodImposterBuilder NoParamsReturn()
        {
            return new NoParamsReturnMethodImposter.Builder(_noParamsReturnMethodImposter);
        }

        public IParamsReturnMethodImposterBuilder ParamsReturn(Arg<int> value)
        {
            return new ParamsReturnMethodImposter.Builder(_paramsReturnMethodImposter, new ParamsReturnArgumentsCriteria(value));
        }

        private ImposterTargetInstance _imposterInstance;
        public ISutForMethodSetupsImposter()
        {
            this._noParamsNoReturnMethodImposter = new NoParamsNoReturnMethodImposter();
            this._paramsNoReturnMethodImposter = new ParamsNoReturnMethodImposter();
            this._noParamsReturnMethodImposter = new NoParamsReturnMethodImposter();
            this._paramsReturnMethodImposter = new ParamsReturnMethodImposter();
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups
        {
            ISutForMethodSetupsImposter _imposter;
            public ImposterTargetInstance(ISutForMethodSetupsImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void NoParamsNoReturn()
            {
                _imposter._noParamsNoReturnMethodImposter.Invoke();
            }

            public void ParamsNoReturn(int value)
            {
                _imposter._paramsNoReturnMethodImposter.Invoke(value);
            }

            public int NoParamsReturn()
            {
                return _imposter._noParamsReturnMethodImposter.Invoke();
            }

            public int ParamsReturn(int value)
            {
                return _imposter._paramsReturnMethodImposter.Invoke(value);
            }
        }

        global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups>.Instance()
        {
            return _imposterInstance;
        }
    }
}