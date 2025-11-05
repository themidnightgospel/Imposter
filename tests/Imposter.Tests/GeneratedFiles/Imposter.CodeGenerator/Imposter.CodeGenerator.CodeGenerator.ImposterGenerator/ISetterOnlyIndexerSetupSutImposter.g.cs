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
    public class ISetterOnlyIndexerSetupSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.ISetterOnlyIndexerSetupSut>
    {
        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.ISetterOnlyIndexerSetupSut Instance() => _imposterInstance;
        global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.ISetterOnlyIndexerSetupSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.ISetterOnlyIndexerSetupSut>.Instance()
        {
            return _imposterInstance;
        }

        private readonly IndexerIndexerBuilder _IndexerIndexer;
        public IIndexerIndexerBuilder this[Imposter.Abstractions.Arg<int> key] => new IndexerIndexerBuilder.InvocationBuilder(_IndexerIndexer, new IndexerIndexerArgumentsCriteria(key));
        public delegate int IndexerIndexerDelegate(int key);
        public delegate void IndexerIndexerGetterCallback(int key);
        public delegate void IndexerIndexerSetterCallback(int key, int value);
        public delegate System.Exception IndexerIndexerExceptionGenerator(int key);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArguments : global::System.IEquatable<IndexerIndexerArguments>
        {
            public int key;
            internal IndexerIndexerArguments(int key)
            {
                this.key = key;
            }

            public bool Equals(IndexerIndexerArguments other)
            {
                return true && key == other.key;
            }

            public override bool Equals(object obj)
            {
                return obj is IndexerIndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                global::System.HashCode hash = new global::System.HashCode();
                hash.Add(key);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> key;
            internal IndexerIndexerArgumentsCriteria(Imposter.Abstractions.Arg<int> key)
            {
                this.key = key;
            }

            public bool Matches(IndexerIndexerArguments arguments)
            {
                return key.Matches(arguments.key);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerBuilder
        {
            private readonly DefaultIndexerIndexerBehaviour _IndexerDefaultIndexerBehaviour = new DefaultIndexerIndexerBehaviour();
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

            internal void Set(int key, int value)
            {
                _setterImposter.Set(key, value);
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

                internal void Set(int key, int value)
                {
                    EnsureSetterConfigured();
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(key);
                    _invocationHistory.Add(arguments);
                    foreach (var registration in _callbacks)
                    {
                        if (registration.Criteria.Matches(arguments))
                        {
                            registration.Callback(arguments.key, value);
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
        public interface IIndexerIndexerSetterBuilder
        {
            IIndexerIndexerSetterBuilder Callback(IndexerIndexerSetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexerIndexerSetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerBuilder
        {
            IIndexerIndexerSetterBuilder Setter();
        }

        public ISetterOnlyIndexerSetupSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
            this._IndexerIndexer = new IndexerIndexerBuilder(invocationBehavior, "Imposter.CodeGenerator.Tests.Features.IndexerSetup.ISetterOnlyIndexerSetupSut.this[int key]");
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.IndexerSetup.ISetterOnlyIndexerSetupSut
        {
            ISetterOnlyIndexerSetupSutImposter _imposter;
            public ImposterTargetInstance(ISetterOnlyIndexerSetupSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int this[int key]
            {
                set
                {
                    _imposter._IndexerIndexer.Set(key, value);
                }
            }
        }
    }
}