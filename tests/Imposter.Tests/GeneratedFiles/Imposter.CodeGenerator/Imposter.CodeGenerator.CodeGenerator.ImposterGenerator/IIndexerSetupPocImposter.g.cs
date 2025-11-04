using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Shared;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Shared
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IIndexerSetupPocImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Shared.IIndexerSetupPoc>
    {
        private readonly IndexerMethodMethodImposter _indexerMethodMethodImposter;
        private readonly IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection = new IndexerMethodMethodInvocationHistoryCollection();
        public IIndexerMethodMethodImposterBuilder IndexerMethod(Imposter.Abstractions.Arg<int> name, Imposter.Abstractions.Arg<string> lastname, Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> dog)
        {
            return new IndexerMethodMethodImposter.Builder(_indexerMethodMethodImposter, _indexerMethodMethodInvocationHistoryCollection, new IndexerMethodArgumentsCriteria(name, lastname, dog));
        }

        private ImposterTargetInstance _imposterInstance;
        public IIndexerSetupPocImposter()
        {
            this._indexerMethodMethodImposter = new IndexerMethodMethodImposter(_indexerMethodMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.CodeGenerator.Tests.Shared.IIndexerSetupPoc Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Shared.IIndexerSetupPoc>.Instance()
        {
            return _imposterInstance;
        }

        // string IIndexerSetupPoc.IndexerMethod(int name, string lastname, in Regex dog)
        public delegate string IndexerMethodDelegate(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog);
        // string IIndexerSetupPoc.IndexerMethod(int name, string lastname, in Regex dog)
        public delegate void IndexerMethodCallbackDelegate(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog);
        // string IIndexerSetupPoc.IndexerMethod(int name, string lastname, in Regex dog)
        public delegate System.Exception IndexerMethodExceptionGeneratorDelegate(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IndexerMethodArguments
        {
            public int name;
            public string lastname;
            public global::System.Text.RegularExpressions.Regex dog;
            internal IndexerMethodArguments(int name, string lastname, global::System.Text.RegularExpressions.Regex dog)
            {
                this.name = name;
                this.lastname = lastname;
                this.dog = dog;
            }
        }

        // string IIndexerSetupPoc.IndexerMethod(int name, string lastname, in Regex dog)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IndexerMethodArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> name { get; }
            public Imposter.Abstractions.Arg<string> lastname { get; }
            public Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> dog { get; }

            public IndexerMethodArgumentsCriteria(Imposter.Abstractions.Arg<int> name, Imposter.Abstractions.Arg<string> lastname, Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> dog)
            {
                this.name = name;
                this.lastname = lastname;
                this.dog = dog;
            }

            public bool Matches(IndexerMethodArguments arguments)
            {
                return name.Matches(arguments.name) && lastname.Matches(arguments.lastname) && dog.Matches(arguments.dog);
            }
        }

        public interface IIndexerMethodMethodInvocationHistory
        {
            bool Matches(IndexerMethodArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodMethodInvocationHistory : IIndexerMethodMethodInvocationHistory
        {
            internal IndexerMethodArguments Arguments;
            internal string Result;
            internal System.Exception Exception;
            public IndexerMethodMethodInvocationHistory(IndexerMethodArguments Arguments, string Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IndexerMethodArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIndexerMethodMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIndexerMethodMethodInvocationHistory>();
            internal void Add(IIndexerMethodMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IndexerMethodArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // string IIndexerSetupPoc.IndexerMethod(int name, string lastname, in Regex dog)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IndexerMethodMethodInvocationImposterGroup
        {
            internal static IndexerMethodMethodInvocationImposterGroup Default = new IndexerMethodMethodInvocationImposterGroup(new IndexerMethodArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex>.Any()));
            internal IndexerMethodArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public IndexerMethodMethodInvocationImposterGroup(IndexerMethodArgumentsCriteria argumentsCriteria)
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

            public string Invoke(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(name, lastname, in dog);
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

                private IndexerMethodDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<IndexerMethodCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IndexerMethodCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public string Invoke(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    string result = _resultGenerator.Invoke(name, lastname, in dog);
                    foreach (var callback in _callbacks)
                    {
                        callback(name, lastname, in dog);
                    }

                    return result;
                }

                internal void Callback(IndexerMethodCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(IndexerMethodDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(string value)
                {
                    _resultGenerator = (int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                    {
                        return value;
                    };
                }

                internal void Throws(IndexerMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                    {
                        throw exceptionGenerator(name, lastname, in dog);
                    };
                }

                internal static string DefaultResultGenerator(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerMethodMethodInvocationImposterGroup
        {
            IIndexerMethodMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IIndexerMethodMethodInvocationImposterGroup Throws(System.Exception exception);
            IIndexerMethodMethodInvocationImposterGroup Throws(IndexerMethodExceptionGeneratorDelegate exceptionGenerator);
            IIndexerMethodMethodInvocationImposterGroup Callback(IndexerMethodCallbackDelegate callback);
            IIndexerMethodMethodInvocationImposterGroup Returns(IndexerMethodDelegate resultGenerator);
            IIndexerMethodMethodInvocationImposterGroup Returns(string value);
            IIndexerMethodMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IndexerMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // string IIndexerSetupPoc.IndexerMethod(int name, string lastname, in Regex dog)
        public interface IIndexerMethodMethodImposterBuilder : IIndexerMethodMethodInvocationImposterGroup, IndexerMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IndexerMethodMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<IndexerMethodMethodInvocationImposterGroup>();
            private readonly IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection;
            public IndexerMethodMethodImposter(IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection)
            {
                this._indexerMethodMethodInvocationHistoryCollection = _indexerMethodMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IndexerMethodArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private IndexerMethodMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(IndexerMethodArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public string Invoke(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
            {
                var arguments = new IndexerMethodArguments(name, lastname, dog);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? IndexerMethodMethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(name, lastname, in dog);
                    _indexerMethodMethodInvocationHistoryCollection.Add(new IndexerMethodMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _indexerMethodMethodInvocationHistoryCollection.Add(new IndexerMethodMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIndexerMethodMethodImposterBuilder
            {
                private readonly IndexerMethodMethodImposter _imposter;
                private readonly IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection;
                private readonly IndexerMethodArgumentsCriteria _argumentsCriteria;
                private readonly IndexerMethodMethodInvocationImposterGroup _invocationImposterGroup;
                private IndexerMethodMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(IndexerMethodMethodImposter _imposter, IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection, IndexerMethodArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._indexerMethodMethodInvocationHistoryCollection = _indexerMethodMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new IndexerMethodMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Throws(IndexerMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                    {
                        throw exceptionGenerator.Invoke(name, lastname, in dog);
                    });
                    return this;
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Callback(IndexerMethodCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Returns(IndexerMethodDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Returns(string value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IIndexerMethodMethodInvocationImposterGroup IIndexerMethodMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void IndexerMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _indexerMethodMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Shared.IIndexerSetupPoc
        {
            IIndexerSetupPocImposter _imposter;
            public ImposterTargetInstance(IIndexerSetupPocImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public string IndexerMethod(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
            {
                return _imposter._indexerMethodMethodImposter.Invoke(name, lastname, in dog);
            }
        }
    }
}