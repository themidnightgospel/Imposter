using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Setup.Methods.Generics.Poc;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Setup.Methods.Generics.Poc
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ISutWithGenericMethodImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.Poc.ISutWithGenericMethod>
    {
        private PrintMethodImposterCollection _printMethodImposterCollection;
        private PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;
        public IPrintMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<TOrdinaryParam> ordinaryParam, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
        {
            return new PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Builder(_printMethodImposterCollection, _printMethodInvocationHistoryCollection, new PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ordinaryParam, outParam, inParam, refParam, paramsParam));
        }

        private ImposterTargetInstance _imposterInstance;
        public ISutWithGenericMethodImposter()
        {
            this._printMethodImposterCollection = new PrintMethodImposterCollection(_printMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.Poc.ISutWithGenericMethod
        {
            ISutWithGenericMethodImposter _imposter;
            public ImposterTargetInstance(ISutWithGenericMethodImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public TResult Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
            {
                return _imposter._printMethodImposterCollection.GetImposterWithMatchingSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam)).Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
            }
        }

        global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.Poc.ISutWithGenericMethod Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Setup.Methods.Generics.Poc.ISutWithGenericMethod>.Instance()
        {
            return _imposterInstance;
        }

        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate TResult PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate void PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public delegate System.Exception PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
        public class PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public TOrdinaryParam ordinaryParam { get; }
            public TInParam inParam { get; }
            public TRefParam refParam { get; }
            public TParamsParam[] paramsParam { get; }

            public PrintArguments(TOrdinaryParam ordinaryParam, TInParam inParam, TRefParam refParam, TParamsParam[] paramsParam)
            {
                this.ordinaryParam = ordinaryParam;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public PrintArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new PrintArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(TypeCaster.Cast<TOrdinaryParam, TOrdinaryParamTarget>(ordinaryParam), TypeCaster.Cast<TInParam, TInParamTarget>(inParam), TypeCaster.Cast<TRefParam, TRefParamTarget>(refParam), TypeCaster.Cast<TParamsParam[], TParamsParamTarget[]>(paramsParam));
            }
        }

        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public Arg<TOrdinaryParam> ordinaryParam { get; }
            public OutArg<TOutParam> outParam { get; }
            public Arg<TInParam> inParam { get; }
            public Arg<TRefParam> refParam { get; }
            public Arg<TParamsParam[]> paramsParam { get; }

            public PrintArgumentsCriteria(Arg<TOrdinaryParam> ordinaryParam, OutArg<TOutParam> outParam, Arg<TInParam> inParam, Arg<TRefParam> refParam, Arg<TParamsParam[]> paramsParam)
            {
                this.ordinaryParam = ordinaryParam;
                this.outParam = outParam;
                this.inParam = inParam;
                this.refParam = refParam;
                this.paramsParam = paramsParam;
            }

            public bool Matches(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return ordinaryParam.Matches(arguments.ordinaryParam) && inParam.Matches(arguments.inParam) && refParam.Matches(arguments.refParam) && paramsParam.Matches(arguments.paramsParam);
            }

            public PrintArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                return new PrintArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(Imposter.Abstractions.Arg<TOrdinaryParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TOrdinaryParamTarget, TOrdinaryParam>(it, out TOrdinaryParam ordinaryParamTarget) && ordinaryParam.Matches(ordinaryParamTarget)), Imposter.Abstractions.OutArg<TOutParamTarget>.Any, Imposter.Abstractions.Arg<TInParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TInParamTarget, TInParam>(it, out TInParam inParamTarget) && inParam.Matches(inParamTarget)), Imposter.Abstractions.Arg<TRefParamTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TRefParamTarget, TRefParam>(it, out TRefParam refParamTarget) && refParam.Matches(refParamTarget)), Imposter.Abstractions.Arg<TParamsParamTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TParamsParamTarget[], TParamsParam[]>(it, out TParamsParam[] paramsParamTarget) && paramsParam.Matches(paramsParamTarget)));
            }
        }

        public interface IPrintMethodInvocationHistory
        {
            bool Matches<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> criteria);
        }

        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public class PrintMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodInvocationHistory
        {
            public PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Arguments { get; }
            public TResult? Result { get; }
            public System.Exception? Exception { get; }

            public PrintMethodInvocationHistory(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments, TResult? result = default(TResult? ), System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(PrintArgumentsCriteria<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> criteria)
            {
                return criteria.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class PrintMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IPrintMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IPrintMethodInvocationHistory>();
            internal void Add(IPrintMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        internal class PrintMethodImposterCollection
        {
            private readonly PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;
            public PrintMethodImposterCollection(PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection)
            {
                this._printMethodInvocationHistoryCollection = _printMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IPrintMethodImposter> _imposters = new ConcurrentStack<IPrintMethodImposter>();
            internal PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()
            {
                var imposter = new PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_printMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IPrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetImposterWithMatchingSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return _imposters.Select(it => it.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()).Where(it => it is not null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>();
            }
        }

        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            internal static PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> DefaultInvocationSetup = new PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(new PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(Arg<TOrdinaryParam>.Any, OutArg<TOutParam>.Any, Arg<TInParam>.Any, Arg<TRefParam>.Any, Arg<TParamsParam[]>.Any));
            internal PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> ArgumentsCriteria { get; }

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

            public PrintMethodInvocationsSetup(PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
            {
                outParam = default(TOutParam);
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    return value;
                };
                return this;
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw new TException();
                };
                return this;
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    InitializeOutParametersWithDefaultValues(out outParam);
                    throw ex;
                };
                return this;
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam) =>
                {
                    throw exceptionGenerator(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                };
                return this;
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
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
                internal PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? ResultGenerator { get; set; }
                internal PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallBefore { get; set; }
                internal PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator);
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Returns(TResult value);
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws<TException>()
                where TException : Exception, new();
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(System.Exception ex);
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> Throws(PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator);
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallBefore(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
            public IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> CallAfter(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback);
        }

        internal interface IPrintMethodImposter
        {
            IPrintMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IPrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodImposter
        {
            TResult Invoke(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam);
            bool HasMatchingSetup(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public interface PrintMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        public interface IPrintMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>, PrintMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
        }

        // TResult ISutWithGenericMethod.Print<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(TOrdinaryParam ordinaryParam, out TOutParam outParam, in TInParam inParam, ref TRefParam refParam, TParamsParam[] paramsParam)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> : IPrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
        {
            private readonly ConcurrentStack<PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>> _invocationSetups = new ConcurrentStack<PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>>();
            private readonly PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection;
            public PrintMethodImposter(PrintMethodInvocationHistoryCollection _printMethodInvocationHistoryCollection)
            {
                this._printMethodInvocationHistoryCollection = _printMethodInvocationHistoryCollection;
            }

            IPrintMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>? IPrintMethodImposter.As<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>()
            {
                if (typeof(TOrdinaryParamTarget).IsAssignableTo(typeof(TOrdinaryParam)) && typeof(TOutParam).IsAssignableTo(typeof(TOutParamTarget)) && typeof(TInParamTarget).IsAssignableTo(typeof(TInParam)) && typeof(TRefParamTarget) == typeof(TRefParam) && typeof(TParamsParamTarget[]).IsAssignableTo(typeof(TParamsParam[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>(this);
                }

                return null;
            }

            private class Adapter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> : IPrintMethodImposter<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget>
            {
                private readonly PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _target;
                public Adapter(PrintMethodImposter<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> target)
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

                public bool HasMatchingSetup(PrintArguments<TOrdinaryParamTarget, TOutParamTarget, TInParamTarget, TRefParamTarget, TResultTarget, TParamsParamTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>());
                }

                IPrintMethodImposter<TOrdinaryParamTarget1, TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>? IPrintMethodImposter.As<TOrdinaryParamTarget1, TOutParamTarget1, TInParamTarget1, TRefParamTarget1, TResultTarget1, TParamsParamTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out TOutParam outParam)
            {
                outParam = default(TOutParam);
            }

            public bool HasMatchingSetup(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
            {
                return FindMatchingSetup(arguments)is not null;
            }

            private PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? FindMatchingSetup(PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> arguments)
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
                var arguments = new PrintArguments<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(ordinaryParam, inParam, refParam, paramsParam);
                var matchingSetup = FindMatchingSetup(arguments) ?? PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ordinaryParam, out outParam, in inParam, ref refParam, paramsParam);
                    _printMethodInvocationHistoryCollection.Add(new PrintMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _printMethodInvocationHistoryCollection.Add(new PrintMethodInvocationHistory<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IPrintMethodImposterBuilder<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>
            {
                private readonly PrintMethodImposterCollection _imposterCollection;
                private readonly PrintMethodInvocationHistoryCollection _invocationHistoryCollection;
                private readonly PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria;
                private PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>? _existingInvocationSetup;
                public Builder(PrintMethodImposterCollection _imposterCollection, PrintMethodInvocationHistoryCollection _invocationHistoryCollection, PrintArgumentsCriteria<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._invocationHistoryCollection = _invocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new PrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>(_argumentsCriteria);
                        _imposterCollection.AddNew<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(PrintDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Throws(PrintExceptionGeneratorDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallBefore(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> IPrintMethodInvocationsSetup<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.CallAfter(PrintCallbackDelegate<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void PrintMethodInvocationVerifier<TOrdinaryParam, TOutParam, TInParam, TRefParam, TResult, TParamsParam>.Called(Count count)
                {
                    var invocationCount = _invocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }
    }
}