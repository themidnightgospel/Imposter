using System;
using System.Linq;
using Imposter.Abstractions;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.PropertySetup
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertySetupPocV2SutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut>
    {
        private readonly AgePropertyImposterBuilder _Age = new AgePropertyImposterBuilder();
        public IAgePropertyImposterBuilder Age => _Age;

        private ImposterTargetInstance _imposterInstance;
        public IPropertySetupPocV2SutImposter()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut
        {
            IPropertySetupPocV2SutImposter _imposter;
            public ImposterTargetInstance(IPropertySetupPocV2SutImposter _imposter)
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

        global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut>.Instance()
        {
            return _imposterInstance;
        }

        public interface IAgePropertyGetterImposterBuilder
        {
            IAgePropertyGetterImposterBuilder Returns(int value);
            
            IAgePropertyGetterImposterBuilder Returns(System.Func<int> valueGenerator);
            
            IAgePropertyGetterImposterBuilder Throws(System.Exception exception);
            
            IAgePropertyGetterImposterBuilder Throws<TException>()
                where TException : Exception, new();
            
            IAgePropertyGetterImposterBuilder Callback(System.Action callback);
            
            void Called(Imposter.Abstractions.Count count);
        }

        public interface IAgePropertySetterImposterBuilder
        {
            IAgePropertySetterImposterBuilder Callback(System.Action<int> callback);
            
            void Called(Imposter.Abstractions.Count count);
        }
        
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyImposterBuilder
        {
            IAgePropertyGetterImposterBuilder Getter();

            IAgePropertySetterImposterBuilder Setter(Imposter.Abstractions.Arg<int> _criteria);
        }


        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AgePropertyImposterBuilder : IAgePropertyImposterBuilder
        {
            internal readonly DefaultPropertyBehaviour _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
            internal readonly SetterImposter _setterImposter;
            internal readonly GetterImposterBuilder _getterImposterBuilder;

            public AgePropertyImposterBuilder()
            {
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour);
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour);
            }

            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                
                internal int BackingField = default;
            }

            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _setterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<int> _setterInvocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;

                public SetterImposter(DefaultPropertyBehaviour defaultPropertyBehaviour)
                {
                    _defaultPropertyBehaviour = defaultPropertyBehaviour;
                }

                internal void Callback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
                {
                    _setterCallbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                }

                internal void Called(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count)
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

                internal class Builder : IAgePropertySetterImposterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly Imposter.Abstractions.Arg<int> _criteria;

                    public Builder(SetterImposter setterImposter, Arg<int> criteria)
                    {
                        _setterImposter = setterImposter;
                        _criteria = criteria;
                    }

                    public IAgePropertySetterImposterBuilder Callback(Action<int> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    public void Called(Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }
                }
            }

            internal class GetterImposterBuilder : IAgePropertyGetterImposterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _getterReturnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _getterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastGetterReturnValue = () => default;
                private int _getterInvocationCount;
                
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;

                public GetterImposterBuilder(DefaultPropertyBehaviour defaultPropertyBehaviour)
                {
                    _defaultPropertyBehaviour = defaultPropertyBehaviour;
                }

                private void AddGetterReturnValue(System.Func<int> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _getterReturnValues.Enqueue(valueGenerator);
                }
                
                IAgePropertyGetterImposterBuilder IAgePropertyGetterImposterBuilder.Returns(int value)
                {
                    AddGetterReturnValue(() => value);
                    return this;
                }

                IAgePropertyGetterImposterBuilder IAgePropertyGetterImposterBuilder.Returns(System.Func<int> valueGenerator)
                {
                    AddGetterReturnValue(valueGenerator);
                    return this;
                }

                IAgePropertyGetterImposterBuilder IAgePropertyGetterImposterBuilder.Throws(System.Exception exception)
                {
                    AddGetterReturnValue(() => throw exception);
                    return this;
                }

                IAgePropertyGetterImposterBuilder IAgePropertyGetterImposterBuilder.Throws<TException>()
                {
                    AddGetterReturnValue(() => throw new TException());
                    return this;
                }

                IAgePropertyGetterImposterBuilder IAgePropertyGetterImposterBuilder.Callback(System.Action callback)
                {
                    _getterCallbacks.Enqueue(callback);
                    return this;
                }

                void IAgePropertyGetterImposterBuilder.Called(Imposter.Abstractions.Count count)
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

            IAgePropertyGetterImposterBuilder IAgePropertyImposterBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            IAgePropertySetterImposterBuilder IAgePropertyImposterBuilder.Setter(Imposter.Abstractions.Arg<int> criteria)
            {
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }
    }
}

namespace Imposter.CodeGenerator.Tests.Features.PropertySetup
{
    public interface IPropertySetupPocV2Sut
    {
        int Age { get; set; }
    }
}
