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
    public class IPropertyGetterBuilderNameCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IIWeirdPropertyGetterBuilderPropertyBuilder IWeirdPropertyGetterBuilder => _IWeirdPropertyGetterBuilderPropertyBuilderField;

        private readonly IWeirdPropertyGetterBuilderPropertyBuilder _IWeirdPropertyGetterBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder : IIWeirdPropertyGetterBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyGetterFluentBuilder : IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyGetterBuilder : IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterBuilderPropertyGetterCallbackBuilder, IIWeirdPropertyGetterBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertySetterContinuationBuilder : IIWeirdPropertyGetterBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertySetterFluentBuilder : IIWeirdPropertyGetterBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertySetterBuilder : IIWeirdPropertyGetterBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterBuilderPropertyBuilder
        {
            IIWeirdPropertyGetterBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterBuilderPropertyBuilder : IIWeirdPropertyGetterBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterBuilderPropertyGetterBuilder, IIWeirdPropertyGetterBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertyGetterBuilderPropertySetterBuilder, IIWeirdPropertyGetterBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterBuilderPropertySetterContinuationBuilder IIWeirdPropertyGetterBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterBuilderPropertySetterFluentBuilder IIWeirdPropertyGetterBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterBuilderPropertyGetterBuilder IIWeirdPropertyGetterBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterBuilderPropertySetterBuilder IIWeirdPropertyGetterBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertyGetterCallbackBuilderPropertyBuilder IWeirdPropertyGetterCallbackBuilder => _IWeirdPropertyGetterCallbackBuilderPropertyBuilderField;

        private readonly IWeirdPropertyGetterCallbackBuilderPropertyBuilder _IWeirdPropertyGetterCallbackBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder : IIWeirdPropertyGetterCallbackBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyGetterFluentBuilder : IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyGetterBuilder : IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterCallbackBuilderPropertyGetterCallbackBuilder, IIWeirdPropertyGetterCallbackBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterCallbackBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertySetterContinuationBuilder : IIWeirdPropertyGetterCallbackBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterCallbackBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertySetterFluentBuilder : IIWeirdPropertyGetterCallbackBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterCallbackBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertySetterBuilder : IIWeirdPropertyGetterCallbackBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterCallbackBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterCallbackBuilderPropertyBuilder
        {
            IIWeirdPropertyGetterCallbackBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterCallbackBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterCallbackBuilderPropertyBuilder : IIWeirdPropertyGetterCallbackBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterCallbackBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterCallbackBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterCallbackBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterCallbackBuilderPropertyGetterBuilder, IIWeirdPropertyGetterCallbackBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterCallbackBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterCallbackBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterCallbackBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterCallbackBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterCallbackBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertyGetterCallbackBuilderPropertySetterBuilder, IIWeirdPropertyGetterCallbackBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterCallbackBuilderPropertySetterContinuationBuilder IIWeirdPropertyGetterCallbackBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterCallbackBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterCallbackBuilderPropertySetterFluentBuilder IIWeirdPropertyGetterCallbackBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterCallbackBuilderPropertyGetterBuilder IIWeirdPropertyGetterCallbackBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterCallbackBuilderPropertySetterBuilder IIWeirdPropertyGetterCallbackBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertyGetterContinuationBuilderPropertyBuilder IWeirdPropertyGetterContinuationBuilder => _IWeirdPropertyGetterContinuationBuilderPropertyBuilderField;

        private readonly IWeirdPropertyGetterContinuationBuilderPropertyBuilder _IWeirdPropertyGetterContinuationBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder : IIWeirdPropertyGetterContinuationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyGetterFluentBuilder : IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyGetterBuilder : IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterContinuationBuilderPropertyGetterCallbackBuilder, IIWeirdPropertyGetterContinuationBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterContinuationBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertySetterContinuationBuilder : IIWeirdPropertyGetterContinuationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterContinuationBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertySetterFluentBuilder : IIWeirdPropertyGetterContinuationBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterContinuationBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertySetterBuilder : IIWeirdPropertyGetterContinuationBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterContinuationBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterContinuationBuilderPropertyBuilder
        {
            IIWeirdPropertyGetterContinuationBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterContinuationBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterContinuationBuilderPropertyBuilder : IIWeirdPropertyGetterContinuationBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterContinuationBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterContinuationBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterContinuationBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterContinuationBuilderPropertyGetterBuilder, IIWeirdPropertyGetterContinuationBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterContinuationBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterContinuationBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterContinuationBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterContinuationBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterContinuationBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertyGetterContinuationBuilderPropertySetterBuilder, IIWeirdPropertyGetterContinuationBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterContinuationBuilderPropertySetterContinuationBuilder IIWeirdPropertyGetterContinuationBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterContinuationBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterContinuationBuilderPropertySetterFluentBuilder IIWeirdPropertyGetterContinuationBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterContinuationBuilderPropertyGetterBuilder IIWeirdPropertyGetterContinuationBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterContinuationBuilderPropertySetterBuilder IIWeirdPropertyGetterContinuationBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertyGetterFluentBuilderPropertyBuilder IWeirdPropertyGetterFluentBuilder => _IWeirdPropertyGetterFluentBuilderPropertyBuilderField;

        private readonly IWeirdPropertyGetterFluentBuilderPropertyBuilder _IWeirdPropertyGetterFluentBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder : IIWeirdPropertyGetterFluentBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterFluentBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyGetterFluentBuilder : IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyGetterBuilder : IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterFluentBuilderPropertyGetterCallbackBuilder, IIWeirdPropertyGetterFluentBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterFluentBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertySetterContinuationBuilder : IIWeirdPropertyGetterFluentBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterFluentBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertySetterFluentBuilder : IIWeirdPropertyGetterFluentBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterFluentBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertySetterBuilder : IIWeirdPropertyGetterFluentBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterFluentBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterFluentBuilderPropertyBuilder
        {
            IIWeirdPropertyGetterFluentBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterFluentBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterFluentBuilderPropertyBuilder : IIWeirdPropertyGetterFluentBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterFluentBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterFluentBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterFluentBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterFluentBuilderPropertyGetterBuilder, IIWeirdPropertyGetterFluentBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterFluentBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterFluentBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterFluentBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterFluentBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterFluentBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertyGetterFluentBuilderPropertySetterBuilder, IIWeirdPropertyGetterFluentBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterFluentBuilderPropertySetterContinuationBuilder IIWeirdPropertyGetterFluentBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterFluentBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterFluentBuilderPropertySetterFluentBuilder IIWeirdPropertyGetterFluentBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterFluentBuilderPropertyGetterBuilder IIWeirdPropertyGetterFluentBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterFluentBuilderPropertySetterBuilder IIWeirdPropertyGetterFluentBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertyGetterOutcomeBuilderPropertyBuilder IWeirdPropertyGetterOutcomeBuilder => _IWeirdPropertyGetterOutcomeBuilderPropertyBuilderField;

        private readonly IWeirdPropertyGetterOutcomeBuilderPropertyBuilder _IWeirdPropertyGetterOutcomeBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyGetterFluentBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyGetterBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertyGetterCallbackBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterOutcomeBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertySetterContinuationBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterOutcomeBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertySetterFluentBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertySetterBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterOutcomeBuilderPropertyBuilder
        {
            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterOutcomeBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterOutcomeBuilderPropertyBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterOutcomeBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterOutcomeBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterOutcomeBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterOutcomeBuilderPropertyGetterBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterOutcomeBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterOutcomeBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertyGetterOutcomeBuilderPropertySetterBuilder, IIWeirdPropertyGetterOutcomeBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterOutcomeBuilderPropertySetterContinuationBuilder IIWeirdPropertyGetterOutcomeBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterOutcomeBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterOutcomeBuilderPropertySetterFluentBuilder IIWeirdPropertyGetterOutcomeBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterOutcomeBuilderPropertyGetterBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterOutcomeBuilderPropertySetterBuilder IIWeirdPropertyGetterOutcomeBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertyGetterVerifierPropertyBuilder IWeirdPropertyGetterVerifier => _IWeirdPropertyGetterVerifierPropertyBuilderField;

        private readonly IWeirdPropertyGetterVerifierPropertyBuilder _IWeirdPropertyGetterVerifierPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder : IIWeirdPropertyGetterVerifierPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterVerifierPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyGetterFluentBuilder : IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyGetterBuilder : IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterVerifierPropertyGetterCallbackBuilder, IIWeirdPropertyGetterVerifierPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterVerifierPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertySetterContinuationBuilder : IIWeirdPropertyGetterVerifierPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterVerifierPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertySetterFluentBuilder : IIWeirdPropertyGetterVerifierPropertySetterCallbackBuilder, IIWeirdPropertyGetterVerifierPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertySetterBuilder : IIWeirdPropertyGetterVerifierPropertySetterCallbackBuilder, IIWeirdPropertyGetterVerifierPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterVerifierPropertyBuilder
        {
            IIWeirdPropertyGetterVerifierPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterVerifierPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterVerifierPropertyBuilder : IIWeirdPropertyGetterVerifierPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterVerifierPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterVerifier");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget.IWeirdPropertyGetterVerifier");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterVerifierPropertyGetterBuilder, IIWeirdPropertyGetterVerifierPropertyGetterFluentBuilder
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

                IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertyGetterVerifierPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertyGetterVerifierPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterVerifierPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterVerifierPropertyGetterFluentBuilder IIWeirdPropertyGetterVerifierPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertyGetterVerifierPropertySetterBuilder, IIWeirdPropertyGetterVerifierPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterVerifierPropertySetterContinuationBuilder IIWeirdPropertyGetterVerifierPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterVerifierPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterVerifierPropertySetterFluentBuilder IIWeirdPropertyGetterVerifierPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterVerifierPropertyGetterBuilder IIWeirdPropertyGetterVerifierPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterVerifierPropertySetterBuilder IIWeirdPropertyGetterVerifierPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertyGetterBuilderNameCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._IWeirdPropertyGetterBuilderPropertyBuilderField = new IWeirdPropertyGetterBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertyGetterCallbackBuilderPropertyBuilderField = new IWeirdPropertyGetterCallbackBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertyGetterContinuationBuilderPropertyBuilderField = new IWeirdPropertyGetterContinuationBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertyGetterFluentBuilderPropertyBuilderField = new IWeirdPropertyGetterFluentBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertyGetterOutcomeBuilderPropertyBuilderField = new IWeirdPropertyGetterOutcomeBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertyGetterVerifierPropertyBuilderField = new IWeirdPropertyGetterVerifierPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget
        {
            IPropertyGetterBuilderNameCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IPropertyGetterBuilderNameCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int IWeirdPropertyGetterBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertyGetterBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertyGetterCallbackBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterCallbackBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertyGetterCallbackBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertyGetterContinuationBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterContinuationBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertyGetterContinuationBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertyGetterFluentBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterFluentBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertyGetterFluentBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertyGetterOutcomeBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterOutcomeBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertyGetterOutcomeBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertyGetterVerifier
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterVerifierPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertyGetterVerifierPropertyBuilderField._setterImposter.Set(value);
                }
            }
        }
    }
}
#pragma warning restore nullable
