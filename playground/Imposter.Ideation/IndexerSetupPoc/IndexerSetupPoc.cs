using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;

namespace Imposter.Ideation.IndexerSetupPoc
{
    public delegate int IndexerDelegate(int value1, string value2, object? value3);
    public delegate Exception IndexerExceptionGeneratorDelegate(
        int value1,
        string value2,
        object? value3
    );
    public delegate void IndexerGetterCallbackDelegate(int value1, string value2, object? value3);
    public delegate void IndexerSetterCallbackDelegate(
        int value1,
        string value2,
        object? value3,
        int value
    );

    public interface IIndexerSetupPocV2Sut
    {
        int this[int value1, string value2, object value3] { get; set; }
    }

    public interface IIndexerImposterBuilder
    {
        IIndexerGetterImposterBuilder Getter();

        IIndexerSetterImposterBuilder Setter();
    }

    public interface IIndexerGetterImposterBuilder
    {
        IIndexerGetterImposterBuilder Returns(int value);

        IIndexerGetterImposterBuilder Returns(Func<int> valueGenerator);

        IIndexerGetterImposterBuilder Returns(IndexerDelegate valueGenerator);

        IIndexerGetterImposterBuilder Throws(Exception exception);

        IIndexerGetterImposterBuilder Throws<TException>()
            where TException : Exception, new();

        IIndexerGetterImposterBuilder Throws(IndexerExceptionGeneratorDelegate exceptionGenerator);

        IIndexerGetterImposterBuilder Callback(IndexerGetterCallbackDelegate callback);

        void Called(Count count);

        IIndexerGetterImposterBuilder Then();
    }

    public interface IIndexerSetterImposterBuilder
    {
        IIndexerSetterImposterBuilder Callback(IndexerSetterCallbackDelegate callback);

        void Called(Count count);

        IIndexerSetterImposterBuilder Then();
    }

    public sealed class IndexerSetupPocImposter : IHaveImposterInstance<IIndexerSetupPocV2Sut>
    {
        private const string IndexerSignature =
            "Imposter.CodeGenerator.Tests.Features.IndexerSetupPoc.IIndexerSetupPocV2Sut.this[int value1, string value2, object value3]";

        private readonly IndexerImposterBuilder _indexer;
        private readonly IIndexerSetupPocV2Sut _instance;

        public IndexerSetupPocImposter(ImposterMode mode = ImposterMode.Implicit)
        {
            _indexer = new IndexerImposterBuilder(mode, IndexerSignature);
            _instance = new ImposterTargetInstance(this);
        }

        public IIndexerImposterBuilder this[
            Arg<int> value1,
            Arg<string> value2,
            Arg<object?> value3
        ] => new IndexerInvocationBuilder(_indexer, value1, value2, value3);

        private sealed class IndexerInvocationBuilder : IIndexerImposterBuilder
        {
            private readonly IndexerImposterBuilder _builder;
            private readonly IndexerArgumentsCriteria _criteria;

            internal IndexerInvocationBuilder(
                IndexerImposterBuilder builder,
                Arg<int> value1,
                Arg<string> value2,
                Arg<object?> value3
            )
            {
                _builder = builder;
                _criteria = new IndexerArgumentsCriteria(value1, value2, value3);
            }

            public IIndexerGetterImposterBuilder Getter() => _builder.CreateGetter(_criteria);

            public IIndexerSetterImposterBuilder Setter() => _builder.CreateSetter(_criteria);
        }

        public IIndexerSetupPocV2Sut Instance() =>
            ((IHaveImposterInstance<IIndexerSetupPocV2Sut>)this).Instance();

        IIndexerSetupPocV2Sut IHaveImposterInstance<IIndexerSetupPocV2Sut>.Instance() => _instance;

        private sealed class ImposterTargetInstance : IIndexerSetupPocV2Sut
        {
            private readonly IndexerSetupPocImposter _owner;

            public ImposterTargetInstance(IndexerSetupPocImposter owner)
            {
                _owner = owner;
            }

            public int this[int value1, string value2, object value3]
            {
                get => _owner._indexer.Get(value1, value2, value3);
                set => _owner._indexer.Set(value1, value2, value3, value);
            }
        }

        private sealed class IndexerImposterBuilder
        {
            private readonly DefaultIndexerBehaviour _defaultBehaviour =
                new DefaultIndexerBehaviour();
            private readonly GetterImposter _getter;
            private readonly SetterImposter _setter;

            public IndexerImposterBuilder(ImposterMode mode, string propertyDisplayName)
            {
                _getter = new GetterImposter(_defaultBehaviour, mode, propertyDisplayName);
                _setter = new SetterImposter(_defaultBehaviour, mode, propertyDisplayName);
            }

            public IIndexerGetterImposterBuilder CreateGetter(IndexerArgumentsCriteria criteria) =>
                new GetterImposter.Builder(_getter, criteria);

            public IIndexerSetterImposterBuilder CreateSetter(IndexerArgumentsCriteria criteria)
            {
                _setter.MarkConfigured();
                return new SetterImposter.Builder(_setter, criteria);
            }

            internal int Get(int value1, string value2, object value3) =>
                _getter.Get(value1, value2, value3);

            internal void Set(int value1, string value2, object value3, int value) =>
                _setter.Set(value1, value2, value3, value);
        }

        private sealed class DefaultIndexerBehaviour
        {
            private readonly ConcurrentDictionary<IndexerArguments, int> _backingField =
                new ConcurrentDictionary<IndexerArguments, int>();
            private bool _isOn = true;

            internal bool IsOn
            {
                get => Volatile.Read(ref _isOn);
                set => Volatile.Write(ref _isOn, value);
            }

            internal void Set(IndexerArguments arguments, int value) =>
                _backingField[arguments] = value;

            internal int Get(IndexerArguments arguments) =>
                _backingField.TryGetValue(arguments, out var value) ? value : default;
        }

        private readonly struct IndexerArguments : IEquatable<IndexerArguments>
        {
            internal IndexerArguments(int value1, string value2, object? value3)
            {
                Value1 = value1;
                Value2 = value2;
                Value3 = value3;
            }

            internal int Value1 { get; }
            internal string Value2 { get; }
            internal object? Value3 { get; }

            public bool Equals(IndexerArguments other) =>
                Value1 == other.Value1
                && string.Equals(Value2, other.Value2, StringComparison.Ordinal)
                && Equals(Value3, other.Value3);

            public override bool Equals(object? obj) =>
                obj is IndexerArguments other && Equals(other);

            public override int GetHashCode()
            {
                var hash = new HashCode();
                hash.Add(Value1);
                hash.Add(Value2, StringComparer.Ordinal);
                hash.Add(Value3);
                return hash.ToHashCode();
            }
        }

        private sealed class IndexerArgumentsCriteria
        {
            private readonly Arg<int> _value1;
            private readonly Arg<string> _value2;
            private readonly Arg<object?> _value3;

            public IndexerArgumentsCriteria(
                Arg<int> value1,
                Arg<string> value2,
                Arg<object?> value3
            )
            {
                _value1 = value1;
                _value2 = value2;
                _value3 = value3;
            }

            public bool Matches(IndexerArguments arguments) =>
                _value1.Matches(arguments.Value1)
                && _value2.Matches(arguments.Value2)
                && _value3.Matches(arguments.Value3);
        }

        private sealed class GetterImposter
        {
            private readonly DefaultIndexerBehaviour _defaultBehaviour;
            private readonly ConcurrentStack<GetterInvocationImposter> _setups =
                new ConcurrentStack<GetterInvocationImposter>();
            private readonly ConcurrentDictionary<
                IndexerArgumentsCriteria,
                GetterInvocationImposter
            > _setupLookup =
                new ConcurrentDictionary<IndexerArgumentsCriteria, GetterInvocationImposter>();
            private readonly ConcurrentBag<IndexerArguments> _invocationHistory =
                new ConcurrentBag<IndexerArguments>();
            private readonly ImposterMode _mode;
            private readonly string _propertyDisplayName;
            private bool _hasConfiguredReturn;

            public GetterImposter(
                DefaultIndexerBehaviour defaultBehaviour,
                ImposterMode mode,
                string propertyDisplayName
            )
            {
                _defaultBehaviour = defaultBehaviour;
                _mode = mode;
                _propertyDisplayName = propertyDisplayName;
            }

            internal int Get(int value1, string value2, object value3)
            {
                var arguments = new IndexerArguments(value1, value2, value3);
                try
                {
                    var setup = FindMatchingSetup(arguments);
                    if (setup is null)
                    {
                        EnsureGetterConfigured();

                        if (_defaultBehaviour.IsOn)
                        {
                            return _defaultBehaviour.Get(arguments);
                        }

                        throw new MissingImposterException(_propertyDisplayName + " (getter)");
                    }

                    return setup.Invoke(arguments);
                }
                finally
                {
                    _invocationHistory.Add(arguments);
                }
            }

            private GetterInvocationImposter? FindMatchingSetup(IndexerArguments arguments)
            {
                foreach (var candidate in _setups)
                {
                    if (candidate.Criteria.Matches(arguments))
                    {
                        return candidate;
                    }
                }

                return null;
            }

            private GetterInvocationImposter GetOrCreate(IndexerArgumentsCriteria criteria)
            {
                return _setupLookup.GetOrAdd(criteria, CreateSetup);

                GetterInvocationImposter CreateSetup(IndexerArgumentsCriteria key)
                {
                    var setup = new GetterInvocationImposter(this, _defaultBehaviour, key);
                    _setups.Push(setup);
                    return setup;
                }
            }

            private void Called(IndexerArgumentsCriteria criteria, Count count)
            {
                var invocationCount = _invocationHistory.Count(criteria.Matches);

                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException(count, invocationCount);
                }
            }

            internal string PropertyDisplayName => _propertyDisplayName;

            internal void MarkReturnConfigured() => Volatile.Write(ref _hasConfiguredReturn, true);

            private void EnsureGetterConfigured()
            {
                if (_mode == ImposterMode.Explicit && !Volatile.Read(ref _hasConfiguredReturn))
                {
                    throw new MissingImposterException(_propertyDisplayName + " (getter)");
                }
            }

            internal sealed class Builder : IIndexerGetterImposterBuilder
            {
                private readonly GetterImposter _imposter;
                private readonly IndexerArgumentsCriteria _criteria;

                public Builder(GetterImposter imposter, IndexerArgumentsCriteria criteria)
                {
                    _imposter = imposter;
                    _criteria = criteria;
                }

                private GetterInvocationImposter InvocationImposter =>
                    _imposter.GetOrCreate(_criteria);

                public IIndexerGetterImposterBuilder Returns(int value)
                {
                    InvocationImposter.AddReturnValue(_ => value);
                    return this;
                }

                public IIndexerGetterImposterBuilder Returns(Func<int> valueGenerator)
                {
                    InvocationImposter.AddReturnValue(_ => valueGenerator());
                    return this;
                }

                public IIndexerGetterImposterBuilder Returns(IndexerDelegate valueGenerator)
                {
                    InvocationImposter.AddReturnValue(args =>
                        valueGenerator(args.Value1, args.Value2, args.Value3)
                    );
                    return this;
                }

                public IIndexerGetterImposterBuilder Throws(Exception exception)
                {
                    InvocationImposter.AddReturnValue(_ => throw exception);
                    return this;
                }

                public IIndexerGetterImposterBuilder Throws<TException>()
                    where TException : Exception, new()
                {
                    InvocationImposter.AddReturnValue(_ => throw new TException());
                    return this;
                }

                public IIndexerGetterImposterBuilder Throws(
                    IndexerExceptionGeneratorDelegate exceptionGenerator
                )
                {
                    InvocationImposter.AddReturnValue(args =>
                        throw exceptionGenerator(args.Value1, args.Value2, args.Value3)
                    );
                    return this;
                }

                public IIndexerGetterImposterBuilder Callback(
                    IndexerGetterCallbackDelegate callback
                )
                {
                    InvocationImposter.AddCallback(callback);
                    return this;
                }

                public void Called(Count count) => _imposter.Called(_criteria, count);

                public IIndexerGetterImposterBuilder Then() => this;
            }

            private sealed class GetterInvocationImposter
            {
                private readonly GetterImposter _parent;
                private readonly DefaultIndexerBehaviour _defaultBehaviour;
                private readonly ConcurrentQueue<Func<IndexerArguments, int>> _returnValues =
                    new ConcurrentQueue<Func<IndexerArguments, int>>();
                private readonly ConcurrentQueue<IndexerGetterCallbackDelegate> _callbacks =
                    new ConcurrentQueue<IndexerGetterCallbackDelegate>();
                private Func<IndexerArguments, int>? _lastReturnValue;
                private int _invocationCount;
                private readonly string _propertyDisplayName;

                public GetterInvocationImposter(
                    GetterImposter parent,
                    DefaultIndexerBehaviour defaultBehaviour,
                    IndexerArgumentsCriteria criteria
                )
                {
                    _parent = parent;
                    _defaultBehaviour = defaultBehaviour;
                    Criteria = criteria;
                    _propertyDisplayName = parent.PropertyDisplayName;
                }

                public IndexerArgumentsCriteria Criteria { get; }

                public int InvocationCount => Volatile.Read(ref _invocationCount);

                public void AddReturnValue(Func<IndexerArguments, int> generator)
                {
                    _defaultBehaviour.IsOn = false;
                    _returnValues.Enqueue(generator);
                    _parent.MarkReturnConfigured();
                }

                public void AddCallback(IndexerGetterCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                public int Invoke(IndexerArguments arguments)
                {
                    Interlocked.Increment(ref _invocationCount);

                    foreach (var callback in _callbacks)
                    {
                        callback(arguments.Value1, arguments.Value2, arguments.Value3);
                    }

                    var generator = ResolveNextGenerator();
                    return generator(arguments);
                }

                private Func<IndexerArguments, int> ResolveNextGenerator()
                {
                    if (_defaultBehaviour.IsOn)
                    {
                        return args => _defaultBehaviour.Get(args);
                    }

                    if (_returnValues.TryDequeue(out var generator))
                    {
                        _lastReturnValue = generator;
                    }

                    if (_lastReturnValue is null)
                    {
                        throw new MissingImposterException(_propertyDisplayName + " (getter)");
                    }

                    return _lastReturnValue;
                }
            }
        }

        private sealed class SetterImposter
        {
            private readonly ConcurrentQueue<(
                IndexerArgumentsCriteria Criteria,
                IndexerSetterCallbackDelegate Callback
            )> _callbacks =
                new ConcurrentQueue<(IndexerArgumentsCriteria, IndexerSetterCallbackDelegate)>();
            private readonly ConcurrentBag<IndexerArguments> _invocationHistory =
                new ConcurrentBag<IndexerArguments>();
            private readonly DefaultIndexerBehaviour _defaultBehaviour;
            private readonly ImposterMode _mode;
            private readonly string _propertyDisplayName;
            private bool _hasConfiguredSetter;

            public SetterImposter(
                DefaultIndexerBehaviour defaultBehaviour,
                ImposterMode mode,
                string propertyDisplayName
            )
            {
                _defaultBehaviour = defaultBehaviour;
                _mode = mode;
                _propertyDisplayName = propertyDisplayName;
            }

            public void Callback(
                IndexerArgumentsCriteria criteria,
                IndexerSetterCallbackDelegate callback
            )
            {
                _callbacks.Enqueue((criteria, callback));
            }

            public void Called(IndexerArgumentsCriteria criteria, Count count)
            {
                var invocationCount = _invocationHistory.Count(criteria.Matches);

                if (!count.Matches(invocationCount))
                {
                    throw new VerificationFailedException(count, invocationCount);
                }
            }

            public void Set(int value1, string value2, object value3, int value)
            {
                EnsureSetterConfigured();
                var arguments = new IndexerArguments(value1, value2, value3);
                _invocationHistory.Add(arguments);

                foreach (var (criteria, callback) in _callbacks)
                {
                    if (criteria.Matches(arguments))
                    {
                        callback(value1, value2, value3, value);
                    }
                }

                if (_defaultBehaviour.IsOn)
                {
                    _defaultBehaviour.Set(arguments, value);
                }
            }

            private void EnsureSetterConfigured()
            {
                if (_mode == ImposterMode.Explicit && !Volatile.Read(ref _hasConfiguredSetter))
                {
                    throw new MissingImposterException(_propertyDisplayName + " (setter)");
                }
            }

            internal void MarkConfigured()
            {
                Volatile.Write(ref _hasConfiguredSetter, true);
            }

            internal sealed class Builder : IIndexerSetterImposterBuilder
            {
                private readonly SetterImposter _setterImposter;
                private readonly IndexerArgumentsCriteria _criteria;

                public Builder(SetterImposter setterImposter, IndexerArgumentsCriteria criteria)
                {
                    _setterImposter = setterImposter;
                    _criteria = criteria;
                }

                public IIndexerSetterImposterBuilder Callback(
                    IndexerSetterCallbackDelegate callback
                )
                {
                    _setterImposter.Callback(_criteria, callback);
                    return this;
                }

                public void Called(Count count)
                {
                    _setterImposter.Called(_criteria, count);
                }

                public IIndexerSetterImposterBuilder Then() => this;
            }
        }
    }
}
