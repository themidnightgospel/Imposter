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
    public class IPropertyDuplicateAccessorWithSetterCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IReusedPropertyBuilder Reused => _ReusedPropertyBuilderField;

        private readonly ReusedPropertyBuilder _ReusedPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyGetterOutcomeBuilder
        {
            IReusedPropertyGetterContinuationBuilder Returns(int value);
            IReusedPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IReusedPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IReusedPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyGetterCallbackBuilder
        {
            IReusedPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyGetterContinuationBuilder : IReusedPropertyGetterCallbackBuilder
        {
            IReusedPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyGetterFluentBuilder : IReusedPropertyGetterOutcomeBuilder, IReusedPropertyGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyGetterBuilder : IReusedPropertyGetterOutcomeBuilder, IReusedPropertyGetterCallbackBuilder, IReusedPropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertySetterCallbackBuilder
        {
            IReusedPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertySetterContinuationBuilder : IReusedPropertySetterCallbackBuilder
        {
            IReusedPropertySetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertySetterFluentBuilder : IReusedPropertySetterCallbackBuilder, IReusedPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertySetterBuilder : IReusedPropertySetterCallbackBuilder, IReusedPropertySetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReusedPropertyBuilder
        {
            IReusedPropertyGetterBuilder Getter();
            IReusedPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReusedPropertyBuilder : IReusedPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal ReusedPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget.Reused");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget.Reused");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IReusedPropertyGetterBuilder, IReusedPropertyGetterFluentBuilder
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

                IReusedPropertyGetterContinuationBuilder IReusedPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IReusedPropertyGetterContinuationBuilder IReusedPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IReusedPropertyGetterContinuationBuilder IReusedPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IReusedPropertyGetterContinuationBuilder IReusedPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IReusedPropertyGetterContinuationBuilder IReusedPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IReusedPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IReusedPropertyGetterFluentBuilder IReusedPropertyGetterContinuationBuilder.Then()
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
                internal class Builder : IReusedPropertySetterBuilder, IReusedPropertySetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IReusedPropertySetterContinuationBuilder IReusedPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IReusedPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IReusedPropertySetterFluentBuilder IReusedPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IReusedPropertyGetterBuilder IReusedPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IReusedPropertySetterBuilder IReusedPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertyDuplicateAccessorWithSetterCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._ReusedPropertyBuilderField = new ReusedPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget
        {
            IPropertyDuplicateAccessorWithSetterCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IPropertyDuplicateAccessorWithSetterCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Reused
            {
                get
                {
                    return _imposter._ReusedPropertyBuilderField._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._ReusedPropertyBuilderField._setterImposter.Set(value);
                }
            }
        }
    }
}
#pragma warning restore nullable
