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
    public class IGetterOnlyIndexerSetupSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IGetterOnlyIndexerSetupSut>
    {
        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IGetterOnlyIndexerSetupSut Instance() => _imposterInstance;
        global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IGetterOnlyIndexerSetupSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IGetterOnlyIndexerSetupSut>.Instance()
        {
            return _imposterInstance;
        }

        private readonly IndexerIndexerBuilder _IndexerIndexer;
        public IIndexerIndexerBuilder this[Imposter.Abstractions.Arg<int> key, Imposter.Abstractions.Arg<string> name] => new IndexerIndexerBuilder.InvocationBuilder(_IndexerIndexer, new IndexerIndexerArgumentsCriteria(key, name));
        public delegate int IndexerIndexerDelegate(int key, string name);
        public delegate void IndexerIndexerGetterCallback(int key, string name);
        public delegate void IndexerIndexerSetterCallback(int key, string name, int value);
        public delegate System.Exception IndexerIndexerExceptionGenerator(int key, string name);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArguments : global::System.IEquatable<IndexerIndexerArguments>
        {
            public int key;
            public string name;
            internal IndexerIndexerArguments(int key, string name)
            {
                this.key = key;
                this.name = name;
            }

            public bool Equals(IndexerIndexerArguments other)
            {
                return true && key == other.key && name == other.name;
            }

            public override bool Equals(object obj)
            {
                return obj is IndexerIndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                global::System.HashCode hash = new global::System.HashCode();
                hash.Add(key);
                hash.Add(name);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> key;
            public Imposter.Abstractions.Arg<string> name;
            internal IndexerIndexerArgumentsCriteria(Imposter.Abstractions.Arg<int> key, Imposter.Abstractions.Arg<string> name)
            {
                this.key = key;
                this.name = name;
            }

            public bool Matches(IndexerIndexerArguments arguments)
            {
                return key.Matches(arguments.key) && name.Matches(arguments.name);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerBuilder
        {
            private readonly DefaultIndexerIndexerBehaviour _IndexerDefaultIndexerBehaviour = new DefaultIndexerIndexerBehaviour();
            private readonly GetterImposter _getterImposter;
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
            }

            internal IIndexerIndexerGetterBuilder CreateGetter(IndexerIndexerArgumentsCriteria criteria)
            {
                return new GetterImposter.Builder(_getterImposter, criteria);
            }

            internal IIndexerIndexerSetterBuilder CreateSetter(IndexerIndexerArgumentsCriteria criteria)
            {
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

            internal int Get(int key, string name)
            {
                return _getterImposter.Get(key, name);
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

                internal int Get(int key, string name)
                {
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(key, name);
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
                        InvocationImposter.AddReturnValue(arguments => valueGenerator(arguments.key, arguments.name));
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
                        InvocationImposter.AddReturnValue(arguments => throw exceptionGenerator(arguments.key, arguments.name));
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
                            callback(arguments.key, arguments.name);
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
        public interface IIndexerIndexerBuilder
        {
            IIndexerIndexerGetterBuilder Getter();
        }

        public IGetterOnlyIndexerSetupSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
            this._IndexerIndexer = new IndexerIndexerBuilder(invocationBehavior, "Imposter.CodeGenerator.Tests.Features.IndexerSetup.IGetterOnlyIndexerSetupSut.this[int key, string name]");
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.IGetterOnlyIndexerSetupSut
        {
            IGetterOnlyIndexerSetupSutImposter _imposter;
            public ImposterTargetInstance(IGetterOnlyIndexerSetupSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int this[int key, string name]
            {
                get
                {
                    return _imposter._IndexerIndexer.Get(key, name);
                }
            }
        }
    }
}