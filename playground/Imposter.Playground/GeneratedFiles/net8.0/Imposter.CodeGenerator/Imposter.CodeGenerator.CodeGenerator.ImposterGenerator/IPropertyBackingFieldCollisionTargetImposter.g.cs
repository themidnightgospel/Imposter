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
    public class IPropertyBackingFieldCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyBackingFieldCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.IPropertyBackingFieldCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyBackingFieldCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IPropPropertyBuilder Prop => _PropPropertyBuilderField;

        private readonly PropPropertyBuilder _PropPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyGetterOutcomeBuilder
        {
            IPropPropertyGetterContinuationBuilder Returns(int value);
            IPropPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IPropPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IPropPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyGetterCallbackBuilder
        {
            IPropPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyGetterContinuationBuilder : IPropPropertyGetterCallbackBuilder
        {
            IPropPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyGetterFluentBuilder : IPropPropertyGetterOutcomeBuilder, IPropPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyGetterBuilder : IPropPropertyGetterOutcomeBuilder, IPropPropertyGetterCallbackBuilder, IPropPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertySetterCallbackBuilder
        {
            IPropPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertySetterContinuationBuilder : IPropPropertySetterCallbackBuilder
        {
            IPropPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertySetterFluentBuilder : IPropPropertySetterCallbackBuilder, IPropPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertySetterBuilder : IPropPropertySetterCallbackBuilder, IPropPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPropPropertyBuilder
        {
            IPropPropertyGetterBuilder Getter();
            IPropPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class PropPropertyBuilder : IPropPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal PropPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget.Prop");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget.Prop");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IPropPropertyGetterBuilder, IPropPropertyGetterFluentBuilder
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

                IPropPropertyGetterContinuationBuilder IPropPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IPropPropertyGetterContinuationBuilder IPropPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IPropPropertyGetterContinuationBuilder IPropPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IPropPropertyGetterContinuationBuilder IPropPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IPropPropertyGetterContinuationBuilder IPropPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IPropPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IPropPropertyGetterFluentBuilder IPropPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IPropPropertySetterBuilder, IPropPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IPropPropertySetterContinuationBuilder IPropPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IPropPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IPropPropertySetterFluentBuilder IPropPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IPropPropertyGetterBuilder IPropPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IPropPropertySetterBuilder IPropPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public I_MyPropertyPropertyBuilder _MyProperty => __MyPropertyPropertyBuilderField;

        private readonly _MyPropertyPropertyBuilder __MyPropertyPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyGetterOutcomeBuilder
        {
            I_MyPropertyPropertyGetterContinuationBuilder Returns(int value);
            I_MyPropertyPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            I_MyPropertyPropertyGetterContinuationBuilder Throws(System.Exception exception);
            I_MyPropertyPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyGetterCallbackBuilder
        {
            I_MyPropertyPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyGetterContinuationBuilder : I_MyPropertyPropertyGetterCallbackBuilder
        {
            I_MyPropertyPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyGetterFluentBuilder : I_MyPropertyPropertyGetterOutcomeBuilder, I_MyPropertyPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyGetterBuilder : I_MyPropertyPropertyGetterOutcomeBuilder, I_MyPropertyPropertyGetterCallbackBuilder, I_MyPropertyPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertySetterCallbackBuilder
        {
            I_MyPropertyPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertySetterContinuationBuilder : I_MyPropertyPropertySetterCallbackBuilder
        {
            I_MyPropertyPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertySetterFluentBuilder : I_MyPropertyPropertySetterCallbackBuilder, I_MyPropertyPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertySetterBuilder : I_MyPropertyPropertySetterCallbackBuilder, I_MyPropertyPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_MyPropertyPropertyBuilder
        {
            I_MyPropertyPropertyGetterBuilder Getter();
            I_MyPropertyPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _MyPropertyPropertyBuilder : I_MyPropertyPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal _MyPropertyPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget._MyProperty");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget._MyProperty");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : I_MyPropertyPropertyGetterBuilder, I_MyPropertyPropertyGetterFluentBuilder
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

                I_MyPropertyPropertyGetterContinuationBuilder I_MyPropertyPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                I_MyPropertyPropertyGetterContinuationBuilder I_MyPropertyPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                I_MyPropertyPropertyGetterContinuationBuilder I_MyPropertyPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                I_MyPropertyPropertyGetterContinuationBuilder I_MyPropertyPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                I_MyPropertyPropertyGetterContinuationBuilder I_MyPropertyPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void I_MyPropertyPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                I_MyPropertyPropertyGetterFluentBuilder I_MyPropertyPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : I_MyPropertyPropertySetterBuilder, I_MyPropertyPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    I_MyPropertyPropertySetterContinuationBuilder I_MyPropertyPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void I_MyPropertyPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    I_MyPropertyPropertySetterFluentBuilder I_MyPropertyPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            I_MyPropertyPropertyGetterBuilder I_MyPropertyPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            I_MyPropertyPropertySetterBuilder I_MyPropertyPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public I_PropPropertyBuilder _Prop => __PropPropertyBuilderField;

        private readonly _PropPropertyBuilder __PropPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyGetterOutcomeBuilder
        {
            I_PropPropertyGetterContinuationBuilder Returns(int value);
            I_PropPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            I_PropPropertyGetterContinuationBuilder Throws(System.Exception exception);
            I_PropPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyGetterCallbackBuilder
        {
            I_PropPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyGetterContinuationBuilder : I_PropPropertyGetterCallbackBuilder
        {
            I_PropPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyGetterFluentBuilder : I_PropPropertyGetterOutcomeBuilder, I_PropPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyGetterBuilder : I_PropPropertyGetterOutcomeBuilder, I_PropPropertyGetterCallbackBuilder, I_PropPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertySetterCallbackBuilder
        {
            I_PropPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertySetterContinuationBuilder : I_PropPropertySetterCallbackBuilder
        {
            I_PropPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertySetterFluentBuilder : I_PropPropertySetterCallbackBuilder, I_PropPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertySetterBuilder : I_PropPropertySetterCallbackBuilder, I_PropPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_PropPropertyBuilder
        {
            I_PropPropertyGetterBuilder Getter();
            I_PropPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _PropPropertyBuilder : I_PropPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal _PropPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget._Prop");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget._Prop");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : I_PropPropertyGetterBuilder, I_PropPropertyGetterFluentBuilder
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

                I_PropPropertyGetterContinuationBuilder I_PropPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                I_PropPropertyGetterContinuationBuilder I_PropPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                I_PropPropertyGetterContinuationBuilder I_PropPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                I_PropPropertyGetterContinuationBuilder I_PropPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                I_PropPropertyGetterContinuationBuilder I_PropPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void I_PropPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                I_PropPropertyGetterFluentBuilder I_PropPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : I_PropPropertySetterBuilder, I_PropPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    I_PropPropertySetterContinuationBuilder I_PropPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void I_PropPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    I_PropPropertySetterFluentBuilder I_PropPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            I_PropPropertyGetterBuilder I_PropPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            I_PropPropertySetterBuilder I_PropPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public I_defaultPropertyBehaviourPropertyBuilder _defaultPropertyBehaviour => __defaultPropertyBehaviourPropertyBuilderField;

        private readonly _defaultPropertyBehaviourPropertyBuilder __defaultPropertyBehaviourPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder
        {
            I_defaultPropertyBehaviourPropertyGetterContinuationBuilder Returns(int value);
            I_defaultPropertyBehaviourPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            I_defaultPropertyBehaviourPropertyGetterContinuationBuilder Throws(System.Exception exception);
            I_defaultPropertyBehaviourPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyGetterCallbackBuilder
        {
            I_defaultPropertyBehaviourPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyGetterContinuationBuilder : I_defaultPropertyBehaviourPropertyGetterCallbackBuilder
        {
            I_defaultPropertyBehaviourPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyGetterFluentBuilder : I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder, I_defaultPropertyBehaviourPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyGetterBuilder : I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder, I_defaultPropertyBehaviourPropertyGetterCallbackBuilder, I_defaultPropertyBehaviourPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertySetterCallbackBuilder
        {
            I_defaultPropertyBehaviourPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertySetterContinuationBuilder : I_defaultPropertyBehaviourPropertySetterCallbackBuilder
        {
            I_defaultPropertyBehaviourPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertySetterFluentBuilder : I_defaultPropertyBehaviourPropertySetterCallbackBuilder, I_defaultPropertyBehaviourPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertySetterBuilder : I_defaultPropertyBehaviourPropertySetterCallbackBuilder, I_defaultPropertyBehaviourPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_defaultPropertyBehaviourPropertyBuilder
        {
            I_defaultPropertyBehaviourPropertyGetterBuilder Getter();
            I_defaultPropertyBehaviourPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _defaultPropertyBehaviourPropertyBuilder : I_defaultPropertyBehaviourPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal _defaultPropertyBehaviourPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget._defaultPropertyBehaviour");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyBackingFieldCollisionTarget._defaultPropertyBehaviour");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : I_defaultPropertyBehaviourPropertyGetterBuilder, I_defaultPropertyBehaviourPropertyGetterFluentBuilder
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

                I_defaultPropertyBehaviourPropertyGetterContinuationBuilder I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                I_defaultPropertyBehaviourPropertyGetterContinuationBuilder I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                I_defaultPropertyBehaviourPropertyGetterContinuationBuilder I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                I_defaultPropertyBehaviourPropertyGetterContinuationBuilder I_defaultPropertyBehaviourPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                I_defaultPropertyBehaviourPropertyGetterContinuationBuilder I_defaultPropertyBehaviourPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void I_defaultPropertyBehaviourPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                I_defaultPropertyBehaviourPropertyGetterFluentBuilder I_defaultPropertyBehaviourPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : I_defaultPropertyBehaviourPropertySetterBuilder, I_defaultPropertyBehaviourPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    I_defaultPropertyBehaviourPropertySetterContinuationBuilder I_defaultPropertyBehaviourPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void I_defaultPropertyBehaviourPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    I_defaultPropertyBehaviourPropertySetterFluentBuilder I_defaultPropertyBehaviourPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            I_defaultPropertyBehaviourPropertyGetterBuilder I_defaultPropertyBehaviourPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            I_defaultPropertyBehaviourPropertySetterBuilder I_defaultPropertyBehaviourPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertyBackingFieldCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._PropPropertyBuilderField = new PropPropertyBuilder(invocationBehavior);
            this.__MyPropertyPropertyBuilderField = new _MyPropertyPropertyBuilder(invocationBehavior);
            this.__PropPropertyBuilderField = new _PropPropertyBuilder(invocationBehavior);
            this.__defaultPropertyBehaviourPropertyBuilderField = new _defaultPropertyBehaviourPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.IPropertyBackingFieldCollisionTarget
        {
            IPropertyBackingFieldCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IPropertyBackingFieldCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Prop
            {
                get
                {
                    return _imposter._PropPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._PropPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int _MyProperty
            {
                get
                {
                    return _imposter.__MyPropertyPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter.__MyPropertyPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int _Prop
            {
                get
                {
                    return _imposter.__PropPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter.__PropPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int _defaultPropertyBehaviour
            {
                get
                {
                    return _imposter.__defaultPropertyBehaviourPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter.__defaultPropertyBehaviourPropertyBuilderField._setterImposter.Set(value);
                }
            }
        }
    }
}
#pragma warning restore nullable
