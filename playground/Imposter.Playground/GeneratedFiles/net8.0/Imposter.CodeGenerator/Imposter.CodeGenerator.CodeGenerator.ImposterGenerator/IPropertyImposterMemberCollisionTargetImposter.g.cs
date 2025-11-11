#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using global::Imposter.Abstractions;
using System.Collections.Concurrent;
using Sample.NamingCollision;

namespace Sample.NamingCollision
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertyImposterMemberCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyImposterMemberCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior_1;
        private ImposterTargetInstance_1 _imposterInstance_1;
        global::Sample.NamingCollision.IPropertyImposterMemberCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyImposterMemberCollisionTarget>.Instance()
        {
            return _imposterInstance_1;
        }

        public IImposterTargetInstancePropertyBuilder ImposterTargetInstance => _ImposterTargetInstancePropertyBuilderField;

        private readonly ImposterTargetInstancePropertyBuilder _ImposterTargetInstancePropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyGetterOutcomeBuilder
        {
            IImposterTargetInstancePropertyGetterContinuationBuilder Returns(object value);
            IImposterTargetInstancePropertyGetterContinuationBuilder Returns(System.Func<object> valueGenerator);
            IImposterTargetInstancePropertyGetterContinuationBuilder Throws(System.Exception exception);
            IImposterTargetInstancePropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyGetterCallbackBuilder
        {
            IImposterTargetInstancePropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyGetterContinuationBuilder : IImposterTargetInstancePropertyGetterCallbackBuilder
        {
            IImposterTargetInstancePropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyGetterFluentBuilder : IImposterTargetInstancePropertyGetterOutcomeBuilder, IImposterTargetInstancePropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyGetterBuilder : IImposterTargetInstancePropertyGetterOutcomeBuilder, IImposterTargetInstancePropertyGetterCallbackBuilder, IImposterTargetInstancePropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertySetterCallbackBuilder
        {
            IImposterTargetInstancePropertySetterContinuationBuilder Callback(System.Action<object> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertySetterContinuationBuilder : IImposterTargetInstancePropertySetterCallbackBuilder
        {
            IImposterTargetInstancePropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertySetterFluentBuilder : IImposterTargetInstancePropertySetterCallbackBuilder, IImposterTargetInstancePropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertySetterBuilder : IImposterTargetInstancePropertySetterCallbackBuilder, IImposterTargetInstancePropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstancePropertyBuilder
        {
            IImposterTargetInstancePropertyGetterBuilder Getter();
            IImposterTargetInstancePropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<object> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ImposterTargetInstancePropertyBuilder : IImposterTargetInstancePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal ImposterTargetInstancePropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget.ImposterTargetInstance");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget.ImposterTargetInstance");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal object BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IImposterTargetInstancePropertyGetterBuilder, IImposterTargetInstancePropertyGetterFluentBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<object>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<object>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<object> _lastReturnValue = () => default;
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

                private void AddReturnValue(System.Func<object> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                    _hasConfiguredReturn = true;
                }

                IImposterTargetInstancePropertyGetterContinuationBuilder IImposterTargetInstancePropertyGetterOutcomeBuilder.Returns(object value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IImposterTargetInstancePropertyGetterContinuationBuilder IImposterTargetInstancePropertyGetterOutcomeBuilder.Returns(System.Func<object> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IImposterTargetInstancePropertyGetterContinuationBuilder IImposterTargetInstancePropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IImposterTargetInstancePropertyGetterContinuationBuilder IImposterTargetInstancePropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IImposterTargetInstancePropertyGetterContinuationBuilder IImposterTargetInstancePropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IImposterTargetInstancePropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IImposterTargetInstancePropertyGetterFluentBuilder IImposterTargetInstancePropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                internal object Get(System.Func<object> baseImplementation = null)
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
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<object> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<object>();
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

                internal void Callback(global::Imposter.Abstractions.Arg<object> criteria, System.Action<object> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>(criteria, callback));
                }

                void Called(global::Imposter.Abstractions.Arg<object> criteria, global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(object value, System.Action baseImplementation = null)
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
                internal class Builder : IImposterTargetInstancePropertySetterBuilder, IImposterTargetInstancePropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<object> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<object> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IImposterTargetInstancePropertySetterContinuationBuilder IImposterTargetInstancePropertySetterCallbackBuilder.Callback(System.Action<object> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IImposterTargetInstancePropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IImposterTargetInstancePropertySetterFluentBuilder IImposterTargetInstancePropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IImposterTargetInstancePropertyGetterBuilder IImposterTargetInstancePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IImposterTargetInstancePropertySetterBuilder IImposterTargetInstancePropertyBuilder.Setter(global::Imposter.Abstractions.Arg<object> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IInitializeOutParametersWithDefaultValuesPropertyBuilder InitializeOutParametersWithDefaultValues => _InitializeOutParametersWithDefaultValuesPropertyBuilderField;

        private readonly InitializeOutParametersWithDefaultValuesPropertyBuilder _InitializeOutParametersWithDefaultValuesPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder
        {
            IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder Returns(int value);
            IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyGetterCallbackBuilder
        {
            IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder : IInitializeOutParametersWithDefaultValuesPropertyGetterCallbackBuilder
        {
            IInitializeOutParametersWithDefaultValuesPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyGetterFluentBuilder : IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder, IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyGetterBuilder : IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder, IInitializeOutParametersWithDefaultValuesPropertyGetterCallbackBuilder, IInitializeOutParametersWithDefaultValuesPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertySetterCallbackBuilder
        {
            IInitializeOutParametersWithDefaultValuesPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertySetterContinuationBuilder : IInitializeOutParametersWithDefaultValuesPropertySetterCallbackBuilder
        {
            IInitializeOutParametersWithDefaultValuesPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertySetterFluentBuilder : IInitializeOutParametersWithDefaultValuesPropertySetterCallbackBuilder, IInitializeOutParametersWithDefaultValuesPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertySetterBuilder : IInitializeOutParametersWithDefaultValuesPropertySetterCallbackBuilder, IInitializeOutParametersWithDefaultValuesPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesPropertyBuilder
        {
            IInitializeOutParametersWithDefaultValuesPropertyGetterBuilder Getter();
            IInitializeOutParametersWithDefaultValuesPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesPropertyBuilder : IInitializeOutParametersWithDefaultValuesPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal InitializeOutParametersWithDefaultValuesPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IInitializeOutParametersWithDefaultValuesPropertyGetterBuilder, IInitializeOutParametersWithDefaultValuesPropertyGetterFluentBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastReturnValue = () => default;
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

                private void AddReturnValue(System.Func<int> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                    _hasConfiguredReturn = true;
                }

                IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder IInitializeOutParametersWithDefaultValuesPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder IInitializeOutParametersWithDefaultValuesPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IInitializeOutParametersWithDefaultValuesPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IInitializeOutParametersWithDefaultValuesPropertyGetterFluentBuilder IInitializeOutParametersWithDefaultValuesPropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                internal int Get(System.Func<int> baseImplementation = null)
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
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<int>, System.Action<int>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
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

                internal void Callback(global::Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<global::Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                }

                void Called(global::Imposter.Abstractions.Arg<int> criteria, global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(int value, System.Action baseImplementation = null)
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
                internal class Builder : IInitializeOutParametersWithDefaultValuesPropertySetterBuilder, IInitializeOutParametersWithDefaultValuesPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IInitializeOutParametersWithDefaultValuesPropertySetterContinuationBuilder IInitializeOutParametersWithDefaultValuesPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IInitializeOutParametersWithDefaultValuesPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IInitializeOutParametersWithDefaultValuesPropertySetterFluentBuilder IInitializeOutParametersWithDefaultValuesPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IInitializeOutParametersWithDefaultValuesPropertyGetterBuilder IInitializeOutParametersWithDefaultValuesPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IInitializeOutParametersWithDefaultValuesPropertySetterBuilder IInitializeOutParametersWithDefaultValuesPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IInstancePropertyBuilder Instance => _InstancePropertyBuilderField;

        private readonly InstancePropertyBuilder _InstancePropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyGetterOutcomeBuilder
        {
            IInstancePropertyGetterContinuationBuilder Returns(object value);
            IInstancePropertyGetterContinuationBuilder Returns(System.Func<object> valueGenerator);
            IInstancePropertyGetterContinuationBuilder Throws(System.Exception exception);
            IInstancePropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyGetterCallbackBuilder
        {
            IInstancePropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyGetterContinuationBuilder : IInstancePropertyGetterCallbackBuilder
        {
            IInstancePropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyGetterFluentBuilder : IInstancePropertyGetterOutcomeBuilder, IInstancePropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyGetterBuilder : IInstancePropertyGetterOutcomeBuilder, IInstancePropertyGetterCallbackBuilder, IInstancePropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertySetterCallbackBuilder
        {
            IInstancePropertySetterContinuationBuilder Callback(System.Action<object> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertySetterContinuationBuilder : IInstancePropertySetterCallbackBuilder
        {
            IInstancePropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertySetterFluentBuilder : IInstancePropertySetterCallbackBuilder, IInstancePropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertySetterBuilder : IInstancePropertySetterCallbackBuilder, IInstancePropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstancePropertyBuilder
        {
            IInstancePropertyGetterBuilder Getter();
            IInstancePropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<object> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InstancePropertyBuilder : IInstancePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal InstancePropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget.Instance");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget.Instance");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal object BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IInstancePropertyGetterBuilder, IInstancePropertyGetterFluentBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<object>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<object>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<object> _lastReturnValue = () => default;
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

                private void AddReturnValue(System.Func<object> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                    _hasConfiguredReturn = true;
                }

                IInstancePropertyGetterContinuationBuilder IInstancePropertyGetterOutcomeBuilder.Returns(object value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IInstancePropertyGetterContinuationBuilder IInstancePropertyGetterOutcomeBuilder.Returns(System.Func<object> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IInstancePropertyGetterContinuationBuilder IInstancePropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IInstancePropertyGetterContinuationBuilder IInstancePropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IInstancePropertyGetterContinuationBuilder IInstancePropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IInstancePropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IInstancePropertyGetterFluentBuilder IInstancePropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                internal object Get(System.Func<object> baseImplementation = null)
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
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<object> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<object>();
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

                internal void Callback(global::Imposter.Abstractions.Arg<object> criteria, System.Action<object> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>(criteria, callback));
                }

                void Called(global::Imposter.Abstractions.Arg<object> criteria, global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(object value, System.Action baseImplementation = null)
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
                internal class Builder : IInstancePropertySetterBuilder, IInstancePropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<object> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<object> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IInstancePropertySetterContinuationBuilder IInstancePropertySetterCallbackBuilder.Callback(System.Action<object> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IInstancePropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IInstancePropertySetterFluentBuilder IInstancePropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IInstancePropertyGetterBuilder IInstancePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IInstancePropertySetterBuilder IInstancePropertyBuilder.Setter(global::Imposter.Abstractions.Arg<object> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public I_imposterInstancePropertyBuilder _imposterInstance => __imposterInstancePropertyBuilderField;

        private readonly _imposterInstancePropertyBuilder __imposterInstancePropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyGetterOutcomeBuilder
        {
            I_imposterInstancePropertyGetterContinuationBuilder Returns(object value);
            I_imposterInstancePropertyGetterContinuationBuilder Returns(System.Func<object> valueGenerator);
            I_imposterInstancePropertyGetterContinuationBuilder Throws(System.Exception exception);
            I_imposterInstancePropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyGetterCallbackBuilder
        {
            I_imposterInstancePropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyGetterContinuationBuilder : I_imposterInstancePropertyGetterCallbackBuilder
        {
            I_imposterInstancePropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyGetterFluentBuilder : I_imposterInstancePropertyGetterOutcomeBuilder, I_imposterInstancePropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyGetterBuilder : I_imposterInstancePropertyGetterOutcomeBuilder, I_imposterInstancePropertyGetterCallbackBuilder, I_imposterInstancePropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertySetterCallbackBuilder
        {
            I_imposterInstancePropertySetterContinuationBuilder Callback(System.Action<object> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertySetterContinuationBuilder : I_imposterInstancePropertySetterCallbackBuilder
        {
            I_imposterInstancePropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertySetterFluentBuilder : I_imposterInstancePropertySetterCallbackBuilder, I_imposterInstancePropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertySetterBuilder : I_imposterInstancePropertySetterCallbackBuilder, I_imposterInstancePropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstancePropertyBuilder
        {
            I_imposterInstancePropertyGetterBuilder Getter();
            I_imposterInstancePropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<object> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _imposterInstancePropertyBuilder : I_imposterInstancePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal _imposterInstancePropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget._imposterInstance");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget._imposterInstance");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal object BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : I_imposterInstancePropertyGetterBuilder, I_imposterInstancePropertyGetterFluentBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<object>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<object>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<object> _lastReturnValue = () => default;
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

                private void AddReturnValue(System.Func<object> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                    _hasConfiguredReturn = true;
                }

                I_imposterInstancePropertyGetterContinuationBuilder I_imposterInstancePropertyGetterOutcomeBuilder.Returns(object value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                I_imposterInstancePropertyGetterContinuationBuilder I_imposterInstancePropertyGetterOutcomeBuilder.Returns(System.Func<object> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                I_imposterInstancePropertyGetterContinuationBuilder I_imposterInstancePropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                I_imposterInstancePropertyGetterContinuationBuilder I_imposterInstancePropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                I_imposterInstancePropertyGetterContinuationBuilder I_imposterInstancePropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void I_imposterInstancePropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                I_imposterInstancePropertyGetterFluentBuilder I_imposterInstancePropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                internal object Get(System.Func<object> baseImplementation = null)
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
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<object> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<object>();
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

                internal void Callback(global::Imposter.Abstractions.Arg<object> criteria, System.Action<object> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<global::Imposter.Abstractions.Arg<object>, System.Action<object>>(criteria, callback));
                }

                void Called(global::Imposter.Abstractions.Arg<object> criteria, global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(object value, System.Action baseImplementation = null)
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
                internal class Builder : I_imposterInstancePropertySetterBuilder, I_imposterInstancePropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<object> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<object> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    I_imposterInstancePropertySetterContinuationBuilder I_imposterInstancePropertySetterCallbackBuilder.Callback(System.Action<object> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void I_imposterInstancePropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    I_imposterInstancePropertySetterFluentBuilder I_imposterInstancePropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            I_imposterInstancePropertyGetterBuilder I_imposterInstancePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            I_imposterInstancePropertySetterBuilder I_imposterInstancePropertyBuilder.Setter(global::Imposter.Abstractions.Arg<object> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public I_invocationBehaviorPropertyBuilder _invocationBehavior => __invocationBehaviorPropertyBuilderField;

        private readonly _invocationBehaviorPropertyBuilder __invocationBehaviorPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyGetterOutcomeBuilder
        {
            I_invocationBehaviorPropertyGetterContinuationBuilder Returns(string value);
            I_invocationBehaviorPropertyGetterContinuationBuilder Returns(System.Func<string> valueGenerator);
            I_invocationBehaviorPropertyGetterContinuationBuilder Throws(System.Exception exception);
            I_invocationBehaviorPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyGetterCallbackBuilder
        {
            I_invocationBehaviorPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyGetterContinuationBuilder : I_invocationBehaviorPropertyGetterCallbackBuilder
        {
            I_invocationBehaviorPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyGetterFluentBuilder : I_invocationBehaviorPropertyGetterOutcomeBuilder, I_invocationBehaviorPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyGetterBuilder : I_invocationBehaviorPropertyGetterOutcomeBuilder, I_invocationBehaviorPropertyGetterCallbackBuilder, I_invocationBehaviorPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertySetterCallbackBuilder
        {
            I_invocationBehaviorPropertySetterContinuationBuilder Callback(System.Action<string> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertySetterContinuationBuilder : I_invocationBehaviorPropertySetterCallbackBuilder
        {
            I_invocationBehaviorPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertySetterFluentBuilder : I_invocationBehaviorPropertySetterCallbackBuilder, I_invocationBehaviorPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertySetterBuilder : I_invocationBehaviorPropertySetterCallbackBuilder, I_invocationBehaviorPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorPropertyBuilder
        {
            I_invocationBehaviorPropertyGetterBuilder Getter();
            I_invocationBehaviorPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<string> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationBehaviorPropertyBuilder : I_invocationBehaviorPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal _invocationBehaviorPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget._invocationBehavior");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyImposterMemberCollisionTarget._invocationBehavior");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal string BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : I_invocationBehaviorPropertyGetterBuilder, I_invocationBehaviorPropertyGetterFluentBuilder
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

                I_invocationBehaviorPropertyGetterContinuationBuilder I_invocationBehaviorPropertyGetterOutcomeBuilder.Returns(string value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                I_invocationBehaviorPropertyGetterContinuationBuilder I_invocationBehaviorPropertyGetterOutcomeBuilder.Returns(System.Func<string> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                I_invocationBehaviorPropertyGetterContinuationBuilder I_invocationBehaviorPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                I_invocationBehaviorPropertyGetterContinuationBuilder I_invocationBehaviorPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                I_invocationBehaviorPropertyGetterContinuationBuilder I_invocationBehaviorPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void I_invocationBehaviorPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                I_invocationBehaviorPropertyGetterFluentBuilder I_invocationBehaviorPropertyGetterContinuationBuilder.Then()
                {
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
                internal class Builder : I_invocationBehaviorPropertySetterBuilder, I_invocationBehaviorPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<string> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<string> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    I_invocationBehaviorPropertySetterContinuationBuilder I_invocationBehaviorPropertySetterCallbackBuilder.Callback(System.Action<string> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void I_invocationBehaviorPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    I_invocationBehaviorPropertySetterFluentBuilder I_invocationBehaviorPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            I_invocationBehaviorPropertyGetterBuilder I_invocationBehaviorPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            I_invocationBehaviorPropertySetterBuilder I_invocationBehaviorPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<string> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertyImposterMemberCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._ImposterTargetInstancePropertyBuilderField = new ImposterTargetInstancePropertyBuilder(invocationBehavior);
            this._InitializeOutParametersWithDefaultValuesPropertyBuilderField = new InitializeOutParametersWithDefaultValuesPropertyBuilder(invocationBehavior);
            this._InstancePropertyBuilderField = new InstancePropertyBuilder(invocationBehavior);
            this.__imposterInstancePropertyBuilderField = new _imposterInstancePropertyBuilder(invocationBehavior);
            this.__invocationBehaviorPropertyBuilderField = new _invocationBehaviorPropertyBuilder(invocationBehavior);
            this._imposterInstance_1 = new ImposterTargetInstance_1(this);
            this._invocationBehavior_1 = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance_1 : global::Sample.NamingCollision.IPropertyImposterMemberCollisionTarget
        {
            IPropertyImposterMemberCollisionTargetImposter _imposter;
            public ImposterTargetInstance_1(IPropertyImposterMemberCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public object ImposterTargetInstance
            {
                get
                {
                    return _imposter._ImposterTargetInstancePropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._ImposterTargetInstancePropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int InitializeOutParametersWithDefaultValues
            {
                get
                {
                    return _imposter._InitializeOutParametersWithDefaultValuesPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._InitializeOutParametersWithDefaultValuesPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public object Instance
            {
                get
                {
                    return _imposter._InstancePropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._InstancePropertyBuilderField._setterImposter.Set(value);
                }
            }

            public object _imposterInstance
            {
                get
                {
                    return _imposter.__imposterInstancePropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter.__imposterInstancePropertyBuilderField._setterImposter.Set(value);
                }
            }

            public string _invocationBehavior
            {
                get
                {
                    return _imposter.__invocationBehaviorPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter.__invocationBehaviorPropertyBuilderField._setterImposter.Set(value);
                }
            }
        }
    }
}
#pragma warning restore nullable
