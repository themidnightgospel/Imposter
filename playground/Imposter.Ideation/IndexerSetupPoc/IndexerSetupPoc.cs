using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Imposter.Abstractions;

#pragma warning disable nullable
namespace Imposter.Ideation.IndexerSetupPoc
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IIndexerSetupPocSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Ideation.IndexerSetupPoc.IIndexerSetupPocSut>
    {
        private readonly TripleParamIndexerImposterBuilder _TripleParamIndexer = new TripleParamIndexerImposterBuilder();
        public ITripleParamIndexerImposterBuilder TripleParamIndexer => _TripleParamIndexer;

        private ImposterTargetInstance _imposterInstance;
        public IIndexerSetupPocSutImposter()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Ideation.IndexerSetupPoc.IIndexerSetupPocSut
        {
            IIndexerSetupPocSutImposter _imposter;
            public ImposterTargetInstance(IIndexerSetupPocSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public string this[int index1, string index2, object index3]
            {
                get
                {
                    return _imposter._TripleParamIndexer._getterImposterBuilder.Get(index1, index2, index3);
                }
                set
                {
                    _imposter._TripleParamIndexer._setterImposter.Set(index1, index2, index3, value);
                }
            }
        }

        global::Imposter.Ideation.IndexerSetupPoc.IIndexerSetupPocSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Ideation.IndexerSetupPoc.IIndexerSetupPocSut>.Instance()
        {
            return _imposterInstance;
        }

        // Triple Parameter Indexer Interfaces
        public interface ITripleParamIndexerGetterImposterBuilder
        {
            ITripleParamIndexerGetterImposterBuilder Returns(string value);
            ITripleParamIndexerGetterImposterBuilder Returns(System.Func<int, string, object, string> valueGenerator);
            ITripleParamIndexerGetterImposterBuilder Throws(System.Exception exception);
            ITripleParamIndexerGetterImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ITripleParamIndexerGetterImposterBuilder Callback(System.Action<int, string, object> callback);
            void Called(Imposter.Abstractions.Count count);
        }

        public interface ITripleParamIndexerSetterImposterBuilder
        {
            ITripleParamIndexerSetterImposterBuilder Callback(System.Action<int, string, object, string> callback);
            void Called(Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ITripleParamIndexerImposterBuilder
        {
            ITripleParamIndexerGetterImposterBuilder Getter(
                Imposter.Abstractions.Arg<int> index1Criteria,
                Imposter.Abstractions.Arg<string> index2Criteria,
                Imposter.Abstractions.Arg<object> index3Criteria);
            
            ITripleParamIndexerSetterImposterBuilder Setter(
                Imposter.Abstractions.Arg<int> index1Criteria,
                Imposter.Abstractions.Arg<string> index2Criteria,
                Imposter.Abstractions.Arg<object> index3Criteria,
                Imposter.Abstractions.Arg<string> valueCriteria);
        }

        // Triple Parameter Indexer Implementation
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class TripleParamIndexerImposterBuilder : ITripleParamIndexerImposterBuilder
        {
            internal readonly DefaultIndexerBehaviour _defaultIndexerBehaviour = new DefaultIndexerBehaviour();
            internal readonly SetterImposter _setterImposter;
            internal readonly GetterImposterBuilder _getterImposterBuilder;

            public TripleParamIndexerImposterBuilder()
            {
                _setterImposter = new SetterImposter(_defaultIndexerBehaviour);
                _getterImposterBuilder = new GetterImposterBuilder(_defaultIndexerBehaviour);
            }

            internal class DefaultIndexerBehaviour
            {
                internal bool IsOn = true;
                internal Dictionary<(int, string, object), string> BackingStore = new Dictionary<(int, string, object), string>();
            }

            internal class SetterImposter
            {
                private readonly ConcurrentQueue<SetterCallbackEntry> _setterCallbacks = new ConcurrentQueue<SetterCallbackEntry>();
                private readonly ConcurrentBag<(int, string, object, string)> _setterInvocationHistory = new ConcurrentBag<(int, string, object, string)>();
                private readonly DefaultIndexerBehaviour _defaultIndexerBehaviour;

                public SetterImposter(DefaultIndexerBehaviour defaultIndexerBehaviour)
                {
                    _defaultIndexerBehaviour = defaultIndexerBehaviour;
                }

                internal void Callback(
                    Imposter.Abstractions.Arg<int> index1Criteria,
                    Imposter.Abstractions.Arg<string> index2Criteria,
                    Imposter.Abstractions.Arg<object> index3Criteria,
                    Imposter.Abstractions.Arg<string> valueCriteria,
                    System.Action<int, string, object, string> callback)
                {
                    _setterCallbacks.Enqueue(new SetterCallbackEntry(index1Criteria, index2Criteria, index3Criteria, valueCriteria, callback));
                }

                internal void Called(
                    Imposter.Abstractions.Arg<int> index1Criteria,
                    Imposter.Abstractions.Arg<string> index2Criteria,
                    Imposter.Abstractions.Arg<object> index3Criteria,
                    Imposter.Abstractions.Arg<string> valueCriteria,
                    Imposter.Abstractions.Count count)
                {
                    var setterInvocationCount = _setterInvocationHistory.Count(invocation =>
                        index1Criteria.Matches(invocation.Item1) &&
                        index2Criteria.Matches(invocation.Item2) &&
                        index3Criteria.Matches(invocation.Item3) &&
                        valueCriteria.Matches(invocation.Item4));
                    
                    if (!count.Matches(setterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, setterInvocationCount);
                }

                internal void Set(int index1, string index2, object index3, string value)
                {
                    _setterInvocationHistory.Add((index1, index2, index3, value));
                    
                    foreach (var callbackEntry in _setterCallbacks)
                    {
                        if (callbackEntry.Index1Criteria.Matches(index1) &&
                            callbackEntry.Index2Criteria.Matches(index2) &&
                            callbackEntry.Index3Criteria.Matches(index3) &&
                            callbackEntry.ValueCriteria.Matches(value))
                        {
                            callbackEntry.Callback(index1, index2, index3, value);
                        }
                    }

                    if (_defaultIndexerBehaviour.IsOn)
                        _defaultIndexerBehaviour.BackingStore[(index1, index2, index3)] = value;
                }

                private struct SetterCallbackEntry
                {
                    public readonly Imposter.Abstractions.Arg<int> Index1Criteria;
                    public readonly Imposter.Abstractions.Arg<string> Index2Criteria;
                    public readonly Imposter.Abstractions.Arg<object> Index3Criteria;
                    public readonly Imposter.Abstractions.Arg<string> ValueCriteria;
                    public readonly System.Action<int, string, object, string> Callback;

                    public SetterCallbackEntry(
                        Imposter.Abstractions.Arg<int> index1Criteria,
                        Imposter.Abstractions.Arg<string> index2Criteria,
                        Imposter.Abstractions.Arg<object> index3Criteria,
                        Imposter.Abstractions.Arg<string> valueCriteria,
                        System.Action<int, string, object, string> callback)
                    {
                        Index1Criteria = index1Criteria;
                        Index2Criteria = index2Criteria;
                        Index3Criteria = index3Criteria;
                        ValueCriteria = valueCriteria;
                        Callback = callback;
                    }
                }

                internal class Builder : ITripleParamIndexerSetterImposterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly Imposter.Abstractions.Arg<int> _index1Criteria;
                    private readonly Imposter.Abstractions.Arg<string> _index2Criteria;
                    private readonly Imposter.Abstractions.Arg<object> _index3Criteria;
                    private readonly Imposter.Abstractions.Arg<string> _valueCriteria;

                    public Builder(
                        SetterImposter setterImposter,
                        Arg<int> index1Criteria,
                        Arg<string> index2Criteria,
                        Arg<object> index3Criteria,
                        Arg<string> valueCriteria)
                    {
                        _setterImposter = setterImposter;
                        _index1Criteria = index1Criteria;
                        _index2Criteria = index2Criteria;
                        _index3Criteria = index3Criteria;
                        _valueCriteria = valueCriteria;
                    }

                    public ITripleParamIndexerSetterImposterBuilder Callback(Action<int, string, object, string> callback)
                    {
                        _setterImposter.Callback(_index1Criteria, _index2Criteria, _index3Criteria, _valueCriteria, callback);
                        return this;
                    }

                    public void Called(Count count)
                    {
                        _setterImposter.Called(_index1Criteria, _index2Criteria, _index3Criteria, _valueCriteria, count);
                    }
                }
            }

            internal class GetterImposterBuilder : ITripleParamIndexerGetterImposterBuilder
            {
                private readonly ConcurrentQueue<GetterReturnEntry> _getterReturnValues = new ConcurrentQueue<GetterReturnEntry>();
                private readonly ConcurrentQueue<GetterCallbackEntry> _getterCallbacks = new ConcurrentQueue<GetterCallbackEntry>();
                private System.Func<int, string, object, string> _lastGetterReturnValue = (i1, i2, i3) => default;
                private readonly ConcurrentBag<(int, string, object)> _getterInvocationHistory = new ConcurrentBag<(int, string, object)>();

                private readonly DefaultIndexerBehaviour _defaultIndexerBehaviour;

                public GetterImposterBuilder(DefaultIndexerBehaviour defaultIndexerBehaviour)
                {
                    _defaultIndexerBehaviour = defaultIndexerBehaviour;
                }

                internal void AddGetterReturnValue(
                    Imposter.Abstractions.Arg<int> index1Criteria,
                    Imposter.Abstractions.Arg<string> index2Criteria,
                    Imposter.Abstractions.Arg<object> index3Criteria,
                    System.Func<int, string, object, string> valueGenerator)
                {
                    _defaultIndexerBehaviour.IsOn = false;
                    _getterReturnValues.Enqueue(new GetterReturnEntry(index1Criteria, index2Criteria, index3Criteria, valueGenerator));
                }

                ITripleParamIndexerGetterImposterBuilder ITripleParamIndexerGetterImposterBuilder.Returns(string value)
                {
                    AddGetterReturnValue(Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any(), (i1, i2, i3) => value);
                    return this;
                }

                ITripleParamIndexerGetterImposterBuilder ITripleParamIndexerGetterImposterBuilder.Returns(System.Func<int, string, object, string> valueGenerator)
                {
                    AddGetterReturnValue(Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any(), valueGenerator);
                    return this;
                }

                ITripleParamIndexerGetterImposterBuilder ITripleParamIndexerGetterImposterBuilder.Throws(System.Exception exception)
                {
                    AddGetterReturnValue(Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any(), (i1, i2, i3) => throw exception);
                    return this;
                }

                ITripleParamIndexerGetterImposterBuilder ITripleParamIndexerGetterImposterBuilder.Throws<TException>()
                {
                    AddGetterReturnValue(Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any(), (i1, i2, i3) => throw new TException());
                    return this;
                }

                ITripleParamIndexerGetterImposterBuilder ITripleParamIndexerGetterImposterBuilder.Callback(System.Action<int, string, object> callback)
                {
                    _getterCallbacks.Enqueue(new GetterCallbackEntry(Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any(), callback));
                    return this;
                }

                void ITripleParamIndexerGetterImposterBuilder.Called(Imposter.Abstractions.Count count)
                {
                    var getterInvocationCount = _getterInvocationHistory.Count;
                    if (!count.Matches(getterInvocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, getterInvocationCount);
                }

                internal string Get(int index1, string index2, object index3)
                {
                    _getterInvocationHistory.Add((index1, index2, index3));

                    foreach (var callbackEntry in _getterCallbacks)
                    {
                        if (callbackEntry.Index1Criteria.Matches(index1) &&
                            callbackEntry.Index2Criteria.Matches(index2) &&
                            callbackEntry.Index3Criteria.Matches(index3))
                        {
                            callbackEntry.Callback(index1, index2, index3);
                        }
                    }

                    if (_defaultIndexerBehaviour.IsOn)
                    {
                        return _defaultIndexerBehaviour.BackingStore.TryGetValue((index1, index2, index3), out var value) ? value : default;
                    }

                    foreach (var returnEntry in _getterReturnValues)
                    {
                        if (returnEntry.Index1Criteria.Matches(index1) &&
                            returnEntry.Index2Criteria.Matches(index2) &&
                            returnEntry.Index3Criteria.Matches(index3))
                        {
                            _lastGetterReturnValue = returnEntry.ValueGenerator;
                            break;
                        }
                    }

                    return _lastGetterReturnValue(index1, index2, index3);
                }

                private struct GetterReturnEntry
                {
                    public readonly Imposter.Abstractions.Arg<int> Index1Criteria;
                    public readonly Imposter.Abstractions.Arg<string> Index2Criteria;
                    public readonly Imposter.Abstractions.Arg<object> Index3Criteria;
                    public readonly System.Func<int, string, object, string> ValueGenerator;

                    public GetterReturnEntry(
                        Imposter.Abstractions.Arg<int> index1Criteria,
                        Imposter.Abstractions.Arg<string> index2Criteria,
                        Imposter.Abstractions.Arg<object> index3Criteria,
                        System.Func<int, string, object, string> valueGenerator)
                    {
                        Index1Criteria = index1Criteria;
                        Index2Criteria = index2Criteria;
                        Index3Criteria = index3Criteria;
                        ValueGenerator = valueGenerator;
                    }
                }

                private struct GetterCallbackEntry
                {
                    public readonly Imposter.Abstractions.Arg<int> Index1Criteria;
                    public readonly Imposter.Abstractions.Arg<string> Index2Criteria;
                    public readonly Imposter.Abstractions.Arg<object> Index3Criteria;
                    public readonly System.Action<int, string, object> Callback;

                    public GetterCallbackEntry(
                        Imposter.Abstractions.Arg<int> index1Criteria,
                        Imposter.Abstractions.Arg<string> index2Criteria,
                        Imposter.Abstractions.Arg<object> index3Criteria,
                        System.Action<int, string, object> callback)
                    {
                        Index1Criteria = index1Criteria;
                        Index2Criteria = index2Criteria;
                        Index3Criteria = index3Criteria;
                        Callback = callback;
                    }
                }
            }

            ITripleParamIndexerGetterImposterBuilder ITripleParamIndexerImposterBuilder.Getter(
                Imposter.Abstractions.Arg<int> index1Criteria,
                Imposter.Abstractions.Arg<string> index2Criteria,
                Imposter.Abstractions.Arg<object> index3Criteria)
            {
                return new GetterBuilder(_getterImposterBuilder, index1Criteria, index2Criteria, index3Criteria);
            }

            ITripleParamIndexerSetterImposterBuilder ITripleParamIndexerImposterBuilder.Setter(
                Imposter.Abstractions.Arg<int> index1Criteria,
                Imposter.Abstractions.Arg<string> index2Criteria,
                Imposter.Abstractions.Arg<object> index3Criteria,
                Imposter.Abstractions.Arg<string> valueCriteria)
            {
                return new SetterImposter.Builder(_setterImposter, index1Criteria, index2Criteria, index3Criteria, valueCriteria);
            }

            internal class GetterBuilder : ITripleParamIndexerGetterImposterBuilder
            {
                private readonly GetterImposterBuilder _getterImposterBuilder;
                private readonly Imposter.Abstractions.Arg<int> _index1Criteria;
                private readonly Imposter.Abstractions.Arg<string> _index2Criteria;
                private readonly Imposter.Abstractions.Arg<object> _index3Criteria;

                public GetterBuilder(
                    GetterImposterBuilder getterImposterBuilder,
                    Arg<int> index1Criteria,
                    Arg<string> index2Criteria,
                    Arg<object> index3Criteria)
                {
                    _getterImposterBuilder = getterImposterBuilder;
                    _index1Criteria = index1Criteria;
                    _index2Criteria = index2Criteria;
                    _index3Criteria = index3Criteria;
                }

                public ITripleParamIndexerGetterImposterBuilder Returns(string value)
                {
                    _getterImposterBuilder.AddGetterReturnValue(_index1Criteria, _index2Criteria, _index3Criteria, (i1, i2, i3) => value);
                    return this;
                }

                public ITripleParamIndexerGetterImposterBuilder Returns(Func<int, string, object, string> valueGenerator)
                {
                    _getterImposterBuilder.AddGetterReturnValue(_index1Criteria, _index2Criteria, _index3Criteria, valueGenerator);
                    return this;
                }

                public ITripleParamIndexerGetterImposterBuilder Throws(Exception exception)
                {
                    _getterImposterBuilder.AddGetterReturnValue(_index1Criteria, _index2Criteria, _index3Criteria, (i1, i2, i3) => throw exception);
                    return this;
                }

                public ITripleParamIndexerGetterImposterBuilder Throws<TException>() where TException : Exception, new()
                {
                    _getterImposterBuilder.AddGetterReturnValue(_index1Criteria, _index2Criteria, _index3Criteria, (i1, i2, i3) => throw new TException());
                    return this;
                }

                public ITripleParamIndexerGetterImposterBuilder Callback(Action<int, string, object> callback)
                {
                    _getterImposterBuilder._getterCallbacks.Enqueue(new GetterImposterBuilder.GetterCallbackEntry(_index1Criteria, _index2Criteria, _index3Criteria, callback));
                    return this;
                }

                public void Called(Count count)
                {
                    var getterInvocationCount = _getterImposterBuilder._getterInvocationHistory.Count(invocation =>
                        _index1Criteria.Matches(invocation.Item1) &&
                        _index2Criteria.Matches(invocation.Item2) &&
                        _index3Criteria.Matches(invocation.Item3));
                    
                    if (!count.Matches(getterInvocationCount))
                        throw new VerificationFailedException(count, getterInvocationCount);
                }
            }
        }
    }
}

namespace Imposter.Ideation.IndexerSetupPoc
{
    public interface IIndexerSetupPocSut
    {
        string this[int index1, string index2, object index3] { get; set; }
    }
}