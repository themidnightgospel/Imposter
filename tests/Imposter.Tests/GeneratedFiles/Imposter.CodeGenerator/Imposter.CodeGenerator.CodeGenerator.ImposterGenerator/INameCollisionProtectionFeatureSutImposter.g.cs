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
        class CollisionWithReturnsMethodInvocationImposterGroup<TValue>
        {
            internal static CollisionWithReturnsMethodInvocationImposterGroup<TValue> Default = new CollisionWithReturnsMethodInvocationImposterGroup<TValue>(new CollisionWithReturnsArgumentsCriteria<TValue>(Imposter.Abstractions.Arg<TValue>.Any()));
            internal CollisionWithReturnsArgumentsCriteria<TValue> ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollisionWithReturnsMethodInvocationImposterGroup(CollisionWithReturnsArgumentsCriteria<TValue> argumentsCriteria)
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

            public int Invoke(TValue value)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(value);
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

                private CollisionWithReturnsDelegate<TValue> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturnsCallbackDelegate<TValue>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturnsCallbackDelegate<TValue>>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(TValue value)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(value);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }
                }

                internal void Callback(CollisionWithReturnsCallbackDelegate<TValue> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CollisionWithReturnsDelegate<TValue> resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value_1)
                {
                    _resultGenerator = (TValue value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    _resultGenerator = (TValue value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal static int DefaultResultGenerator(TValue value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>
        {
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Throws(System.Exception exception);
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator);
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Callback(CollisionWithReturnsCallbackDelegate<TValue> callback);
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Returns(CollisionWithReturnsDelegate<TValue> resultGenerator);
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Returns(int value_1);
            ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> Then();
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
        public interface ICollisionWithReturnsMethodImposterBuilder<TValue> : ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>, CollisionWithReturnsMethodInvocationVerifier<TValue>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturnsMethodImposter<TValue> : ICollisionWithReturnsMethodImposter<TValue>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturnsMethodInvocationImposterGroup<TValue>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturnsMethodInvocationImposterGroup<TValue>>();
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
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollisionWithReturnsMethodInvocationImposterGroup<TValue>? FindMatchingInvocationImposterGroup(CollisionWithReturnsArguments<TValue> arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? CollisionWithReturnsMethodInvocationImposterGroup<TValue>.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(value);
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
                private readonly CollisionWithReturnsMethodInvocationImposterGroup<TValue> _invocationImposterGroup;
                private CollisionWithReturnsMethodInvocationImposterGroup<TValue>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollisionWithReturnsMethodImposterCollection _imposterCollection, CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection, CollisionWithReturnsArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._collisionWithReturnsMethodInvocationHistoryCollection = _collisionWithReturnsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollisionWithReturnsMethodInvocationImposterGroup<TValue>(_argumentsCriteria);
                    CollisionWithReturnsMethodImposter<TValue> methodImposter = _imposterCollection.AddNew<TValue>();
                    methodImposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((TValue value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((TValue value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((TValue value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Callback(CollisionWithReturnsCallbackDelegate<TValue> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Returns(CollisionWithReturnsDelegate<TValue> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                ICollisionWithReturnsMethodInvocationImposterBuilder<TValue> ICollisionWithReturnsMethodInvocationImposterBuilder<TValue>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class CollisionWithReturns_1MethodInvocationImposterGroup
        {
            internal static CollisionWithReturns_1MethodInvocationImposterGroup Default = new CollisionWithReturns_1MethodInvocationImposterGroup(new CollisionWithReturns_1ArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal CollisionWithReturns_1ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollisionWithReturns_1MethodInvocationImposterGroup(CollisionWithReturns_1ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(int value)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(value);
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

                private CollisionWithReturns_1Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturns_1CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturns_1CallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(int value)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(value);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }
                }

                internal void Callback(CollisionWithReturns_1CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CollisionWithReturns_1Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value_1)
                {
                    _resultGenerator = (int value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal static int DefaultResultGenerator(int value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturns_1MethodInvocationImposterBuilder
        {
            ICollisionWithReturns_1MethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturns_1MethodInvocationImposterBuilder Throws(System.Exception exception);
            ICollisionWithReturns_1MethodInvocationImposterBuilder Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithReturns_1MethodInvocationImposterBuilder Callback(CollisionWithReturns_1CallbackDelegate callback);
            ICollisionWithReturns_1MethodInvocationImposterBuilder Returns(CollisionWithReturns_1Delegate resultGenerator);
            ICollisionWithReturns_1MethodInvocationImposterBuilder Returns(int value_1);
            ICollisionWithReturns_1MethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturns_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public interface ICollisionWithReturns_1MethodImposterBuilder : ICollisionWithReturns_1MethodInvocationImposterBuilder, CollisionWithReturns_1MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_1MethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_1MethodInvocationImposterGroup>();
            private readonly CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection;
            public CollisionWithReturns_1MethodImposter(CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection)
            {
                this._collisionWithReturns_1MethodInvocationHistoryCollection = _collisionWithReturns_1MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithReturns_1Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollisionWithReturns_1MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CollisionWithReturns_1Arguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? CollisionWithReturns_1MethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(value);
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
                private readonly CollisionWithReturns_1MethodInvocationImposterGroup _invocationImposterGroup;
                private CollisionWithReturns_1MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollisionWithReturns_1MethodImposter _imposter, CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection, CollisionWithReturns_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithReturns_1MethodInvocationHistoryCollection = _collisionWithReturns_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollisionWithReturns_1MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Callback(CollisionWithReturns_1CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Returns(CollisionWithReturns_1Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                ICollisionWithReturns_1MethodInvocationImposterBuilder ICollisionWithReturns_1MethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class CollisionWithReturns_2MethodInvocationImposterGroup
        {
            internal static CollisionWithReturns_2MethodInvocationImposterGroup Default = new CollisionWithReturns_2MethodInvocationImposterGroup(new CollisionWithReturns_2ArgumentsCriteria(Imposter.Abstractions.Arg<string>.Any()));
            internal CollisionWithReturns_2ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollisionWithReturns_2MethodInvocationImposterGroup(CollisionWithReturns_2ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(string result)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(result);
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

                private CollisionWithReturns_2Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturns_2CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturns_2CallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(string result)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(result);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(result);
                    }
                }

                internal void Callback(CollisionWithReturns_2CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CollisionWithReturns_2Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (string result) =>
                    {
                        return value;
                    };
                }

                internal void Throws(CollisionWithReturns_2ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (string result) =>
                    {
                        throw exceptionGenerator(result);
                    };
                }

                internal static int DefaultResultGenerator(string result)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturns_2MethodInvocationImposterBuilder
        {
            ICollisionWithReturns_2MethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturns_2MethodInvocationImposterBuilder Throws(System.Exception exception);
            ICollisionWithReturns_2MethodInvocationImposterBuilder Throws(CollisionWithReturns_2ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithReturns_2MethodInvocationImposterBuilder Callback(CollisionWithReturns_2CallbackDelegate callback);
            ICollisionWithReturns_2MethodInvocationImposterBuilder Returns(CollisionWithReturns_2Delegate resultGenerator);
            ICollisionWithReturns_2MethodInvocationImposterBuilder Returns(int value);
            ICollisionWithReturns_2MethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturns_2MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(string result)
        public interface ICollisionWithReturns_2MethodImposterBuilder : ICollisionWithReturns_2MethodInvocationImposterBuilder, CollisionWithReturns_2MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_2MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_2MethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_2MethodInvocationImposterGroup>();
            private readonly CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection;
            public CollisionWithReturns_2MethodImposter(CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection)
            {
                this._collisionWithReturns_2MethodInvocationHistoryCollection = _collisionWithReturns_2MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithReturns_2Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollisionWithReturns_2MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CollisionWithReturns_2Arguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? CollisionWithReturns_2MethodInvocationImposterGroup.Default;
                try
                {
                    var result_1 = matchingInvocationImposterGroup.Invoke(result);
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
                private readonly CollisionWithReturns_2MethodInvocationImposterGroup _invocationImposterGroup;
                private CollisionWithReturns_2MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollisionWithReturns_2MethodImposter _imposter, CollisionWithReturns_2MethodInvocationHistoryCollection _collisionWithReturns_2MethodInvocationHistoryCollection, CollisionWithReturns_2ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithReturns_2MethodInvocationHistoryCollection = _collisionWithReturns_2MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollisionWithReturns_2MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string result) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string result) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Throws(CollisionWithReturns_2ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string result) =>
                    {
                        throw exceptionGenerator.Invoke(result);
                    });
                    return this;
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Callback(CollisionWithReturns_2CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Returns(CollisionWithReturns_2Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ICollisionWithReturns_2MethodInvocationImposterBuilder ICollisionWithReturns_2MethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class CollisionWithReturns_3MethodInvocationImposterGroup
        {
            internal static CollisionWithReturns_3MethodInvocationImposterGroup Default = new CollisionWithReturns_3MethodInvocationImposterGroup(new CollisionWithReturns_3ArgumentsCriteria(Imposter.Abstractions.Arg<double>.Any()));
            internal CollisionWithReturns_3ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollisionWithReturns_3MethodInvocationImposterGroup(CollisionWithReturns_3ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(double matchingSetup)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(matchingSetup);
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

                private CollisionWithReturns_3Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturns_3CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollisionWithReturns_3CallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(double matchingSetup)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(matchingSetup);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(matchingSetup);
                    }
                }

                internal void Callback(CollisionWithReturns_3CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CollisionWithReturns_3Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (double matchingSetup) =>
                    {
                        return value;
                    };
                }

                internal void Throws(CollisionWithReturns_3ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (double matchingSetup) =>
                    {
                        throw exceptionGenerator(matchingSetup);
                    };
                }

                internal static int DefaultResultGenerator(double matchingSetup)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithReturns_3MethodInvocationImposterBuilder
        {
            ICollisionWithReturns_3MethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ICollisionWithReturns_3MethodInvocationImposterBuilder Throws(System.Exception exception);
            ICollisionWithReturns_3MethodInvocationImposterBuilder Throws(CollisionWithReturns_3ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithReturns_3MethodInvocationImposterBuilder Callback(CollisionWithReturns_3CallbackDelegate callback);
            ICollisionWithReturns_3MethodInvocationImposterBuilder Returns(CollisionWithReturns_3Delegate resultGenerator);
            ICollisionWithReturns_3MethodInvocationImposterBuilder Returns(int value);
            ICollisionWithReturns_3MethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithReturns_3MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(double matchingSetup)
        public interface ICollisionWithReturns_3MethodImposterBuilder : ICollisionWithReturns_3MethodInvocationImposterBuilder, CollisionWithReturns_3MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_3MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_3MethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithReturns_3MethodInvocationImposterGroup>();
            private readonly CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection;
            public CollisionWithReturns_3MethodImposter(CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection)
            {
                this._collisionWithReturns_3MethodInvocationHistoryCollection = _collisionWithReturns_3MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithReturns_3Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollisionWithReturns_3MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CollisionWithReturns_3Arguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? CollisionWithReturns_3MethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(matchingSetup);
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
                private readonly CollisionWithReturns_3MethodInvocationImposterGroup _invocationImposterGroup;
                private CollisionWithReturns_3MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollisionWithReturns_3MethodImposter _imposter, CollisionWithReturns_3MethodInvocationHistoryCollection _collisionWithReturns_3MethodInvocationHistoryCollection, CollisionWithReturns_3ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithReturns_3MethodInvocationHistoryCollection = _collisionWithReturns_3MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollisionWithReturns_3MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((double matchingSetup) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((double matchingSetup) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Throws(CollisionWithReturns_3ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((double matchingSetup) =>
                    {
                        throw exceptionGenerator.Invoke(matchingSetup);
                    });
                    return this;
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Callback(CollisionWithReturns_3CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Returns(CollisionWithReturns_3Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ICollisionWithReturns_3MethodInvocationImposterBuilder ICollisionWithReturns_3MethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class CollisionWithThrowsMethodInvocationImposterGroup<TValue>
        {
            internal static CollisionWithThrowsMethodInvocationImposterGroup<TValue> Default = new CollisionWithThrowsMethodInvocationImposterGroup<TValue>(new CollisionWithThrowsArgumentsCriteria<TValue>(Imposter.Abstractions.Arg<TValue>.Any()));
            internal CollisionWithThrowsArgumentsCriteria<TValue> ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollisionWithThrowsMethodInvocationImposterGroup(CollisionWithThrowsArgumentsCriteria<TValue> argumentsCriteria)
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

            public int Invoke(TValue ex)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(ex);
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

                private CollisionWithThrowsDelegate<TValue> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollisionWithThrowsCallbackDelegate<TValue>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollisionWithThrowsCallbackDelegate<TValue>>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(TValue ex)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(ex);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(ex);
                    }
                }

                internal void Callback(CollisionWithThrowsCallbackDelegate<TValue> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CollisionWithThrowsDelegate<TValue> resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (TValue ex) =>
                    {
                        return value;
                    };
                }

                internal void Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    _resultGenerator = (TValue ex) =>
                    {
                        throw exceptionGenerator(ex);
                    };
                }

                internal static int DefaultResultGenerator(TValue ex)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>
        {
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Throws<TException>()
                where TException : Exception, new();
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Throws(System.Exception exception);
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator);
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Callback(CollisionWithThrowsCallbackDelegate<TValue> callback);
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Returns(CollisionWithThrowsDelegate<TValue> resultGenerator);
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Returns(int value);
            ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> Then();
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
        public interface ICollisionWithThrowsMethodImposterBuilder<TValue> : ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>, CollisionWithThrowsMethodInvocationVerifier<TValue>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrowsMethodImposter<TValue> : ICollisionWithThrowsMethodImposter<TValue>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithThrowsMethodInvocationImposterGroup<TValue>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithThrowsMethodInvocationImposterGroup<TValue>>();
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
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollisionWithThrowsMethodInvocationImposterGroup<TValue>? FindMatchingInvocationImposterGroup(CollisionWithThrowsArguments<TValue> arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? CollisionWithThrowsMethodInvocationImposterGroup<TValue>.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(ex);
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
                private readonly CollisionWithThrowsMethodInvocationImposterGroup<TValue> _invocationImposterGroup;
                private CollisionWithThrowsMethodInvocationImposterGroup<TValue>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollisionWithThrowsMethodImposterCollection _imposterCollection, CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection, CollisionWithThrowsArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._collisionWithThrowsMethodInvocationHistoryCollection = _collisionWithThrowsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollisionWithThrowsMethodInvocationImposterGroup<TValue>(_argumentsCriteria);
                    CollisionWithThrowsMethodImposter<TValue> methodImposter = _imposterCollection.AddNew<TValue>();
                    methodImposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((TValue ex) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((TValue ex) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((TValue ex) =>
                    {
                        throw exceptionGenerator.Invoke(ex);
                    });
                    return this;
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Callback(CollisionWithThrowsCallbackDelegate<TValue> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Returns(CollisionWithThrowsDelegate<TValue> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ICollisionWithThrowsMethodInvocationImposterBuilder<TValue> ICollisionWithThrowsMethodInvocationImposterBuilder<TValue>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class CollisionWithThrows_1MethodInvocationImposterGroup
        {
            internal static CollisionWithThrows_1MethodInvocationImposterGroup Default = new CollisionWithThrows_1MethodInvocationImposterGroup(new CollisionWithThrows_1ArgumentsCriteria(Imposter.Abstractions.Arg<global::System.Exception>.Any()));
            internal CollisionWithThrows_1ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollisionWithThrows_1MethodInvocationImposterGroup(CollisionWithThrows_1ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::System.Exception ex)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(ex);
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

                private CollisionWithThrows_1Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollisionWithThrows_1CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollisionWithThrows_1CallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(global::System.Exception ex)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(ex);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(ex);
                    }
                }

                internal void Callback(CollisionWithThrows_1CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CollisionWithThrows_1Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (global::System.Exception ex) =>
                    {
                        return value;
                    };
                }

                internal void Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (global::System.Exception ex) =>
                    {
                        throw exceptionGenerator(ex);
                    };
                }

                internal static int DefaultResultGenerator(global::System.Exception ex)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollisionWithThrows_1MethodInvocationImposterBuilder
        {
            ICollisionWithThrows_1MethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ICollisionWithThrows_1MethodInvocationImposterBuilder Throws(System.Exception exception);
            ICollisionWithThrows_1MethodInvocationImposterBuilder Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator);
            ICollisionWithThrows_1MethodInvocationImposterBuilder Callback(CollisionWithThrows_1CallbackDelegate callback);
            ICollisionWithThrows_1MethodInvocationImposterBuilder Returns(CollisionWithThrows_1Delegate resultGenerator);
            ICollisionWithThrows_1MethodInvocationImposterBuilder Returns(int value);
            ICollisionWithThrows_1MethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollisionWithThrows_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public interface ICollisionWithThrows_1MethodImposterBuilder : ICollisionWithThrows_1MethodInvocationImposterBuilder, CollisionWithThrows_1MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrows_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollisionWithThrows_1MethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<CollisionWithThrows_1MethodInvocationImposterGroup>();
            private readonly CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection;
            public CollisionWithThrows_1MethodImposter(CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection)
            {
                this._collisionWithThrows_1MethodInvocationHistoryCollection = _collisionWithThrows_1MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(CollisionWithThrows_1Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollisionWithThrows_1MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CollisionWithThrows_1Arguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? CollisionWithThrows_1MethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(ex);
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
                private readonly CollisionWithThrows_1MethodInvocationImposterGroup _invocationImposterGroup;
                private CollisionWithThrows_1MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollisionWithThrows_1MethodImposter _imposter, CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection, CollisionWithThrows_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collisionWithThrows_1MethodInvocationHistoryCollection = _collisionWithThrows_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollisionWithThrows_1MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((global::System.Exception ex) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((global::System.Exception ex) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((global::System.Exception ex) =>
                    {
                        throw exceptionGenerator.Invoke(ex);
                    });
                    return this;
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Callback(CollisionWithThrows_1CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Returns(CollisionWithThrows_1Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ICollisionWithThrows_1MethodInvocationImposterBuilder ICollisionWithThrows_1MethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup
        {
            internal static MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup Default = new MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup();
            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup()
            {
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

            public int Invoke()
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke();
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

                private MethodWithSameNameDifferentSignatureDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<MethodWithSameNameDifferentSignatureCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<MethodWithSameNameDifferentSignatureCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke()
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke();
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }
                }

                internal void Callback(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = () =>
                    {
                        return value;
                    };
                }

                internal void Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal static int DefaultResultGenerator()
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder
        {
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Throws(System.Exception exception);
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator);
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Callback(MethodWithSameNameDifferentSignatureCallbackDelegate callback);
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator);
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Returns(int value);
            IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodWithSameNameDifferentSignatureMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public interface IMethodWithSameNameDifferentSignatureMethodImposterBuilder : IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder, MethodWithSameNameDifferentSignatureMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignatureMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup>();
            private readonly MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
            public MethodWithSameNameDifferentSignatureMethodImposter(MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection)
            {
                this._methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection = _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup() ?? MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke();
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
                private readonly MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup _invocationImposterGroup;
                private MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(MethodWithSameNameDifferentSignatureMethodImposter _imposter, MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection = _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new MethodWithSameNameDifferentSignatureMethodInvocationImposterGroup();
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Callback(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder IMethodWithSameNameDifferentSignatureMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup
        {
            internal static MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup Default = new MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup(new MethodWithSameNameDifferentSignature_1ArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal MethodWithSameNameDifferentSignature_1ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup(MethodWithSameNameDifferentSignature_1ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(int input)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(input);
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

                private MethodWithSameNameDifferentSignature_1Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<MethodWithSameNameDifferentSignature_1CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<MethodWithSameNameDifferentSignature_1CallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(int input)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(input);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(input);
                    }
                }

                internal void Callback(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int input) =>
                    {
                        return value;
                    };
                }

                internal void Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int input) =>
                    {
                        throw exceptionGenerator(input);
                    };
                }

                internal static int DefaultResultGenerator(int input)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder
        {
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Throws(System.Exception exception);
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator);
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Callback(MethodWithSameNameDifferentSignature_1CallbackDelegate callback);
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator);
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Returns(int value);
            IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodWithSameNameDifferentSignature_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public interface IMethodWithSameNameDifferentSignature_1MethodImposterBuilder : IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder, MethodWithSameNameDifferentSignature_1MethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup>();
            private readonly MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
            public MethodWithSameNameDifferentSignature_1MethodImposter(MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection)
            {
                this._methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(MethodWithSameNameDifferentSignature_1Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(MethodWithSameNameDifferentSignature_1Arguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(input);
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
                private readonly MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup _invocationImposterGroup;
                private MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(MethodWithSameNameDifferentSignature_1MethodImposter _imposter, MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection, MethodWithSameNameDifferentSignature_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new MethodWithSameNameDifferentSignature_1MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw exceptionGenerator.Invoke(input);
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Callback(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder IMethodWithSameNameDifferentSignature_1MethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>
        {
            internal static MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T> Default = new MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>(new MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T>(Imposter.Abstractions.Arg<T>.Any()));
            internal MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> argumentsCriteria)
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

            public int Invoke(T input)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(input);
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

                private MethodWithSameNameDifferentSignature_2Delegate<T> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<MethodWithSameNameDifferentSignature_2CallbackDelegate<T>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<MethodWithSameNameDifferentSignature_2CallbackDelegate<T>>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(T input)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    int result = _resultGenerator.Invoke(input);
                    return result;
                    foreach (var callback in _callbacks)
                    {
                        callback(input);
                    }
                }

                internal void Callback(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (T input) =>
                    {
                        return value;
                    };
                }

                internal void Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator)
                {
                    _resultGenerator = (T input) =>
                    {
                        throw exceptionGenerator(input);
                    };
                }

                internal static int DefaultResultGenerator(T input)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>
        {
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Throws<TException>()
                where TException : Exception, new();
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Throws(System.Exception exception);
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator);
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Callback(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback);
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator);
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Returns(int value);
            IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> Then();
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
        public interface IMethodWithSameNameDifferentSignature_2MethodImposterBuilder<T> : IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>, MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_2MethodImposter<T> : IMethodWithSameNameDifferentSignature_2MethodImposter<T>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>>();
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
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>? FindMatchingInvocationImposterGroup(MethodWithSameNameDifferentSignature_2Arguments<T> arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(input);
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
                private readonly MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T> _invocationImposterGroup;
                private MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(MethodWithSameNameDifferentSignature_2MethodImposterCollection _imposterCollection, MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection, MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection = _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new MethodWithSameNameDifferentSignature_2MethodInvocationImposterGroup<T>(_argumentsCriteria);
                    MethodWithSameNameDifferentSignature_2MethodImposter<T> methodImposter = _imposterCollection.AddNew<T>();
                    methodImposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((T input) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((T input) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((T input) =>
                    {
                        throw exceptionGenerator.Invoke(input);
                    });
                    return this;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Callback(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T> IMethodWithSameNameDifferentSignature_2MethodInvocationImposterBuilder<T>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class DefaultInvocationSetupMethodInvocationImposterGroup
        {
            internal static DefaultInvocationSetupMethodInvocationImposterGroup Default = new DefaultInvocationSetupMethodInvocationImposterGroup(new DefaultInvocationSetupArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal DefaultInvocationSetupArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public DefaultInvocationSetupMethodInvocationImposterGroup(DefaultInvocationSetupArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int DefaultInvocationSetup)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(DefaultInvocationSetup);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private DefaultInvocationSetupDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<DefaultInvocationSetupCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<DefaultInvocationSetupCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int DefaultInvocationSetup)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(DefaultInvocationSetup);
                    foreach (var callback in _callbacks)
                    {
                        callback(DefaultInvocationSetup);
                    }
                }

                internal void Callback(DefaultInvocationSetupCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(DefaultInvocationSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int DefaultInvocationSetup) =>
                    {
                        throw exceptionGenerator(DefaultInvocationSetup);
                    };
                }

                internal static void DefaultResultGenerator(int DefaultInvocationSetup)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultInvocationSetupMethodInvocationImposterBuilder
        {
            IDefaultInvocationSetupMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IDefaultInvocationSetupMethodInvocationImposterBuilder Throws(System.Exception exception);
            IDefaultInvocationSetupMethodInvocationImposterBuilder Throws(DefaultInvocationSetupExceptionGeneratorDelegate exceptionGenerator);
            IDefaultInvocationSetupMethodInvocationImposterBuilder Callback(DefaultInvocationSetupCallbackDelegate callback);
            IDefaultInvocationSetupMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface DefaultInvocationSetupMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.DefaultInvocationSetup(int DefaultInvocationSetup)
        public interface IDefaultInvocationSetupMethodImposterBuilder : IDefaultInvocationSetupMethodInvocationImposterBuilder, DefaultInvocationSetupMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultInvocationSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<DefaultInvocationSetupMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<DefaultInvocationSetupMethodInvocationImposterGroup>();
            private readonly DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection;
            public DefaultInvocationSetupMethodImposter(DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection)
            {
                this._defaultInvocationSetupMethodInvocationHistoryCollection = _defaultInvocationSetupMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(DefaultInvocationSetupArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private DefaultInvocationSetupMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(DefaultInvocationSetupArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? DefaultInvocationSetupMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(DefaultInvocationSetup);
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
                private readonly DefaultInvocationSetupMethodInvocationImposterGroup _invocationImposterGroup;
                private DefaultInvocationSetupMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(DefaultInvocationSetupMethodImposter _imposter, DefaultInvocationSetupMethodInvocationHistoryCollection _defaultInvocationSetupMethodInvocationHistoryCollection, DefaultInvocationSetupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._defaultInvocationSetupMethodInvocationHistoryCollection = _defaultInvocationSetupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new DefaultInvocationSetupMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDefaultInvocationSetupMethodInvocationImposterBuilder IDefaultInvocationSetupMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int DefaultInvocationSetup) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDefaultInvocationSetupMethodInvocationImposterBuilder IDefaultInvocationSetupMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int DefaultInvocationSetup) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDefaultInvocationSetupMethodInvocationImposterBuilder IDefaultInvocationSetupMethodInvocationImposterBuilder.Throws(DefaultInvocationSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int DefaultInvocationSetup) =>
                    {
                        throw exceptionGenerator.Invoke(DefaultInvocationSetup);
                    });
                    return this;
                }

                IDefaultInvocationSetupMethodInvocationImposterBuilder IDefaultInvocationSetupMethodInvocationImposterBuilder.Callback(DefaultInvocationSetupCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDefaultInvocationSetupMethodInvocationImposterBuilder IDefaultInvocationSetupMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class _invocationSetupsMethodInvocationImposterGroup
        {
            internal static _invocationSetupsMethodInvocationImposterGroup Default = new _invocationSetupsMethodInvocationImposterGroup(new _invocationSetupsArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal _invocationSetupsArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public _invocationSetupsMethodInvocationImposterGroup(_invocationSetupsArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int _invocationSetups)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(_invocationSetups);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private _invocationSetupsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<_invocationSetupsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<_invocationSetupsCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int _invocationSetups)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(_invocationSetups);
                    foreach (var callback in _callbacks)
                    {
                        callback(_invocationSetups);
                    }
                }

                internal void Callback(_invocationSetupsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(_invocationSetupsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int _invocationSetups) =>
                    {
                        throw exceptionGenerator(_invocationSetups);
                    };
                }

                internal static void DefaultResultGenerator(int _invocationSetups)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationSetupsMethodInvocationImposterBuilder
        {
            I_invocationSetupsMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            I_invocationSetupsMethodInvocationImposterBuilder Throws(System.Exception exception);
            I_invocationSetupsMethodInvocationImposterBuilder Throws(_invocationSetupsExceptionGeneratorDelegate exceptionGenerator);
            I_invocationSetupsMethodInvocationImposterBuilder Callback(_invocationSetupsCallbackDelegate callback);
            I_invocationSetupsMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface _invocationSetupsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut._invocationSetups(int _invocationSetups)
        public interface I_invocationSetupsMethodImposterBuilder : I_invocationSetupsMethodInvocationImposterBuilder, _invocationSetupsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationSetupsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<_invocationSetupsMethodInvocationImposterGroup> _invocationSetups_1 = new System.Collections.Concurrent.ConcurrentStack<_invocationSetupsMethodInvocationImposterGroup>();
            private readonly _invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection;
            public _invocationSetupsMethodImposter(_invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection)
            {
                this.__invocationSetupsMethodInvocationHistoryCollection = __invocationSetupsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(_invocationSetupsArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private _invocationSetupsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(_invocationSetupsArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? _invocationSetupsMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationSetups);
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
                private readonly _invocationSetupsMethodInvocationImposterGroup _invocationImposterGroup;
                private _invocationSetupsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(_invocationSetupsMethodImposter _imposter, _invocationSetupsMethodInvocationHistoryCollection __invocationSetupsMethodInvocationHistoryCollection, _invocationSetupsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this.__invocationSetupsMethodInvocationHistoryCollection = __invocationSetupsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new _invocationSetupsMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups_1.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                I_invocationSetupsMethodInvocationImposterBuilder I_invocationSetupsMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int _invocationSetups) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                I_invocationSetupsMethodInvocationImposterBuilder I_invocationSetupsMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int _invocationSetups) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                I_invocationSetupsMethodInvocationImposterBuilder I_invocationSetupsMethodInvocationImposterBuilder.Throws(_invocationSetupsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int _invocationSetups) =>
                    {
                        throw exceptionGenerator.Invoke(_invocationSetups);
                    });
                    return this;
                }

                I_invocationSetupsMethodInvocationImposterBuilder I_invocationSetupsMethodInvocationImposterBuilder.Callback(_invocationSetupsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                I_invocationSetupsMethodInvocationImposterBuilder I_invocationSetupsMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class itMethodInvocationImposterGroup
        {
            internal static itMethodInvocationImposterGroup Default = new itMethodInvocationImposterGroup(new itArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal itArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public itMethodInvocationImposterGroup(itArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int it)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(it);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private itDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<itCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<itCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int it)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(it);
                    foreach (var callback in _callbacks)
                    {
                        callback(it);
                    }
                }

                internal void Callback(itCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(itExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int it) =>
                    {
                        throw exceptionGenerator(it);
                    };
                }

                internal static void DefaultResultGenerator(int it)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IitMethodInvocationImposterBuilder
        {
            IitMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IitMethodInvocationImposterBuilder Throws(System.Exception exception);
            IitMethodInvocationImposterBuilder Throws(itExceptionGeneratorDelegate exceptionGenerator);
            IitMethodInvocationImposterBuilder Callback(itCallbackDelegate callback);
            IitMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface itMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.it(int it)
        public interface IitMethodImposterBuilder : IitMethodInvocationImposterBuilder, itMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class itMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<itMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<itMethodInvocationImposterGroup>();
            private readonly itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection;
            public itMethodImposter(itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection)
            {
                this._itMethodInvocationHistoryCollection = _itMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(itArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private itMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(itArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? itMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(it);
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
                private readonly itMethodInvocationImposterGroup _invocationImposterGroup;
                private itMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(itMethodImposter _imposter, itMethodInvocationHistoryCollection _itMethodInvocationHistoryCollection, itArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._itMethodInvocationHistoryCollection = _itMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new itMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IitMethodInvocationImposterBuilder IitMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int it) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IitMethodInvocationImposterBuilder IitMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int it) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IitMethodInvocationImposterBuilder IitMethodInvocationImposterBuilder.Throws(itExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int it) =>
                    {
                        throw exceptionGenerator.Invoke(it);
                    });
                    return this;
                }

                IitMethodInvocationImposterBuilder IitMethodInvocationImposterBuilder.Callback(itCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IitMethodInvocationImposterBuilder IitMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class invocationHistoryMethodInvocationImposterGroup
        {
            internal static invocationHistoryMethodInvocationImposterGroup Default = new invocationHistoryMethodInvocationImposterGroup(new invocationHistoryArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal invocationHistoryArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public invocationHistoryMethodInvocationImposterGroup(invocationHistoryArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int invocationHistory)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(invocationHistory);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private invocationHistoryDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<invocationHistoryCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<invocationHistoryCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int invocationHistory)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(invocationHistory);
                    foreach (var callback in _callbacks)
                    {
                        callback(invocationHistory);
                    }
                }

                internal void Callback(invocationHistoryCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(invocationHistoryExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int invocationHistory) =>
                    {
                        throw exceptionGenerator(invocationHistory);
                    };
                }

                internal static void DefaultResultGenerator(int invocationHistory)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IinvocationHistoryMethodInvocationImposterBuilder
        {
            IinvocationHistoryMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IinvocationHistoryMethodInvocationImposterBuilder Throws(System.Exception exception);
            IinvocationHistoryMethodInvocationImposterBuilder Throws(invocationHistoryExceptionGeneratorDelegate exceptionGenerator);
            IinvocationHistoryMethodInvocationImposterBuilder Callback(invocationHistoryCallbackDelegate callback);
            IinvocationHistoryMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface invocationHistoryMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.invocationHistory(int invocationHistory)
        public interface IinvocationHistoryMethodImposterBuilder : IinvocationHistoryMethodInvocationImposterBuilder, invocationHistoryMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class invocationHistoryMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<invocationHistoryMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<invocationHistoryMethodInvocationImposterGroup>();
            private readonly invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection;
            public invocationHistoryMethodImposter(invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection)
            {
                this._invocationHistoryMethodInvocationHistoryCollection = _invocationHistoryMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(invocationHistoryArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private invocationHistoryMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(invocationHistoryArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? invocationHistoryMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(invocationHistory);
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
                private readonly invocationHistoryMethodInvocationImposterGroup _invocationImposterGroup;
                private invocationHistoryMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(invocationHistoryMethodImposter _imposter, invocationHistoryMethodInvocationHistoryCollection _invocationHistoryMethodInvocationHistoryCollection, invocationHistoryArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._invocationHistoryMethodInvocationHistoryCollection = _invocationHistoryMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new invocationHistoryMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IinvocationHistoryMethodInvocationImposterBuilder IinvocationHistoryMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int invocationHistory) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IinvocationHistoryMethodInvocationImposterBuilder IinvocationHistoryMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int invocationHistory) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IinvocationHistoryMethodInvocationImposterBuilder IinvocationHistoryMethodInvocationImposterBuilder.Throws(invocationHistoryExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int invocationHistory) =>
                    {
                        throw exceptionGenerator.Invoke(invocationHistory);
                    });
                    return this;
                }

                IinvocationHistoryMethodInvocationImposterBuilder IinvocationHistoryMethodInvocationImposterBuilder.Callback(invocationHistoryCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IinvocationHistoryMethodInvocationImposterBuilder IinvocationHistoryMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class nextSetupMethodInvocationImposterGroup
        {
            internal static nextSetupMethodInvocationImposterGroup Default = new nextSetupMethodInvocationImposterGroup(new nextSetupArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal nextSetupArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public nextSetupMethodInvocationImposterGroup(nextSetupArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int nextSetup)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(nextSetup);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private nextSetupDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<nextSetupCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<nextSetupCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int nextSetup)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(nextSetup);
                    foreach (var callback in _callbacks)
                    {
                        callback(nextSetup);
                    }
                }

                internal void Callback(nextSetupCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(nextSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int nextSetup) =>
                    {
                        throw exceptionGenerator(nextSetup);
                    };
                }

                internal static void DefaultResultGenerator(int nextSetup)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InextSetupMethodInvocationImposterBuilder
        {
            InextSetupMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            InextSetupMethodInvocationImposterBuilder Throws(System.Exception exception);
            InextSetupMethodInvocationImposterBuilder Throws(nextSetupExceptionGeneratorDelegate exceptionGenerator);
            InextSetupMethodInvocationImposterBuilder Callback(nextSetupCallbackDelegate callback);
            InextSetupMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface nextSetupMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.nextSetup(int nextSetup)
        public interface InextSetupMethodImposterBuilder : InextSetupMethodInvocationImposterBuilder, nextSetupMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class nextSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<nextSetupMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<nextSetupMethodInvocationImposterGroup>();
            private readonly nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection;
            public nextSetupMethodImposter(nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection)
            {
                this._nextSetupMethodInvocationHistoryCollection = _nextSetupMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(nextSetupArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private nextSetupMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(nextSetupArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? nextSetupMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(nextSetup);
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
                private readonly nextSetupMethodInvocationImposterGroup _invocationImposterGroup;
                private nextSetupMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(nextSetupMethodImposter _imposter, nextSetupMethodInvocationHistoryCollection _nextSetupMethodInvocationHistoryCollection, nextSetupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._nextSetupMethodInvocationHistoryCollection = _nextSetupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new nextSetupMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                InextSetupMethodInvocationImposterBuilder InextSetupMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int nextSetup) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                InextSetupMethodInvocationImposterBuilder InextSetupMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int nextSetup) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                InextSetupMethodInvocationImposterBuilder InextSetupMethodInvocationImposterBuilder.Throws(nextSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int nextSetup) =>
                    {
                        throw exceptionGenerator.Invoke(nextSetup);
                    });
                    return this;
                }

                InextSetupMethodInvocationImposterBuilder InextSetupMethodInvocationImposterBuilder.Callback(nextSetupCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                InextSetupMethodInvocationImposterBuilder InextSetupMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class GetNextSetupMethodInvocationImposterGroup
        {
            internal static GetNextSetupMethodInvocationImposterGroup Default = new GetNextSetupMethodInvocationImposterGroup(new GetNextSetupArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal GetNextSetupArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public GetNextSetupMethodInvocationImposterGroup(GetNextSetupArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int GetNextSetup)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(GetNextSetup);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private GetNextSetupDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<GetNextSetupCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<GetNextSetupCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int GetNextSetup)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(GetNextSetup);
                    foreach (var callback in _callbacks)
                    {
                        callback(GetNextSetup);
                    }
                }

                internal void Callback(GetNextSetupCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(GetNextSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int GetNextSetup) =>
                    {
                        throw exceptionGenerator(GetNextSetup);
                    };
                }

                internal static void DefaultResultGenerator(int GetNextSetup)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGetNextSetupMethodInvocationImposterBuilder
        {
            IGetNextSetupMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IGetNextSetupMethodInvocationImposterBuilder Throws(System.Exception exception);
            IGetNextSetupMethodInvocationImposterBuilder Throws(GetNextSetupExceptionGeneratorDelegate exceptionGenerator);
            IGetNextSetupMethodInvocationImposterBuilder Callback(GetNextSetupCallbackDelegate callback);
            IGetNextSetupMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GetNextSetupMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.GetNextSetup(int GetNextSetup)
        public interface IGetNextSetupMethodImposterBuilder : IGetNextSetupMethodInvocationImposterBuilder, GetNextSetupMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GetNextSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GetNextSetupMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GetNextSetupMethodInvocationImposterGroup>();
            private readonly GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection;
            public GetNextSetupMethodImposter(GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection)
            {
                this._getNextSetupMethodInvocationHistoryCollection = _getNextSetupMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(GetNextSetupArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private GetNextSetupMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(GetNextSetupArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? GetNextSetupMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(GetNextSetup);
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
                private readonly GetNextSetupMethodInvocationImposterGroup _invocationImposterGroup;
                private GetNextSetupMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(GetNextSetupMethodImposter _imposter, GetNextSetupMethodInvocationHistoryCollection _getNextSetupMethodInvocationHistoryCollection, GetNextSetupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._getNextSetupMethodInvocationHistoryCollection = _getNextSetupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new GetNextSetupMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IGetNextSetupMethodInvocationImposterBuilder IGetNextSetupMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int GetNextSetup) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IGetNextSetupMethodInvocationImposterBuilder IGetNextSetupMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int GetNextSetup) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IGetNextSetupMethodInvocationImposterBuilder IGetNextSetupMethodInvocationImposterBuilder.Throws(GetNextSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int GetNextSetup) =>
                    {
                        throw exceptionGenerator.Invoke(GetNextSetup);
                    });
                    return this;
                }

                IGetNextSetupMethodInvocationImposterBuilder IGetNextSetupMethodInvocationImposterBuilder.Callback(GetNextSetupCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IGetNextSetupMethodInvocationImposterBuilder IGetNextSetupMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class exceptionGeneratorMethodInvocationImposterGroup
        {
            internal static exceptionGeneratorMethodInvocationImposterGroup Default = new exceptionGeneratorMethodInvocationImposterGroup(new exceptionGeneratorArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal exceptionGeneratorArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public exceptionGeneratorMethodInvocationImposterGroup(exceptionGeneratorArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int exceptionGenerator)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(exceptionGenerator);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private exceptionGeneratorDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<exceptionGeneratorCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<exceptionGeneratorCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int exceptionGenerator)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(exceptionGenerator);
                    foreach (var callback in _callbacks)
                    {
                        callback(exceptionGenerator);
                    }
                }

                internal void Callback(exceptionGeneratorCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(exceptionGeneratorExceptionGeneratorDelegate exceptionGenerator_1)
                {
                    _resultGenerator = (int exceptionGenerator) =>
                    {
                        throw exceptionGenerator_1(exceptionGenerator);
                    };
                }

                internal static void DefaultResultGenerator(int exceptionGenerator)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IexceptionGeneratorMethodInvocationImposterBuilder
        {
            IexceptionGeneratorMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IexceptionGeneratorMethodInvocationImposterBuilder Throws(System.Exception exception);
            IexceptionGeneratorMethodInvocationImposterBuilder Throws(exceptionGeneratorExceptionGeneratorDelegate exceptionGenerator_1);
            IexceptionGeneratorMethodInvocationImposterBuilder Callback(exceptionGeneratorCallbackDelegate callback);
            IexceptionGeneratorMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface exceptionGeneratorMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.exceptionGenerator(int exceptionGenerator)
        public interface IexceptionGeneratorMethodImposterBuilder : IexceptionGeneratorMethodInvocationImposterBuilder, exceptionGeneratorMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class exceptionGeneratorMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<exceptionGeneratorMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<exceptionGeneratorMethodInvocationImposterGroup>();
            private readonly exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection;
            public exceptionGeneratorMethodImposter(exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection)
            {
                this._exceptionGeneratorMethodInvocationHistoryCollection = _exceptionGeneratorMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(exceptionGeneratorArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private exceptionGeneratorMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(exceptionGeneratorArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? exceptionGeneratorMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(exceptionGenerator);
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
                private readonly exceptionGeneratorMethodInvocationImposterGroup _invocationImposterGroup;
                private exceptionGeneratorMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(exceptionGeneratorMethodImposter _imposter, exceptionGeneratorMethodInvocationHistoryCollection _exceptionGeneratorMethodInvocationHistoryCollection, exceptionGeneratorArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._exceptionGeneratorMethodInvocationHistoryCollection = _exceptionGeneratorMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new exceptionGeneratorMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IexceptionGeneratorMethodInvocationImposterBuilder IexceptionGeneratorMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int exceptionGenerator) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IexceptionGeneratorMethodInvocationImposterBuilder IexceptionGeneratorMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int exceptionGenerator) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IexceptionGeneratorMethodInvocationImposterBuilder IexceptionGeneratorMethodInvocationImposterBuilder.Throws(exceptionGeneratorExceptionGeneratorDelegate exceptionGenerator_1)
                {
                    _currentInvocationImposter.Throws((int exceptionGenerator) =>
                    {
                        throw exceptionGenerator_1.Invoke(exceptionGenerator);
                    });
                    return this;
                }

                IexceptionGeneratorMethodInvocationImposterBuilder IexceptionGeneratorMethodInvocationImposterBuilder.Callback(exceptionGeneratorCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IexceptionGeneratorMethodInvocationImposterBuilder IexceptionGeneratorMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class callbackMethodInvocationImposterGroup
        {
            internal static callbackMethodInvocationImposterGroup Default = new callbackMethodInvocationImposterGroup(new callbackArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal callbackArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public callbackMethodInvocationImposterGroup(callbackArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int callback)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(callback);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private callbackDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<callbackCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<callbackCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int callback)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(callback);
                    foreach (var callback in _callbacks)
                    {
                        callback(callback);
                    }
                }

                internal void Callback(callbackCallbackDelegate callback_1)
                {
                    _callbacks.Enqueue(callback_1);
                }

                internal void Throws(callbackExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int callback) =>
                    {
                        throw exceptionGenerator(callback);
                    };
                }

                internal static void DefaultResultGenerator(int callback)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IcallbackMethodInvocationImposterBuilder
        {
            IcallbackMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IcallbackMethodInvocationImposterBuilder Throws(System.Exception exception);
            IcallbackMethodInvocationImposterBuilder Throws(callbackExceptionGeneratorDelegate exceptionGenerator);
            IcallbackMethodInvocationImposterBuilder Callback(callbackCallbackDelegate callback_1);
            IcallbackMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface callbackMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.callback(int callback)
        public interface IcallbackMethodImposterBuilder : IcallbackMethodInvocationImposterBuilder, callbackMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class callbackMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<callbackMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<callbackMethodInvocationImposterGroup>();
            private readonly callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection;
            public callbackMethodImposter(callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection)
            {
                this._callbackMethodInvocationHistoryCollection = _callbackMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(callbackArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private callbackMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(callbackArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? callbackMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(callback);
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
                private readonly callbackMethodInvocationImposterGroup _invocationImposterGroup;
                private callbackMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(callbackMethodImposter _imposter, callbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection, callbackArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._callbackMethodInvocationHistoryCollection = _callbackMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new callbackMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IcallbackMethodInvocationImposterBuilder IcallbackMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int callback) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IcallbackMethodInvocationImposterBuilder IcallbackMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int callback) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IcallbackMethodInvocationImposterBuilder IcallbackMethodInvocationImposterBuilder.Throws(callbackExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int callback) =>
                    {
                        throw exceptionGenerator.Invoke(callback);
                    });
                    return this;
                }

                IcallbackMethodInvocationImposterBuilder IcallbackMethodInvocationImposterBuilder.Callback(callbackCallbackDelegate callback_1)
                {
                    _currentInvocationImposter.Callback(callback_1);
                    return this;
                }

                IcallbackMethodInvocationImposterBuilder IcallbackMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class resultGeneratorMethodInvocationImposterGroup
        {
            internal static resultGeneratorMethodInvocationImposterGroup Default = new resultGeneratorMethodInvocationImposterGroup(new resultGeneratorArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal resultGeneratorArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public resultGeneratorMethodInvocationImposterGroup(resultGeneratorArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int resultGenerator)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(resultGenerator);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private resultGeneratorDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<resultGeneratorCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<resultGeneratorCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int resultGenerator)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(resultGenerator);
                    foreach (var callback in _callbacks)
                    {
                        callback(resultGenerator);
                    }
                }

                internal void Callback(resultGeneratorCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(resultGeneratorExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int resultGenerator) =>
                    {
                        throw exceptionGenerator(resultGenerator);
                    };
                }

                internal static void DefaultResultGenerator(int resultGenerator)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IresultGeneratorMethodInvocationImposterBuilder
        {
            IresultGeneratorMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IresultGeneratorMethodInvocationImposterBuilder Throws(System.Exception exception);
            IresultGeneratorMethodInvocationImposterBuilder Throws(resultGeneratorExceptionGeneratorDelegate exceptionGenerator);
            IresultGeneratorMethodInvocationImposterBuilder Callback(resultGeneratorCallbackDelegate callback);
            IresultGeneratorMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface resultGeneratorMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.resultGenerator(int resultGenerator)
        public interface IresultGeneratorMethodImposterBuilder : IresultGeneratorMethodInvocationImposterBuilder, resultGeneratorMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class resultGeneratorMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<resultGeneratorMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<resultGeneratorMethodInvocationImposterGroup>();
            private readonly resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection;
            public resultGeneratorMethodImposter(resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection)
            {
                this._resultGeneratorMethodInvocationHistoryCollection = _resultGeneratorMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(resultGeneratorArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private resultGeneratorMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(resultGeneratorArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? resultGeneratorMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(resultGenerator);
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
                private readonly resultGeneratorMethodInvocationImposterGroup _invocationImposterGroup;
                private resultGeneratorMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(resultGeneratorMethodImposter _imposter, resultGeneratorMethodInvocationHistoryCollection _resultGeneratorMethodInvocationHistoryCollection, resultGeneratorArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._resultGeneratorMethodInvocationHistoryCollection = _resultGeneratorMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new resultGeneratorMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IresultGeneratorMethodInvocationImposterBuilder IresultGeneratorMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int resultGenerator) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IresultGeneratorMethodInvocationImposterBuilder IresultGeneratorMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int resultGenerator) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IresultGeneratorMethodInvocationImposterBuilder IresultGeneratorMethodInvocationImposterBuilder.Throws(resultGeneratorExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int resultGenerator) =>
                    {
                        throw exceptionGenerator.Invoke(resultGenerator);
                    });
                    return this;
                }

                IresultGeneratorMethodInvocationImposterBuilder IresultGeneratorMethodInvocationImposterBuilder.Callback(resultGeneratorCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IresultGeneratorMethodInvocationImposterBuilder IresultGeneratorMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class argumentsCriteriaMethodInvocationImposterGroup
        {
            internal static argumentsCriteriaMethodInvocationImposterGroup Default = new argumentsCriteriaMethodInvocationImposterGroup(new argumentsCriteriaArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal argumentsCriteriaArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public argumentsCriteriaMethodInvocationImposterGroup(argumentsCriteriaArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int argumentsCriteria)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(argumentsCriteria);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private argumentsCriteriaDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<argumentsCriteriaCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<argumentsCriteriaCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int argumentsCriteria)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(argumentsCriteria);
                    foreach (var callback in _callbacks)
                    {
                        callback(argumentsCriteria);
                    }
                }

                internal void Callback(argumentsCriteriaCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(argumentsCriteriaExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int argumentsCriteria) =>
                    {
                        throw exceptionGenerator(argumentsCriteria);
                    };
                }

                internal static void DefaultResultGenerator(int argumentsCriteria)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IargumentsCriteriaMethodInvocationImposterBuilder
        {
            IargumentsCriteriaMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IargumentsCriteriaMethodInvocationImposterBuilder Throws(System.Exception exception);
            IargumentsCriteriaMethodInvocationImposterBuilder Throws(argumentsCriteriaExceptionGeneratorDelegate exceptionGenerator);
            IargumentsCriteriaMethodInvocationImposterBuilder Callback(argumentsCriteriaCallbackDelegate callback);
            IargumentsCriteriaMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface argumentsCriteriaMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.argumentsCriteria(int argumentsCriteria)
        public interface IargumentsCriteriaMethodImposterBuilder : IargumentsCriteriaMethodInvocationImposterBuilder, argumentsCriteriaMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class argumentsCriteriaMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<argumentsCriteriaMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<argumentsCriteriaMethodInvocationImposterGroup>();
            private readonly argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection;
            public argumentsCriteriaMethodImposter(argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection)
            {
                this._argumentsCriteriaMethodInvocationHistoryCollection = _argumentsCriteriaMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(argumentsCriteriaArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private argumentsCriteriaMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(argumentsCriteriaArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? argumentsCriteriaMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(argumentsCriteria);
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
                private readonly argumentsCriteriaMethodInvocationImposterGroup _invocationImposterGroup;
                private argumentsCriteriaMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(argumentsCriteriaMethodImposter _imposter, argumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection, argumentsCriteriaArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._argumentsCriteriaMethodInvocationHistoryCollection = _argumentsCriteriaMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new argumentsCriteriaMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IargumentsCriteriaMethodInvocationImposterBuilder IargumentsCriteriaMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int argumentsCriteria) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IargumentsCriteriaMethodInvocationImposterBuilder IargumentsCriteriaMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int argumentsCriteria) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IargumentsCriteriaMethodInvocationImposterBuilder IargumentsCriteriaMethodInvocationImposterBuilder.Throws(argumentsCriteriaExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int argumentsCriteria) =>
                    {
                        throw exceptionGenerator.Invoke(argumentsCriteria);
                    });
                    return this;
                }

                IargumentsCriteriaMethodInvocationImposterBuilder IargumentsCriteriaMethodInvocationImposterBuilder.Callback(argumentsCriteriaCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IargumentsCriteriaMethodInvocationImposterBuilder IargumentsCriteriaMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class TExceptionMethodInvocationImposterGroup
        {
            internal static TExceptionMethodInvocationImposterGroup Default = new TExceptionMethodInvocationImposterGroup(new TExceptionArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal TExceptionArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public TExceptionMethodInvocationImposterGroup(TExceptionArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int TException)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(TException);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private TExceptionDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<TExceptionCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<TExceptionCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int TException)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(TException);
                    foreach (var callback in _callbacks)
                    {
                        callback(TException);
                    }
                }

                internal void Callback(TExceptionCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(TExceptionExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int TException) =>
                    {
                        throw exceptionGenerator(TException);
                    };
                }

                internal static void DefaultResultGenerator(int TException)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ITExceptionMethodInvocationImposterBuilder
        {
            ITExceptionMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ITExceptionMethodInvocationImposterBuilder Throws(System.Exception exception);
            ITExceptionMethodInvocationImposterBuilder Throws(TExceptionExceptionGeneratorDelegate exceptionGenerator);
            ITExceptionMethodInvocationImposterBuilder Callback(TExceptionCallbackDelegate callback);
            ITExceptionMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface TExceptionMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.TException(int TException)
        public interface ITExceptionMethodImposterBuilder : ITExceptionMethodInvocationImposterBuilder, TExceptionMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class TExceptionMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<TExceptionMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<TExceptionMethodInvocationImposterGroup>();
            private readonly TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection;
            public TExceptionMethodImposter(TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection)
            {
                this._tExceptionMethodInvocationHistoryCollection = _tExceptionMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(TExceptionArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private TExceptionMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(TExceptionArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? TExceptionMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(TException);
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
                private readonly TExceptionMethodInvocationImposterGroup _invocationImposterGroup;
                private TExceptionMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(TExceptionMethodImposter _imposter, TExceptionMethodInvocationHistoryCollection _tExceptionMethodInvocationHistoryCollection, TExceptionArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._tExceptionMethodInvocationHistoryCollection = _tExceptionMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new TExceptionMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ITExceptionMethodInvocationImposterBuilder ITExceptionMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int TException) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ITExceptionMethodInvocationImposterBuilder ITExceptionMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int TException) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ITExceptionMethodInvocationImposterBuilder ITExceptionMethodInvocationImposterBuilder.Throws(TExceptionExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int TException) =>
                    {
                        throw exceptionGenerator.Invoke(TException);
                    });
                    return this;
                }

                ITExceptionMethodInvocationImposterBuilder ITExceptionMethodInvocationImposterBuilder.Callback(TExceptionCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ITExceptionMethodInvocationImposterBuilder ITExceptionMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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
        class InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup
        {
            internal static InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup Default = new InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup(new InitializeOutParametersWithDefaultValuesArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal InitializeOutParametersWithDefaultValuesArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup(InitializeOutParametersWithDefaultValuesArgumentsCriteria argumentsCriteria)
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

            public void Invoke(int InitializeOutParametersWithDefaultValues)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                invocationImposter.Invoke(InitializeOutParametersWithDefaultValues);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private InitializeOutParametersWithDefaultValuesDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<InitializeOutParametersWithDefaultValuesCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<InitializeOutParametersWithDefaultValuesCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(int InitializeOutParametersWithDefaultValues)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
                    _resultGenerator.Invoke(InitializeOutParametersWithDefaultValues);
                    foreach (var callback in _callbacks)
                    {
                        callback(InitializeOutParametersWithDefaultValues);
                    }
                }

                internal void Callback(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int InitializeOutParametersWithDefaultValues) =>
                    {
                        throw exceptionGenerator(InitializeOutParametersWithDefaultValues);
                    };
                }

                internal static void DefaultResultGenerator(int InitializeOutParametersWithDefaultValues)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder
        {
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder Throws(System.Exception exception);
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator);
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder Callback(InitializeOutParametersWithDefaultValuesCallbackDelegate callback);
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InitializeOutParametersWithDefaultValuesMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void INameCollisionProtectionFeatureSut.InitializeOutParametersWithDefaultValues(int InitializeOutParametersWithDefaultValues)
        public interface IInitializeOutParametersWithDefaultValuesMethodImposterBuilder : IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder, InitializeOutParametersWithDefaultValuesMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup>();
            private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
            public InitializeOutParametersWithDefaultValuesMethodImposter(InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection)
            {
                this._initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(InitializeOutParametersWithDefaultValuesArguments arguments)
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
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.Default;
                try
                {
                    matchingInvocationImposterGroup.Invoke(InitializeOutParametersWithDefaultValues);
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
                private readonly InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup _invocationImposterGroup;
                private InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(InitializeOutParametersWithDefaultValuesMethodImposter _imposter, InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, InitializeOutParametersWithDefaultValuesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationSetups.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int InitializeOutParametersWithDefaultValues) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int InitializeOutParametersWithDefaultValues) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder.Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int InitializeOutParametersWithDefaultValues) =>
                    {
                        throw exceptionGenerator.Invoke(InitializeOutParametersWithDefaultValues);
                    });
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder.Callback(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder IInitializeOutParametersWithDefaultValuesMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
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