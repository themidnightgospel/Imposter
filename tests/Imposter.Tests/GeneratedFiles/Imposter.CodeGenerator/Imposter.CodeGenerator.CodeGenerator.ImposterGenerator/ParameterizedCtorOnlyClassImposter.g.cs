#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using global::Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.ClassImposter.Suts;

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ParameterizedCtorOnlyClassImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass>
    {
        private readonly ComputeMethodImposter _computeMethodImposter;
        private readonly ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection = new ComputeMethodInvocationHistoryCollection();
        public IComputeMethodImposterBuilder Compute(global::Imposter.Abstractions.Arg<int> value)
        {
            return new ComputeMethodImposter.Builder(_computeMethodImposter, _computeMethodInvocationHistoryCollection, new ComputeArgumentsCriteria(value));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass>.Instance()
        {
            return _imposterInstance;
        }

        // virtual int ParameterizedCtorOnlyClass.Compute(int value)
        public delegate int ComputeDelegate(int value);
        // virtual int ParameterizedCtorOnlyClass.Compute(int value)
        public delegate void ComputeCallbackDelegate(int value);
        // virtual int ParameterizedCtorOnlyClass.Compute(int value)
        public delegate System.Exception ComputeExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ComputeArguments
        {
            public int value;
            internal ComputeArguments(int value)
            {
                this.value = value;
            }
        }

        // virtual int ParameterizedCtorOnlyClass.Compute(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ComputeArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public ComputeArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(ComputeArguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IComputeMethodInvocationHistory
        {
            bool Matches(ComputeArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ComputeMethodInvocationHistory : IComputeMethodInvocationHistory
        {
            internal ComputeArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public ComputeMethodInvocationHistory(ComputeArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(ComputeArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ComputeMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IComputeMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IComputeMethodInvocationHistory>();
            internal void Add(IComputeMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ComputeArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual int ParameterizedCtorOnlyClass.Compute(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ComputeMethodInvocationImposterGroup
        {
            internal static ComputeMethodInvocationImposterGroup Default = new ComputeMethodInvocationImposterGroup(new ComputeArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal ComputeArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ComputeMethodInvocationImposterGroup(ComputeArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal MethodInvocationImposter AddInvocationImposter()
            {
                MethodInvocationImposter invocationImposter = new MethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private MethodInvocationImposter? GetInvocationImposter()
            {
                MethodInvocationImposter invocationImposter;
                if (_invocationImposters.TryDequeue(out invocationImposter))
                {
                    if (!invocationImposter.IsEmpty)
                    {
                        _lastestInvocationImposter = invocationImposter;
                    }
                }

                return _lastestInvocationImposter;
            }

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value, ComputeDelegate baseImplementation = null)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value, baseImplementation);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.Returns(DefaultResultGenerator);
                }

                private ComputeDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ComputeCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ComputeCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && ((_resultGenerator == null) && (_callbacks.Count == 0));

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value, ComputeDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(value);
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }

                    return result;
                }

                internal void Callback(ComputeCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ComputeDelegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value_1)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(ComputeExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static int DefaultResultGenerator(int value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IComputeMethodInvocationImposterGroupCallback
        {
            IComputeMethodInvocationImposterGroupContinuation Callback(ComputeCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IComputeMethodInvocationImposterGroupContinuation : IComputeMethodInvocationImposterGroupCallback
        {
            IComputeMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IComputeMethodInvocationImposterGroup : IComputeMethodInvocationImposterGroupCallback
        {
            IComputeMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IComputeMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IComputeMethodInvocationImposterGroupContinuation Throws(ComputeExceptionGeneratorDelegate exceptionGenerator);
            IComputeMethodInvocationImposterGroupContinuation Returns(ComputeDelegate resultGenerator);
            IComputeMethodInvocationImposterGroupContinuation Returns(int value);
            IComputeMethodInvocationImposterGroupContinuation UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ComputeInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual int ParameterizedCtorOnlyClass.Compute(int value)
        public interface IComputeMethodImposterBuilder : IComputeMethodInvocationImposterGroup, IComputeMethodInvocationImposterGroupCallback, ComputeInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ComputeMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ComputeMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ComputeMethodInvocationImposterGroup>();
            private readonly ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ComputeMethodImposter(ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._computeMethodInvocationHistoryCollection = _computeMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(ComputeArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ComputeMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(ComputeArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int value, ComputeDelegate baseImplementation = null)
            {
                var arguments = new ComputeArguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("virtual int ParameterizedCtorOnlyClass.Compute(int value)");
                    }

                    matchingInvocationImposterGroup = ComputeMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual int ParameterizedCtorOnlyClass.Compute(int value)", value, baseImplementation);
                    _computeMethodInvocationHistoryCollection.Add(new ComputeMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _computeMethodInvocationHistoryCollection.Add(new ComputeMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IComputeMethodImposterBuilder, IComputeMethodInvocationImposterGroupContinuation
            {
                private readonly ComputeMethodImposter _imposter;
                private readonly ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection;
                private readonly ComputeArgumentsCriteria _argumentsCriteria;
                private readonly ComputeMethodInvocationImposterGroup _invocationImposterGroup;
                private ComputeMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ComputeMethodImposter _imposter, ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection, ComputeArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._computeMethodInvocationHistoryCollection = _computeMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ComputeMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroup.Throws(ComputeExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroupCallback.Callback(ComputeCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroup.Returns(ComputeDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroup.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IComputeMethodInvocationImposterGroupContinuation IComputeMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ComputeInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _computeMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public INamePropertyBuilder Name => _NamePropertyBuilderField;

        private readonly NamePropertyBuilder _NamePropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterOutcomeBuilder
        {
            INamePropertyGetterContinuationBuilder Returns(string value);
            INamePropertyGetterContinuationBuilder Returns(System.Func<string> valueGenerator);
            INamePropertyGetterContinuationBuilder Throws(System.Exception exception);
            INamePropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterCallbackBuilder
        {
            INamePropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterContinuationBuilder : INamePropertyGetterCallbackBuilder
        {
            INamePropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterFluentBuilder : INamePropertyGetterOutcomeBuilder, INamePropertyGetterContinuationBuilder
        {
            INamePropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterBuilder : INamePropertyGetterOutcomeBuilder, INamePropertyGetterCallbackBuilder, INamePropertyGetterVerifier
        {
            INamePropertyGetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterUseBaseImplementationBuilder
        {
            INamePropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterCallbackBuilder
        {
            INamePropertySetterContinuationBuilder Callback(System.Action<string> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterContinuationBuilder : INamePropertySetterCallbackBuilder
        {
            INamePropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterFluentBuilder : INamePropertySetterCallbackBuilder, INamePropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterBuilder : INamePropertySetterCallbackBuilder, INamePropertySetterVerifier
        {
            INamePropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterUseBaseImplementationBuilder : INamePropertySetterFluentBuilder
        {
            INamePropertySetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyBuilder
        {
            INamePropertyGetterBuilder Getter();
            INamePropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<string> criteria);
            INamePropertyBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class NamePropertyBuilder : INamePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal NamePropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass.Name");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass.Name");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal string BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : INamePropertyGetterBuilder, INamePropertyGetterFluentBuilder, INamePropertyGetterUseBaseImplementationBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<string>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<string>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<string> _lastReturnValue = () => default;
                private int _invocationCount;
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                private bool _useBaseImplementation;
                internal GetterImposterBuilder(DefaultPropertyBehaviour _defaultPropertyBehaviour, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                private void AddReturnValue(System.Func<string> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                    _hasConfiguredReturn = true;
                }

                INamePropertyGetterContinuationBuilder INamePropertyGetterOutcomeBuilder.Returns(string value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                INamePropertyGetterContinuationBuilder INamePropertyGetterOutcomeBuilder.Returns(System.Func<string> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                INamePropertyGetterContinuationBuilder INamePropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                INamePropertyGetterContinuationBuilder INamePropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                INamePropertyGetterContinuationBuilder INamePropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void INamePropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                INamePropertyGetterFluentBuilder INamePropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                INamePropertyGetterUseBaseImplementationBuilder INamePropertyGetterBuilder.Then()
                {
                    return this;
                }

                internal void EnableBaseImplementation()
                {
                    _defaultPropertyBehaviour.IsOn = true;
                    _useBaseImplementation = true;
                    _hasConfiguredReturn = true;
                }

                INamePropertyGetterFluentBuilder INamePropertyGetterUseBaseImplementationBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
                    return this;
                }

                INamePropertyGetterFluentBuilder INamePropertyGetterFluentBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
                    return this;
                }

                internal string Get(System.Func<string> baseImplementation = null)
                {
                    EnsureGetterConfigured();
                    System.Threading.Interlocked.Increment(ref _invocationCount);
                    foreach (var getterCallback in _callbacks)
                    {
                        getterCallback();
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                    {
                        if (_useBaseImplementation)
                        {
                            if (baseImplementation != null)
                                return baseImplementation();
                            else
                                throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        if (((_invocationCount == 1) && (baseImplementation != null)) && (!_useBaseImplementation && !_defaultPropertyBehaviour.HasValueSet))
                        {
                            _defaultPropertyBehaviour.BackingField = baseImplementation();
                        }

                        return _defaultPropertyBehaviour.BackingField;
                    }

                    if (_returnValues.TryDequeue(out var returnValue))
                        _lastReturnValue = returnValue;
                    return _lastReturnValue();
                }

                private void EnsureGetterConfigured()
                {
                    if ((_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit) && !_hasConfiguredReturn)
                        throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<string>, System.Action<string>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<string>, System.Action<string>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<string> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<string>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                private bool _useBaseImplementation;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal void Callback(global::Imposter.Abstractions.Arg<string> criteria, System.Action<string> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<global::Imposter.Abstractions.Arg<string>, System.Action<string>>(criteria, callback));
                }

                void Called(global::Imposter.Abstractions.Arg<string> criteria, global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void UseBaseImplementation()
                {
                    _hasConfiguredSetter = true;
                    _useBaseImplementation = true;
                }

                internal void Set(string value, System.Action baseImplementation = null)
                {
                    EnsureSetterConfigured();
                    _invocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _callbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                    {
                        if (_useBaseImplementation)
                        {
                            if (baseImplementation != null)
                            {
                                baseImplementation();
                                return;
                            }
                            else
                                throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                        }

                        {
                            _defaultPropertyBehaviour.BackingField = value;
                            _defaultPropertyBehaviour.HasValueSet = true;
                        }
                    }
                }

                private void EnsureSetterConfigured()
                {
                    if ((_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit) && !_hasConfiguredSetter)
                        throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                }

                internal void MarkConfigured()
                {
                    _hasConfiguredSetter = true;
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : INamePropertySetterBuilder, INamePropertySetterFluentBuilder, INamePropertySetterUseBaseImplementationBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<string> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<string> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    INamePropertySetterContinuationBuilder INamePropertySetterCallbackBuilder.Callback(System.Action<string> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void INamePropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    INamePropertySetterUseBaseImplementationBuilder INamePropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    INamePropertySetterUseBaseImplementationBuilder INamePropertySetterBuilder.Then()
                    {
                        return this;
                    }

                    INamePropertySetterFluentBuilder INamePropertySetterUseBaseImplementationBuilder.UseBaseImplementation()
                    {
                        _setterImposter.UseBaseImplementation();
                        return this;
                    }
                }
            }

            INamePropertyGetterBuilder INamePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            INamePropertySetterBuilder INamePropertyBuilder.Setter(global::Imposter.Abstractions.Arg<string> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            INamePropertyBuilder INamePropertyBuilder.UseBaseImplementation()
            {
                _getterImposterBuilder.EnableBaseImplementation();
                _setterImposter.UseBaseImplementation();
                return this;
            }
        }

        public ISomethingHappenedEventImposterBuilder SomethingHappened => _SomethingHappened;

        private readonly SomethingHappenedEventImposterBuilder _SomethingHappened;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISomethingHappenedEventImposterBuilder : ISomethingHappenedEventImposterSetupBuilder, ISomethingHappenedEventImposterVerificationBuilder
        {
            ISomethingHappenedEventImposterBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISomethingHappenedEventImposterSetupBuilder
        {
            ISomethingHappenedEventImposterSetupBuilder Callback(global::System.EventHandler callback);
            ISomethingHappenedEventImposterSetupBuilder Raise(object sender, global::System.EventArgs e);
            ISomethingHappenedEventImposterSetupBuilder OnSubscribe(System.Action<global::System.EventHandler> interceptor);
            ISomethingHappenedEventImposterSetupBuilder OnUnsubscribe(System.Action<global::System.EventHandler> interceptor);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISomethingHappenedEventImposterVerificationBuilder
        {
            ISomethingHappenedEventImposterVerificationBuilder Subscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count);
            ISomethingHappenedEventImposterVerificationBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count);
            ISomethingHappenedEventImposterVerificationBuilder Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> eCriteria, global::Imposter.Abstractions.Count count);
            ISomethingHappenedEventImposterVerificationBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.EventHandler> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class SomethingHappenedEventImposterBuilder : ISomethingHappenedEventImposterBuilder, ISomethingHappenedEventImposterSetupBuilder, ISomethingHappenedEventImposterVerificationBuilder
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _handlerOrder = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentDictionary<global::System.EventHandler, int> _handlerCounts = new System.Collections.Concurrent.ConcurrentDictionary<global::System.EventHandler, int>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs e)> _history = new System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs e)>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _subscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _unsubscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>> _subscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>> _unsubscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(global::System.EventHandler Handler, object sender, global::System.EventArgs e)> _handlerInvocations = new System.Collections.Concurrent.ConcurrentQueue<(global::System.EventHandler Handler, object sender, global::System.EventArgs e)>();
            private bool _useBaseImplementation;
            private readonly string _eventDisplayName = "Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass.SomethingHappened";
            internal SomethingHappenedEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.EventHandler handler, System.Action baseImplementation = null)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerOrder.Enqueue(handler);
                _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
                _subscribeHistory.Enqueue(handler);
                foreach (var interceptor in _subscribeInterceptors)
                {
                    interceptor(handler);
                }

                if (_useBaseImplementation)
                {
                    if (baseImplementation != null)
                    {
                        baseImplementation();
                    }
                    else
                        throw new global::Imposter.Abstractions.MissingImposterException(_eventDisplayName + " (event)");
                }
            }

            internal void Unsubscribe(global::System.EventHandler handler, System.Action baseImplementation = null)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerCounts.AddOrUpdate(handler, 0, (_, count) =>
                {
                    if (count > 0)
                    {
                        return count - 1;
                    }

                    return 0;
                });
                _unsubscribeHistory.Enqueue(handler);
                foreach (var interceptor in _unsubscribeInterceptors)
                {
                    interceptor(handler);
                }

                if (_useBaseImplementation)
                {
                    if (baseImplementation != null)
                    {
                        baseImplementation();
                    }
                    else
                        throw new global::Imposter.Abstractions.MissingImposterException(_eventDisplayName + " (event)");
                }
            }

            ISomethingHappenedEventImposterSetupBuilder ISomethingHappenedEventImposterSetupBuilder.Callback(global::System.EventHandler callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            ISomethingHappenedEventImposterSetupBuilder ISomethingHappenedEventImposterSetupBuilder.Raise(object sender, global::System.EventArgs e)
            {
                RaiseInternal(sender, e);
                return this;
            }

            ISomethingHappenedEventImposterVerificationBuilder ISomethingHappenedEventImposterVerificationBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            ISomethingHappenedEventImposterVerificationBuilder ISomethingHappenedEventImposterVerificationBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            ISomethingHappenedEventImposterSetupBuilder ISomethingHappenedEventImposterSetupBuilder.OnSubscribe(System.Action<global::System.EventHandler> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ISomethingHappenedEventImposterSetupBuilder ISomethingHappenedEventImposterSetupBuilder.OnUnsubscribe(System.Action<global::System.EventHandler> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ISomethingHappenedEventImposterVerificationBuilder ISomethingHappenedEventImposterVerificationBuilder.Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> eCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(senderCriteria);
                ArgumentNullException.ThrowIfNull(eCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => senderCriteria.Matches(entry.sender) && eCriteria.Matches(entry.e));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            ISomethingHappenedEventImposterVerificationBuilder ISomethingHappenedEventImposterVerificationBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.EventHandler> handlerCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(handlerCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
                EnsureCountMatches(actual, count, "invoked");
                return this;
            }

            private void RaiseInternal(object sender, global::System.EventArgs e)
            {
                _history.Enqueue((sender, e));
                foreach (var callback in _callbacks)
                {
                    callback(sender, e);
                }

                foreach (var handler in EnumerateActiveHandlers())
                {
                    _handlerInvocations.Enqueue((handler, sender, e));
                    handler(sender, e);
                }
            }

            private System.Collections.Generic.IEnumerable<global::System.EventHandler> EnumerateActiveHandlers()
            {
                System.Collections.Generic.Dictionary<global::System.EventHandler, int> budgets = new System.Collections.Generic.Dictionary<global::System.EventHandler, int>(_handlerCounts);
                foreach (var handler in _handlerOrder)
                {
                    int remaining;
                    if (budgets.TryGetValue(handler, out remaining))
                    {
                        if (remaining > 0)
                        {
                            budgets[handler] = remaining - 1;
                            yield return handler;
                        }
                    }
                }
            }

            private static int CountMatches<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
            {
                int count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }

                return count;
            }

            private static void EnsureCountMatches(int actual, global::Imposter.Abstractions.Count expected, string action)
            {
                if (!expected.Matches(actual))
                {
                    throw new global::Imposter.Abstractions.VerificationFailedException(expected, actual);
                }
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.UseBaseImplementation()
            {
                _useBaseImplementation = true;
                return this;
            }
        }

        private readonly IndexerIndexerBuilder _IndexerIndexer;
        public IIndexerIndexerBuilder this[global::Imposter.Abstractions.Arg<int> index] => new IndexerIndexerBuilder.InvocationBuilder(_IndexerIndexer, new IndexerIndexerArgumentsCriteria(index));
        public delegate int IndexerIndexerDelegate(int index);
        public delegate void IndexerIndexerGetterCallback(int index);
        public delegate void IndexerIndexerSetterCallback(int index, int value);
        public delegate System.Exception IndexerIndexerExceptionGenerator(int index);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArguments : System.IEquatable<IndexerIndexerArguments>
        {
            public int index;
            internal IndexerIndexerArguments(int index)
            {
                this.index = index;
            }

            public bool Equals(IndexerIndexerArguments other)
            {
                return true && (index == other.index);
            }

            public override bool Equals(object obj)
            {
                return obj is IndexerIndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                System.HashCode hash = new System.HashCode();
                hash.Add(index);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> index;
            internal IndexerIndexerArgumentsCriteria(global::Imposter.Abstractions.Arg<int> index)
            {
                this.index = index;
            }

            public bool Matches(IndexerIndexerArguments arguments)
            {
                return index.Matches(arguments.index);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerBuilder
        {
            private readonly DefaultIndexerIndexerBehaviour _IndexerDefaultIndexerBehaviour = new DefaultIndexerIndexerBehaviour();
            private readonly GetterImposter _getterImposter;
            private readonly SetterImposter _setterImposter;
            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultIndexerIndexerBehaviour
            {
                private bool _isOn = true;
                internal bool IsOn
                {
                    get
                    {
                        return System.Threading.Volatile.Read(ref _isOn);
                    }

                    set
                    {
                        System.Threading.Volatile.Write(ref _isOn, value);
                    }
                }

                internal System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArguments, int> BackingField = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArguments, int>();
                internal int Get(IndexerIndexerArguments arguments, System.Func<int> baseImplementation = null)
                {
                    int value = default(int);
                    if (BackingField.TryGetValue(arguments, out value))
                        return value;
                    if (baseImplementation != null)
                        return baseImplementation();
                    return default(int);
                }

                internal void Set(IndexerIndexerArguments arguments, int value, System.Action baseImplementation = null)
                {
                    if (baseImplementation != null)
                    {
                        baseImplementation();
                        return;
                    }

                    BackingField[arguments] = value;
                }
            }

            internal IndexerIndexerBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
            {
                this._getterImposter = new GetterImposter(_IndexerDefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
                this._setterImposter = new SetterImposter(_IndexerDefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
            }

            internal IIndexerIndexerGetterBuilder CreateGetter(IndexerIndexerArgumentsCriteria criteria)
            {
                return new GetterImposter.Builder(_getterImposter, criteria);
            }

            internal IIndexerIndexerSetterBuilder CreateSetter(IndexerIndexerArgumentsCriteria criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class InvocationBuilder : IIndexerIndexerBuilder
            {
                private readonly IndexerIndexerBuilder _builder;
                private readonly IndexerIndexerArgumentsCriteria _criteria;
                internal InvocationBuilder(IndexerIndexerBuilder builder, IndexerIndexerArgumentsCriteria criteria)
                {
                    this._builder = builder;
                    this._criteria = criteria;
                }

                public IIndexerIndexerGetterBuilder Getter()
                {
                    return _builder.CreateGetter(_criteria);
                }

                public IIndexerIndexerSetterBuilder Setter()
                {
                    return _builder.CreateSetter(_criteria);
                }
            }

            internal int Get(int index, System.Func<int> baseImplementation = null)
            {
                return _getterImposter.Get(index, baseImplementation);
            }

            internal void Set(int index, int value, System.Action baseImplementation = null)
            {
                _setterImposter.Set(index, value, baseImplementation);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class GetterImposter
            {
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter> _getterInvocationImposters = new System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter> _setupLookup = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal int Get(int index, System.Func<int> baseImplementation = null)
                {
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(index);
                    try
                    {
                        var getterInvocationImposter = FindGetterInvocationImposter(arguments);
                        if (getterInvocationImposter is null)
                        {
                            EnsureGetterConfigured();
                            if (_defaultBehaviour.IsOn)
                                return _defaultBehaviour.Get(arguments, baseImplementation);
                            if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                            {
                                throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                            }
                            else
                            {
                                return default(int);
                            }
                        }

                        return getterInvocationImposter.Invoke(arguments, baseImplementation);
                    }
                    finally
                    {
                        _invocationHistory.Add(arguments);
                    }
                }

                private GetterInvocationImposter FindGetterInvocationImposter(IndexerIndexerArguments arguments)
                {
                    foreach (var getterInvocationImposter in _getterInvocationImposters)
                    {
                        if (getterInvocationImposter.Criteria.Matches(arguments))
                        {
                            return getterInvocationImposter;
                        }
                    }

                    return null;
                }

                private GetterInvocationImposter GetOrCreate(IndexerIndexerArgumentsCriteria criteria)
                {
                    return _setupLookup.GetOrAdd(criteria, CreateSetup);
                    GetterInvocationImposter CreateSetup(IndexerIndexerArgumentsCriteria key)
                    {
                        GetterInvocationImposter getterInvocationImposter = new GetterInvocationImposter(this, _defaultBehaviour, key);
                        _getterInvocationImposters.Push(getterInvocationImposter);
                        return getterInvocationImposter;
                    }
                }

                private void Called(IndexerIndexerArgumentsCriteria criteria, global::Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal string PropertyDisplayName => _propertyDisplayName;

                internal void MarkReturnConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredReturn, true);
                }

                private void EnsureGetterConfigured()
                {
                    if ((_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit) && !System.Threading.Volatile.Read(ref _hasConfiguredReturn))
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerGetterBuilder, IIndexerIndexerGetterFluentBuilder
                {
                    private readonly GetterImposter _imposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(GetterImposter _imposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._imposter = _imposter;
                        this._criteria = _criteria;
                    }

                    private GetterInvocationImposter InvocationImposter => _imposter.GetOrCreate(_criteria);

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Returns(int value)
                    {
                        InvocationImposter.AddReturnValue(arguments => value);
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator());
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Returns(IndexerIndexerDelegate valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator(arguments.index));
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Throws(System.Exception exception)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exception);
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Throws<TException>()
                    {
                        InvocationImposter.AddReturnValue(arguments => throw new TException());
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Throws(IndexerIndexerExceptionGenerator exceptionGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exceptionGenerator(arguments.index));
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterCallbackBuilder.Callback(IndexerIndexerGetterCallback callback)
                    {
                        InvocationImposter.AddCallback(callback);
                        return this;
                    }

                    void IIndexerIndexerGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _imposter.Called(_criteria, count);
                    }

                    IIndexerIndexerGetterFluentBuilder IIndexerIndexerGetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.UseBaseImplementation()
                    {
                        InvocationImposter.UseBaseImplementation();
                        return this;
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                private sealed class GetterInvocationImposter
                {
                    private readonly GetterImposter _parent;
                    private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                    private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<IndexerIndexerArguments, System.Func<int>, int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<IndexerIndexerArguments, System.Func<int>, int>>();
                    private readonly System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerGetterCallback> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerGetterCallback>();
                    private System.Func<IndexerIndexerArguments, System.Func<int>, int>? _lastReturnValue;
                    private int _invocationCount;
                    private string _propertyDisplayName;
                    internal IndexerIndexerArgumentsCriteria Criteria { get; private set; }
                    internal int InvocationCount => System.Threading.Volatile.Read(ref _invocationCount);

                    internal GetterInvocationImposter(GetterImposter parent, DefaultIndexerIndexerBehaviour defaultBehaviour, IndexerIndexerArgumentsCriteria criteria)
                    {
                        this._parent = parent;
                        this._defaultBehaviour = defaultBehaviour;
                        this._propertyDisplayName = parent.PropertyDisplayName;
                        this.Criteria = criteria;
                    }

                    internal void AddReturnValue(System.Func<IndexerIndexerArguments, int> generator)
                    {
                        _defaultBehaviour.IsOn = false;
                        _returnValues.Enqueue((arguments, baseImplementation) => generator(arguments));
                        _parent.MarkReturnConfigured();
                    }

                    internal void AddCallback(IndexerIndexerGetterCallback callback)
                    {
                        _callbacks.Enqueue(callback);
                    }

                    internal int Invoke(IndexerIndexerArguments arguments, System.Func<int> baseImplementation = null)
                    {
                        System.Threading.Interlocked.Increment(ref _invocationCount);
                        foreach (var callback in _callbacks)
                        {
                            callback(arguments.index);
                        }

                        System.Func<IndexerIndexerArguments, System.Func<int>, int> generator = ResolveNextGenerator(arguments);
                        return generator(arguments, baseImplementation);
                    }

                    private System.Func<IndexerIndexerArguments, System.Func<int>, int> ResolveNextGenerator(IndexerIndexerArguments arguments)
                    {
                        if (_defaultBehaviour.IsOn)
                        {
                            return (arguments, baseImplementation) => _defaultBehaviour.Get(arguments, baseImplementation);
                        }

                        if (_returnValues.TryDequeue(out System.Func<IndexerIndexerArguments, System.Func<int>, int> returnValue))
                        {
                            _lastReturnValue = returnValue;
                        }

                        if (_lastReturnValue == null)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return _lastReturnValue!;
                    }

                    internal void UseBaseImplementation()
                    {
                        _parent.MarkReturnConfigured();
                        _defaultBehaviour.IsOn = false;
                        _returnValues.Enqueue((arguments, baseImplementation) =>
                        {
                            if (baseImplementation == null)
                            {
                                throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                            }

                            return baseImplementation();
                        });
                        _lastReturnValue = null;
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<(IndexerIndexerArgumentsCriteria Criteria, IndexerIndexerSetterCallback Callback)> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<(IndexerIndexerArgumentsCriteria Criteria, IndexerIndexerSetterCallback Callback)>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerArgumentsCriteria> _baseCriteria = new System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerArgumentsCriteria>();
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                public void Callback(IndexerIndexerArgumentsCriteria criteria, IndexerIndexerSetterCallback callback)
                {
                    _callbacks.Enqueue((criteria, callback));
                }

                public void Called(IndexerIndexerArgumentsCriteria criteria, global::Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal void Set(int index, int value, System.Action baseImplementation = null)
                {
                    EnsureSetterConfigured();
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(index);
                    _invocationHistory.Add(arguments);
                    bool matchedCallback = false;
                    foreach (var registration in _callbacks)
                    {
                        if (registration.Criteria.Matches(arguments))
                        {
                            registration.Callback(arguments.index, value);
                            matchedCallback = true;
                        }
                    }

                    bool invokedBaseImplementation = false;
                    foreach (var criteria in _baseCriteria)
                    {
                        if (criteria.Matches(arguments))
                        {
                            if (baseImplementation == null)
                            {
                                throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                            }

                            baseImplementation();
                            invokedBaseImplementation = true;
                            break;
                        }
                    }

                    if (!invokedBaseImplementation && (!matchedCallback && _defaultBehaviour.IsOn))
                    {
                        _defaultBehaviour.Set(arguments, value, null);
                    }
                }

                private void EnsureSetterConfigured()
                {
                    if ((_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit) && !System.Threading.Volatile.Read(ref _hasConfiguredSetter))
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                    }
                }

                internal void MarkConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredSetter, true);
                }

                public void UseBaseImplementation(IndexerIndexerArgumentsCriteria criteria)
                {
                    _baseCriteria.Enqueue(criteria);
                    MarkConfigured();
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerSetterBuilder, IIndexerIndexerSetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(SetterImposter _setterImposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIndexerIndexerSetterContinuationBuilder IIndexerIndexerSetterCallbackBuilder.Callback(IndexerIndexerSetterCallback callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIndexerIndexerSetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIndexerIndexerSetterFluentBuilder IIndexerIndexerSetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    IIndexerIndexerSetterFluentBuilder IIndexerIndexerSetterBuilder.UseBaseImplementation()
                    {
                        _setterImposter.UseBaseImplementation(_criteria);
                        return this;
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterOutcomeBuilder
        {
            IIndexerIndexerGetterContinuationBuilder Returns(int value);
            IIndexerIndexerGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIndexerIndexerGetterContinuationBuilder Returns(IndexerIndexerDelegate valueGenerator);
            IIndexerIndexerGetterContinuationBuilder Throws(System.Exception exception);
            IIndexerIndexerGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
            IIndexerIndexerGetterContinuationBuilder Throws(IndexerIndexerExceptionGenerator exceptionGenerator);
            IIndexerIndexerGetterContinuationBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterCallbackBuilder
        {
            IIndexerIndexerGetterContinuationBuilder Callback(IndexerIndexerGetterCallback callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterContinuationBuilder : IIndexerIndexerGetterCallbackBuilder
        {
            IIndexerIndexerGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterFluentBuilder : IIndexerIndexerGetterOutcomeBuilder, IIndexerIndexerGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterBuilder : IIndexerIndexerGetterOutcomeBuilder, IIndexerIndexerGetterCallbackBuilder, IIndexerIndexerGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterCallbackBuilder
        {
            IIndexerIndexerSetterContinuationBuilder Callback(IndexerIndexerSetterCallback callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterContinuationBuilder : IIndexerIndexerSetterCallbackBuilder
        {
            IIndexerIndexerSetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterFluentBuilder : IIndexerIndexerSetterCallbackBuilder, IIndexerIndexerSetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterBuilder : IIndexerIndexerSetterCallbackBuilder, IIndexerIndexerSetterVerifier
        {
            IIndexerIndexerSetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerBuilder
        {
            IIndexerIndexerGetterBuilder Getter();
            IIndexerIndexerSetterBuilder Setter();
        }

        public ParameterizedCtorOnlyClassImposter(int seed, string name, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._computeMethodImposter = new ComputeMethodImposter(_computeMethodInvocationHistoryCollection, invocationBehavior);
            this._NamePropertyBuilderField = new NamePropertyBuilder(invocationBehavior);
            this._SomethingHappened = new SomethingHappenedEventImposterBuilder();
            this._IndexerIndexer = new IndexerIndexerBuilder(invocationBehavior, "Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass.this[int index]");
            this._imposterInstance = new ImposterTargetInstance(seed, name);
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.Suts.ParameterizedCtorOnlyClass
        {
            ParameterizedCtorOnlyClassImposter _imposter;
            internal void InitializeImposter(ParameterizedCtorOnlyClassImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance(int seed, string name) : base(seed, name)
            {
            }

            public override int Compute(int value)
            {
                return _imposter._computeMethodImposter.Invoke(value, base.Compute);
            }

            public override string Name
            {
                get
                {
                    return _imposter._NamePropertyBuilderField._getterImposterBuilder.Get(() => base.Name);
                }

                set
                {
                    _imposter._NamePropertyBuilderField._setterImposter.Set(value, () =>
                    {
                        base.Name = value;
                    });
                }
            }

            public override event global::System.EventHandler SomethingHappened
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._SomethingHappened.Subscribe(value, () =>
                    {
                        base.SomethingHappened += value;
                    });
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._SomethingHappened.Unsubscribe(value, () =>
                    {
                        base.SomethingHappened -= value;
                    });
                }
            }

            public override int this[int index]
            {
                get
                {
                    return _imposter._IndexerIndexer.Get(index, () => base[index]);
                }

                set
                {
                    _imposter._IndexerIndexer.Set(index, value, () =>
                    {
                        base[index] = value;
                    });
                }
            }
        }
    }
}
#pragma warning restore nullable
