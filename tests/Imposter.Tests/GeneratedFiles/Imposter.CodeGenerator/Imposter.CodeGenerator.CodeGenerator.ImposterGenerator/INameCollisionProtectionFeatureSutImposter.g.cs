using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Features.NameCollisionProtection;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.NameCollisionProtection
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class INameCollisionProtectionFeatureSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.NameCollisionProtection.INameCollisionProtectionFeatureSut>
    {
        private readonly CollisionWithReturnsMethodImposterCollection _collisionWithReturnsMethodImposterCollection;
        private readonly CollisionWithReturns_1MethodImposter _collisionWithReturns_1MethodImposter;
        private readonly CollisionWithReturns_2MethodImposter _collisionWithReturns_2MethodImposter;
        private readonly CollisionWithReturns_3MethodImposter _collisionWithReturns_3MethodImposter;
        private readonly CollisionWithThrowsMethodImposterCollection _collisionWithThrowsMethodImposterCollection;
        private readonly CollisionWithThrows_1MethodImposter _collisionWithThrows_1MethodImposter;
        private readonly MethodWithSameNameDifferentSignatureMethodImposter _methodWithSameNameDifferentSignatureMethodImposter;
        private readonly MethodWithSameNameDifferentSignature_1MethodImposter _methodWithSameNameDifferentSignature_1MethodImposter;
        private readonly MethodWithSameNameDifferentSignature_2MethodImposterCollection _methodWithSameNameDifferentSignature_2MethodImposterCollection;
        private readonly DefaultInvocationSetupMethodImposter _defaultInvocationSetupMethodImposter;
        private readonly _invocationSetupsMethodImposter __invocationSetupsMethodImposter;
        private readonly itMethodImposter _itMethodImposter;
        private readonly invocationHistoryMethodImposter _invocationHistoryMethodImposter;
        private readonly nextSetupMethodImposter _nextSetupMethodImposter;
        private readonly GetNextSetupMethodImposter _getNextSetupMethodImposter;
        private readonly exceptionGeneratorMethodImposter _exceptionGeneratorMethodImposter;
        private readonly callbackMethodImposter _callbackMethodImposter;
        private readonly resultGeneratorMethodImposter _resultGeneratorMethodImposter;
        private readonly argumentsCriteriaMethodImposter _argumentsCriteriaMethodImposter;
        private readonly TExceptionMethodImposter _tExceptionMethodImposter;
        private readonly InitializeOutParametersWithDefaultValuesMethodImposter _initializeOutParametersWithDefaultValuesMethodImposter;
        private readonly CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection = new CollisionWithReturnsMethodInvocationHistoryCollection();
        private readonly CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection = new CollisionWithReturns_1MethodInvocationHistoryCollection();
        private readonly CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection = new CollisionWithReturns_2MethodInvocationHistoryCollection();
        private readonly CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection = new CollisionWithReturns_3MethodInvocationHistoryCollection();
        private readonly CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection = new CollisionWithThrowsMethodInvocationHistoryCollection();
        private readonly CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection = new CollisionWithThrows_1MethodInvocationHistoryCollection();
        private readonly MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection = new MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection();
        private readonly MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection = new MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection();
        private readonly MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection = new MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection();
        private readonly DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection = new DefaultInvocationSetupMethodInvocationHistoryCollection();
        private readonly _invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection = new _invocationSetupsMethodInvocationHistoryCollection();
        private readonly itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection = new itMethodInvocationHistoryCollection();
        private readonly invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection = new invocationHistoryMethodInvocationHistoryCollection();
        private readonly nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection = new nextSetupMethodInvocationHistoryCollection();
        private readonly GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection = new GetNextSetupMethodInvocationHistoryCollection();
        private readonly exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection = new exceptionGeneratorMethodInvocationHistoryCollection();
        private readonly callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection = new callbackMethodInvocationHistoryCollection();
        private readonly resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection = new resultGeneratorMethodInvocationHistoryCollection();
        private readonly argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection = new argumentsCriteriaMethodInvocationHistoryCollection();
        private readonly TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection = new TExceptionMethodInvocationHistoryCollection();
        private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = new InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection();
        public ICollisionWithReturnsMethodImposterBuilder<TValue> CollisionWithReturns<TValue>(Imposter.Abstractions.Arg<TValue> value)
        {
            return new CollisionWithReturnsMethodImposter<TValue>.Builder(_collisionWithReturnsMethodImposterCollection, _collisionWithReturnsMethodInvocationHistoryCollection, new CollisionWithReturnsArgumentsCriteria<TValue>(value));
        }

        public ICollisionWithReturns_1MethodImposterBuilder CollisionWithReturns(Imposter.Abstractions.Arg<int> value)
        {
            return new CollisionWithReturns_1MethodImposter.Builder(_collisionWithReturns_1MethodImposter, _collisionWithReturns_1MethodInvocationHistoryCollection, new CollisionWithReturns_1ArgumentsCriteria(value));
        }

        public ICollisionWithReturns_2MethodImposterBuilder CollisionWithReturns(Imposter.Abstractions.Arg<string> result)
        {
            return new CollisionWithReturns_2MethodImposter.Builder(_collisionWithReturns_2MethodImposter, _collisionWithReturns_2MethodInvocationHistoryCollection, new CollisionWithReturns_2ArgumentsCriteria(result));
        }

        public ICollisionWithReturns_3MethodImposterBuilder CollisionWithReturns(Imposter.Abstractions.Arg<double> matchingSetup)
        {
            return new CollisionWithReturns_3MethodImposter.Builder(_collisionWithReturns_3MethodImposter, _collisionWithReturns_3MethodInvocationHistoryCollection, new CollisionWithReturns_3ArgumentsCriteria(matchingSetup));
        }

        public ICollisionWithThrowsMethodImposterBuilder<TValue> CollisionWithThrows<TValue>(Imposter.Abstractions.Arg<TValue> ex)
        {
            return new CollisionWithThrowsMethodImposter<TValue>.Builder(_collisionWithThrowsMethodImposterCollection, _collisionWithThrowsMethodInvocationHistoryCollection, new CollisionWithThrowsArgumentsCriteria<TValue>(ex));
        }

        public ICollisionWithThrows_1MethodImposterBuilder CollisionWithThrows(Imposter.Abstractions.Arg<global::System.Exception> ex)
        {
            return new CollisionWithThrows_1MethodImposter.Builder(_collisionWithThrows_1MethodImposter, _collisionWithThrows_1MethodInvocationHistoryCollection, new CollisionWithThrows_1ArgumentsCriteria(ex));
        }

        public IMethodWithSameNameDifferentSignatureMethodImposterBuilder MethodWithSameNameDifferentSignature()
        {
            return new MethodWithSameNameDifferentSignatureMethodImposter.Builder(_methodWithSameNameDifferentSignatureMethodImposter, _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection);
        }

        public IMethodWithSameNameDifferentSignature_1MethodImposterBuilder MethodWithSameNameDifferentSignature(Imposter.Abstractions.Arg<int> input)
        {
            return new MethodWithSameNameDifferentSignature_1MethodImposter.Builder(_methodWithSameNameDifferentSignature_1MethodImposter, _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection, new MethodWithSameNameDifferentSignature_1ArgumentsCriteria(input));
        }

        public IMethodWithSameNameDifferentSignature_2MethodImposterBuilder<T> MethodWithSameNameDifferentSignature<T>(Imposter.Abstractions.Arg<T> input)
        {
            return new MethodWithSameNameDifferentSignature_2MethodImposter<T>.Builder(_methodWithSameNameDifferentSignature_2MethodImposterCollection, _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection, new MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T>(input));
        }

        public IDefaultInvocationSetupMethodImposterBuilder DefaultInvocationSetup(Imposter.Abstractions.Arg<int> DefaultInvocationSetup)
        {
            return new DefaultInvocationSetupMethodImposter.Builder(_defaultInvocationSetupMethodImposter, _defaultInvocationSetupMethodInvocationHistoryCollection, new DefaultInvocationSetupArgumentsCriteria(DefaultInvocationSetup));
        }

        public I_invocationSetupsMethodImposterBuilder _invocationSetups(Imposter.Abstractions.Arg<int> _invocationSetups)
        {
            return new _invocationSetupsMethodImposter.Builder(__invocationSetupsMethodImposter, __invocationSetupsMethodInvocationHistoryCollection, new _invocationSetupsArgumentsCriteria(_invocationSetups));
        }

        public IitMethodImposterBuilder it(Imposter.Abstractions.Arg<int> it)
        {
            return new itMethodImposter.Builder(_itMethodImposter, _itMethodInvocationHistoryCollection, new itArgumentsCriteria(it));
        }

        public IinvocationHistoryMethodImposterBuilder invocationHistory(Imposter.Abstractions.Arg<int> invocationHistory)
        {
            return new invocationHistoryMethodImposter.Builder(_invocationHistoryMethodImposter, _invocationHistoryMethodInvocationHistoryCollection, new invocationHistoryArgumentsCriteria(invocationHistory));
        }

        public InextSetupMethodImposterBuilder nextSetup(Imposter.Abstractions.Arg<int> nextSetup)
        {
            return new nextSetupMethodImposter.Builder(_nextSetupMethodImposter, _nextSetupMethodInvocationHistoryCollection, new nextSetupArgumentsCriteria(nextSetup));
        }

        public IGetNextSetupMethodImposterBuilder GetNextSetup(Imposter.Abstractions.Arg<int> GetNextSetup)
        {
            return new GetNextSetupMethodImposter.Builder(_getNextSetupMethodImposter, _getNextSetupMethodInvocationHistoryCollection, new GetNextSetupArgumentsCriteria(GetNextSetup));
        }

        public IexceptionGeneratorMethodImposterBuilder exceptionGenerator(Imposter.Abstractions.Arg<int> exceptionGenerator)
        {
            return new exceptionGeneratorMethodImposter.Builder(_exceptionGeneratorMethodImposter, _exceptionGeneratorMethodInvocationHistoryCollection, new exceptionGeneratorArgumentsCriteria(exceptionGenerator));
        }

        public IcallbackMethodImposterBuilder callback(Imposter.Abstractions.Arg<int> callback)
        {
            return new callbackMethodImposter.Builder(_callbackMethodImposter, _callbackMethodInvocationHistoryCollection, new callbackArgumentsCriteria(callback));
        }

        public IresultGeneratorMethodImposterBuilder resultGenerator(Imposter.Abstractions.Arg<int> resultGenerator)
        {
            return new resultGeneratorMethodImposter.Builder(_resultGeneratorMethodImposter, _resultGeneratorMethodInvocationHistoryCollection, new resultGeneratorArgumentsCriteria(resultGenerator));
        }

        public IargumentsCriteriaMethodImposterBuilder argumentsCriteria(Imposter.Abstractions.Arg<int> argumentsCriteria)
        {
            return new argumentsCriteriaMethodImposter.Builder(_argumentsCriteriaMethodImposter, _argumentsCriteriaMethodInvocationHistoryCollection, new argumentsCriteriaArgumentsCriteria(argumentsCriteria));
        }

        public ITExceptionMethodImposterBuilder TException(Imposter.Abstractions.Arg<int> TException)
        {
            return new TExceptionMethodImposter.Builder(_tExceptionMethodImposter, _tExceptionMethodInvocationHistoryCollection, new TExceptionArgumentsCriteria(TException));
        }

        public IInitializeOutParametersWithDefaultValuesMethodImposterBuilder InitializeOutParametersWithDefaultValues(Imposter.Abstractions.Arg<int> InitializeOutParametersWithDefaultValues)
        {
            return new InitializeOutParametersWithDefaultValuesMethodImposter.Builder(_initializeOutParametersWithDefaultValuesMethodImposter, _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, new InitializeOutParametersWithDefaultValuesArgumentsCriteria(InitializeOutParametersWithDefaultValues));
        }

        private ImposterTargetInstance _imposterInstance;
        public INameCollisionProtectionFeatureSutImposter()
        {
            this._collisionWithReturnsMethodImposterCollection = new CollisionWithReturnsMethodImposterCollection(_collisionWithReturnsMethodInvocationHistoryCollection);
            this._collisionWithReturns_1MethodImposter = new CollisionWithReturns_1MethodImposter(_collisionWithReturns_1MethodInvocationHistoryCollection);
            this._collisionWithReturns_2MethodImposter = new CollisionWithReturns_2MethodImposter(_collisionWithReturns_2MethodInvocationHistoryCollection);
            this._collisionWithReturns_3MethodImposter = new CollisionWithReturns_3MethodImposter(_collisionWithReturns_3MethodInvocationHistoryCollection);
            this._collisionWithThrowsMethodImposterCollection = new CollisionWithThrowsMethodImposterCollection(_collisionWithThrowsMethodInvocationHistoryCollection);
            this._collisionWithThrows_1MethodImposter = new CollisionWithThrows_1MethodImposter(_collisionWithThrows_1MethodInvocationHistoryCollection);
            this._methodWithSameNameDifferentSignatureMethodImposter = new MethodWithSameNameDifferentSignatureMethodImposter(_methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection);
            this._methodWithSameNameDifferentSignature_1MethodImposter = new MethodWithSameNameDifferentSignature_1MethodImposter(_methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection);
            this._methodWithSameNameDifferentSignature_2MethodImposterCollection = new MethodWithSameNameDifferentSignature_2MethodImposterCollection(_methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection);
            this._defaultInvocationSetupMethodImposter = new DefaultInvocationSetupMethodImposter(_defaultInvocationSetupMethodInvocationHistoryCollection);
            this.__invocationSetupsMethodImposter = new _invocationSetupsMethodImposter(__invocationSetupsMethodInvocationHistoryCollection);
            this._itMethodImposter = new itMethodImposter(_itMethodInvocationHistoryCollection);
            this._invocationHistoryMethodImposter = new invocationHistoryMethodImposter(_invocationHistoryMethodInvocationHistoryCollection);
            this._nextSetupMethodImposter = new nextSetupMethodImposter(_nextSetupMethodInvocationHistoryCollection);
            this._getNextSetupMethodImposter = new GetNextSetupMethodImposter(_getNextSetupMethodInvocationHistoryCollection);
            this._exceptionGeneratorMethodImposter = new exceptionGeneratorMethodImposter(_exceptionGeneratorMethodInvocationHistoryCollection);
            this._callbackMethodImposter = new callbackMethodImposter(_callbackMethodInvocationHistoryCollection);
            this._resultGeneratorMethodImposter = new resultGeneratorMethodImposter(_resultGeneratorMethodInvocationHistoryCollection);
            this._argumentsCriteriaMethodImposter = new argumentsCriteriaMethodImposter(_argumentsCriteriaMethodInvocationHistoryCollection);
            this._tExceptionMethodImposter = new TExceptionMethodImposter(_tExceptionMethodInvocationHistoryCollection);
            this._initializeOutParametersWithDefaultValuesMethodImposter = new InitializeOutParametersWithDefaultValuesMethodImposter(_initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.CodeGenerator.Tests.Features.NameCollisionProtection.INameCollisionProtectionFeatureSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.NameCollisionProtection.INameCollisionProtectionFeatureSut>.Instance()
        {
            return _imposterInstance;
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public delegate int CollisionWithReturnsDelegate<TValue>(TValue value);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public delegate void CollisionWithReturnsCallbackDelegate<TValue>(TValue value);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public delegate System.Exception CollisionWithReturnsExceptionGeneratorDelegate<TValue>(TValue value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturnsArguments<TValue>
        {
            public TValue value;
            internal CollisionWithReturnsArguments(TValue value)
            {
                this.value = value;
            }

            public CollisionWithReturnsArguments<TValueTarget> As<TValueTarget>()
            {
                return new CollisionWithReturnsArguments<TValueTarget>(TypeCaster.Cast<TValue, TValueTarget>(value));
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturnsArgumentsCriteria<TValue>
        {
            public Imposter.Abstractions.Arg<TValue> value { get; }

            public CollisionWithReturnsArgumentsCriteria(Imposter.Abstractions.Arg<TValue> value)
            {
                this.value = value;
            }

            public bool Matches(CollisionWithReturnsArguments<TValue> arguments)
            {
                return value.Matches(arguments.value);
            }

            public CollisionWithReturnsArgumentsCriteria<TValueTarget> As<TValueTarget>()
            {
                return new CollisionWithReturnsArgumentsCriteria<TValueTarget>(Imposter.Abstractions.Arg<TValueTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget, TValue>(it, out TValue valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface ICollisionWithReturnsMethodInvocationHistory
        {
            bool Matches<TValueTarget>(CollisionWithReturnsArgumentsCriteria<TValueTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturnsMethodInvocationHistory<TValue> : ICollisionWithReturnsMethodInvocationHistory
        {
            internal CollisionWithReturnsArguments<TValue> Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CollisionWithReturnsMethodInvocationHistory(CollisionWithReturnsArguments<TValue> Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget>(CollisionWithReturnsArgumentsCriteria<TValueTarget> criteria)
            {
                return ((typeof(TValueTarget) == typeof(TValue)) && (typeof(int) == typeof(int))) && criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturnsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturnsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturnsMethodInvocationHistory>();
            internal void Add(ICollisionWithReturnsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue>(CollisionWithReturnsArgumentsCriteria<TValue> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue>(argumentsCriteria));
            }
        }

        internal class CollisionWithReturnsMethodImposterCollection
        {
            private readonly CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection;
            public CollisionWithReturnsMethodImposterCollection(CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection)
            {
                this._collisionWithReturnsMethodInvocationHistoryCollection = _collisionWithReturnsMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturnsMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturnsMethodImposter>();
            internal CollisionWithReturnsMethodImposter<TValue> AddNew<TValue>()
            {
                var imposter = new CollisionWithReturnsMethodImposter<TValue>(_collisionWithReturnsMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal ICollisionWithReturnsMethodImposter<TValue> GetImposterWithMatchingSetup<TValue>(CollisionWithReturnsArguments<TValue> arguments)
            {
                return _imposters.Select(it => it.As<TValue>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue>();
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollisionWithReturnsMethodInvocationsSetup<TValue> : ICollisionWithReturnsMethodInvocationsSetup<TValue>
        {
            internal static CollisionWithReturnsMethodInvocationsSetup<TValue> DefaultInvocationSetup = new CollisionWithReturnsMethodInvocationsSetup<TValue>(new CollisionWithReturnsArgumentsCriteria<TValue>(Imposter.Abstractions.Arg<TValue>.Any()));
            internal CollisionWithReturnsArgumentsCriteria<TValue> ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(TValue value)
            {
                return default;
            }

            public CollisionWithReturnsMethodInvocationsSetup(CollisionWithReturnsArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Returns(CollisionWithReturnsDelegate<TValue> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Returns(int value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    return value_1;
                };
                return this;
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw new TException();
                };
                return this;
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw exception;
                };
                return this;
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.CallBefore(CollisionWithReturnsCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.CallAfter(CollisionWithReturnsCallbackDelegate<TValue> callback)
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

            public int Invoke(TValue value)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(value);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(value);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(value);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal CollisionWithReturnsDelegate<TValue>? ResultGenerator { get; set; }
                internal CollisionWithReturnsCallbackDelegate<TValue>? CallBefore { get; set; }
                internal CollisionWithReturnsCallbackDelegate<TValue>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturnsMethodInvocationsSetup<TValue>
        {
            ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws(System.Exception exception);
            ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator);
            ICollisionWithReturnsMethodInvocationsSetup<TValue> CallBefore(CollisionWithReturnsCallbackDelegate<TValue> callback);
            ICollisionWithReturnsMethodInvocationsSetup<TValue> CallAfter(CollisionWithReturnsCallbackDelegate<TValue> callback);
            ICollisionWithReturnsMethodInvocationsSetup<TValue> Returns(CollisionWithReturnsDelegate<TValue> resultGenerator);
            ICollisionWithReturnsMethodInvocationsSetup<TValue> Returns(int value_1);
        }

        internal interface ICollisionWithReturnsMethodImposter
        {
            ICollisionWithReturnsMethodImposter<TValueTarget>? As<TValueTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface ICollisionWithReturnsMethodImposter<TValue> : ICollisionWithReturnsMethodImposter
        {
            int Invoke(TValue value);
            bool HasMatchingSetup(CollisionWithReturnsArguments<TValue> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturnsMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public interface ICollisionWithReturnsMethodImposterBuilder<TValue> : ICollisionWithReturnsMethodInvocationsSetup<TValue>, CollisionWithReturnsMethodInvocationVerifier<TValue>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturnsMethodImposter<TValue> : ICollisionWithReturnsMethodImposter<TValue>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturnsMethodInvocationsSetup<TValue>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturnsMethodInvocationsSetup<TValue>>();
            private readonly CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection;
            public CollisionWithReturnsMethodImposter(CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection)
            {
                this._collisionWithReturnsMethodInvocationHistoryCollection = _collisionWithReturnsMethodInvocationHistoryCollection;
            }

            ICollisionWithReturnsMethodImposter<TValueTarget>? ICollisionWithReturnsMethodImposter.As<TValueTarget>()
            {
                if (typeof(TValueTarget).IsAssignableTo(typeof(TValue)))
                {
                    return new Adapter<TValueTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget> : ICollisionWithReturnsMethodImposter<TValueTarget>
            {
                private readonly CollisionWithReturnsMethodImposter<TValue> _target;
                public Adapter(CollisionWithReturnsMethodImposter<TValue> target)
                {
                    _target = target;
                }

                public int Invoke(TValueTarget value)
                {
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TValueTarget, TValue>(value));
                    return Imposter.Abstractions.TypeCaster.Cast<int, int>(result);
                }

                public bool HasMatchingSetup(CollisionWithReturnsArguments<TValueTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue>());
                }

                ICollisionWithReturnsMethodImposter<TValueTarget1>? ICollisionWithReturnsMethodImposter.As<TValueTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(CollisionWithReturnsArguments<TValue> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private CollisionWithReturnsMethodInvocationsSetup<TValue>? FindMatchingSetup(CollisionWithReturnsArguments<TValue> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(TValue value)
            {
                var arguments = new CollisionWithReturnsArguments<TValue>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? CollisionWithReturnsMethodInvocationsSetup<TValue>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value);
                    _collisionWithReturnsMethodInvocationHistoryCollection.Add(new CollisionWithReturnsMethodInvocationHistory<TValue>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _collisionWithReturnsMethodInvocationHistoryCollection.Add(new CollisionWithReturnsMethodInvocationHistory<TValue>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollisionWithReturnsMethodImposterBuilder<TValue>
            {
                private readonly CollisionWithReturnsMethodImposterCollection _imposterCollection;
                private readonly CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection;
                private readonly CollisionWithReturnsArgumentsCriteria<TValue> _argumentsCriteria;
                private CollisionWithReturnsMethodInvocationsSetup<TValue>? _existingInvocationSetup;
                public Builder(CollisionWithReturnsMethodImposterCollection _imposterCollection, CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection, CollisionWithReturnsArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._collisionWithReturnsMethodInvocationHistoryCollection = _collisionWithReturnsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ICollisionWithReturnsMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithReturnsMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.CallBefore(CollisionWithReturnsCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.CallAfter(CollisionWithReturnsCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Returns(CollisionWithReturnsDelegate<TValue> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Returns(int value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void CollisionWithReturnsMethodInvocationVerifier<TValue>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collisionWithReturnsMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public delegate int CollisionWithReturns_1Delegate(int value);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public delegate void CollisionWithReturns_1CallbackDelegate(int value);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public delegate System.Exception CollisionWithReturns_1ExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_1Arguments
        {
            public int value;
            internal CollisionWithReturns_1Arguments(int value)
            {
                this.value = value;
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_1ArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> value { get; }

            public CollisionWithReturns_1ArgumentsCriteria(Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(CollisionWithReturns_1Arguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        public interface ICollisionWithReturns_1MethodInvocationHistory
        {
            bool Matches(CollisionWithReturns_1ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_1MethodInvocationHistory : ICollisionWithReturns_1MethodInvocationHistory
        {
            internal CollisionWithReturns_1Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CollisionWithReturns_1MethodInvocationHistory(CollisionWithReturns_1Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(CollisionWithReturns_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_1MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturns_1MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturns_1MethodInvocationHistory>();
            internal void Add(ICollisionWithReturns_1MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CollisionWithReturns_1ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollisionWithReturns_1MethodInvocationsSetup : ICollisionWithReturns_1MethodInvocationsSetup
        {
            internal static CollisionWithReturns_1MethodInvocationsSetup DefaultInvocationSetup = new CollisionWithReturns_1MethodInvocationsSetup(new CollisionWithReturns_1ArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal CollisionWithReturns_1ArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(int value)
            {
                return default;
            }

            public CollisionWithReturns_1MethodInvocationsSetup(CollisionWithReturns_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Returns(CollisionWithReturns_1Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Returns(int value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    return value_1;
                };
                return this;
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw new TException();
                };
                return this;
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw exception;
                };
                return this;
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.CallBefore(CollisionWithReturns_1CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.CallAfter(CollisionWithReturns_1CallbackDelegate callback)
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

            public int Invoke(int value)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(value);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(value);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(value);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal CollisionWithReturns_1Delegate? ResultGenerator { get; set; }
                internal CollisionWithReturns_1CallbackDelegate? CallBefore { get; set; }
                internal CollisionWithReturns_1CallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturns_1MethodInvocationsSetup
        {
            ICollisionWithReturns_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturns_1MethodInvocationsSetup Throws(System.Exception exception);
            ICollisionWithReturns_1MethodInvocationsSetup Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithReturns_1MethodInvocationsSetup CallBefore(CollisionWithReturns_1CallbackDelegate callback);
            ICollisionWithReturns_1MethodInvocationsSetup CallAfter(CollisionWithReturns_1CallbackDelegate callback);
            ICollisionWithReturns_1MethodInvocationsSetup Returns(CollisionWithReturns_1Delegate resultGenerator);
            ICollisionWithReturns_1MethodInvocationsSetup Returns(int value_1);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturns_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public interface ICollisionWithReturns_1MethodImposterBuilder : ICollisionWithReturns_1MethodInvocationsSetup, CollisionWithReturns_1MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_1MethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_1MethodInvocationsSetup>();
            private readonly CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection;
            public CollisionWithReturns_1MethodImposter(CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection)
            {
                this._collisionWithReturns_1MethodInvocationHistoryCollection = _collisionWithReturns_1MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithReturns_1Arguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private CollisionWithReturns_1MethodInvocationsSetup? FindMatchingSetup(CollisionWithReturns_1Arguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int value)
            {
                var arguments = new CollisionWithReturns_1Arguments(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? CollisionWithReturns_1MethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value);
                    _collisionWithReturns_1MethodInvocationHistoryCollection.Add(new CollisionWithReturns_1MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _collisionWithReturns_1MethodInvocationHistoryCollection.Add(new CollisionWithReturns_1MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollisionWithReturns_1MethodImposterBuilder
            {
                private readonly CollisionWithReturns_1MethodImposter _imposter;
                private readonly CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection;
                private readonly CollisionWithReturns_1ArgumentsCriteria _argumentsCriteria;
                private CollisionWithReturns_1MethodInvocationsSetup? _existingInvocationSetup;
                public Builder(CollisionWithReturns_1MethodImposter _imposter, CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection, CollisionWithReturns_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithReturns_1MethodInvocationHistoryCollection = _collisionWithReturns_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ICollisionWithReturns_1MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithReturns_1MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.CallBefore(CollisionWithReturns_1CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.CallAfter(CollisionWithReturns_1CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Returns(CollisionWithReturns_1Delegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Returns(int value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void CollisionWithReturns_1MethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collisionWithReturns_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        public delegate int CollisionWithReturns_2Delegate(string result);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        public delegate void CollisionWithReturns_2CallbackDelegate(string result);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        public delegate System.Exception CollisionWithReturns_2ExceptionGeneratorDelegate(string result);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_2Arguments
        {
            public string result;
            internal CollisionWithReturns_2Arguments(string result)
            {
                this.result = result;
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_2ArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<string> result { get; }

            public CollisionWithReturns_2ArgumentsCriteria(Imposter.Abstractions.Arg<string> result)
            {
                this.result = result;
            }

            public bool Matches(CollisionWithReturns_2Arguments arguments)
            {
                return result.Matches(arguments.result);
            }
        }

        public interface ICollisionWithReturns_2MethodInvocationHistory
        {
            bool Matches(CollisionWithReturns_2ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_2MethodInvocationHistory : ICollisionWithReturns_2MethodInvocationHistory
        {
            internal CollisionWithReturns_2Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CollisionWithReturns_2MethodInvocationHistory(CollisionWithReturns_2Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(CollisionWithReturns_2ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_2MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturns_2MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturns_2MethodInvocationHistory>();
            internal void Add(ICollisionWithReturns_2MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CollisionWithReturns_2ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollisionWithReturns_2MethodInvocationsSetup : ICollisionWithReturns_2MethodInvocationsSetup
        {
            internal static CollisionWithReturns_2MethodInvocationsSetup DefaultInvocationSetup = new CollisionWithReturns_2MethodInvocationsSetup(new CollisionWithReturns_2ArgumentsCriteria(Imposter.Abstractions.Arg<string>.Any()));
            internal CollisionWithReturns_2ArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(string result)
            {
                return default;
            }

            public CollisionWithReturns_2MethodInvocationsSetup(CollisionWithReturns_2ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Returns(CollisionWithReturns_2Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string result) =>
                {
                    return value;
                };
                return this;
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string result) =>
                {
                    throw new TException();
                };
                return this;
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string result) =>
                {
                    throw exception;
                };
                return this;
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Throws(CollisionWithReturns_2ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string result) =>
                {
                    throw exceptionGenerator(result);
                };
                return this;
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.CallBefore(CollisionWithReturns_2CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.CallAfter(CollisionWithReturns_2CallbackDelegate callback)
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

            public int Invoke(string result)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(result);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result_1 = nextSetup.ResultGenerator.Invoke(result);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(result);
                }

                return result_1;
            }

            internal class MethodInvocationSetup
            {
                internal CollisionWithReturns_2Delegate? ResultGenerator { get; set; }
                internal CollisionWithReturns_2CallbackDelegate? CallBefore { get; set; }
                internal CollisionWithReturns_2CallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturns_2MethodInvocationsSetup
        {
            ICollisionWithReturns_2MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturns_2MethodInvocationsSetup Throws(System.Exception exception);
            ICollisionWithReturns_2MethodInvocationsSetup Throws(CollisionWithReturns_2ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithReturns_2MethodInvocationsSetup CallBefore(CollisionWithReturns_2CallbackDelegate callback);
            ICollisionWithReturns_2MethodInvocationsSetup CallAfter(CollisionWithReturns_2CallbackDelegate callback);
            ICollisionWithReturns_2MethodInvocationsSetup Returns(CollisionWithReturns_2Delegate resultGenerator);
            ICollisionWithReturns_2MethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturns_2MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        public interface ICollisionWithReturns_2MethodImposterBuilder : ICollisionWithReturns_2MethodInvocationsSetup, CollisionWithReturns_2MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_2MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_2MethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_2MethodInvocationsSetup>();
            private readonly CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection;
            public CollisionWithReturns_2MethodImposter(CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection)
            {
                this._collisionWithReturns_2MethodInvocationHistoryCollection = _collisionWithReturns_2MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithReturns_2Arguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private CollisionWithReturns_2MethodInvocationsSetup? FindMatchingSetup(CollisionWithReturns_2Arguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(string result)
            {
                var arguments = new CollisionWithReturns_2Arguments(result);
                var matchingSetup = FindMatchingSetup(arguments) ?? CollisionWithReturns_2MethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result_1 = matchingSetup.Invoke(result);
                    _collisionWithReturns_2MethodInvocationHistoryCollection.Add(new CollisionWithReturns_2MethodInvocationHistory(arguments, result_1, default));
                    return result_1;
                }
                catch (System.Exception ex)
                {
                    _collisionWithReturns_2MethodInvocationHistoryCollection.Add(new CollisionWithReturns_2MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollisionWithReturns_2MethodImposterBuilder
            {
                private readonly CollisionWithReturns_2MethodImposter _imposter;
                private readonly CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection;
                private readonly CollisionWithReturns_2ArgumentsCriteria _argumentsCriteria;
                private CollisionWithReturns_2MethodInvocationsSetup? _existingInvocationSetup;
                public Builder(CollisionWithReturns_2MethodImposter _imposter, CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection, CollisionWithReturns_2ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithReturns_2MethodInvocationHistoryCollection = _collisionWithReturns_2MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ICollisionWithReturns_2MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithReturns_2MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Throws(CollisionWithReturns_2ExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.CallBefore(CollisionWithReturns_2CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.CallAfter(CollisionWithReturns_2CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Returns(CollisionWithReturns_2Delegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_2MethodInvocationsSetup ICollisionWithReturns_2MethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void CollisionWithReturns_2MethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collisionWithReturns_2MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        public delegate int CollisionWithReturns_3Delegate(double matchingSetup);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        public delegate void CollisionWithReturns_3CallbackDelegate(double matchingSetup);
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        public delegate System.Exception CollisionWithReturns_3ExceptionGeneratorDelegate(double matchingSetup);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_3Arguments
        {
            public double matchingSetup;
            internal CollisionWithReturns_3Arguments(double matchingSetup)
            {
                this.matchingSetup = matchingSetup;
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_3ArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<double> matchingSetup { get; }

            public CollisionWithReturns_3ArgumentsCriteria(Imposter.Abstractions.Arg<double> matchingSetup)
            {
                this.matchingSetup = matchingSetup;
            }

            public bool Matches(CollisionWithReturns_3Arguments arguments)
            {
                return matchingSetup.Matches(arguments.matchingSetup);
            }
        }

        public interface ICollisionWithReturns_3MethodInvocationHistory
        {
            bool Matches(CollisionWithReturns_3ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_3MethodInvocationHistory : ICollisionWithReturns_3MethodInvocationHistory
        {
            internal CollisionWithReturns_3Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CollisionWithReturns_3MethodInvocationHistory(CollisionWithReturns_3Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(CollisionWithReturns_3ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_3MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturns_3MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithReturns_3MethodInvocationHistory>();
            internal void Add(ICollisionWithReturns_3MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CollisionWithReturns_3ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollisionWithReturns_3MethodInvocationsSetup : ICollisionWithReturns_3MethodInvocationsSetup
        {
            internal static CollisionWithReturns_3MethodInvocationsSetup DefaultInvocationSetup = new CollisionWithReturns_3MethodInvocationsSetup(new CollisionWithReturns_3ArgumentsCriteria(Imposter.Abstractions.Arg<double>.Any()));
            internal CollisionWithReturns_3ArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(double matchingSetup)
            {
                return default;
            }

            public CollisionWithReturns_3MethodInvocationsSetup(CollisionWithReturns_3ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Returns(CollisionWithReturns_3Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (double matchingSetup) =>
                {
                    return value;
                };
                return this;
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (double matchingSetup) =>
                {
                    throw new TException();
                };
                return this;
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (double matchingSetup) =>
                {
                    throw exception;
                };
                return this;
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Throws(CollisionWithReturns_3ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (double matchingSetup) =>
                {
                    throw exceptionGenerator(matchingSetup);
                };
                return this;
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.CallBefore(CollisionWithReturns_3CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.CallAfter(CollisionWithReturns_3CallbackDelegate callback)
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

            public int Invoke(double matchingSetup)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(matchingSetup);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(matchingSetup);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(matchingSetup);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal CollisionWithReturns_3Delegate? ResultGenerator { get; set; }
                internal CollisionWithReturns_3CallbackDelegate? CallBefore { get; set; }
                internal CollisionWithReturns_3CallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturns_3MethodInvocationsSetup
        {
            ICollisionWithReturns_3MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturns_3MethodInvocationsSetup Throws(System.Exception exception);
            ICollisionWithReturns_3MethodInvocationsSetup Throws(CollisionWithReturns_3ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithReturns_3MethodInvocationsSetup CallBefore(CollisionWithReturns_3CallbackDelegate callback);
            ICollisionWithReturns_3MethodInvocationsSetup CallAfter(CollisionWithReturns_3CallbackDelegate callback);
            ICollisionWithReturns_3MethodInvocationsSetup Returns(CollisionWithReturns_3Delegate resultGenerator);
            ICollisionWithReturns_3MethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturns_3MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        public interface ICollisionWithReturns_3MethodImposterBuilder : ICollisionWithReturns_3MethodInvocationsSetup, CollisionWithReturns_3MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_3MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_3MethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_3MethodInvocationsSetup>();
            private readonly CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection;
            public CollisionWithReturns_3MethodImposter(CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection)
            {
                this._collisionWithReturns_3MethodInvocationHistoryCollection = _collisionWithReturns_3MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithReturns_3Arguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private CollisionWithReturns_3MethodInvocationsSetup? FindMatchingSetup(CollisionWithReturns_3Arguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(double matchingSetup)
            {
                var arguments = new CollisionWithReturns_3Arguments(matchingSetup);
                var matchingSetup_1 = FindMatchingSetup(arguments) ?? CollisionWithReturns_3MethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup_1.Invoke(matchingSetup);
                    _collisionWithReturns_3MethodInvocationHistoryCollection.Add(new CollisionWithReturns_3MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _collisionWithReturns_3MethodInvocationHistoryCollection.Add(new CollisionWithReturns_3MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollisionWithReturns_3MethodImposterBuilder
            {
                private readonly CollisionWithReturns_3MethodImposter _imposter;
                private readonly CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection;
                private readonly CollisionWithReturns_3ArgumentsCriteria _argumentsCriteria;
                private CollisionWithReturns_3MethodInvocationsSetup? _existingInvocationSetup;
                public Builder(CollisionWithReturns_3MethodImposter _imposter, CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection, CollisionWithReturns_3ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithReturns_3MethodInvocationHistoryCollection = _collisionWithReturns_3MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ICollisionWithReturns_3MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithReturns_3MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Throws(CollisionWithReturns_3ExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.CallBefore(CollisionWithReturns_3CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.CallAfter(CollisionWithReturns_3CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Returns(CollisionWithReturns_3Delegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_3MethodInvocationsSetup ICollisionWithReturns_3MethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void CollisionWithReturns_3MethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collisionWithReturns_3MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public delegate int CollisionWithThrowsDelegate<TValue>(TValue ex);
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public delegate void CollisionWithThrowsCallbackDelegate<TValue>(TValue ex);
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public delegate System.Exception CollisionWithThrowsExceptionGeneratorDelegate<TValue>(TValue ex);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithThrowsArguments<TValue>
        {
            public TValue ex;
            internal CollisionWithThrowsArguments(TValue ex)
            {
                this.ex = ex;
            }

            public CollisionWithThrowsArguments<TValueTarget> As<TValueTarget>()
            {
                return new CollisionWithThrowsArguments<TValueTarget>(TypeCaster.Cast<TValue, TValueTarget>(ex));
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithThrowsArgumentsCriteria<TValue>
        {
            public Imposter.Abstractions.Arg<TValue> ex { get; }

            public CollisionWithThrowsArgumentsCriteria(Imposter.Abstractions.Arg<TValue> ex)
            {
                this.ex = ex;
            }

            public bool Matches(CollisionWithThrowsArguments<TValue> arguments)
            {
                return ex.Matches(arguments.ex);
            }

            public CollisionWithThrowsArgumentsCriteria<TValueTarget> As<TValueTarget>()
            {
                return new CollisionWithThrowsArgumentsCriteria<TValueTarget>(Imposter.Abstractions.Arg<TValueTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget, TValue>(it, out TValue exTarget) && ex.Matches(exTarget)));
            }
        }

        public interface ICollisionWithThrowsMethodInvocationHistory
        {
            bool Matches<TValueTarget>(CollisionWithThrowsArgumentsCriteria<TValueTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrowsMethodInvocationHistory<TValue> : ICollisionWithThrowsMethodInvocationHistory
        {
            internal CollisionWithThrowsArguments<TValue> Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CollisionWithThrowsMethodInvocationHistory(CollisionWithThrowsArguments<TValue> Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget>(CollisionWithThrowsArgumentsCriteria<TValueTarget> criteria)
            {
                return ((typeof(TValueTarget) == typeof(TValue)) && (typeof(int) == typeof(int))) && criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrowsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithThrowsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithThrowsMethodInvocationHistory>();
            internal void Add(ICollisionWithThrowsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue>(CollisionWithThrowsArgumentsCriteria<TValue> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue>(argumentsCriteria));
            }
        }

        internal class CollisionWithThrowsMethodImposterCollection
        {
            private readonly CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection;
            public CollisionWithThrowsMethodImposterCollection(CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection)
            {
                this._collisionWithThrowsMethodInvocationHistoryCollection = _collisionWithThrowsMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithThrowsMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithThrowsMethodImposter>();
            internal CollisionWithThrowsMethodImposter<TValue> AddNew<TValue>()
            {
                var imposter = new CollisionWithThrowsMethodImposter<TValue>(_collisionWithThrowsMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal ICollisionWithThrowsMethodImposter<TValue> GetImposterWithMatchingSetup<TValue>(CollisionWithThrowsArguments<TValue> arguments)
            {
                return _imposters.Select(it => it.As<TValue>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue>();
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollisionWithThrowsMethodInvocationsSetup<TValue> : ICollisionWithThrowsMethodInvocationsSetup<TValue>
        {
            internal static CollisionWithThrowsMethodInvocationsSetup<TValue> DefaultInvocationSetup = new CollisionWithThrowsMethodInvocationsSetup<TValue>(new CollisionWithThrowsArgumentsCriteria<TValue>(Imposter.Abstractions.Arg<TValue>.Any()));
            internal CollisionWithThrowsArgumentsCriteria<TValue> ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(TValue ex)
            {
                return default;
            }

            public CollisionWithThrowsMethodInvocationsSetup(CollisionWithThrowsArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Returns(CollisionWithThrowsDelegate<TValue> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    return value;
                };
                return this;
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    throw new TException();
                };
                return this;
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    throw exception;
                };
                return this;
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    throw exceptionGenerator(ex);
                };
                return this;
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.CallBefore(CollisionWithThrowsCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.CallAfter(CollisionWithThrowsCallbackDelegate<TValue> callback)
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

            public int Invoke(TValue ex)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(ex);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(ex);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(ex);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal CollisionWithThrowsDelegate<TValue>? ResultGenerator { get; set; }
                internal CollisionWithThrowsCallbackDelegate<TValue>? CallBefore { get; set; }
                internal CollisionWithThrowsCallbackDelegate<TValue>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithThrowsMethodInvocationsSetup<TValue>
        {
            ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws(System.Exception exception);
            ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator);
            ICollisionWithThrowsMethodInvocationsSetup<TValue> CallBefore(CollisionWithThrowsCallbackDelegate<TValue> callback);
            ICollisionWithThrowsMethodInvocationsSetup<TValue> CallAfter(CollisionWithThrowsCallbackDelegate<TValue> callback);
            ICollisionWithThrowsMethodInvocationsSetup<TValue> Returns(CollisionWithThrowsDelegate<TValue> resultGenerator);
            ICollisionWithThrowsMethodInvocationsSetup<TValue> Returns(int value);
        }

        internal interface ICollisionWithThrowsMethodImposter
        {
            ICollisionWithThrowsMethodImposter<TValueTarget>? As<TValueTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface ICollisionWithThrowsMethodImposter<TValue> : ICollisionWithThrowsMethodImposter
        {
            int Invoke(TValue ex);
            bool HasMatchingSetup(CollisionWithThrowsArguments<TValue> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithThrowsMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public interface ICollisionWithThrowsMethodImposterBuilder<TValue> : ICollisionWithThrowsMethodInvocationsSetup<TValue>, CollisionWithThrowsMethodInvocationVerifier<TValue>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrowsMethodImposter<TValue> : ICollisionWithThrowsMethodImposter<TValue>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithThrowsMethodInvocationsSetup<TValue>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithThrowsMethodInvocationsSetup<TValue>>();
            private readonly CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection;
            public CollisionWithThrowsMethodImposter(CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection)
            {
                this._collisionWithThrowsMethodInvocationHistoryCollection = _collisionWithThrowsMethodInvocationHistoryCollection;
            }

            ICollisionWithThrowsMethodImposter<TValueTarget>? ICollisionWithThrowsMethodImposter.As<TValueTarget>()
            {
                if (typeof(TValueTarget).IsAssignableTo(typeof(TValue)))
                {
                    return new Adapter<TValueTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget> : ICollisionWithThrowsMethodImposter<TValueTarget>
            {
                private readonly CollisionWithThrowsMethodImposter<TValue> _target;
                public Adapter(CollisionWithThrowsMethodImposter<TValue> target)
                {
                    _target = target;
                }

                public int Invoke(TValueTarget ex)
                {
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TValueTarget, TValue>(ex));
                    return Imposter.Abstractions.TypeCaster.Cast<int, int>(result);
                }

                public bool HasMatchingSetup(CollisionWithThrowsArguments<TValueTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue>());
                }

                ICollisionWithThrowsMethodImposter<TValueTarget1>? ICollisionWithThrowsMethodImposter.As<TValueTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(CollisionWithThrowsArguments<TValue> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private CollisionWithThrowsMethodInvocationsSetup<TValue>? FindMatchingSetup(CollisionWithThrowsArguments<TValue> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(TValue ex)
            {
                var arguments = new CollisionWithThrowsArguments<TValue>(ex);
                var matchingSetup = FindMatchingSetup(arguments) ?? CollisionWithThrowsMethodInvocationsSetup<TValue>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ex);
                    _collisionWithThrowsMethodInvocationHistoryCollection.Add(new CollisionWithThrowsMethodInvocationHistory<TValue>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex_1)
                {
                    _collisionWithThrowsMethodInvocationHistoryCollection.Add(new CollisionWithThrowsMethodInvocationHistory<TValue>(arguments, default, ex_1));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollisionWithThrowsMethodImposterBuilder<TValue>
            {
                private readonly CollisionWithThrowsMethodImposterCollection _imposterCollection;
                private readonly CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection;
                private readonly CollisionWithThrowsArgumentsCriteria<TValue> _argumentsCriteria;
                private CollisionWithThrowsMethodInvocationsSetup<TValue>? _existingInvocationSetup;
                public Builder(CollisionWithThrowsMethodImposterCollection _imposterCollection, CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection, CollisionWithThrowsArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._collisionWithThrowsMethodInvocationHistoryCollection = _collisionWithThrowsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ICollisionWithThrowsMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithThrowsMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.CallBefore(CollisionWithThrowsCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.CallAfter(CollisionWithThrowsCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Returns(CollisionWithThrowsDelegate<TValue> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void CollisionWithThrowsMethodInvocationVerifier<TValue>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collisionWithThrowsMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public delegate int CollisionWithThrows_1Delegate(global::System.Exception ex);
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public delegate void CollisionWithThrows_1CallbackDelegate(global::System.Exception ex);
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public delegate System.Exception CollisionWithThrows_1ExceptionGeneratorDelegate(global::System.Exception ex);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithThrows_1Arguments
        {
            public global::System.Exception ex;
            internal CollisionWithThrows_1Arguments(global::System.Exception ex)
            {
                this.ex = ex;
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithThrows_1ArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<global::System.Exception> ex { get; }

            public CollisionWithThrows_1ArgumentsCriteria(Imposter.Abstractions.Arg<global::System.Exception> ex)
            {
                this.ex = ex;
            }

            public bool Matches(CollisionWithThrows_1Arguments arguments)
            {
                return ex.Matches(arguments.ex);
            }
        }

        public interface ICollisionWithThrows_1MethodInvocationHistory
        {
            bool Matches(CollisionWithThrows_1ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrows_1MethodInvocationHistory : ICollisionWithThrows_1MethodInvocationHistory
        {
            internal CollisionWithThrows_1Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CollisionWithThrows_1MethodInvocationHistory(CollisionWithThrows_1Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(CollisionWithThrows_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrows_1MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollisionWithThrows_1MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollisionWithThrows_1MethodInvocationHistory>();
            internal void Add(ICollisionWithThrows_1MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CollisionWithThrows_1ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollisionWithThrows_1MethodInvocationsSetup : ICollisionWithThrows_1MethodInvocationsSetup
        {
            internal static CollisionWithThrows_1MethodInvocationsSetup DefaultInvocationSetup = new CollisionWithThrows_1MethodInvocationsSetup(new CollisionWithThrows_1ArgumentsCriteria(Imposter.Abstractions.Arg<global::System.Exception>.Any()));
            internal CollisionWithThrows_1ArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(global::System.Exception ex)
            {
                return default;
            }

            public CollisionWithThrows_1MethodInvocationsSetup(CollisionWithThrows_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Returns(CollisionWithThrows_1Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    return value;
                };
                return this;
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    throw new TException();
                };
                return this;
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    throw exception;
                };
                return this;
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    throw exceptionGenerator(ex);
                };
                return this;
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.CallBefore(CollisionWithThrows_1CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.CallAfter(CollisionWithThrows_1CallbackDelegate callback)
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

            public int Invoke(global::System.Exception ex)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(ex);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(ex);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(ex);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal CollisionWithThrows_1Delegate? ResultGenerator { get; set; }
                internal CollisionWithThrows_1CallbackDelegate? CallBefore { get; set; }
                internal CollisionWithThrows_1CallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithThrows_1MethodInvocationsSetup
        {
            ICollisionWithThrows_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            ICollisionWithThrows_1MethodInvocationsSetup Throws(System.Exception exception);
            ICollisionWithThrows_1MethodInvocationsSetup Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithThrows_1MethodInvocationsSetup CallBefore(CollisionWithThrows_1CallbackDelegate callback);
            ICollisionWithThrows_1MethodInvocationsSetup CallAfter(CollisionWithThrows_1CallbackDelegate callback);
            ICollisionWithThrows_1MethodInvocationsSetup Returns(CollisionWithThrows_1Delegate resultGenerator);
            ICollisionWithThrows_1MethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithThrows_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public interface ICollisionWithThrows_1MethodImposterBuilder : ICollisionWithThrows_1MethodInvocationsSetup, CollisionWithThrows_1MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrows_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithThrows_1MethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithThrows_1MethodInvocationsSetup>();
            private readonly CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection;
            public CollisionWithThrows_1MethodImposter(CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection)
            {
                this._collisionWithThrows_1MethodInvocationHistoryCollection = _collisionWithThrows_1MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithThrows_1Arguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private CollisionWithThrows_1MethodInvocationsSetup? FindMatchingSetup(CollisionWithThrows_1Arguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(global::System.Exception ex)
            {
                var arguments = new CollisionWithThrows_1Arguments(ex);
                var matchingSetup = FindMatchingSetup(arguments) ?? CollisionWithThrows_1MethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ex);
                    _collisionWithThrows_1MethodInvocationHistoryCollection.Add(new CollisionWithThrows_1MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex_1)
                {
                    _collisionWithThrows_1MethodInvocationHistoryCollection.Add(new CollisionWithThrows_1MethodInvocationHistory(arguments, default, ex_1));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollisionWithThrows_1MethodImposterBuilder
            {
                private readonly CollisionWithThrows_1MethodImposter _imposter;
                private readonly CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection;
                private readonly CollisionWithThrows_1ArgumentsCriteria _argumentsCriteria;
                private CollisionWithThrows_1MethodInvocationsSetup? _existingInvocationSetup;
                public Builder(CollisionWithThrows_1MethodImposter _imposter, CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection, CollisionWithThrows_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithThrows_1MethodInvocationHistoryCollection = _collisionWithThrows_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ICollisionWithThrows_1MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithThrows_1MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.CallBefore(CollisionWithThrows_1CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.CallAfter(CollisionWithThrows_1CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Returns(CollisionWithThrows_1Delegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void CollisionWithThrows_1MethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collisionWithThrows_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public delegate int MethodWithSameNameDifferentSignatureDelegate();
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public delegate void MethodWithSameNameDifferentSignatureCallbackDelegate();
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public delegate System.Exception MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate();
        public interface IMethodWithSameNameDifferentSignatureMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignatureMethodInvocationHistory : IMethodWithSameNameDifferentSignatureMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public MethodWithSameNameDifferentSignatureMethodInvocationHistory(int Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignatureMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignatureMethodInvocationHistory>();
            internal void Add(IMethodWithSameNameDifferentSignatureMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MethodWithSameNameDifferentSignatureMethodInvocationsSetup : IMethodWithSameNameDifferentSignatureMethodInvocationsSetup
        {
            internal static MethodWithSameNameDifferentSignatureMethodInvocationsSetup DefaultInvocationSetup = new MethodWithSameNameDifferentSignatureMethodInvocationsSetup();
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

            internal static int DefaultResultGenerator()
            {
                return default;
            }

            public MethodWithSameNameDifferentSignatureMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    return value;
                };
                return this;
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exception;
                };
                return this;
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.CallBefore(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.CallAfter(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
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

            public int Invoke()
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore();
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke();
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter();
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal MethodWithSameNameDifferentSignatureDelegate? ResultGenerator { get; set; }
                internal MethodWithSameNameDifferentSignatureCallbackDelegate? CallBefore { get; set; }
                internal MethodWithSameNameDifferentSignatureCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodWithSameNameDifferentSignatureMethodInvocationsSetup
        {
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws(System.Exception exception);
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator);
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup CallBefore(MethodWithSameNameDifferentSignatureCallbackDelegate callback);
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup CallAfter(MethodWithSameNameDifferentSignatureCallbackDelegate callback);
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator);
            IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodWithSameNameDifferentSignatureMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public interface IMethodWithSameNameDifferentSignatureMethodImposterBuilder : IMethodWithSameNameDifferentSignatureMethodInvocationsSetup, MethodWithSameNameDifferentSignatureMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignatureMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignatureMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignatureMethodInvocationsSetup>();
            private readonly MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
            public MethodWithSameNameDifferentSignatureMethodImposter(MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection)
            {
                this._methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection = _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private MethodWithSameNameDifferentSignatureMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? MethodWithSameNameDifferentSignatureMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke();
                    _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection.Add(new MethodWithSameNameDifferentSignatureMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection.Add(new MethodWithSameNameDifferentSignatureMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMethodWithSameNameDifferentSignatureMethodImposterBuilder
            {
                private readonly MethodWithSameNameDifferentSignatureMethodImposter _imposter;
                private readonly MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
                private MethodWithSameNameDifferentSignatureMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(MethodWithSameNameDifferentSignatureMethodImposter _imposter, MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection = _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
                }

                private IMethodWithSameNameDifferentSignatureMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MethodWithSameNameDifferentSignatureMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.CallBefore(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.CallAfter(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void MethodWithSameNameDifferentSignatureMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public delegate int MethodWithSameNameDifferentSignature_1Delegate(int input);
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public delegate void MethodWithSameNameDifferentSignature_1CallbackDelegate(int input);
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public delegate System.Exception MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate(int input);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodWithSameNameDifferentSignature_1Arguments
        {
            public int input;
            internal MethodWithSameNameDifferentSignature_1Arguments(int input)
            {
                this.input = input;
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodWithSameNameDifferentSignature_1ArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> input { get; }

            public MethodWithSameNameDifferentSignature_1ArgumentsCriteria(Imposter.Abstractions.Arg<int> input)
            {
                this.input = input;
            }

            public bool Matches(MethodWithSameNameDifferentSignature_1Arguments arguments)
            {
                return input.Matches(arguments.input);
            }
        }

        public interface IMethodWithSameNameDifferentSignature_1MethodInvocationHistory
        {
            bool Matches(MethodWithSameNameDifferentSignature_1ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_1MethodInvocationHistory : IMethodWithSameNameDifferentSignature_1MethodInvocationHistory
        {
            internal MethodWithSameNameDifferentSignature_1Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public MethodWithSameNameDifferentSignature_1MethodInvocationHistory(MethodWithSameNameDifferentSignature_1Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(MethodWithSameNameDifferentSignature_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignature_1MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignature_1MethodInvocationHistory>();
            internal void Add(IMethodWithSameNameDifferentSignature_1MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(MethodWithSameNameDifferentSignature_1ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MethodWithSameNameDifferentSignature_1MethodInvocationsSetup : IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup
        {
            internal static MethodWithSameNameDifferentSignature_1MethodInvocationsSetup DefaultInvocationSetup = new MethodWithSameNameDifferentSignature_1MethodInvocationsSetup(new MethodWithSameNameDifferentSignature_1ArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal MethodWithSameNameDifferentSignature_1ArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(int input)
            {
                return default;
            }

            public MethodWithSameNameDifferentSignature_1MethodInvocationsSetup(MethodWithSameNameDifferentSignature_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    return value;
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw new TException();
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw exception;
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw exceptionGenerator(input);
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.CallBefore(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.CallAfter(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
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

            public int Invoke(int input)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(input);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(input);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(input);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal MethodWithSameNameDifferentSignature_1Delegate? ResultGenerator { get; set; }
                internal MethodWithSameNameDifferentSignature_1CallbackDelegate? CallBefore { get; set; }
                internal MethodWithSameNameDifferentSignature_1CallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup
        {
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws(System.Exception exception);
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator);
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup CallBefore(MethodWithSameNameDifferentSignature_1CallbackDelegate callback);
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup CallAfter(MethodWithSameNameDifferentSignature_1CallbackDelegate callback);
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator);
            IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodWithSameNameDifferentSignature_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public interface IMethodWithSameNameDifferentSignature_1MethodImposterBuilder : IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup, MethodWithSameNameDifferentSignature_1MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_1MethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_1MethodInvocationsSetup>();
            private readonly MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
            public MethodWithSameNameDifferentSignature_1MethodImposter(MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection)
            {
                this._methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(MethodWithSameNameDifferentSignature_1Arguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private MethodWithSameNameDifferentSignature_1MethodInvocationsSetup? FindMatchingSetup(MethodWithSameNameDifferentSignature_1Arguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int input)
            {
                var arguments = new MethodWithSameNameDifferentSignature_1Arguments(input);
                var matchingSetup = FindMatchingSetup(arguments) ?? MethodWithSameNameDifferentSignature_1MethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(input);
                    _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection.Add(new MethodWithSameNameDifferentSignature_1MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection.Add(new MethodWithSameNameDifferentSignature_1MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMethodWithSameNameDifferentSignature_1MethodImposterBuilder
            {
                private readonly MethodWithSameNameDifferentSignature_1MethodImposter _imposter;
                private readonly MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
                private readonly MethodWithSameNameDifferentSignature_1ArgumentsCriteria _argumentsCriteria;
                private MethodWithSameNameDifferentSignature_1MethodInvocationsSetup? _existingInvocationSetup;
                public Builder(MethodWithSameNameDifferentSignature_1MethodImposter _imposter, MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection, MethodWithSameNameDifferentSignature_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MethodWithSameNameDifferentSignature_1MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.CallBefore(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.CallAfter(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void MethodWithSameNameDifferentSignature_1MethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public delegate int MethodWithSameNameDifferentSignature_2Delegate<T>(T input);
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public delegate void MethodWithSameNameDifferentSignature_2CallbackDelegate<T>(T input);
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public delegate System.Exception MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T>(T input);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodWithSameNameDifferentSignature_2Arguments<T>
        {
            public T input;
            internal MethodWithSameNameDifferentSignature_2Arguments(T input)
            {
                this.input = input;
            }

            public MethodWithSameNameDifferentSignature_2Arguments<TTarget> As<TTarget>()
            {
                return new MethodWithSameNameDifferentSignature_2Arguments<TTarget>(TypeCaster.Cast<T, TTarget>(input));
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T>
        {
            public Imposter.Abstractions.Arg<T> input { get; }

            public MethodWithSameNameDifferentSignature_2ArgumentsCriteria(Imposter.Abstractions.Arg<T> input)
            {
                this.input = input;
            }

            public bool Matches(MethodWithSameNameDifferentSignature_2Arguments<T> arguments)
            {
                return input.Matches(arguments.input);
            }

            public MethodWithSameNameDifferentSignature_2ArgumentsCriteria<TTarget> As<TTarget>()
            {
                return new MethodWithSameNameDifferentSignature_2ArgumentsCriteria<TTarget>(Imposter.Abstractions.Arg<TTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TTarget, T>(it, out T inputTarget) && input.Matches(inputTarget)));
            }
        }

        public interface IMethodWithSameNameDifferentSignature_2MethodInvocationHistory
        {
            bool Matches<TTarget>(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<TTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_2MethodInvocationHistory<T> : IMethodWithSameNameDifferentSignature_2MethodInvocationHistory
        {
            internal MethodWithSameNameDifferentSignature_2Arguments<T> Arguments;
            internal int Result;
            internal System.Exception Exception;
            public MethodWithSameNameDifferentSignature_2MethodInvocationHistory(MethodWithSameNameDifferentSignature_2Arguments<T> Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TTarget>(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<TTarget> criteria)
            {
                return ((typeof(TTarget) == typeof(T)) && (typeof(int) == typeof(int))) && criteria.As<T>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodInvocationHistory>();
            internal void Add(IMethodWithSameNameDifferentSignature_2MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<T>(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<T>(argumentsCriteria));
            }
        }

        internal class MethodWithSameNameDifferentSignature_2MethodImposterCollection
        {
            private readonly MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
            public MethodWithSameNameDifferentSignature_2MethodImposterCollection(MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection)
            {
                this._methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodImposter>();
            internal MethodWithSameNameDifferentSignature_2MethodImposter<T> AddNew<T>()
            {
                var imposter = new MethodWithSameNameDifferentSignature_2MethodImposter<T>(_methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IMethodWithSameNameDifferentSignature_2MethodImposter<T> GetImposterWithMatchingSetup<T>(MethodWithSameNameDifferentSignature_2Arguments<T> arguments)
            {
                return _imposters.Select(it => it.As<T>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<T>();
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> : IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>
        {
            internal static MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> DefaultInvocationSetup = new MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>(new MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T>(Imposter.Abstractions.Arg<T>.Any()));
            internal MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(T input)
            {
                return default;
            }

            public MethodWithSameNameDifferentSignature_2MethodInvocationsSetup(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    return value;
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    throw new TException();
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    throw exception;
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    throw exceptionGenerator(input);
                };
                return this;
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.CallBefore(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.CallAfter(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
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

            public int Invoke(T input)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(input);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(input);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(input);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal MethodWithSameNameDifferentSignature_2Delegate<T>? ResultGenerator { get; set; }
                internal MethodWithSameNameDifferentSignature_2CallbackDelegate<T>? CallBefore { get; set; }
                internal MethodWithSameNameDifferentSignature_2CallbackDelegate<T>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>
        {
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws<TException>()
                where TException : Exception, new();
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws(System.Exception exception);
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator);
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> CallBefore(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback);
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> CallAfter(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback);
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator);
            IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Returns(int value);
        }

        internal interface IMethodWithSameNameDifferentSignature_2MethodImposter
        {
            IMethodWithSameNameDifferentSignature_2MethodImposter<TTarget>? As<TTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IMethodWithSameNameDifferentSignature_2MethodImposter<T> : IMethodWithSameNameDifferentSignature_2MethodImposter
        {
            int Invoke(T input);
            bool HasMatchingSetup(MethodWithSameNameDifferentSignature_2Arguments<T> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public interface IMethodWithSameNameDifferentSignature_2MethodImposterBuilder<T> : IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>, MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_2MethodImposter<T> : IMethodWithSameNameDifferentSignature_2MethodImposter<T>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>>();
            private readonly MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
            public MethodWithSameNameDifferentSignature_2MethodImposter(MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection)
            {
                this._methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
            }

            IMethodWithSameNameDifferentSignature_2MethodImposter<TTarget>? IMethodWithSameNameDifferentSignature_2MethodImposter.As<TTarget>()
            {
                if (typeof(TTarget).IsAssignableTo(typeof(T)))
                {
                    return new Adapter<TTarget>(this);
                }

                return null;
            }

            private class Adapter<TTarget> : IMethodWithSameNameDifferentSignature_2MethodImposter<TTarget>
            {
                private readonly MethodWithSameNameDifferentSignature_2MethodImposter<T> _target;
                public Adapter(MethodWithSameNameDifferentSignature_2MethodImposter<T> target)
                {
                    _target = target;
                }

                public int Invoke(TTarget input)
                {
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TTarget, T>(input));
                    return Imposter.Abstractions.TypeCaster.Cast<int, int>(result);
                }

                public bool HasMatchingSetup(MethodWithSameNameDifferentSignature_2Arguments<TTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<T>());
                }

                IMethodWithSameNameDifferentSignature_2MethodImposter<TTarget1>? IMethodWithSameNameDifferentSignature_2MethodImposter.As<TTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(MethodWithSameNameDifferentSignature_2Arguments<T> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>? FindMatchingSetup(MethodWithSameNameDifferentSignature_2Arguments<T> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(T input)
            {
                var arguments = new MethodWithSameNameDifferentSignature_2Arguments<T>(input);
                var matchingSetup = FindMatchingSetup(arguments) ?? MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(input);
                    _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection.Add(new MethodWithSameNameDifferentSignature_2MethodInvocationHistory<T>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection.Add(new MethodWithSameNameDifferentSignature_2MethodInvocationHistory<T>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMethodWithSameNameDifferentSignature_2MethodImposterBuilder<T>
            {
                private readonly MethodWithSameNameDifferentSignature_2MethodImposterCollection _imposterCollection;
                private readonly MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
                private readonly MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> _argumentsCriteria;
                private MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>? _existingInvocationSetup;
                public Builder(MethodWithSameNameDifferentSignature_2MethodImposterCollection _imposterCollection, MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection, MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>(_argumentsCriteria);
                        _imposterCollection.AddNew<T>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.CallBefore(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.CallAfter(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection.Count<T>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        public delegate void DefaultInvocationSetupDelegate(int DefaultInvocationSetup);
        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        public delegate void DefaultInvocationSetupCallbackDelegate(int DefaultInvocationSetup);
        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        public delegate System.Exception DefaultInvocationSetupExceptionGeneratorDelegate(int DefaultInvocationSetup);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class DefaultInvocationSetupArguments
        {
            public int DefaultInvocationSetup;
            internal DefaultInvocationSetupArguments(int DefaultInvocationSetup)
            {
                this.DefaultInvocationSetup = DefaultInvocationSetup;
            }
        }

        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class DefaultInvocationSetupArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> DefaultInvocationSetup { get; }

            public DefaultInvocationSetupArgumentsCriteria(Imposter.Abstractions.Arg<int> DefaultInvocationSetup)
            {
                this.DefaultInvocationSetup = DefaultInvocationSetup;
            }

            public bool Matches(DefaultInvocationSetupArguments arguments)
            {
                return DefaultInvocationSetup.Matches(arguments.DefaultInvocationSetup);
            }
        }

        public interface IDefaultInvocationSetupMethodInvocationHistory
        {
            bool Matches(DefaultInvocationSetupArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultInvocationSetupMethodInvocationHistory : IDefaultInvocationSetupMethodInvocationHistory
        {
            internal DefaultInvocationSetupArguments Arguments;
            internal System.Exception Exception;
            public DefaultInvocationSetupMethodInvocationHistory(DefaultInvocationSetupArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(DefaultInvocationSetupArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultInvocationSetupMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDefaultInvocationSetupMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDefaultInvocationSetupMethodInvocationHistory>();
            internal void Add(IDefaultInvocationSetupMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(DefaultInvocationSetupArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class DefaultInvocationSetupMethodInvocationsSetup : IDefaultInvocationSetupMethodInvocationsSetup
        {
            internal static DefaultInvocationSetupMethodInvocationsSetup DefaultInvocationSetup = new DefaultInvocationSetupMethodInvocationsSetup(new DefaultInvocationSetupArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal DefaultInvocationSetupArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int DefaultInvocationSetup)
            {
            }

            public DefaultInvocationSetupMethodInvocationsSetup(DefaultInvocationSetupArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int DefaultInvocationSetup) =>
                {
                    throw new TException();
                };
                return this;
            }

            IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int DefaultInvocationSetup) =>
                {
                    throw exception;
                };
                return this;
            }

            IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.Throws(DefaultInvocationSetupExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int DefaultInvocationSetup) =>
                {
                    throw exceptionGenerator(DefaultInvocationSetup);
                };
                return this;
            }

            IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.CallBefore(DefaultInvocationSetupCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.CallAfter(DefaultInvocationSetupCallbackDelegate callback)
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

            public void Invoke(int DefaultInvocationSetup)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(DefaultInvocationSetup);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(DefaultInvocationSetup);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(DefaultInvocationSetup);
                };
            }

            internal class MethodInvocationSetup
            {
                internal DefaultInvocationSetupDelegate? ResultGenerator { get; set; }
                internal DefaultInvocationSetupCallbackDelegate? CallBefore { get; set; }
                internal DefaultInvocationSetupCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultInvocationSetupMethodInvocationsSetup
        {
            IDefaultInvocationSetupMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IDefaultInvocationSetupMethodInvocationsSetup Throws(System.Exception exception);
            IDefaultInvocationSetupMethodInvocationsSetup Throws(DefaultInvocationSetupExceptionGeneratorDelegate exceptionGenerator);
            IDefaultInvocationSetupMethodInvocationsSetup CallBefore(DefaultInvocationSetupCallbackDelegate callback);
            IDefaultInvocationSetupMethodInvocationsSetup CallAfter(DefaultInvocationSetupCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface DefaultInvocationSetupMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        public interface IDefaultInvocationSetupMethodImposterBuilder : IDefaultInvocationSetupMethodInvocationsSetup, DefaultInvocationSetupMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultInvocationSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<DefaultInvocationSetupMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<DefaultInvocationSetupMethodInvocationsSetup>();
            private readonly DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection;
            public DefaultInvocationSetupMethodImposter(DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection)
            {
                this._defaultInvocationSetupMethodInvocationHistoryCollection = _defaultInvocationSetupMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(DefaultInvocationSetupArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private DefaultInvocationSetupMethodInvocationsSetup? FindMatchingSetup(DefaultInvocationSetupArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int DefaultInvocationSetup)
            {
                var arguments = new DefaultInvocationSetupArguments(DefaultInvocationSetup);
                var matchingSetup = FindMatchingSetup(arguments) ?? DefaultInvocationSetupMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(DefaultInvocationSetup);
                    _defaultInvocationSetupMethodInvocationHistoryCollection.Add(new DefaultInvocationSetupMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _defaultInvocationSetupMethodInvocationHistoryCollection.Add(new DefaultInvocationSetupMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDefaultInvocationSetupMethodImposterBuilder
            {
                private readonly DefaultInvocationSetupMethodImposter _imposter;
                private readonly DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection;
                private readonly DefaultInvocationSetupArgumentsCriteria _argumentsCriteria;
                private DefaultInvocationSetupMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(DefaultInvocationSetupMethodImposter _imposter, DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection, DefaultInvocationSetupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._defaultInvocationSetupMethodInvocationHistoryCollection = _defaultInvocationSetupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IDefaultInvocationSetupMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new DefaultInvocationSetupMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.Throws(DefaultInvocationSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.CallBefore(DefaultInvocationSetupCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IDefaultInvocationSetupMethodInvocationsSetup IDefaultInvocationSetupMethodInvocationsSetup.CallAfter(DefaultInvocationSetupCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void DefaultInvocationSetupMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _defaultInvocationSetupMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        public delegate void _invocationSetupsDelegate(int _invocationSetups);
        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        public delegate void _invocationSetupsCallbackDelegate(int _invocationSetups);
        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        public delegate System.Exception _invocationSetupsExceptionGeneratorDelegate(int _invocationSetups);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class _invocationSetupsArguments
        {
            public int _invocationSetups;
            internal _invocationSetupsArguments(int _invocationSetups)
            {
                this._invocationSetups = _invocationSetups;
            }
        }

        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class _invocationSetupsArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> _invocationSetups { get; }

            public _invocationSetupsArgumentsCriteria(Imposter.Abstractions.Arg<int> _invocationSetups)
            {
                this._invocationSetups = _invocationSetups;
            }

            public bool Matches(_invocationSetupsArguments arguments)
            {
                return _invocationSetups.Matches(arguments._invocationSetups);
            }
        }

        public interface I_invocationSetupsMethodInvocationHistory
        {
            bool Matches(_invocationSetupsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationSetupsMethodInvocationHistory : I_invocationSetupsMethodInvocationHistory
        {
            internal _invocationSetupsArguments Arguments;
            internal System.Exception Exception;
            public _invocationSetupsMethodInvocationHistory(_invocationSetupsArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(_invocationSetupsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationSetupsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<I_invocationSetupsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<I_invocationSetupsMethodInvocationHistory>();
            internal void Add(I_invocationSetupsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(_invocationSetupsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class _invocationSetupsMethodInvocationsSetup : I_invocationSetupsMethodInvocationsSetup
        {
            internal static _invocationSetupsMethodInvocationsSetup DefaultInvocationSetup = new _invocationSetupsMethodInvocationsSetup(new _invocationSetupsArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal _invocationSetupsArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int _invocationSetups)
            {
            }

            public _invocationSetupsMethodInvocationsSetup(_invocationSetupsArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int _invocationSetups) =>
                {
                    throw new TException();
                };
                return this;
            }

            I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int _invocationSetups) =>
                {
                    throw exception;
                };
                return this;
            }

            I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.Throws(_invocationSetupsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int _invocationSetups) =>
                {
                    throw exceptionGenerator(_invocationSetups);
                };
                return this;
            }

            I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.CallBefore(_invocationSetupsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.CallAfter(_invocationSetupsCallbackDelegate callback)
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

            public void Invoke(int _invocationSetups)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(_invocationSetups);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(_invocationSetups);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(_invocationSetups);
                };
            }

            internal class MethodInvocationSetup
            {
                internal _invocationSetupsDelegate? ResultGenerator { get; set; }
                internal _invocationSetupsCallbackDelegate? CallBefore { get; set; }
                internal _invocationSetupsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationSetupsMethodInvocationsSetup
        {
            I_invocationSetupsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            I_invocationSetupsMethodInvocationsSetup Throws(System.Exception exception);
            I_invocationSetupsMethodInvocationsSetup Throws(_invocationSetupsExceptionGeneratorDelegate exceptionGenerator);
            I_invocationSetupsMethodInvocationsSetup CallBefore(_invocationSetupsCallbackDelegate callback);
            I_invocationSetupsMethodInvocationsSetup CallAfter(_invocationSetupsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface _invocationSetupsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        public interface I_invocationSetupsMethodImposterBuilder : I_invocationSetupsMethodInvocationsSetup, _invocationSetupsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationSetupsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<_invocationSetupsMethodInvocationsSetup> _invocationSetups_1 = new System.Collections.Concurrent.ConcurrentStack<_invocationSetupsMethodInvocationsSetup>();
            private readonly _invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection;
            public _invocationSetupsMethodImposter(_invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection)
            {
                this.__invocationSetupsMethodInvocationHistoryCollection = __invocationSetupsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(_invocationSetupsArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private _invocationSetupsMethodInvocationsSetup? FindMatchingSetup(_invocationSetupsArguments arguments)
            {
                foreach (var setup in _invocationSetups_1)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int _invocationSetups)
            {
                var arguments = new _invocationSetupsArguments(_invocationSetups);
                var matchingSetup = FindMatchingSetup(arguments) ?? _invocationSetupsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(_invocationSetups);
                    __invocationSetupsMethodInvocationHistoryCollection.Add(new _invocationSetupsMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    __invocationSetupsMethodInvocationHistoryCollection.Add(new _invocationSetupsMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : I_invocationSetupsMethodImposterBuilder
            {
                private readonly _invocationSetupsMethodImposter _imposter;
                private readonly _invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection;
                private readonly _invocationSetupsArgumentsCriteria _argumentsCriteria;
                private _invocationSetupsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(_invocationSetupsMethodImposter _imposter, _invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection, _invocationSetupsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this.__invocationSetupsMethodInvocationHistoryCollection = __invocationSetupsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private I_invocationSetupsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new _invocationSetupsMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups_1.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.Throws(_invocationSetupsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.CallBefore(_invocationSetupsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                I_invocationSetupsMethodInvocationsSetup I_invocationSetupsMethodInvocationsSetup.CallAfter(_invocationSetupsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void _invocationSetupsMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = __invocationSetupsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.it(int it)
        public delegate void itDelegate(int it);
        // void INameCollisionProtectionFeatureSut.it(int it)
        public delegate void itCallbackDelegate(int it);
        // void INameCollisionProtectionFeatureSut.it(int it)
        public delegate System.Exception itExceptionGeneratorDelegate(int it);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class itArguments
        {
            public int it;
            internal itArguments(int it)
            {
                this.it = it;
            }
        }

        // void INameCollisionProtectionFeatureSut.it(int it)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class itArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> it { get; }

            public itArgumentsCriteria(Imposter.Abstractions.Arg<int> it)
            {
                this.it = it;
            }

            public bool Matches(itArguments arguments)
            {
                return it.Matches(arguments.it);
            }
        }

        public interface IitMethodInvocationHistory
        {
            bool Matches(itArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class itMethodInvocationHistory : IitMethodInvocationHistory
        {
            internal itArguments Arguments;
            internal System.Exception Exception;
            public itMethodInvocationHistory(itArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(itArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class itMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IitMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IitMethodInvocationHistory>();
            internal void Add(IitMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(itArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.it(int it)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class itMethodInvocationsSetup : IitMethodInvocationsSetup
        {
            internal static itMethodInvocationsSetup DefaultInvocationSetup = new itMethodInvocationsSetup(new itArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal itArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int it)
            {
            }

            public itMethodInvocationsSetup(itArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IitMethodInvocationsSetup IitMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int it) =>
                {
                    throw new TException();
                };
                return this;
            }

            IitMethodInvocationsSetup IitMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int it) =>
                {
                    throw exception;
                };
                return this;
            }

            IitMethodInvocationsSetup IitMethodInvocationsSetup.Throws(itExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int it) =>
                {
                    throw exceptionGenerator(it);
                };
                return this;
            }

            IitMethodInvocationsSetup IitMethodInvocationsSetup.CallBefore(itCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IitMethodInvocationsSetup IitMethodInvocationsSetup.CallAfter(itCallbackDelegate callback)
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

            public void Invoke(int it)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(it);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(it);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(it);
                };
            }

            internal class MethodInvocationSetup
            {
                internal itDelegate? ResultGenerator { get; set; }
                internal itCallbackDelegate? CallBefore { get; set; }
                internal itCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IitMethodInvocationsSetup
        {
            IitMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IitMethodInvocationsSetup Throws(System.Exception exception);
            IitMethodInvocationsSetup Throws(itExceptionGeneratorDelegate exceptionGenerator);
            IitMethodInvocationsSetup CallBefore(itCallbackDelegate callback);
            IitMethodInvocationsSetup CallAfter(itCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface itMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.it(int it)
        public interface IitMethodImposterBuilder : IitMethodInvocationsSetup, itMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class itMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<itMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<itMethodInvocationsSetup>();
            private readonly itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection;
            public itMethodImposter(itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection)
            {
                this._itMethodInvocationHistoryCollection = _itMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(itArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private itMethodInvocationsSetup? FindMatchingSetup(itArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int it)
            {
                var arguments = new itArguments(it);
                var matchingSetup = FindMatchingSetup(arguments) ?? itMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(it);
                    _itMethodInvocationHistoryCollection.Add(new itMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _itMethodInvocationHistoryCollection.Add(new itMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IitMethodImposterBuilder
            {
                private readonly itMethodImposter _imposter;
                private readonly itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection;
                private readonly itArgumentsCriteria _argumentsCriteria;
                private itMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(itMethodImposter _imposter, itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection, itArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._itMethodInvocationHistoryCollection = _itMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IitMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new itMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IitMethodInvocationsSetup IitMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IitMethodInvocationsSetup IitMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IitMethodInvocationsSetup IitMethodInvocationsSetup.Throws(itExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IitMethodInvocationsSetup IitMethodInvocationsSetup.CallBefore(itCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IitMethodInvocationsSetup IitMethodInvocationsSetup.CallAfter(itCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void itMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _itMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        public delegate void invocationHistoryDelegate(int invocationHistory);
        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        public delegate void invocationHistoryCallbackDelegate(int invocationHistory);
        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        public delegate System.Exception invocationHistoryExceptionGeneratorDelegate(int invocationHistory);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class invocationHistoryArguments
        {
            public int invocationHistory;
            internal invocationHistoryArguments(int invocationHistory)
            {
                this.invocationHistory = invocationHistory;
            }
        }

        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class invocationHistoryArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> invocationHistory { get; }

            public invocationHistoryArgumentsCriteria(Imposter.Abstractions.Arg<int> invocationHistory)
            {
                this.invocationHistory = invocationHistory;
            }

            public bool Matches(invocationHistoryArguments arguments)
            {
                return invocationHistory.Matches(arguments.invocationHistory);
            }
        }

        public interface IinvocationHistoryMethodInvocationHistory
        {
            bool Matches(invocationHistoryArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class invocationHistoryMethodInvocationHistory : IinvocationHistoryMethodInvocationHistory
        {
            internal invocationHistoryArguments Arguments;
            internal System.Exception Exception;
            public invocationHistoryMethodInvocationHistory(invocationHistoryArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(invocationHistoryArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class invocationHistoryMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IinvocationHistoryMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IinvocationHistoryMethodInvocationHistory>();
            internal void Add(IinvocationHistoryMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(invocationHistoryArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class invocationHistoryMethodInvocationsSetup : IinvocationHistoryMethodInvocationsSetup
        {
            internal static invocationHistoryMethodInvocationsSetup DefaultInvocationSetup = new invocationHistoryMethodInvocationsSetup(new invocationHistoryArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal invocationHistoryArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int invocationHistory)
            {
            }

            public invocationHistoryMethodInvocationsSetup(invocationHistoryArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int invocationHistory) =>
                {
                    throw new TException();
                };
                return this;
            }

            IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int invocationHistory) =>
                {
                    throw exception;
                };
                return this;
            }

            IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.Throws(invocationHistoryExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int invocationHistory) =>
                {
                    throw exceptionGenerator(invocationHistory);
                };
                return this;
            }

            IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.CallBefore(invocationHistoryCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.CallAfter(invocationHistoryCallbackDelegate callback)
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

            public void Invoke(int invocationHistory)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(invocationHistory);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(invocationHistory);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(invocationHistory);
                };
            }

            internal class MethodInvocationSetup
            {
                internal invocationHistoryDelegate? ResultGenerator { get; set; }
                internal invocationHistoryCallbackDelegate? CallBefore { get; set; }
                internal invocationHistoryCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IinvocationHistoryMethodInvocationsSetup
        {
            IinvocationHistoryMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IinvocationHistoryMethodInvocationsSetup Throws(System.Exception exception);
            IinvocationHistoryMethodInvocationsSetup Throws(invocationHistoryExceptionGeneratorDelegate exceptionGenerator);
            IinvocationHistoryMethodInvocationsSetup CallBefore(invocationHistoryCallbackDelegate callback);
            IinvocationHistoryMethodInvocationsSetup CallAfter(invocationHistoryCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface invocationHistoryMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        public interface IinvocationHistoryMethodImposterBuilder : IinvocationHistoryMethodInvocationsSetup, invocationHistoryMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class invocationHistoryMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<invocationHistoryMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<invocationHistoryMethodInvocationsSetup>();
            private readonly invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection;
            public invocationHistoryMethodImposter(invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection)
            {
                this._invocationHistoryMethodInvocationHistoryCollection = _invocationHistoryMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(invocationHistoryArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private invocationHistoryMethodInvocationsSetup? FindMatchingSetup(invocationHistoryArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int invocationHistory)
            {
                var arguments = new invocationHistoryArguments(invocationHistory);
                var matchingSetup = FindMatchingSetup(arguments) ?? invocationHistoryMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(invocationHistory);
                    _invocationHistoryMethodInvocationHistoryCollection.Add(new invocationHistoryMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _invocationHistoryMethodInvocationHistoryCollection.Add(new invocationHistoryMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IinvocationHistoryMethodImposterBuilder
            {
                private readonly invocationHistoryMethodImposter _imposter;
                private readonly invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection;
                private readonly invocationHistoryArgumentsCriteria _argumentsCriteria;
                private invocationHistoryMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(invocationHistoryMethodImposter _imposter, invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection, invocationHistoryArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._invocationHistoryMethodInvocationHistoryCollection = _invocationHistoryMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IinvocationHistoryMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new invocationHistoryMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.Throws(invocationHistoryExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.CallBefore(invocationHistoryCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IinvocationHistoryMethodInvocationsSetup IinvocationHistoryMethodInvocationsSetup.CallAfter(invocationHistoryCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void invocationHistoryMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistoryMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        public delegate void nextSetupDelegate(int nextSetup);
        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        public delegate void nextSetupCallbackDelegate(int nextSetup);
        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        public delegate System.Exception nextSetupExceptionGeneratorDelegate(int nextSetup);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class nextSetupArguments
        {
            public int nextSetup;
            internal nextSetupArguments(int nextSetup)
            {
                this.nextSetup = nextSetup;
            }
        }

        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class nextSetupArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> nextSetup { get; }

            public nextSetupArgumentsCriteria(Imposter.Abstractions.Arg<int> nextSetup)
            {
                this.nextSetup = nextSetup;
            }

            public bool Matches(nextSetupArguments arguments)
            {
                return nextSetup.Matches(arguments.nextSetup);
            }
        }

        public interface InextSetupMethodInvocationHistory
        {
            bool Matches(nextSetupArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class nextSetupMethodInvocationHistory : InextSetupMethodInvocationHistory
        {
            internal nextSetupArguments Arguments;
            internal System.Exception Exception;
            public nextSetupMethodInvocationHistory(nextSetupArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(nextSetupArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class nextSetupMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InextSetupMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<InextSetupMethodInvocationHistory>();
            internal void Add(InextSetupMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(nextSetupArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class nextSetupMethodInvocationsSetup : InextSetupMethodInvocationsSetup
        {
            internal static nextSetupMethodInvocationsSetup DefaultInvocationSetup = new nextSetupMethodInvocationsSetup(new nextSetupArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal nextSetupArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int nextSetup)
            {
            }

            public nextSetupMethodInvocationsSetup(nextSetupArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int nextSetup) =>
                {
                    throw new TException();
                };
                return this;
            }

            InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int nextSetup) =>
                {
                    throw exception;
                };
                return this;
            }

            InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.Throws(nextSetupExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int nextSetup) =>
                {
                    throw exceptionGenerator(nextSetup);
                };
                return this;
            }

            InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.CallBefore(nextSetupCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.CallAfter(nextSetupCallbackDelegate callback)
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

            public void Invoke(int nextSetup)
            {
                var nextSetup_1 = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup_1.CallBefore != null)
                {
                    nextSetup_1.CallBefore(nextSetup);
                }

                if (nextSetup_1.ResultGenerator == null)
                {
                    nextSetup_1.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup_1.ResultGenerator.Invoke(nextSetup);
                if (nextSetup_1.CallAfter != null)
                {
                    nextSetup_1.CallAfter(nextSetup);
                };
            }

            internal class MethodInvocationSetup
            {
                internal nextSetupDelegate? ResultGenerator { get; set; }
                internal nextSetupCallbackDelegate? CallBefore { get; set; }
                internal nextSetupCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InextSetupMethodInvocationsSetup
        {
            InextSetupMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            InextSetupMethodInvocationsSetup Throws(System.Exception exception);
            InextSetupMethodInvocationsSetup Throws(nextSetupExceptionGeneratorDelegate exceptionGenerator);
            InextSetupMethodInvocationsSetup CallBefore(nextSetupCallbackDelegate callback);
            InextSetupMethodInvocationsSetup CallAfter(nextSetupCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface nextSetupMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        public interface InextSetupMethodImposterBuilder : InextSetupMethodInvocationsSetup, nextSetupMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class nextSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<nextSetupMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<nextSetupMethodInvocationsSetup>();
            private readonly nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection;
            public nextSetupMethodImposter(nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection)
            {
                this._nextSetupMethodInvocationHistoryCollection = _nextSetupMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(nextSetupArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private nextSetupMethodInvocationsSetup? FindMatchingSetup(nextSetupArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int nextSetup)
            {
                var arguments = new nextSetupArguments(nextSetup);
                var matchingSetup = FindMatchingSetup(arguments) ?? nextSetupMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(nextSetup);
                    _nextSetupMethodInvocationHistoryCollection.Add(new nextSetupMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _nextSetupMethodInvocationHistoryCollection.Add(new nextSetupMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : InextSetupMethodImposterBuilder
            {
                private readonly nextSetupMethodImposter _imposter;
                private readonly nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection;
                private readonly nextSetupArgumentsCriteria _argumentsCriteria;
                private nextSetupMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(nextSetupMethodImposter _imposter, nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection, nextSetupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._nextSetupMethodInvocationHistoryCollection = _nextSetupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private InextSetupMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new nextSetupMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.Throws(nextSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.CallBefore(nextSetupCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                InextSetupMethodInvocationsSetup InextSetupMethodInvocationsSetup.CallAfter(nextSetupCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void nextSetupMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _nextSetupMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        public delegate void GetNextSetupDelegate(int GetNextSetup);
        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        public delegate void GetNextSetupCallbackDelegate(int GetNextSetup);
        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        public delegate System.Exception GetNextSetupExceptionGeneratorDelegate(int GetNextSetup);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GetNextSetupArguments
        {
            public int GetNextSetup;
            internal GetNextSetupArguments(int GetNextSetup)
            {
                this.GetNextSetup = GetNextSetup;
            }
        }

        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GetNextSetupArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> GetNextSetup { get; }

            public GetNextSetupArgumentsCriteria(Imposter.Abstractions.Arg<int> GetNextSetup)
            {
                this.GetNextSetup = GetNextSetup;
            }

            public bool Matches(GetNextSetupArguments arguments)
            {
                return GetNextSetup.Matches(arguments.GetNextSetup);
            }
        }

        public interface IGetNextSetupMethodInvocationHistory
        {
            bool Matches(GetNextSetupArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GetNextSetupMethodInvocationHistory : IGetNextSetupMethodInvocationHistory
        {
            internal GetNextSetupArguments Arguments;
            internal System.Exception Exception;
            public GetNextSetupMethodInvocationHistory(GetNextSetupArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(GetNextSetupArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GetNextSetupMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGetNextSetupMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGetNextSetupMethodInvocationHistory>();
            internal void Add(IGetNextSetupMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(GetNextSetupArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GetNextSetupMethodInvocationsSetup : IGetNextSetupMethodInvocationsSetup
        {
            internal static GetNextSetupMethodInvocationsSetup DefaultInvocationSetup = new GetNextSetupMethodInvocationsSetup(new GetNextSetupArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal GetNextSetupArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int GetNextSetup)
            {
            }

            public GetNextSetupMethodInvocationsSetup(GetNextSetupArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int GetNextSetup) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int GetNextSetup) =>
                {
                    throw exception;
                };
                return this;
            }

            IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.Throws(GetNextSetupExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int GetNextSetup) =>
                {
                    throw exceptionGenerator(GetNextSetup);
                };
                return this;
            }

            IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.CallBefore(GetNextSetupCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.CallAfter(GetNextSetupCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
            private MethodInvocationSetup? GetNextSetup_1()
            {
                if (_callSetups.TryDequeue(out var callSetup))
                {
                    _nextSetup = callSetup;
                }

                return _nextSetup;
            }

            public void Invoke(int GetNextSetup)
            {
                var nextSetup = GetNextSetup_1() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(GetNextSetup);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(GetNextSetup);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(GetNextSetup);
                };
            }

            internal class MethodInvocationSetup
            {
                internal GetNextSetupDelegate? ResultGenerator { get; set; }
                internal GetNextSetupCallbackDelegate? CallBefore { get; set; }
                internal GetNextSetupCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGetNextSetupMethodInvocationsSetup
        {
            IGetNextSetupMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IGetNextSetupMethodInvocationsSetup Throws(System.Exception exception);
            IGetNextSetupMethodInvocationsSetup Throws(GetNextSetupExceptionGeneratorDelegate exceptionGenerator);
            IGetNextSetupMethodInvocationsSetup CallBefore(GetNextSetupCallbackDelegate callback);
            IGetNextSetupMethodInvocationsSetup CallAfter(GetNextSetupCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GetNextSetupMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        public interface IGetNextSetupMethodImposterBuilder : IGetNextSetupMethodInvocationsSetup, GetNextSetupMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GetNextSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GetNextSetupMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GetNextSetupMethodInvocationsSetup>();
            private readonly GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection;
            public GetNextSetupMethodImposter(GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection)
            {
                this._getNextSetupMethodInvocationHistoryCollection = _getNextSetupMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(GetNextSetupArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GetNextSetupMethodInvocationsSetup? FindMatchingSetup(GetNextSetupArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int GetNextSetup)
            {
                var arguments = new GetNextSetupArguments(GetNextSetup);
                var matchingSetup = FindMatchingSetup(arguments) ?? GetNextSetupMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(GetNextSetup);
                    _getNextSetupMethodInvocationHistoryCollection.Add(new GetNextSetupMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _getNextSetupMethodInvocationHistoryCollection.Add(new GetNextSetupMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGetNextSetupMethodImposterBuilder
            {
                private readonly GetNextSetupMethodImposter _imposter;
                private readonly GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection;
                private readonly GetNextSetupArgumentsCriteria _argumentsCriteria;
                private GetNextSetupMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(GetNextSetupMethodImposter _imposter, GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection, GetNextSetupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._getNextSetupMethodInvocationHistoryCollection = _getNextSetupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGetNextSetupMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GetNextSetupMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.Throws(GetNextSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.CallBefore(GetNextSetupCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGetNextSetupMethodInvocationsSetup IGetNextSetupMethodInvocationsSetup.CallAfter(GetNextSetupCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void GetNextSetupMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _getNextSetupMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        public delegate void exceptionGeneratorDelegate(int exceptionGenerator);
        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        public delegate void exceptionGeneratorCallbackDelegate(int exceptionGenerator);
        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        public delegate System.Exception exceptionGeneratorExceptionGeneratorDelegate(int exceptionGenerator);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class exceptionGeneratorArguments
        {
            public int exceptionGenerator;
            internal exceptionGeneratorArguments(int exceptionGenerator)
            {
                this.exceptionGenerator = exceptionGenerator;
            }
        }

        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class exceptionGeneratorArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> exceptionGenerator { get; }

            public exceptionGeneratorArgumentsCriteria(Imposter.Abstractions.Arg<int> exceptionGenerator)
            {
                this.exceptionGenerator = exceptionGenerator;
            }

            public bool Matches(exceptionGeneratorArguments arguments)
            {
                return exceptionGenerator.Matches(arguments.exceptionGenerator);
            }
        }

        public interface IexceptionGeneratorMethodInvocationHistory
        {
            bool Matches(exceptionGeneratorArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class exceptionGeneratorMethodInvocationHistory : IexceptionGeneratorMethodInvocationHistory
        {
            internal exceptionGeneratorArguments Arguments;
            internal System.Exception Exception;
            public exceptionGeneratorMethodInvocationHistory(exceptionGeneratorArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(exceptionGeneratorArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class exceptionGeneratorMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IexceptionGeneratorMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IexceptionGeneratorMethodInvocationHistory>();
            internal void Add(IexceptionGeneratorMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(exceptionGeneratorArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class exceptionGeneratorMethodInvocationsSetup : IexceptionGeneratorMethodInvocationsSetup
        {
            internal static exceptionGeneratorMethodInvocationsSetup DefaultInvocationSetup = new exceptionGeneratorMethodInvocationsSetup(new exceptionGeneratorArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal exceptionGeneratorArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int exceptionGenerator)
            {
            }

            public exceptionGeneratorMethodInvocationsSetup(exceptionGeneratorArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int exceptionGenerator) =>
                {
                    throw new TException();
                };
                return this;
            }

            IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int exceptionGenerator) =>
                {
                    throw exception;
                };
                return this;
            }

            IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.Throws(exceptionGeneratorExceptionGeneratorDelegate exceptionGenerator_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int exceptionGenerator) =>
                {
                    throw exceptionGenerator_1(exceptionGenerator);
                };
                return this;
            }

            IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.CallBefore(exceptionGeneratorCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.CallAfter(exceptionGeneratorCallbackDelegate callback)
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

            public void Invoke(int exceptionGenerator)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(exceptionGenerator);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(exceptionGenerator);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(exceptionGenerator);
                };
            }

            internal class MethodInvocationSetup
            {
                internal exceptionGeneratorDelegate? ResultGenerator { get; set; }
                internal exceptionGeneratorCallbackDelegate? CallBefore { get; set; }
                internal exceptionGeneratorCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IexceptionGeneratorMethodInvocationsSetup
        {
            IexceptionGeneratorMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IexceptionGeneratorMethodInvocationsSetup Throws(System.Exception exception);
            IexceptionGeneratorMethodInvocationsSetup Throws(exceptionGeneratorExceptionGeneratorDelegate exceptionGenerator_1);
            IexceptionGeneratorMethodInvocationsSetup CallBefore(exceptionGeneratorCallbackDelegate callback);
            IexceptionGeneratorMethodInvocationsSetup CallAfter(exceptionGeneratorCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface exceptionGeneratorMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        public interface IexceptionGeneratorMethodImposterBuilder : IexceptionGeneratorMethodInvocationsSetup, exceptionGeneratorMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class exceptionGeneratorMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<exceptionGeneratorMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<exceptionGeneratorMethodInvocationsSetup>();
            private readonly exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection;
            public exceptionGeneratorMethodImposter(exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection)
            {
                this._exceptionGeneratorMethodInvocationHistoryCollection = _exceptionGeneratorMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(exceptionGeneratorArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private exceptionGeneratorMethodInvocationsSetup? FindMatchingSetup(exceptionGeneratorArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int exceptionGenerator)
            {
                var arguments = new exceptionGeneratorArguments(exceptionGenerator);
                var matchingSetup = FindMatchingSetup(arguments) ?? exceptionGeneratorMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(exceptionGenerator);
                    _exceptionGeneratorMethodInvocationHistoryCollection.Add(new exceptionGeneratorMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _exceptionGeneratorMethodInvocationHistoryCollection.Add(new exceptionGeneratorMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IexceptionGeneratorMethodImposterBuilder
            {
                private readonly exceptionGeneratorMethodImposter _imposter;
                private readonly exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection;
                private readonly exceptionGeneratorArgumentsCriteria _argumentsCriteria;
                private exceptionGeneratorMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(exceptionGeneratorMethodImposter _imposter, exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection, exceptionGeneratorArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._exceptionGeneratorMethodInvocationHistoryCollection = _exceptionGeneratorMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IexceptionGeneratorMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new exceptionGeneratorMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.Throws(exceptionGeneratorExceptionGeneratorDelegate exceptionGenerator_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator_1);
                    return invocationSetup;
                }

                IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.CallBefore(exceptionGeneratorCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IexceptionGeneratorMethodInvocationsSetup IexceptionGeneratorMethodInvocationsSetup.CallAfter(exceptionGeneratorCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void exceptionGeneratorMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _exceptionGeneratorMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.callback(int callback)
        public delegate void callbackDelegate(int callback);
        // void INameCollisionProtectionFeatureSut.callback(int callback)
        public delegate void callbackCallbackDelegate(int callback);
        // void INameCollisionProtectionFeatureSut.callback(int callback)
        public delegate System.Exception callbackExceptionGeneratorDelegate(int callback);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class callbackArguments
        {
            public int callback;
            internal callbackArguments(int callback)
            {
                this.callback = callback;
            }
        }

        // void INameCollisionProtectionFeatureSut.callback(int callback)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class callbackArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> callback { get; }

            public callbackArgumentsCriteria(Imposter.Abstractions.Arg<int> callback)
            {
                this.callback = callback;
            }

            public bool Matches(callbackArguments arguments)
            {
                return callback.Matches(arguments.callback);
            }
        }

        public interface IcallbackMethodInvocationHistory
        {
            bool Matches(callbackArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class callbackMethodInvocationHistory : IcallbackMethodInvocationHistory
        {
            internal callbackArguments Arguments;
            internal System.Exception Exception;
            public callbackMethodInvocationHistory(callbackArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(callbackArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class callbackMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IcallbackMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IcallbackMethodInvocationHistory>();
            internal void Add(IcallbackMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(callbackArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.callback(int callback)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class callbackMethodInvocationsSetup : IcallbackMethodInvocationsSetup
        {
            internal static callbackMethodInvocationsSetup DefaultInvocationSetup = new callbackMethodInvocationsSetup(new callbackArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal callbackArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int callback)
            {
            }

            public callbackMethodInvocationsSetup(callbackArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int callback) =>
                {
                    throw new TException();
                };
                return this;
            }

            IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int callback) =>
                {
                    throw exception;
                };
                return this;
            }

            IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.Throws(callbackExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int callback) =>
                {
                    throw exceptionGenerator(callback);
                };
                return this;
            }

            IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.CallBefore(callbackCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.CallAfter(callbackCallbackDelegate callback)
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

            public void Invoke(int callback)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(callback);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(callback);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(callback);
                };
            }

            internal class MethodInvocationSetup
            {
                internal callbackDelegate? ResultGenerator { get; set; }
                internal callbackCallbackDelegate? CallBefore { get; set; }
                internal callbackCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IcallbackMethodInvocationsSetup
        {
            IcallbackMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IcallbackMethodInvocationsSetup Throws(System.Exception exception);
            IcallbackMethodInvocationsSetup Throws(callbackExceptionGeneratorDelegate exceptionGenerator);
            IcallbackMethodInvocationsSetup CallBefore(callbackCallbackDelegate callback_1);
            IcallbackMethodInvocationsSetup CallAfter(callbackCallbackDelegate callback_1);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface callbackMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.callback(int callback)
        public interface IcallbackMethodImposterBuilder : IcallbackMethodInvocationsSetup, callbackMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class callbackMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<callbackMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<callbackMethodInvocationsSetup>();
            private readonly callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection;
            public callbackMethodImposter(callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection)
            {
                this._callbackMethodInvocationHistoryCollection = _callbackMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(callbackArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private callbackMethodInvocationsSetup? FindMatchingSetup(callbackArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int callback)
            {
                var arguments = new callbackArguments(callback);
                var matchingSetup = FindMatchingSetup(arguments) ?? callbackMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(callback);
                    _callbackMethodInvocationHistoryCollection.Add(new callbackMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _callbackMethodInvocationHistoryCollection.Add(new callbackMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IcallbackMethodImposterBuilder
            {
                private readonly callbackMethodImposter _imposter;
                private readonly callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection;
                private readonly callbackArgumentsCriteria _argumentsCriteria;
                private callbackMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(callbackMethodImposter _imposter, callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection, callbackArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._callbackMethodInvocationHistoryCollection = _callbackMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IcallbackMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new callbackMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.Throws(callbackExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.CallBefore(callbackCallbackDelegate callback_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback_1);
                    return invocationSetup;
                }

                IcallbackMethodInvocationsSetup IcallbackMethodInvocationsSetup.CallAfter(callbackCallbackDelegate callback_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback_1);
                    return invocationSetup;
                }

                void callbackMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _callbackMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        public delegate void resultGeneratorDelegate(int resultGenerator);
        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        public delegate void resultGeneratorCallbackDelegate(int resultGenerator);
        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        public delegate System.Exception resultGeneratorExceptionGeneratorDelegate(int resultGenerator);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class resultGeneratorArguments
        {
            public int resultGenerator;
            internal resultGeneratorArguments(int resultGenerator)
            {
                this.resultGenerator = resultGenerator;
            }
        }

        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class resultGeneratorArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> resultGenerator { get; }

            public resultGeneratorArgumentsCriteria(Imposter.Abstractions.Arg<int> resultGenerator)
            {
                this.resultGenerator = resultGenerator;
            }

            public bool Matches(resultGeneratorArguments arguments)
            {
                return resultGenerator.Matches(arguments.resultGenerator);
            }
        }

        public interface IresultGeneratorMethodInvocationHistory
        {
            bool Matches(resultGeneratorArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class resultGeneratorMethodInvocationHistory : IresultGeneratorMethodInvocationHistory
        {
            internal resultGeneratorArguments Arguments;
            internal System.Exception Exception;
            public resultGeneratorMethodInvocationHistory(resultGeneratorArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(resultGeneratorArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class resultGeneratorMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IresultGeneratorMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IresultGeneratorMethodInvocationHistory>();
            internal void Add(IresultGeneratorMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(resultGeneratorArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class resultGeneratorMethodInvocationsSetup : IresultGeneratorMethodInvocationsSetup
        {
            internal static resultGeneratorMethodInvocationsSetup DefaultInvocationSetup = new resultGeneratorMethodInvocationsSetup(new resultGeneratorArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal resultGeneratorArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int resultGenerator)
            {
            }

            public resultGeneratorMethodInvocationsSetup(resultGeneratorArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int resultGenerator) =>
                {
                    throw new TException();
                };
                return this;
            }

            IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int resultGenerator) =>
                {
                    throw exception;
                };
                return this;
            }

            IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.Throws(resultGeneratorExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int resultGenerator) =>
                {
                    throw exceptionGenerator(resultGenerator);
                };
                return this;
            }

            IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.CallBefore(resultGeneratorCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.CallAfter(resultGeneratorCallbackDelegate callback)
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

            public void Invoke(int resultGenerator)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(resultGenerator);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(resultGenerator);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(resultGenerator);
                };
            }

            internal class MethodInvocationSetup
            {
                internal resultGeneratorDelegate? ResultGenerator { get; set; }
                internal resultGeneratorCallbackDelegate? CallBefore { get; set; }
                internal resultGeneratorCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IresultGeneratorMethodInvocationsSetup
        {
            IresultGeneratorMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IresultGeneratorMethodInvocationsSetup Throws(System.Exception exception);
            IresultGeneratorMethodInvocationsSetup Throws(resultGeneratorExceptionGeneratorDelegate exceptionGenerator);
            IresultGeneratorMethodInvocationsSetup CallBefore(resultGeneratorCallbackDelegate callback);
            IresultGeneratorMethodInvocationsSetup CallAfter(resultGeneratorCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface resultGeneratorMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        public interface IresultGeneratorMethodImposterBuilder : IresultGeneratorMethodInvocationsSetup, resultGeneratorMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class resultGeneratorMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<resultGeneratorMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<resultGeneratorMethodInvocationsSetup>();
            private readonly resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection;
            public resultGeneratorMethodImposter(resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection)
            {
                this._resultGeneratorMethodInvocationHistoryCollection = _resultGeneratorMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(resultGeneratorArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private resultGeneratorMethodInvocationsSetup? FindMatchingSetup(resultGeneratorArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int resultGenerator)
            {
                var arguments = new resultGeneratorArguments(resultGenerator);
                var matchingSetup = FindMatchingSetup(arguments) ?? resultGeneratorMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(resultGenerator);
                    _resultGeneratorMethodInvocationHistoryCollection.Add(new resultGeneratorMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _resultGeneratorMethodInvocationHistoryCollection.Add(new resultGeneratorMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IresultGeneratorMethodImposterBuilder
            {
                private readonly resultGeneratorMethodImposter _imposter;
                private readonly resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection;
                private readonly resultGeneratorArgumentsCriteria _argumentsCriteria;
                private resultGeneratorMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(resultGeneratorMethodImposter _imposter, resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection, resultGeneratorArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._resultGeneratorMethodInvocationHistoryCollection = _resultGeneratorMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IresultGeneratorMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new resultGeneratorMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.Throws(resultGeneratorExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.CallBefore(resultGeneratorCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IresultGeneratorMethodInvocationsSetup IresultGeneratorMethodInvocationsSetup.CallAfter(resultGeneratorCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void resultGeneratorMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _resultGeneratorMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        public delegate void argumentsCriteriaDelegate(int argumentsCriteria);
        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        public delegate void argumentsCriteriaCallbackDelegate(int argumentsCriteria);
        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        public delegate System.Exception argumentsCriteriaExceptionGeneratorDelegate(int argumentsCriteria);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class argumentsCriteriaArguments
        {
            public int argumentsCriteria;
            internal argumentsCriteriaArguments(int argumentsCriteria)
            {
                this.argumentsCriteria = argumentsCriteria;
            }
        }

        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class argumentsCriteriaArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> argumentsCriteria { get; }

            public argumentsCriteriaArgumentsCriteria(Imposter.Abstractions.Arg<int> argumentsCriteria)
            {
                this.argumentsCriteria = argumentsCriteria;
            }

            public bool Matches(argumentsCriteriaArguments arguments)
            {
                return argumentsCriteria.Matches(arguments.argumentsCriteria);
            }
        }

        public interface IargumentsCriteriaMethodInvocationHistory
        {
            bool Matches(argumentsCriteriaArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class argumentsCriteriaMethodInvocationHistory : IargumentsCriteriaMethodInvocationHistory
        {
            internal argumentsCriteriaArguments Arguments;
            internal System.Exception Exception;
            public argumentsCriteriaMethodInvocationHistory(argumentsCriteriaArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(argumentsCriteriaArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class argumentsCriteriaMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IargumentsCriteriaMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IargumentsCriteriaMethodInvocationHistory>();
            internal void Add(IargumentsCriteriaMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(argumentsCriteriaArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class argumentsCriteriaMethodInvocationsSetup : IargumentsCriteriaMethodInvocationsSetup
        {
            internal static argumentsCriteriaMethodInvocationsSetup DefaultInvocationSetup = new argumentsCriteriaMethodInvocationsSetup(new argumentsCriteriaArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal argumentsCriteriaArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int argumentsCriteria)
            {
            }

            public argumentsCriteriaMethodInvocationsSetup(argumentsCriteriaArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int argumentsCriteria) =>
                {
                    throw new TException();
                };
                return this;
            }

            IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int argumentsCriteria) =>
                {
                    throw exception;
                };
                return this;
            }

            IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.Throws(argumentsCriteriaExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int argumentsCriteria) =>
                {
                    throw exceptionGenerator(argumentsCriteria);
                };
                return this;
            }

            IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.CallBefore(argumentsCriteriaCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.CallAfter(argumentsCriteriaCallbackDelegate callback)
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

            public void Invoke(int argumentsCriteria)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(argumentsCriteria);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(argumentsCriteria);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(argumentsCriteria);
                };
            }

            internal class MethodInvocationSetup
            {
                internal argumentsCriteriaDelegate? ResultGenerator { get; set; }
                internal argumentsCriteriaCallbackDelegate? CallBefore { get; set; }
                internal argumentsCriteriaCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IargumentsCriteriaMethodInvocationsSetup
        {
            IargumentsCriteriaMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IargumentsCriteriaMethodInvocationsSetup Throws(System.Exception exception);
            IargumentsCriteriaMethodInvocationsSetup Throws(argumentsCriteriaExceptionGeneratorDelegate exceptionGenerator);
            IargumentsCriteriaMethodInvocationsSetup CallBefore(argumentsCriteriaCallbackDelegate callback);
            IargumentsCriteriaMethodInvocationsSetup CallAfter(argumentsCriteriaCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface argumentsCriteriaMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        public interface IargumentsCriteriaMethodImposterBuilder : IargumentsCriteriaMethodInvocationsSetup, argumentsCriteriaMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class argumentsCriteriaMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<argumentsCriteriaMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<argumentsCriteriaMethodInvocationsSetup>();
            private readonly argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection;
            public argumentsCriteriaMethodImposter(argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection)
            {
                this._argumentsCriteriaMethodInvocationHistoryCollection = _argumentsCriteriaMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(argumentsCriteriaArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private argumentsCriteriaMethodInvocationsSetup? FindMatchingSetup(argumentsCriteriaArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int argumentsCriteria)
            {
                var arguments = new argumentsCriteriaArguments(argumentsCriteria);
                var matchingSetup = FindMatchingSetup(arguments) ?? argumentsCriteriaMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(argumentsCriteria);
                    _argumentsCriteriaMethodInvocationHistoryCollection.Add(new argumentsCriteriaMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _argumentsCriteriaMethodInvocationHistoryCollection.Add(new argumentsCriteriaMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IargumentsCriteriaMethodImposterBuilder
            {
                private readonly argumentsCriteriaMethodImposter _imposter;
                private readonly argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection;
                private readonly argumentsCriteriaArgumentsCriteria _argumentsCriteria;
                private argumentsCriteriaMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(argumentsCriteriaMethodImposter _imposter, argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection, argumentsCriteriaArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._argumentsCriteriaMethodInvocationHistoryCollection = _argumentsCriteriaMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IargumentsCriteriaMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new argumentsCriteriaMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.Throws(argumentsCriteriaExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.CallBefore(argumentsCriteriaCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IargumentsCriteriaMethodInvocationsSetup IargumentsCriteriaMethodInvocationsSetup.CallAfter(argumentsCriteriaCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void argumentsCriteriaMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _argumentsCriteriaMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.TException(int TException)
        public delegate void TExceptionDelegate(int TException);
        // void INameCollisionProtectionFeatureSut.TException(int TException)
        public delegate void TExceptionCallbackDelegate(int TException);
        // void INameCollisionProtectionFeatureSut.TException(int TException)
        public delegate System.Exception TExceptionExceptionGeneratorDelegate(int TException);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class TExceptionArguments
        {
            public int TException;
            internal TExceptionArguments(int TException)
            {
                this.TException = TException;
            }
        }

        // void INameCollisionProtectionFeatureSut.TException(int TException)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class TExceptionArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> TException { get; }

            public TExceptionArgumentsCriteria(Imposter.Abstractions.Arg<int> TException)
            {
                this.TException = TException;
            }

            public bool Matches(TExceptionArguments arguments)
            {
                return TException.Matches(arguments.TException);
            }
        }

        public interface ITExceptionMethodInvocationHistory
        {
            bool Matches(TExceptionArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class TExceptionMethodInvocationHistory : ITExceptionMethodInvocationHistory
        {
            internal TExceptionArguments Arguments;
            internal System.Exception Exception;
            public TExceptionMethodInvocationHistory(TExceptionArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(TExceptionArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class TExceptionMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ITExceptionMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ITExceptionMethodInvocationHistory>();
            internal void Add(ITExceptionMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(TExceptionArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.TException(int TException)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class TExceptionMethodInvocationsSetup : ITExceptionMethodInvocationsSetup
        {
            internal static TExceptionMethodInvocationsSetup DefaultInvocationSetup = new TExceptionMethodInvocationsSetup(new TExceptionArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal TExceptionArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int TException)
            {
            }

            public TExceptionMethodInvocationsSetup(TExceptionArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int TException) =>
                {
                    throw new TException();
                };
                return this;
            }

            ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int TException) =>
                {
                    throw exception;
                };
                return this;
            }

            ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.Throws(TExceptionExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int TException) =>
                {
                    throw exceptionGenerator(TException);
                };
                return this;
            }

            ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.CallBefore(TExceptionCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.CallAfter(TExceptionCallbackDelegate callback)
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

            public void Invoke(int TException)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(TException);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(TException);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(TException);
                };
            }

            internal class MethodInvocationSetup
            {
                internal TExceptionDelegate? ResultGenerator { get; set; }
                internal TExceptionCallbackDelegate? CallBefore { get; set; }
                internal TExceptionCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ITExceptionMethodInvocationsSetup
        {
            ITExceptionMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            ITExceptionMethodInvocationsSetup Throws(System.Exception exception);
            ITExceptionMethodInvocationsSetup Throws(TExceptionExceptionGeneratorDelegate exceptionGenerator);
            ITExceptionMethodInvocationsSetup CallBefore(TExceptionCallbackDelegate callback);
            ITExceptionMethodInvocationsSetup CallAfter(TExceptionCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface TExceptionMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.TException(int TException)
        public interface ITExceptionMethodImposterBuilder : ITExceptionMethodInvocationsSetup, TExceptionMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class TExceptionMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<TExceptionMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<TExceptionMethodInvocationsSetup>();
            private readonly TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection;
            public TExceptionMethodImposter(TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection)
            {
                this._tExceptionMethodInvocationHistoryCollection = _tExceptionMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(TExceptionArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private TExceptionMethodInvocationsSetup? FindMatchingSetup(TExceptionArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int TException)
            {
                var arguments = new TExceptionArguments(TException);
                var matchingSetup = FindMatchingSetup(arguments) ?? TExceptionMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(TException);
                    _tExceptionMethodInvocationHistoryCollection.Add(new TExceptionMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _tExceptionMethodInvocationHistoryCollection.Add(new TExceptionMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ITExceptionMethodImposterBuilder
            {
                private readonly TExceptionMethodImposter _imposter;
                private readonly TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection;
                private readonly TExceptionArgumentsCriteria _argumentsCriteria;
                private TExceptionMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(TExceptionMethodImposter _imposter, TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection, TExceptionArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._tExceptionMethodInvocationHistoryCollection = _tExceptionMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ITExceptionMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new TExceptionMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.Throws(TExceptionExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.CallBefore(TExceptionCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ITExceptionMethodInvocationsSetup ITExceptionMethodInvocationsSetup.CallAfter(TExceptionCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void TExceptionMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _tExceptionMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        public delegate void InitializeOutParametersWithDefaultValuesDelegate(int InitializeOutParametersWithDefaultValues);
        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        public delegate void InitializeOutParametersWithDefaultValuesCallbackDelegate(int InitializeOutParametersWithDefaultValues);
        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        public delegate System.Exception InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate(int InitializeOutParametersWithDefaultValues);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InitializeOutParametersWithDefaultValuesArguments
        {
            public int InitializeOutParametersWithDefaultValues;
            internal InitializeOutParametersWithDefaultValuesArguments(int InitializeOutParametersWithDefaultValues)
            {
                this.InitializeOutParametersWithDefaultValues = InitializeOutParametersWithDefaultValues;
            }
        }

        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InitializeOutParametersWithDefaultValuesArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> InitializeOutParametersWithDefaultValues { get; }

            public InitializeOutParametersWithDefaultValuesArgumentsCriteria(Imposter.Abstractions.Arg<int> InitializeOutParametersWithDefaultValues)
            {
                this.InitializeOutParametersWithDefaultValues = InitializeOutParametersWithDefaultValues;
            }

            public bool Matches(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                return InitializeOutParametersWithDefaultValues.Matches(arguments.InitializeOutParametersWithDefaultValues);
            }
        }

        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationHistory
        {
            bool Matches(InitializeOutParametersWithDefaultValuesArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodInvocationHistory : IInitializeOutParametersWithDefaultValuesMethodInvocationHistory
        {
            internal InitializeOutParametersWithDefaultValuesArguments Arguments;
            internal System.Exception Exception;
            public InitializeOutParametersWithDefaultValuesMethodInvocationHistory(InitializeOutParametersWithDefaultValuesArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(InitializeOutParametersWithDefaultValuesArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IInitializeOutParametersWithDefaultValuesMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IInitializeOutParametersWithDefaultValuesMethodInvocationHistory>();
            internal void Add(IInitializeOutParametersWithDefaultValuesMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(InitializeOutParametersWithDefaultValuesArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class InitializeOutParametersWithDefaultValuesMethodInvocationsSetup : IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup
        {
            internal static InitializeOutParametersWithDefaultValuesMethodInvocationsSetup DefaultInvocationSetup = new InitializeOutParametersWithDefaultValuesMethodInvocationsSetup(new InitializeOutParametersWithDefaultValuesArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal InitializeOutParametersWithDefaultValuesArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(int InitializeOutParametersWithDefaultValues)
            {
            }

            public InitializeOutParametersWithDefaultValuesMethodInvocationsSetup(InitializeOutParametersWithDefaultValuesArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int InitializeOutParametersWithDefaultValues) =>
                {
                    throw new TException();
                };
                return this;
            }

            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int InitializeOutParametersWithDefaultValues) =>
                {
                    throw exception;
                };
                return this;
            }

            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int InitializeOutParametersWithDefaultValues) =>
                {
                    throw exceptionGenerator(InitializeOutParametersWithDefaultValues);
                };
                return this;
            }

            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.CallBefore(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.CallAfter(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
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

            public void Invoke(int InitializeOutParametersWithDefaultValues)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(InitializeOutParametersWithDefaultValues);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(InitializeOutParametersWithDefaultValues);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(InitializeOutParametersWithDefaultValues);
                };
            }

            internal class MethodInvocationSetup
            {
                internal InitializeOutParametersWithDefaultValuesDelegate? ResultGenerator { get; set; }
                internal InitializeOutParametersWithDefaultValuesCallbackDelegate? CallBefore { get; set; }
                internal InitializeOutParametersWithDefaultValuesCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup
        {
            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup Throws(System.Exception exception);
            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator);
            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup CallBefore(InitializeOutParametersWithDefaultValuesCallbackDelegate callback);
            IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup CallAfter(InitializeOutParametersWithDefaultValuesCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InitializeOutParametersWithDefaultValuesMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        public interface IInitializeOutParametersWithDefaultValuesMethodImposterBuilder : IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup, InitializeOutParametersWithDefaultValuesMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InitializeOutParametersWithDefaultValuesMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<InitializeOutParametersWithDefaultValuesMethodInvocationsSetup>();
            private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
            public InitializeOutParametersWithDefaultValuesMethodImposter(InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection)
            {
                this._initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private InitializeOutParametersWithDefaultValuesMethodInvocationsSetup? FindMatchingSetup(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(int InitializeOutParametersWithDefaultValues)
            {
                var arguments = new InitializeOutParametersWithDefaultValuesArguments(InitializeOutParametersWithDefaultValues);
                var matchingSetup = FindMatchingSetup(arguments) ?? InitializeOutParametersWithDefaultValuesMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(InitializeOutParametersWithDefaultValues);
                    _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection.Add(new InitializeOutParametersWithDefaultValuesMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection.Add(new InitializeOutParametersWithDefaultValuesMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInitializeOutParametersWithDefaultValuesMethodImposterBuilder
            {
                private readonly InitializeOutParametersWithDefaultValuesMethodImposter _imposter;
                private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
                private readonly InitializeOutParametersWithDefaultValuesArgumentsCriteria _argumentsCriteria;
                private InitializeOutParametersWithDefaultValuesMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(InitializeOutParametersWithDefaultValuesMethodImposter _imposter, InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, InitializeOutParametersWithDefaultValuesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new InitializeOutParametersWithDefaultValuesMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.CallBefore(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup IInitializeOutParametersWithDefaultValuesMethodInvocationsSetup.CallAfter(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void InitializeOutParametersWithDefaultValuesMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.NameCollisionProtection.INameCollisionProtectionFeatureSut
        {
            INameCollisionProtectionFeatureSutImposter _imposter;
            public ImposterTargetInstance(INameCollisionProtectionFeatureSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int CollisionWithReturns<TValue>(TValue value)
            {
                return _imposter._collisionWithReturnsMethodImposterCollection.GetImposterWithMatchingSetup<TValue>(new CollisionWithReturnsArguments<TValue>(value)).Invoke(value);
            }

            public int CollisionWithReturns(int value)
            {
                return _imposter._collisionWithReturns_1MethodImposter.Invoke(value);
            }

            public int CollisionWithReturns(string result)
            {
                return _imposter._collisionWithReturns_2MethodImposter.Invoke(result);
            }

            public int CollisionWithReturns(double matchingSetup)
            {
                return _imposter._collisionWithReturns_3MethodImposter.Invoke(matchingSetup);
            }

            public int CollisionWithThrows<TValue>(TValue ex)
            {
                return _imposter._collisionWithThrowsMethodImposterCollection.GetImposterWithMatchingSetup<TValue>(new CollisionWithThrowsArguments<TValue>(ex)).Invoke(ex);
            }

            public int CollisionWithThrows(global::System.Exception ex)
            {
                return _imposter._collisionWithThrows_1MethodImposter.Invoke(ex);
            }

            public int MethodWithSameNameDifferentSignature()
            {
                return _imposter._methodWithSameNameDifferentSignatureMethodImposter.Invoke();
            }

            public int MethodWithSameNameDifferentSignature(int input)
            {
                return _imposter._methodWithSameNameDifferentSignature_1MethodImposter.Invoke(input);
            }

            public int MethodWithSameNameDifferentSignature<T>(T input)
            {
                return _imposter._methodWithSameNameDifferentSignature_2MethodImposterCollection.GetImposterWithMatchingSetup<T>(new MethodWithSameNameDifferentSignature_2Arguments<T>(input)).Invoke(input);
            }

            public void DefaultInvocationSetup(int DefaultInvocationSetup)
            {
                _imposter._defaultInvocationSetupMethodImposter.Invoke(DefaultInvocationSetup);
            }

            public void _invocationSetups(int _invocationSetups)
            {
                _imposter.__invocationSetupsMethodImposter.Invoke(_invocationSetups);
            }

            public void it(int it)
            {
                _imposter._itMethodImposter.Invoke(it);
            }

            public void invocationHistory(int invocationHistory)
            {
                _imposter._invocationHistoryMethodImposter.Invoke(invocationHistory);
            }

            public void nextSetup(int nextSetup)
            {
                _imposter._nextSetupMethodImposter.Invoke(nextSetup);
            }

            public void GetNextSetup(int GetNextSetup)
            {
                _imposter._getNextSetupMethodImposter.Invoke(GetNextSetup);
            }

            public void exceptionGenerator(int exceptionGenerator)
            {
                _imposter._exceptionGeneratorMethodImposter.Invoke(exceptionGenerator);
            }

            public void callback(int callback)
            {
                _imposter._callbackMethodImposter.Invoke(callback);
            }

            public void resultGenerator(int resultGenerator)
            {
                _imposter._resultGeneratorMethodImposter.Invoke(resultGenerator);
            }

            public void argumentsCriteria(int argumentsCriteria)
            {
                _imposter._argumentsCriteriaMethodImposter.Invoke(argumentsCriteria);
            }

            public void TException(int TException)
            {
                _imposter._tExceptionMethodImposter.Invoke(TException);
            }

            public void InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
            {
                _imposter._initializeOutParametersWithDefaultValuesMethodImposter.Invoke(InitializeOutParametersWithDefaultValues);
            }
        }
    }
}