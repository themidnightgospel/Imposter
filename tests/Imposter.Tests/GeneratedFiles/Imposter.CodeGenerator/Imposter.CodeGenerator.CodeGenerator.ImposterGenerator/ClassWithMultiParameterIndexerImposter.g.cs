#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using global::Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.ClassImposter.Suts;

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ClassWithMultiParameterIndexerImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithMultiParameterIndexer>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithMultiParameterIndexer global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithMultiParameterIndexer>.Instance()
        {
            return _imposterInstance;
        }

        private readonly IndexerIndexerBuilder _IndexerIndexer;
        public IIndexerIndexerBuilder this[global::Imposter.Abstractions.Arg<int> row, global::Imposter.Abstractions.Arg<string> column] => new IndexerIndexerBuilder.InvocationBuilder(_IndexerIndexer, new IndexerIndexerArgumentsCriteria(row, column));
        public delegate int IndexerIndexerDelegate(int row, string column);
        public delegate void IndexerIndexerGetterCallback(int row, string column);
        public delegate void IndexerIndexerSetterCallback(int row, string column, int value);
        public delegate System.Exception IndexerIndexerExceptionGenerator(int row, string column);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArguments : System.IEquatable<IndexerIndexerArguments>
        {
            public int row;
            public string column;
            internal IndexerIndexerArguments(int row, string column)
            {
                this.row = row;
                this.column = column;
            }

            public bool Equals(IndexerIndexerArguments other)
            {
                return true && row == other.row && column == other.column;
            }

            public override bool Equals(object obj)
            {
                return obj is IndexerIndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                System.HashCode hash = new System.HashCode();
                hash.Add(row);
                hash.Add(column);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> row;
            public global::Imposter.Abstractions.Arg<string> column;
            internal IndexerIndexerArgumentsCriteria(global::Imposter.Abstractions.Arg<int> row, global::Imposter.Abstractions.Arg<string> column)
            {
                this.row = row;
                this.column = column;
            }

            public bool Matches(IndexerIndexerArguments arguments)
            {
                return row.Matches(arguments.row) && column.Matches(arguments.column);
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

            internal IndexerIndexerBuilder(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
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

            internal int Get(int row, string column)
            {
                return _getterImposter.Get(row, column);
            }

            internal void Set(int row, string column, int value)
            {
                _setterImposter.Set(row, column, value);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class GetterImposter
            {
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter> _setups = new System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter> _setupLookup = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal int Get(int row, string column)
                {
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(row, column);
                    try
                    {
                        var setup = FindMatchingSetup(arguments);
                        if (setup is null)
                        {
                            EnsureGetterConfigured();
                            if (_defaultBehaviour.IsOn)
                                return _defaultBehaviour.Get(arguments);
                            throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
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

                private void Called(IndexerIndexerArgumentsCriteria criteria, global::Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal string PropertyDisplayName => _propertyDisplayName;

                internal void MarkReturnConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredReturn, true);
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredReturn))
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerGetterBuilder, IIndexerIndexerGetterFluentBuilder
                {
                    private readonly GetterImposter _imposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(GetterImposter _imposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._imposter = _imposter;
                        this._criteria = _criteria;
                    }

                    private GetterInvocationImposter InvocationImposter => _imposter.GetOrCreate(_criteria);

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Returns(int value)
                    {
                        InvocationImposter.AddReturnValue(arguments => value);
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Returns(System.Func<int> valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator());
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Returns(IndexerIndexerDelegate valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator(arguments.row, arguments.column));
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Throws(System.Exception exception)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exception);
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Throws<TException>()
                    {
                        InvocationImposter.AddReturnValue(arguments => throw new TException());
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterOutcomeBuilder.Throws(IndexerIndexerExceptionGenerator exceptionGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exceptionGenerator(arguments.row, arguments.column));
                        return this;
                    }

                    IIndexerIndexerGetterContinuationBuilder IIndexerIndexerGetterCallbackBuilder.Callback(IndexerIndexerGetterCallback callback)
                    {
                        InvocationImposter.AddCallback(callback);
                        return this;
                    }

                    void IIndexerIndexerGetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _imposter.Called(_criteria, count);
                    }

                    IIndexerIndexerGetterFluentBuilder IIndexerIndexerGetterContinuationBuilder.Then()
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
                            callback(arguments.row, arguments.column);
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
                            throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
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
                private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                public void Callback(IndexerIndexerArgumentsCriteria criteria, IndexerIndexerSetterCallback callback)
                {
                    _callbacks.Enqueue((criteria, callback));
                }

                public void Called(IndexerIndexerArgumentsCriteria criteria, global::Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal void Set(int row, string column, int value)
                {
                    EnsureSetterConfigured();
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(row, column);
                    _invocationHistory.Add(arguments);
                    foreach (var registration in _callbacks)
                    {
                        if (registration.Criteria.Matches(arguments))
                        {
                            registration.Callback(arguments.row, arguments.column, value);
                        }
                    }

                    if (_defaultBehaviour.IsOn)
                    {
                        _defaultBehaviour.Set(arguments, value);
                    }
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredSetter))
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                    }
                }

                internal void MarkConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredSetter, true);
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerSetterBuilder, IIndexerIndexerSetterFluentBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(SetterImposter _setterImposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    IIndexerIndexerSetterContinuationBuilder IIndexerIndexerSetterCallbackBuilder.Callback(IndexerIndexerSetterCallback callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void IIndexerIndexerSetterVerifier.Called(global::Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    IIndexerIndexerSetterFluentBuilder IIndexerIndexerSetterContinuationBuilder.Then()
                    {
                        return this;
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterOutcomeBuilder
        {
            IIndexerIndexerGetterContinuationBuilder Returns(int value);
            IIndexerIndexerGetterContinuationBuilder Returns(System.Func<int> valueGenerator);
            IIndexerIndexerGetterContinuationBuilder Returns(IndexerIndexerDelegate valueGenerator);
            IIndexerIndexerGetterContinuationBuilder Throws(System.Exception exception);
            IIndexerIndexerGetterContinuationBuilder Throws<TException>()
                where TException : Exception, new();
            IIndexerIndexerGetterContinuationBuilder Throws(IndexerIndexerExceptionGenerator exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterCallbackBuilder
        {
            IIndexerIndexerGetterContinuationBuilder Callback(IndexerIndexerGetterCallback callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterContinuationBuilder : IIndexerIndexerGetterCallbackBuilder
        {
            IIndexerIndexerGetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterFluentBuilder : IIndexerIndexerGetterOutcomeBuilder, IIndexerIndexerGetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterBuilder : IIndexerIndexerGetterOutcomeBuilder, IIndexerIndexerGetterCallbackBuilder, IIndexerIndexerGetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterCallbackBuilder
        {
            IIndexerIndexerSetterContinuationBuilder Callback(IndexerIndexerSetterCallback callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterContinuationBuilder : IIndexerIndexerSetterCallbackBuilder
        {
            IIndexerIndexerSetterFluentBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterFluentBuilder : IIndexerIndexerSetterCallbackBuilder, IIndexerIndexerSetterContinuationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterVerifier
        {
            void Called(global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterBuilder : IIndexerIndexerSetterCallbackBuilder, IIndexerIndexerSetterVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerBuilder
        {
            IIndexerIndexerGetterBuilder Getter();
            IIndexerIndexerSetterBuilder Setter();
        }

        public ClassWithMultiParameterIndexerImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._IndexerIndexer = new IndexerIndexerBuilder(invocationBehavior, "Imposter.Tests.Features.ClassImposter.Suts.ClassWithMultiParameterIndexer.this[int row, string column]");
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithMultiParameterIndexer
        {
            ClassWithMultiParameterIndexerImposter _imposter;
            internal void InitializeImposter(ClassWithMultiParameterIndexerImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            protected override int this[int row, string column]
            {
                get
                {
                    return _imposter._IndexerIndexer.Get(row, column);
                }

                set
                {
                    _imposter._IndexerIndexer.Set(row, column, value);
                }
            }
        }
    }
}