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
        private readonly CollisionWithThrowsMethodImposterCollection _collisionWithThrowsMethodImposterCollection;
        private readonly CollisionWithThrows_1MethodImposter _collisionWithThrows_1MethodImposter;
        private readonly MethodWithSameNameDifferentSignatureMethodImposter _methodWithSameNameDifferentSignatureMethodImposter;
        private readonly MethodWithSameNameDifferentSignature_1MethodImposter _methodWithSameNameDifferentSignature_1MethodImposter;
        private readonly MethodWithSameNameDifferentSignature_2MethodImposterCollection _methodWithSameNameDifferentSignature_2MethodImposterCollection;
        private readonly CollisionWithReturnsMethodInvocationHistoryCollection _collisionWithReturnsMethodInvocationHistoryCollection = new CollisionWithReturnsMethodInvocationHistoryCollection();
        private readonly CollisionWithReturns_1MethodInvocationHistoryCollection _collisionWithReturns_1MethodInvocationHistoryCollection = new CollisionWithReturns_1MethodInvocationHistoryCollection();
        private readonly CollisionWithThrowsMethodInvocationHistoryCollection _collisionWithThrowsMethodInvocationHistoryCollection = new CollisionWithThrowsMethodInvocationHistoryCollection();
        private readonly CollisionWithThrows_1MethodInvocationHistoryCollection _collisionWithThrows_1MethodInvocationHistoryCollection = new CollisionWithThrows_1MethodInvocationHistoryCollection();
        private readonly MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection = new MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection();
        private readonly MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection = new MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection();
        private readonly MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection = new MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection();
        public ICollisionWithReturnsMethodImposterBuilder<TValue> CollisionWithReturns<TValue>(Arg<TValue> value)
        {
            return new CollisionWithReturnsMethodImposter<TValue>.Builder(_collisionWithReturnsMethodImposterCollection, _collisionWithReturnsMethodInvocationHistoryCollection, new CollisionWithReturnsArgumentsCriteria<TValue>(value));
        }

        public ICollisionWithReturns_1MethodImposterBuilder CollisionWithReturns(Arg<int> value)
        {
            return new CollisionWithReturns_1MethodImposter.Builder(_collisionWithReturns_1MethodImposter, _collisionWithReturns_1MethodInvocationHistoryCollection, new CollisionWithReturns_1ArgumentsCriteria(value));
        }

        public ICollisionWithThrowsMethodImposterBuilder<TValue> CollisionWithThrows<TValue>(Arg<TValue> ex)
        {
            return new CollisionWithThrowsMethodImposter<TValue>.Builder(_collisionWithThrowsMethodImposterCollection, _collisionWithThrowsMethodInvocationHistoryCollection, new CollisionWithThrowsArgumentsCriteria<TValue>(ex));
        }

        public ICollisionWithThrows_1MethodImposterBuilder CollisionWithThrows(Arg<global::System.Exception> ex)
        {
            return new CollisionWithThrows_1MethodImposter.Builder(_collisionWithThrows_1MethodImposter, _collisionWithThrows_1MethodInvocationHistoryCollection, new CollisionWithThrows_1ArgumentsCriteria(ex));
        }

        public IMethodWithSameNameDifferentSignatureMethodImposterBuilder MethodWithSameNameDifferentSignature()
        {
            return new MethodWithSameNameDifferentSignatureMethodImposter.Builder(_methodWithSameNameDifferentSignatureMethodImposter, _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection);
        }

        public IMethodWithSameNameDifferentSignature_1MethodImposterBuilder MethodWithSameNameDifferentSignature(Arg<int> input)
        {
            return new MethodWithSameNameDifferentSignature_1MethodImposter.Builder(_methodWithSameNameDifferentSignature_1MethodImposter, _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection, new MethodWithSameNameDifferentSignature_1ArgumentsCriteria(input));
        }

        public IMethodWithSameNameDifferentSignature_2MethodImposterBuilder<T> MethodWithSameNameDifferentSignature<T>(Arg<T> input)
        {
            return new MethodWithSameNameDifferentSignature_2MethodImposter<T>.Builder(_methodWithSameNameDifferentSignature_2MethodImposterCollection, _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection, new MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T>(input));
        }

        private ImposterTargetInstance _imposterInstance;
        public INameCollisionProtectionFeatureSutImposter()
        {
            this._collisionWithReturnsMethodImposterCollection = new CollisionWithReturnsMethodImposterCollection(_collisionWithReturnsMethodInvocationHistoryCollection);
            this._collisionWithReturns_1MethodImposter = new CollisionWithReturns_1MethodImposter(_collisionWithReturns_1MethodInvocationHistoryCollection);
            this._collisionWithThrowsMethodImposterCollection = new CollisionWithThrowsMethodImposterCollection(_collisionWithThrowsMethodInvocationHistoryCollection);
            this._collisionWithThrows_1MethodImposter = new CollisionWithThrows_1MethodImposter(_collisionWithThrows_1MethodInvocationHistoryCollection);
            this._methodWithSameNameDifferentSignatureMethodImposter = new MethodWithSameNameDifferentSignatureMethodImposter(_methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection);
            this._methodWithSameNameDifferentSignature_1MethodImposter = new MethodWithSameNameDifferentSignature_1MethodImposter(_methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection);
            this._methodWithSameNameDifferentSignature_2MethodImposterCollection = new MethodWithSameNameDifferentSignature_2MethodImposterCollection(_methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
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
        public class CollisionWithReturnsArguments<TValue>
        {
            public TValue value { get; }

            public CollisionWithReturnsArguments(TValue value)
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
            public Arg<TValue> value { get; }

            public CollisionWithReturnsArgumentsCriteria(Arg<TValue> value)
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
            bool Matches<TValue>(CollisionWithReturnsArgumentsCriteria<TValue> criteria);
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public class CollisionWithReturnsMethodInvocationHistory<TValue> : ICollisionWithReturnsMethodInvocationHistory
        {
            public CollisionWithReturnsArguments<TValue> Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public CollisionWithReturnsMethodInvocationHistory(CollisionWithReturnsArguments<TValue> arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TValueTarget>(CollisionWithReturnsArgumentsCriteria<TValueTarget> criteria)
            {
                return criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturnsMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<ICollisionWithReturnsMethodInvocationHistory> _invocationHistory = new ConcurrentStack<ICollisionWithReturnsMethodInvocationHistory>();
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

            private readonly ConcurrentStack<ICollisionWithReturnsMethodImposter> _imposters = new ConcurrentStack<ICollisionWithReturnsMethodImposter>();
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
            internal static CollisionWithReturnsMethodInvocationsSetup<TValue> DefaultInvocationSetup = new CollisionWithReturnsMethodInvocationsSetup<TValue>(new CollisionWithReturnsArgumentsCriteria<TValue>(Arg<TValue>.Any()));
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
                return default(int);
            }

            public CollisionWithReturnsMethodInvocationsSetup(CollisionWithReturnsArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Returns(CollisionWithReturnsDelegate<TValue> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    return value;
                };
                return this;
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw ex;
                };
                return this;
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> CallBefore(CollisionWithReturnsCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public ICollisionWithReturnsMethodInvocationsSetup<TValue> CallAfter(CollisionWithReturnsCallbackDelegate<TValue> callback)
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
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Returns(CollisionWithReturnsDelegate<TValue> resultGenerator);
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Returns(int value);
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws(System.Exception ex);
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> Throws(CollisionWithReturnsExceptionGeneratorDelegate<TValue> exceptionGenerator);
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> CallBefore(CollisionWithReturnsCallbackDelegate<TValue> callback);
            public ICollisionWithReturnsMethodInvocationsSetup<TValue> CallAfter(CollisionWithReturnsCallbackDelegate<TValue> callback);
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
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public interface CollisionWithReturnsMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        public interface ICollisionWithReturnsMethodImposterBuilder<TValue> : ICollisionWithReturnsMethodInvocationsSetup<TValue>, CollisionWithReturnsMethodInvocationVerifier<TValue>
        {
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturnsMethodImposter<TValue> : ICollisionWithReturnsMethodImposter<TValue>
        {
            private readonly ConcurrentStack<CollisionWithReturnsMethodInvocationsSetup<TValue>> _invocationSetups = new ConcurrentStack<CollisionWithReturnsMethodInvocationsSetup<TValue>>();
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
                catch (Exception ex)
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

                private CollisionWithReturnsMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithReturnsMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Returns(CollisionWithReturnsDelegate<TValue> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithReturnsMethodInvocationsSetup<TValue> ICollisionWithReturnsMethodInvocationsSetup<TValue>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void CollisionWithReturnsMethodInvocationVerifier<TValue>.Called(Count count)
                {
                    var invocationCount = _collisionWithReturnsMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
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
        public class CollisionWithReturns_1Arguments
        {
            public int value { get; }

            public CollisionWithReturns_1Arguments(int value)
            {
                this.value = value;
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithReturns_1ArgumentsCriteria
        {
            public Arg<int> value { get; }

            public CollisionWithReturns_1ArgumentsCriteria(Arg<int> value)
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

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public class CollisionWithReturns_1MethodInvocationHistory : ICollisionWithReturns_1MethodInvocationHistory
        {
            public CollisionWithReturns_1Arguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public CollisionWithReturns_1MethodInvocationHistory(CollisionWithReturns_1Arguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(CollisionWithReturns_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_1MethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<ICollisionWithReturns_1MethodInvocationHistory> _invocationHistory = new ConcurrentStack<ICollisionWithReturns_1MethodInvocationHistory>();
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
            internal static CollisionWithReturns_1MethodInvocationsSetup DefaultInvocationSetup = new CollisionWithReturns_1MethodInvocationsSetup(new CollisionWithReturns_1ArgumentsCriteria(Arg<int>.Any()));
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
                return default(int);
            }

            public CollisionWithReturns_1MethodInvocationsSetup(CollisionWithReturns_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public ICollisionWithReturns_1MethodInvocationsSetup Returns(CollisionWithReturns_1Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public ICollisionWithReturns_1MethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    return value;
                };
                return this;
            }

            public ICollisionWithReturns_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public ICollisionWithReturns_1MethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw ex;
                };
                return this;
            }

            public ICollisionWithReturns_1MethodInvocationsSetup Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            public ICollisionWithReturns_1MethodInvocationsSetup CallBefore(CollisionWithReturns_1CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public ICollisionWithReturns_1MethodInvocationsSetup CallAfter(CollisionWithReturns_1CallbackDelegate callback)
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
            public ICollisionWithReturns_1MethodInvocationsSetup Returns(CollisionWithReturns_1Delegate resultGenerator);
            public ICollisionWithReturns_1MethodInvocationsSetup Returns(int value);
            public ICollisionWithReturns_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public ICollisionWithReturns_1MethodInvocationsSetup Throws(System.Exception ex);
            public ICollisionWithReturns_1MethodInvocationsSetup Throws(CollisionWithReturns_1ExceptionGeneratorDelegate exceptionGenerator);
            public ICollisionWithReturns_1MethodInvocationsSetup CallBefore(CollisionWithReturns_1CallbackDelegate callback);
            public ICollisionWithReturns_1MethodInvocationsSetup CallAfter(CollisionWithReturns_1CallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public interface CollisionWithReturns_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        public interface ICollisionWithReturns_1MethodImposterBuilder : ICollisionWithReturns_1MethodInvocationsSetup, CollisionWithReturns_1MethodInvocationVerifier
        {
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithReturns(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithReturns_1MethodImposter
        {
            private readonly ConcurrentStack<CollisionWithReturns_1MethodInvocationsSetup> _invocationSetups = new ConcurrentStack<CollisionWithReturns_1MethodInvocationsSetup>();
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
                catch (Exception ex)
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

                private CollisionWithReturns_1MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithReturns_1MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Returns(CollisionWithReturns_1Delegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithReturns_1MethodInvocationsSetup ICollisionWithReturns_1MethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void CollisionWithReturns_1MethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _collisionWithReturns_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
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
        public class CollisionWithThrowsArguments<TValue>
        {
            public TValue ex { get; }

            public CollisionWithThrowsArguments(TValue ex)
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
            public Arg<TValue> ex { get; }

            public CollisionWithThrowsArgumentsCriteria(Arg<TValue> ex)
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
            bool Matches<TValue>(CollisionWithThrowsArgumentsCriteria<TValue> criteria);
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public class CollisionWithThrowsMethodInvocationHistory<TValue> : ICollisionWithThrowsMethodInvocationHistory
        {
            public CollisionWithThrowsArguments<TValue> Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public CollisionWithThrowsMethodInvocationHistory(CollisionWithThrowsArguments<TValue> arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TValueTarget>(CollisionWithThrowsArgumentsCriteria<TValueTarget> criteria)
            {
                return criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrowsMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<ICollisionWithThrowsMethodInvocationHistory> _invocationHistory = new ConcurrentStack<ICollisionWithThrowsMethodInvocationHistory>();
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

            private readonly ConcurrentStack<ICollisionWithThrowsMethodImposter> _imposters = new ConcurrentStack<ICollisionWithThrowsMethodImposter>();
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
            internal static CollisionWithThrowsMethodInvocationsSetup<TValue> DefaultInvocationSetup = new CollisionWithThrowsMethodInvocationsSetup<TValue>(new CollisionWithThrowsArgumentsCriteria<TValue>(Arg<TValue>.Any()));
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
                return default(int);
            }

            public CollisionWithThrowsMethodInvocationsSetup(CollisionWithThrowsArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Returns(CollisionWithThrowsDelegate<TValue> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    return value;
                };
                return this;
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    throw new TException();
                };
                return this;
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    throw ex;
                };
                return this;
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue ex) =>
                {
                    throw exceptionGenerator(ex);
                };
                return this;
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> CallBefore(CollisionWithThrowsCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public ICollisionWithThrowsMethodInvocationsSetup<TValue> CallAfter(CollisionWithThrowsCallbackDelegate<TValue> callback)
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
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Returns(CollisionWithThrowsDelegate<TValue> resultGenerator);
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Returns(int value);
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws(System.Exception ex);
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> Throws(CollisionWithThrowsExceptionGeneratorDelegate<TValue> exceptionGenerator);
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> CallBefore(CollisionWithThrowsCallbackDelegate<TValue> callback);
            public ICollisionWithThrowsMethodInvocationsSetup<TValue> CallAfter(CollisionWithThrowsCallbackDelegate<TValue> callback);
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
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public interface CollisionWithThrowsMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        public interface ICollisionWithThrowsMethodImposterBuilder<TValue> : ICollisionWithThrowsMethodInvocationsSetup<TValue>, CollisionWithThrowsMethodInvocationVerifier<TValue>
        {
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows<TValue>(TValue ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrowsMethodImposter<TValue> : ICollisionWithThrowsMethodImposter<TValue>
        {
            private readonly ConcurrentStack<CollisionWithThrowsMethodInvocationsSetup<TValue>> _invocationSetups = new ConcurrentStack<CollisionWithThrowsMethodInvocationsSetup<TValue>>();
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
                catch (Exception ex)
                {
                    _collisionWithThrowsMethodInvocationHistoryCollection.Add(new CollisionWithThrowsMethodInvocationHistory<TValue>(arguments, default, ex));
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

                private CollisionWithThrowsMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithThrowsMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
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

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithThrowsMethodInvocationsSetup<TValue> ICollisionWithThrowsMethodInvocationsSetup<TValue>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void CollisionWithThrowsMethodInvocationVerifier<TValue>.Called(Count count)
                {
                    var invocationCount = _collisionWithThrowsMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
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
        public class CollisionWithThrows_1Arguments
        {
            public global::System.Exception ex { get; }

            public CollisionWithThrows_1Arguments(global::System.Exception ex)
            {
                this.ex = ex;
            }
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollisionWithThrows_1ArgumentsCriteria
        {
            public Arg<global::System.Exception> ex { get; }

            public CollisionWithThrows_1ArgumentsCriteria(Arg<global::System.Exception> ex)
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

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public class CollisionWithThrows_1MethodInvocationHistory : ICollisionWithThrows_1MethodInvocationHistory
        {
            public CollisionWithThrows_1Arguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public CollisionWithThrows_1MethodInvocationHistory(CollisionWithThrows_1Arguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(CollisionWithThrows_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrows_1MethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<ICollisionWithThrows_1MethodInvocationHistory> _invocationHistory = new ConcurrentStack<ICollisionWithThrows_1MethodInvocationHistory>();
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
            internal static CollisionWithThrows_1MethodInvocationsSetup DefaultInvocationSetup = new CollisionWithThrows_1MethodInvocationsSetup(new CollisionWithThrows_1ArgumentsCriteria(Arg<global::System.Exception>.Any()));
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
                return default(int);
            }

            public CollisionWithThrows_1MethodInvocationsSetup(CollisionWithThrows_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public ICollisionWithThrows_1MethodInvocationsSetup Returns(CollisionWithThrows_1Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public ICollisionWithThrows_1MethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    return value;
                };
                return this;
            }

            public ICollisionWithThrows_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    throw new TException();
                };
                return this;
            }

            public ICollisionWithThrows_1MethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    throw ex;
                };
                return this;
            }

            public ICollisionWithThrows_1MethodInvocationsSetup Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Exception ex) =>
                {
                    throw exceptionGenerator(ex);
                };
                return this;
            }

            public ICollisionWithThrows_1MethodInvocationsSetup CallBefore(CollisionWithThrows_1CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public ICollisionWithThrows_1MethodInvocationsSetup CallAfter(CollisionWithThrows_1CallbackDelegate callback)
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
            public ICollisionWithThrows_1MethodInvocationsSetup Returns(CollisionWithThrows_1Delegate resultGenerator);
            public ICollisionWithThrows_1MethodInvocationsSetup Returns(int value);
            public ICollisionWithThrows_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public ICollisionWithThrows_1MethodInvocationsSetup Throws(System.Exception ex);
            public ICollisionWithThrows_1MethodInvocationsSetup Throws(CollisionWithThrows_1ExceptionGeneratorDelegate exceptionGenerator);
            public ICollisionWithThrows_1MethodInvocationsSetup CallBefore(CollisionWithThrows_1CallbackDelegate callback);
            public ICollisionWithThrows_1MethodInvocationsSetup CallAfter(CollisionWithThrows_1CallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public interface CollisionWithThrows_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        public interface ICollisionWithThrows_1MethodImposterBuilder : ICollisionWithThrows_1MethodInvocationsSetup, CollisionWithThrows_1MethodInvocationVerifier
        {
        }

        // int INameCollisionProtectionFeatureSut.CollisionWithThrows(Exception ex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollisionWithThrows_1MethodImposter
        {
            private readonly ConcurrentStack<CollisionWithThrows_1MethodInvocationsSetup> _invocationSetups = new ConcurrentStack<CollisionWithThrows_1MethodInvocationsSetup>();
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
                catch (Exception ex)
                {
                    _collisionWithThrows_1MethodInvocationHistoryCollection.Add(new CollisionWithThrows_1MethodInvocationHistory(arguments, default, ex));
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

                private CollisionWithThrows_1MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new CollisionWithThrows_1MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
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

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ICollisionWithThrows_1MethodInvocationsSetup ICollisionWithThrows_1MethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void CollisionWithThrows_1MethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _collisionWithThrows_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
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

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public class MethodWithSameNameDifferentSignatureMethodInvocationHistory : IMethodWithSameNameDifferentSignatureMethodInvocationHistory
        {
            public int Result { get; }
            public System.Exception? Exception { get; }

            public MethodWithSameNameDifferentSignatureMethodInvocationHistory(int result, System.Exception? exception = default(System.Exception? ))
            {
                Result = result;
                Exception = exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignatureMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IMethodWithSameNameDifferentSignatureMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IMethodWithSameNameDifferentSignatureMethodInvocationHistory>();
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
                return default(int);
            }

            public MethodWithSameNameDifferentSignatureMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    return value;
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw ex;
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup CallBefore(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup CallAfter(MethodWithSameNameDifferentSignatureCallbackDelegate callback)
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
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Returns(MethodWithSameNameDifferentSignatureDelegate resultGenerator);
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Returns(int value);
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws(System.Exception ex);
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup Throws(MethodWithSameNameDifferentSignatureExceptionGeneratorDelegate exceptionGenerator);
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup CallBefore(MethodWithSameNameDifferentSignatureCallbackDelegate callback);
            public IMethodWithSameNameDifferentSignatureMethodInvocationsSetup CallAfter(MethodWithSameNameDifferentSignatureCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public interface MethodWithSameNameDifferentSignatureMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        public interface IMethodWithSameNameDifferentSignatureMethodImposterBuilder : IMethodWithSameNameDifferentSignatureMethodInvocationsSetup, MethodWithSameNameDifferentSignatureMethodInvocationVerifier
        {
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignatureMethodImposter
        {
            private readonly ConcurrentStack<MethodWithSameNameDifferentSignatureMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<MethodWithSameNameDifferentSignatureMethodInvocationsSetup>();
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
                catch (Exception ex)
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

                private MethodWithSameNameDifferentSignatureMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MethodWithSameNameDifferentSignatureMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
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

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignatureMethodInvocationsSetup IMethodWithSameNameDifferentSignatureMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void MethodWithSameNameDifferentSignatureMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _methodWithSameNameDifferentSignatureMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
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
        public class MethodWithSameNameDifferentSignature_1Arguments
        {
            public int input { get; }

            public MethodWithSameNameDifferentSignature_1Arguments(int input)
            {
                this.input = input;
            }
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodWithSameNameDifferentSignature_1ArgumentsCriteria
        {
            public Arg<int> input { get; }

            public MethodWithSameNameDifferentSignature_1ArgumentsCriteria(Arg<int> input)
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

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public class MethodWithSameNameDifferentSignature_1MethodInvocationHistory : IMethodWithSameNameDifferentSignature_1MethodInvocationHistory
        {
            public MethodWithSameNameDifferentSignature_1Arguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public MethodWithSameNameDifferentSignature_1MethodInvocationHistory(MethodWithSameNameDifferentSignature_1Arguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(MethodWithSameNameDifferentSignature_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IMethodWithSameNameDifferentSignature_1MethodInvocationHistory> _invocationHistory = new ConcurrentStack<IMethodWithSameNameDifferentSignature_1MethodInvocationHistory>();
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
            internal static MethodWithSameNameDifferentSignature_1MethodInvocationsSetup DefaultInvocationSetup = new MethodWithSameNameDifferentSignature_1MethodInvocationsSetup(new MethodWithSameNameDifferentSignature_1ArgumentsCriteria(Arg<int>.Any()));
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
                return default(int);
            }

            public MethodWithSameNameDifferentSignature_1MethodInvocationsSetup(MethodWithSameNameDifferentSignature_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    return value;
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw ex;
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw exceptionGenerator(input);
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup CallBefore(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup CallAfter(MethodWithSameNameDifferentSignature_1CallbackDelegate callback)
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
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Returns(MethodWithSameNameDifferentSignature_1Delegate resultGenerator);
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Returns(int value);
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws(System.Exception ex);
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup Throws(MethodWithSameNameDifferentSignature_1ExceptionGeneratorDelegate exceptionGenerator);
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup CallBefore(MethodWithSameNameDifferentSignature_1CallbackDelegate callback);
            public IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup CallAfter(MethodWithSameNameDifferentSignature_1CallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public interface MethodWithSameNameDifferentSignature_1MethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        public interface IMethodWithSameNameDifferentSignature_1MethodImposterBuilder : IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup, MethodWithSameNameDifferentSignature_1MethodInvocationVerifier
        {
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_1MethodImposter
        {
            private readonly ConcurrentStack<MethodWithSameNameDifferentSignature_1MethodInvocationsSetup> _invocationSetups = new ConcurrentStack<MethodWithSameNameDifferentSignature_1MethodInvocationsSetup>();
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
                catch (Exception ex)
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

                private MethodWithSameNameDifferentSignature_1MethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MethodWithSameNameDifferentSignature_1MethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
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

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup IMethodWithSameNameDifferentSignature_1MethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void MethodWithSameNameDifferentSignature_1MethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _methodWithSameNameDifferentSignature_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
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
        public class MethodWithSameNameDifferentSignature_2Arguments<T>
        {
            public T input { get; }

            public MethodWithSameNameDifferentSignature_2Arguments(T input)
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
            public Arg<T> input { get; }

            public MethodWithSameNameDifferentSignature_2ArgumentsCriteria(Arg<T> input)
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
            bool Matches<T>(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> criteria);
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public class MethodWithSameNameDifferentSignature_2MethodInvocationHistory<T> : IMethodWithSameNameDifferentSignature_2MethodInvocationHistory
        {
            public MethodWithSameNameDifferentSignature_2Arguments<T> Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public MethodWithSameNameDifferentSignature_2MethodInvocationHistory(MethodWithSameNameDifferentSignature_2Arguments<T> arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TTarget>(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<TTarget> criteria)
            {
                return criteria.As<T>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodInvocationHistory> _invocationHistory = new ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodInvocationHistory>();
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

            private readonly ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodImposter> _imposters = new ConcurrentStack<IMethodWithSameNameDifferentSignature_2MethodImposter>();
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
            internal static MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> DefaultInvocationSetup = new MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>(new MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T>(Arg<T>.Any()));
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
                return default(int);
            }

            public MethodWithSameNameDifferentSignature_2MethodInvocationsSetup(MethodWithSameNameDifferentSignature_2ArgumentsCriteria<T> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    return value;
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    throw ex;
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (T input) =>
                {
                    throw exceptionGenerator(input);
                };
                return this;
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> CallBefore(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> CallAfter(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback)
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
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Returns(MethodWithSameNameDifferentSignature_2Delegate<T> resultGenerator);
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Returns(int value);
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws<TException>()
                where TException : Exception, new();
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws(System.Exception ex);
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> Throws(MethodWithSameNameDifferentSignature_2ExceptionGeneratorDelegate<T> exceptionGenerator);
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> CallBefore(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback);
            public IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> CallAfter(MethodWithSameNameDifferentSignature_2CallbackDelegate<T> callback);
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
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public interface MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        public interface IMethodWithSameNameDifferentSignature_2MethodImposterBuilder<T> : IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>, MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>
        {
        }

        // int INameCollisionProtectionFeatureSut.MethodWithSameNameDifferentSignature<T>(T input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodWithSameNameDifferentSignature_2MethodImposter<T> : IMethodWithSameNameDifferentSignature_2MethodImposter<T>
        {
            private readonly ConcurrentStack<MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>> _invocationSetups = new ConcurrentStack<MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>>();
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
                catch (Exception ex)
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

                private MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>(_argumentsCriteria);
                        _imposterCollection.AddNew<T>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
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

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T> IMethodWithSameNameDifferentSignature_2MethodInvocationsSetup<T>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
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

                void MethodWithSameNameDifferentSignature_2MethodInvocationVerifier<T>.Called(Count count)
                {
                    var invocationCount = _methodWithSameNameDifferentSignature_2MethodInvocationHistoryCollection.Count<T>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }
    }
}