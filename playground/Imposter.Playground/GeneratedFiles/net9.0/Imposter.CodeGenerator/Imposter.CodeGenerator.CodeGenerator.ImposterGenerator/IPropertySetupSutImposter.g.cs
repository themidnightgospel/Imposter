using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Playground;

#pragma warning disable nullable
namespace Imposter.Playground
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertySetupSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IPropertySetupSut>
    {
        private ImposterTargetInstance _imposterInstance;
        public IPropertySetupSutImposter()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.Playground.IPropertySetupSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IPropertySetupSut>.Instance()
        {
            return _imposterInstance;
        }

        public IAgePropertyBuilder Age => _Age;

        private readonly AgePropertyBuilder _Age = new AgePropertyBuilder();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyGetterBuilder
        {
            IAgePropertyGetterBuilder Returns(int value);
            IAgePropertyGetterBuilder Returns(System.Func<int> valueGenerator);
            IAgePropertyGetterBuilder Throws(System.Exception exception);
            IAgePropertyGetterBuilder Throws<TException>()
                where TException : Exception, new();
            IAgePropertyGetterBuilder GetterCallback(System.Action callback);
            void GetterCalled(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertySetterBuilder
        {
            IAgePropertySetterBuilder Callback(System.Action<int> callback);
            void Called(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyBuilder
        {
            IAgePropertyGetterBuilder Getter();
            IAgePropertySetterBuilder Setter(Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AgePropertyBuilder : IAgePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly SetterImposter _setterImposter;
            private readonly GetterImposterBuilder _getterImposterBuilder;
            AgePropertyBuilder()
            {
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour);
                _getterImposterBuilder = new GetterImposterBuilder(_getterImposterBuilder);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class DefaultPropertyBehaviour
            {
                private readonly bool IsOn = true;
                private readonly int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class GetterImposterBuilder : IAgePropertyGetterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _getterReturnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _getterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastGetterReturnValue = () => default;
                private int _getterInvocationCount;
                private void AddGetterReturnValue(System.Func<int> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _getterReturnValues.Enqueue(valueGenerator);
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Returns(int value)
                {
                    AddGetterReturnValue(() => value);
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddGetterReturnValue(valueGenerator);
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Throws(System.Exception exception)
                {
                    AddGetterReturnValue(() => throw exception);
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Throws<TException>()
                {
                    AddGetterReturnValue(() => throw new TException());
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.GetterCallback(System.Action callback)
                {
                    _getterCallbacks.Enqueue(callback);
                    return this;
                }

                void IAgePropertyGetterBuilder.GetterCalled(Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_getterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, _getterInvocationCount);
                }

                internal int Get()
                {
                    System.Threading.Interlocked.Increment(ref _getterInvocationCount);
                    foreach (var getterCallback in _getterCallbacks)
                    {
                        getterCallback();
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        return _defaultPropertyBehaviour.BackingField;
                    if (_getterReturnValues.TryDequeue(out var returnValue))
                        _lastGetterReturnValue = returnValue;
                    return _lastGetterReturnValue();
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _setterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _setterInvocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                }

                internal void Callback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
                {
                    _setterCallbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                    return this;
                }

                void Called(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count)
                {
                    var setterInvocationCount = _setterInvocationHistory.Count(criteria.Matches);
                    if (!count.Matches(setterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, setterInvocationCount);
                }

                internal void Set(int value)
                {
                    _setterInvocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _setterCallbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField = value;
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                class Builder : IAgePropertySetterBuilder
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
                }
            }

            IAgePropertyGetterBuilder IAgePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IAgePropertySetterBuilder IAgePropertyBuilder.Setter(Imposter.Abstractions.Arg<int> criteria)
            {
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public INamePropertyBuilder Name => _Name;

        private readonly NamePropertyBuilder _Name = new NamePropertyBuilder();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterBuilder
        {
            INamePropertyGetterBuilder Returns(int value);
            INamePropertyGetterBuilder Returns(System.Func<int> valueGenerator);
            INamePropertyGetterBuilder Throws(System.Exception exception);
            INamePropertyGetterBuilder Throws<TException>()
                where TException : Exception, new();
            INamePropertyGetterBuilder GetterCallback(System.Action callback);
            void GetterCalled(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyBuilder
        {
            INamePropertyGetterBuilder Getter();
            INamePropertySetterBuilder Setter(Imposter.Abstractions.Arg<int> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class NamePropertyBuilder : INamePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly SetterImposter _setterImposter;
            private readonly GetterImposterBuilder _getterImposterBuilder;
            NamePropertyBuilder()
            {
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour);
                _getterImposterBuilder = new GetterImposterBuilder(_getterImposterBuilder);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class DefaultPropertyBehaviour
            {
                private readonly bool IsOn = true;
                private readonly int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class GetterImposterBuilder : INamePropertyGetterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _getterReturnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _getterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastGetterReturnValue = () => default;
                private int _getterInvocationCount;
                private void AddGetterReturnValue(System.Func<int> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _getterReturnValues.Enqueue(valueGenerator);
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Returns(int value)
                {
                    AddGetterReturnValue(() => value);
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddGetterReturnValue(valueGenerator);
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Throws(System.Exception exception)
                {
                    AddGetterReturnValue(() => throw exception);
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Throws<TException>()
                {
                    AddGetterReturnValue(() => throw new TException());
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.GetterCallback(System.Action callback)
                {
                    _getterCallbacks.Enqueue(callback);
                    return this;
                }

                void INamePropertyGetterBuilder.GetterCalled(Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_getterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, _getterInvocationCount);
                }

                internal int Get()
                {
                    System.Threading.Interlocked.Increment(ref _getterInvocationCount);
                    foreach (var getterCallback in _getterCallbacks)
                    {
                        getterCallback();
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        return _defaultPropertyBehaviour.BackingField;
                    if (_getterReturnValues.TryDequeue(out var returnValue))
                        _lastGetterReturnValue = returnValue;
                    return _lastGetterReturnValue();
                }
            }

            INamePropertyGetterBuilder INamePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }
        }

        public ILastNamePropertyBuilder LastName => _LastName;

        private readonly LastNamePropertyBuilder _LastName = new LastNamePropertyBuilder();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ILastNamePropertySetterBuilder
        {
            ILastNamePropertySetterBuilder Callback(System.Action<int> callback);
            void Called(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ILastNamePropertyBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class LastNamePropertyBuilder : ILastNamePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly SetterImposter _setterImposter;
            private readonly GetterImposterBuilder _getterImposterBuilder;
            LastNamePropertyBuilder()
            {
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour);
                _getterImposterBuilder = new GetterImposterBuilder(_getterImposterBuilder);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class DefaultPropertyBehaviour
            {
                private readonly bool IsOn = true;
                private readonly int BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _setterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _setterInvocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                }

                internal void Callback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
                {
                    _setterCallbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                    return this;
                }

                void Called(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count)
                {
                    var setterInvocationCount = _setterInvocationHistory.Count(criteria.Matches);
                    if (!count.Matches(setterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, setterInvocationCount);
                }

                internal void Set(int value)
                {
                    _setterInvocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _setterCallbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField = value;
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                class Builder : ILastNamePropertySetterBuilder
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
                }
            }

            ILastNamePropertySetterBuilder ILastNamePropertyBuilder.Setter(Imposter.Abstractions.Arg<int> criteria)
            {
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IPropertySetupSut
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