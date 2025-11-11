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
    public class PropertyGetterBuilderClassCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder IWeirdPropertyGetterUseBaseImplementationBuilder => _IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilderField;

        private readonly IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder _IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterVerifier
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterContinuationBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterFluentBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterCallbackBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterVerifier
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterFluentBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder
        {
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget.IWeirdPropertyGetterUseBaseImplementationBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget.IWeirdPropertyGetterUseBaseImplementationBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder
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

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterBuilder.Then()
                {
                    return this;
                }

                internal void EnableBaseImplementation()
                {
                    _defaultPropertyBehaviour.IsOn = true;
                    _useBaseImplementation = true;
                    _hasConfiguredReturn = true;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
                    return this;
                }

                IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterFluentBuilder.UseBaseImplementation()
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
                internal class Builder : IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterFluentBuilder, IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterContinuationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterBuilder.Then()
                    {
                        return this;
                    }

                    IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterFluentBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder.UseBaseImplementation()
                    {
                        _setterImposter.UseBaseImplementation();
                        return this;
                    }
                }
            }

            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyGetterBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertySetterBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder IIWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder.UseBaseImplementation()
            {
                _getterImposterBuilder.EnableBaseImplementation();
                _setterImposter.UseBaseImplementation();
                return this;
            }
        }

        public PropertyGetterBuilderClassCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilderField = new IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget
        {
            PropertyGetterBuilderClassCollisionTargetImposter _imposter;
            internal void InitializeImposter(PropertyGetterBuilderClassCollisionTargetImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            public override int IWeirdPropertyGetterUseBaseImplementationBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilderField._getterImposterBuilder.Get(() => base.IWeirdPropertyGetterUseBaseImplementationBuilder);
                }

                set
                {
                    _imposter._IWeirdPropertyGetterUseBaseImplementationBuilderPropertyBuilderField._setterImposter.Set(value, () =>
                    {
                        base.IWeirdPropertyGetterUseBaseImplementationBuilder = value;
                    });
                }
            }
        }
    }
}
#pragma warning restore nullable
