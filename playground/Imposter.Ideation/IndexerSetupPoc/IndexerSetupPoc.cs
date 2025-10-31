using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Imposter.Abstractions;

#pragma warning disable nullable

namespace Imposter.Ideation.IndexerSetupPoc
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IndexerSetupPoc : Imposter.Abstractions.IHaveImposterInstance<IIndexerSetupPoc>
    {
        private ImposterTargetInstance _imposterInstance;
        private readonly IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection = new IndexerMethodMethodInvocationHistoryCollection();
        private readonly IndexerMethodMethodImposter _indexerMethodMethodImposter;

        public IndexerSetupPoc()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
            this._indexerMethodMethodImposter = new IndexerMethodMethodImposter(_indexerMethodMethodInvocationHistoryCollection);
        }

        public IIndexerMethodMethodImposterBuilder this[Imposter.Abstractions.Arg<int> name, Imposter.Abstractions.Arg<string> lastname, Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> dog]
        {
            get => new IndexerMethodMethodImposter.Builder(_indexerMethodMethodImposter, _indexerMethodMethodInvocationHistoryCollection, new IndexerMethodArgumentsCriteria(name, lastname, dog));
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : IIndexerSetupPoc
        {
            IndexerSetupPoc _imposter;

            public ImposterTargetInstance(IndexerSetupPoc _imposter)
            {
                this._imposter = _imposter;
            }

            public string this[int name, string lastname, in Regex dog]
            {
                get => _imposter._indexerMethodMethodImposter.Invoke(name, lastname, in dog);
                set => throw new NotImplementedException();
            }

            public string this[string name] => throw new NotImplementedException();
        }

        IIndexerSetupPoc IHaveImposterInstance<IIndexerSetupPoc>.Instance()
        {
            return _imposterInstance;
        }
    }

    public interface IIndexerSetupPoc
    {
        string this[int name, string lastname, in Regex dog] { get; set; }

        string this[string name] { get; }
    }
    
            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerMethodMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IndexerMethodMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IndexerMethodMethodInvocationsSetup>();
            private readonly IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection;
            public IndexerMethodMethodImposter(IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection)
            {
                this._indexerMethodMethodInvocationHistoryCollection = _indexerMethodMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IndexerMethodArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IndexerMethodMethodInvocationsSetup? FindMatchingSetup(IndexerMethodArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public string Invoke(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
            {
                var arguments = new IndexerMethodArguments(name, lastname, dog);
                var matchingSetup = FindMatchingSetup(arguments) ?? IndexerMethodMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(name, lastname, in dog);
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
        class IndexerMethodMethodInvocationsSetup : IIndexerMethodMethodInvocationsSetup
        {
            internal static IndexerMethodMethodInvocationsSetup DefaultInvocationSetup = new IndexerMethodMethodInvocationsSetup(new IndexerMethodArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex>.Any()));
            internal IndexerMethodArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static string DefaultResultGenerator(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
            {
                return default;
            }

            public IndexerMethodMethodInvocationsSetup(IndexerMethodArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Returns(IndexerMethodDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Returns(string value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                {
                    return value;
                };
                return this;
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                {
                    throw exception;
                };
                return this;
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Throws(IndexerMethodExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int name, string lastname, in global::System.Text.RegularExpressions.Regex dog) =>
                {
                    throw exceptionGenerator(name, lastname, in dog);
                };
                return this;
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.CallBefore(IndexerMethodCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.CallAfter(IndexerMethodCallbackDelegate callback)
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

            public string Invoke(int name, string lastname, in global::System.Text.RegularExpressions.Regex dog)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(name, lastname, in dog);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(name, lastname, in dog);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(name, lastname, in dog);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal IndexerMethodDelegate? ResultGenerator { get; set; }
                internal IndexerMethodCallbackDelegate? CallBefore { get; set; }
                internal IndexerMethodCallbackDelegate? CallAfter { get; set; }
            }
        }


            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIndexerMethodMethodImposterBuilder
            {
                private readonly IndexerMethodMethodImposter _imposter;
                private readonly IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection;
                private readonly IndexerMethodArgumentsCriteria _argumentsCriteria;
                private IndexerMethodMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IndexerMethodMethodImposter _imposter, IndexerMethodMethodInvocationHistoryCollection _indexerMethodMethodInvocationHistoryCollection, IndexerMethodArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._indexerMethodMethodInvocationHistoryCollection = _indexerMethodMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIndexerMethodMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IndexerMethodMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Throws(IndexerMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.CallBefore(IndexerMethodCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.CallAfter(IndexerMethodCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Returns(IndexerMethodDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIndexerMethodMethodInvocationsSetup IIndexerMethodMethodInvocationsSetup.Returns(string value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IndexerMethodMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
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
        public interface IIndexerMethodMethodInvocationsSetup
        {
            IIndexerMethodMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIndexerMethodMethodInvocationsSetup Throws(System.Exception exception);
            IIndexerMethodMethodInvocationsSetup Throws(IndexerMethodExceptionGeneratorDelegate exceptionGenerator);
            IIndexerMethodMethodInvocationsSetup CallBefore(IndexerMethodCallbackDelegate callback);
            IIndexerMethodMethodInvocationsSetup CallAfter(IndexerMethodCallbackDelegate callback);
            IIndexerMethodMethodInvocationsSetup Returns(IndexerMethodDelegate resultGenerator);
            IIndexerMethodMethodInvocationsSetup Returns(string value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IndexerMethodMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerMethodMethodImposterBuilder : IIndexerMethodMethodInvocationsSetup, IndexerMethodMethodInvocationVerifier
        {
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



    public class Consumer
    {
        static void SutUsage()
        {
                
        }
    }
}