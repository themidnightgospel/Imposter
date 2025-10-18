using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Setup.Methods;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Setup.Methods
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ISutForMethodSetupsImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups>
    {
        private readonly NoParamsNoReturnMethodImposter _noParamsNoReturnMethodImposter;
        private readonly ParamsNoReturnMethodImposter _paramsNoReturnMethodImposter;
        private readonly NoParamsReturnMethodImposter _noParamsReturnMethodImposter;
        private readonly ParamsReturnMethodImposter _paramsReturnMethodImposter;
        private readonly NoParamsNoReturnMethodInvocationHistoryCollection _noParamsNoReturnMethodInvocationHistoryCollection = new NoParamsNoReturnMethodInvocationHistoryCollection();
        private readonly ParamsNoReturnMethodInvocationHistoryCollection _paramsNoReturnMethodInvocationHistoryCollection = new ParamsNoReturnMethodInvocationHistoryCollection();
        private readonly NoParamsReturnMethodInvocationHistoryCollection _noParamsReturnMethodInvocationHistoryCollection = new NoParamsReturnMethodInvocationHistoryCollection();
        private readonly ParamsReturnMethodInvocationHistoryCollection _paramsReturnMethodInvocationHistoryCollection = new ParamsReturnMethodInvocationHistoryCollection();
        public INoParamsNoReturnMethodImposterBuilder NoParamsNoReturn()
        {
            return new NoParamsNoReturnMethodImposter.Builder(_noParamsNoReturnMethodImposter, _noParamsNoReturnMethodInvocationHistoryCollection);
        }

        public IParamsNoReturnMethodImposterBuilder ParamsNoReturn(Arg<int> value)
        {
            return new ParamsNoReturnMethodImposter.Builder(_paramsNoReturnMethodImposter, _paramsNoReturnMethodInvocationHistoryCollection, new ParamsNoReturnArgumentsCriteria(value));
        }

        public INoParamsReturnMethodImposterBuilder NoParamsReturn()
        {
            return new NoParamsReturnMethodImposter.Builder(_noParamsReturnMethodImposter, _noParamsReturnMethodInvocationHistoryCollection);
        }

        public IParamsReturnMethodImposterBuilder ParamsReturn(Arg<int> value)
        {
            return new ParamsReturnMethodImposter.Builder(_paramsReturnMethodImposter, _paramsReturnMethodInvocationHistoryCollection, new ParamsReturnArgumentsCriteria(value));
        }

        private ImposterTargetInstance _imposterInstance;
        public ISutForMethodSetupsImposter()
        {
            this._noParamsNoReturnMethodImposter = new NoParamsNoReturnMethodImposter(_noParamsNoReturnMethodInvocationHistoryCollection);
            this._paramsNoReturnMethodImposter = new ParamsNoReturnMethodImposter(_paramsNoReturnMethodInvocationHistoryCollection);
            this._noParamsReturnMethodImposter = new NoParamsReturnMethodImposter(_noParamsReturnMethodInvocationHistoryCollection);
            this._paramsReturnMethodImposter = new ParamsReturnMethodImposter(_paramsReturnMethodInvocationHistoryCollection);
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

        global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups>.Instance()
        {
            return _imposterInstance;
        }

        // void ISutForMethodSetups.NoParamsNoReturn()
        public delegate void NoParamsNoReturnDelegate();
        // void ISutForMethodSetups.NoParamsNoReturn()
        public delegate void NoParamsNoReturnCallbackDelegate();
        // void ISutForMethodSetups.NoParamsNoReturn()
        public delegate System.Exception NoParamsNoReturnExceptionGeneratorDelegate();
        public interface INoParamsNoReturnMethodInvocationHistory
        {
            bool Matches();
        }

        // void ISutForMethodSetups.NoParamsNoReturn()
        public class NoParamsNoReturnMethodInvocationHistory : INoParamsNoReturnMethodInvocationHistory
        {
            public System.Exception? Exception { get; }

            public NoParamsNoReturnMethodInvocationHistory(System.Exception? exception = default(System.Exception? ))
            {
                Exception = exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class NoParamsNoReturnMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<INoParamsNoReturnMethodInvocationHistory> _invocationHistory = new ConcurrentStack<INoParamsNoReturnMethodInvocationHistory>();
            internal void Add(INoParamsNoReturnMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void ISutForMethodSetups.NoParamsNoReturn()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class NoParamsNoReturnMethodInvocationsSetup : INoParamsNoReturnMethodInvocationsSetup
        {
            internal static NoParamsNoReturnMethodInvocationsSetup DefaultInvocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
            private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
            private MethodInvocationSetup? _currentlySetupCall;
            private MethodInvocationSetup GetOrAddMethodSetup(Func<MethodInvocationSetup, bool> addNew)
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
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public INoParamsNoReturnMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            public INoParamsNoReturnMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw ex;
                };
                return this;
            }

            public INoParamsNoReturnMethodInvocationsSetup Throws(NoParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            public INoParamsNoReturnMethodInvocationsSetup CallBefore(NoParamsNoReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public INoParamsNoReturnMethodInvocationsSetup CallAfter(NoParamsNoReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
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
                internal NoParamsNoReturnDelegate? ResultGenerator { get; set; }
                internal NoParamsNoReturnCallbackDelegate? CallBefore { get; set; }
                internal NoParamsNoReturnCallbackDelegate? CallAfter { get; set; }
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
        internal class NoParamsNoReturnMethodImposter
        {
            private readonly ConcurrentStack<NoParamsNoReturnMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<NoParamsNoReturnMethodInvocationsSetup>();
            private readonly NoParamsNoReturnMethodInvocationHistoryCollection _noParamsNoReturnMethodInvocationHistoryCollection;
            public NoParamsNoReturnMethodImposter(NoParamsNoReturnMethodInvocationHistoryCollection _noParamsNoReturnMethodInvocationHistoryCollection)
            {
                this._noParamsNoReturnMethodInvocationHistoryCollection = _noParamsNoReturnMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup()is not null;
            }

            private NoParamsNoReturnMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public void Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? NoParamsNoReturnMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke();
                    _noParamsNoReturnMethodInvocationHistoryCollection.Add(new NoParamsNoReturnMethodInvocationHistory(default));
                }
                catch (Exception ex)
                {
                    _noParamsNoReturnMethodInvocationHistoryCollection.Add(new NoParamsNoReturnMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : INoParamsNoReturnMethodImposterBuilder
            {
                private readonly NoParamsNoReturnMethodImposter _imposter;
                private readonly NoParamsNoReturnMethodInvocationHistoryCollection _noParamsNoReturnMethodInvocationHistoryCollection;
                private NoParamsNoReturnMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(NoParamsNoReturnMethodImposter _imposter, NoParamsNoReturnMethodInvocationHistoryCollection _noParamsNoReturnMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._noParamsNoReturnMethodInvocationHistoryCollection = _noParamsNoReturnMethodInvocationHistoryCollection;
                }

                private NoParamsNoReturnMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new NoParamsNoReturnMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.Throws(NoParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.CallBefore(NoParamsNoReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                INoParamsNoReturnMethodInvocationsSetup INoParamsNoReturnMethodInvocationsSetup.CallAfter(NoParamsNoReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void NoParamsNoReturnMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _noParamsNoReturnMethodInvocationHistoryCollection.Count();
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
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
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

        public interface IParamsNoReturnMethodInvocationHistory
        {
            bool Matches(ParamsNoReturnArgumentsCriteria criteria);
        }

        // void ISutForMethodSetups.ParamsNoReturn(int value)
        public class ParamsNoReturnMethodInvocationHistory : IParamsNoReturnMethodInvocationHistory
        {
            public ParamsNoReturnArguments Arguments { get; }
            public System.Exception? Exception { get; }

            public ParamsNoReturnMethodInvocationHistory(ParamsNoReturnArguments arguments, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Exception = exception;
            }

            public bool Matches(ParamsNoReturnArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ParamsNoReturnMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IParamsNoReturnMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IParamsNoReturnMethodInvocationHistory>();
            internal void Add(IParamsNoReturnMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ParamsNoReturnArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void ISutForMethodSetups.ParamsNoReturn(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ParamsNoReturnMethodInvocationsSetup : IParamsNoReturnMethodInvocationsSetup
        {
            internal static ParamsNoReturnMethodInvocationsSetup DefaultInvocationSetup = new ParamsNoReturnMethodInvocationsSetup(new ParamsNoReturnArgumentsCriteria(Arg<int>.Any()));
            internal ParamsNoReturnArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
            private MethodInvocationSetup? _currentlySetupCall;
            private MethodInvocationSetup GetOrAddMethodSetup(Func<MethodInvocationSetup, bool> addNew)
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
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IParamsNoReturnMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IParamsNoReturnMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw ex;
                };
                return this;
            }

            public IParamsNoReturnMethodInvocationsSetup Throws(ParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            public IParamsNoReturnMethodInvocationsSetup CallBefore(ParamsNoReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IParamsNoReturnMethodInvocationsSetup CallAfter(ParamsNoReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
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
                internal ParamsNoReturnDelegate? ResultGenerator { get; set; }
                internal ParamsNoReturnCallbackDelegate? CallBefore { get; set; }
                internal ParamsNoReturnCallbackDelegate? CallAfter { get; set; }
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
        internal class ParamsNoReturnMethodImposter
        {
            private readonly ConcurrentStack<ParamsNoReturnMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<ParamsNoReturnMethodInvocationsSetup>();
            private readonly ParamsNoReturnMethodInvocationHistoryCollection _paramsNoReturnMethodInvocationHistoryCollection;
            public ParamsNoReturnMethodImposter(ParamsNoReturnMethodInvocationHistoryCollection _paramsNoReturnMethodInvocationHistoryCollection)
            {
                this._paramsNoReturnMethodInvocationHistoryCollection = _paramsNoReturnMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(ParamsNoReturnArguments arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private ParamsNoReturnMethodInvocationsSetup? FindMatchingSetup(ParamsNoReturnArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int value)
            {
                var arguments = new ParamsNoReturnArguments(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? ParamsNoReturnMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(value);
                    _paramsNoReturnMethodInvocationHistoryCollection.Add(new ParamsNoReturnMethodInvocationHistory(arguments, default));
                }
                catch (Exception ex)
                {
                    _paramsNoReturnMethodInvocationHistoryCollection.Add(new ParamsNoReturnMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IParamsNoReturnMethodImposterBuilder
            {
                private readonly ParamsNoReturnMethodImposter _imposter;
                private readonly ParamsNoReturnMethodInvocationHistoryCollection _paramsNoReturnMethodInvocationHistoryCollection;
                private readonly ParamsNoReturnArgumentsCriteria _argumentsCriteria;
                private ParamsNoReturnMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(ParamsNoReturnMethodImposter _imposter, ParamsNoReturnMethodInvocationHistoryCollection _paramsNoReturnMethodInvocationHistoryCollection, ParamsNoReturnArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._paramsNoReturnMethodInvocationHistoryCollection = _paramsNoReturnMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ParamsNoReturnMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new ParamsNoReturnMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.Throws(ParamsNoReturnExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.CallBefore(ParamsNoReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IParamsNoReturnMethodInvocationsSetup IParamsNoReturnMethodInvocationsSetup.CallAfter(ParamsNoReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void ParamsNoReturnMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _paramsNoReturnMethodInvocationHistoryCollection.Count(_argumentsCriteria);
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
        public interface INoParamsReturnMethodInvocationHistory
        {
            bool Matches();
        }

        // int ISutForMethodSetups.NoParamsReturn()
        public class NoParamsReturnMethodInvocationHistory : INoParamsReturnMethodInvocationHistory
        {
            public int? Result { get; }
            public System.Exception? Exception { get; }

            public NoParamsReturnMethodInvocationHistory(int? result = default(int? ), System.Exception? exception = default(System.Exception? ))
            {
                Result = result;
                Exception = exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class NoParamsReturnMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<INoParamsReturnMethodInvocationHistory> _invocationHistory = new ConcurrentStack<INoParamsReturnMethodInvocationHistory>();
            internal void Add(INoParamsReturnMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int ISutForMethodSetups.NoParamsReturn()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class NoParamsReturnMethodInvocationsSetup : INoParamsReturnMethodInvocationsSetup
        {
            internal static NoParamsReturnMethodInvocationsSetup DefaultInvocationSetup = new NoParamsReturnMethodInvocationsSetup();
            private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
            private MethodInvocationSetup? _currentlySetupCall;
            private MethodInvocationSetup GetOrAddMethodSetup(Func<MethodInvocationSetup, bool> addNew)
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
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public INoParamsReturnMethodInvocationsSetup Returns(NoParamsReturnDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public INoParamsReturnMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    return value;
                };
                return this;
            }

            public INoParamsReturnMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            public INoParamsReturnMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw ex;
                };
                return this;
            }

            public INoParamsReturnMethodInvocationsSetup Throws(NoParamsReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            public INoParamsReturnMethodInvocationsSetup CallBefore(NoParamsReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public INoParamsReturnMethodInvocationsSetup CallAfter(NoParamsReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
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
                internal NoParamsReturnDelegate? ResultGenerator { get; set; }
                internal NoParamsReturnCallbackDelegate? CallBefore { get; set; }
                internal NoParamsReturnCallbackDelegate? CallAfter { get; set; }
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
        internal class NoParamsReturnMethodImposter
        {
            private readonly ConcurrentStack<NoParamsReturnMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<NoParamsReturnMethodInvocationsSetup>();
            private readonly NoParamsReturnMethodInvocationHistoryCollection _noParamsReturnMethodInvocationHistoryCollection;
            public NoParamsReturnMethodImposter(NoParamsReturnMethodInvocationHistoryCollection _noParamsReturnMethodInvocationHistoryCollection)
            {
                this._noParamsReturnMethodInvocationHistoryCollection = _noParamsReturnMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup()is not null;
            }

            private NoParamsReturnMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? NoParamsReturnMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke();
                    _noParamsReturnMethodInvocationHistoryCollection.Add(new NoParamsReturnMethodInvocationHistory(result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _noParamsReturnMethodInvocationHistoryCollection.Add(new NoParamsReturnMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : INoParamsReturnMethodImposterBuilder
            {
                private readonly NoParamsReturnMethodImposter _imposter;
                private readonly NoParamsReturnMethodInvocationHistoryCollection _noParamsReturnMethodInvocationHistoryCollection;
                private NoParamsReturnMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(NoParamsReturnMethodImposter _imposter, NoParamsReturnMethodInvocationHistoryCollection _noParamsReturnMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._noParamsReturnMethodInvocationHistoryCollection = _noParamsReturnMethodInvocationHistoryCollection;
                }

                private NoParamsReturnMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new NoParamsReturnMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Returns(NoParamsReturnDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.Throws(NoParamsReturnExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.CallBefore(NoParamsReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                INoParamsReturnMethodInvocationsSetup INoParamsReturnMethodInvocationsSetup.CallAfter(NoParamsReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void NoParamsReturnMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _noParamsReturnMethodInvocationHistoryCollection.Count();
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
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
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

        public interface IParamsReturnMethodInvocationHistory
        {
            bool Matches(ParamsReturnArgumentsCriteria criteria);
        }

        // int ISutForMethodSetups.ParamsReturn(int value)
        public class ParamsReturnMethodInvocationHistory : IParamsReturnMethodInvocationHistory
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

            public bool Matches(ParamsReturnArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ParamsReturnMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IParamsReturnMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IParamsReturnMethodInvocationHistory>();
            internal void Add(IParamsReturnMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ParamsReturnArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int ISutForMethodSetups.ParamsReturn(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ParamsReturnMethodInvocationsSetup : IParamsReturnMethodInvocationsSetup
        {
            internal static ParamsReturnMethodInvocationsSetup DefaultInvocationSetup = new ParamsReturnMethodInvocationsSetup(new ParamsReturnArgumentsCriteria(Arg<int>.Any()));
            internal ParamsReturnArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
            private MethodInvocationSetup? _currentlySetupCall;
            private MethodInvocationSetup GetOrAddMethodSetup(Func<MethodInvocationSetup, bool> addNew)
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
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IParamsReturnMethodInvocationsSetup Returns(ParamsReturnDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IParamsReturnMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    return value;
                };
                return this;
            }

            public IParamsReturnMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IParamsReturnMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw ex;
                };
                return this;
            }

            public IParamsReturnMethodInvocationsSetup Throws(ParamsReturnExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            public IParamsReturnMethodInvocationsSetup CallBefore(ParamsReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IParamsReturnMethodInvocationsSetup CallAfter(ParamsReturnCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
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
                internal ParamsReturnDelegate? ResultGenerator { get; set; }
                internal ParamsReturnCallbackDelegate? CallBefore { get; set; }
                internal ParamsReturnCallbackDelegate? CallAfter { get; set; }
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
        internal class ParamsReturnMethodImposter
        {
            private readonly ConcurrentStack<ParamsReturnMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<ParamsReturnMethodInvocationsSetup>();
            private readonly ParamsReturnMethodInvocationHistoryCollection _paramsReturnMethodInvocationHistoryCollection;
            public ParamsReturnMethodImposter(ParamsReturnMethodInvocationHistoryCollection _paramsReturnMethodInvocationHistoryCollection)
            {
                this._paramsReturnMethodInvocationHistoryCollection = _paramsReturnMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(ParamsReturnArguments arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private ParamsReturnMethodInvocationsSetup? FindMatchingSetup(ParamsReturnArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int value)
            {
                var arguments = new ParamsReturnArguments(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? ParamsReturnMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value);
                    _paramsReturnMethodInvocationHistoryCollection.Add(new ParamsReturnMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _paramsReturnMethodInvocationHistoryCollection.Add(new ParamsReturnMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IParamsReturnMethodImposterBuilder
            {
                private readonly ParamsReturnMethodImposter _imposter;
                private readonly ParamsReturnMethodInvocationHistoryCollection _paramsReturnMethodInvocationHistoryCollection;
                private readonly ParamsReturnArgumentsCriteria _argumentsCriteria;
                private ParamsReturnMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(ParamsReturnMethodImposter _imposter, ParamsReturnMethodInvocationHistoryCollection _paramsReturnMethodInvocationHistoryCollection, ParamsReturnArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._paramsReturnMethodInvocationHistoryCollection = _paramsReturnMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ParamsReturnMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new ParamsReturnMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Returns(ParamsReturnDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.Throws(ParamsReturnExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.CallBefore(ParamsReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IParamsReturnMethodInvocationsSetup IParamsReturnMethodInvocationsSetup.CallAfter(ParamsReturnCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void ParamsReturnMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _paramsReturnMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }
    }
}