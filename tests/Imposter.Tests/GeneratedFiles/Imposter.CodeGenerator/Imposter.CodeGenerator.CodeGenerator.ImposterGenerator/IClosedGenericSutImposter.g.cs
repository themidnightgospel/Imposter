using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Features.MethodSetup;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.MethodSetup
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IClosedGenericSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IClosedGenericSut<int, string>>
    {
        private readonly GenericMethodMethodImposter _genericMethodMethodImposter;
        private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection = new GenericMethodMethodInvocationHistoryCollection();
        public IGenericMethodMethodImposterBuilder GenericMethod(Imposter.Abstractions.Arg<int> age)
        {
            return new GenericMethodMethodImposter.Builder(_genericMethodMethodImposter, _genericMethodMethodInvocationHistoryCollection, new GenericMethodArgumentsCriteria(age));
        }

        private ImposterTargetInstance _imposterInstance;
        public IClosedGenericSutImposter()
        {
            this._genericMethodMethodImposter = new GenericMethodMethodImposter(_genericMethodMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IClosedGenericSut<int, string>
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

        global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IClosedGenericSut<int, string> Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IClosedGenericSut<int, string>>.Instance()
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
            public Imposter.Abstractions.Arg<int> age { get; }

            public GenericMethodArgumentsCriteria(Imposter.Abstractions.Arg<int> age)
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

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericMethodMethodInvocationsSetup : IGenericMethodMethodInvocationsSetup
        {
            internal static GenericMethodMethodInvocationsSetup DefaultInvocationSetup = new GenericMethodMethodInvocationsSetup(new GenericMethodArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal GenericMethodArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static string DefaultResultGenerator(int age)
            {
                return default;
            }

            public GenericMethodMethodInvocationsSetup(GenericMethodArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Returns(GenericMethodDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Returns(string value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    return value;
                };
                return this;
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Throws(GenericMethodExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw exceptionGenerator(age);
                };
                return this;
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.CallBefore(GenericMethodCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.CallAfter(GenericMethodCallbackDelegate callback)
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

            public string Invoke(int age)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(age);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(age);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(age);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal GenericMethodDelegate? ResultGenerator { get; set; }
                internal GenericMethodCallbackDelegate? CallBefore { get; set; }
                internal GenericMethodCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationsSetup
        {
            IGenericMethodMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IGenericMethodMethodInvocationsSetup Throws(System.Exception exception);
            IGenericMethodMethodInvocationsSetup Throws(GenericMethodExceptionGeneratorDelegate exceptionGenerator);
            IGenericMethodMethodInvocationsSetup CallBefore(GenericMethodCallbackDelegate callback);
            IGenericMethodMethodInvocationsSetup CallAfter(GenericMethodCallbackDelegate callback);
            IGenericMethodMethodInvocationsSetup Returns(GenericMethodDelegate resultGenerator);
            IGenericMethodMethodInvocationsSetup Returns(string value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericMethodMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodImposterBuilder : IGenericMethodMethodInvocationsSetup, GenericMethodMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericMethodMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericMethodMethodInvocationsSetup>();
            private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
            public GenericMethodMethodImposter(GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection)
            {
                this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(GenericMethodArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericMethodMethodInvocationsSetup? FindMatchingSetup(GenericMethodArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public string Invoke(int age)
            {
                var arguments = new GenericMethodArguments(age);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericMethodMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age);
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
            internal class Builder : IGenericMethodMethodImposterBuilder
            {
                private readonly GenericMethodMethodImposter _imposter;
                private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
                private readonly GenericMethodArgumentsCriteria _argumentsCriteria;
                private GenericMethodMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(GenericMethodMethodImposter _imposter, GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection, GenericMethodArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericMethodMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericMethodMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Throws(GenericMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.CallBefore(GenericMethodCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.CallAfter(GenericMethodCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Returns(GenericMethodDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericMethodMethodInvocationsSetup IGenericMethodMethodInvocationsSetup.Returns(string value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void GenericMethodMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericMethodMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }
    }
}