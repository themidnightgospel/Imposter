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
    public class IPropertySetterBuilderNameCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IIWeirdPropertySetterBuilderPropertyBuilder IWeirdPropertySetterBuilder => _IWeirdPropertySetterBuilderPropertyBuilderField;

        private readonly IWeirdPropertySetterBuilderPropertyBuilder _IWeirdPropertySetterBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder : IIWeirdPropertySetterBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyGetterFluentBuilder : IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyGetterBuilder : IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterBuilderPropertyGetterCallbackBuilder, IIWeirdPropertySetterBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertySetterContinuationBuilder : IIWeirdPropertySetterBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertySetterFluentBuilder : IIWeirdPropertySetterBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertySetterBuilder : IIWeirdPropertySetterBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterBuilderPropertyBuilder
        {
            IIWeirdPropertySetterBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertySetterBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertySetterBuilderPropertyBuilder : IIWeirdPropertySetterBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertySetterBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertySetterBuilderPropertyGetterBuilder, IIWeirdPropertySetterBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertySetterBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertySetterBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertySetterBuilderPropertySetterBuilder, IIWeirdPropertySetterBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertySetterBuilderPropertySetterContinuationBuilder IIWeirdPropertySetterBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertySetterBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertySetterBuilderPropertySetterFluentBuilder IIWeirdPropertySetterBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertySetterBuilderPropertyGetterBuilder IIWeirdPropertySetterBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertySetterBuilderPropertySetterBuilder IIWeirdPropertySetterBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertySetterCallbackBuilderPropertyBuilder IWeirdPropertySetterCallbackBuilder => _IWeirdPropertySetterCallbackBuilderPropertyBuilderField;

        private readonly IWeirdPropertySetterCallbackBuilderPropertyBuilder _IWeirdPropertySetterCallbackBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder : IIWeirdPropertySetterCallbackBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterCallbackBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyGetterFluentBuilder : IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyGetterBuilder : IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterCallbackBuilderPropertyGetterCallbackBuilder, IIWeirdPropertySetterCallbackBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterCallbackBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertySetterContinuationBuilder : IIWeirdPropertySetterCallbackBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterCallbackBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertySetterFluentBuilder : IIWeirdPropertySetterCallbackBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterCallbackBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertySetterBuilder : IIWeirdPropertySetterCallbackBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterCallbackBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterCallbackBuilderPropertyBuilder
        {
            IIWeirdPropertySetterCallbackBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertySetterCallbackBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertySetterCallbackBuilderPropertyBuilder : IIWeirdPropertySetterCallbackBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertySetterCallbackBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterCallbackBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterCallbackBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertySetterCallbackBuilderPropertyGetterBuilder, IIWeirdPropertySetterCallbackBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterCallbackBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterCallbackBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertySetterCallbackBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertySetterCallbackBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterCallbackBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertySetterCallbackBuilderPropertySetterBuilder, IIWeirdPropertySetterCallbackBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertySetterCallbackBuilderPropertySetterContinuationBuilder IIWeirdPropertySetterCallbackBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertySetterCallbackBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertySetterCallbackBuilderPropertySetterFluentBuilder IIWeirdPropertySetterCallbackBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertySetterCallbackBuilderPropertyGetterBuilder IIWeirdPropertySetterCallbackBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertySetterCallbackBuilderPropertySetterBuilder IIWeirdPropertySetterCallbackBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertySetterContinuationBuilderPropertyBuilder IWeirdPropertySetterContinuationBuilder => _IWeirdPropertySetterContinuationBuilderPropertyBuilderField;

        private readonly IWeirdPropertySetterContinuationBuilderPropertyBuilder _IWeirdPropertySetterContinuationBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder : IIWeirdPropertySetterContinuationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterContinuationBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyGetterFluentBuilder : IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyGetterBuilder : IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterContinuationBuilderPropertyGetterCallbackBuilder, IIWeirdPropertySetterContinuationBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterContinuationBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertySetterContinuationBuilder : IIWeirdPropertySetterContinuationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterContinuationBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertySetterFluentBuilder : IIWeirdPropertySetterContinuationBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterContinuationBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertySetterBuilder : IIWeirdPropertySetterContinuationBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterContinuationBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterContinuationBuilderPropertyBuilder
        {
            IIWeirdPropertySetterContinuationBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertySetterContinuationBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertySetterContinuationBuilderPropertyBuilder : IIWeirdPropertySetterContinuationBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertySetterContinuationBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterContinuationBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterContinuationBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertySetterContinuationBuilderPropertyGetterBuilder, IIWeirdPropertySetterContinuationBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterContinuationBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterContinuationBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertySetterContinuationBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertySetterContinuationBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterContinuationBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertySetterContinuationBuilderPropertySetterBuilder, IIWeirdPropertySetterContinuationBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertySetterContinuationBuilderPropertySetterContinuationBuilder IIWeirdPropertySetterContinuationBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertySetterContinuationBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertySetterContinuationBuilderPropertySetterFluentBuilder IIWeirdPropertySetterContinuationBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertySetterContinuationBuilderPropertyGetterBuilder IIWeirdPropertySetterContinuationBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertySetterContinuationBuilderPropertySetterBuilder IIWeirdPropertySetterContinuationBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertySetterFluentBuilderPropertyBuilder IWeirdPropertySetterFluentBuilder => _IWeirdPropertySetterFluentBuilderPropertyBuilderField;

        private readonly IWeirdPropertySetterFluentBuilderPropertyBuilder _IWeirdPropertySetterFluentBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder : IIWeirdPropertySetterFluentBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterFluentBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyGetterFluentBuilder : IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyGetterBuilder : IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterFluentBuilderPropertyGetterCallbackBuilder, IIWeirdPropertySetterFluentBuilderPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterFluentBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertySetterContinuationBuilder : IIWeirdPropertySetterFluentBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterFluentBuilderPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertySetterFluentBuilder : IIWeirdPropertySetterFluentBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterFluentBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertySetterBuilder : IIWeirdPropertySetterFluentBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterFluentBuilderPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterFluentBuilderPropertyBuilder
        {
            IIWeirdPropertySetterFluentBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertySetterFluentBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertySetterFluentBuilderPropertyBuilder : IIWeirdPropertySetterFluentBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertySetterFluentBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterFluentBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterFluentBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertySetterFluentBuilderPropertyGetterBuilder, IIWeirdPropertySetterFluentBuilderPropertyGetterFluentBuilder
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

                IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterFluentBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterFluentBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertySetterFluentBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertySetterFluentBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterFluentBuilderPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertySetterFluentBuilderPropertySetterBuilder, IIWeirdPropertySetterFluentBuilderPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertySetterFluentBuilderPropertySetterContinuationBuilder IIWeirdPropertySetterFluentBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertySetterFluentBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertySetterFluentBuilderPropertySetterFluentBuilder IIWeirdPropertySetterFluentBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertySetterFluentBuilderPropertyGetterBuilder IIWeirdPropertySetterFluentBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertySetterFluentBuilderPropertySetterBuilder IIWeirdPropertySetterFluentBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IIWeirdPropertySetterVerifierPropertyBuilder IWeirdPropertySetterVerifier => _IWeirdPropertySetterVerifierPropertyBuilderField;

        private readonly IWeirdPropertySetterVerifierPropertyBuilder _IWeirdPropertySetterVerifierPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder : IIWeirdPropertySetterVerifierPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterVerifierPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyGetterFluentBuilder : IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder, IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyGetterBuilder : IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder, IIWeirdPropertySetterVerifierPropertyGetterCallbackBuilder, IIWeirdPropertySetterVerifierPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterVerifierPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertySetterContinuationBuilder : IIWeirdPropertySetterVerifierPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterVerifierPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertySetterFluentBuilder : IIWeirdPropertySetterVerifierPropertySetterCallbackBuilder, IIWeirdPropertySetterVerifierPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertySetterBuilder : IIWeirdPropertySetterVerifierPropertySetterCallbackBuilder, IIWeirdPropertySetterVerifierPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterVerifierPropertyBuilder
        {
            IIWeirdPropertySetterVerifierPropertyGetterBuilder Getter();
            IIWeirdPropertySetterVerifierPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertySetterVerifierPropertyBuilder : IIWeirdPropertySetterVerifierPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertySetterVerifierPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterVerifier");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget.IWeirdPropertySetterVerifier");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertySetterVerifierPropertyGetterBuilder, IIWeirdPropertySetterVerifierPropertyGetterFluentBuilder
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

                IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertySetterVerifierPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder IIWeirdPropertySetterVerifierPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertySetterVerifierPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertySetterVerifierPropertyGetterFluentBuilder IIWeirdPropertySetterVerifierPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IIWeirdPropertySetterVerifierPropertySetterBuilder, IIWeirdPropertySetterVerifierPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertySetterVerifierPropertySetterContinuationBuilder IIWeirdPropertySetterVerifierPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertySetterVerifierPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertySetterVerifierPropertySetterFluentBuilder IIWeirdPropertySetterVerifierPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IIWeirdPropertySetterVerifierPropertyGetterBuilder IIWeirdPropertySetterVerifierPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertySetterVerifierPropertySetterBuilder IIWeirdPropertySetterVerifierPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertySetterBuilderNameCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._IWeirdPropertySetterBuilderPropertyBuilderField = new IWeirdPropertySetterBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertySetterCallbackBuilderPropertyBuilderField = new IWeirdPropertySetterCallbackBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertySetterContinuationBuilderPropertyBuilderField = new IWeirdPropertySetterContinuationBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertySetterFluentBuilderPropertyBuilderField = new IWeirdPropertySetterFluentBuilderPropertyBuilder(invocationBehavior);
            this._IWeirdPropertySetterVerifierPropertyBuilderField = new IWeirdPropertySetterVerifierPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget
        {
            IPropertySetterBuilderNameCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IPropertySetterBuilderNameCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int IWeirdPropertySetterBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertySetterBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertySetterBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertySetterCallbackBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertySetterCallbackBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertySetterCallbackBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertySetterContinuationBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertySetterContinuationBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertySetterContinuationBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertySetterFluentBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertySetterFluentBuilderPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertySetterFluentBuilderPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int IWeirdPropertySetterVerifier
            {
                get
                {
                    return _imposter._IWeirdPropertySetterVerifierPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._IWeirdPropertySetterVerifierPropertyBuilderField._setterImposter.Set(value);
                }
            }
        }
    }
}
#pragma warning restore nullable
