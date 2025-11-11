#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using global::Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Playground;

namespace Imposter.Playground
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IGenericMethodPocV1Imposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IGenericMethodPocV1>
    {
        private readonly GenericAllRefKindMethodImposterCollection _genericAllRefKindMethodImposterCollection;
        private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection = new GenericAllRefKindMethodInvocationHistoryCollection();
        public IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(global::Imposter.Abstractions.OutArg<TOut> outValue, global::Imposter.Abstractions.Arg<TRef> refValue, global::Imposter.Abstractions.Arg<TIn> inValue, global::Imposter.Abstractions.Arg<TParams[]> paramsValues)
        {
            return new GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>.Builder(_genericAllRefKindMethodImposterCollection, _genericAllRefKindMethodInvocationHistoryCollection, new GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(outValue, refValue, inValue, paramsValues));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IGenericMethodPocV1 global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IGenericMethodPocV1>.Instance()
        {
            return _imposterInstance;
        }

        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate TResult GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate void GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate System.Exception GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult>
        {
            public TRef refValue;
            public TIn inValue;
            public TParams[] paramsValues;
            internal GenericAllRefKindArguments(TRef refValue, TIn inValue, TParams[] paramsValues)
            {
                this.refValue = refValue;
                this.inValue = inValue;
                this.paramsValues = paramsValues;
            }

            public GenericAllRefKindArguments<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                return new GenericAllRefKindArguments<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(TypeCaster.Cast<TRef, TRefTarget>(refValue), TypeCaster.Cast<TIn, TInTarget>(inValue), TypeCaster.Cast<TParams[], TParamsTarget[]>(paramsValues));
            }
        }

        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>
        {
            public global::Imposter.Abstractions.OutArg<TOut> outValue { get; }
            public global::Imposter.Abstractions.Arg<TRef> refValue { get; }
            public global::Imposter.Abstractions.Arg<TIn> inValue { get; }
            public global::Imposter.Abstractions.Arg<TParams[]> paramsValues { get; }

            public GenericAllRefKindArgumentsCriteria(global::Imposter.Abstractions.OutArg<TOut> outValue, global::Imposter.Abstractions.Arg<TRef> refValue, global::Imposter.Abstractions.Arg<TIn> inValue, global::Imposter.Abstractions.Arg<TParams[]> paramsValues)
            {
                this.outValue = outValue;
                this.refValue = refValue;
                this.inValue = inValue;
                this.paramsValues = paramsValues;
            }

            public bool Matches(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return (refValue.Matches(arguments.refValue) && inValue.Matches(arguments.inValue)) && paramsValues.Matches(arguments.paramsValues);
            }

            public GenericAllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                return new GenericAllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(global::Imposter.Abstractions.OutArg<TOutTarget>.Any(), global::Imposter.Abstractions.Arg<TRefTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TRefTarget, TRef>(it, out TRef refValueTarget) && refValue.Matches(refValueTarget)), global::Imposter.Abstractions.Arg<TInTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TInTarget, TIn>(it, out TIn inValueTarget) && inValue.Matches(inValueTarget)), global::Imposter.Abstractions.Arg<TParamsTarget[]>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TParamsTarget[], TParams[]>(it, out TParams[] paramsValuesTarget) && paramsValues.Matches(paramsValuesTarget)));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericAllRefKindMethodInvocationHistory
        {
            bool Matches<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(GenericAllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericAllRefKindMethodInvocationHistory<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodInvocationHistory
        {
            internal GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> Arguments;
            internal TResult Result;
            internal System.Exception Exception;
            public GenericAllRefKindMethodInvocationHistory(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> Arguments, TResult Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(GenericAllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> criteria)
            {
                return (((((typeof(TOutTarget) == typeof(TOut)) && (typeof(TRefTarget) == typeof(TRef))) && (typeof(TInTarget) == typeof(TIn))) && (typeof(TParamsTarget[]) == typeof(TParams[]))) && (typeof(TResult) == typeof(TResultTarget))) && criteria.As<TOut, TRef, TIn, TParams, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericAllRefKindMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericAllRefKindMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericAllRefKindMethodInvocationHistory>();
            internal void Add(IGenericAllRefKindMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TOut, TRef, TIn, TParams, TResult>(GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TOut, TRef, TIn, TParams, TResult>(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericAllRefKindMethodImposterCollection
        {
            private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public GenericAllRefKindMethodImposterCollection(GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._genericAllRefKindMethodInvocationHistoryCollection = _genericAllRefKindMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericAllRefKindMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericAllRefKindMethodImposter>();
            internal GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> AddNew<TOut, TRef, TIn, TParams, TResult>()
            {
                var imposter = new GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>(_genericAllRefKindMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> GetImposterWithMatchingSetup<TOut, TRef, TIn, TParams, TResult>(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TOut, TRef, TIn, TParams, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TOut, TRef, TIn, TParams, TResult>();
            }
        }

        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>
        {
            internal static GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult> Default = new GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>(new GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(global::Imposter.Abstractions.OutArg<TOut>.Any(), global::Imposter.Abstractions.Arg<TRef>.Any(), global::Imposter.Abstractions.Arg<TIn>.Any(), global::Imposter.Abstractions.Arg<TParams[]>.Any()));
            internal GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public GenericAllRefKindMethodInvocationImposterGroup(GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> argumentsCriteria)
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

            public TResult Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, out outValue, ref refValue, in inValue, paramsValues);
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

                private GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public TResult Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    TResult result = _resultGenerator.Invoke(out outValue, ref refValue, in inValue, paramsValues);
                    foreach (var callback in _callbacks)
                    {
                        callback(out outValue, ref refValue, in inValue, paramsValues);
                    }

                    return result;
                }

                internal void Callback(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(TResult value)
                {
                    _resultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                    {
                        InitializeOutParametersWithDefaultValues(out outValue);
                        return value;
                    };
                }

                internal void Throws(GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator)
                {
                    _resultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                    {
                        throw exceptionGenerator(out outValue, ref refValue, in inValue, paramsValues);
                    };
                }

                private static void InitializeOutParametersWithDefaultValues(out TOut outValue)
                {
                    outValue = default(TOut);
                }

                internal static TResult DefaultResultGenerator(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericAllRefKindMethodInvocationImposterGroupCallback<TOut, TRef, TIn, TParams, TResult>
        {
            IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> Callback(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodInvocationImposterGroupCallback<TOut, TRef, TIn, TParams, TResult>
        {
            IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodInvocationImposterGroupCallback<TOut, TRef, TIn, TParams, TResult>
        {
            IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> Throws<TException>()
                where TException : System.Exception, new();
            IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> Throws(System.Exception exception);
            IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> Throws(GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator);
            IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> Returns(GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator);
            IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> Returns(TResult value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericAllRefKindMethodImposter
        {
            IGenericAllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>? As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodImposter
        {
            TResult Invoke(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
            bool HasMatchingSetup(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericAllRefKindInvocationVerifier<TOut, TRef, TIn, TParams, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public interface IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>, IGenericAllRefKindMethodInvocationImposterGroupCallback<TOut, TRef, TIn, TParams, TResult>, GenericAllRefKindInvocationVerifier<TOut, TRef, TIn, TParams, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>>();
            private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public GenericAllRefKindMethodImposter(GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._genericAllRefKindMethodInvocationHistoryCollection = _genericAllRefKindMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            IGenericAllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>? IGenericAllRefKindMethodImposter.As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                if ((((typeof(TOut).IsAssignableTo(typeof(TOutTarget)) && (typeof(TRefTarget) == typeof(TRef))) && typeof(TInTarget).IsAssignableTo(typeof(TIn))) && typeof(TParamsTarget[]).IsAssignableTo(typeof(TParams[]))) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> : IGenericAllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>
            {
                private readonly GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> _target;
                public Adapter(GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(out TOutTarget outValue, ref TRefTarget refValue, in TInTarget inValue, TParamsTarget[] paramsValues)
                {
                    TOut outValueAdapted;
                    TRef refValueAdapted = global::Imposter.Abstractions.TypeCaster.Cast<TRefTarget, TRef>(refValue);
                    var result = _target.Invoke(out outValueAdapted, ref refValueAdapted, global::Imposter.Abstractions.TypeCaster.Cast<TInTarget, TIn>(inValue), global::Imposter.Abstractions.TypeCaster.Cast<TParamsTarget[], TParams[]>(paramsValues));
                    outValue = global::Imposter.Abstractions.TypeCaster.Cast<TOut, TOutTarget>(outValueAdapted);
                    refValue = global::Imposter.Abstractions.TypeCaster.Cast<TRef, TRefTarget>(refValueAdapted);
                    return global::Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(GenericAllRefKindArguments<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TOut, TRef, TIn, TParams, TResult>());
                }

                IGenericAllRefKindMethodImposter<TOutTarget1, TRefTarget1, TInTarget1, TParamsTarget1, TResultTarget1>? IGenericAllRefKindMethodImposter.As<TOutTarget1, TRefTarget1, TInTarget1, TParamsTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out TOut outValue)
            {
                outValue = default(TOut);
            }

            public bool HasMatchingSetup(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>? FindMatchingInvocationImposterGroup(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public TResult Invoke(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                var arguments = new GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult>(refValue, inValue, paramsValues);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)");
                    }

                    matchingInvocationImposterGroup = GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)", out outValue, ref refValue, in inValue, paramsValues);
                    _genericAllRefKindMethodInvocationHistoryCollection.Add(new GenericAllRefKindMethodInvocationHistory<TOut, TRef, TIn, TParams, TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericAllRefKindMethodInvocationHistoryCollection.Add(new GenericAllRefKindMethodInvocationHistory<TOut, TRef, TIn, TParams, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult>, IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult>
            {
                private readonly GenericAllRefKindMethodImposterCollection _imposterCollection;
                private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection;
                private readonly GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> _argumentsCriteria;
                private readonly GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult> _invocationImposterGroup;
                private GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(GenericAllRefKindMethodImposterCollection _imposterCollection, GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection, GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericAllRefKindMethodInvocationHistoryCollection = _genericAllRefKindMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new GenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>(_argumentsCriteria);
                    GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> methodImposter = _imposterCollection.AddNew<TOut, TRef, TIn, TParams, TResult>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.Throws(GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                    {
                        throw exceptionGenerator.Invoke(out outValue, ref refValue, in inValue, paramsValues);
                    });
                    return this;
                }

                IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroupCallback<TOut, TRef, TIn, TParams, TResult>.Callback(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.Returns(GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult>.Returns(TResult value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IGenericAllRefKindMethodInvocationImposterGroup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationImposterGroupContinuation<TOut, TRef, TIn, TParams, TResult>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void GenericAllRefKindInvocationVerifier<TOut, TRef, TIn, TParams, TResult>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericAllRefKindMethodInvocationHistoryCollection.Count<TOut, TRef, TIn, TParams, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IGenericMethodPocV1Imposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._genericAllRefKindMethodImposterCollection = new GenericAllRefKindMethodImposterCollection(_genericAllRefKindMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IGenericMethodPocV1
        {
            IGenericMethodPocV1Imposter _imposter;
            public ImposterTargetInstance(IGenericMethodPocV1Imposter _imposter)
            {
                this._imposter = _imposter;
            }

            public TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                return _imposter._genericAllRefKindMethodImposterCollection.GetImposterWithMatchingSetup<TOut, TRef, TIn, TParams, TResult>(new GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult>(refValue, inValue, paramsValues)).Invoke(out outValue, ref refValue, in inValue, paramsValues);
            }
        }
    }
}
#pragma warning restore nullable
