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

        private ImposterTargetInstance _imposterInstance;
        public IIndexerSetupPocSutImposter()
        {
            this._indexerMethodImposter = new IndexerMethodImposter(_indexerMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

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
        class IndexerMethodInvocationsSetup : IIndexerMethodInvocationsSetup
        {
            internal static IndexerMethodInvocationsSetup DefaultInvocationSetup = new IndexerMethodInvocationsSetup(new IndexerArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<object>.Any()));
            internal IndexerArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
            private MethodInvocationSetup? _currentlySetupCall;
            private MethodInvocationSetup GetOrAddMethodSetup(Func<MethodInvocationSetup, bool> addNew)
            {
                if (_currentlySetupCall is null || addNew(_currentlySetupCall))
                {
                    _currentlySetupCall = new MethodInvocationSetup();
                    _callSetups.Enqueue(_currentlySetupCall);
                }

                return _currentlySetupCall;
            }

            internal static string DefaultResultGenerator(int value1, string value2, object value3)
            {
                return default;
            }

            public IndexerMethodInvocationsSetup(IndexerArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Returns(IndexerDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Returns(string value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value1, string value2, object value3) =>
                {
                    return value;
                };
                return this;
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value1, string value2, object value3) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value1, string value2, object value3) =>
                {
                    throw exception;
                };
                return this;
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Throws(IndexerExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value1, string value2, object value3) =>
                {
                    throw exceptionGenerator(value1, value2, value3);
                };
                return this;
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.CallBefore(IndexerCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.CallAfter(IndexerCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
            private MethodInvocationSetup? GetNextSetup()
            {
                if (_callSetups.TryDequeue(out var callSetup))
                {
                    _nextSetup = callSetup;
                }

                return _nextSetup;
            }

            public string Invoke(int value1, string value2, object value3)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(value1, value2, value3);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(value1, value2, value3);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(value1, value2, value3);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal IndexerDelegate? ResultGenerator { get; set; }
                internal IndexerCallbackDelegate? CallBefore { get; set; }
                internal IndexerCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerMethodInvocationsSetup
        {
            IIndexerMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIndexerMethodInvocationsSetup Throws(System.Exception exception);
            IIndexerMethodInvocationsSetup Throws(IndexerExceptionGeneratorDelegate exceptionGenerator);
            IIndexerMethodInvocationsSetup CallBefore(IndexerCallbackDelegate callback);
            IIndexerMethodInvocationsSetup CallAfter(IndexerCallbackDelegate callback);
            IIndexerMethodInvocationsSetup Returns(IndexerDelegate resultGenerator);
            IIndexerMethodInvocationsSetup Returns(string value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IndexerMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // string IIndexerSetupPocSut.Indexer(int value1, string value2, object value3)
        public interface IIndexerMethodImposterBuilder : IIndexerMethodInvocationsSetup, IndexerMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IndexerMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IndexerMethodInvocationsSetup>();
            private readonly IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection;
            public IndexerMethodImposter(IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection)
            {
                this._indexerMethodInvocationHistoryCollection = _indexerMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IndexerArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IndexerMethodInvocationsSetup? FindMatchingSetup(IndexerArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public string Invoke(int value1, string value2, object value3)
            {
                var arguments = new IndexerArguments(value1, value2, value3);
                var matchingSetup = FindMatchingSetup(arguments) ?? IndexerMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value1, value2, value3);
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
                private IndexerMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IndexerMethodImposter _imposter, IndexerMethodInvocationHistoryCollection _indexerMethodInvocationHistoryCollection, IndexerArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._indexerMethodInvocationHistoryCollection = _indexerMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIndexerMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IndexerMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Throws(IndexerExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.CallBefore(IndexerCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.CallAfter(IndexerCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Returns(IndexerDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIndexerMethodInvocationsSetup IIndexerMethodInvocationsSetup.Returns(string value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IndexerMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _indexerMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
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