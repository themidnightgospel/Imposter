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
    public class PropertySetterBuilderClassCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        public IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder IWeirdPropertySetterUseBaseImplementationBuilder => _IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilderField;

        private readonly IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder _IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilderField;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Returns(int value);
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Throws(System.Exception exception);
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder Callback(System.Action callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterVerifier
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterContinuationBuilder Callback(System.Action<int> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterContinuationBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterCallbackBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterFluentBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterCallbackBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterVerifier
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterFluentBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterFluentBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder
        {
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterBuilder Getter();
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterBuilder Setter(global::Imposter.Abstractions.Arg<int> criteria);
            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget.IWeirdPropertySetterUseBaseImplementationBuilder");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget.IWeirdPropertySetterUseBaseImplementationBuilder");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal bool HasValueSet = false;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder
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

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterCallbackBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterBuilder.Then()
                {
                    return this;
                }

                internal void EnableBaseImplementation()
                {
                    _defaultPropertyBehaviour.IsOn = true;
                    _useBaseImplementation = true;
                    _hasConfiguredReturn = true;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterUseBaseImplementationBuilder.UseBaseImplementation()
                {
                    EnableBaseImplementation();
                    return this;
                }

                IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterFluentBuilder.UseBaseImplementation()
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
                internal class Builder : IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterFluentBuilder, IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly global::Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, global::Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterContinuationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterCallbackBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterContinuationBuilder.Then()
                    {
                        return this;
                    }

                    IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterBuilder.Then()
                    {
                        return this;
                    }

                    IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterFluentBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterUseBaseImplementationBuilder.UseBaseImplementation()
                    {
                        _setterImposter.UseBaseImplementation();
                        return this;
                    }
                }
            }

            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyGetterBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IIWeirdPropertySetterUseBaseImplementationBuilderPropertySetterBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder.Setter(global::Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder IIWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder.UseBaseImplementation()
            {
                _getterImposterBuilder.EnableBaseImplementation();
                _setterImposter.UseBaseImplementation();
                return this;
            }
        }

        public PropertySetterBuilderClassCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilderField = new IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget
        {
            PropertySetterBuilderClassCollisionTargetImposter _imposter;
            internal void InitializeImposter(PropertySetterBuilderClassCollisionTargetImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            public override int IWeirdPropertySetterUseBaseImplementationBuilder
            {
                get
                {
                    return _imposter._IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilderField._getterImposterBuilder.Get(() => base.IWeirdPropertySetterUseBaseImplementationBuilder);
                }

                set
                {
                    _imposter._IWeirdPropertySetterUseBaseImplementationBuilderPropertyBuilderField._setterImposter.Set(value, () =>
                    {
                        base.IWeirdPropertySetterUseBaseImplementationBuilder = value;
                    });
                }
            }
        }
    }
}
#pragma warning restore nullable
