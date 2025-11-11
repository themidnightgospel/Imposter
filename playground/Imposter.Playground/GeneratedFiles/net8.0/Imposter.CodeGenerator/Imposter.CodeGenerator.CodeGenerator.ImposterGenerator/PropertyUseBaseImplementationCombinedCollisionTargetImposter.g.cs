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
    public class PropertyUseBaseImplementationCombinedCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IThenPropertyBuilder Then => _ThenPropertyBuilderField;

        private readonly ThenPropertyBuilder _ThenPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterOutcomeBuilder
        {
            IThenPropertyGetterContinuationBuilder Returns(int value);
            IThenPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IThenPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IThenPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterCallbackBuilder
        {
            IThenPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterContinuationBuilder : IThenPropertyGetterCallbackBuilder
        {
            IThenPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterFluentBuilder : IThenPropertyGetterOutcomeBuilder, IThenPropertyGetterContinuationBuilder
        {
            IThenPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterBuilder : IThenPropertyGetterOutcomeBuilder, IThenPropertyGetterCallbackBuilder, IThenPropertyGetterVerifier
        {
            IThenPropertyGetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyGetterUseBaseImplementationBuilder
        {
            IThenPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertySetterCallbackBuilder
        {
            IThenPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertySetterContinuationBuilder : IThenPropertySetterCallbackBuilder
        {
            IThenPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertySetterFluentBuilder : IThenPropertySetterCallbackBuilder, IThenPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertySetterBuilder : IThenPropertySetterCallbackBuilder, IThenPropertySetterVerifier
        {
            IThenPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertySetterUseBaseImplementationBuilder : IThenPropertySetterFluentBuilder
        {
            IThenPropertySetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenPropertyBuilder
        {
            IThenPropertyGetterBuilder Getter();
            IThenPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
            IThenPropertyBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThenPropertyBuilder : IThenPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal ThenPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget.Then");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget.Then");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IThenPropertyGetterBuilder, IThenPropertyGetterFluentBuilder, IThenPropertyGetterUseBaseImplementationBuilder
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

                IThenPropertyGetterContinuationBuilder IThenPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IThenPropertyGetterContinuationBuilder IThenPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IThenPropertyGetterContinuationBuilder IThenPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IThenPropertyGetterContinuationBuilder IThenPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IThenPropertyGetterContinuationBuilder IThenPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IThenPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IThenPropertyGetterFluentBuilder IThenPropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                IThenPropertyGetterUseBaseImplementationBuilder IThenPropertyGetterBuilder.Then()
                {
                    return this;
                }

                internal void EnableBaseImplementation()
                {
                    _defaultPropertyBehaviour.IsOn = true;
                    _useBaseImplementation = true;
                    _hasConfiguredReturn = true;
                }

                IThenPropertyGetterFluentBuilder IThenPropertyGetterUseBaseImplementationBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
                    return this;
                }

                IThenPropertyGetterFluentBuilder IThenPropertyGetterFluentBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
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

                internal void UseBaseImplementation()
                {
                    _hasConfiguredSetter = true;
                    _useBaseImplementation = true;
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
                internal class Builder : IThenPropertySetterBuilder, IThenPropertySetterFluentBuilder, IThenPropertySetterUseBaseImplementationBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IThenPropertySetterContinuationBuilder IThenPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IThenPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IThenPropertySetterUseBaseImplementationBuilder IThenPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    IThenPropertySetterUseBaseImplementationBuilder IThenPropertySetterBuilder.Then()
                    {
                        return this;
                    }

                    IThenPropertySetterFluentBuilder IThenPropertySetterUseBaseImplementationBuilder.UseBaseImplementation()
                    {
                        _setterImposter.UseBaseImplementation();
                        return this;
                    }
                }
            }

            IThenPropertyGetterBuilder IThenPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IThenPropertySetterBuilder IThenPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            IThenPropertyBuilder IThenPropertyBuilder.UseBaseImplementation()
            {
                _getterImposterBuilder.EnableBaseImplementation();
                _setterImposter.UseBaseImplementation();
                return this;
            }
        }

        public IUseBaseImplementationPropertyBuilder UseBaseImplementation => _UseBaseImplementationPropertyBuilderField;

        private readonly UseBaseImplementationPropertyBuilder _UseBaseImplementationPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterOutcomeBuilder
        {
            IUseBaseImplementationPropertyGetterContinuationBuilder Returns(int value);
            IUseBaseImplementationPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IUseBaseImplementationPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IUseBaseImplementationPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterCallbackBuilder
        {
            IUseBaseImplementationPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterContinuationBuilder : IUseBaseImplementationPropertyGetterCallbackBuilder
        {
            IUseBaseImplementationPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterFluentBuilder : IUseBaseImplementationPropertyGetterOutcomeBuilder, IUseBaseImplementationPropertyGetterContinuationBuilder
        {
            IUseBaseImplementationPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterBuilder : IUseBaseImplementationPropertyGetterOutcomeBuilder, IUseBaseImplementationPropertyGetterCallbackBuilder, IUseBaseImplementationPropertyGetterVerifier
        {
            IUseBaseImplementationPropertyGetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyGetterUseBaseImplementationBuilder
        {
            IUseBaseImplementationPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertySetterCallbackBuilder
        {
            IUseBaseImplementationPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertySetterContinuationBuilder : IUseBaseImplementationPropertySetterCallbackBuilder
        {
            IUseBaseImplementationPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertySetterFluentBuilder : IUseBaseImplementationPropertySetterCallbackBuilder, IUseBaseImplementationPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertySetterBuilder : IUseBaseImplementationPropertySetterCallbackBuilder, IUseBaseImplementationPropertySetterVerifier
        {
            IUseBaseImplementationPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertySetterUseBaseImplementationBuilder : IUseBaseImplementationPropertySetterFluentBuilder
        {
            IUseBaseImplementationPropertySetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationPropertyBuilder
        {
            IUseBaseImplementationPropertyGetterBuilder Getter();
            IUseBaseImplementationPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
            IUseBaseImplementationPropertyBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class UseBaseImplementationPropertyBuilder : IUseBaseImplementationPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal UseBaseImplementationPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget.UseBaseImplementation");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget.UseBaseImplementation");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IUseBaseImplementationPropertyGetterBuilder, IUseBaseImplementationPropertyGetterFluentBuilder, IUseBaseImplementationPropertyGetterUseBaseImplementationBuilder
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

                IUseBaseImplementationPropertyGetterContinuationBuilder IUseBaseImplementationPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IUseBaseImplementationPropertyGetterContinuationBuilder IUseBaseImplementationPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IUseBaseImplementationPropertyGetterContinuationBuilder IUseBaseImplementationPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IUseBaseImplementationPropertyGetterContinuationBuilder IUseBaseImplementationPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IUseBaseImplementationPropertyGetterContinuationBuilder IUseBaseImplementationPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IUseBaseImplementationPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IUseBaseImplementationPropertyGetterFluentBuilder IUseBaseImplementationPropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                IUseBaseImplementationPropertyGetterUseBaseImplementationBuilder IUseBaseImplementationPropertyGetterBuilder.Then()
                {
                    return this;
                }

                internal void EnableBaseImplementation()
                {
                    _defaultPropertyBehaviour.IsOn = true;
                    _useBaseImplementation = true;
                    _hasConfiguredReturn = true;
                }

                IUseBaseImplementationPropertyGetterFluentBuilder IUseBaseImplementationPropertyGetterUseBaseImplementationBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
                    return this;
                }

                IUseBaseImplementationPropertyGetterFluentBuilder IUseBaseImplementationPropertyGetterFluentBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
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

                internal void UseBaseImplementation()
                {
                    _hasConfiguredSetter = true;
                    _useBaseImplementation = true;
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
                internal class Builder : IUseBaseImplementationPropertySetterBuilder, IUseBaseImplementationPropertySetterFluentBuilder, IUseBaseImplementationPropertySetterUseBaseImplementationBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IUseBaseImplementationPropertySetterContinuationBuilder IUseBaseImplementationPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IUseBaseImplementationPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IUseBaseImplementationPropertySetterUseBaseImplementationBuilder IUseBaseImplementationPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    IUseBaseImplementationPropertySetterUseBaseImplementationBuilder IUseBaseImplementationPropertySetterBuilder.Then()
                    {
                        return this;
                    }

                    IUseBaseImplementationPropertySetterFluentBuilder IUseBaseImplementationPropertySetterUseBaseImplementationBuilder.UseBaseImplementation()
                    {
                        _setterImposter.UseBaseImplementation();
                        return this;
                    }
                }
            }

            IUseBaseImplementationPropertyGetterBuilder IUseBaseImplementationPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IUseBaseImplementationPropertySetterBuilder IUseBaseImplementationPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            IUseBaseImplementationPropertyBuilder IUseBaseImplementationPropertyBuilder.UseBaseImplementation()
            {
                _getterImposterBuilder.EnableBaseImplementation();
                _setterImposter.UseBaseImplementation();
                return this;
            }
        }

        public PropertyUseBaseImplementationCombinedCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._ThenPropertyBuilderField = new ThenPropertyBuilder(invocationBehavior);
            this._UseBaseImplementationPropertyBuilderField = new UseBaseImplementationPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget
        {
            PropertyUseBaseImplementationCombinedCollisionTargetImposter _imposter;
            internal void InitializeImposter(PropertyUseBaseImplementationCombinedCollisionTargetImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            public override int Then
            {
                get
                {
                    return _imposter._ThenPropertyBuilderField._getterImposterBuilder.Get(() => base.Then);
                }

                set
                {
                    _imposter._ThenPropertyBuilderField._setterImposter.Set(value, () =>
                    {
                        base.Then = value;
                    });
                }
            }

            public override int UseBaseImplementation
            {
                get
                {
                    return _imposter._UseBaseImplementationPropertyBuilderField._getterImposterBuilder.Get(() => base.UseBaseImplementation);
                }

                set
                {
                    _imposter._UseBaseImplementationPropertyBuilderField._setterImposter.Set(value, () =>
                    {
                        base.UseBaseImplementation = value;
                    });
                }
            }
        }
    }
}
#pragma warning restore nullable
