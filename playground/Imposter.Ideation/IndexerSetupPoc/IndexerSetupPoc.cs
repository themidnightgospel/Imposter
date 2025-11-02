/*using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.IndexerSetupPoc
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertySetupPocSutV2Imposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetupPoc.IPropertySetupPocV2Sut>
    {
        private readonly AgePropertyImposterBuilder _Age = new AgePropertyImposterBuilder();
        public IAgePropertyImposterBuilder Age => _Age;

        private ImposterTargetInstance _imposterInstance;

        public IPropertySetupPocSutV2Imposter()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut
        {
            IPropertySetupPocSutV2Imposter _imposter;

            public ImposterTargetInstance(IPropertySetupPocSutV2Imposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Age
            {
                get { return _imposter._Age._getterImposterBuilder.Get(); }

                set { _imposter._Age._setterImposter.Set(value); }
            }
        }

        global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut>.Instance()
        {
            return _imposterInstance;
        }
        
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IndexerArguments
        {
            public int value1;
            public string value2;
            public object value3;
            internal IndexerArguments(int value1, string value2, object value3)
            {
                this.value1 = value1;
                this.value2 = value2;
                this.value3 = value3;
            }
        }
        
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IndexerArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> value1 { get; }
            public Imposter.Abstractions.Arg<string> value2 { get; }
            public Imposter.Abstractions.Arg<object> value3 { get; }

            public IndexerArgumentsCriteria(Imposter.Abstractions.Arg<int> value1, Imposter.Abstractions.Arg<string> value2, Imposter.Abstractions.Arg<object> value3)
            {
                this.value1 = value1;
                this.value2 = value2;
                this.value3 = value3;
            }

            public bool Matches(IndexerArguments arguments)
            {
                return value1.Matches(arguments.value1) && value2.Matches(arguments.value2) && value3.Matches(arguments.value3);
            }
        }
        
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public delegate int IndexerDelegate(int value1, string value2, object value3);
        
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public delegate void IndexerCallbackDelegate(int value1, string value2, object value3);
        
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public delegate System.Exception IndexerExceptionGeneratorDelegate(int value1, string value2, object value3);

        public interface IAgePropertyGetterImposterBuilder
        {
            IAgePropertyGetterImposterBuilder Returns(int value);

            IAgePropertyGetterImposterBuilder Returns(IndexerDelegate valueGenerator);

            IAgePropertyGetterImposterBuilder Throws(System.Exception exception);

            IAgePropertyGetterImposterBuilder Throws<TException>()
                where TException : Exception, new();
            
            IAgePropertyGetterImposterBuilder Throws(IndexerExceptionGeneratorDelegate exception);

            IAgePropertyGetterImposterBuilder Callback(IndexerCallbackDelegate callback);

            void Called(Imposter.Abstractions.Count count);
        }

        public interface IAgePropertySetterImposterBuilder
        {
            IAgePropertySetterImposterBuilder Callback(IndexerCallbackDelegate callback);

            void Called(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgePropertyImposterBuilder
        {
            IAgePropertyGetterImposterBuilder Getter(Imposter.Abstractions.Arg<int> value1, Imposter.Abstractions.Arg<string> value2, Imposter.Abstractions.Arg<object> value3);

            IAgePropertySetterImposterBuilder Setter(Imposter.Abstractions.Arg<int> value1, Imposter.Abstractions.Arg<string> value2, Imposter.Abstractions.Arg<object> value3);
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

                internal Dictionary<IndexerArguments, int> BackingField = new Dictionary<IndexerArguments, int>();
            }
            
            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class IndexerMethodInvocationHistory
            {
                internal IndexerArguments Arguments;
                internal int Result;
                internal System.Exception Exception;
                public IndexerMethodInvocationHistory(IndexerArguments Arguments, int Result, System.Exception Exception)
                {
                    this.Arguments = Arguments;
                    this.Result = Result;
                    this.Exception = Exception;
                }

                public bool Matches(IndexerArgumentsCriteria criteria)
                {
                    return criteria.Matches(Arguments);
                }
            }

            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<IndexerArgumentsCriteria, IndexerCallbackDelegate>> _setterCallbacks
                    = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<IndexerArgumentsCriteria, IndexerCallbackDelegate>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerArguments> _setterInvocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerArguments>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;

                public SetterImposter(DefaultPropertyBehaviour defaultPropertyBehaviour)
                {
                    _defaultPropertyBehaviour = defaultPropertyBehaviour;
                }

                internal void Callback(IndexerArgumentsCriteria criteria, IndexerCallbackDelegate callback)
                {
                    _setterCallbacks.Enqueue(new System.Tuple<IndexerArgumentsCriteria, IndexerCallbackDelegate>(criteria, callback));
                }

                internal void Called(IndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    var setterInvocationCount = _setterInvocationHistory.Count(criteria.Matches);
                    if (!count.Matches(setterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, setterInvocationCount);
                }

                // Name collision
                internal void Set(int value1, string value2, object value3, int value)
                {
                    var arguments = new IndexerArguments(value1, value2, value3);
                    _setterInvocationHistory.Add(arguments);
                    foreach (var (criteria, setterCallback)in _setterCallbacks)
                    {
                        if (criteria.Matches(arguments))
                            setterCallback(value1, value2, value3);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField[arguments] = value;
                }

                internal class Builder : IAgePropertySetterImposterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly IndexerArgumentsCriteria _criteria;

                    public Builder(SetterImposter setterImposter, IndexerArgumentsCriteria criteria)
                    {
                        _setterImposter = setterImposter;
                        _criteria = criteria;
                    }

                    public IAgePropertySetterImposterBuilder Callback(IndexerCallbackDelegate callback)
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

            internal class GetterImposter : IAgePropertyGetterImposterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _getterReturnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _getterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<int> _lastGetterReturnValue = () => default;
                private readonly System.Collections.Concurrent.ConcurrentStack<IndexerMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IndexerMethodInvocationHistory>();

                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;

                public GetterImposter(DefaultPropertyBehaviour defaultPropertyBehaviour)
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

                public IAgePropertyGetterImposterBuilder Returns(IndexerDelegate valueGenerator)
                {
                    throw new NotImplementedException();
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

                public IAgePropertyGetterImposterBuilder Throws(IndexerExceptionGeneratorDelegate exception)
                {
                    throw new NotImplementedException();
                }

                public IAgePropertyGetterImposterBuilder Callback(IndexerCallbackDelegate callback)
                {
                    throw new NotImplementedException();
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

                internal int Get(int value1, string value2, object value3)
                {
                    System.Threading.Interlocked.Increment(ref _getterInvocationCount);
                    try
                    {
                        foreach (var getterCallback in _getterCallbacks)
                        {
                            getterCallback();
                        }

                        if (_defaultPropertyBehaviour.IsOn)
                            return _defaultPropertyBehaviour.BackingField;

                        if (_getterReturnValues.TryDequeue(out var returnValue))
                            _lastGetterReturnValue = returnValue;

                        var result = _lastGetterReturnValue();
                        
                        _invocationHistory.Push(new IndexerMethodInvocationHistory(new IndexerArguments(value1, value2, value3), result, null));
                    }
                    catch (Exception ex)
                    {
                        _invocationHistory.Push(new IndexerMethodInvocationHistory(new IndexerArguments(value1, value2, value3), null, ex));
                    }
                }

                internal class Builder : IAgePropertyGetterImposterBuilder
                {
                    private readonly GetterImposter _getterImposter;
                    private readonly IndexerArgumentsCriteria _criteria;

                    public Builder(GetterImposter getterImposter, IndexerArgumentsCriteria criteria)
                    {
                        _getterImposter = getterImposter;
                        _criteria = criteria;
                    }

                    public IAgePropertyGetterImposterBuilder Returns(int value)
                    {
                        throw new NotImplementedException();
                    }

                    public IAgePropertyGetterImposterBuilder Returns(IndexerDelegate valueGenerator)
                    {
                        throw new NotImplementedException();
                    }

                    public IAgePropertyGetterImposterBuilder Throws(Exception exception)
                    {
                        throw new NotImplementedException();
                    }

                    public IAgePropertyGetterImposterBuilder Throws<TException>() where TException : Exception, new()
                    {
                        throw new NotImplementedException();
                    }

                    public IAgePropertyGetterImposterBuilder Throws(IndexerExceptionGeneratorDelegate exception)
                    {
                        throw new NotImplementedException();
                    }

                    public IAgePropertyGetterImposterBuilder Callback(IndexerCallbackDelegate callback)
                    {
                        throw new NotImplementedException();
                    }

                    public void Called(Count count)
                    {
                        throw new NotImplementedException();
                    }
                }
                
            }

            IAgePropertyGetterImposterBuilder IAgePropertyImposterBuilder.Getter(Imposter.Abstractions.Arg<int> value1, Imposter.Abstractions.Arg<string> value2, Imposter.Abstractions.Arg<object> value3)
            {
                return new GetterImposter.Builder(_getterImposterBuilder, new IndexerArgumentsCriteria(value1, value2, value3));
            }

            IAgePropertySetterImposterBuilder IAgePropertyImposterBuilder.Setter(Imposter.Abstractions.Arg<int> value1, Imposter.Abstractions.Arg<string> value2, Imposter.Abstractions.Arg<object> value3)
            {
                return new SetterImposter.Builder(_setterImposter, new IndexerArgumentsCriteria(value1, value2, value3));
            }
        }
    }
}

namespace Imposter.CodeGenerator.Tests.Features.IndexerSetupPoc
{
    public interface IPropertySetupPocV2Sut
    {
        int this[int value1, string value2, object value3] { get; set; }
    }
}*/