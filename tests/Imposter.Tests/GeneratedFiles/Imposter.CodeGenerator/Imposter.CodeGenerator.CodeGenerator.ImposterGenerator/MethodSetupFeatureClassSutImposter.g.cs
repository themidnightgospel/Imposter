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
    public class MethodSetupFeatureClassSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut>
    {
        private readonly IntSingleParamMethodImposter _intSingleParamMethodImposter;
        private readonly VoidWithSideEffectMethodImposter _voidWithSideEffectMethodImposter;
        private readonly SumAsyncMethodImposter _sumAsyncMethodImposter;
        private readonly BuildLabelAsyncMethodImposter _buildLabelAsyncMethodImposter;
        private readonly RefOutWithParamsMethodImposter _refOutWithParamsMethodImposter;
        private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection = new IntSingleParamMethodInvocationHistoryCollection();
        private readonly VoidWithSideEffectMethodInvocationHistoryCollection _voidWithSideEffectMethodInvocationHistoryCollection = new VoidWithSideEffectMethodInvocationHistoryCollection();
        private readonly SumAsyncMethodInvocationHistoryCollection _sumAsyncMethodInvocationHistoryCollection = new SumAsyncMethodInvocationHistoryCollection();
        private readonly BuildLabelAsyncMethodInvocationHistoryCollection _buildLabelAsyncMethodInvocationHistoryCollection = new BuildLabelAsyncMethodInvocationHistoryCollection();
        private readonly RefOutWithParamsMethodInvocationHistoryCollection _refOutWithParamsMethodInvocationHistoryCollection = new RefOutWithParamsMethodInvocationHistoryCollection();
        public IIntSingleParamMethodImposterBuilder IntSingleParam(Imposter.Abstractions.Arg<int> age)
        {
            return new IntSingleParamMethodImposter.Builder(_intSingleParamMethodImposter, _intSingleParamMethodInvocationHistoryCollection, new IntSingleParamArgumentsCriteria(age));
        }

        public IVoidWithSideEffectMethodImposterBuilder VoidWithSideEffect(Imposter.Abstractions.Arg<int> delta)
        {
            return new VoidWithSideEffectMethodImposter.Builder(_voidWithSideEffectMethodImposter, _voidWithSideEffectMethodInvocationHistoryCollection, new VoidWithSideEffectArgumentsCriteria(delta));
        }

        public ISumAsyncMethodImposterBuilder SumAsync(Imposter.Abstractions.Arg<int> left, Imposter.Abstractions.Arg<int> right)
        {
            return new SumAsyncMethodImposter.Builder(_sumAsyncMethodImposter, _sumAsyncMethodInvocationHistoryCollection, new SumAsyncArgumentsCriteria(left, right));
        }

        public IBuildLabelAsyncMethodImposterBuilder BuildLabelAsync(Imposter.Abstractions.Arg<string> prefix, Imposter.Abstractions.Arg<string> suffix)
        {
            return new BuildLabelAsyncMethodImposter.Builder(_buildLabelAsyncMethodImposter, _buildLabelAsyncMethodInvocationHistoryCollection, new BuildLabelAsyncArgumentsCriteria(prefix, suffix));
        }

        public IRefOutWithParamsMethodImposterBuilder RefOutWithParams(Imposter.Abstractions.Arg<int> seed, Imposter.Abstractions.OutArg<int> doubled, Imposter.Abstractions.Arg<int[]> adjustments)
        {
            return new RefOutWithParamsMethodImposter.Builder(_refOutWithParamsMethodImposter, _refOutWithParamsMethodInvocationHistoryCollection, new RefOutWithParamsArgumentsCriteria(seed, doubled, adjustments));
        }

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut Instance() => _imposterInstance;
        global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut>.Instance()
        {
            return _imposterInstance;
        }

        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public delegate int IntSingleParamDelegate(int age);
        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public delegate void IntSingleParamCallbackDelegate(int age);
        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public delegate System.Exception IntSingleParamExceptionGeneratorDelegate(int age);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntSingleParamArguments
        {
            public int age;
            internal IntSingleParamArguments(int age)
            {
                this.age = age;
            }
        }

        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntSingleParamArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> age { get; }

            public IntSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<int> age)
            {
                this.age = age;
            }

            public bool Matches(IntSingleParamArguments arguments)
            {
                return age.Matches(arguments.age);
            }
        }

        public interface IIntSingleParamMethodInvocationHistory
        {
            bool Matches(IntSingleParamArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodInvocationHistory : IIntSingleParamMethodInvocationHistory
        {
            internal IntSingleParamArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntSingleParamMethodInvocationHistory(IntSingleParamArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntSingleParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntSingleParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntSingleParamMethodInvocationHistory>();
            internal void Add(IIntSingleParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntSingleParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntSingleParamMethodInvocationImposterGroup
        {
            internal static IntSingleParamMethodInvocationImposterGroup Default = new IntSingleParamMethodInvocationImposterGroup(new IntSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal IntSingleParamArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public IntSingleParamMethodInvocationImposterGroup(IntSingleParamArgumentsCriteria argumentsCriteria)
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

            public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int age, IntSingleParamDelegate baseImplementation = null)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, age, baseImplementation);
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

                private IntSingleParamDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<IntSingleParamCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IntSingleParamCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int age, IntSingleParamDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(age);
                    foreach (var callback in _callbacks)
                    {
                        callback(age);
                    }

                    return result;
                }

                internal void Callback(IntSingleParamCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(IntSingleParamDelegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int age) =>
                    {
                        return value;
                    };
                }

                internal void Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int age) =>
                    {
                        throw exceptionGenerator(age);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static int DefaultResultGenerator(int age)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntSingleParamMethodInvocationImposterGroup
        {
            IIntSingleParamMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IIntSingleParamMethodInvocationImposterGroup Throws(System.Exception exception);
            IIntSingleParamMethodInvocationImposterGroup Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator);
            IIntSingleParamMethodInvocationImposterGroup Callback(IntSingleParamCallbackDelegate callback);
            IIntSingleParamMethodInvocationImposterGroup Returns(IntSingleParamDelegate resultGenerator);
            IIntSingleParamMethodInvocationImposterGroup Returns(int value);
            IIntSingleParamMethodInvocationImposterGroup UseBaseImplementation();
            IIntSingleParamMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntSingleParamInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public interface IIntSingleParamMethodImposterBuilder : IIntSingleParamMethodInvocationImposterGroup, IntSingleParamInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntSingleParamMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<IntSingleParamMethodInvocationImposterGroup>();
            private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public IntSingleParamMethodImposter(IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._intSingleParamMethodInvocationHistoryCollection = _intSingleParamMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(IntSingleParamArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private IntSingleParamMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(IntSingleParamArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int age, IntSingleParamDelegate baseImplementation = null)
            {
                var arguments = new IntSingleParamArguments(age);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)");
                    }

                    matchingInvocationImposterGroup = IntSingleParamMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)", age, baseImplementation);
                    _intSingleParamMethodInvocationHistoryCollection.Add(new IntSingleParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intSingleParamMethodInvocationHistoryCollection.Add(new IntSingleParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntSingleParamMethodImposterBuilder
            {
                private readonly IntSingleParamMethodImposter _imposter;
                private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection;
                private readonly IntSingleParamArgumentsCriteria _argumentsCriteria;
                private readonly IntSingleParamMethodInvocationImposterGroup _invocationImposterGroup;
                private IntSingleParamMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(IntSingleParamMethodImposter _imposter, IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection, IntSingleParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intSingleParamMethodInvocationHistoryCollection = _intSingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new IntSingleParamMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw exceptionGenerator.Invoke(age);
                    });
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Callback(IntSingleParamCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Returns(IntSingleParamDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void IntSingleParamInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intSingleParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)
        public delegate void VoidWithSideEffectDelegate(int delta);
        // virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)
        public delegate void VoidWithSideEffectCallbackDelegate(int delta);
        // virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)
        public delegate System.Exception VoidWithSideEffectExceptionGeneratorDelegate(int delta);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class VoidWithSideEffectArguments
        {
            public int delta;
            internal VoidWithSideEffectArguments(int delta)
            {
                this.delta = delta;
            }
        }

        // virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class VoidWithSideEffectArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> delta { get; }

            public VoidWithSideEffectArgumentsCriteria(Imposter.Abstractions.Arg<int> delta)
            {
                this.delta = delta;
            }

            public bool Matches(VoidWithSideEffectArguments arguments)
            {
                return delta.Matches(arguments.delta);
            }
        }

        public interface IVoidWithSideEffectMethodInvocationHistory
        {
            bool Matches(VoidWithSideEffectArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class VoidWithSideEffectMethodInvocationHistory : IVoidWithSideEffectMethodInvocationHistory
        {
            internal VoidWithSideEffectArguments Arguments;
            internal System.Exception Exception;
            public VoidWithSideEffectMethodInvocationHistory(VoidWithSideEffectArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(VoidWithSideEffectArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class VoidWithSideEffectMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IVoidWithSideEffectMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IVoidWithSideEffectMethodInvocationHistory>();
            internal void Add(IVoidWithSideEffectMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(VoidWithSideEffectArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class VoidWithSideEffectMethodInvocationImposterGroup
        {
            internal static VoidWithSideEffectMethodInvocationImposterGroup Default = new VoidWithSideEffectMethodInvocationImposterGroup(new VoidWithSideEffectArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal VoidWithSideEffectArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public VoidWithSideEffectMethodInvocationImposterGroup(VoidWithSideEffectArgumentsCriteria argumentsCriteria)
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

            public void Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int delta, VoidWithSideEffectDelegate baseImplementation = null)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, delta, baseImplementation);
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

                private VoidWithSideEffectDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<VoidWithSideEffectCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<VoidWithSideEffectCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int delta, VoidWithSideEffectDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(delta);
                    foreach (var callback in _callbacks)
                    {
                        callback(delta);
                    }
                }

                internal void Callback(VoidWithSideEffectCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(VoidWithSideEffectExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int delta) =>
                    {
                        throw exceptionGenerator(delta);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static void DefaultResultGenerator(int delta)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IVoidWithSideEffectMethodInvocationImposterGroup
        {
            IVoidWithSideEffectMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IVoidWithSideEffectMethodInvocationImposterGroup Throws(System.Exception exception);
            IVoidWithSideEffectMethodInvocationImposterGroup Throws(VoidWithSideEffectExceptionGeneratorDelegate exceptionGenerator);
            IVoidWithSideEffectMethodInvocationImposterGroup Callback(VoidWithSideEffectCallbackDelegate callback);
            IVoidWithSideEffectMethodInvocationImposterGroup UseBaseImplementation();
            IVoidWithSideEffectMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface VoidWithSideEffectInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)
        public interface IVoidWithSideEffectMethodImposterBuilder : IVoidWithSideEffectMethodInvocationImposterGroup, VoidWithSideEffectInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class VoidWithSideEffectMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<VoidWithSideEffectMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<VoidWithSideEffectMethodInvocationImposterGroup>();
            private readonly VoidWithSideEffectMethodInvocationHistoryCollection _voidWithSideEffectMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public VoidWithSideEffectMethodImposter(VoidWithSideEffectMethodInvocationHistoryCollection _voidWithSideEffectMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._voidWithSideEffectMethodInvocationHistoryCollection = _voidWithSideEffectMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(VoidWithSideEffectArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private VoidWithSideEffectMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(VoidWithSideEffectArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(int delta, VoidWithSideEffectDelegate baseImplementation = null)
            {
                var arguments = new VoidWithSideEffectArguments(delta);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)");
                    }

                    matchingInvocationImposterGroup = VoidWithSideEffectMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual void MethodSetupFeatureClassSut.VoidWithSideEffect(int delta)", delta, baseImplementation);
                    _voidWithSideEffectMethodInvocationHistoryCollection.Add(new VoidWithSideEffectMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _voidWithSideEffectMethodInvocationHistoryCollection.Add(new VoidWithSideEffectMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IVoidWithSideEffectMethodImposterBuilder
            {
                private readonly VoidWithSideEffectMethodImposter _imposter;
                private readonly VoidWithSideEffectMethodInvocationHistoryCollection _voidWithSideEffectMethodInvocationHistoryCollection;
                private readonly VoidWithSideEffectArgumentsCriteria _argumentsCriteria;
                private readonly VoidWithSideEffectMethodInvocationImposterGroup _invocationImposterGroup;
                private VoidWithSideEffectMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(VoidWithSideEffectMethodImposter _imposter, VoidWithSideEffectMethodInvocationHistoryCollection _voidWithSideEffectMethodInvocationHistoryCollection, VoidWithSideEffectArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._voidWithSideEffectMethodInvocationHistoryCollection = _voidWithSideEffectMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new VoidWithSideEffectMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IVoidWithSideEffectMethodInvocationImposterGroup IVoidWithSideEffectMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int delta) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IVoidWithSideEffectMethodInvocationImposterGroup IVoidWithSideEffectMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int delta) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IVoidWithSideEffectMethodInvocationImposterGroup IVoidWithSideEffectMethodInvocationImposterGroup.Throws(VoidWithSideEffectExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int delta) =>
                    {
                        throw exceptionGenerator.Invoke(delta);
                    });
                    return this;
                }

                IVoidWithSideEffectMethodInvocationImposterGroup IVoidWithSideEffectMethodInvocationImposterGroup.Callback(VoidWithSideEffectCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IVoidWithSideEffectMethodInvocationImposterGroup IVoidWithSideEffectMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IVoidWithSideEffectMethodInvocationImposterGroup IVoidWithSideEffectMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void VoidWithSideEffectInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _voidWithSideEffectMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)
        public delegate global::System.Threading.Tasks.Task<int> SumAsyncDelegate(int left, int right);
        // virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)
        public delegate System.Threading.Tasks.Task SumAsyncCallbackDelegate(int left, int right);
        // virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)
        public delegate System.Exception SumAsyncExceptionGeneratorDelegate(int left, int right);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SumAsyncArguments
        {
            public int left;
            public int right;
            internal SumAsyncArguments(int left, int right)
            {
                this.left = left;
                this.right = right;
            }
        }

        // virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SumAsyncArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> left { get; }
            public Imposter.Abstractions.Arg<int> right { get; }

            public SumAsyncArgumentsCriteria(Imposter.Abstractions.Arg<int> left, Imposter.Abstractions.Arg<int> right)
            {
                this.left = left;
                this.right = right;
            }

            public bool Matches(SumAsyncArguments arguments)
            {
                return left.Matches(arguments.left) && right.Matches(arguments.right);
            }
        }

        public interface ISumAsyncMethodInvocationHistory
        {
            bool Matches(SumAsyncArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SumAsyncMethodInvocationHistory : ISumAsyncMethodInvocationHistory
        {
            internal SumAsyncArguments Arguments;
            internal global::System.Threading.Tasks.Task<int> Result;
            internal System.Exception Exception;
            public SumAsyncMethodInvocationHistory(SumAsyncArguments Arguments, global::System.Threading.Tasks.Task<int> Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(SumAsyncArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SumAsyncMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ISumAsyncMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ISumAsyncMethodInvocationHistory>();
            internal void Add(ISumAsyncMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(SumAsyncArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class SumAsyncMethodInvocationImposterGroup
        {
            internal static SumAsyncMethodInvocationImposterGroup Default = new SumAsyncMethodInvocationImposterGroup(new SumAsyncArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<int>.Any()));
            internal SumAsyncArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public SumAsyncMethodInvocationImposterGroup(SumAsyncArgumentsCriteria argumentsCriteria)
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

            public global::System.Threading.Tasks.Task<int> Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int left, int right, SumAsyncDelegate baseImplementation = null)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, left, right, baseImplementation);
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

                private SumAsyncDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<SumAsyncCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<SumAsyncCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && _resultGenerator == null && _callbacks.Count == 0;

                public global::System.Threading.Tasks.Task<int> Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int left, int right, SumAsyncDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    global::System.Threading.Tasks.Task<int> result = _resultGenerator.Invoke(left, right);
                    foreach (var callback in _callbacks)
                    {
                        callback(left, right);
                    }

                    return result;
                }

                internal void Callback(SumAsyncCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(SumAsyncDelegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(global::System.Threading.Tasks.Task<int> value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int left, int right) =>
                    {
                        return value;
                    };
                }

                internal void ReturnsAsync(int value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int left, int right) =>
                    {
                        return System.Threading.Tasks.Task.FromResult(value);
                    };
                }

                internal void Throws(SumAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int left, int right) =>
                    {
                        throw exceptionGenerator(left, right);
                    };
                }

                internal void ThrowsAsync(System.Exception exception)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int left, int right) =>
                    {
                        return System.Threading.Tasks.Task.FromException<int>(exception);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static async global::System.Threading.Tasks.Task<int> DefaultResultGenerator(int left, int right)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISumAsyncMethodInvocationImposterGroup
        {
            ISumAsyncMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            ISumAsyncMethodInvocationImposterGroup Throws(System.Exception exception);
            ISumAsyncMethodInvocationImposterGroup Throws(SumAsyncExceptionGeneratorDelegate exceptionGenerator);
            ISumAsyncMethodInvocationImposterGroup Callback(SumAsyncCallbackDelegate callback);
            ISumAsyncMethodInvocationImposterGroup Returns(SumAsyncDelegate resultGenerator);
            ISumAsyncMethodInvocationImposterGroup Returns(global::System.Threading.Tasks.Task<int> value);
            ISumAsyncMethodInvocationImposterGroup ReturnsAsync(int value);
            ISumAsyncMethodInvocationImposterGroup ThrowsAsync(System.Exception exception);
            ISumAsyncMethodInvocationImposterGroup UseBaseImplementation();
            ISumAsyncMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface SumAsyncInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)
        public interface ISumAsyncMethodImposterBuilder : ISumAsyncMethodInvocationImposterGroup, SumAsyncInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SumAsyncMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<SumAsyncMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<SumAsyncMethodInvocationImposterGroup>();
            private readonly SumAsyncMethodInvocationHistoryCollection _sumAsyncMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public SumAsyncMethodImposter(SumAsyncMethodInvocationHistoryCollection _sumAsyncMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._sumAsyncMethodInvocationHistoryCollection = _sumAsyncMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(SumAsyncArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private SumAsyncMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(SumAsyncArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public global::System.Threading.Tasks.Task<int> Invoke(int left, int right, SumAsyncDelegate baseImplementation = null)
            {
                var arguments = new SumAsyncArguments(left, right);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)");
                    }

                    matchingInvocationImposterGroup = SumAsyncMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual Task<int> MethodSetupFeatureClassSut.SumAsync(int left, int right)", left, right, baseImplementation);
                    _sumAsyncMethodInvocationHistoryCollection.Add(new SumAsyncMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _sumAsyncMethodInvocationHistoryCollection.Add(new SumAsyncMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ISumAsyncMethodImposterBuilder
            {
                private readonly SumAsyncMethodImposter _imposter;
                private readonly SumAsyncMethodInvocationHistoryCollection _sumAsyncMethodInvocationHistoryCollection;
                private readonly SumAsyncArgumentsCriteria _argumentsCriteria;
                private readonly SumAsyncMethodInvocationImposterGroup _invocationImposterGroup;
                private SumAsyncMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(SumAsyncMethodImposter _imposter, SumAsyncMethodInvocationHistoryCollection _sumAsyncMethodInvocationHistoryCollection, SumAsyncArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._sumAsyncMethodInvocationHistoryCollection = _sumAsyncMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new SumAsyncMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int left, int right) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int left, int right) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Throws(SumAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int left, int right) =>
                    {
                        throw exceptionGenerator.Invoke(left, right);
                    });
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Callback(SumAsyncCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Returns(SumAsyncDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Returns(global::System.Threading.Tasks.Task<int> value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.ReturnsAsync(int value)
                {
                    _currentInvocationImposter.ReturnsAsync(value);
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.ThrowsAsync(System.Exception exception)
                {
                    _currentInvocationImposter.ThrowsAsync(exception);
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                ISumAsyncMethodInvocationImposterGroup ISumAsyncMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void SumAsyncInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _sumAsyncMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)
        public delegate global::System.Threading.Tasks.ValueTask<string> BuildLabelAsyncDelegate(string prefix, string suffix);
        // virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)
        public delegate System.Threading.Tasks.Task BuildLabelAsyncCallbackDelegate(string prefix, string suffix);
        // virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)
        public delegate System.Exception BuildLabelAsyncExceptionGeneratorDelegate(string prefix, string suffix);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class BuildLabelAsyncArguments
        {
            public string prefix;
            public string suffix;
            internal BuildLabelAsyncArguments(string prefix, string suffix)
            {
                this.prefix = prefix;
                this.suffix = suffix;
            }
        }

        // virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class BuildLabelAsyncArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<string> prefix { get; }
            public Imposter.Abstractions.Arg<string> suffix { get; }

            public BuildLabelAsyncArgumentsCriteria(Imposter.Abstractions.Arg<string> prefix, Imposter.Abstractions.Arg<string> suffix)
            {
                this.prefix = prefix;
                this.suffix = suffix;
            }

            public bool Matches(BuildLabelAsyncArguments arguments)
            {
                return prefix.Matches(arguments.prefix) && suffix.Matches(arguments.suffix);
            }
        }

        public interface IBuildLabelAsyncMethodInvocationHistory
        {
            bool Matches(BuildLabelAsyncArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BuildLabelAsyncMethodInvocationHistory : IBuildLabelAsyncMethodInvocationHistory
        {
            internal BuildLabelAsyncArguments Arguments;
            internal global::System.Threading.Tasks.ValueTask<string> Result;
            internal System.Exception Exception;
            public BuildLabelAsyncMethodInvocationHistory(BuildLabelAsyncArguments Arguments, global::System.Threading.Tasks.ValueTask<string> Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(BuildLabelAsyncArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BuildLabelAsyncMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IBuildLabelAsyncMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IBuildLabelAsyncMethodInvocationHistory>();
            internal void Add(IBuildLabelAsyncMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(BuildLabelAsyncArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class BuildLabelAsyncMethodInvocationImposterGroup
        {
            internal static BuildLabelAsyncMethodInvocationImposterGroup Default = new BuildLabelAsyncMethodInvocationImposterGroup(new BuildLabelAsyncArgumentsCriteria(Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<string>.Any()));
            internal BuildLabelAsyncArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public BuildLabelAsyncMethodInvocationImposterGroup(BuildLabelAsyncArgumentsCriteria argumentsCriteria)
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

            public global::System.Threading.Tasks.ValueTask<string> Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string prefix, string suffix, BuildLabelAsyncDelegate baseImplementation = null)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, prefix, suffix, baseImplementation);
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

                private BuildLabelAsyncDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<BuildLabelAsyncCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<BuildLabelAsyncCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && _resultGenerator == null && _callbacks.Count == 0;

                public global::System.Threading.Tasks.ValueTask<string> Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string prefix, string suffix, BuildLabelAsyncDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    global::System.Threading.Tasks.ValueTask<string> result = _resultGenerator.Invoke(prefix, suffix);
                    foreach (var callback in _callbacks)
                    {
                        callback(prefix, suffix);
                    }

                    return result;
                }

                internal void Callback(BuildLabelAsyncCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(BuildLabelAsyncDelegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(global::System.Threading.Tasks.ValueTask<string> value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (string prefix, string suffix) =>
                    {
                        return value;
                    };
                }

                internal void ReturnsAsync(string value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (string prefix, string suffix) =>
                    {
                        return new System.Threading.Tasks.ValueTask<string>(value);
                    };
                }

                internal void Throws(BuildLabelAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (string prefix, string suffix) =>
                    {
                        throw exceptionGenerator(prefix, suffix);
                    };
                }

                internal void ThrowsAsync(System.Exception exception)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (string prefix, string suffix) =>
                    {
                        return new System.Threading.Tasks.ValueTask<string>(System.Threading.Tasks.Task.FromException<string>(exception));
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static async global::System.Threading.Tasks.ValueTask<string> DefaultResultGenerator(string prefix, string suffix)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IBuildLabelAsyncMethodInvocationImposterGroup
        {
            IBuildLabelAsyncMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IBuildLabelAsyncMethodInvocationImposterGroup Throws(System.Exception exception);
            IBuildLabelAsyncMethodInvocationImposterGroup Throws(BuildLabelAsyncExceptionGeneratorDelegate exceptionGenerator);
            IBuildLabelAsyncMethodInvocationImposterGroup Callback(BuildLabelAsyncCallbackDelegate callback);
            IBuildLabelAsyncMethodInvocationImposterGroup Returns(BuildLabelAsyncDelegate resultGenerator);
            IBuildLabelAsyncMethodInvocationImposterGroup Returns(global::System.Threading.Tasks.ValueTask<string> value);
            IBuildLabelAsyncMethodInvocationImposterGroup ReturnsAsync(string value);
            IBuildLabelAsyncMethodInvocationImposterGroup ThrowsAsync(System.Exception exception);
            IBuildLabelAsyncMethodInvocationImposterGroup UseBaseImplementation();
            IBuildLabelAsyncMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface BuildLabelAsyncInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)
        public interface IBuildLabelAsyncMethodImposterBuilder : IBuildLabelAsyncMethodInvocationImposterGroup, BuildLabelAsyncInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BuildLabelAsyncMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<BuildLabelAsyncMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<BuildLabelAsyncMethodInvocationImposterGroup>();
            private readonly BuildLabelAsyncMethodInvocationHistoryCollection _buildLabelAsyncMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public BuildLabelAsyncMethodImposter(BuildLabelAsyncMethodInvocationHistoryCollection _buildLabelAsyncMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._buildLabelAsyncMethodInvocationHistoryCollection = _buildLabelAsyncMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(BuildLabelAsyncArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private BuildLabelAsyncMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(BuildLabelAsyncArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public global::System.Threading.Tasks.ValueTask<string> Invoke(string prefix, string suffix, BuildLabelAsyncDelegate baseImplementation = null)
            {
                var arguments = new BuildLabelAsyncArguments(prefix, suffix);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)");
                    }

                    matchingInvocationImposterGroup = BuildLabelAsyncMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual ValueTask<string> MethodSetupFeatureClassSut.BuildLabelAsync(string prefix, string suffix)", prefix, suffix, baseImplementation);
                    _buildLabelAsyncMethodInvocationHistoryCollection.Add(new BuildLabelAsyncMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _buildLabelAsyncMethodInvocationHistoryCollection.Add(new BuildLabelAsyncMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IBuildLabelAsyncMethodImposterBuilder
            {
                private readonly BuildLabelAsyncMethodImposter _imposter;
                private readonly BuildLabelAsyncMethodInvocationHistoryCollection _buildLabelAsyncMethodInvocationHistoryCollection;
                private readonly BuildLabelAsyncArgumentsCriteria _argumentsCriteria;
                private readonly BuildLabelAsyncMethodInvocationImposterGroup _invocationImposterGroup;
                private BuildLabelAsyncMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(BuildLabelAsyncMethodImposter _imposter, BuildLabelAsyncMethodInvocationHistoryCollection _buildLabelAsyncMethodInvocationHistoryCollection, BuildLabelAsyncArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._buildLabelAsyncMethodInvocationHistoryCollection = _buildLabelAsyncMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new BuildLabelAsyncMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string prefix, string suffix) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string prefix, string suffix) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Throws(BuildLabelAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string prefix, string suffix) =>
                    {
                        throw exceptionGenerator.Invoke(prefix, suffix);
                    });
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Callback(BuildLabelAsyncCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Returns(BuildLabelAsyncDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Returns(global::System.Threading.Tasks.ValueTask<string> value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.ReturnsAsync(string value)
                {
                    _currentInvocationImposter.ReturnsAsync(value);
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.ThrowsAsync(System.Exception exception)
                {
                    _currentInvocationImposter.ThrowsAsync(exception);
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IBuildLabelAsyncMethodInvocationImposterGroup IBuildLabelAsyncMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void BuildLabelAsyncInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _buildLabelAsyncMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        public delegate int RefOutWithParamsDelegate(ref int seed, out int doubled, int[] adjustments);
        // virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        public delegate void RefOutWithParamsCallbackDelegate(ref int seed, out int doubled, int[] adjustments);
        // virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        public delegate System.Exception RefOutWithParamsExceptionGeneratorDelegate(ref int seed, out int doubled, int[] adjustments);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class RefOutWithParamsArguments
        {
            public int seed;
            public int[] adjustments;
            internal RefOutWithParamsArguments(int seed, int[] adjustments)
            {
                this.seed = seed;
                this.adjustments = adjustments;
            }
        }

        // virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class RefOutWithParamsArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> seed { get; }
            public Imposter.Abstractions.OutArg<int> doubled { get; }
            public Imposter.Abstractions.Arg<int[]> adjustments { get; }

            public RefOutWithParamsArgumentsCriteria(Imposter.Abstractions.Arg<int> seed, Imposter.Abstractions.OutArg<int> doubled, Imposter.Abstractions.Arg<int[]> adjustments)
            {
                this.seed = seed;
                this.doubled = doubled;
                this.adjustments = adjustments;
            }

            public bool Matches(RefOutWithParamsArguments arguments)
            {
                return seed.Matches(arguments.seed) && adjustments.Matches(arguments.adjustments);
            }
        }

        public interface IRefOutWithParamsMethodInvocationHistory
        {
            bool Matches(RefOutWithParamsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class RefOutWithParamsMethodInvocationHistory : IRefOutWithParamsMethodInvocationHistory
        {
            internal RefOutWithParamsArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public RefOutWithParamsMethodInvocationHistory(RefOutWithParamsArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(RefOutWithParamsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class RefOutWithParamsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IRefOutWithParamsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IRefOutWithParamsMethodInvocationHistory>();
            internal void Add(IRefOutWithParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(RefOutWithParamsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class RefOutWithParamsMethodInvocationImposterGroup
        {
            internal static RefOutWithParamsMethodInvocationImposterGroup Default = new RefOutWithParamsMethodInvocationImposterGroup(new RefOutWithParamsArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.OutArg<int>.Any(), Imposter.Abstractions.Arg<int[]>.Any()));
            internal RefOutWithParamsArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public RefOutWithParamsMethodInvocationImposterGroup(RefOutWithParamsArgumentsCriteria argumentsCriteria)
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

            public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ref int seed, out int doubled, int[] adjustments, RefOutWithParamsDelegate baseImplementation = null)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, ref seed, out doubled, adjustments, baseImplementation);
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

                private RefOutWithParamsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<RefOutWithParamsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<RefOutWithParamsCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ref int seed, out int doubled, int[] adjustments, RefOutWithParamsDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(ref seed, out doubled, adjustments);
                    foreach (var callback in _callbacks)
                    {
                        callback(ref seed, out doubled, adjustments);
                    }

                    return result;
                }

                internal void Callback(RefOutWithParamsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(RefOutWithParamsDelegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (ref int seed, out int doubled, int[] adjustments) =>
                    {
                        InitializeOutParametersWithDefaultValues(out doubled);
                        return value;
                    };
                }

                internal void Throws(RefOutWithParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (ref int seed, out int doubled, int[] adjustments) =>
                    {
                        throw exceptionGenerator(ref seed, out doubled, adjustments);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                private static void InitializeOutParametersWithDefaultValues(out int doubled)
                {
                    doubled = default(int);
                }

                internal static int DefaultResultGenerator(ref int seed, out int doubled, int[] adjustments)
                {
                    InitializeOutParametersWithDefaultValues(out doubled);
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IRefOutWithParamsMethodInvocationImposterGroup
        {
            IRefOutWithParamsMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IRefOutWithParamsMethodInvocationImposterGroup Throws(System.Exception exception);
            IRefOutWithParamsMethodInvocationImposterGroup Throws(RefOutWithParamsExceptionGeneratorDelegate exceptionGenerator);
            IRefOutWithParamsMethodInvocationImposterGroup Callback(RefOutWithParamsCallbackDelegate callback);
            IRefOutWithParamsMethodInvocationImposterGroup Returns(RefOutWithParamsDelegate resultGenerator);
            IRefOutWithParamsMethodInvocationImposterGroup Returns(int value);
            IRefOutWithParamsMethodInvocationImposterGroup UseBaseImplementation();
            IRefOutWithParamsMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface RefOutWithParamsInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)
        public interface IRefOutWithParamsMethodImposterBuilder : IRefOutWithParamsMethodInvocationImposterGroup, RefOutWithParamsInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class RefOutWithParamsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<RefOutWithParamsMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<RefOutWithParamsMethodInvocationImposterGroup>();
            private readonly RefOutWithParamsMethodInvocationHistoryCollection _refOutWithParamsMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public RefOutWithParamsMethodImposter(RefOutWithParamsMethodInvocationHistoryCollection _refOutWithParamsMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._refOutWithParamsMethodInvocationHistoryCollection = _refOutWithParamsMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private static void InitializeOutParametersWithDefaultValues(out int doubled)
            {
                doubled = default(int);
            }

            public bool HasMatchingSetup(RefOutWithParamsArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private RefOutWithParamsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(RefOutWithParamsArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(ref int seed, out int doubled, int[] adjustments, RefOutWithParamsDelegate baseImplementation = null)
            {
                var arguments = new RefOutWithParamsArguments(seed, adjustments);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)");
                    }

                    matchingInvocationImposterGroup = RefOutWithParamsMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual int MethodSetupFeatureClassSut.RefOutWithParams(ref int seed, out int doubled, params int[] adjustments)", ref seed, out doubled, adjustments, baseImplementation);
                    _refOutWithParamsMethodInvocationHistoryCollection.Add(new RefOutWithParamsMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _refOutWithParamsMethodInvocationHistoryCollection.Add(new RefOutWithParamsMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IRefOutWithParamsMethodImposterBuilder
            {
                private readonly RefOutWithParamsMethodImposter _imposter;
                private readonly RefOutWithParamsMethodInvocationHistoryCollection _refOutWithParamsMethodInvocationHistoryCollection;
                private readonly RefOutWithParamsArgumentsCriteria _argumentsCriteria;
                private readonly RefOutWithParamsMethodInvocationImposterGroup _invocationImposterGroup;
                private RefOutWithParamsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(RefOutWithParamsMethodImposter _imposter, RefOutWithParamsMethodInvocationHistoryCollection _refOutWithParamsMethodInvocationHistoryCollection, RefOutWithParamsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._refOutWithParamsMethodInvocationHistoryCollection = _refOutWithParamsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new RefOutWithParamsMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((ref int seed, out int doubled, int[] adjustments) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((ref int seed, out int doubled, int[] adjustments) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Throws(RefOutWithParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((ref int seed, out int doubled, int[] adjustments) =>
                    {
                        throw exceptionGenerator.Invoke(ref seed, out doubled, adjustments);
                    });
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Callback(RefOutWithParamsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Returns(RefOutWithParamsDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IRefOutWithParamsMethodInvocationImposterGroup IRefOutWithParamsMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void RefOutWithParamsInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _refOutWithParamsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public MethodSetupFeatureClassSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._intSingleParamMethodImposter = new IntSingleParamMethodImposter(_intSingleParamMethodInvocationHistoryCollection, invocationBehavior);
            this._voidWithSideEffectMethodImposter = new VoidWithSideEffectMethodImposter(_voidWithSideEffectMethodInvocationHistoryCollection, invocationBehavior);
            this._sumAsyncMethodImposter = new SumAsyncMethodImposter(_sumAsyncMethodInvocationHistoryCollection, invocationBehavior);
            this._buildLabelAsyncMethodImposter = new BuildLabelAsyncMethodImposter(_buildLabelAsyncMethodInvocationHistoryCollection, invocationBehavior);
            this._refOutWithParamsMethodImposter = new RefOutWithParamsMethodImposter(_refOutWithParamsMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut
        {
            MethodSetupFeatureClassSutImposter _imposter;
            internal void InitializeImposter(MethodSetupFeatureClassSutImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            internal override int IntSingleParam(int age)
            {
                return _imposter._intSingleParamMethodImposter.Invoke(age, base.IntSingleParam);
            }

            internal override void VoidWithSideEffect(int delta)
            {
                _imposter._voidWithSideEffectMethodImposter.Invoke(delta, base.VoidWithSideEffect);
            }

            internal override global::System.Threading.Tasks.Task<int> SumAsync(int left, int right)
            {
                return _imposter._sumAsyncMethodImposter.Invoke(left, right, base.SumAsync);
            }

            internal override global::System.Threading.Tasks.ValueTask<string> BuildLabelAsync(string prefix, string suffix)
            {
                return _imposter._buildLabelAsyncMethodImposter.Invoke(prefix, suffix, base.BuildLabelAsync);
            }

            internal override int RefOutWithParams(ref int seed, out int doubled, int[] adjustments)
            {
                return _imposter._refOutWithParamsMethodImposter.Invoke(ref seed, out doubled, adjustments, base.RefOutWithParams);
            }
        }
    }
}