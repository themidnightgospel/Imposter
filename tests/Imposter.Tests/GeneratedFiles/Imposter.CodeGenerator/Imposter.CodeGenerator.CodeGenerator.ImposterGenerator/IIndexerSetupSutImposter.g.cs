using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Features.IndexerSetup;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.IndexerSetup
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IIndexerSetupSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut>
    {
        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut Instance() => _imposterInstance;
        global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut>.Instance()
        {
            return _imposterInstance;
        }

        private readonly IndexerIndexerBuilder _IndexerIndexer;
        public IIndexerIndexerBuilder this[Imposter.Abstractions.Arg<int> key1, Imposter.Abstractions.Arg<string> key2, Imposter.Abstractions.Arg<object> key3] => new IndexerIndexerBuilder.InvocationBuilder(_IndexerIndexer, new IndexerIndexerArgumentsCriteria(key1, key2, key3));
        public delegate int IndexerIndexerDelegate(int key1, string key2, object key3);
        public delegate void IndexerIndexerGetterCallback(int key1, string key2, object key3);
        public delegate void IndexerIndexerSetterCallback(int key1, string key2, object key3, int value);
        public delegate System.Exception IndexerIndexerExceptionGenerator(int key1, string key2, object key3);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArguments : global::System.IEquatable<IndexerIndexerArguments>
        {
            public int key1;
            public string key2;
            public object key3;
            internal IndexerIndexerArguments(int key1, string key2, object key3)
            {
                this.key1 = key1;
                this.key2 = key2;
                this.key3 = key3;
            }

            public bool Equals(IndexerIndexerArguments other)
            {
                return true && key1 == other.key1 && key2 == other.key2 && key3 == other.key3;
            }

            public override bool Equals(object obj)
            {
                return obj is IndexerIndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                global::System.HashCode hash = new global::System.HashCode();
                hash.Add(key1);
                hash.Add(key2);
                hash.Add(key3);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> key1;
            public Imposter.Abstractions.Arg<string> key2;
            public Imposter.Abstractions.Arg<object> key3;
            internal IndexerIndexerArgumentsCriteria(Imposter.Abstractions.Arg<int> key1, Imposter.Abstractions.Arg<string> key2, Imposter.Abstractions.Arg<object> key3)
            {
                this.key1 = key1;
                this.key2 = key2;
                this.key3 = key3;
            }

            public bool Matches(IndexerIndexerArguments arguments)
            {
                return key1.Matches(arguments.key1) && key2.Matches(arguments.key2) && key3.Matches(arguments.key3);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerBuilder
        {
            private readonly DefaultIndexerIndexerBehaviour _IndexerDefaultIndexerBehaviour = new DefaultIndexerIndexerBehaviour();
            private readonly GetterImposter _getterImposter;
            private readonly SetterImposter _setterImposter;
            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultIndexerIndexerBehaviour
            {
                private bool _isOn = true;
                internal bool IsOn
                {
                    get
                    {
                        return System.Threading.Volatile.Read(ref _isOn);
                    }

                    set
                    {
                        System.Threading.Volatile.Write(ref _isOn, value);
                    }
                }

                internal System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArguments, int> BackingField = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArguments, int>();
                internal int Get(IndexerIndexerArguments arguments)
                {
                    int value = default(int);
                    if (BackingField.TryGetValue(arguments, out value))
                        return value;
                    return default(int);
                }

                internal void Set(IndexerIndexerArguments arguments, int value)
                {
                    BackingField[arguments] = value;
                }
            }

            internal IndexerIndexerBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
            {
                this._getterImposter = new GetterImposter(_IndexerDefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
                this._setterImposter = new SetterImposter(_IndexerDefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
            }

            internal IIndexerIndexerGetterBuilder CreateGetter(IndexerIndexerArgumentsCriteria criteria)
            {
                return new GetterImposter.Builder(_getterImposter, criteria);
            }

            internal IIndexerIndexerSetterBuilder CreateSetter(IndexerIndexerArgumentsCriteria criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class InvocationBuilder : IIndexerIndexerBuilder
            {
                private readonly IndexerIndexerBuilder _builder;
                private readonly IndexerIndexerArgumentsCriteria _criteria;
                internal InvocationBuilder(IndexerIndexerBuilder builder, IndexerIndexerArgumentsCriteria criteria)
                {
                    this._builder = builder;
                    this._criteria = criteria;
                }

                public IIndexerIndexerGetterBuilder Getter()
                {
                    return _builder.CreateGetter(_criteria);
                }

                public IIndexerIndexerSetterBuilder Setter()
                {
                    return _builder.CreateSetter(_criteria);
                }
            }

            internal int Get(int key1, string key2, object key3)
            {
                return _getterImposter.Get(key1, key2, key3);
            }

            internal void Set(int key1, string key2, object key3, int value)
            {
                _setterImposter.Set(key1, key2, key3, value);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class GetterImposter
            {
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter> _setups = new System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter> _setupLookup = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal int Get(int key1, string key2, object key3)
                {
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(key1, key2, key3);
                    try
                    {
                        var setup = FindMatchingSetup(arguments);
                        if (setup is null)
                        {
                            EnsureGetterConfigured();
                            if (_defaultBehaviour.IsOn)
                                return _defaultBehaviour.Get(arguments);
                            throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return setup.Invoke(arguments);
                    }
                    finally
                    {
                        _invocationHistory.Add(arguments);
                    }
                }

                private GetterInvocationImposter FindMatchingSetup(IndexerIndexerArguments arguments)
                {
                    foreach (var setup in _setups)
                    {
                        if (setup.Criteria.Matches(arguments))
                        {
                            return setup;
                        }
                    }

                    return null;
                }

                private GetterInvocationImposter GetOrCreate(IndexerIndexerArgumentsCriteria criteria)
                {
                    return _setupLookup.GetOrAdd(criteria, CreateSetup);
                    GetterInvocationImposter CreateSetup(IndexerIndexerArgumentsCriteria key)
                    {
                        GetterInvocationImposter setup = new GetterInvocationImposter(this, _defaultBehaviour, key);
                        _setups.Push(setup);
                        return setup;
                    }
                }

                private void Called(IndexerIndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal string PropertyDisplayName => _propertyDisplayName;

                internal void MarkReturnConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredReturn, true);
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredReturn))
                    {
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerGetterBuilder
                {
                    private readonly GetterImposter _imposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(GetterImposter _imposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._imposter = _imposter;
                        this._criteria = _criteria;
                    }

                    private GetterInvocationImposter InvocationImposter => _imposter.GetOrCreate(_criteria);

                    public IIndexerIndexerGetterBuilder Returns(int value)
                    {
                        InvocationImposter.AddReturnValue(arguments => value);
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Returns(System.Func<int> valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator());
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Returns(IndexerIndexerDelegate valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator(arguments.key1, arguments.key2, arguments.key3));
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Throws(System.Exception exception)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exception);
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Throws<TException>()
                        where TException : Exception, new()
                    {
                        InvocationImposter.AddReturnValue(arguments => throw new TException());
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Throws(IndexerIndexerExceptionGenerator exceptionGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exceptionGenerator(arguments.key1, arguments.key2, arguments.key3));
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Callback(IndexerIndexerGetterCallback callback)
                    {
                        InvocationImposter.AddCallback(callback);
                        return this;
                    }

                    public void Called(Imposter.Abstractions.Count count)
                    {
                        _imposter.Called(_criteria, count);
                    }

                    public IIndexerIndexerGetterBuilder Then()
                    {
                        return this;
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                private sealed class GetterInvocationImposter
                {
                    private readonly GetterImposter _parent;
                    private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                    private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<IndexerIndexerArguments, int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<IndexerIndexerArguments, int>>();
                    private readonly System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerGetterCallback> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerGetterCallback>();
                    private System.Func<IndexerIndexerArguments, int>? _lastReturnValue;
                    private int _invocationCount;
                    private string _propertyDisplayName;
                    internal IndexerIndexerArgumentsCriteria Criteria { get; private set; }
                    internal int InvocationCount => System.Threading.Volatile.Read(ref _invocationCount);

                    internal GetterInvocationImposter(GetterImposter parent, DefaultIndexerIndexerBehaviour defaultBehaviour, IndexerIndexerArgumentsCriteria criteria)
                    {
                        this._parent = parent;
                        this._defaultBehaviour = defaultBehaviour;
                        this._propertyDisplayName = parent.PropertyDisplayName;
                        this.Criteria = criteria;
                    }

                    internal void AddReturnValue(System.Func<IndexerIndexerArguments, int> generator)
                    {
                        _defaultBehaviour.IsOn = false;
                        _returnValues.Enqueue(generator);
                        _parent.MarkReturnConfigured();
                    }

                    internal void AddCallback(IndexerIndexerGetterCallback callback)
                    {
                        _callbacks.Enqueue(callback);
                    }

                    internal int Invoke(IndexerIndexerArguments arguments)
                    {
                        System.Threading.Interlocked.Increment(ref _invocationCount);
                        foreach (var callback in _callbacks)
                        {
                            callback(arguments.key1, arguments.key2, arguments.key3);
                        }

                        System.Func<IndexerIndexerArguments, int> generator = ResolveNextGenerator(arguments);
                        return generator(arguments);
                    }

                    private System.Func<IndexerIndexerArguments, int> ResolveNextGenerator(IndexerIndexerArguments arguments)
                    {
                        if (_defaultBehaviour.IsOn)
                        {
                            return arguments => _defaultBehaviour.Get(arguments);
                        }

                        if (_returnValues.TryDequeue(out System.Func<IndexerIndexerArguments, int> returnValue))
                        {
                            _lastReturnValue = returnValue;
                        }

                        if (_lastReturnValue == null)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return _lastReturnValue;
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<(IndexerIndexerArgumentsCriteria Criteria, IndexerIndexerSetterCallback Callback)> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<(IndexerIndexerArgumentsCriteria Criteria, IndexerIndexerSetterCallback Callback)>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                public void Callback(IndexerIndexerArgumentsCriteria criteria, IndexerIndexerSetterCallback callback)
                {
                    _callbacks.Enqueue((criteria, callback));
                }

                public void Called(IndexerIndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal void Set(int key1, string key2, object key3, int value)
                {
                    EnsureSetterConfigured();
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(key1, key2, key3);
                    _invocationHistory.Add(arguments);
                    foreach (var registration in _callbacks)
                    {
                        if (registration.Criteria.Matches(arguments))
                        {
                            registration.Callback(arguments.key1, arguments.key2, arguments.key3, value);
                        }
                    }

                    if (_defaultBehaviour.IsOn)
                    {
                        _defaultBehaviour.Set(arguments, value);
                    }
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredSetter))
                    {
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                    }
                }

                internal void MarkConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredSetter, true);
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerSetterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(SetterImposter _setterImposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    public IIndexerIndexerSetterBuilder Callback(IndexerIndexerSetterCallback callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    public void Called(Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    public IIndexerIndexerSetterBuilder Then()
                    {
                        return this;
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterBuilder
        {
            IIndexerIndexerGetterBuilder Returns(int value);
            IIndexerIndexerGetterBuilder Returns(System.Func<int> valueGenerator);
            IIndexerIndexerGetterBuilder Returns(IndexerIndexerDelegate valueGenerator);
            IIndexerIndexerGetterBuilder Throws(System.Exception exception);
            IIndexerIndexerGetterBuilder Throws<TException>()
                where TException : Exception, new();
            IIndexerIndexerGetterBuilder Throws(IndexerIndexerExceptionGenerator exceptionGenerator);
            IIndexerIndexerGetterBuilder Callback(IndexerIndexerGetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexerIndexerGetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterBuilder
        {
            IIndexerIndexerSetterBuilder Callback(IndexerIndexerSetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexerIndexerSetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerBuilder
        {
            IIndexerIndexerGetterBuilder Getter();
            IIndexerIndexerSetterBuilder Setter();
        }

        private readonly Indexer_1IndexerBuilder _Indexer_1Indexer;
        public IIndexer_1IndexerBuilder this[Imposter.Abstractions.Arg<int> key1] => new Indexer_1IndexerBuilder.InvocationBuilder(_Indexer_1Indexer, new Indexer_1IndexerArgumentsCriteria(key1));
        public delegate int Indexer_1IndexerDelegate(int key1);
        public delegate void Indexer_1IndexerGetterCallback(int key1);
        public delegate void Indexer_1IndexerSetterCallback(int key1, int value);
        public delegate System.Exception Indexer_1IndexerExceptionGenerator(int key1);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Indexer_1IndexerArguments : global::System.IEquatable<Indexer_1IndexerArguments>
        {
            public int key1;
            internal Indexer_1IndexerArguments(int key1)
            {
                this.key1 = key1;
            }

            public bool Equals(Indexer_1IndexerArguments other)
            {
                return true && key1 == other.key1;
            }

            public override bool Equals(object obj)
            {
                return obj is Indexer_1IndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                global::System.HashCode hash = new global::System.HashCode();
                hash.Add(key1);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Indexer_1IndexerArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> key1;
            internal Indexer_1IndexerArgumentsCriteria(Imposter.Abstractions.Arg<int> key1)
            {
                this.key1 = key1;
            }

            public bool Matches(Indexer_1IndexerArguments arguments)
            {
                return key1.Matches(arguments.key1);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Indexer_1IndexerBuilder
        {
            private readonly DefaultIndexer_1IndexerBehaviour _Indexer_1DefaultIndexerBehaviour = new DefaultIndexer_1IndexerBehaviour();
            private readonly GetterImposter _getterImposter;
            private readonly SetterImposter _setterImposter;
            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultIndexer_1IndexerBehaviour
            {
                private bool _isOn = true;
                internal bool IsOn
                {
                    get
                    {
                        return System.Threading.Volatile.Read(ref _isOn);
                    }

                    set
                    {
                        System.Threading.Volatile.Write(ref _isOn, value);
                    }
                }

                internal System.Collections.Concurrent.ConcurrentDictionary<Indexer_1IndexerArguments, int> BackingField = new System.Collections.Concurrent.ConcurrentDictionary<Indexer_1IndexerArguments, int>();
                internal int Get(Indexer_1IndexerArguments arguments)
                {
                    int value = default(int);
                    if (BackingField.TryGetValue(arguments, out value))
                        return value;
                    return default(int);
                }

                internal void Set(Indexer_1IndexerArguments arguments, int value)
                {
                    BackingField[arguments] = value;
                }
            }

            internal Indexer_1IndexerBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
            {
                this._getterImposter = new GetterImposter(_Indexer_1DefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
                this._setterImposter = new SetterImposter(_Indexer_1DefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
            }

            internal IIndexer_1IndexerGetterBuilder CreateGetter(Indexer_1IndexerArgumentsCriteria criteria)
            {
                return new GetterImposter.Builder(_getterImposter, criteria);
            }

            internal IIndexer_1IndexerSetterBuilder CreateSetter(Indexer_1IndexerArgumentsCriteria criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class InvocationBuilder : IIndexer_1IndexerBuilder
            {
                private readonly Indexer_1IndexerBuilder _builder;
                private readonly Indexer_1IndexerArgumentsCriteria _criteria;
                internal InvocationBuilder(Indexer_1IndexerBuilder builder, Indexer_1IndexerArgumentsCriteria criteria)
                {
                    this._builder = builder;
                    this._criteria = criteria;
                }

                public IIndexer_1IndexerGetterBuilder Getter()
                {
                    return _builder.CreateGetter(_criteria);
                }

                public IIndexer_1IndexerSetterBuilder Setter()
                {
                    return _builder.CreateSetter(_criteria);
                }
            }

            internal int Get(int key1)
            {
                return _getterImposter.Get(key1);
            }

            internal void Set(int key1, int value)
            {
                _setterImposter.Set(key1, value);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class GetterImposter
            {
                private readonly DefaultIndexer_1IndexerBehaviour _defaultBehaviour;
                private readonly System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter> _setups = new System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentDictionary<Indexer_1IndexerArgumentsCriteria, GetterInvocationImposter> _setupLookup = new System.Collections.Concurrent.ConcurrentDictionary<Indexer_1IndexerArgumentsCriteria, GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentBag<Indexer_1IndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<Indexer_1IndexerArguments>();
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposter(DefaultIndexer_1IndexerBehaviour defaultBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal int Get(int key1)
                {
                    Indexer_1IndexerArguments arguments = new Indexer_1IndexerArguments(key1);
                    try
                    {
                        var setup = FindMatchingSetup(arguments);
                        if (setup is null)
                        {
                            EnsureGetterConfigured();
                            if (_defaultBehaviour.IsOn)
                                return _defaultBehaviour.Get(arguments);
                            throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return setup.Invoke(arguments);
                    }
                    finally
                    {
                        _invocationHistory.Add(arguments);
                    }
                }

                private GetterInvocationImposter FindMatchingSetup(Indexer_1IndexerArguments arguments)
                {
                    foreach (var setup in _setups)
                    {
                        if (setup.Criteria.Matches(arguments))
                        {
                            return setup;
                        }
                    }

                    return null;
                }

                private GetterInvocationImposter GetOrCreate(Indexer_1IndexerArgumentsCriteria criteria)
                {
                    return _setupLookup.GetOrAdd(criteria, CreateSetup);
                    GetterInvocationImposter CreateSetup(Indexer_1IndexerArgumentsCriteria key)
                    {
                        GetterInvocationImposter setup = new GetterInvocationImposter(this, _defaultBehaviour, key);
                        _setups.Push(setup);
                        return setup;
                    }
                }

                private void Called(Indexer_1IndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal string PropertyDisplayName => _propertyDisplayName;

                internal void MarkReturnConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredReturn, true);
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredReturn))
                    {
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexer_1IndexerGetterBuilder
                {
                    private readonly GetterImposter _imposter;
                    private readonly Indexer_1IndexerArgumentsCriteria _criteria;
                    internal Builder(GetterImposter _imposter, Indexer_1IndexerArgumentsCriteria _criteria)
                    {
                        this._imposter = _imposter;
                        this._criteria = _criteria;
                    }

                    private GetterInvocationImposter InvocationImposter => _imposter.GetOrCreate(_criteria);

                    public IIndexer_1IndexerGetterBuilder Returns(int value)
                    {
                        InvocationImposter.AddReturnValue(arguments => value);
                        return this;
                    }

                    public IIndexer_1IndexerGetterBuilder Returns(System.Func<int> valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator());
                        return this;
                    }

                    public IIndexer_1IndexerGetterBuilder Returns(Indexer_1IndexerDelegate valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator(arguments.key1));
                        return this;
                    }

                    public IIndexer_1IndexerGetterBuilder Throws(System.Exception exception)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exception);
                        return this;
                    }

                    public IIndexer_1IndexerGetterBuilder Throws<TException>()
                        where TException : Exception, new()
                    {
                        InvocationImposter.AddReturnValue(arguments => throw new TException());
                        return this;
                    }

                    public IIndexer_1IndexerGetterBuilder Throws(Indexer_1IndexerExceptionGenerator exceptionGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exceptionGenerator(arguments.key1));
                        return this;
                    }

                    public IIndexer_1IndexerGetterBuilder Callback(Indexer_1IndexerGetterCallback callback)
                    {
                        InvocationImposter.AddCallback(callback);
                        return this;
                    }

                    public void Called(Imposter.Abstractions.Count count)
                    {
                        _imposter.Called(_criteria, count);
                    }

                    public IIndexer_1IndexerGetterBuilder Then()
                    {
                        return this;
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                private sealed class GetterInvocationImposter
                {
                    private readonly GetterImposter _parent;
                    private readonly DefaultIndexer_1IndexerBehaviour _defaultBehaviour;
                    private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<Indexer_1IndexerArguments, int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<Indexer_1IndexerArguments, int>>();
                    private readonly System.Collections.Concurrent.ConcurrentQueue<Indexer_1IndexerGetterCallback> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<Indexer_1IndexerGetterCallback>();
                    private System.Func<Indexer_1IndexerArguments, int>? _lastReturnValue;
                    private int _invocationCount;
                    private string _propertyDisplayName;
                    internal Indexer_1IndexerArgumentsCriteria Criteria { get; private set; }
                    internal int InvocationCount => System.Threading.Volatile.Read(ref _invocationCount);

                    internal GetterInvocationImposter(GetterImposter parent, DefaultIndexer_1IndexerBehaviour defaultBehaviour, Indexer_1IndexerArgumentsCriteria criteria)
                    {
                        this._parent = parent;
                        this._defaultBehaviour = defaultBehaviour;
                        this._propertyDisplayName = parent.PropertyDisplayName;
                        this.Criteria = criteria;
                    }

                    internal void AddReturnValue(System.Func<Indexer_1IndexerArguments, int> generator)
                    {
                        _defaultBehaviour.IsOn = false;
                        _returnValues.Enqueue(generator);
                        _parent.MarkReturnConfigured();
                    }

                    internal void AddCallback(Indexer_1IndexerGetterCallback callback)
                    {
                        _callbacks.Enqueue(callback);
                    }

                    internal int Invoke(Indexer_1IndexerArguments arguments)
                    {
                        System.Threading.Interlocked.Increment(ref _invocationCount);
                        foreach (var callback in _callbacks)
                        {
                            callback(arguments.key1);
                        }

                        System.Func<Indexer_1IndexerArguments, int> generator = ResolveNextGenerator(arguments);
                        return generator(arguments);
                    }

                    private System.Func<Indexer_1IndexerArguments, int> ResolveNextGenerator(Indexer_1IndexerArguments arguments)
                    {
                        if (_defaultBehaviour.IsOn)
                        {
                            return arguments => _defaultBehaviour.Get(arguments);
                        }

                        if (_returnValues.TryDequeue(out System.Func<Indexer_1IndexerArguments, int> returnValue))
                        {
                            _lastReturnValue = returnValue;
                        }

                        if (_lastReturnValue == null)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return _lastReturnValue;
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<(Indexer_1IndexerArgumentsCriteria Criteria, Indexer_1IndexerSetterCallback Callback)> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<(Indexer_1IndexerArgumentsCriteria Criteria, Indexer_1IndexerSetterCallback Callback)>();
                private readonly System.Collections.Concurrent.ConcurrentBag<Indexer_1IndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<Indexer_1IndexerArguments>();
                private readonly DefaultIndexer_1IndexerBehaviour _defaultBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultIndexer_1IndexerBehaviour defaultBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                public void Callback(Indexer_1IndexerArgumentsCriteria criteria, Indexer_1IndexerSetterCallback callback)
                {
                    _callbacks.Enqueue((criteria, callback));
                }

                public void Called(Indexer_1IndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal void Set(int key1, int value)
                {
                    EnsureSetterConfigured();
                    Indexer_1IndexerArguments arguments = new Indexer_1IndexerArguments(key1);
                    _invocationHistory.Add(arguments);
                    foreach (var registration in _callbacks)
                    {
                        if (registration.Criteria.Matches(arguments))
                        {
                            registration.Callback(arguments.key1, value);
                        }
                    }

                    if (_defaultBehaviour.IsOn)
                    {
                        _defaultBehaviour.Set(arguments, value);
                    }
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredSetter))
                    {
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                    }
                }

                internal void MarkConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredSetter, true);
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexer_1IndexerSetterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly Indexer_1IndexerArgumentsCriteria _criteria;
                    internal Builder(SetterImposter _setterImposter, Indexer_1IndexerArgumentsCriteria _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    public IIndexer_1IndexerSetterBuilder Callback(Indexer_1IndexerSetterCallback callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    public void Called(Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    public IIndexer_1IndexerSetterBuilder Then()
                    {
                        return this;
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexer_1IndexerGetterBuilder
        {
            IIndexer_1IndexerGetterBuilder Returns(int value);
            IIndexer_1IndexerGetterBuilder Returns(System.Func<int> valueGenerator);
            IIndexer_1IndexerGetterBuilder Returns(Indexer_1IndexerDelegate valueGenerator);
            IIndexer_1IndexerGetterBuilder Throws(System.Exception exception);
            IIndexer_1IndexerGetterBuilder Throws<TException>()
                where TException : Exception, new();
            IIndexer_1IndexerGetterBuilder Throws(Indexer_1IndexerExceptionGenerator exceptionGenerator);
            IIndexer_1IndexerGetterBuilder Callback(Indexer_1IndexerGetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexer_1IndexerGetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexer_1IndexerSetterBuilder
        {
            IIndexer_1IndexerSetterBuilder Callback(Indexer_1IndexerSetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexer_1IndexerSetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexer_1IndexerBuilder
        {
            IIndexer_1IndexerGetterBuilder Getter();
            IIndexer_1IndexerSetterBuilder Setter();
        }

        public IIndexerSetupSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
            this._IndexerIndexer = new IndexerIndexerBuilder(invocationBehavior, "Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut.this[int key1, string key2, object key3]");
            this._Indexer_1Indexer = new Indexer_1IndexerBuilder(invocationBehavior, "Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut.this[int key1]");
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IIndexerSetupSut
        {
            IIndexerSetupSutImposter _imposter;
            public ImposterTargetInstance(IIndexerSetupSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int this[int key1, string key2, object key3]
            {
                get
                {
                    return _imposter._IndexerIndexer.Get(key1, key2, key3);
                }

                set
                {
                    _imposter._IndexerIndexer.Set(key1, key2, key3, value);
                }
            }

            public int this[int key1]
            {
                get
                {
                    return _imposter._Indexer_1Indexer.Get(key1);
                }

                set
                {
                    _imposter._Indexer_1Indexer.Set(key1, value);
                }
            }
        }
    }
}