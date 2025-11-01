using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Features.PropertySetup;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.PropertySetup
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertySetupSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupSut>
    {
        private ImposterTargetInstance _imposterInstance;
        public IPropertySetupSutImposter()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupSut>.Instance()
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
            IAgePropertyGetterBuilder Callback(System.Action callback);
            void Called(Imposter.Abstractions.Count count);
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
        internal class AgePropertyBuilder : IAgePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal AgePropertyBuilder()
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour);
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour);
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
                internal GetterImposterBuilder(DefaultPropertyBehaviour _defaultPropertyBehaviour)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                }

                private void AddReturnValue(System.Func<int> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Returns(int value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                IAgePropertyGetterBuilder IAgePropertyGetterBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void IAgePropertyGetterBuilder.Called(Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                internal int Get()
                {
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
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
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
                    _invocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _callbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField = value;
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

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupSut
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
        }
    }
}