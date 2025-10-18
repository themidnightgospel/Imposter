using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Setup.Methods.Generics;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IGenericAnimalImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.IGenericAnimal>
    {
        private readonly RoarMethodImposterCollection _roarMethodImposterCollection;
        private readonly ChirpMethodImposterCollection _chirpMethodImposterCollection;
        private readonly MeowMethodImposterCollection _meowMethodImposterCollection;
        private readonly BarkMethodImposter _barkMethodImposter;
        private readonly SumMethodImposter _sumMethodImposter;
        private readonly RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection = new RoarMethodInvocationHistoryCollection();
        private readonly ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection = new ChirpMethodInvocationHistoryCollection();
        private readonly MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection = new MeowMethodInvocationHistoryCollection();
        private readonly BarkMethodInvocationHistoryCollection _barkMethodInvocationHistoryCollection = new BarkMethodInvocationHistoryCollection();
        private readonly SumMethodInvocationHistoryCollection _sumMethodInvocationHistoryCollection = new SumMethodInvocationHistoryCollection();
        public IRoarMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<TOrdinaryParam> ordinaryParam, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
        {
            return new RoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Builder(_roarMethodImposterCollection, _roarMethodInvocationHistoryCollection, new RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ordinaryParam, outParam, inParam, refParam, paramsParam));
        }

        public IChirpMethodImposterBuilder<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<int> age, Arg<string> name, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
        {
            return new ChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Builder(_chirpMethodImposterCollection, _chirpMethodInvocationHistoryCollection, new ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(age, name, outParam, inParam, refParam, paramsParam));
        }

        public IMeowMethodImposterBuilder<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<int> age, OutArg<global::System.Collections.Generic.List<TOutParam>> outParam, Arg<global::System.Collections.Generic.List<TInParam>> inParam, Arg<global::System.Collections.Generic.Dictionary<int, TRefParam>> refParam, Arg<TParamsParam[]> paramsParam)
        {
            return new MeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Builder(_meowMethodImposterCollection, _meowMethodInvocationHistoryCollection, new MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(age, outParam, inParam, refParam, paramsParam));
        }

        public IBarkMethodImposterBuilder Bark(Arg<string> name)
        {
            return new BarkMethodImposter.Builder(_barkMethodImposter, _barkMethodInvocationHistoryCollection, new BarkArgumentsCriteria(name));
        }

        public ISumMethodImposterBuilder Sum(Arg<int> left, Arg<int> right)
        {
            return new SumMethodImposter.Builder(_sumMethodImposter, _sumMethodInvocationHistoryCollection, new SumArgumentsCriteria(left, right));
        }

        private ImposterTargetInstance _imposterInstance;
        public IGenericAnimalImposter()
        {
            this._roarMethodImposterCollection = new RoarMethodImposterCollection(_roarMethodInvocationHistoryCollection);
            this._chirpMethodImposterCollection = new ChirpMethodImposterCollection(_chirpMethodInvocationHistoryCollection);
            this._meowMethodImposterCollection = new MeowMethodImposterCollection(_meowMethodInvocationHistoryCollection);
            this._barkMethodImposter = new BarkMethodImposter(_barkMethodInvocationHistoryCollection);
            this._sumMethodImposter = new SumMethodImposter(_sumMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.IGenericAnimal
        {
            IGenericAnimalImposter _imposter;
            public ImposterTargetInstance(IGenericAnimalImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public TResult Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                return _imposter._roarMethodImposterCollection.GetImposterWithMatchingSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam)).Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
            }

            public TResult Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                return _imposter._chirpMethodImposterCollection.GetImposterWithMatchingSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(age, name, inParam, refParam, paramsParam)).Invoke(age, name, out outParam, in inParam, ref refParam, paramsParam);
            }

            public TResult Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
            {
                return _imposter._meowMethodImposterCollection.GetImposterWithMatchingSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(age, inParam, refParam, paramsParam)).Invoke(age, out outParam, in inParam, ref refParam, paramsParam);
            }

            public void Bark(string name)
            {
                _imposter._barkMethodImposter.Invoke(name);
            }

            public int Sum(int left, int right)
            {
                return _imposter._sumMethodImposter.Invoke(left, right);
            }
        }

        global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.IGenericAnimal Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.IGenericAnimal>.Instance()
        {
            return _imposterInstance;
        }

        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate TResult RoarDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate void RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate System.Exception RoarExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        public class RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public TOrdinaryParam ordinaryParam { get; }
            public TInParam inParam { get; }
            public TRefParam refParam { get; }
            public TParamsParam[] paramsParam { get; }

            public RoarArguments(TOrdinaryParam ordinaryParam, TInParam inParam, TRefParam refParam, TParamsParam[] paramsParam)
            {
                this.ordinaryParam = ordinaryParam;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public RoarArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new RoarArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(TypeCaster.Cast<TOrdinaryParam, TOrdinaryParamTarget>(ordinaryParam), TypeCaster.Cast<TInParam, TInParamTarget>(inParam), TypeCaster.Cast<TRefParam, TRefParamTarget>(refParam), TypeCaster.Cast<TParamsParam[], TParamsParamTarget[]>(paramsParam));
            }
        }

        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public Arg<TOrdinaryParam> ordinaryParam { get; }
            public OutArg<TOutParam> outParam { get; }
            public Arg<TInParam> inParam { get; }
            public Arg<TRefParam> refParam { get; }
            public Arg<TParamsParam[]> paramsParam { get; }

            public RoarArgumentsCriteria(Arg<TOrdinaryParam> ordinaryParam, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
            {
                this.ordinaryParam = ordinaryParam;
                this.outParam = outParam;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public bool Matches(RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return ordinaryParam.Matches(arguments.ordinaryParam) && inParam.Matches(arguments.inParam) && refParam.Matches(arguments.refParam) && paramsParam.Matches(arguments.paramsParam);
            }

            public RoarArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new RoarArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(Imposter.Abstractions.Arg<TOrdinaryParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TOrdinaryParamTarget, TOrdinaryParam>(it, out TOrdinaryParam ordinaryParamTarget) && ordinaryParam.Matches(ordinaryParamTarget)), Imposter.Abstractions.OutArg<TOutParamTarget>.Any(), Imposter.Abstractions.Arg<TInParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TInParamTarget, TInParam>(it, out TInParam inParamTarget) && inParam.Matches(inParamTarget)), Imposter.Abstractions.Arg<TRefParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TRefParamTarget, TRefParam>(it, out TRefParam refParamTarget) && refParam.Matches(refParamTarget)), Imposter.Abstractions.Arg<TParamsParamTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TParamsParamTarget[], TParamsParam[]>(it, out TParamsParam[] paramsParamTarget) && paramsParam.Matches(paramsParamTarget)));
            }
        }

        public interface IRoarMethodInvocationHistory
        {
            bool Matches<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> criteria);
        }

        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public class RoarMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IRoarMethodInvocationHistory
        {
            public RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Arguments { get; }
            public TResult? Result { get; }
            public System.Exception? Exception { get; }

            public RoarMethodInvocationHistory(RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments, TResult? result = default(TResult? ), System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(RoarArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> criteria)
            {
                return criteria.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class RoarMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IRoarMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IRoarMethodInvocationHistory>();
            internal void Add(IRoarMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(argumentsCriteria));
            }
        }

        internal class RoarMethodImposterCollection
        {
            private readonly RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection;
            public RoarMethodImposterCollection(RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection)
            {
                this._roarMethodInvocationHistoryCollection = _roarMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IRoarMethodImposter> _imposters = new ConcurrentStack<IRoarMethodImposter>();
            internal RoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()
            {
                var imposter = new RoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_roarMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IRoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetImposterWithMatchingSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return _imposters.Select(it => it.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()).Where(it => it is not null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>();
            }
        }

        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            internal static RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> DefaultInvocationSetup = new RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<TOrdinaryParam>.Any(), OutArg<TOutParam>.Any(), Arg<TInParam>.Any(), Arg<TRefParam>.Any(), Arg<TParamsParam[]>.Any()));
            internal RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> ArgumentsCriteria { get; }

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

            internal static TResult DefaultResultGenerator(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                InitializeOutParametersWithDefaultValues(out outParam);
                return default(TResult);
            }

            public RoarMethodInvocationsSetup(RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
            {
                outParam = default(TOutParam);
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(RoarDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    return value;
                };
                return this;
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw new TException();
                };
                return this;
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw ex;
                };
                return this;
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(RoarExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    throw exceptionGenerator(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                };
                return this;
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
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

            public TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore is not null)
                {
                    nextSetup.CallBefore(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                }

                if (nextSetup.ResultGenerator is null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                if (nextSetup.CallAfter is not null)
                {
                    nextSetup.CallAfter(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal RoarDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? ResultGenerator { get; set; }
                internal RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallBefore { get; set; }
                internal RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(RoarDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator);
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value);
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new();
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex);
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(RoarExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator);
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
            public IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
        }

        internal interface IRoarMethodImposter
        {
            IRoarMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IRoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IRoarMethodImposter
        {
            TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
            bool HasMatchingSetup(RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public interface RoarMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public interface IRoarMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>, RoarMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
        }

        // TResult IGenericAnimal.Roar<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class RoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IRoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            private readonly ConcurrentStack<RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>> _invocationSetups = new ConcurrentStack<RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>>();
            private readonly RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection;
            public RoarMethodImposter(RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection)
            {
                this._roarMethodInvocationHistoryCollection = _roarMethodInvocationHistoryCollection;
            }

            IRoarMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? IRoarMethodImposter.As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                if (typeof(TOrdinaryParamTarget).IsAssignableTo(typeof(TOrdinaryParam)) && typeof(TOutParam).IsAssignableTo(typeof(TOutParamTarget)) && typeof(TInParamTarget).IsAssignableTo(typeof(TInParam)) && typeof(TRefParamTarget) == typeof(TRefParam) && typeof(TParamsParamTarget[]).IsAssignableTo(typeof(TParamsParam[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(this);
                }

                return null;
            }

            private class Adapter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> : IRoarMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>
            {
                private readonly RoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _target;
                public Adapter(RoarMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(TOrdinaryParamTarget ordinaryParam, out TOutParamTarget outParam, in TInParamTarget inParam, ref TRefParamTarget refParam, TParamsParamTarget[] paramsParam)
                {
                    TOutParam outParamAdapted;
                    TRefParam refParamAdapted = Imposter.Abstractions.TypeCaster.Cast<TRefParamTarget, TRefParam>(refParam);
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TOrdinaryParamTarget, TOrdinaryParam>(ordinaryParam), out outParamAdapted, Imposter.Abstractions.TypeCaster.Cast<TInParamTarget, TInParam>(inParam), ref refParamAdapted, Imposter.Abstractions.TypeCaster.Cast<TParamsParamTarget[], TParamsParam[]>(paramsParam));
                    outParam = Imposter.Abstractions.TypeCaster.Cast<TOutParam, TOutParamTarget>(outParamAdapted);
                    refParam = Imposter.Abstractions.TypeCaster.Cast<TRefParam, TRefParamTarget>(refParamAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(RoarArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>());
                }

                IRoarMethodImposter<TOrdinaryParamTarget1, TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>? IRoarMethodImposter.As<TOrdinaryParamTarget1, TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
            {
                outParam = default(TOutParam);
            }

            public bool HasMatchingSetup(RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? FindMatchingSetup(RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                var arguments = new RoarArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam);
                var matchingSetup = FindMatchingSetup(arguments) ?? RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                    _roarMethodInvocationHistoryCollection.Add(new RoarMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _roarMethodInvocationHistoryCollection.Add(new RoarMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IRoarMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
            {
                private readonly RoarMethodImposterCollection _imposterCollection;
                private readonly RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection;
                private readonly RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria;
                private RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? _existingInvocationSetup;
                public Builder(RoarMethodImposterCollection _imposterCollection, RoarMethodInvocationHistoryCollection _roarMethodInvocationHistoryCollection, RoarArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._roarMethodInvocationHistoryCollection = _roarMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new RoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                        _imposterCollection.AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(RoarDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(RoarExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallBefore(RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IRoarMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallAfter(RoarCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void RoarMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Called(Count count)
                {
                    var invocationCount = _roarMethodInvocationHistoryCollection.Count<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate TResult ChirpDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate void ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate System.Exception ChirpExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        public class ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public int age { get; }
            public string name { get; }
            public TInParam inParam { get; }
            public TRefParam refParam { get; }
            public TParamsParam[] paramsParam { get; }

            public ChirpArguments(int age, string name, TInParam inParam, TRefParam refParam, TParamsParam[] paramsParam)
            {
                this.age = age;
                this.name = name;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public ChirpArguments<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new ChirpArguments<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(TypeCaster.Cast<int, int>(age), TypeCaster.Cast<string, string>(name), TypeCaster.Cast<TInParam, TInParamTarget>(inParam), TypeCaster.Cast<TRefParam, TRefParamTarget>(refParam), TypeCaster.Cast<TParamsParam[], TParamsParamTarget[]>(paramsParam));
            }
        }

        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public Arg<int> age { get; }
            public Arg<string> name { get; }
            public OutArg<TOutParam> outParam { get; }
            public Arg<TInParam> inParam { get; }
            public Arg<TRefParam> refParam { get; }
            public Arg<TParamsParam[]> paramsParam { get; }

            public ChirpArgumentsCriteria(Arg<int> age, Arg<string> name, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
            {
                this.age = age;
                this.name = name;
                this.outParam = outParam;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public bool Matches(ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return age.Matches(arguments.age) && name.Matches(arguments.name) && inParam.Matches(arguments.inParam) && refParam.Matches(arguments.refParam) && paramsParam.Matches(arguments.paramsParam);
            }

            public ChirpArgumentsCriteria<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new ChirpArgumentsCriteria<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(Imposter.Abstractions.Arg<int>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<int, int>(it, out int ageTarget) && age.Matches(ageTarget)), Imposter.Abstractions.Arg<string>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<string, string>(it, out string nameTarget) && name.Matches(nameTarget)), Imposter.Abstractions.OutArg<TOutParamTarget>.Any(), Imposter.Abstractions.Arg<TInParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TInParamTarget, TInParam>(it, out TInParam inParamTarget) && inParam.Matches(inParamTarget)), Imposter.Abstractions.Arg<TRefParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TRefParamTarget, TRefParam>(it, out TRefParam refParamTarget) && refParam.Matches(refParamTarget)), Imposter.Abstractions.Arg<TParamsParamTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TParamsParamTarget[], TParamsParam[]>(it, out TParamsParam[] paramsParamTarget) && paramsParam.Matches(paramsParamTarget)));
            }
        }

        public interface IChirpMethodInvocationHistory
        {
            bool Matches<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> criteria);
        }

        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public class ChirpMethodInvocationHistory<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IChirpMethodInvocationHistory
        {
            public ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Arguments { get; }
            public TResult? Result { get; }
            public System.Exception? Exception { get; }

            public ChirpMethodInvocationHistory(ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments, TResult? result = default(TResult? ), System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(ChirpArgumentsCriteria<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> criteria)
            {
                return criteria.As<TOutParam, TInParam, TRefParam, TResult, TParamsParam>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ChirpMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IChirpMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IChirpMethodInvocationHistory>();
            internal void Add(IChirpMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(argumentsCriteria));
            }
        }

        internal class ChirpMethodImposterCollection
        {
            private readonly ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection;
            public ChirpMethodImposterCollection(ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection)
            {
                this._chirpMethodInvocationHistoryCollection = _chirpMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IChirpMethodImposter> _imposters = new ConcurrentStack<IChirpMethodImposter>();
            internal ChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> AddNew<TOutParam, TInParam, TRefParam, TResult, TParamsParam>()
            {
                var imposter = new ChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_chirpMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetImposterWithMatchingSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return _imposters.Select(it => it.As<TOutParam, TInParam, TRefParam, TResult, TParamsParam>()).Where(it => it is not null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TOutParam, TInParam, TRefParam, TResult, TParamsParam>();
            }
        }

        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            internal static ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> DefaultInvocationSetup = new ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<int>.Any(), Arg<string>.Any(), OutArg<TOutParam>.Any(), Arg<TInParam>.Any(), Arg<TRefParam>.Any(), Arg<TParamsParam[]>.Any()));
            internal ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> ArgumentsCriteria { get; }

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

            internal static TResult DefaultResultGenerator(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                InitializeOutParametersWithDefaultValues(out outParam);
                return default(TResult);
            }

            public ChirpMethodInvocationsSetup(ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
            {
                outParam = default(TOutParam);
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(ChirpDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    return value;
                };
                return this;
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw new TException();
                };
                return this;
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw ex;
                };
                return this;
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(ChirpExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    throw exceptionGenerator(age, name, out outParam, in inParam, ref refParam, paramsParam);
                };
                return this;
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
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

            public TResult Invoke(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore is not null)
                {
                    nextSetup.CallBefore(age, name, out outParam, in inParam, ref refParam, paramsParam);
                }

                if (nextSetup.ResultGenerator is null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(age, name, out outParam, in inParam, ref refParam, paramsParam);
                if (nextSetup.CallAfter is not null)
                {
                    nextSetup.CallAfter(age, name, out outParam, in inParam, ref refParam, paramsParam);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal ChirpDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? ResultGenerator { get; set; }
                internal ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallBefore { get; set; }
                internal ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(ChirpDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator);
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value);
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new();
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex);
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(ChirpExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator);
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
            public IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
        }

        internal interface IChirpMethodImposter
        {
            IChirpMethodImposter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IChirpMethodImposter
        {
            TResult Invoke(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
            bool HasMatchingSetup(ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public interface ChirpMethodInvocationVerifier<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public interface IChirpMethodImposterBuilder<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>, ChirpMethodInvocationVerifier<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
        }

        // TResult IGenericAnimal.Chirp<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            private readonly ConcurrentStack<ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>> _invocationSetups = new ConcurrentStack<ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>>();
            private readonly ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection;
            public ChirpMethodImposter(ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection)
            {
                this._chirpMethodInvocationHistoryCollection = _chirpMethodInvocationHistoryCollection;
            }

            IChirpMethodImposter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? IChirpMethodImposter.As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                if (typeof(TOutParam).IsAssignableTo(typeof(TOutParamTarget)) && typeof(TInParamTarget).IsAssignableTo(typeof(TInParam)) && typeof(TRefParamTarget) == typeof(TRefParam) && typeof(TParamsParamTarget[]).IsAssignableTo(typeof(TParamsParam[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(this);
                }

                return null;
            }

            private class Adapter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> : IChirpMethodImposter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>
            {
                private readonly ChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> _target;
                public Adapter(ChirpMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(int age, string name, out TOutParamTarget outParam, in TInParamTarget inParam, ref TRefParamTarget refParam, TParamsParamTarget[] paramsParam)
                {
                    TOutParam outParamAdapted;
                    TRefParam refParamAdapted = Imposter.Abstractions.TypeCaster.Cast<TRefParamTarget, TRefParam>(refParam);
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<int, int>(age), Imposter.Abstractions.TypeCaster.Cast<string, string>(name), out outParamAdapted, Imposter.Abstractions.TypeCaster.Cast<TInParamTarget, TInParam>(inParam), ref refParamAdapted, Imposter.Abstractions.TypeCaster.Cast<TParamsParamTarget[], TParamsParam[]>(paramsParam));
                    outParam = Imposter.Abstractions.TypeCaster.Cast<TOutParam, TOutParamTarget>(outParamAdapted);
                    refParam = Imposter.Abstractions.TypeCaster.Cast<TRefParam, TRefParamTarget>(refParamAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(ChirpArguments<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TOutParam, TInParam, TRefParam, TResult, TParamsParam>());
                }

                IChirpMethodImposter<TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>? IChirpMethodImposter.As<TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
            {
                outParam = default(TOutParam);
            }

            public bool HasMatchingSetup(ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? FindMatchingSetup(ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public TResult Invoke(int age, string name, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                var arguments = new ChirpArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(age, name, inParam, refParam, paramsParam);
                var matchingSetup = FindMatchingSetup(arguments) ?? ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age, name, out outParam, in inParam, ref refParam, paramsParam);
                    _chirpMethodInvocationHistoryCollection.Add(new ChirpMethodInvocationHistory<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _chirpMethodInvocationHistoryCollection.Add(new ChirpMethodInvocationHistory<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IChirpMethodImposterBuilder<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
            {
                private readonly ChirpMethodImposterCollection _imposterCollection;
                private readonly ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection;
                private readonly ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria;
                private ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? _existingInvocationSetup;
                public Builder(ChirpMethodImposterCollection _imposterCollection, ChirpMethodInvocationHistoryCollection _chirpMethodInvocationHistoryCollection, ChirpArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._chirpMethodInvocationHistoryCollection = _chirpMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new ChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                        _imposterCollection.AddNew<TOutParam, TInParam, TRefParam, TResult, TParamsParam>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(ChirpDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(ChirpExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallBefore(ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IChirpMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallAfter(ChirpCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void ChirpMethodInvocationVerifier<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Called(Count count)
                {
                    var invocationCount = _chirpMethodInvocationHistoryCollection.Count<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        public delegate TResult MeowDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam);
        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        public delegate void MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam);
        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        public delegate System.Exception MeowExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam);
        public class MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public int age { get; }
            public global::System.Collections.Generic.List<TInParam> inParam { get; }
            public global::System.Collections.Generic.Dictionary<int, TRefParam> refParam { get; }
            public TParamsParam[] paramsParam { get; }

            public MeowArguments(int age, global::System.Collections.Generic.List<TInParam> inParam, global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
            {
                this.age = age;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public MeowArguments<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new MeowArguments<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(TypeCaster.Cast<int, int>(age), TypeCaster.Cast<global::System.Collections.Generic.List<TInParam>, global::System.Collections.Generic.List<TInParamTarget>>(inParam), TypeCaster.Cast<global::System.Collections.Generic.Dictionary<int, TRefParam>, global::System.Collections.Generic.Dictionary<int, TRefParamTarget>>(refParam), TypeCaster.Cast<TParamsParam[], TParamsParamTarget[]>(paramsParam));
            }
        }

        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public Arg<int> age { get; }
            public OutArg<global::System.Collections.Generic.List<TOutParam>> outParam { get; }
            public Arg<global::System.Collections.Generic.List<TInParam>> inParam { get; }
            public Arg<global::System.Collections.Generic.Dictionary<int, TRefParam>> refParam { get; }
            public Arg<TParamsParam[]> paramsParam { get; }

            public MeowArgumentsCriteria(Arg<int> age, OutArg<global::System.Collections.Generic.List<TOutParam>> outParam, Arg<global::System.Collections.Generic.List<TInParam>> inParam, Arg<global::System.Collections.Generic.Dictionary<int, TRefParam>> refParam, Arg<TParamsParam[]> paramsParam)
            {
                this.age = age;
                this.outParam = outParam;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public bool Matches(MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return age.Matches(arguments.age) && inParam.Matches(arguments.inParam) && refParam.Matches(arguments.refParam) && paramsParam.Matches(arguments.paramsParam);
            }

            public MeowArgumentsCriteria<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new MeowArgumentsCriteria<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(Imposter.Abstractions.Arg<int>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<int, int>(it, out int ageTarget) && age.Matches(ageTarget)), Imposter.Abstractions.OutArg<global::System.Collections.Generic.List<TOutParamTarget>>.Any(), Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TInParamTarget>>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<global::System.Collections.Generic.List<TInParamTarget>, global::System.Collections.Generic.List<TInParam>>(it, out global::System.Collections.Generic.List<TInParam> inParamTarget) && inParam.Matches(inParamTarget)), Imposter.Abstractions.Arg<global::System.Collections.Generic.Dictionary<int, TRefParamTarget>>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<global::System.Collections.Generic.Dictionary<int, TRefParamTarget>, global::System.Collections.Generic.Dictionary<int, TRefParam>>(it, out global::System.Collections.Generic.Dictionary<int, TRefParam> refParamTarget) && refParam.Matches(refParamTarget)), Imposter.Abstractions.Arg<TParamsParamTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TParamsParamTarget[], TParamsParam[]>(it, out TParamsParam[] paramsParamTarget) && paramsParam.Matches(paramsParamTarget)));
            }
        }

        public interface IMeowMethodInvocationHistory
        {
            bool Matches<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> criteria);
        }

        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        public class MeowMethodInvocationHistory<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IMeowMethodInvocationHistory
        {
            public MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Arguments { get; }
            public TResult? Result { get; }
            public System.Exception? Exception { get; }

            public MeowMethodInvocationHistory(MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments, TResult? result = default(TResult? ), System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(MeowArgumentsCriteria<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> criteria)
            {
                return criteria.As<TOutParam, TInParam, TRefParam, TResult, TParamsParam>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MeowMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IMeowMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IMeowMethodInvocationHistory>();
            internal void Add(IMeowMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(argumentsCriteria));
            }
        }

        internal class MeowMethodImposterCollection
        {
            private readonly MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection;
            public MeowMethodImposterCollection(MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection)
            {
                this._meowMethodInvocationHistoryCollection = _meowMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IMeowMethodImposter> _imposters = new ConcurrentStack<IMeowMethodImposter>();
            internal MeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> AddNew<TOutParam, TInParam, TRefParam, TResult, TParamsParam>()
            {
                var imposter = new MeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_meowMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IMeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetImposterWithMatchingSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return _imposters.Select(it => it.As<TOutParam, TInParam, TRefParam, TResult, TParamsParam>()).Where(it => it is not null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TOutParam, TInParam, TRefParam, TResult, TParamsParam>();
            }
        }

        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            internal static MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> DefaultInvocationSetup = new MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<int>.Any(), OutArg<global::System.Collections.Generic.List<TOutParam>>.Any(), Arg<global::System.Collections.Generic.List<TInParam>>.Any(), Arg<global::System.Collections.Generic.Dictionary<int, TRefParam>>.Any(), Arg<TParamsParam[]>.Any()));
            internal MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> ArgumentsCriteria { get; }

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

            internal static TResult DefaultResultGenerator(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
            {
                InitializeOutParametersWithDefaultValues(out outParam);
                return default(TResult);
            }

            public MeowMethodInvocationsSetup(MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out global::System.Collections.Generic.List<TOutParam> outParam)
            {
                outParam = default(global::System.Collections.Generic.List<TOutParam>);
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(MeowDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    return value;
                };
                return this;
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw new TException();
                };
                return this;
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw ex;
                };
                return this;
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(MeowExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam) =>
                {
                    throw exceptionGenerator(age, out outParam, in inParam, ref refParam, paramsParam);
                };
                return this;
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
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

            public TResult Invoke(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore is not null)
                {
                    nextSetup.CallBefore(age, out outParam, in inParam, ref refParam, paramsParam);
                }

                if (nextSetup.ResultGenerator is null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(age, out outParam, in inParam, ref refParam, paramsParam);
                if (nextSetup.CallAfter is not null)
                {
                    nextSetup.CallAfter(age, out outParam, in inParam, ref refParam, paramsParam);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal MeowDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? ResultGenerator { get; set; }
                internal MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallBefore { get; set; }
                internal MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(MeowDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator);
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value);
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new();
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex);
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(MeowExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator);
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
            public IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
        }

        internal interface IMeowMethodImposter
        {
            IMeowMethodImposter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IMeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IMeowMethodImposter
        {
            TResult Invoke(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam);
            bool HasMatchingSetup(MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        public interface MeowMethodInvocationVerifier<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        public interface IMeowMethodImposterBuilder<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>, MeowMethodInvocationVerifier<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
        }

        // TResult IGenericAnimal.Meow<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(int age, out List<TOutParam> outParam, in List<TInParam> inParam, ref Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IMeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            private readonly ConcurrentStack<MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>> _invocationSetups = new ConcurrentStack<MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>>();
            private readonly MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection;
            public MeowMethodImposter(MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection)
            {
                this._meowMethodInvocationHistoryCollection = _meowMethodInvocationHistoryCollection;
            }

            IMeowMethodImposter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? IMeowMethodImposter.As<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                if (typeof(global::System.Collections.Generic.List<TOutParam>).IsAssignableTo(typeof(global::System.Collections.Generic.List<TOutParamTarget>)) && typeof(global::System.Collections.Generic.List<TInParamTarget>).IsAssignableTo(typeof(global::System.Collections.Generic.List<TInParam>)) && typeof(global::System.Collections.Generic.Dictionary<int, TRefParamTarget>) == typeof(global::System.Collections.Generic.Dictionary<int, TRefParam>) && typeof(TParamsParamTarget[]).IsAssignableTo(typeof(TParamsParam[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(this);
                }

                return null;
            }

            private class Adapter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> : IMeowMethodImposter<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>
            {
                private readonly MeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> _target;
                public Adapter(MeowMethodImposter<TOutParam, TInParam, TRefParam, TResult, TParamsParam> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(int age, out List<TOutParamTarget> outParam, in List<TInParamTarget> inParam, ref Dictionary<int, TRefParamTarget> refParam, TParamsParamTarget[] paramsParam)
                {
                    global::System.Collections.Generic.List<TOutParam> outParamAdapted;
                    global::System.Collections.Generic.Dictionary<int, TRefParam> refParamAdapted = Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.Dictionary<int, TRefParamTarget>, global::System.Collections.Generic.Dictionary<int, TRefParam>>(refParam);
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<int, int>(age), out outParamAdapted, Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TInParamTarget>, global::System.Collections.Generic.List<TInParam>>(inParam), ref refParamAdapted, Imposter.Abstractions.TypeCaster.Cast<TParamsParamTarget[], TParamsParam[]>(paramsParam));
                    outParam = Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TOutParam>, global::System.Collections.Generic.List<TOutParamTarget>>(outParamAdapted);
                    refParam = Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.Dictionary<int, TRefParam>, global::System.Collections.Generic.Dictionary<int, TRefParamTarget>>(refParamAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(MeowArguments<TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TOutParam, TInParam, TRefParam, TResult, TParamsParam>());
                }

                IMeowMethodImposter<TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>? IMeowMethodImposter.As<TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out global::System.Collections.Generic.List<TOutParam> outParam)
            {
                outParam = default(global::System.Collections.Generic.List<TOutParam>);
            }

            public bool HasMatchingSetup(MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? FindMatchingSetup(MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public TResult Invoke(int age, out global::System.Collections.Generic.List<TOutParam> outParam, in global::System.Collections.Generic.List<TInParam> inParam, ref global::System.Collections.Generic.Dictionary<int, TRefParam> refParam, TParamsParam[] paramsParam)
            {
                var arguments = new MeowArguments<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(age, inParam, refParam, paramsParam);
                var matchingSetup = FindMatchingSetup(arguments) ?? MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age, out outParam, in inParam, ref refParam, paramsParam);
                    _meowMethodInvocationHistoryCollection.Add(new MeowMethodInvocationHistory<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _meowMethodInvocationHistoryCollection.Add(new MeowMethodInvocationHistory<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMeowMethodImposterBuilder<TOutParam, TInParam, TRefParam, TResult, TParamsParam>
            {
                private readonly MeowMethodImposterCollection _imposterCollection;
                private readonly MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection;
                private readonly MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria;
                private MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>? _existingInvocationSetup;
                public Builder(MeowMethodImposterCollection _imposterCollection, MeowMethodInvocationHistoryCollection _meowMethodInvocationHistoryCollection, MeowArgumentsCriteria<TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._meowMethodInvocationHistoryCollection = _meowMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new MeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                        _imposterCollection.AddNew<TOutParam, TInParam, TRefParam, TResult, TParamsParam>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(MeowDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(MeowExceptionGeneratorDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallBefore(MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam> IMeowMethodInvocationsSetup<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallAfter(MeowCallbackDelegate<TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void MeowMethodInvocationVerifier<TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Called(Count count)
                {
                    var invocationCount = _meowMethodInvocationHistoryCollection.Count<TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // void IGenericAnimal.Bark(string name)
        public delegate void BarkDelegate(string name);
        // void IGenericAnimal.Bark(string name)
        public delegate void BarkCallbackDelegate(string name);
        // void IGenericAnimal.Bark(string name)
        public delegate System.Exception BarkExceptionGeneratorDelegate(string name);
        public class BarkArguments
        {
            public string name { get; }

            public BarkArguments(string name)
            {
                this.name = name;
            }
        }

        // void IGenericAnimal.Bark(string name)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class BarkArgumentsCriteria
        {
            public Arg<string> name { get; }

            public BarkArgumentsCriteria(Arg<string> name)
            {
                this.name = name;
            }

            public bool Matches(BarkArguments arguments)
            {
                return name.Matches(arguments.name);
            }
        }

        public interface IBarkMethodInvocationHistory
        {
            bool Matches(BarkArgumentsCriteria criteria);
        }

        // void IGenericAnimal.Bark(string name)
        public class BarkMethodInvocationHistory : IBarkMethodInvocationHistory
        {
            public BarkArguments Arguments { get; }
            public System.Exception? Exception { get; }

            public BarkMethodInvocationHistory(BarkArguments arguments, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Exception = exception;
            }

            public bool Matches(BarkArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BarkMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IBarkMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IBarkMethodInvocationHistory>();
            internal void Add(IBarkMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(BarkArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IGenericAnimal.Bark(string name)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class BarkMethodInvocationsSetup : IBarkMethodInvocationsSetup
        {
            internal static BarkMethodInvocationsSetup DefaultInvocationSetup = new BarkMethodInvocationsSetup(new BarkArgumentsCriteria(Arg<string>.Any()));
            internal BarkArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(string name)
            {
            }

            public BarkMethodInvocationsSetup(BarkArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IBarkMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string name) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IBarkMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string name) =>
                {
                    throw ex;
                };
                return this;
            }

            public IBarkMethodInvocationsSetup Throws(BarkExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string name) =>
                {
                    throw exceptionGenerator(name);
                };
                return this;
            }

            public IBarkMethodInvocationsSetup CallBefore(BarkCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IBarkMethodInvocationsSetup CallAfter(BarkCallbackDelegate callback)
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

            public void Invoke(string name)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore is not null)
                {
                    nextSetup.CallBefore(name);
                }

                if (nextSetup.ResultGenerator is null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                nextSetup.ResultGenerator.Invoke(name);
                if (nextSetup.CallAfter is not null)
                {
                    nextSetup.CallAfter(name);
                };
            }

            internal class MethodInvocationSetup
            {
                internal BarkDelegate? ResultGenerator { get; set; }
                internal BarkCallbackDelegate? CallBefore { get; set; }
                internal BarkCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IBarkMethodInvocationsSetup
        {
            public IBarkMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IBarkMethodInvocationsSetup Throws(System.Exception ex);
            public IBarkMethodInvocationsSetup Throws(BarkExceptionGeneratorDelegate exceptionGenerator);
            public IBarkMethodInvocationsSetup CallBefore(BarkCallbackDelegate callback);
            public IBarkMethodInvocationsSetup CallAfter(BarkCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IGenericAnimal.Bark(string name)
        public interface BarkMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IGenericAnimal.Bark(string name)
        public interface IBarkMethodImposterBuilder : IBarkMethodInvocationsSetup, BarkMethodInvocationVerifier
        {
        }

        // void IGenericAnimal.Bark(string name)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BarkMethodImposter
        {
            private readonly ConcurrentStack<BarkMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<BarkMethodInvocationsSetup>();
            private readonly BarkMethodInvocationHistoryCollection _barkMethodInvocationHistoryCollection;
            public BarkMethodImposter(BarkMethodInvocationHistoryCollection _barkMethodInvocationHistoryCollection)
            {
                this._barkMethodInvocationHistoryCollection = _barkMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(BarkArguments arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private BarkMethodInvocationsSetup? FindMatchingSetup(BarkArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(string name)
            {
                var arguments = new BarkArguments(name);
                var matchingSetup = FindMatchingSetup(arguments) ?? BarkMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(name);
                    _barkMethodInvocationHistoryCollection.Add(new BarkMethodInvocationHistory(arguments, default));
                }
                catch (Exception ex)
                {
                    _barkMethodInvocationHistoryCollection.Add(new BarkMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IBarkMethodImposterBuilder
            {
                private readonly BarkMethodImposter _imposter;
                private readonly BarkMethodInvocationHistoryCollection _barkMethodInvocationHistoryCollection;
                private readonly BarkArgumentsCriteria _argumentsCriteria;
                private BarkMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(BarkMethodImposter _imposter, BarkMethodInvocationHistoryCollection _barkMethodInvocationHistoryCollection, BarkArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._barkMethodInvocationHistoryCollection = _barkMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private BarkMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new BarkMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IBarkMethodInvocationsSetup IBarkMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IBarkMethodInvocationsSetup IBarkMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IBarkMethodInvocationsSetup IBarkMethodInvocationsSetup.Throws(BarkExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IBarkMethodInvocationsSetup IBarkMethodInvocationsSetup.CallBefore(BarkCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IBarkMethodInvocationsSetup IBarkMethodInvocationsSetup.CallAfter(BarkCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void BarkMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _barkMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IGenericAnimal.Sum(int left, int right)
        public delegate int SumDelegate(int left, int right);
        // int IGenericAnimal.Sum(int left, int right)
        public delegate void SumCallbackDelegate(int left, int right);
        // int IGenericAnimal.Sum(int left, int right)
        public delegate System.Exception SumExceptionGeneratorDelegate(int left, int right);
        public class SumArguments
        {
            public int left { get; }
            public int right { get; }

            public SumArguments(int left, int right)
            {
                this.left = left;
                this.right = right;
            }
        }

        // int IGenericAnimal.Sum(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SumArgumentsCriteria
        {
            public Arg<int> left { get; }
            public Arg<int> right { get; }

            public SumArgumentsCriteria(Arg<int> left, Arg<int> right)
            {
                this.left = left;
                this.right = right;
            }

            public bool Matches(SumArguments arguments)
            {
                return left.Matches(arguments.left) && right.Matches(arguments.right);
            }
        }

        public interface ISumMethodInvocationHistory
        {
            bool Matches(SumArgumentsCriteria criteria);
        }

        // int IGenericAnimal.Sum(int left, int right)
        public class SumMethodInvocationHistory : ISumMethodInvocationHistory
        {
            public SumArguments Arguments { get; }
            public int? Result { get; }
            public System.Exception? Exception { get; }

            public SumMethodInvocationHistory(SumArguments arguments, int? result = default(int? ), System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(SumArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SumMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<ISumMethodInvocationHistory> _invocationHistory = new ConcurrentStack<ISumMethodInvocationHistory>();
            internal void Add(ISumMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(SumArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IGenericAnimal.Sum(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class SumMethodInvocationsSetup : ISumMethodInvocationsSetup
        {
            internal static SumMethodInvocationsSetup DefaultInvocationSetup = new SumMethodInvocationsSetup(new SumArgumentsCriteria(Arg<int>.Any(), Arg<int>.Any()));
            internal SumArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(int left, int right)
            {
                return default(int);
            }

            public SumMethodInvocationsSetup(SumArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public ISumMethodInvocationsSetup Returns(SumDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public ISumMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int left, int right) =>
                {
                    return value;
                };
                return this;
            }

            public ISumMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int left, int right) =>
                {
                    throw new TException();
                };
                return this;
            }

            public ISumMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int left, int right) =>
                {
                    throw ex;
                };
                return this;
            }

            public ISumMethodInvocationsSetup Throws(SumExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int left, int right) =>
                {
                    throw exceptionGenerator(left, right);
                };
                return this;
            }

            public ISumMethodInvocationsSetup CallBefore(SumCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public ISumMethodInvocationsSetup CallAfter(SumCallbackDelegate callback)
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

            public int Invoke(int left, int right)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore is not null)
                {
                    nextSetup.CallBefore(left, right);
                }

                if (nextSetup.ResultGenerator is null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(left, right);
                if (nextSetup.CallAfter is not null)
                {
                    nextSetup.CallAfter(left, right);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal SumDelegate? ResultGenerator { get; set; }
                internal SumCallbackDelegate? CallBefore { get; set; }
                internal SumCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISumMethodInvocationsSetup
        {
            public ISumMethodInvocationsSetup Returns(SumDelegate resultGenerator);
            public ISumMethodInvocationsSetup Returns(int value);
            public ISumMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public ISumMethodInvocationsSetup Throws(System.Exception ex);
            public ISumMethodInvocationsSetup Throws(SumExceptionGeneratorDelegate exceptionGenerator);
            public ISumMethodInvocationsSetup CallBefore(SumCallbackDelegate callback);
            public ISumMethodInvocationsSetup CallAfter(SumCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IGenericAnimal.Sum(int left, int right)
        public interface SumMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IGenericAnimal.Sum(int left, int right)
        public interface ISumMethodImposterBuilder : ISumMethodInvocationsSetup, SumMethodInvocationVerifier
        {
        }

        // int IGenericAnimal.Sum(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SumMethodImposter
        {
            private readonly ConcurrentStack<SumMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<SumMethodInvocationsSetup>();
            private readonly SumMethodInvocationHistoryCollection _sumMethodInvocationHistoryCollection;
            public SumMethodImposter(SumMethodInvocationHistoryCollection _sumMethodInvocationHistoryCollection)
            {
                this._sumMethodInvocationHistoryCollection = _sumMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(SumArguments arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private SumMethodInvocationsSetup? FindMatchingSetup(SumArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int left, int right)
            {
                var arguments = new SumArguments(left, right);
                var matchingSetup = FindMatchingSetup(arguments) ?? SumMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(left, right);
                    _sumMethodInvocationHistoryCollection.Add(new SumMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _sumMethodInvocationHistoryCollection.Add(new SumMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ISumMethodImposterBuilder
            {
                private readonly SumMethodImposter _imposter;
                private readonly SumMethodInvocationHistoryCollection _sumMethodInvocationHistoryCollection;
                private readonly SumArgumentsCriteria _argumentsCriteria;
                private SumMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(SumMethodImposter _imposter, SumMethodInvocationHistoryCollection _sumMethodInvocationHistoryCollection, SumArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._sumMethodInvocationHistoryCollection = _sumMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private SumMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new SumMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.Returns(SumDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.Throws(SumExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.CallBefore(SumCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ISumMethodInvocationsSetup ISumMethodInvocationsSetup.CallAfter(SumCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void SumMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _sumMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }
    }
}