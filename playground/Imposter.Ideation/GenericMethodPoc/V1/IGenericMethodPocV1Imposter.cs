using System;
using System.Linq;
using System.Collections.Generic;
using Imposter.Abstractions;

#pragma warning disable nullable

// ReSharper disable once CheckNamespace
namespace Imposter.Playground
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IGenericMethodPocV1Imposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IGenericMethodPocV1>
    {
        private readonly GenericAllRefKindMethodImposterCollection _genericAllRefKindMethodImposterCollection;
        private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection = new GenericAllRefKindMethodInvocationHistoryCollection();
        public IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(Imposter.Abstractions.OutArg<TOut> outValue, Imposter.Abstractions.Arg<TRef> refValue, Imposter.Abstractions.Arg<TIn> inValue, Imposter.Abstractions.Arg<TParams[]> paramsValues)
        {
            return new GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>.Builder(_genericAllRefKindMethodImposterCollection, _genericAllRefKindMethodInvocationHistoryCollection, new GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(outValue, refValue, inValue, paramsValues));
        }

        private ImposterTargetInstance _imposterInstance;
        public IGenericMethodPocV1Imposter()
        {
            this._genericAllRefKindMethodImposterCollection = new GenericAllRefKindMethodImposterCollection(_genericAllRefKindMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.Playground.IGenericMethodPocV1 Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IGenericMethodPocV1>.Instance()
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
            public Imposter.Abstractions.OutArg<TOut> outValue { get; }
            public Imposter.Abstractions.Arg<TRef> refValue { get; }
            public Imposter.Abstractions.Arg<TIn> inValue { get; }
            public Imposter.Abstractions.Arg<TParams[]> paramsValues { get; }

            public GenericAllRefKindArgumentsCriteria(Imposter.Abstractions.OutArg<TOut> outValue, Imposter.Abstractions.Arg<TRef> refValue, Imposter.Abstractions.Arg<TIn> inValue, Imposter.Abstractions.Arg<TParams[]> paramsValues)
            {
                this.outValue = outValue;
                this.refValue = refValue;
                this.inValue = inValue;
                this.paramsValues = paramsValues;
            }

            public bool Matches(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return refValue.Matches(arguments.refValue) && inValue.Matches(arguments.inValue) && paramsValues.Matches(arguments.paramsValues);
            }

            public GenericAllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                return new GenericAllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(Imposter.Abstractions.OutArg<TOutTarget>.Any(), Imposter.Abstractions.Arg<TRefTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TRefTarget, TRef>(it, out TRef refValueTarget) && refValue.Matches(refValueTarget)), Imposter.Abstractions.Arg<TInTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TInTarget, TIn>(it, out TIn inValueTarget) && inValue.Matches(inValueTarget)), Imposter.Abstractions.Arg<TParamsTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TParamsTarget[], TParams[]>(it, out TParams[] paramsValuesTarget) && paramsValues.Matches(paramsValuesTarget)));
            }
        }

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

        internal class GenericAllRefKindMethodImposterCollection
        {
            private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection;
            public GenericAllRefKindMethodImposterCollection(GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection)
            {
                this._genericAllRefKindMethodInvocationHistoryCollection = _genericAllRefKindMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericAllRefKindMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericAllRefKindMethodImposter>();
            internal GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> AddNew<TOut, TRef, TIn, TParams, TResult>()
            {
                var imposter = new GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>(_genericAllRefKindMethodInvocationHistoryCollection);
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
        class GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>
        {
            internal static GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> DefaultInvocationSetup = new GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>(new GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(Imposter.Abstractions.OutArg<TOut>.Any(), Imposter.Abstractions.Arg<TRef>.Any(), Imposter.Abstractions.Arg<TIn>.Any(), Imposter.Abstractions.Arg<TParams[]>.Any()));
            internal GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> ArgumentsCriteria { get; }

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

            internal static TResult DefaultResultGenerator(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                InitializeOutParametersWithDefaultValues(out outValue);
                return default;
            }

            public GenericAllRefKindMethodInvocationsSetup(GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TOut outValue)
            {
                outValue = default(TOut);
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Returns(GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    return value;
                };
                return this;
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw new TException();
                };
                return this;
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw exception;
                };
                return this;
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws(GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    throw exceptionGenerator(out outValue, ref refValue, in inValue, paramsValues);
                };
                return this;
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.CallBefore(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.CallAfter(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
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

            public TResult Invoke(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(out outValue, ref refValue, in inValue, paramsValues);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(out outValue, ref refValue, in inValue, paramsValues);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(out outValue, ref refValue, in inValue, paramsValues);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult>? ResultGenerator { get; set; }
                internal GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>? CallBefore { get; set; }
                internal GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>
        {
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws(System.Exception exception);
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws(GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator);
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> CallBefore(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback);
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> CallAfter(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback);
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Returns(GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator);
            IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Returns(TResult value);
        }

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
        public interface GenericAllRefKindMethodInvocationVerifier<TOut, TRef, TIn, TParams, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericMethodPocV1.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public interface IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>, GenericAllRefKindMethodInvocationVerifier<TOut, TRef, TIn, TParams, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> : IGenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>>();
            private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection;
            public GenericAllRefKindMethodImposter(GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection)
            {
                this._genericAllRefKindMethodInvocationHistoryCollection = _genericAllRefKindMethodInvocationHistoryCollection;
            }

            IGenericAllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>? IGenericAllRefKindMethodImposter.As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                if (typeof(TOut).IsAssignableTo(typeof(TOutTarget)) && typeof(TRefTarget) == typeof(TRef) && typeof(TInTarget).IsAssignableTo(typeof(TIn)) && typeof(TParamsTarget[]).IsAssignableTo(typeof(TParams[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(this);
                }

                return null;
            }

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
                    TRef refValueAdapted = Imposter.Abstractions.TypeCaster.Cast<TRefTarget, TRef>(refValue);
                    var result = _target.Invoke(out outValueAdapted, ref refValueAdapted, Imposter.Abstractions.TypeCaster.Cast<TInTarget, TIn>(inValue), Imposter.Abstractions.TypeCaster.Cast<TParamsTarget[], TParams[]>(paramsValues));
                    outValue = Imposter.Abstractions.TypeCaster.Cast<TOut, TOutTarget>(outValueAdapted);
                    refValue = Imposter.Abstractions.TypeCaster.Cast<TRef, TRefTarget>(refValueAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
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
                return FindMatchingSetup(arguments) != null;
            }

            private GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>? FindMatchingSetup(GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public TResult Invoke(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                var arguments = new GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult>(refValue, inValue, paramsValues);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out outValue, ref refValue, in inValue, paramsValues);
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
            internal class Builder : IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult>
            {
                private readonly GenericAllRefKindMethodImposterCollection _imposterCollection;
                private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection;
                private readonly GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> _argumentsCriteria;
                private GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>? _existingInvocationSetup;
                public Builder(GenericAllRefKindMethodImposterCollection _imposterCollection, GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection, GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericAllRefKindMethodInvocationHistoryCollection = _genericAllRefKindMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TOut, TRef, TIn, TParams, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws(GenericAllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.CallBefore(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.CallAfter(GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Returns(GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGenericAllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void GenericAllRefKindMethodInvocationVerifier<TOut, TRef, TIn, TParams, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericAllRefKindMethodInvocationHistoryCollection.Count<TOut, TRef, TIn, TParams, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
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
    
    public interface IGenericMethodPocV1
    {
        TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(
            out TOut outValue,
            ref TRef refValue,
            in TIn inValue,
            params TParams[] paramsValues);
    }
}