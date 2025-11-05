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
    public class IIndexerSetupPocSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IIndexerSetupPocSut>
    {
        private readonly IndexerMethodImposter _indexerMethodImposter;
        private readonly IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection = new IndexerMethodInvocationHistoryCollection();
        public IIndexerMethodImposterBuilder Indexer(Imposter.Abstractions.Arg<int> value1, Imposter.Abstractions.Arg<string> value2, Imposter.Abstractions.Arg<object> value3)
        {
            return new IndexerMethodImposter.Builder(_indexerMethodImposter, _indexerMethodInvocationHistoryCollection, new IndexerArgumentsCriteria(value1, value2, value3));
        }

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IIndexerSetupPocSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IIndexerSetupPocSut>.Instance()
        {
            return _imposterInstance;
        }

        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public delegate string IndexerDelegate(int value1, string value2, object value3);
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public delegate void IndexerCallbackDelegate(int value1, string value2, object value3);
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public delegate System.Exception IndexerExceptionGeneratorDelegate(int value1, string value2, object value3);
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

        public interface IIndexerMethodInvocationHistory
        {
            bool Matches(IndexerArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodInvocationHistory : IIndexerMethodInvocationHistory
        {
            internal IndexerArguments Arguments;
            internal string Result;
            internal System.Exception Exception;
            public IndexerMethodInvocationHistory(IndexerArguments Arguments, string Result, System.Exception Exception)
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

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIndexerMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIndexerMethodInvocationHistory>();
            internal void Add(IIndexerMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IndexerArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IndexerMethodInvocationImposterGroup
        {
            internal static IndexerMethodInvocationImposterGroup Default = new IndexerMethodInvocationImposterGroup(new IndexerArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<object>.Any()));
            internal IndexerArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public IndexerMethodInvocationImposterGroup(IndexerArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal MethodInvocationImposter AddInvocationImposter()
            {
                MethodInvocationImposter invocationImposter = new MethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private MethodInvocationImposter? GetInvocationImposter()
            {
                MethodInvocationImposter invocationImposter;
                if (_invocationImposters.TryDequeue(out invocationImposter))
                {
                    if (!invocationImposter.IsEmpty)
                    {
                        _lastestInvocationImposter = invocationImposter;
                    }
                }

                return _lastestInvocationImposter;
            }

            public string Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value1, string value2, object value3)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value1, value2, value3);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.Returns(DefaultResultGenerator);
                }

                private IndexerDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<IndexerCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IndexerCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public string Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value1, string value2, object value3)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    string result = _resultGenerator.Invoke(value1, value2, value3);
                    foreach (var callback in _callbacks)
                    {
                        callback(value1, value2, value3);
                    }

                    return result;
                }

                internal void Callback(IndexerCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(IndexerDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(string value)
                {
                    _resultGenerator = (int value1, string value2, object value3) =>
                    {
                        return value;
                    };
                }

                internal void Throws(IndexerExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int value1, string value2, object value3) =>
                    {
                        throw exceptionGenerator(value1, value2, value3);
                    };
                }

                internal static string DefaultResultGenerator(int value1, string value2, object value3)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerMethodInvocationImposterGroup
        {
            IIndexerMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IIndexerMethodInvocationImposterGroup Throws(System.Exception exception);
            IIndexerMethodInvocationImposterGroup Throws(IndexerExceptionGeneratorDelegate exceptionGenerator);
            IIndexerMethodInvocationImposterGroup Callback(IndexerCallbackDelegate callback);
            IIndexerMethodInvocationImposterGroup Returns(IndexerDelegate resultGenerator);
            IIndexerMethodInvocationImposterGroup Returns(string value);
            IIndexerMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IndexerInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public interface IIndexerMethodImposterBuilder : IIndexerMethodInvocationImposterGroup, IndexerInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IndexerMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<IndexerMethodInvocationImposterGroup>();
            private readonly IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public IndexerMethodImposter(IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._indexerMethodInvocationHistoryCollection = _indexerMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(IndexerArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private IndexerMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(IndexerArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public string Invoke(int value1, string value2, object value3)
            {
                var arguments = new IndexerArguments(value1, value2, value3);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)");
                    }

                    matchingInvocationImposterGroup = IndexerMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)", value1, value2, value3);
                    _indexerMethodInvocationHistoryCollection.Add(new IndexerMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _indexerMethodInvocationHistoryCollection.Add(new IndexerMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIndexerMethodImposterBuilder
            {
                private readonly IndexerMethodImposter _imposter;
                private readonly IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection;
                private readonly IndexerArgumentsCriteria _argumentsCriteria;
                private readonly IndexerMethodInvocationImposterGroup _invocationImposterGroup;
                private IndexerMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(IndexerMethodImposter _imposter, IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection, IndexerArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._indexerMethodInvocationHistoryCollection = _indexerMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new IndexerMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value1, string value2, object value3) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value1, string value2, object value3) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Throws(IndexerExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value1, string value2, object value3) =>
                    {
                        throw exceptionGenerator.Invoke(value1, value2, value3);
                    });
                    return this;
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Callback(IndexerCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Returns(IndexerDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Returns(string value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IIndexerMethodInvocationImposterGroup IIndexerMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void IndexerInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _indexerMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IIndexerSetupPocSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._indexerMethodImposter = new IndexerMethodImposter(_indexerMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IIndexerSetupPocSut
        {
            IIndexerSetupPocSutImposter _imposter;
            public ImposterTargetInstance(IIndexerSetupPocSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public string Indexer(int value1, string value2, object value3)
            {
                return _imposter._indexerMethodImposter.Invoke(value1, value2, value3);
            }
        }
    }
}