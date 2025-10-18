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
    public class IMethodSetupFeatureSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut>
    {
        private readonly Void_NoParamsMethodImposter _void_NoParamsMethodImposter;
        private readonly Int_NoParamsMethodImposter _int_NoParamsMethodImposter;
        private readonly Int_SingleParamMethodImposter _int_SingleParamMethodImposter;
        private readonly Int_ParamsMethodImposter _int_ParamsMethodImposter;
        private readonly Int_OutParamMethodImposter _int_OutParamMethodImposter;
        private readonly Int_RefParamMethodImposter _int_RefParamMethodImposter;
        private readonly Int_ParamsParamMethodImposter _int_ParamsParamMethodImposter;
        private readonly Int_InParamMethodImposter _int_InParamMethodImposter;
        private readonly Int_AllRefKindsMethodImposter _int_AllRefKindsMethodImposter;
        private readonly Generic_SingleParamMethodImposterCollection _generic_SingleParamMethodImposterCollection;
        private readonly Generic_OutParamMethodImposterCollection _generic_OutParamMethodImposterCollection;
        private readonly Generic_RefParamMethodImposterCollection _generic_RefParamMethodImposterCollection;
        private readonly Generic_ParamsParamMethodImposterCollection _generic_ParamsParamMethodImposterCollection;
        private readonly Generic_AllRefKindMethodImposterCollection _generic_AllRefKindMethodImposterCollection;
        private readonly Void_NoParamsMethodInvocationHistoryCollection _void_NoParamsMethodInvocationHistoryCollection = new Void_NoParamsMethodInvocationHistoryCollection();
        private readonly Int_NoParamsMethodInvocationHistoryCollection _int_NoParamsMethodInvocationHistoryCollection = new Int_NoParamsMethodInvocationHistoryCollection();
        private readonly Int_SingleParamMethodInvocationHistoryCollection _int_SingleParamMethodInvocationHistoryCollection = new Int_SingleParamMethodInvocationHistoryCollection();
        private readonly Int_ParamsMethodInvocationHistoryCollection _int_ParamsMethodInvocationHistoryCollection = new Int_ParamsMethodInvocationHistoryCollection();
        private readonly Int_OutParamMethodInvocationHistoryCollection _int_OutParamMethodInvocationHistoryCollection = new Int_OutParamMethodInvocationHistoryCollection();
        private readonly Int_RefParamMethodInvocationHistoryCollection _int_RefParamMethodInvocationHistoryCollection = new Int_RefParamMethodInvocationHistoryCollection();
        private readonly Int_ParamsParamMethodInvocationHistoryCollection _int_ParamsParamMethodInvocationHistoryCollection = new Int_ParamsParamMethodInvocationHistoryCollection();
        private readonly Int_InParamMethodInvocationHistoryCollection _int_InParamMethodInvocationHistoryCollection = new Int_InParamMethodInvocationHistoryCollection();
        private readonly Int_AllRefKindsMethodInvocationHistoryCollection _int_AllRefKindsMethodInvocationHistoryCollection = new Int_AllRefKindsMethodInvocationHistoryCollection();
        private readonly Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection = new Generic_SingleParamMethodInvocationHistoryCollection();
        private readonly Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection = new Generic_OutParamMethodInvocationHistoryCollection();
        private readonly Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection = new Generic_RefParamMethodInvocationHistoryCollection();
        private readonly Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection = new Generic_ParamsParamMethodInvocationHistoryCollection();
        private readonly Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection = new Generic_AllRefKindMethodInvocationHistoryCollection();
        public IVoid_NoParamsMethodImposterBuilder Void_NoParams()
        {
            return new Void_NoParamsMethodImposter.Builder(_void_NoParamsMethodImposter, _void_NoParamsMethodInvocationHistoryCollection);
        }

        public IInt_NoParamsMethodImposterBuilder Int_NoParams()
        {
            return new Int_NoParamsMethodImposter.Builder(_int_NoParamsMethodImposter, _int_NoParamsMethodInvocationHistoryCollection);
        }

        public IInt_SingleParamMethodImposterBuilder Int_SingleParam(Arg<int> age)
        {
            return new Int_SingleParamMethodImposter.Builder(_int_SingleParamMethodImposter, _int_SingleParamMethodInvocationHistoryCollection, new Int_SingleParamArgumentsCriteria(age));
        }

        public IInt_ParamsMethodImposterBuilder Int_Params(Arg<int> age, Arg<string> name, Arg<global::System.Text.RegularExpressions.Regex> regex)
        {
            return new Int_ParamsMethodImposter.Builder(_int_ParamsMethodImposter, _int_ParamsMethodInvocationHistoryCollection, new Int_ParamsArgumentsCriteria(age, name, regex));
        }

        public IInt_OutParamMethodImposterBuilder Int_OutParam(OutArg<int> outValue)
        {
            return new Int_OutParamMethodImposter.Builder(_int_OutParamMethodImposter, _int_OutParamMethodInvocationHistoryCollection);
        }

        public IInt_RefParamMethodImposterBuilder Int_RefParam(Arg<int> refValue)
        {
            return new Int_RefParamMethodImposter.Builder(_int_RefParamMethodImposter, _int_RefParamMethodInvocationHistoryCollection, new Int_RefParamArgumentsCriteria(refValue));
        }

        public IInt_ParamsParamMethodImposterBuilder Int_ParamsParam(Arg<string[]> paramsStrings)
        {
            return new Int_ParamsParamMethodImposter.Builder(_int_ParamsParamMethodImposter, _int_ParamsParamMethodInvocationHistoryCollection, new Int_ParamsParamArgumentsCriteria(paramsStrings));
        }

        public IInt_InParamMethodImposterBuilder Int_InParam(Arg<string> inStringValue)
        {
            return new Int_InParamMethodImposter.Builder(_int_InParamMethodImposter, _int_InParamMethodInvocationHistoryCollection, new Int_InParamArgumentsCriteria(inStringValue));
        }

        public IInt_AllRefKindsMethodImposterBuilder Int_AllRefKinds(OutArg<int> value, Arg<int> refValue, Arg<int> inValue, Arg<string> valueAsString, Arg<string[]> paramsStrings)
        {
            return new Int_AllRefKindsMethodImposter.Builder(_int_AllRefKindsMethodImposter, _int_AllRefKindsMethodInvocationHistoryCollection, new Int_AllRefKindsArgumentsCriteria(value, refValue, inValue, valueAsString, paramsStrings));
        }

        public IGeneric_SingleParamMethodImposterBuilder<TValue> Generic_SingleParam<TValue>(Arg<TValue> value)
        {
            return new Generic_SingleParamMethodImposter<TValue>.Builder(_generic_SingleParamMethodImposterCollection, _generic_SingleParamMethodInvocationHistoryCollection, new Generic_SingleParamArgumentsCriteria<TValue>(value));
        }

        public IGeneric_OutParamMethodImposterBuilder<TValue, TResult> Generic_OutParam<TValue, TResult>(OutArg<TValue> value)
        {
            return new Generic_OutParamMethodImposter<TValue, TResult>.Builder(_generic_OutParamMethodImposterCollection, _generic_OutParamMethodInvocationHistoryCollection);
        }

        public IGeneric_RefParamMethodImposterBuilder<TValue, TResult> Generic_RefParam<TValue, TResult>(Arg<TValue> value)
        {
            return new Generic_RefParamMethodImposter<TValue, TResult>.Builder(_generic_RefParamMethodImposterCollection, _generic_RefParamMethodInvocationHistoryCollection, new Generic_RefParamArgumentsCriteria<TValue, TResult>(value));
        }

        public IGeneric_ParamsParamMethodImposterBuilder<TValue, TResult> Generic_ParamsParam<TValue, TResult>(Arg<TValue[]> value)
        {
            return new Generic_ParamsParamMethodImposter<TValue, TResult>.Builder(_generic_ParamsParamMethodImposterCollection, _generic_ParamsParamMethodInvocationHistoryCollection, new Generic_ParamsParamArgumentsCriteria<TValue, TResult>(value));
        }

        public IGeneric_AllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(OutArg<TOut> outValue, Arg<TRef> refValue, Arg<TIn> inValue, Arg<TParams[]> paramsValues)
        {
            return new Generic_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>.Builder(_generic_AllRefKindMethodImposterCollection, _generic_AllRefKindMethodInvocationHistoryCollection, new Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(outValue, refValue, inValue, paramsValues));
        }

        private ImposterTargetInstance _imposterInstance;
        public IMethodSetupFeatureSutImposter()
        {
            this._void_NoParamsMethodImposter = new Void_NoParamsMethodImposter(_void_NoParamsMethodInvocationHistoryCollection);
            this._int_NoParamsMethodImposter = new Int_NoParamsMethodImposter(_int_NoParamsMethodInvocationHistoryCollection);
            this._int_SingleParamMethodImposter = new Int_SingleParamMethodImposter(_int_SingleParamMethodInvocationHistoryCollection);
            this._int_ParamsMethodImposter = new Int_ParamsMethodImposter(_int_ParamsMethodInvocationHistoryCollection);
            this._int_OutParamMethodImposter = new Int_OutParamMethodImposter(_int_OutParamMethodInvocationHistoryCollection);
            this._int_RefParamMethodImposter = new Int_RefParamMethodImposter(_int_RefParamMethodInvocationHistoryCollection);
            this._int_ParamsParamMethodImposter = new Int_ParamsParamMethodImposter(_int_ParamsParamMethodInvocationHistoryCollection);
            this._int_InParamMethodImposter = new Int_InParamMethodImposter(_int_InParamMethodInvocationHistoryCollection);
            this._int_AllRefKindsMethodImposter = new Int_AllRefKindsMethodImposter(_int_AllRefKindsMethodInvocationHistoryCollection);
            this._generic_SingleParamMethodImposterCollection = new Generic_SingleParamMethodImposterCollection(_generic_SingleParamMethodInvocationHistoryCollection);
            this._generic_OutParamMethodImposterCollection = new Generic_OutParamMethodImposterCollection(_generic_OutParamMethodInvocationHistoryCollection);
            this._generic_RefParamMethodImposterCollection = new Generic_RefParamMethodImposterCollection(_generic_RefParamMethodInvocationHistoryCollection);
            this._generic_ParamsParamMethodImposterCollection = new Generic_ParamsParamMethodImposterCollection(_generic_ParamsParamMethodInvocationHistoryCollection);
            this._generic_AllRefKindMethodImposterCollection = new Generic_AllRefKindMethodImposterCollection(_generic_AllRefKindMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut
        {
            IMethodSetupFeatureSutImposter _imposter;
            public ImposterTargetInstance(IMethodSetupFeatureSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void Void_NoParams()
            {
                _imposter._void_NoParamsMethodImposter.Invoke();
            }

            public int Int_NoParams()
            {
                return _imposter._int_NoParamsMethodImposter.Invoke();
            }

            public int Int_SingleParam(int age)
            {
                return _imposter._int_SingleParamMethodImposter.Invoke(age);
            }

            public int Int_Params(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                return _imposter._int_ParamsMethodImposter.Invoke(age, name, regex);
            }

            public int Int_OutParam(out int outValue)
            {
                return _imposter._int_OutParamMethodImposter.Invoke(out outValue);
            }

            public int Int_RefParam(ref int refValue)
            {
                return _imposter._int_RefParamMethodImposter.Invoke(ref refValue);
            }

            public int Int_ParamsParam(string[] paramsStrings)
            {
                return _imposter._int_ParamsParamMethodImposter.Invoke(paramsStrings);
            }

            public int Int_InParam(in string inStringValue)
            {
                return _imposter._int_InParamMethodImposter.Invoke(in inStringValue);
            }

            public int Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings)
            {
                return _imposter._int_AllRefKindsMethodImposter.Invoke(out value, ref refValue, in inValue, valueAsString, paramsStrings);
            }

            public void Generic_SingleParam<TValue>(TValue value)
            {
                _imposter._generic_SingleParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue>(new Generic_SingleParamArguments<TValue>(value)).Invoke(value);
            }

            public TResult Generic_OutParam<TValue, TResult>(out TValue value)
            {
                return _imposter._generic_OutParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>().Invoke(out value);
            }

            public TResult Generic_RefParam<TValue, TResult>(ref TValue value)
            {
                return _imposter._generic_RefParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>(new Generic_RefParamArguments<TValue, TResult>(value)).Invoke(ref value);
            }

            public TResult Generic_ParamsParam<TValue, TResult>(TValue[] value)
            {
                return _imposter._generic_ParamsParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>(new Generic_ParamsParamArguments<TValue, TResult>(value)).Invoke(value);
            }

            public TResult Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                return _imposter._generic_AllRefKindMethodImposterCollection.GetImposterWithMatchingSetup<TOut, TRef, TIn, TParams, TResult>(new Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult>(refValue, inValue, paramsValues)).Invoke(out outValue, ref refValue, in inValue, paramsValues);
            }
        }

        global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodSetupFeatureSut.Void_NoParams()
        public delegate void Void_NoParamsDelegate();
        // void IMethodSetupFeatureSut.Void_NoParams()
        public delegate void Void_NoParamsCallbackDelegate();
        // void IMethodSetupFeatureSut.Void_NoParams()
        public delegate System.Exception Void_NoParamsExceptionGeneratorDelegate();
        public interface IVoid_NoParamsMethodInvocationHistory
        {
            bool Matches();
        }

        // void IMethodSetupFeatureSut.Void_NoParams()
        public class Void_NoParamsMethodInvocationHistory : IVoid_NoParamsMethodInvocationHistory
        {
            public System.Exception? Exception { get; }

            public Void_NoParamsMethodInvocationHistory(System.Exception? exception = default(System.Exception? ))
            {
                Exception = exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Void_NoParamsMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IVoid_NoParamsMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IVoid_NoParamsMethodInvocationHistory>();
            internal void Add(IVoid_NoParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodSetupFeatureSut.Void_NoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Void_NoParamsMethodInvocationsSetup : IVoid_NoParamsMethodInvocationsSetup
        {
            internal static Void_NoParamsMethodInvocationsSetup DefaultInvocationSetup = new Void_NoParamsMethodInvocationsSetup();
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

            internal static void DefaultResultGenerator()
            {
            }

            public Void_NoParamsMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IVoid_NoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            public IVoid_NoParamsMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw ex;
                };
                return this;
            }

            public IVoid_NoParamsMethodInvocationsSetup Throws(Void_NoParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            public IVoid_NoParamsMethodInvocationsSetup CallBefore(Void_NoParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IVoid_NoParamsMethodInvocationsSetup CallAfter(Void_NoParamsCallbackDelegate callback)
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

            public void Invoke()
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

                nextSetup.ResultGenerator.Invoke();
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter();
                };
            }

            internal class MethodInvocationSetup
            {
                internal Void_NoParamsDelegate? ResultGenerator { get; set; }
                internal Void_NoParamsCallbackDelegate? CallBefore { get; set; }
                internal Void_NoParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IVoid_NoParamsMethodInvocationsSetup
        {
            public IVoid_NoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IVoid_NoParamsMethodInvocationsSetup Throws(System.Exception ex);
            public IVoid_NoParamsMethodInvocationsSetup Throws(Void_NoParamsExceptionGeneratorDelegate exceptionGenerator);
            public IVoid_NoParamsMethodInvocationsSetup CallBefore(Void_NoParamsCallbackDelegate callback);
            public IVoid_NoParamsMethodInvocationsSetup CallAfter(Void_NoParamsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.Void_NoParams()
        public interface Void_NoParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.Void_NoParams()
        public interface IVoid_NoParamsMethodImposterBuilder : IVoid_NoParamsMethodInvocationsSetup, Void_NoParamsMethodInvocationVerifier
        {
        }

        // void IMethodSetupFeatureSut.Void_NoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Void_NoParamsMethodImposter
        {
            private readonly ConcurrentStack<Void_NoParamsMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Void_NoParamsMethodInvocationsSetup>();
            private readonly Void_NoParamsMethodInvocationHistoryCollection _void_NoParamsMethodInvocationHistoryCollection;
            public Void_NoParamsMethodImposter(Void_NoParamsMethodInvocationHistoryCollection _void_NoParamsMethodInvocationHistoryCollection)
            {
                this._void_NoParamsMethodInvocationHistoryCollection = _void_NoParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private Void_NoParamsMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public void Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? Void_NoParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke();
                    _void_NoParamsMethodInvocationHistoryCollection.Add(new Void_NoParamsMethodInvocationHistory(default));
                }
                catch (Exception ex)
                {
                    _void_NoParamsMethodInvocationHistoryCollection.Add(new Void_NoParamsMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IVoid_NoParamsMethodImposterBuilder
            {
                private readonly Void_NoParamsMethodImposter _imposter;
                private readonly Void_NoParamsMethodInvocationHistoryCollection _void_NoParamsMethodInvocationHistoryCollection;
                private Void_NoParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Void_NoParamsMethodImposter _imposter, Void_NoParamsMethodInvocationHistoryCollection _void_NoParamsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._void_NoParamsMethodInvocationHistoryCollection = _void_NoParamsMethodInvocationHistoryCollection;
                }

                private Void_NoParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Void_NoParamsMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IVoid_NoParamsMethodInvocationsSetup IVoid_NoParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IVoid_NoParamsMethodInvocationsSetup IVoid_NoParamsMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IVoid_NoParamsMethodInvocationsSetup IVoid_NoParamsMethodInvocationsSetup.Throws(Void_NoParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IVoid_NoParamsMethodInvocationsSetup IVoid_NoParamsMethodInvocationsSetup.CallBefore(Void_NoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IVoid_NoParamsMethodInvocationsSetup IVoid_NoParamsMethodInvocationsSetup.CallAfter(Void_NoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Void_NoParamsMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _void_NoParamsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_NoParams()
        public delegate int Int_NoParamsDelegate();
        // int IMethodSetupFeatureSut.Int_NoParams()
        public delegate void Int_NoParamsCallbackDelegate();
        // int IMethodSetupFeatureSut.Int_NoParams()
        public delegate System.Exception Int_NoParamsExceptionGeneratorDelegate();
        public interface IInt_NoParamsMethodInvocationHistory
        {
            bool Matches();
        }

        // int IMethodSetupFeatureSut.Int_NoParams()
        public class Int_NoParamsMethodInvocationHistory : IInt_NoParamsMethodInvocationHistory
        {
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_NoParamsMethodInvocationHistory(int result, System.Exception? exception = default(System.Exception? ))
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
        internal class Int_NoParamsMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_NoParamsMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_NoParamsMethodInvocationHistory>();
            internal void Add(IInt_NoParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodSetupFeatureSut.Int_NoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_NoParamsMethodInvocationsSetup : IInt_NoParamsMethodInvocationsSetup
        {
            internal static Int_NoParamsMethodInvocationsSetup DefaultInvocationSetup = new Int_NoParamsMethodInvocationsSetup();
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

            public Int_NoParamsMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IInt_NoParamsMethodInvocationsSetup Returns(Int_NoParamsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_NoParamsMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    return value;
                };
                return this;
            }

            public IInt_NoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            public IInt_NoParamsMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw ex;
                };
                return this;
            }

            public IInt_NoParamsMethodInvocationsSetup Throws(Int_NoParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            public IInt_NoParamsMethodInvocationsSetup CallBefore(Int_NoParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_NoParamsMethodInvocationsSetup CallAfter(Int_NoParamsCallbackDelegate callback)
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
                internal Int_NoParamsDelegate? ResultGenerator { get; set; }
                internal Int_NoParamsCallbackDelegate? CallBefore { get; set; }
                internal Int_NoParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_NoParamsMethodInvocationsSetup
        {
            public IInt_NoParamsMethodInvocationsSetup Returns(Int_NoParamsDelegate resultGenerator);
            public IInt_NoParamsMethodInvocationsSetup Returns(int value);
            public IInt_NoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_NoParamsMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_NoParamsMethodInvocationsSetup Throws(Int_NoParamsExceptionGeneratorDelegate exceptionGenerator);
            public IInt_NoParamsMethodInvocationsSetup CallBefore(Int_NoParamsCallbackDelegate callback);
            public IInt_NoParamsMethodInvocationsSetup CallAfter(Int_NoParamsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_NoParams()
        public interface Int_NoParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_NoParams()
        public interface IInt_NoParamsMethodImposterBuilder : IInt_NoParamsMethodInvocationsSetup, Int_NoParamsMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_NoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_NoParamsMethodImposter
        {
            private readonly ConcurrentStack<Int_NoParamsMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_NoParamsMethodInvocationsSetup>();
            private readonly Int_NoParamsMethodInvocationHistoryCollection _int_NoParamsMethodInvocationHistoryCollection;
            public Int_NoParamsMethodImposter(Int_NoParamsMethodInvocationHistoryCollection _int_NoParamsMethodInvocationHistoryCollection)
            {
                this._int_NoParamsMethodInvocationHistoryCollection = _int_NoParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private Int_NoParamsMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? Int_NoParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke();
                    _int_NoParamsMethodInvocationHistoryCollection.Add(new Int_NoParamsMethodInvocationHistory(result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_NoParamsMethodInvocationHistoryCollection.Add(new Int_NoParamsMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_NoParamsMethodImposterBuilder
            {
                private readonly Int_NoParamsMethodImposter _imposter;
                private readonly Int_NoParamsMethodInvocationHistoryCollection _int_NoParamsMethodInvocationHistoryCollection;
                private Int_NoParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_NoParamsMethodImposter _imposter, Int_NoParamsMethodInvocationHistoryCollection _int_NoParamsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._int_NoParamsMethodInvocationHistoryCollection = _int_NoParamsMethodInvocationHistoryCollection;
                }

                private Int_NoParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_NoParamsMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.Returns(Int_NoParamsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.Throws(Int_NoParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.CallBefore(Int_NoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_NoParamsMethodInvocationsSetup IInt_NoParamsMethodInvocationsSetup.CallAfter(Int_NoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_NoParamsMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_NoParamsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        public delegate int Int_SingleParamDelegate(int age);
        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        public delegate void Int_SingleParamCallbackDelegate(int age);
        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        public delegate System.Exception Int_SingleParamExceptionGeneratorDelegate(int age);
        public class Int_SingleParamArguments
        {
            public int age { get; }

            public Int_SingleParamArguments(int age)
            {
                this.age = age;
            }
        }

        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Int_SingleParamArgumentsCriteria
        {
            public Arg<int> age { get; }

            public Int_SingleParamArgumentsCriteria(Arg<int> age)
            {
                this.age = age;
            }

            public bool Matches(Int_SingleParamArguments arguments)
            {
                return age.Matches(arguments.age);
            }
        }

        public interface IInt_SingleParamMethodInvocationHistory
        {
            bool Matches(Int_SingleParamArgumentsCriteria criteria);
        }

        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        public class Int_SingleParamMethodInvocationHistory : IInt_SingleParamMethodInvocationHistory
        {
            public Int_SingleParamArguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_SingleParamMethodInvocationHistory(Int_SingleParamArguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(Int_SingleParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_SingleParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_SingleParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_SingleParamMethodInvocationHistory>();
            internal void Add(IInt_SingleParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Int_SingleParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_SingleParamMethodInvocationsSetup : IInt_SingleParamMethodInvocationsSetup
        {
            internal static Int_SingleParamMethodInvocationsSetup DefaultInvocationSetup = new Int_SingleParamMethodInvocationsSetup(new Int_SingleParamArgumentsCriteria(Arg<int>.Any()));
            internal Int_SingleParamArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(int age)
            {
                return default(int);
            }

            public Int_SingleParamMethodInvocationsSetup(Int_SingleParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IInt_SingleParamMethodInvocationsSetup Returns(Int_SingleParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_SingleParamMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    return value;
                };
                return this;
            }

            public IInt_SingleParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IInt_SingleParamMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw ex;
                };
                return this;
            }

            public IInt_SingleParamMethodInvocationsSetup Throws(Int_SingleParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw exceptionGenerator(age);
                };
                return this;
            }

            public IInt_SingleParamMethodInvocationsSetup CallBefore(Int_SingleParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_SingleParamMethodInvocationsSetup CallAfter(Int_SingleParamCallbackDelegate callback)
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

            public int Invoke(int age)
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
                internal Int_SingleParamDelegate? ResultGenerator { get; set; }
                internal Int_SingleParamCallbackDelegate? CallBefore { get; set; }
                internal Int_SingleParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_SingleParamMethodInvocationsSetup
        {
            public IInt_SingleParamMethodInvocationsSetup Returns(Int_SingleParamDelegate resultGenerator);
            public IInt_SingleParamMethodInvocationsSetup Returns(int value);
            public IInt_SingleParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_SingleParamMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_SingleParamMethodInvocationsSetup Throws(Int_SingleParamExceptionGeneratorDelegate exceptionGenerator);
            public IInt_SingleParamMethodInvocationsSetup CallBefore(Int_SingleParamCallbackDelegate callback);
            public IInt_SingleParamMethodInvocationsSetup CallAfter(Int_SingleParamCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        public interface Int_SingleParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        public interface IInt_SingleParamMethodImposterBuilder : IInt_SingleParamMethodInvocationsSetup, Int_SingleParamMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_SingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_SingleParamMethodImposter
        {
            private readonly ConcurrentStack<Int_SingleParamMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_SingleParamMethodInvocationsSetup>();
            private readonly Int_SingleParamMethodInvocationHistoryCollection _int_SingleParamMethodInvocationHistoryCollection;
            public Int_SingleParamMethodImposter(Int_SingleParamMethodInvocationHistoryCollection _int_SingleParamMethodInvocationHistoryCollection)
            {
                this._int_SingleParamMethodInvocationHistoryCollection = _int_SingleParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(Int_SingleParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Int_SingleParamMethodInvocationsSetup? FindMatchingSetup(Int_SingleParamArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int age)
            {
                var arguments = new Int_SingleParamArguments(age);
                var matchingSetup = FindMatchingSetup(arguments) ?? Int_SingleParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age);
                    _int_SingleParamMethodInvocationHistoryCollection.Add(new Int_SingleParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_SingleParamMethodInvocationHistoryCollection.Add(new Int_SingleParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_SingleParamMethodImposterBuilder
            {
                private readonly Int_SingleParamMethodImposter _imposter;
                private readonly Int_SingleParamMethodInvocationHistoryCollection _int_SingleParamMethodInvocationHistoryCollection;
                private readonly Int_SingleParamArgumentsCriteria _argumentsCriteria;
                private Int_SingleParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_SingleParamMethodImposter _imposter, Int_SingleParamMethodInvocationHistoryCollection _int_SingleParamMethodInvocationHistoryCollection, Int_SingleParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._int_SingleParamMethodInvocationHistoryCollection = _int_SingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Int_SingleParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_SingleParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.Returns(Int_SingleParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.Throws(Int_SingleParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.CallBefore(Int_SingleParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_SingleParamMethodInvocationsSetup IInt_SingleParamMethodInvocationsSetup.CallAfter(Int_SingleParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_SingleParamMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_SingleParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        public delegate int Int_ParamsDelegate(int age, string name, global::System.Text.RegularExpressions.Regex regex);
        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        public delegate void Int_ParamsCallbackDelegate(int age, string name, global::System.Text.RegularExpressions.Regex regex);
        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        public delegate System.Exception Int_ParamsExceptionGeneratorDelegate(int age, string name, global::System.Text.RegularExpressions.Regex regex);
        public class Int_ParamsArguments
        {
            public int age { get; }
            public string name { get; }
            public global::System.Text.RegularExpressions.Regex regex { get; }

            public Int_ParamsArguments(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                this.age = age;
                this.name = name;
                this.regex = regex;
            }
        }

        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Int_ParamsArgumentsCriteria
        {
            public Arg<int> age { get; }
            public Arg<string> name { get; }
            public Arg<global::System.Text.RegularExpressions.Regex> regex { get; }

            public Int_ParamsArgumentsCriteria(Arg<int> age, Arg<string> name, Arg<global::System.Text.RegularExpressions.Regex> regex)
            {
                this.age = age;
                this.name = name;
                this.regex = regex;
            }

            public bool Matches(Int_ParamsArguments arguments)
            {
                return age.Matches(arguments.age) && name.Matches(arguments.name) && regex.Matches(arguments.regex);
            }
        }

        public interface IInt_ParamsMethodInvocationHistory
        {
            bool Matches(Int_ParamsArgumentsCriteria criteria);
        }

        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        public class Int_ParamsMethodInvocationHistory : IInt_ParamsMethodInvocationHistory
        {
            public Int_ParamsArguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_ParamsMethodInvocationHistory(Int_ParamsArguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(Int_ParamsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_ParamsMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_ParamsMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_ParamsMethodInvocationHistory>();
            internal void Add(IInt_ParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Int_ParamsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_ParamsMethodInvocationsSetup : IInt_ParamsMethodInvocationsSetup
        {
            internal static Int_ParamsMethodInvocationsSetup DefaultInvocationSetup = new Int_ParamsMethodInvocationsSetup(new Int_ParamsArgumentsCriteria(Arg<int>.Any(), Arg<string>.Any(), Arg<global::System.Text.RegularExpressions.Regex>.Any()));
            internal Int_ParamsArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                return default(int);
            }

            public Int_ParamsMethodInvocationsSetup(Int_ParamsArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IInt_ParamsMethodInvocationsSetup Returns(Int_ParamsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_ParamsMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    return value;
                };
                return this;
            }

            public IInt_ParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IInt_ParamsMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    throw ex;
                };
                return this;
            }

            public IInt_ParamsMethodInvocationsSetup Throws(Int_ParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    throw exceptionGenerator(age, name, regex);
                };
                return this;
            }

            public IInt_ParamsMethodInvocationsSetup CallBefore(Int_ParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_ParamsMethodInvocationsSetup CallAfter(Int_ParamsCallbackDelegate callback)
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

            public int Invoke(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(age, name, regex);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(age, name, regex);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(age, name, regex);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Int_ParamsDelegate? ResultGenerator { get; set; }
                internal Int_ParamsCallbackDelegate? CallBefore { get; set; }
                internal Int_ParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_ParamsMethodInvocationsSetup
        {
            public IInt_ParamsMethodInvocationsSetup Returns(Int_ParamsDelegate resultGenerator);
            public IInt_ParamsMethodInvocationsSetup Returns(int value);
            public IInt_ParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_ParamsMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_ParamsMethodInvocationsSetup Throws(Int_ParamsExceptionGeneratorDelegate exceptionGenerator);
            public IInt_ParamsMethodInvocationsSetup CallBefore(Int_ParamsCallbackDelegate callback);
            public IInt_ParamsMethodInvocationsSetup CallAfter(Int_ParamsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        public interface Int_ParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        public interface IInt_ParamsMethodImposterBuilder : IInt_ParamsMethodInvocationsSetup, Int_ParamsMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_Params(int age, string name, Regex regex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_ParamsMethodImposter
        {
            private readonly ConcurrentStack<Int_ParamsMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_ParamsMethodInvocationsSetup>();
            private readonly Int_ParamsMethodInvocationHistoryCollection _int_ParamsMethodInvocationHistoryCollection;
            public Int_ParamsMethodImposter(Int_ParamsMethodInvocationHistoryCollection _int_ParamsMethodInvocationHistoryCollection)
            {
                this._int_ParamsMethodInvocationHistoryCollection = _int_ParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(Int_ParamsArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Int_ParamsMethodInvocationsSetup? FindMatchingSetup(Int_ParamsArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                var arguments = new Int_ParamsArguments(age, name, regex);
                var matchingSetup = FindMatchingSetup(arguments) ?? Int_ParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age, name, regex);
                    _int_ParamsMethodInvocationHistoryCollection.Add(new Int_ParamsMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_ParamsMethodInvocationHistoryCollection.Add(new Int_ParamsMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_ParamsMethodImposterBuilder
            {
                private readonly Int_ParamsMethodImposter _imposter;
                private readonly Int_ParamsMethodInvocationHistoryCollection _int_ParamsMethodInvocationHistoryCollection;
                private readonly Int_ParamsArgumentsCriteria _argumentsCriteria;
                private Int_ParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_ParamsMethodImposter _imposter, Int_ParamsMethodInvocationHistoryCollection _int_ParamsMethodInvocationHistoryCollection, Int_ParamsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._int_ParamsMethodInvocationHistoryCollection = _int_ParamsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Int_ParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_ParamsMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.Returns(Int_ParamsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.Throws(Int_ParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.CallBefore(Int_ParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_ParamsMethodInvocationsSetup IInt_ParamsMethodInvocationsSetup.CallAfter(Int_ParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_ParamsMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_ParamsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        public delegate int Int_OutParamDelegate(out int outValue);
        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        public delegate void Int_OutParamCallbackDelegate(out int outValue);
        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        public delegate System.Exception Int_OutParamExceptionGeneratorDelegate(out int outValue);
        public interface IInt_OutParamMethodInvocationHistory
        {
            bool Matches();
        }

        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        public class Int_OutParamMethodInvocationHistory : IInt_OutParamMethodInvocationHistory
        {
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_OutParamMethodInvocationHistory(int result, System.Exception? exception = default(System.Exception? ))
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
        internal class Int_OutParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_OutParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_OutParamMethodInvocationHistory>();
            internal void Add(IInt_OutParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_OutParamMethodInvocationsSetup : IInt_OutParamMethodInvocationsSetup
        {
            internal static Int_OutParamMethodInvocationsSetup DefaultInvocationSetup = new Int_OutParamMethodInvocationsSetup();
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

            internal static int DefaultResultGenerator(out int outValue)
            {
                InitializeOutParametersWithDefaultValues(out outValue);
                return default(int);
            }

            public Int_OutParamMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out int outValue)
            {
                outValue = default(int);
            }

            public IInt_OutParamMethodInvocationsSetup Returns(Int_OutParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_OutParamMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    return value;
                };
                return this;
            }

            public IInt_OutParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw new TException();
                };
                return this;
            }

            public IInt_OutParamMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw ex;
                };
                return this;
            }

            public IInt_OutParamMethodInvocationsSetup Throws(Int_OutParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    throw exceptionGenerator(out outValue);
                };
                return this;
            }

            public IInt_OutParamMethodInvocationsSetup CallBefore(Int_OutParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_OutParamMethodInvocationsSetup CallAfter(Int_OutParamCallbackDelegate callback)
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

            public int Invoke(out int outValue)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(out outValue);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(out outValue);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(out outValue);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Int_OutParamDelegate? ResultGenerator { get; set; }
                internal Int_OutParamCallbackDelegate? CallBefore { get; set; }
                internal Int_OutParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_OutParamMethodInvocationsSetup
        {
            public IInt_OutParamMethodInvocationsSetup Returns(Int_OutParamDelegate resultGenerator);
            public IInt_OutParamMethodInvocationsSetup Returns(int value);
            public IInt_OutParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_OutParamMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_OutParamMethodInvocationsSetup Throws(Int_OutParamExceptionGeneratorDelegate exceptionGenerator);
            public IInt_OutParamMethodInvocationsSetup CallBefore(Int_OutParamCallbackDelegate callback);
            public IInt_OutParamMethodInvocationsSetup CallAfter(Int_OutParamCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        public interface Int_OutParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        public interface IInt_OutParamMethodImposterBuilder : IInt_OutParamMethodInvocationsSetup, Int_OutParamMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_OutParam(out int outValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_OutParamMethodImposter
        {
            private readonly ConcurrentStack<Int_OutParamMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_OutParamMethodInvocationsSetup>();
            private readonly Int_OutParamMethodInvocationHistoryCollection _int_OutParamMethodInvocationHistoryCollection;
            public Int_OutParamMethodImposter(Int_OutParamMethodInvocationHistoryCollection _int_OutParamMethodInvocationHistoryCollection)
            {
                this._int_OutParamMethodInvocationHistoryCollection = _int_OutParamMethodInvocationHistoryCollection;
            }

            private static void InitializeOutParametersWithDefaultValues(out int outValue)
            {
                outValue = default(int);
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private Int_OutParamMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke(out int outValue)
            {
                var matchingSetup = FindMatchingSetup() ?? Int_OutParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out outValue);
                    _int_OutParamMethodInvocationHistoryCollection.Add(new Int_OutParamMethodInvocationHistory(result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_OutParamMethodInvocationHistoryCollection.Add(new Int_OutParamMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_OutParamMethodImposterBuilder
            {
                private readonly Int_OutParamMethodImposter _imposter;
                private readonly Int_OutParamMethodInvocationHistoryCollection _int_OutParamMethodInvocationHistoryCollection;
                private Int_OutParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_OutParamMethodImposter _imposter, Int_OutParamMethodInvocationHistoryCollection _int_OutParamMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._int_OutParamMethodInvocationHistoryCollection = _int_OutParamMethodInvocationHistoryCollection;
                }

                private Int_OutParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_OutParamMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.Returns(Int_OutParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.Throws(Int_OutParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.CallBefore(Int_OutParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_OutParamMethodInvocationsSetup IInt_OutParamMethodInvocationsSetup.CallAfter(Int_OutParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_OutParamMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_OutParamMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        public delegate int Int_RefParamDelegate(ref int refValue);
        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        public delegate void Int_RefParamCallbackDelegate(ref int refValue);
        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        public delegate System.Exception Int_RefParamExceptionGeneratorDelegate(ref int refValue);
        public class Int_RefParamArguments
        {
            public int refValue { get; }

            public Int_RefParamArguments(int refValue)
            {
                this.refValue = refValue;
            }
        }

        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Int_RefParamArgumentsCriteria
        {
            public Arg<int> refValue { get; }

            public Int_RefParamArgumentsCriteria(Arg<int> refValue)
            {
                this.refValue = refValue;
            }

            public bool Matches(Int_RefParamArguments arguments)
            {
                return refValue.Matches(arguments.refValue);
            }
        }

        public interface IInt_RefParamMethodInvocationHistory
        {
            bool Matches(Int_RefParamArgumentsCriteria criteria);
        }

        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        public class Int_RefParamMethodInvocationHistory : IInt_RefParamMethodInvocationHistory
        {
            public Int_RefParamArguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_RefParamMethodInvocationHistory(Int_RefParamArguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(Int_RefParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_RefParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_RefParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_RefParamMethodInvocationHistory>();
            internal void Add(IInt_RefParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Int_RefParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_RefParamMethodInvocationsSetup : IInt_RefParamMethodInvocationsSetup
        {
            internal static Int_RefParamMethodInvocationsSetup DefaultInvocationSetup = new Int_RefParamMethodInvocationsSetup(new Int_RefParamArgumentsCriteria(Arg<int>.Any()));
            internal Int_RefParamArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(ref int refValue)
            {
                return default(int);
            }

            public Int_RefParamMethodInvocationsSetup(Int_RefParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IInt_RefParamMethodInvocationsSetup Returns(Int_RefParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_RefParamMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    return value;
                };
                return this;
            }

            public IInt_RefParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IInt_RefParamMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    throw ex;
                };
                return this;
            }

            public IInt_RefParamMethodInvocationsSetup Throws(Int_RefParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    throw exceptionGenerator(ref refValue);
                };
                return this;
            }

            public IInt_RefParamMethodInvocationsSetup CallBefore(Int_RefParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_RefParamMethodInvocationsSetup CallAfter(Int_RefParamCallbackDelegate callback)
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

            public int Invoke(ref int refValue)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(ref refValue);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(ref refValue);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(ref refValue);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Int_RefParamDelegate? ResultGenerator { get; set; }
                internal Int_RefParamCallbackDelegate? CallBefore { get; set; }
                internal Int_RefParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_RefParamMethodInvocationsSetup
        {
            public IInt_RefParamMethodInvocationsSetup Returns(Int_RefParamDelegate resultGenerator);
            public IInt_RefParamMethodInvocationsSetup Returns(int value);
            public IInt_RefParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_RefParamMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_RefParamMethodInvocationsSetup Throws(Int_RefParamExceptionGeneratorDelegate exceptionGenerator);
            public IInt_RefParamMethodInvocationsSetup CallBefore(Int_RefParamCallbackDelegate callback);
            public IInt_RefParamMethodInvocationsSetup CallAfter(Int_RefParamCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        public interface Int_RefParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        public interface IInt_RefParamMethodImposterBuilder : IInt_RefParamMethodInvocationsSetup, Int_RefParamMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_RefParam(ref int refValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_RefParamMethodImposter
        {
            private readonly ConcurrentStack<Int_RefParamMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_RefParamMethodInvocationsSetup>();
            private readonly Int_RefParamMethodInvocationHistoryCollection _int_RefParamMethodInvocationHistoryCollection;
            public Int_RefParamMethodImposter(Int_RefParamMethodInvocationHistoryCollection _int_RefParamMethodInvocationHistoryCollection)
            {
                this._int_RefParamMethodInvocationHistoryCollection = _int_RefParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(Int_RefParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Int_RefParamMethodInvocationsSetup? FindMatchingSetup(Int_RefParamArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(ref int refValue)
            {
                var arguments = new Int_RefParamArguments(refValue);
                var matchingSetup = FindMatchingSetup(arguments) ?? Int_RefParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ref refValue);
                    _int_RefParamMethodInvocationHistoryCollection.Add(new Int_RefParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_RefParamMethodInvocationHistoryCollection.Add(new Int_RefParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_RefParamMethodImposterBuilder
            {
                private readonly Int_RefParamMethodImposter _imposter;
                private readonly Int_RefParamMethodInvocationHistoryCollection _int_RefParamMethodInvocationHistoryCollection;
                private readonly Int_RefParamArgumentsCriteria _argumentsCriteria;
                private Int_RefParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_RefParamMethodImposter _imposter, Int_RefParamMethodInvocationHistoryCollection _int_RefParamMethodInvocationHistoryCollection, Int_RefParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._int_RefParamMethodInvocationHistoryCollection = _int_RefParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Int_RefParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_RefParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.Returns(Int_RefParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.Throws(Int_RefParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.CallBefore(Int_RefParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_RefParamMethodInvocationsSetup IInt_RefParamMethodInvocationsSetup.CallAfter(Int_RefParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_RefParamMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_RefParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        public delegate int Int_ParamsParamDelegate(string[] paramsStrings);
        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        public delegate void Int_ParamsParamCallbackDelegate(string[] paramsStrings);
        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        public delegate System.Exception Int_ParamsParamExceptionGeneratorDelegate(string[] paramsStrings);
        public class Int_ParamsParamArguments
        {
            public string[] paramsStrings { get; }

            public Int_ParamsParamArguments(string[] paramsStrings)
            {
                this.paramsStrings = paramsStrings;
            }
        }

        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Int_ParamsParamArgumentsCriteria
        {
            public Arg<string[]> paramsStrings { get; }

            public Int_ParamsParamArgumentsCriteria(Arg<string[]> paramsStrings)
            {
                this.paramsStrings = paramsStrings;
            }

            public bool Matches(Int_ParamsParamArguments arguments)
            {
                return paramsStrings.Matches(arguments.paramsStrings);
            }
        }

        public interface IInt_ParamsParamMethodInvocationHistory
        {
            bool Matches(Int_ParamsParamArgumentsCriteria criteria);
        }

        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        public class Int_ParamsParamMethodInvocationHistory : IInt_ParamsParamMethodInvocationHistory
        {
            public Int_ParamsParamArguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_ParamsParamMethodInvocationHistory(Int_ParamsParamArguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(Int_ParamsParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_ParamsParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_ParamsParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_ParamsParamMethodInvocationHistory>();
            internal void Add(IInt_ParamsParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Int_ParamsParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_ParamsParamMethodInvocationsSetup : IInt_ParamsParamMethodInvocationsSetup
        {
            internal static Int_ParamsParamMethodInvocationsSetup DefaultInvocationSetup = new Int_ParamsParamMethodInvocationsSetup(new Int_ParamsParamArgumentsCriteria(Arg<string[]>.Any()));
            internal Int_ParamsParamArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(string[] paramsStrings)
            {
                return default(int);
            }

            public Int_ParamsParamMethodInvocationsSetup(Int_ParamsParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IInt_ParamsParamMethodInvocationsSetup Returns(Int_ParamsParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_ParamsParamMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    return value;
                };
                return this;
            }

            public IInt_ParamsParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IInt_ParamsParamMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    throw ex;
                };
                return this;
            }

            public IInt_ParamsParamMethodInvocationsSetup Throws(Int_ParamsParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    throw exceptionGenerator(paramsStrings);
                };
                return this;
            }

            public IInt_ParamsParamMethodInvocationsSetup CallBefore(Int_ParamsParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_ParamsParamMethodInvocationsSetup CallAfter(Int_ParamsParamCallbackDelegate callback)
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

            public int Invoke(string[] paramsStrings)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(paramsStrings);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(paramsStrings);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(paramsStrings);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Int_ParamsParamDelegate? ResultGenerator { get; set; }
                internal Int_ParamsParamCallbackDelegate? CallBefore { get; set; }
                internal Int_ParamsParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_ParamsParamMethodInvocationsSetup
        {
            public IInt_ParamsParamMethodInvocationsSetup Returns(Int_ParamsParamDelegate resultGenerator);
            public IInt_ParamsParamMethodInvocationsSetup Returns(int value);
            public IInt_ParamsParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_ParamsParamMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_ParamsParamMethodInvocationsSetup Throws(Int_ParamsParamExceptionGeneratorDelegate exceptionGenerator);
            public IInt_ParamsParamMethodInvocationsSetup CallBefore(Int_ParamsParamCallbackDelegate callback);
            public IInt_ParamsParamMethodInvocationsSetup CallAfter(Int_ParamsParamCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        public interface Int_ParamsParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        public interface IInt_ParamsParamMethodImposterBuilder : IInt_ParamsParamMethodInvocationsSetup, Int_ParamsParamMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_ParamsParam(params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_ParamsParamMethodImposter
        {
            private readonly ConcurrentStack<Int_ParamsParamMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_ParamsParamMethodInvocationsSetup>();
            private readonly Int_ParamsParamMethodInvocationHistoryCollection _int_ParamsParamMethodInvocationHistoryCollection;
            public Int_ParamsParamMethodImposter(Int_ParamsParamMethodInvocationHistoryCollection _int_ParamsParamMethodInvocationHistoryCollection)
            {
                this._int_ParamsParamMethodInvocationHistoryCollection = _int_ParamsParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(Int_ParamsParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Int_ParamsParamMethodInvocationsSetup? FindMatchingSetup(Int_ParamsParamArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(string[] paramsStrings)
            {
                var arguments = new Int_ParamsParamArguments(paramsStrings);
                var matchingSetup = FindMatchingSetup(arguments) ?? Int_ParamsParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(paramsStrings);
                    _int_ParamsParamMethodInvocationHistoryCollection.Add(new Int_ParamsParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_ParamsParamMethodInvocationHistoryCollection.Add(new Int_ParamsParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_ParamsParamMethodImposterBuilder
            {
                private readonly Int_ParamsParamMethodImposter _imposter;
                private readonly Int_ParamsParamMethodInvocationHistoryCollection _int_ParamsParamMethodInvocationHistoryCollection;
                private readonly Int_ParamsParamArgumentsCriteria _argumentsCriteria;
                private Int_ParamsParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_ParamsParamMethodImposter _imposter, Int_ParamsParamMethodInvocationHistoryCollection _int_ParamsParamMethodInvocationHistoryCollection, Int_ParamsParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._int_ParamsParamMethodInvocationHistoryCollection = _int_ParamsParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Int_ParamsParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_ParamsParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.Returns(Int_ParamsParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.Throws(Int_ParamsParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.CallBefore(Int_ParamsParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_ParamsParamMethodInvocationsSetup IInt_ParamsParamMethodInvocationsSetup.CallAfter(Int_ParamsParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_ParamsParamMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_ParamsParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        public delegate int Int_InParamDelegate(in string inStringValue);
        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        public delegate void Int_InParamCallbackDelegate(in string inStringValue);
        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        public delegate System.Exception Int_InParamExceptionGeneratorDelegate(in string inStringValue);
        public class Int_InParamArguments
        {
            public string inStringValue { get; }

            public Int_InParamArguments(string inStringValue)
            {
                this.inStringValue = inStringValue;
            }
        }

        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Int_InParamArgumentsCriteria
        {
            public Arg<string> inStringValue { get; }

            public Int_InParamArgumentsCriteria(Arg<string> inStringValue)
            {
                this.inStringValue = inStringValue;
            }

            public bool Matches(Int_InParamArguments arguments)
            {
                return inStringValue.Matches(arguments.inStringValue);
            }
        }

        public interface IInt_InParamMethodInvocationHistory
        {
            bool Matches(Int_InParamArgumentsCriteria criteria);
        }

        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        public class Int_InParamMethodInvocationHistory : IInt_InParamMethodInvocationHistory
        {
            public Int_InParamArguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_InParamMethodInvocationHistory(Int_InParamArguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(Int_InParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_InParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_InParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_InParamMethodInvocationHistory>();
            internal void Add(IInt_InParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Int_InParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_InParamMethodInvocationsSetup : IInt_InParamMethodInvocationsSetup
        {
            internal static Int_InParamMethodInvocationsSetup DefaultInvocationSetup = new Int_InParamMethodInvocationsSetup(new Int_InParamArgumentsCriteria(Arg<string>.Any()));
            internal Int_InParamArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(in string inStringValue)
            {
                return default(int);
            }

            public Int_InParamMethodInvocationsSetup(Int_InParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IInt_InParamMethodInvocationsSetup Returns(Int_InParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_InParamMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    return value;
                };
                return this;
            }

            public IInt_InParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IInt_InParamMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    throw ex;
                };
                return this;
            }

            public IInt_InParamMethodInvocationsSetup Throws(Int_InParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    throw exceptionGenerator(in inStringValue);
                };
                return this;
            }

            public IInt_InParamMethodInvocationsSetup CallBefore(Int_InParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_InParamMethodInvocationsSetup CallAfter(Int_InParamCallbackDelegate callback)
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

            public int Invoke(in string inStringValue)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(in inStringValue);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(in inStringValue);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(in inStringValue);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Int_InParamDelegate? ResultGenerator { get; set; }
                internal Int_InParamCallbackDelegate? CallBefore { get; set; }
                internal Int_InParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_InParamMethodInvocationsSetup
        {
            public IInt_InParamMethodInvocationsSetup Returns(Int_InParamDelegate resultGenerator);
            public IInt_InParamMethodInvocationsSetup Returns(int value);
            public IInt_InParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_InParamMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_InParamMethodInvocationsSetup Throws(Int_InParamExceptionGeneratorDelegate exceptionGenerator);
            public IInt_InParamMethodInvocationsSetup CallBefore(Int_InParamCallbackDelegate callback);
            public IInt_InParamMethodInvocationsSetup CallAfter(Int_InParamCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        public interface Int_InParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        public interface IInt_InParamMethodImposterBuilder : IInt_InParamMethodInvocationsSetup, Int_InParamMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_InParam(in string inStringValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_InParamMethodImposter
        {
            private readonly ConcurrentStack<Int_InParamMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_InParamMethodInvocationsSetup>();
            private readonly Int_InParamMethodInvocationHistoryCollection _int_InParamMethodInvocationHistoryCollection;
            public Int_InParamMethodImposter(Int_InParamMethodInvocationHistoryCollection _int_InParamMethodInvocationHistoryCollection)
            {
                this._int_InParamMethodInvocationHistoryCollection = _int_InParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(Int_InParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Int_InParamMethodInvocationsSetup? FindMatchingSetup(Int_InParamArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(in string inStringValue)
            {
                var arguments = new Int_InParamArguments(inStringValue);
                var matchingSetup = FindMatchingSetup(arguments) ?? Int_InParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(in inStringValue);
                    _int_InParamMethodInvocationHistoryCollection.Add(new Int_InParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_InParamMethodInvocationHistoryCollection.Add(new Int_InParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_InParamMethodImposterBuilder
            {
                private readonly Int_InParamMethodImposter _imposter;
                private readonly Int_InParamMethodInvocationHistoryCollection _int_InParamMethodInvocationHistoryCollection;
                private readonly Int_InParamArgumentsCriteria _argumentsCriteria;
                private Int_InParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_InParamMethodImposter _imposter, Int_InParamMethodInvocationHistoryCollection _int_InParamMethodInvocationHistoryCollection, Int_InParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._int_InParamMethodInvocationHistoryCollection = _int_InParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Int_InParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_InParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.Returns(Int_InParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.Throws(Int_InParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.CallBefore(Int_InParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_InParamMethodInvocationsSetup IInt_InParamMethodInvocationsSetup.CallAfter(Int_InParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_InParamMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_InParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public delegate int Int_AllRefKindsDelegate(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings);
        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public delegate void Int_AllRefKindsCallbackDelegate(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings);
        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public delegate System.Exception Int_AllRefKindsExceptionGeneratorDelegate(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings);
        public class Int_AllRefKindsArguments
        {
            public int refValue { get; }
            public int inValue { get; }
            public string valueAsString { get; }
            public string[] paramsStrings { get; }

            public Int_AllRefKindsArguments(int refValue, int inValue, string valueAsString, string[] paramsStrings)
            {
                this.refValue = refValue;
                this.inValue = inValue;
                this.valueAsString = valueAsString;
                this.paramsStrings = paramsStrings;
            }
        }

        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Int_AllRefKindsArgumentsCriteria
        {
            public OutArg<int> value { get; }
            public Arg<int> refValue { get; }
            public Arg<int> inValue { get; }
            public Arg<string> valueAsString { get; }
            public Arg<string[]> paramsStrings { get; }

            public Int_AllRefKindsArgumentsCriteria(OutArg<int> value, Arg<int> refValue, Arg<int> inValue, Arg<string> valueAsString, Arg<string[]> paramsStrings)
            {
                this.value = value;
                this.refValue = refValue;
                this.inValue = inValue;
                this.valueAsString = valueAsString;
                this.paramsStrings = paramsStrings;
            }

            public bool Matches(Int_AllRefKindsArguments arguments)
            {
                return refValue.Matches(arguments.refValue) && inValue.Matches(arguments.inValue) && valueAsString.Matches(arguments.valueAsString) && paramsStrings.Matches(arguments.paramsStrings);
            }
        }

        public interface IInt_AllRefKindsMethodInvocationHistory
        {
            bool Matches(Int_AllRefKindsArgumentsCriteria criteria);
        }

        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public class Int_AllRefKindsMethodInvocationHistory : IInt_AllRefKindsMethodInvocationHistory
        {
            public Int_AllRefKindsArguments Arguments { get; }
            public int Result { get; }
            public System.Exception? Exception { get; }

            public Int_AllRefKindsMethodInvocationHistory(Int_AllRefKindsArguments arguments, int result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches(Int_AllRefKindsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_AllRefKindsMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IInt_AllRefKindsMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IInt_AllRefKindsMethodInvocationHistory>();
            internal void Add(IInt_AllRefKindsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Int_AllRefKindsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Int_AllRefKindsMethodInvocationsSetup : IInt_AllRefKindsMethodInvocationsSetup
        {
            internal static Int_AllRefKindsMethodInvocationsSetup DefaultInvocationSetup = new Int_AllRefKindsMethodInvocationsSetup(new Int_AllRefKindsArgumentsCriteria(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any()));
            internal Int_AllRefKindsArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings)
            {
                InitializeOutParametersWithDefaultValues(out value);
                return default(int);
            }

            public Int_AllRefKindsMethodInvocationsSetup(Int_AllRefKindsArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out int value)
            {
                value = default(int);
            }

            public IInt_AllRefKindsMethodInvocationsSetup Returns(Int_AllRefKindsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IInt_AllRefKindsMethodInvocationsSetup Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    return value;
                };
                return this;
            }

            public IInt_AllRefKindsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw new TException();
                };
                return this;
            }

            public IInt_AllRefKindsMethodInvocationsSetup Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw ex;
                };
                return this;
            }

            public IInt_AllRefKindsMethodInvocationsSetup Throws(Int_AllRefKindsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    throw exceptionGenerator(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                };
                return this;
            }

            public IInt_AllRefKindsMethodInvocationsSetup CallBefore(Int_AllRefKindsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IInt_AllRefKindsMethodInvocationsSetup CallAfter(Int_AllRefKindsCallbackDelegate callback)
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

            public int Invoke(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Int_AllRefKindsDelegate? ResultGenerator { get; set; }
                internal Int_AllRefKindsCallbackDelegate? CallBefore { get; set; }
                internal Int_AllRefKindsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInt_AllRefKindsMethodInvocationsSetup
        {
            public IInt_AllRefKindsMethodInvocationsSetup Returns(Int_AllRefKindsDelegate resultGenerator);
            public IInt_AllRefKindsMethodInvocationsSetup Returns(int value);
            public IInt_AllRefKindsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            public IInt_AllRefKindsMethodInvocationsSetup Throws(System.Exception ex);
            public IInt_AllRefKindsMethodInvocationsSetup Throws(Int_AllRefKindsExceptionGeneratorDelegate exceptionGenerator);
            public IInt_AllRefKindsMethodInvocationsSetup CallBefore(Int_AllRefKindsCallbackDelegate callback);
            public IInt_AllRefKindsMethodInvocationsSetup CallAfter(Int_AllRefKindsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public interface Int_AllRefKindsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public interface IInt_AllRefKindsMethodImposterBuilder : IInt_AllRefKindsMethodInvocationsSetup, Int_AllRefKindsMethodInvocationVerifier
        {
        }

        // int IMethodSetupFeatureSut.Int_AllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Int_AllRefKindsMethodImposter
        {
            private readonly ConcurrentStack<Int_AllRefKindsMethodInvocationsSetup> _invocationSetups = new ConcurrentStack<Int_AllRefKindsMethodInvocationsSetup>();
            private readonly Int_AllRefKindsMethodInvocationHistoryCollection _int_AllRefKindsMethodInvocationHistoryCollection;
            public Int_AllRefKindsMethodImposter(Int_AllRefKindsMethodInvocationHistoryCollection _int_AllRefKindsMethodInvocationHistoryCollection)
            {
                this._int_AllRefKindsMethodInvocationHistoryCollection = _int_AllRefKindsMethodInvocationHistoryCollection;
            }

            private static void InitializeOutParametersWithDefaultValues(out int value)
            {
                value = default(int);
            }

            public bool HasMatchingSetup(Int_AllRefKindsArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Int_AllRefKindsMethodInvocationsSetup? FindMatchingSetup(Int_AllRefKindsArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings)
            {
                var arguments = new Int_AllRefKindsArguments(refValue, inValue, valueAsString, paramsStrings);
                var matchingSetup = FindMatchingSetup(arguments) ?? Int_AllRefKindsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                    _int_AllRefKindsMethodInvocationHistoryCollection.Add(new Int_AllRefKindsMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _int_AllRefKindsMethodInvocationHistoryCollection.Add(new Int_AllRefKindsMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInt_AllRefKindsMethodImposterBuilder
            {
                private readonly Int_AllRefKindsMethodImposter _imposter;
                private readonly Int_AllRefKindsMethodInvocationHistoryCollection _int_AllRefKindsMethodInvocationHistoryCollection;
                private readonly Int_AllRefKindsArgumentsCriteria _argumentsCriteria;
                private Int_AllRefKindsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(Int_AllRefKindsMethodImposter _imposter, Int_AllRefKindsMethodInvocationHistoryCollection _int_AllRefKindsMethodInvocationHistoryCollection, Int_AllRefKindsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._int_AllRefKindsMethodInvocationHistoryCollection = _int_AllRefKindsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Int_AllRefKindsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Int_AllRefKindsMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.Returns(Int_AllRefKindsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.Throws(Int_AllRefKindsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.CallBefore(Int_AllRefKindsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IInt_AllRefKindsMethodInvocationsSetup IInt_AllRefKindsMethodInvocationsSetup.CallAfter(Int_AllRefKindsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Int_AllRefKindsMethodInvocationVerifier.Called(Count count)
                {
                    var invocationCount = _int_AllRefKindsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        public delegate void Generic_SingleParamDelegate<TValue>(TValue value);
        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        public delegate void Generic_SingleParamCallbackDelegate<TValue>(TValue value);
        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        public delegate System.Exception Generic_SingleParamExceptionGeneratorDelegate<TValue>(TValue value);
        public class Generic_SingleParamArguments<TValue>
        {
            public TValue value { get; }

            public Generic_SingleParamArguments(TValue value)
            {
                this.value = value;
            }

            public Generic_SingleParamArguments<TValueTarget> As<TValueTarget>()
            {
                return new Generic_SingleParamArguments<TValueTarget>(TypeCaster.Cast<TValue, TValueTarget>(value));
            }
        }

        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Generic_SingleParamArgumentsCriteria<TValue>
        {
            public Arg<TValue> value { get; }

            public Generic_SingleParamArgumentsCriteria(Arg<TValue> value)
            {
                this.value = value;
            }

            public bool Matches(Generic_SingleParamArguments<TValue> arguments)
            {
                return value.Matches(arguments.value);
            }

            public Generic_SingleParamArgumentsCriteria<TValueTarget> As<TValueTarget>()
            {
                return new Generic_SingleParamArgumentsCriteria<TValueTarget>(Imposter.Abstractions.Arg<TValueTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget, TValue>(it, out TValue valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGeneric_SingleParamMethodInvocationHistory
        {
            bool Matches<TValue>(Generic_SingleParamArgumentsCriteria<TValue> criteria);
        }

        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        public class Generic_SingleParamMethodInvocationHistory<TValue> : IGeneric_SingleParamMethodInvocationHistory
        {
            public Generic_SingleParamArguments<TValue> Arguments { get; }
            public System.Exception? Exception { get; }

            public Generic_SingleParamMethodInvocationHistory(Generic_SingleParamArguments<TValue> arguments, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Exception = exception;
            }

            public bool Matches<TValueTarget>(Generic_SingleParamArgumentsCriteria<TValueTarget> criteria)
            {
                return criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_SingleParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IGeneric_SingleParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IGeneric_SingleParamMethodInvocationHistory>();
            internal void Add(IGeneric_SingleParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue>(Generic_SingleParamArgumentsCriteria<TValue> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue>(argumentsCriteria));
            }
        }

        internal class Generic_SingleParamMethodImposterCollection
        {
            private readonly Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection;
            public Generic_SingleParamMethodImposterCollection(Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection)
            {
                this._generic_SingleParamMethodInvocationHistoryCollection = _generic_SingleParamMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IGeneric_SingleParamMethodImposter> _imposters = new ConcurrentStack<IGeneric_SingleParamMethodImposter>();
            internal Generic_SingleParamMethodImposter<TValue> AddNew<TValue>()
            {
                var imposter = new Generic_SingleParamMethodImposter<TValue>(_generic_SingleParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGeneric_SingleParamMethodImposter<TValue> GetImposterWithMatchingSetup<TValue>(Generic_SingleParamArguments<TValue> arguments)
            {
                return _imposters.Select(it => it.As<TValue>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue>();
            }
        }

        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Generic_SingleParamMethodInvocationsSetup<TValue> : IGeneric_SingleParamMethodInvocationsSetup<TValue>
        {
            internal static Generic_SingleParamMethodInvocationsSetup<TValue> DefaultInvocationSetup = new Generic_SingleParamMethodInvocationsSetup<TValue>(new Generic_SingleParamArgumentsCriteria<TValue>(Arg<TValue>.Any()));
            internal Generic_SingleParamArgumentsCriteria<TValue> ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(TValue value)
            {
            }

            public Generic_SingleParamMethodInvocationsSetup(Generic_SingleParamArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IGeneric_SingleParamMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IGeneric_SingleParamMethodInvocationsSetup<TValue> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw ex;
                };
                return this;
            }

            public IGeneric_SingleParamMethodInvocationsSetup<TValue> Throws(Generic_SingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            public IGeneric_SingleParamMethodInvocationsSetup<TValue> CallBefore(Generic_SingleParamCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IGeneric_SingleParamMethodInvocationsSetup<TValue> CallAfter(Generic_SingleParamCallbackDelegate<TValue> callback)
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

            public void Invoke(TValue value)
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

                nextSetup.ResultGenerator.Invoke(value);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(value);
                };
            }

            internal class MethodInvocationSetup
            {
                internal Generic_SingleParamDelegate<TValue>? ResultGenerator { get; set; }
                internal Generic_SingleParamCallbackDelegate<TValue>? CallBefore { get; set; }
                internal Generic_SingleParamCallbackDelegate<TValue>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGeneric_SingleParamMethodInvocationsSetup<TValue>
        {
            public IGeneric_SingleParamMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            public IGeneric_SingleParamMethodInvocationsSetup<TValue> Throws(System.Exception ex);
            public IGeneric_SingleParamMethodInvocationsSetup<TValue> Throws(Generic_SingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator);
            public IGeneric_SingleParamMethodInvocationsSetup<TValue> CallBefore(Generic_SingleParamCallbackDelegate<TValue> callback);
            public IGeneric_SingleParamMethodInvocationsSetup<TValue> CallAfter(Generic_SingleParamCallbackDelegate<TValue> callback);
        }

        internal interface IGeneric_SingleParamMethodImposter
        {
            IGeneric_SingleParamMethodImposter<TValueTarget>? As<TValueTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGeneric_SingleParamMethodImposter<TValue> : IGeneric_SingleParamMethodImposter
        {
            void Invoke(TValue value);
            bool HasMatchingSetup(Generic_SingleParamArguments<TValue> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        public interface Generic_SingleParamMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        public interface IGeneric_SingleParamMethodImposterBuilder<TValue> : IGeneric_SingleParamMethodInvocationsSetup<TValue>, Generic_SingleParamMethodInvocationVerifier<TValue>
        {
        }

        // void IMethodSetupFeatureSut.Generic_SingleParam<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_SingleParamMethodImposter<TValue> : IGeneric_SingleParamMethodImposter<TValue>
        {
            private readonly ConcurrentStack<Generic_SingleParamMethodInvocationsSetup<TValue>> _invocationSetups = new ConcurrentStack<Generic_SingleParamMethodInvocationsSetup<TValue>>();
            private readonly Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection;
            public Generic_SingleParamMethodImposter(Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection)
            {
                this._generic_SingleParamMethodInvocationHistoryCollection = _generic_SingleParamMethodInvocationHistoryCollection;
            }

            IGeneric_SingleParamMethodImposter<TValueTarget>? IGeneric_SingleParamMethodImposter.As<TValueTarget>()
            {
                if (typeof(TValueTarget).IsAssignableTo(typeof(TValue)))
                {
                    return new Adapter<TValueTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget> : IGeneric_SingleParamMethodImposter<TValueTarget>
            {
                private readonly Generic_SingleParamMethodImposter<TValue> _target;
                public Adapter(Generic_SingleParamMethodImposter<TValue> target)
                {
                    _target = target;
                }

                public void Invoke(TValueTarget value)
                {
                    _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TValueTarget, TValue>(value));
                }

                public bool HasMatchingSetup(Generic_SingleParamArguments<TValueTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue>());
                }

                IGeneric_SingleParamMethodImposter<TValueTarget1>? IGeneric_SingleParamMethodImposter.As<TValueTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(Generic_SingleParamArguments<TValue> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Generic_SingleParamMethodInvocationsSetup<TValue>? FindMatchingSetup(Generic_SingleParamArguments<TValue> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(TValue value)
            {
                var arguments = new Generic_SingleParamArguments<TValue>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? Generic_SingleParamMethodInvocationsSetup<TValue>.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(value);
                    _generic_SingleParamMethodInvocationHistoryCollection.Add(new Generic_SingleParamMethodInvocationHistory<TValue>(arguments, default));
                }
                catch (Exception ex)
                {
                    _generic_SingleParamMethodInvocationHistoryCollection.Add(new Generic_SingleParamMethodInvocationHistory<TValue>(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGeneric_SingleParamMethodImposterBuilder<TValue>
            {
                private readonly Generic_SingleParamMethodImposterCollection _imposterCollection;
                private readonly Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection;
                private readonly Generic_SingleParamArgumentsCriteria<TValue> _argumentsCriteria;
                private Generic_SingleParamMethodInvocationsSetup<TValue>? _existingInvocationSetup;
                public Builder(Generic_SingleParamMethodImposterCollection _imposterCollection, Generic_SingleParamMethodInvocationHistoryCollection _generic_SingleParamMethodInvocationHistoryCollection, Generic_SingleParamArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._generic_SingleParamMethodInvocationHistoryCollection = _generic_SingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Generic_SingleParamMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Generic_SingleParamMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGeneric_SingleParamMethodInvocationsSetup<TValue> IGeneric_SingleParamMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGeneric_SingleParamMethodInvocationsSetup<TValue> IGeneric_SingleParamMethodInvocationsSetup<TValue>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IGeneric_SingleParamMethodInvocationsSetup<TValue> IGeneric_SingleParamMethodInvocationsSetup<TValue>.Throws(Generic_SingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGeneric_SingleParamMethodInvocationsSetup<TValue> IGeneric_SingleParamMethodInvocationsSetup<TValue>.CallBefore(Generic_SingleParamCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGeneric_SingleParamMethodInvocationsSetup<TValue> IGeneric_SingleParamMethodInvocationsSetup<TValue>.CallAfter(Generic_SingleParamCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Generic_SingleParamMethodInvocationVerifier<TValue>.Called(Count count)
                {
                    var invocationCount = _generic_SingleParamMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        public delegate TResult Generic_OutParamDelegate<TValue, TResult>(out TValue value);
        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        public delegate void Generic_OutParamCallbackDelegate<TValue, TResult>(out TValue value);
        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        public delegate System.Exception Generic_OutParamExceptionGeneratorDelegate<TValue, TResult>(out TValue value);
        public interface IGeneric_OutParamMethodInvocationHistory
        {
            bool Matches<TValue, TResult>();
        }

        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        public class Generic_OutParamMethodInvocationHistory<TValue, TResult> : IGeneric_OutParamMethodInvocationHistory
        {
            public TResult Result { get; }
            public System.Exception? Exception { get; }

            public Generic_OutParamMethodInvocationHistory(TResult result, System.Exception? exception = default(System.Exception? ))
            {
                Result = result;
                Exception = exception;
            }

            public bool Matches<TValueTarget, TResultTarget>()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_OutParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IGeneric_OutParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IGeneric_OutParamMethodInvocationHistory>();
            internal void Add(IGeneric_OutParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>()
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>());
            }
        }

        internal class Generic_OutParamMethodImposterCollection
        {
            private readonly Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection;
            public Generic_OutParamMethodImposterCollection(Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection)
            {
                this._generic_OutParamMethodInvocationHistoryCollection = _generic_OutParamMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IGeneric_OutParamMethodImposter> _imposters = new ConcurrentStack<IGeneric_OutParamMethodImposter>();
            internal Generic_OutParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new Generic_OutParamMethodImposter<TValue, TResult>(_generic_OutParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGeneric_OutParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>()
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup()) ?? AddNew<TValue, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Generic_OutParamMethodInvocationsSetup<TValue, TResult> : IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static Generic_OutParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new Generic_OutParamMethodInvocationsSetup<TValue, TResult>();
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

            internal static TResult DefaultResultGenerator(out TValue value)
            {
                InitializeOutParametersWithDefaultValues(out value);
                return default(TResult);
            }

            public Generic_OutParamMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TValue value)
            {
                value = default(TValue);
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Returns(Generic_OutParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    return value;
                };
                return this;
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw new TException();
                };
                return this;
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw ex;
                };
                return this;
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Throws(Generic_OutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    throw exceptionGenerator(out value);
                };
                return this;
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> CallBefore(Generic_OutParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> CallAfter(Generic_OutParamCallbackDelegate<TValue, TResult> callback)
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

            public TResult Invoke(out TValue value)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(out value);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(out value);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(out value);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Generic_OutParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal Generic_OutParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal Generic_OutParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>
        {
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Returns(Generic_OutParamDelegate<TValue, TResult> resultGenerator);
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value);
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception ex);
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> Throws(Generic_OutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> CallBefore(Generic_OutParamCallbackDelegate<TValue, TResult> callback);
            public IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> CallAfter(Generic_OutParamCallbackDelegate<TValue, TResult> callback);
        }

        internal interface IGeneric_OutParamMethodImposter
        {
            IGeneric_OutParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGeneric_OutParamMethodImposter<TValue, TResult> : IGeneric_OutParamMethodImposter
        {
            TResult Invoke(out TValue value);
            bool HasMatchingSetup();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        public interface Generic_OutParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        public interface IGeneric_OutParamMethodImposterBuilder<TValue, TResult> : IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>, Generic_OutParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        // TResult IMethodSetupFeatureSut.Generic_OutParam<TValue, TResult>(out TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_OutParamMethodImposter<TValue, TResult> : IGeneric_OutParamMethodImposter<TValue, TResult>
        {
            private readonly ConcurrentStack<Generic_OutParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new ConcurrentStack<Generic_OutParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection;
            public Generic_OutParamMethodImposter(Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection)
            {
                this._generic_OutParamMethodInvocationHistoryCollection = _generic_OutParamMethodInvocationHistoryCollection;
            }

            IGeneric_OutParamMethodImposter<TValueTarget, TResultTarget>? IGeneric_OutParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(TValue).IsAssignableTo(typeof(TValueTarget)) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGeneric_OutParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly Generic_OutParamMethodImposter<TValue, TResult> _target;
                public Adapter(Generic_OutParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(out TValueTarget value)
                {
                    TValue valueAdapted;
                    var result = _target.Invoke(out valueAdapted);
                    value = Imposter.Abstractions.TypeCaster.Cast<TValue, TValueTarget>(valueAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup()
                {
                    return _target.HasMatchingSetup();
                }

                IGeneric_OutParamMethodImposter<TValueTarget1, TResultTarget1>? IGeneric_OutParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out TValue value)
            {
                value = default(TValue);
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private Generic_OutParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public TResult Invoke(out TValue value)
            {
                var matchingSetup = FindMatchingSetup() ?? Generic_OutParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out value);
                    _generic_OutParamMethodInvocationHistoryCollection.Add(new Generic_OutParamMethodInvocationHistory<TValue, TResult>(result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _generic_OutParamMethodInvocationHistoryCollection.Add(new Generic_OutParamMethodInvocationHistory<TValue, TResult>(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGeneric_OutParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly Generic_OutParamMethodImposterCollection _imposterCollection;
                private readonly Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection;
                private Generic_OutParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(Generic_OutParamMethodImposterCollection _imposterCollection, Generic_OutParamMethodInvocationHistoryCollection _generic_OutParamMethodInvocationHistoryCollection)
                {
                    this._imposterCollection = _imposterCollection;
                    this._generic_OutParamMethodInvocationHistoryCollection = _generic_OutParamMethodInvocationHistoryCollection;
                }

                private Generic_OutParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Generic_OutParamMethodInvocationsSetup<TValue, TResult>();
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.Returns(Generic_OutParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.Throws(Generic_OutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.CallBefore(Generic_OutParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGeneric_OutParamMethodInvocationsSetup<TValue, TResult> IGeneric_OutParamMethodInvocationsSetup<TValue, TResult>.CallAfter(Generic_OutParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Generic_OutParamMethodInvocationVerifier<TValue, TResult>.Called(Count count)
                {
                    var invocationCount = _generic_OutParamMethodInvocationHistoryCollection.Count<TValue, TResult>();
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        public delegate TResult Generic_RefParamDelegate<TValue, TResult>(ref TValue value);
        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        public delegate void Generic_RefParamCallbackDelegate<TValue, TResult>(ref TValue value);
        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        public delegate System.Exception Generic_RefParamExceptionGeneratorDelegate<TValue, TResult>(ref TValue value);
        public class Generic_RefParamArguments<TValue, TResult>
        {
            public TValue value { get; }

            public Generic_RefParamArguments(TValue value)
            {
                this.value = value;
            }

            public Generic_RefParamArguments<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new Generic_RefParamArguments<TValueTarget, TResultTarget>(TypeCaster.Cast<TValue, TValueTarget>(value));
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Generic_RefParamArgumentsCriteria<TValue, TResult>
        {
            public Arg<TValue> value { get; }

            public Generic_RefParamArgumentsCriteria(Arg<TValue> value)
            {
                this.value = value;
            }

            public bool Matches(Generic_RefParamArguments<TValue, TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public Generic_RefParamArgumentsCriteria<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new Generic_RefParamArgumentsCriteria<TValueTarget, TResultTarget>(Imposter.Abstractions.Arg<TValueTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget, TValue>(it, out TValue valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGeneric_RefParamMethodInvocationHistory
        {
            bool Matches<TValue, TResult>(Generic_RefParamArgumentsCriteria<TValue, TResult> criteria);
        }

        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        public class Generic_RefParamMethodInvocationHistory<TValue, TResult> : IGeneric_RefParamMethodInvocationHistory
        {
            public Generic_RefParamArguments<TValue, TResult> Arguments { get; }
            public TResult Result { get; }
            public System.Exception? Exception { get; }

            public Generic_RefParamMethodInvocationHistory(Generic_RefParamArguments<TValue, TResult> arguments, TResult result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TValueTarget, TResultTarget>(Generic_RefParamArgumentsCriteria<TValueTarget, TResultTarget> criteria)
            {
                return criteria.As<TValue, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_RefParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IGeneric_RefParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IGeneric_RefParamMethodInvocationHistory>();
            internal void Add(IGeneric_RefParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>(Generic_RefParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>(argumentsCriteria));
            }
        }

        internal class Generic_RefParamMethodImposterCollection
        {
            private readonly Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection;
            public Generic_RefParamMethodImposterCollection(Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection)
            {
                this._generic_RefParamMethodInvocationHistoryCollection = _generic_RefParamMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IGeneric_RefParamMethodImposter> _imposters = new ConcurrentStack<IGeneric_RefParamMethodImposter>();
            internal Generic_RefParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new Generic_RefParamMethodImposter<TValue, TResult>(_generic_RefParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGeneric_RefParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>(Generic_RefParamArguments<TValue, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Generic_RefParamMethodInvocationsSetup<TValue, TResult> : IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static Generic_RefParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new Generic_RefParamMethodInvocationsSetup<TValue, TResult>(new Generic_RefParamArgumentsCriteria<TValue, TResult>(Arg<TValue>.Any()));
            internal Generic_RefParamArgumentsCriteria<TValue, TResult> ArgumentsCriteria { get; }

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

            internal static TResult DefaultResultGenerator(ref TValue value)
            {
                return default(TResult);
            }

            public Generic_RefParamMethodInvocationsSetup(Generic_RefParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Returns(Generic_RefParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    return value;
                };
                return this;
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    throw ex;
                };
                return this;
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Throws(Generic_RefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    throw exceptionGenerator(ref value);
                };
                return this;
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> CallBefore(Generic_RefParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> CallAfter(Generic_RefParamCallbackDelegate<TValue, TResult> callback)
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

            public TResult Invoke(ref TValue value)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(ref value);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(ref value);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(ref value);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal Generic_RefParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal Generic_RefParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal Generic_RefParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>
        {
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Returns(Generic_RefParamDelegate<TValue, TResult> resultGenerator);
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value);
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception ex);
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> Throws(Generic_RefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> CallBefore(Generic_RefParamCallbackDelegate<TValue, TResult> callback);
            public IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> CallAfter(Generic_RefParamCallbackDelegate<TValue, TResult> callback);
        }

        internal interface IGeneric_RefParamMethodImposter
        {
            IGeneric_RefParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGeneric_RefParamMethodImposter<TValue, TResult> : IGeneric_RefParamMethodImposter
        {
            TResult Invoke(ref TValue value);
            bool HasMatchingSetup(Generic_RefParamArguments<TValue, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        public interface Generic_RefParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        public interface IGeneric_RefParamMethodImposterBuilder<TValue, TResult> : IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>, Generic_RefParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        // TResult IMethodSetupFeatureSut.Generic_RefParam<TValue, TResult>(ref TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_RefParamMethodImposter<TValue, TResult> : IGeneric_RefParamMethodImposter<TValue, TResult>
        {
            private readonly ConcurrentStack<Generic_RefParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new ConcurrentStack<Generic_RefParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection;
            public Generic_RefParamMethodImposter(Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection)
            {
                this._generic_RefParamMethodInvocationHistoryCollection = _generic_RefParamMethodInvocationHistoryCollection;
            }

            IGeneric_RefParamMethodImposter<TValueTarget, TResultTarget>? IGeneric_RefParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(TValueTarget) == typeof(TValue) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGeneric_RefParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly Generic_RefParamMethodImposter<TValue, TResult> _target;
                public Adapter(Generic_RefParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(ref TValueTarget value)
                {
                    TValue valueAdapted = Imposter.Abstractions.TypeCaster.Cast<TValueTarget, TValue>(value);
                    var result = _target.Invoke(ref valueAdapted);
                    value = Imposter.Abstractions.TypeCaster.Cast<TValue, TValueTarget>(valueAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(Generic_RefParamArguments<TValueTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue, TResult>());
                }

                IGeneric_RefParamMethodImposter<TValueTarget1, TResultTarget1>? IGeneric_RefParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(Generic_RefParamArguments<TValue, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Generic_RefParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup(Generic_RefParamArguments<TValue, TResult> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public TResult Invoke(ref TValue value)
            {
                var arguments = new Generic_RefParamArguments<TValue, TResult>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? Generic_RefParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ref value);
                    _generic_RefParamMethodInvocationHistoryCollection.Add(new Generic_RefParamMethodInvocationHistory<TValue, TResult>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _generic_RefParamMethodInvocationHistoryCollection.Add(new Generic_RefParamMethodInvocationHistory<TValue, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGeneric_RefParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly Generic_RefParamMethodImposterCollection _imposterCollection;
                private readonly Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection;
                private readonly Generic_RefParamArgumentsCriteria<TValue, TResult> _argumentsCriteria;
                private Generic_RefParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(Generic_RefParamMethodImposterCollection _imposterCollection, Generic_RefParamMethodInvocationHistoryCollection _generic_RefParamMethodInvocationHistoryCollection, Generic_RefParamArgumentsCriteria<TValue, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._generic_RefParamMethodInvocationHistoryCollection = _generic_RefParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Generic_RefParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Generic_RefParamMethodInvocationsSetup<TValue, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.Returns(Generic_RefParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.Throws(Generic_RefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.CallBefore(Generic_RefParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGeneric_RefParamMethodInvocationsSetup<TValue, TResult> IGeneric_RefParamMethodInvocationsSetup<TValue, TResult>.CallAfter(Generic_RefParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Generic_RefParamMethodInvocationVerifier<TValue, TResult>.Called(Count count)
                {
                    var invocationCount = _generic_RefParamMethodInvocationHistoryCollection.Count<TValue, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        public delegate TResult Generic_ParamsParamDelegate<TValue, TResult>(TValue[] value);
        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        public delegate void Generic_ParamsParamCallbackDelegate<TValue, TResult>(TValue[] value);
        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        public delegate System.Exception Generic_ParamsParamExceptionGeneratorDelegate<TValue, TResult>(TValue[] value);
        public class Generic_ParamsParamArguments<TValue, TResult>
        {
            public TValue[] value { get; }

            public Generic_ParamsParamArguments(TValue[] value)
            {
                this.value = value;
            }

            public Generic_ParamsParamArguments<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new Generic_ParamsParamArguments<TValueTarget, TResultTarget>(TypeCaster.Cast<TValue[], TValueTarget[]>(value));
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Generic_ParamsParamArgumentsCriteria<TValue, TResult>
        {
            public Arg<TValue[]> value { get; }

            public Generic_ParamsParamArgumentsCriteria(Arg<TValue[]> value)
            {
                this.value = value;
            }

            public bool Matches(Generic_ParamsParamArguments<TValue, TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public Generic_ParamsParamArgumentsCriteria<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new Generic_ParamsParamArgumentsCriteria<TValueTarget, TResultTarget>(Imposter.Abstractions.Arg<TValueTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget[], TValue[]>(it, out TValue[] valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGeneric_ParamsParamMethodInvocationHistory
        {
            bool Matches<TValue, TResult>(Generic_ParamsParamArgumentsCriteria<TValue, TResult> criteria);
        }

        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        public class Generic_ParamsParamMethodInvocationHistory<TValue, TResult> : IGeneric_ParamsParamMethodInvocationHistory
        {
            public Generic_ParamsParamArguments<TValue, TResult> Arguments { get; }
            public TResult Result { get; }
            public System.Exception? Exception { get; }

            public Generic_ParamsParamMethodInvocationHistory(Generic_ParamsParamArguments<TValue, TResult> arguments, TResult result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TValueTarget, TResultTarget>(Generic_ParamsParamArgumentsCriteria<TValueTarget, TResultTarget> criteria)
            {
                return criteria.As<TValue, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_ParamsParamMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IGeneric_ParamsParamMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IGeneric_ParamsParamMethodInvocationHistory>();
            internal void Add(IGeneric_ParamsParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>(Generic_ParamsParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>(argumentsCriteria));
            }
        }

        internal class Generic_ParamsParamMethodImposterCollection
        {
            private readonly Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection;
            public Generic_ParamsParamMethodImposterCollection(Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection)
            {
                this._generic_ParamsParamMethodInvocationHistoryCollection = _generic_ParamsParamMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IGeneric_ParamsParamMethodImposter> _imposters = new ConcurrentStack<IGeneric_ParamsParamMethodImposter>();
            internal Generic_ParamsParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new Generic_ParamsParamMethodImposter<TValue, TResult>(_generic_ParamsParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGeneric_ParamsParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>(Generic_ParamsParamArguments<TValue, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Generic_ParamsParamMethodInvocationsSetup<TValue, TResult> : IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static Generic_ParamsParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>(new Generic_ParamsParamArgumentsCriteria<TValue, TResult>(Arg<TValue[]>.Any()));
            internal Generic_ParamsParamArgumentsCriteria<TValue, TResult> ArgumentsCriteria { get; }

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

            internal static TResult DefaultResultGenerator(TValue[] value)
            {
                return default(TResult);
            }

            public Generic_ParamsParamMethodInvocationsSetup(Generic_ParamsParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Returns(Generic_ParamsParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    return value;
                };
                return this;
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    throw new TException();
                };
                return this;
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    throw ex;
                };
                return this;
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Throws(Generic_ParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> CallBefore(Generic_ParamsParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> CallAfter(Generic_ParamsParamCallbackDelegate<TValue, TResult> callback)
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

            public TResult Invoke(TValue[] value)
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
                internal Generic_ParamsParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal Generic_ParamsParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal Generic_ParamsParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>
        {
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Returns(Generic_ParamsParamDelegate<TValue, TResult> resultGenerator);
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value);
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception ex);
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> Throws(Generic_ParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> CallBefore(Generic_ParamsParamCallbackDelegate<TValue, TResult> callback);
            public IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> CallAfter(Generic_ParamsParamCallbackDelegate<TValue, TResult> callback);
        }

        internal interface IGeneric_ParamsParamMethodImposter
        {
            IGeneric_ParamsParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGeneric_ParamsParamMethodImposter<TValue, TResult> : IGeneric_ParamsParamMethodImposter
        {
            TResult Invoke(TValue[] value);
            bool HasMatchingSetup(Generic_ParamsParamArguments<TValue, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        public interface Generic_ParamsParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        public interface IGeneric_ParamsParamMethodImposterBuilder<TValue, TResult> : IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>, Generic_ParamsParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        // TResult IMethodSetupFeatureSut.Generic_ParamsParam<TValue, TResult>(params TValue[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_ParamsParamMethodImposter<TValue, TResult> : IGeneric_ParamsParamMethodImposter<TValue, TResult>
        {
            private readonly ConcurrentStack<Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new ConcurrentStack<Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection;
            public Generic_ParamsParamMethodImposter(Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection)
            {
                this._generic_ParamsParamMethodInvocationHistoryCollection = _generic_ParamsParamMethodInvocationHistoryCollection;
            }

            IGeneric_ParamsParamMethodImposter<TValueTarget, TResultTarget>? IGeneric_ParamsParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(TValueTarget[]).IsAssignableTo(typeof(TValue[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGeneric_ParamsParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly Generic_ParamsParamMethodImposter<TValue, TResult> _target;
                public Adapter(Generic_ParamsParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(TValueTarget[] value)
                {
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TValueTarget[], TValue[]>(value));
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(Generic_ParamsParamArguments<TValueTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue, TResult>());
                }

                IGeneric_ParamsParamMethodImposter<TValueTarget1, TResultTarget1>? IGeneric_ParamsParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(Generic_ParamsParamArguments<TValue, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup(Generic_ParamsParamArguments<TValue, TResult> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public TResult Invoke(TValue[] value)
            {
                var arguments = new Generic_ParamsParamArguments<TValue, TResult>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value);
                    _generic_ParamsParamMethodInvocationHistoryCollection.Add(new Generic_ParamsParamMethodInvocationHistory<TValue, TResult>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _generic_ParamsParamMethodInvocationHistoryCollection.Add(new Generic_ParamsParamMethodInvocationHistory<TValue, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGeneric_ParamsParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly Generic_ParamsParamMethodImposterCollection _imposterCollection;
                private readonly Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection;
                private readonly Generic_ParamsParamArgumentsCriteria<TValue, TResult> _argumentsCriteria;
                private Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(Generic_ParamsParamMethodImposterCollection _imposterCollection, Generic_ParamsParamMethodInvocationHistoryCollection _generic_ParamsParamMethodInvocationHistoryCollection, Generic_ParamsParamArgumentsCriteria<TValue, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._generic_ParamsParamMethodInvocationHistoryCollection = _generic_ParamsParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Generic_ParamsParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Generic_ParamsParamMethodInvocationsSetup<TValue, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(Generic_ParamsParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(Generic_ParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.CallBefore(Generic_ParamsParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult> IGeneric_ParamsParamMethodInvocationsSetup<TValue, TResult>.CallAfter(Generic_ParamsParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Generic_ParamsParamMethodInvocationVerifier<TValue, TResult>.Called(Count count)
                {
                    var invocationCount = _generic_ParamsParamMethodInvocationHistoryCollection.Count<TValue, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate TResult Generic_AllRefKindDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate void Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate System.Exception Generic_AllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        public class Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult>
        {
            public TRef refValue { get; }
            public TIn inValue { get; }
            public TParams[] paramsValues { get; }

            public Generic_AllRefKindArguments(TRef refValue, TIn inValue, TParams[] paramsValues)
            {
                this.refValue = refValue;
                this.inValue = inValue;
                this.paramsValues = paramsValues;
            }

            public Generic_AllRefKindArguments<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                return new Generic_AllRefKindArguments<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(TypeCaster.Cast<TRef, TRefTarget>(refValue), TypeCaster.Cast<TIn, TInTarget>(inValue), TypeCaster.Cast<TParams[], TParamsTarget[]>(paramsValues));
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>
        {
            public OutArg<TOut> outValue { get; }
            public Arg<TRef> refValue { get; }
            public Arg<TIn> inValue { get; }
            public Arg<TParams[]> paramsValues { get; }

            public Generic_AllRefKindArgumentsCriteria(OutArg<TOut> outValue, Arg<TRef> refValue, Arg<TIn> inValue, Arg<TParams[]> paramsValues)
            {
                this.outValue = outValue;
                this.refValue = refValue;
                this.inValue = inValue;
                this.paramsValues = paramsValues;
            }

            public bool Matches(Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return refValue.Matches(arguments.refValue) && inValue.Matches(arguments.inValue) && paramsValues.Matches(arguments.paramsValues);
            }

            public Generic_AllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                return new Generic_AllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(Imposter.Abstractions.OutArg<TOutTarget>.Any(), Imposter.Abstractions.Arg<TRefTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TRefTarget, TRef>(it, out TRef refValueTarget) && refValue.Matches(refValueTarget)), Imposter.Abstractions.Arg<TInTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TInTarget, TIn>(it, out TIn inValueTarget) && inValue.Matches(inValueTarget)), Imposter.Abstractions.Arg<TParamsTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TParamsTarget[], TParams[]>(it, out TParams[] paramsValuesTarget) && paramsValues.Matches(paramsValuesTarget)));
            }
        }

        public interface IGeneric_AllRefKindMethodInvocationHistory
        {
            bool Matches<TOut, TRef, TIn, TParams, TResult>(Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> criteria);
        }

        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public class Generic_AllRefKindMethodInvocationHistory<TOut, TRef, TIn, TParams, TResult> : IGeneric_AllRefKindMethodInvocationHistory
        {
            public Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> Arguments { get; }
            public TResult Result { get; }
            public System.Exception? Exception { get; }

            public Generic_AllRefKindMethodInvocationHistory(Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments, TResult result, System.Exception? exception = default(System.Exception? ))
            {
                Arguments = arguments;
                Result = result;
                Exception = exception;
            }

            public bool Matches<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(Generic_AllRefKindArgumentsCriteria<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> criteria)
            {
                return criteria.As<TOut, TRef, TIn, TParams, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_AllRefKindMethodInvocationHistoryCollection
        {
            private readonly ConcurrentStack<IGeneric_AllRefKindMethodInvocationHistory> _invocationHistory = new ConcurrentStack<IGeneric_AllRefKindMethodInvocationHistory>();
            internal void Add(IGeneric_AllRefKindMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TOut, TRef, TIn, TParams, TResult>(Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TOut, TRef, TIn, TParams, TResult>(argumentsCriteria));
            }
        }

        internal class Generic_AllRefKindMethodImposterCollection
        {
            private readonly Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection;
            public Generic_AllRefKindMethodImposterCollection(Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection)
            {
                this._generic_AllRefKindMethodInvocationHistoryCollection = _generic_AllRefKindMethodInvocationHistoryCollection;
            }

            private readonly ConcurrentStack<IGeneric_AllRefKindMethodImposter> _imposters = new ConcurrentStack<IGeneric_AllRefKindMethodImposter>();
            internal Generic_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> AddNew<TOut, TRef, TIn, TParams, TResult>()
            {
                var imposter = new Generic_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>(_generic_AllRefKindMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGeneric_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> GetImposterWithMatchingSetup<TOut, TRef, TIn, TParams, TResult>(Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TOut, TRef, TIn, TParams, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TOut, TRef, TIn, TParams, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> : IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>
        {
            internal static Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> DefaultInvocationSetup = new Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>(new Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(OutArg<TOut>.Any(), Arg<TRef>.Any(), Arg<TIn>.Any(), Arg<TParams[]>.Any()));
            internal Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> ArgumentsCriteria { get; }

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
                return default(TResult);
            }

            public Generic_AllRefKindMethodInvocationsSetup(Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TOut outValue)
            {
                outValue = default(TOut);
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Returns(Generic_AllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Returns(TResult value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    return value;
                };
                return this;
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws<TException>()
                where TException : Exception, new()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw new TException();
                };
                return this;
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws(System.Exception ex)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw ex;
                };
                return this;
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws(Generic_AllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues) =>
                {
                    throw exceptionGenerator(out outValue, ref refValue, in inValue, paramsValues);
                };
                return this;
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> CallBefore(Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> CallAfter(Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
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
                internal Generic_AllRefKindDelegate<TOut, TRef, TIn, TParams, TResult>? ResultGenerator { get; set; }
                internal Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>? CallBefore { get; set; }
                internal Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>
        {
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Returns(Generic_AllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator);
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Returns(TResult value);
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws<TException>()
                where TException : Exception, new();
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws(System.Exception ex);
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> Throws(Generic_AllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator);
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> CallBefore(Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback);
            public IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> CallAfter(Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback);
        }

        internal interface IGeneric_AllRefKindMethodImposter
        {
            IGeneric_AllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>? As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGeneric_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> : IGeneric_AllRefKindMethodImposter
        {
            TResult Invoke(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
            bool HasMatchingSetup(Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public interface Generic_AllRefKindMethodInvocationVerifier<TOut, TRef, TIn, TParams, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public interface IGeneric_AllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> : IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>, Generic_AllRefKindMethodInvocationVerifier<TOut, TRef, TIn, TParams, TResult>
        {
        }

        // TResult IMethodSetupFeatureSut.Generic_AllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Generic_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> : IGeneric_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>
        {
            private readonly ConcurrentStack<Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>> _invocationSetups = new ConcurrentStack<Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>>();
            private readonly Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection;
            public Generic_AllRefKindMethodImposter(Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection)
            {
                this._generic_AllRefKindMethodInvocationHistoryCollection = _generic_AllRefKindMethodInvocationHistoryCollection;
            }

            IGeneric_AllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>? IGeneric_AllRefKindMethodImposter.As<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>()
            {
                if (typeof(TOut).IsAssignableTo(typeof(TOutTarget)) && typeof(TRefTarget) == typeof(TRef) && typeof(TInTarget).IsAssignableTo(typeof(TIn)) && typeof(TParamsTarget[]).IsAssignableTo(typeof(TParams[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> : IGeneric_AllRefKindMethodImposter<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget>
            {
                private readonly Generic_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> _target;
                public Adapter(Generic_AllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult> target)
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

                public bool HasMatchingSetup(Generic_AllRefKindArguments<TOutTarget, TRefTarget, TInTarget, TParamsTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TOut, TRef, TIn, TParams, TResult>());
                }

                IGeneric_AllRefKindMethodImposter<TOutTarget1, TRefTarget1, TInTarget1, TParamsTarget1, TResultTarget1>? IGeneric_AllRefKindMethodImposter.As<TOutTarget1, TRefTarget1, TInTarget1, TParamsTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out TOut outValue)
            {
                outValue = default(TOut);
            }

            public bool HasMatchingSetup(Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>? FindMatchingSetup(Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult> arguments)
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
                var arguments = new Generic_AllRefKindArguments<TOut, TRef, TIn, TParams, TResult>(refValue, inValue, paramsValues);
                var matchingSetup = FindMatchingSetup(arguments) ?? Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out outValue, ref refValue, in inValue, paramsValues);
                    _generic_AllRefKindMethodInvocationHistoryCollection.Add(new Generic_AllRefKindMethodInvocationHistory<TOut, TRef, TIn, TParams, TResult>(arguments, result, default));
                    return result;
                }
                catch (Exception ex)
                {
                    _generic_AllRefKindMethodInvocationHistoryCollection.Add(new Generic_AllRefKindMethodInvocationHistory<TOut, TRef, TIn, TParams, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGeneric_AllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult>
            {
                private readonly Generic_AllRefKindMethodImposterCollection _imposterCollection;
                private readonly Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection;
                private readonly Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> _argumentsCriteria;
                private Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>? _existingInvocationSetup;
                public Builder(Generic_AllRefKindMethodImposterCollection _imposterCollection, Generic_AllRefKindMethodInvocationHistoryCollection _generic_AllRefKindMethodInvocationHistoryCollection, Generic_AllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._generic_AllRefKindMethodInvocationHistoryCollection = _generic_AllRefKindMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new Generic_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TOut, TRef, TIn, TParams, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Returns(Generic_AllRefKindDelegate<TOut, TRef, TIn, TParams, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Returns(TResult value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws(System.Exception ex)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(ex);
                    return invocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.Throws(Generic_AllRefKindExceptionGeneratorDelegate<TOut, TRef, TIn, TParams, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.CallBefore(Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult> IGeneric_AllRefKindMethodInvocationsSetup<TOut, TRef, TIn, TParams, TResult>.CallAfter(Generic_AllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void Generic_AllRefKindMethodInvocationVerifier<TOut, TRef, TIn, TParams, TResult>.Called(Count count)
                {
                    var invocationCount = _generic_AllRefKindMethodInvocationHistoryCollection.Count<TOut, TRef, TIn, TParams, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new VerificationFailedException("TODO");
                    }
                }
            }
        }
    }
}