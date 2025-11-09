using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.MethodImposter;

#pragma warning disable nullable
namespace Imposter.Tests.Features.MethodImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IClosedGenericSutImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.MethodImposter.IClosedGenericSut<int, string>>
    {
        private readonly GenericMethodMethodImposter _genericMethodMethodImposter;
        private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection = new GenericMethodMethodInvocationHistoryCollection();
        public IGenericMethodMethodImposterBuilder GenericMethod(global::Imposter.Abstractions.Arg<int> age)
        {
            return new GenericMethodMethodImposter.Builder(_genericMethodMethodImposter, _genericMethodMethodInvocationHistoryCollection, new GenericMethodArgumentsCriteria(age));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        internal global::Imposter.Tests.Features.MethodImposter.IClosedGenericSut<int, string> Instance() => _imposterInstance;
        global::Imposter.Tests.Features.MethodImposter.IClosedGenericSut<int, string> global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.MethodImposter.IClosedGenericSut<int, string>>.Instance()
        {
            return _imposterInstance;
        }

        // string IClosedGenericSut<int, string>.GenericMethod(int age)
        public delegate string GenericMethodDelegate(int age);
        // string IClosedGenericSut<int, string>.GenericMethod(int age)
        public delegate void GenericMethodCallbackDelegate(int age);
        // string IClosedGenericSut<int, string>.GenericMethod(int age)
        public delegate System.Exception GenericMethodExceptionGeneratorDelegate(int age);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericMethodArguments
        {
            public int age;
            internal GenericMethodArguments(int age)
            {
                this.age = age;
            }
        }

        // string IClosedGenericSut<int, string>.GenericMethod(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericMethodArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> age { get; }

            public GenericMethodArgumentsCriteria(global::Imposter.Abstractions.Arg<int> age)
            {
                this.age = age;
            }

            public bool Matches(GenericMethodArguments arguments)
            {
                return age.Matches(arguments.age);
            }
        }

        public interface IGenericMethodMethodInvocationHistory
        {
            bool Matches(GenericMethodArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodInvocationHistory : IGenericMethodMethodInvocationHistory
        {
            internal GenericMethodArguments Arguments;
            internal string Result;
            internal System.Exception Exception;
            public GenericMethodMethodInvocationHistory(GenericMethodArguments Arguments, string Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(GenericMethodArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericMethodMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericMethodMethodInvocationHistory>();
            internal void Add(IGenericMethodMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(GenericMethodArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // string IClosedGenericSut<int, string>.GenericMethod(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericMethodMethodInvocationImposterGroup
        {
            internal static GenericMethodMethodInvocationImposterGroup Default = new GenericMethodMethodInvocationImposterGroup(new GenericMethodArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal GenericMethodArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public GenericMethodMethodInvocationImposterGroup(GenericMethodArgumentsCriteria argumentsCriteria)
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

            public string Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int age)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, age);
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

                private GenericMethodDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<GenericMethodCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<GenericMethodCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public string Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int age)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    string result = _resultGenerator.Invoke(age);
                    foreach (var callback in _callbacks)
                    {
                        callback(age);
                    }

                    return result;
                }

                internal void Callback(GenericMethodCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(GenericMethodDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(string value)
                {
                    _resultGenerator = (int age) =>
                    {
                        return value;
                    };
                }

                internal void Throws(GenericMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int age) =>
                    {
                        throw exceptionGenerator(age);
                    };
                }

                internal static string DefaultResultGenerator(int age)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationImposterGroupCallback
        {
            IGenericMethodMethodInvocationImposterGroupContinuation Callback(GenericMethodCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationImposterGroupContinuation : IGenericMethodMethodInvocationImposterGroupCallback
        {
            IGenericMethodMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationImposterGroup : IGenericMethodMethodInvocationImposterGroupCallback
        {
            IGenericMethodMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IGenericMethodMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IGenericMethodMethodInvocationImposterGroupContinuation Throws(GenericMethodExceptionGeneratorDelegate exceptionGenerator);
            IGenericMethodMethodInvocationImposterGroupContinuation Returns(GenericMethodDelegate resultGenerator);
            IGenericMethodMethodInvocationImposterGroupContinuation Returns(string value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // string IClosedGenericSut<int, string>.GenericMethod(int age)
        public interface IGenericMethodMethodImposterBuilder : IGenericMethodMethodInvocationImposterGroup, IGenericMethodMethodInvocationImposterGroupCallback, GenericMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericMethodMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<GenericMethodMethodInvocationImposterGroup>();
            private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public GenericMethodMethodImposter(GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(GenericMethodArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private GenericMethodMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(GenericMethodArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public string Invoke(int age)
            {
                var arguments = new GenericMethodArguments(age);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("string IClosedGenericSut<int, string>.GenericMethod(int age)");
                    }

                    matchingInvocationImposterGroup = GenericMethodMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "string IClosedGenericSut<int, string>.GenericMethod(int age)", age);
                    _genericMethodMethodInvocationHistoryCollection.Add(new GenericMethodMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericMethodMethodInvocationHistoryCollection.Add(new GenericMethodMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericMethodMethodImposterBuilder, IGenericMethodMethodInvocationImposterGroupContinuation
            {
                private readonly GenericMethodMethodImposter _imposter;
                private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
                private readonly GenericMethodArgumentsCriteria _argumentsCriteria;
                private readonly GenericMethodMethodInvocationImposterGroup _invocationImposterGroup;
                private GenericMethodMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(GenericMethodMethodImposter _imposter, GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection, GenericMethodArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new GenericMethodMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IGenericMethodMethodInvocationImposterGroupContinuation IGenericMethodMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation IGenericMethodMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation IGenericMethodMethodInvocationImposterGroup.Throws(GenericMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw exceptionGenerator.Invoke(age);
                    });
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation IGenericMethodMethodInvocationImposterGroupCallback.Callback(GenericMethodCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation IGenericMethodMethodInvocationImposterGroup.Returns(GenericMethodDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation IGenericMethodMethodInvocationImposterGroup.Returns(string value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroup IGenericMethodMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void GenericMethodInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericMethodMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IClosedGenericSutImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._genericMethodMethodImposter = new GenericMethodMethodImposter(_genericMethodMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.MethodImposter.IClosedGenericSut<int, string>
        {
            IClosedGenericSutImposter _imposter;
            public ImposterTargetInstance(IClosedGenericSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public string GenericMethod(int age)
            {
                return _imposter._genericMethodMethodImposter.Invoke(age);
            }
        }
    }
}