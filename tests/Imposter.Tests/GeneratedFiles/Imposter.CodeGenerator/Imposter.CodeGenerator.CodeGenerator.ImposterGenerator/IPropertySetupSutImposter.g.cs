using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.PropertyImposter;

#pragma warning disable nullable
namespace Imposter.Tests.Features.PropertyImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertySetupSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.PropertyImposter.IPropertySetupSut>
    {
        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.PropertyImposter.IPropertySetupSut Instance() => _imposterInstance;
        global::Imposter.Tests.Features.PropertyImposter.IPropertySetupSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.PropertyImposter.IPropertySetupSut>.Instance()
        {
            return _imposterInstance;
        }

        public IAgePropertyBuilder Age => _Age;

        private readonly AgePropertyBuilder _Age;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyGetterOutcomeBuilder
        {
            IAgePropertyGetterContinuationBuilder Returns(int value);
            IAgePropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IAgePropertyGetterContinuationBuilder Throws(System.Exception exception);
            IAgePropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyGetterContinuationBuilder
        {
            IAgePropertyGetterContinuationBuilder Callback(System.Action callback);
            IAgePropertyGetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyGetterVerifier
        {
            void Called(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyGetterBuilder : IAgePropertyGetterOutcomeBuilder, IAgePropertyGetterContinuationBuilder, IAgePropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertySetterBuilder
        {
            IAgePropertySetterBuilder Callback(System.Action<int> callback);
            void Called(Imposter.Abstractions.Count count);
            IAgePropertySetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyBuilder
        {
            IAgePropertyGetterBuilder Getter();
            IAgePropertySetterBuilder Setter(Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AgePropertyBuilder : IAgePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal AgePropertyBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Age");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : IAgePropertyGetterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastReturnValue = () => default;
                private int _invocationCount;
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposterBuilder(DefaultPropertyBehaviour _defaultPropertyBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
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

                IAgePropertyGetterContinuationBuilder IAgePropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IAgePropertyGetterContinuationBuilder IAgePropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IAgePropertyGetterContinuationBuilder IAgePropertyGetterOutcomeBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IAgePropertyGetterContinuationBuilder IAgePropertyGetterOutcomeBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IAgePropertyGetterContinuationBuilder IAgePropertyGetterContinuationBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IAgePropertyGetterVerifier.Called(Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                IAgePropertyGetterBuilder IAgePropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                internal int Get()
                {
                    EnsureGetterConfigured();
                    System.Threading.Interlocked.Increment(ref _invocationCount);
                    foreach (var getterCallback in _callbacks)
                    {
                        getterCallback();
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        return _defaultPropertyBehaviour.BackingField;
                    if (_returnValues.TryDequeue(out var returnValue))
                        _lastReturnValue = returnValue;
                    return _lastReturnValue();
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !_hasConfiguredReturn)
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal void Callback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                }

                void Called(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(int value)
                {
                    EnsureSetterConfigured();
                    _invocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _callbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField = value;
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !_hasConfiguredSetter)
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                }

                internal void MarkConfigured()
                {
                    _hasConfiguredSetter = true;
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IAgePropertySetterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IAgePropertySetterBuilder IAgePropertySetterBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IAgePropertySetterBuilder.Called(Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IAgePropertySetterBuilder IAgePropertySetterBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            IAgePropertyGetterBuilder IAgePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IAgePropertySetterBuilder IAgePropertyBuilder.Setter(Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public INamePropertyBuilder Name => _Name;

        private readonly NamePropertyBuilder _Name;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterOutcomeBuilder
        {
            INamePropertyGetterContinuationBuilder Returns(int value);
            INamePropertyGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            INamePropertyGetterContinuationBuilder Throws(System.Exception exception);
            INamePropertyGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterContinuationBuilder
        {
            INamePropertyGetterContinuationBuilder Callback(System.Action callback);
            INamePropertyGetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterVerifier
        {
            void Called(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterBuilder : INamePropertyGetterOutcomeBuilder, INamePropertyGetterContinuationBuilder, INamePropertyGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyBuilder
        {
            INamePropertyGetterBuilder Getter();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class NamePropertyBuilder : INamePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal NamePropertyBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.Name");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : INamePropertyGetterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastReturnValue = () => default;
                private int _invocationCount;
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposterBuilder(DefaultPropertyBehaviour _defaultPropertyBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
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

                INamePropertyGetterContinuationBuilder INamePropertyGetterOutcomeBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                INamePropertyGetterContinuationBuilder INamePropertyGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
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

                INamePropertyGetterContinuationBuilder INamePropertyGetterContinuationBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void INamePropertyGetterVerifier.Called(Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                INamePropertyGetterBuilder INamePropertyGetterContinuationBuilder.Then()
                {
                    return this;
                }

                internal int Get()
                {
                    EnsureGetterConfigured();
                    System.Threading.Interlocked.Increment(ref _invocationCount);
                    foreach (var getterCallback in _callbacks)
                    {
                        getterCallback();
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        return _defaultPropertyBehaviour.BackingField;
                    if (_returnValues.TryDequeue(out var returnValue))
                        _lastReturnValue = returnValue;
                    return _lastReturnValue();
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !_hasConfiguredReturn)
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                }
            }

            INamePropertyGetterBuilder INamePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }
        }

        public ILastNamePropertyBuilder LastName => _LastName;

        private readonly LastNamePropertyBuilder _LastName;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ILastNamePropertySetterBuilder
        {
            ILastNamePropertySetterBuilder Callback(System.Action<int> callback);
            void Called(Imposter.Abstractions.Count count);
            ILastNamePropertySetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ILastNamePropertyBuilder
        {
            ILastNamePropertySetterBuilder Setter(Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class LastNamePropertyBuilder : ILastNamePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal LastNamePropertyBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.PropertyImposter.IPropertySetupSut.LastName");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal void Callback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                }

                void Called(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(int value)
                {
                    EnsureSetterConfigured();
                    _invocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _callbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField = value;
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !_hasConfiguredSetter)
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                }

                internal void MarkConfigured()
                {
                    _hasConfiguredSetter = true;
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : ILastNamePropertySetterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly Imposter.Abstractions.Arg<int> _criteria;
                    internal Builder(SetterImposter _setterImposter, Imposter.Abstractions.Arg<int> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    ILastNamePropertySetterBuilder ILastNamePropertySetterBuilder.Callback(System.Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void ILastNamePropertySetterBuilder.Called(Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    ILastNamePropertySetterBuilder ILastNamePropertySetterBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            ILastNamePropertySetterBuilder ILastNamePropertyBuilder.Setter(Imposter.Abstractions.Arg<int> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IPropertySetupSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._Age = new AgePropertyBuilder(invocationBehavior);
            this._Name = new NamePropertyBuilder(invocationBehavior);
            this._LastName = new LastNamePropertyBuilder(invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.PropertyImposter.IPropertySetupSut
        {
            IPropertySetupSutImposter _imposter;
            public ImposterTargetInstance(IPropertySetupSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Age
            {
                get
                {
                    return _imposter._Age._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._Age._setterImposter.Set(value);
                }
            }

            public int Name
            {
                get
                {
                    return _imposter._Name._getterImposterBuilder.Get();
                }
            }

            public int LastName
            {
                set
                {
                    _imposter._LastName._setterImposter.Set(value);
                }
            }
        }
    }
}