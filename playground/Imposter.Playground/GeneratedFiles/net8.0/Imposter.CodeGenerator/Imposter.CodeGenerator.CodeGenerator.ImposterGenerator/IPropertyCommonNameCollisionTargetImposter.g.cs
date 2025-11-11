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
    public class IPropertyCommonNameCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyCommonNameCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.IPropertyCommonNameCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyCommonNameCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public ICountPropertyBuilder Count => _CountPropertyBuilderField;

        private readonly CountPropertyBuilder _CountPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyGetterOutcomeBuilder
        {
            ICountPropertyGetterContinuationBuilder Returns(int value);
            ICountPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            ICountPropertyGetterContinuationBuilder Throws(System.Exception exception);
            ICountPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyGetterCallbackBuilder
        {
            ICountPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyGetterContinuationBuilder : ICountPropertyGetterCallbackBuilder
        {
            ICountPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyGetterFluentBuilder : ICountPropertyGetterOutcomeBuilder, ICountPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyGetterBuilder : ICountPropertyGetterOutcomeBuilder, ICountPropertyGetterCallbackBuilder, ICountPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertySetterCallbackBuilder
        {
            ICountPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertySetterContinuationBuilder : ICountPropertySetterCallbackBuilder
        {
            ICountPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertySetterFluentBuilder : ICountPropertySetterCallbackBuilder, ICountPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertySetterBuilder : ICountPropertySetterCallbackBuilder, ICountPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICountPropertyBuilder
        {
            ICountPropertyGetterBuilder Getter();
            ICountPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CountPropertyBuilder : ICountPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal CountPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyCommonNameCollisionTarget.Count");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyCommonNameCollisionTarget.Count");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : ICountPropertyGetterBuilder, ICountPropertyGetterFluentBuilder
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

                ICountPropertyGetterContinuationBuilder ICountPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                ICountPropertyGetterContinuationBuilder ICountPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                ICountPropertyGetterContinuationBuilder ICountPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                ICountPropertyGetterContinuationBuilder ICountPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                ICountPropertyGetterContinuationBuilder ICountPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void ICountPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                ICountPropertyGetterFluentBuilder ICountPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : ICountPropertySetterBuilder, ICountPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    ICountPropertySetterContinuationBuilder ICountPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void ICountPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    ICountPropertySetterFluentBuilder ICountPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            ICountPropertyGetterBuilder ICountPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            ICountPropertySetterBuilder ICountPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IDefaultPropertyBuilder Default => _DefaultPropertyBuilderField;

        private readonly DefaultPropertyBuilder _DefaultPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyGetterOutcomeBuilder
        {
            IDefaultPropertyGetterContinuationBuilder Returns(int value);
            IDefaultPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IDefaultPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IDefaultPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyGetterCallbackBuilder
        {
            IDefaultPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyGetterContinuationBuilder : IDefaultPropertyGetterCallbackBuilder
        {
            IDefaultPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyGetterFluentBuilder : IDefaultPropertyGetterOutcomeBuilder, IDefaultPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyGetterBuilder : IDefaultPropertyGetterOutcomeBuilder, IDefaultPropertyGetterCallbackBuilder, IDefaultPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertySetterCallbackBuilder
        {
            IDefaultPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertySetterContinuationBuilder : IDefaultPropertySetterCallbackBuilder
        {
            IDefaultPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertySetterFluentBuilder : IDefaultPropertySetterCallbackBuilder, IDefaultPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertySetterBuilder : IDefaultPropertySetterCallbackBuilder, IDefaultPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultPropertyBuilder
        {
            IDefaultPropertyGetterBuilder Getter();
            IDefaultPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultPropertyBuilder : IDefaultPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal DefaultPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyCommonNameCollisionTarget.Default");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyCommonNameCollisionTarget.Default");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IDefaultPropertyGetterBuilder, IDefaultPropertyGetterFluentBuilder
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

                IDefaultPropertyGetterContinuationBuilder IDefaultPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IDefaultPropertyGetterContinuationBuilder IDefaultPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IDefaultPropertyGetterContinuationBuilder IDefaultPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IDefaultPropertyGetterContinuationBuilder IDefaultPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IDefaultPropertyGetterContinuationBuilder IDefaultPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IDefaultPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IDefaultPropertyGetterFluentBuilder IDefaultPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IDefaultPropertySetterBuilder, IDefaultPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IDefaultPropertySetterContinuationBuilder IDefaultPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IDefaultPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IDefaultPropertySetterFluentBuilder IDefaultPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IDefaultPropertyGetterBuilder IDefaultPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IDefaultPropertySetterBuilder IDefaultPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertyCommonNameCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._CountPropertyBuilderField = new CountPropertyBuilder(invocationBehavior);
            this._DefaultPropertyBuilderField = new DefaultPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.IPropertyCommonNameCollisionTarget
        {
            IPropertyCommonNameCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IPropertyCommonNameCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Count
            {
                get
                {
                    return _imposter._CountPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._CountPropertyBuilderField._setterImposter.Set(value);
                }
            }

            public int Default
            {
                get
                {
                    return _imposter._DefaultPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._DefaultPropertyBuilderField._setterImposter.Set(value);
                }
            }
        }
    }
}
#pragma warning restore nullable
