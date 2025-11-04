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
        private readonly VoidNoParamsMethodImposter _voidNoParamsMethodImposter;
        private readonly IntNoParamsMethodImposter _intNoParamsMethodImposter;
        private readonly IntSingleParamMethodImposter _intSingleParamMethodImposter;
        private readonly IntParamsMethodImposter _intParamsMethodImposter;
        private readonly IntOutParamMethodImposter _intOutParamMethodImposter;
        private readonly IntRefParamMethodImposter _intRefParamMethodImposter;
        private readonly IntParamsParamMethodImposter _intParamsParamMethodImposter;
        private readonly IntInParamMethodImposter _intInParamMethodImposter;
        private readonly IntAllRefKindsMethodImposter _intAllRefKindsMethodImposter;
        private readonly GenericSingleParamMethodImposterCollection _genericSingleParamMethodImposterCollection;
        private readonly GenericInnerSingleParamMethodImposterCollection _genericInnerSingleParamMethodImposterCollection;
        private readonly GenericOutParamMethodImposterCollection _genericOutParamMethodImposterCollection;
        private readonly GenericInnerOutParamMethodImposterCollection _genericInnerOutParamMethodImposterCollection;
        private readonly GenericRefParamMethodImposterCollection _genericRefParamMethodImposterCollection;
        private readonly GenericInnerRefParamMethodImposterCollection _genericInnerRefParamMethodImposterCollection;
        private readonly GenericParamsParamMethodImposterCollection _genericParamsParamMethodImposterCollection;
        private readonly GenericInnerParamsParamMethodImposterCollection _genericInnerParamsParamMethodImposterCollection;
        private readonly GenericAllRefKindMethodImposterCollection _genericAllRefKindMethodImposterCollection;
        private readonly AsyncTaskIntNoParamsMethodImposter _asyncTaskIntNoParamsMethodImposter;
        private readonly VoidNoParamsMethodInvocationHistoryCollection _voidNoParamsMethodInvocationHistoryCollection = new VoidNoParamsMethodInvocationHistoryCollection();
        private readonly IntNoParamsMethodInvocationHistoryCollection _intNoParamsMethodInvocationHistoryCollection = new IntNoParamsMethodInvocationHistoryCollection();
        private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection = new IntSingleParamMethodInvocationHistoryCollection();
        private readonly IntParamsMethodInvocationHistoryCollection _intParamsMethodInvocationHistoryCollection = new IntParamsMethodInvocationHistoryCollection();
        private readonly IntOutParamMethodInvocationHistoryCollection _intOutParamMethodInvocationHistoryCollection = new IntOutParamMethodInvocationHistoryCollection();
        private readonly IntRefParamMethodInvocationHistoryCollection _intRefParamMethodInvocationHistoryCollection = new IntRefParamMethodInvocationHistoryCollection();
        private readonly IntParamsParamMethodInvocationHistoryCollection _intParamsParamMethodInvocationHistoryCollection = new IntParamsParamMethodInvocationHistoryCollection();
        private readonly IntInParamMethodInvocationHistoryCollection _intInParamMethodInvocationHistoryCollection = new IntInParamMethodInvocationHistoryCollection();
        private readonly IntAllRefKindsMethodInvocationHistoryCollection _intAllRefKindsMethodInvocationHistoryCollection = new IntAllRefKindsMethodInvocationHistoryCollection();
        private readonly GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection = new GenericSingleParamMethodInvocationHistoryCollection();
        private readonly GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection = new GenericInnerSingleParamMethodInvocationHistoryCollection();
        private readonly GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection = new GenericOutParamMethodInvocationHistoryCollection();
        private readonly GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection = new GenericInnerOutParamMethodInvocationHistoryCollection();
        private readonly GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection = new GenericRefParamMethodInvocationHistoryCollection();
        private readonly GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection = new GenericInnerRefParamMethodInvocationHistoryCollection();
        private readonly GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection = new GenericParamsParamMethodInvocationHistoryCollection();
        private readonly GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection = new GenericInnerParamsParamMethodInvocationHistoryCollection();
        private readonly GenericAllRefKindMethodInvocationHistoryCollection _genericAllRefKindMethodInvocationHistoryCollection = new GenericAllRefKindMethodInvocationHistoryCollection();
        private readonly AsyncTaskIntNoParamsMethodInvocationHistoryCollection _asyncTaskIntNoParamsMethodInvocationHistoryCollection = new AsyncTaskIntNoParamsMethodInvocationHistoryCollection();
        public IVoidNoParamsMethodImposterBuilder VoidNoParams()
        {
            return new VoidNoParamsMethodImposter.Builder(_voidNoParamsMethodImposter, _voidNoParamsMethodInvocationHistoryCollection);
        }

        public IIntNoParamsMethodImposterBuilder IntNoParams()
        {
            return new IntNoParamsMethodImposter.Builder(_intNoParamsMethodImposter, _intNoParamsMethodInvocationHistoryCollection);
        }

        public IIntSingleParamMethodImposterBuilder IntSingleParam(Imposter.Abstractions.Arg<int> age)
        {
            return new IntSingleParamMethodImposter.Builder(_intSingleParamMethodImposter, _intSingleParamMethodInvocationHistoryCollection, new IntSingleParamArgumentsCriteria(age));
        }

        public IIntParamsMethodImposterBuilder IntParams(Imposter.Abstractions.Arg<int> age, Imposter.Abstractions.Arg<string> name, Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> regex)
        {
            return new IntParamsMethodImposter.Builder(_intParamsMethodImposter, _intParamsMethodInvocationHistoryCollection, new IntParamsArgumentsCriteria(age, name, regex));
        }

        public IIntOutParamMethodImposterBuilder IntOutParam(Imposter.Abstractions.OutArg<int> outValue)
        {
            return new IntOutParamMethodImposter.Builder(_intOutParamMethodImposter, _intOutParamMethodInvocationHistoryCollection);
        }

        public IIntRefParamMethodImposterBuilder IntRefParam(Imposter.Abstractions.Arg<int> refValue)
        {
            return new IntRefParamMethodImposter.Builder(_intRefParamMethodImposter, _intRefParamMethodInvocationHistoryCollection, new IntRefParamArgumentsCriteria(refValue));
        }

        public IIntParamsParamMethodImposterBuilder IntParamsParam(Imposter.Abstractions.Arg<string[]> paramsStrings)
        {
            return new IntParamsParamMethodImposter.Builder(_intParamsParamMethodImposter, _intParamsParamMethodInvocationHistoryCollection, new IntParamsParamArgumentsCriteria(paramsStrings));
        }

        public IIntInParamMethodImposterBuilder IntInParam(Imposter.Abstractions.Arg<string> inStringValue)
        {
            return new IntInParamMethodImposter.Builder(_intInParamMethodImposter, _intInParamMethodInvocationHistoryCollection, new IntInParamArgumentsCriteria(inStringValue));
        }

        public IIntAllRefKindsMethodImposterBuilder IntAllRefKinds(Imposter.Abstractions.OutArg<int> value, Imposter.Abstractions.Arg<int> refValue, Imposter.Abstractions.Arg<int> inValue, Imposter.Abstractions.Arg<string> valueAsString, Imposter.Abstractions.Arg<string[]> paramsStrings)
        {
            return new IntAllRefKindsMethodImposter.Builder(_intAllRefKindsMethodImposter, _intAllRefKindsMethodInvocationHistoryCollection, new IntAllRefKindsArgumentsCriteria(value, refValue, inValue, valueAsString, paramsStrings));
        }

        public IGenericSingleParamMethodImposterBuilder<TValue> GenericSingleParam<TValue>(Imposter.Abstractions.Arg<TValue> value)
        {
            return new GenericSingleParamMethodImposter<TValue>.Builder(_genericSingleParamMethodImposterCollection, _genericSingleParamMethodInvocationHistoryCollection, new GenericSingleParamArgumentsCriteria<TValue>(value));
        }

        public IGenericInnerSingleParamMethodImposterBuilder<TValue> GenericInnerSingleParam<TValue>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>> value)
        {
            return new GenericInnerSingleParamMethodImposter<TValue>.Builder(_genericInnerSingleParamMethodImposterCollection, _genericInnerSingleParamMethodInvocationHistoryCollection, new GenericInnerSingleParamArgumentsCriteria<TValue>(value));
        }

        public IGenericOutParamMethodImposterBuilder<TValue, TResult> GenericOutParam<TValue, TResult>(Imposter.Abstractions.OutArg<TValue> value)
        {
            return new GenericOutParamMethodImposter<TValue, TResult>.Builder(_genericOutParamMethodImposterCollection, _genericOutParamMethodInvocationHistoryCollection);
        }

        public IGenericInnerOutParamMethodImposterBuilder<TValue, TResult> GenericInnerOutParam<TValue, TResult>(Imposter.Abstractions.OutArg<global::System.Collections.Generic.List<TValue>> value)
        {
            return new GenericInnerOutParamMethodImposter<TValue, TResult>.Builder(_genericInnerOutParamMethodImposterCollection, _genericInnerOutParamMethodInvocationHistoryCollection);
        }

        public IGenericRefParamMethodImposterBuilder<TValue, TResult> GenericRefParam<TValue, TResult>(Imposter.Abstractions.Arg<TValue> value)
        {
            return new GenericRefParamMethodImposter<TValue, TResult>.Builder(_genericRefParamMethodImposterCollection, _genericRefParamMethodInvocationHistoryCollection, new GenericRefParamArgumentsCriteria<TValue, TResult>(value));
        }

        public IGenericInnerRefParamMethodImposterBuilder<TValue, TResult> GenericInnerRefParam<TValue, TResult>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>> value)
        {
            return new GenericInnerRefParamMethodImposter<TValue, TResult>.Builder(_genericInnerRefParamMethodImposterCollection, _genericInnerRefParamMethodInvocationHistoryCollection, new GenericInnerRefParamArgumentsCriteria<TValue, TResult>(value));
        }

        public IGenericParamsParamMethodImposterBuilder<TValue, TResult> GenericParamsParam<TValue, TResult>(Imposter.Abstractions.Arg<TValue[]> value)
        {
            return new GenericParamsParamMethodImposter<TValue, TResult>.Builder(_genericParamsParamMethodImposterCollection, _genericParamsParamMethodInvocationHistoryCollection, new GenericParamsParamArgumentsCriteria<TValue, TResult>(value));
        }

        public IGenericInnerParamsParamMethodImposterBuilder<TValue, TResult> GenericInnerParamsParam<TValue, TResult>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>[]> value)
        {
            return new GenericInnerParamsParamMethodImposter<TValue, TResult>.Builder(_genericInnerParamsParamMethodImposterCollection, _genericInnerParamsParamMethodInvocationHistoryCollection, new GenericInnerParamsParamArgumentsCriteria<TValue, TResult>(value));
        }

        public IGenericAllRefKindMethodImposterBuilder<TOut, TRef, TIn, TParams, TResult> GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(Imposter.Abstractions.OutArg<TOut> outValue, Imposter.Abstractions.Arg<TRef> refValue, Imposter.Abstractions.Arg<TIn> inValue, Imposter.Abstractions.Arg<TParams[]> paramsValues)
        {
            return new GenericAllRefKindMethodImposter<TOut, TRef, TIn, TParams, TResult>.Builder(_genericAllRefKindMethodImposterCollection, _genericAllRefKindMethodInvocationHistoryCollection, new GenericAllRefKindArgumentsCriteria<TOut, TRef, TIn, TParams, TResult>(outValue, refValue, inValue, paramsValues));
        }

        public IAsyncTaskIntNoParamsMethodImposterBuilder AsyncTaskIntNoParams()
        {
            return new AsyncTaskIntNoParamsMethodImposter.Builder(_asyncTaskIntNoParamsMethodImposter, _asyncTaskIntNoParamsMethodInvocationHistoryCollection);
        }

        private ImposterTargetInstance _imposterInstance;
        public IMethodSetupFeatureSutImposter()
        {
            this._voidNoParamsMethodImposter = new VoidNoParamsMethodImposter(_voidNoParamsMethodInvocationHistoryCollection);
            this._intNoParamsMethodImposter = new IntNoParamsMethodImposter(_intNoParamsMethodInvocationHistoryCollection);
            this._intSingleParamMethodImposter = new IntSingleParamMethodImposter(_intSingleParamMethodInvocationHistoryCollection);
            this._intParamsMethodImposter = new IntParamsMethodImposter(_intParamsMethodInvocationHistoryCollection);
            this._intOutParamMethodImposter = new IntOutParamMethodImposter(_intOutParamMethodInvocationHistoryCollection);
            this._intRefParamMethodImposter = new IntRefParamMethodImposter(_intRefParamMethodInvocationHistoryCollection);
            this._intParamsParamMethodImposter = new IntParamsParamMethodImposter(_intParamsParamMethodInvocationHistoryCollection);
            this._intInParamMethodImposter = new IntInParamMethodImposter(_intInParamMethodInvocationHistoryCollection);
            this._intAllRefKindsMethodImposter = new IntAllRefKindsMethodImposter(_intAllRefKindsMethodInvocationHistoryCollection);
            this._genericSingleParamMethodImposterCollection = new GenericSingleParamMethodImposterCollection(_genericSingleParamMethodInvocationHistoryCollection);
            this._genericInnerSingleParamMethodImposterCollection = new GenericInnerSingleParamMethodImposterCollection(_genericInnerSingleParamMethodInvocationHistoryCollection);
            this._genericOutParamMethodImposterCollection = new GenericOutParamMethodImposterCollection(_genericOutParamMethodInvocationHistoryCollection);
            this._genericInnerOutParamMethodImposterCollection = new GenericInnerOutParamMethodImposterCollection(_genericInnerOutParamMethodInvocationHistoryCollection);
            this._genericRefParamMethodImposterCollection = new GenericRefParamMethodImposterCollection(_genericRefParamMethodInvocationHistoryCollection);
            this._genericInnerRefParamMethodImposterCollection = new GenericInnerRefParamMethodImposterCollection(_genericInnerRefParamMethodInvocationHistoryCollection);
            this._genericParamsParamMethodImposterCollection = new GenericParamsParamMethodImposterCollection(_genericParamsParamMethodInvocationHistoryCollection);
            this._genericInnerParamsParamMethodImposterCollection = new GenericInnerParamsParamMethodImposterCollection(_genericInnerParamsParamMethodInvocationHistoryCollection);
            this._genericAllRefKindMethodImposterCollection = new GenericAllRefKindMethodImposterCollection(_genericAllRefKindMethodInvocationHistoryCollection);
            this._asyncTaskIntNoParamsMethodImposter = new AsyncTaskIntNoParamsMethodImposter(_asyncTaskIntNoParamsMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodSetupFeatureSut.VoidNoParams()
        public delegate void VoidNoParamsDelegate();
        // void IMethodSetupFeatureSut.VoidNoParams()
        public delegate void VoidNoParamsCallbackDelegate();
        // void IMethodSetupFeatureSut.VoidNoParams()
        public delegate System.Exception VoidNoParamsExceptionGeneratorDelegate();
        public interface IVoidNoParamsMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class VoidNoParamsMethodInvocationHistory : IVoidNoParamsMethodInvocationHistory
        {
            internal System.Exception Exception;
            public VoidNoParamsMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class VoidNoParamsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IVoidNoParamsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IVoidNoParamsMethodInvocationHistory>();
            internal void Add(IVoidNoParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodSetupFeatureSut.VoidNoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class VoidNoParamsMethodInvocationsSetup : IVoidNoParamsMethodInvocationsSetup
        {
            internal static VoidNoParamsMethodInvocationsSetup DefaultInvocationSetup = new VoidNoParamsMethodInvocationsSetup();
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

            public VoidNoParamsMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exception;
                };
                return this;
            }

            IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.Throws(VoidNoParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.CallBefore(VoidNoParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.CallAfter(VoidNoParamsCallbackDelegate callback)
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
                internal VoidNoParamsDelegate? ResultGenerator { get; set; }
                internal VoidNoParamsCallbackDelegate? CallBefore { get; set; }
                internal VoidNoParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IVoidNoParamsMethodInvocationsSetup
        {
            IVoidNoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IVoidNoParamsMethodInvocationsSetup Throws(System.Exception exception);
            IVoidNoParamsMethodInvocationsSetup Throws(VoidNoParamsExceptionGeneratorDelegate exceptionGenerator);
            IVoidNoParamsMethodInvocationsSetup CallBefore(VoidNoParamsCallbackDelegate callback);
            IVoidNoParamsMethodInvocationsSetup CallAfter(VoidNoParamsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface VoidNoParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.VoidNoParams()
        public interface IVoidNoParamsMethodImposterBuilder : IVoidNoParamsMethodInvocationsSetup, VoidNoParamsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class VoidNoParamsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<VoidNoParamsMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<VoidNoParamsMethodInvocationsSetup>();
            private readonly VoidNoParamsMethodInvocationHistoryCollection _voidNoParamsMethodInvocationHistoryCollection;
            public VoidNoParamsMethodImposter(VoidNoParamsMethodInvocationHistoryCollection _voidNoParamsMethodInvocationHistoryCollection)
            {
                this._voidNoParamsMethodInvocationHistoryCollection = _voidNoParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private VoidNoParamsMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public void Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? VoidNoParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke();
                    _voidNoParamsMethodInvocationHistoryCollection.Add(new VoidNoParamsMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _voidNoParamsMethodInvocationHistoryCollection.Add(new VoidNoParamsMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IVoidNoParamsMethodImposterBuilder
            {
                private readonly VoidNoParamsMethodImposter _imposter;
                private readonly VoidNoParamsMethodInvocationHistoryCollection _voidNoParamsMethodInvocationHistoryCollection;
                private VoidNoParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(VoidNoParamsMethodImposter _imposter, VoidNoParamsMethodInvocationHistoryCollection _voidNoParamsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._voidNoParamsMethodInvocationHistoryCollection = _voidNoParamsMethodInvocationHistoryCollection;
                }

                private IVoidNoParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new VoidNoParamsMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.Throws(VoidNoParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.CallBefore(VoidNoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IVoidNoParamsMethodInvocationsSetup IVoidNoParamsMethodInvocationsSetup.CallAfter(VoidNoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void VoidNoParamsMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _voidNoParamsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntNoParams()
        public delegate int IntNoParamsDelegate();
        // int IMethodSetupFeatureSut.IntNoParams()
        public delegate void IntNoParamsCallbackDelegate();
        // int IMethodSetupFeatureSut.IntNoParams()
        public delegate System.Exception IntNoParamsExceptionGeneratorDelegate();
        public interface IIntNoParamsMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntNoParamsMethodInvocationHistory : IIntNoParamsMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public IntNoParamsMethodInvocationHistory(int Result, System.Exception Exception)
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
        internal class IntNoParamsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntNoParamsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntNoParamsMethodInvocationHistory>();
            internal void Add(IIntNoParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodSetupFeatureSut.IntNoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntNoParamsMethodInvocationsSetup : IIntNoParamsMethodInvocationsSetup
        {
            internal static IntNoParamsMethodInvocationsSetup DefaultInvocationSetup = new IntNoParamsMethodInvocationsSetup();
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

            public IntNoParamsMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Returns(IntNoParamsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    return value;
                };
                return this;
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exception;
                };
                return this;
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Throws(IntNoParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.CallBefore(IntNoParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.CallAfter(IntNoParamsCallbackDelegate callback)
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
                internal IntNoParamsDelegate? ResultGenerator { get; set; }
                internal IntNoParamsCallbackDelegate? CallBefore { get; set; }
                internal IntNoParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntNoParamsMethodInvocationsSetup
        {
            IIntNoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntNoParamsMethodInvocationsSetup Throws(System.Exception exception);
            IIntNoParamsMethodInvocationsSetup Throws(IntNoParamsExceptionGeneratorDelegate exceptionGenerator);
            IIntNoParamsMethodInvocationsSetup CallBefore(IntNoParamsCallbackDelegate callback);
            IIntNoParamsMethodInvocationsSetup CallAfter(IntNoParamsCallbackDelegate callback);
            IIntNoParamsMethodInvocationsSetup Returns(IntNoParamsDelegate resultGenerator);
            IIntNoParamsMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntNoParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntNoParams()
        public interface IIntNoParamsMethodImposterBuilder : IIntNoParamsMethodInvocationsSetup, IntNoParamsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntNoParamsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntNoParamsMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntNoParamsMethodInvocationsSetup>();
            private readonly IntNoParamsMethodInvocationHistoryCollection _intNoParamsMethodInvocationHistoryCollection;
            public IntNoParamsMethodImposter(IntNoParamsMethodInvocationHistoryCollection _intNoParamsMethodInvocationHistoryCollection)
            {
                this._intNoParamsMethodInvocationHistoryCollection = _intNoParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private IntNoParamsMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? IntNoParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke();
                    _intNoParamsMethodInvocationHistoryCollection.Add(new IntNoParamsMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intNoParamsMethodInvocationHistoryCollection.Add(new IntNoParamsMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntNoParamsMethodImposterBuilder
            {
                private readonly IntNoParamsMethodImposter _imposter;
                private readonly IntNoParamsMethodInvocationHistoryCollection _intNoParamsMethodInvocationHistoryCollection;
                private IntNoParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntNoParamsMethodImposter _imposter, IntNoParamsMethodInvocationHistoryCollection _intNoParamsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._intNoParamsMethodInvocationHistoryCollection = _intNoParamsMethodInvocationHistoryCollection;
                }

                private IIntNoParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntNoParamsMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Throws(IntNoParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.CallBefore(IntNoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.CallAfter(IntNoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Returns(IntNoParamsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntNoParamsMethodInvocationsSetup IIntNoParamsMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntNoParamsMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intNoParamsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntSingleParam(int age)
        public delegate int IntSingleParamDelegate(int age);
        // int IMethodSetupFeatureSut.IntSingleParam(int age)
        public delegate void IntSingleParamCallbackDelegate(int age);
        // int IMethodSetupFeatureSut.IntSingleParam(int age)
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

        // int IMethodSetupFeatureSut.IntSingleParam(int age)
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

        // int IMethodSetupFeatureSut.IntSingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntSingleParamMethodInvocationsSetup : IIntSingleParamMethodInvocationsSetup
        {
            internal static IntSingleParamMethodInvocationsSetup DefaultInvocationSetup = new IntSingleParamMethodInvocationsSetup(new IntSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal IntSingleParamArgumentsCriteria ArgumentsCriteria { get; }

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
                return default;
            }

            public IntSingleParamMethodInvocationsSetup(IntSingleParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Returns(IntSingleParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    return value;
                };
                return this;
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw exception;
                };
                return this;
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age) =>
                {
                    throw exceptionGenerator(age);
                };
                return this;
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.CallBefore(IntSingleParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.CallAfter(IntSingleParamCallbackDelegate callback)
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
                internal IntSingleParamDelegate? ResultGenerator { get; set; }
                internal IntSingleParamCallbackDelegate? CallBefore { get; set; }
                internal IntSingleParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntSingleParamMethodInvocationsSetup
        {
            IIntSingleParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntSingleParamMethodInvocationsSetup Throws(System.Exception exception);
            IIntSingleParamMethodInvocationsSetup Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator);
            IIntSingleParamMethodInvocationsSetup CallBefore(IntSingleParamCallbackDelegate callback);
            IIntSingleParamMethodInvocationsSetup CallAfter(IntSingleParamCallbackDelegate callback);
            IIntSingleParamMethodInvocationsSetup Returns(IntSingleParamDelegate resultGenerator);
            IIntSingleParamMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntSingleParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntSingleParam(int age)
        public interface IIntSingleParamMethodImposterBuilder : IIntSingleParamMethodInvocationsSetup, IntSingleParamMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntSingleParamMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntSingleParamMethodInvocationsSetup>();
            private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection;
            public IntSingleParamMethodImposter(IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection)
            {
                this._intSingleParamMethodInvocationHistoryCollection = _intSingleParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IntSingleParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IntSingleParamMethodInvocationsSetup? FindMatchingSetup(IntSingleParamArguments arguments)
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
                var arguments = new IntSingleParamArguments(age);
                var matchingSetup = FindMatchingSetup(arguments) ?? IntSingleParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age);
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
                private IntSingleParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntSingleParamMethodImposter _imposter, IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection, IntSingleParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intSingleParamMethodInvocationHistoryCollection = _intSingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIntSingleParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntSingleParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.CallBefore(IntSingleParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.CallAfter(IntSingleParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Returns(IntSingleParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntSingleParamMethodInvocationsSetup IIntSingleParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntSingleParamMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intSingleParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntParams(int age, string name, Regex regex)
        public delegate int IntParamsDelegate(int age, string name, global::System.Text.RegularExpressions.Regex regex);
        // int IMethodSetupFeatureSut.IntParams(int age, string name, Regex regex)
        public delegate void IntParamsCallbackDelegate(int age, string name, global::System.Text.RegularExpressions.Regex regex);
        // int IMethodSetupFeatureSut.IntParams(int age, string name, Regex regex)
        public delegate System.Exception IntParamsExceptionGeneratorDelegate(int age, string name, global::System.Text.RegularExpressions.Regex regex);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntParamsArguments
        {
            public int age;
            public string name;
            public global::System.Text.RegularExpressions.Regex regex;
            internal IntParamsArguments(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                this.age = age;
                this.name = name;
                this.regex = regex;
            }
        }

        // int IMethodSetupFeatureSut.IntParams(int age, string name, Regex regex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntParamsArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> age { get; }
            public Imposter.Abstractions.Arg<string> name { get; }
            public Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> regex { get; }

            public IntParamsArgumentsCriteria(Imposter.Abstractions.Arg<int> age, Imposter.Abstractions.Arg<string> name, Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex> regex)
            {
                this.age = age;
                this.name = name;
                this.regex = regex;
            }

            public bool Matches(IntParamsArguments arguments)
            {
                return age.Matches(arguments.age) && name.Matches(arguments.name) && regex.Matches(arguments.regex);
            }
        }

        public interface IIntParamsMethodInvocationHistory
        {
            bool Matches(IntParamsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntParamsMethodInvocationHistory : IIntParamsMethodInvocationHistory
        {
            internal IntParamsArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntParamsMethodInvocationHistory(IntParamsArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntParamsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntParamsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntParamsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntParamsMethodInvocationHistory>();
            internal void Add(IIntParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntParamsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.IntParams(int age, string name, Regex regex)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntParamsMethodInvocationsSetup : IIntParamsMethodInvocationsSetup
        {
            internal static IntParamsMethodInvocationsSetup DefaultInvocationSetup = new IntParamsMethodInvocationsSetup(new IntParamsArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<global::System.Text.RegularExpressions.Regex>.Any()));
            internal IntParamsArgumentsCriteria ArgumentsCriteria { get; }

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
                return default;
            }

            public IntParamsMethodInvocationsSetup(IntParamsArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Returns(IntParamsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    return value;
                };
                return this;
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    throw exception;
                };
                return this;
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Throws(IntParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int age, string name, global::System.Text.RegularExpressions.Regex regex) =>
                {
                    throw exceptionGenerator(age, name, regex);
                };
                return this;
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.CallBefore(IntParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.CallAfter(IntParamsCallbackDelegate callback)
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
                internal IntParamsDelegate? ResultGenerator { get; set; }
                internal IntParamsCallbackDelegate? CallBefore { get; set; }
                internal IntParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntParamsMethodInvocationsSetup
        {
            IIntParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntParamsMethodInvocationsSetup Throws(System.Exception exception);
            IIntParamsMethodInvocationsSetup Throws(IntParamsExceptionGeneratorDelegate exceptionGenerator);
            IIntParamsMethodInvocationsSetup CallBefore(IntParamsCallbackDelegate callback);
            IIntParamsMethodInvocationsSetup CallAfter(IntParamsCallbackDelegate callback);
            IIntParamsMethodInvocationsSetup Returns(IntParamsDelegate resultGenerator);
            IIntParamsMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntParams(int age, string name, Regex regex)
        public interface IIntParamsMethodImposterBuilder : IIntParamsMethodInvocationsSetup, IntParamsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntParamsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntParamsMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntParamsMethodInvocationsSetup>();
            private readonly IntParamsMethodInvocationHistoryCollection _intParamsMethodInvocationHistoryCollection;
            public IntParamsMethodImposter(IntParamsMethodInvocationHistoryCollection _intParamsMethodInvocationHistoryCollection)
            {
                this._intParamsMethodInvocationHistoryCollection = _intParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IntParamsArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IntParamsMethodInvocationsSetup? FindMatchingSetup(IntParamsArguments arguments)
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
                var arguments = new IntParamsArguments(age, name, regex);
                var matchingSetup = FindMatchingSetup(arguments) ?? IntParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(age, name, regex);
                    _intParamsMethodInvocationHistoryCollection.Add(new IntParamsMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intParamsMethodInvocationHistoryCollection.Add(new IntParamsMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntParamsMethodImposterBuilder
            {
                private readonly IntParamsMethodImposter _imposter;
                private readonly IntParamsMethodInvocationHistoryCollection _intParamsMethodInvocationHistoryCollection;
                private readonly IntParamsArgumentsCriteria _argumentsCriteria;
                private IntParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntParamsMethodImposter _imposter, IntParamsMethodInvocationHistoryCollection _intParamsMethodInvocationHistoryCollection, IntParamsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intParamsMethodInvocationHistoryCollection = _intParamsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIntParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntParamsMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Throws(IntParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.CallBefore(IntParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.CallAfter(IntParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Returns(IntParamsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntParamsMethodInvocationsSetup IIntParamsMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntParamsMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intParamsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntOutParam(out int outValue)
        public delegate int IntOutParamDelegate(out int outValue);
        // int IMethodSetupFeatureSut.IntOutParam(out int outValue)
        public delegate void IntOutParamCallbackDelegate(out int outValue);
        // int IMethodSetupFeatureSut.IntOutParam(out int outValue)
        public delegate System.Exception IntOutParamExceptionGeneratorDelegate(out int outValue);
        public interface IIntOutParamMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntOutParamMethodInvocationHistory : IIntOutParamMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public IntOutParamMethodInvocationHistory(int Result, System.Exception Exception)
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
        internal class IntOutParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntOutParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntOutParamMethodInvocationHistory>();
            internal void Add(IIntOutParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodSetupFeatureSut.IntOutParam(out int outValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntOutParamMethodInvocationsSetup : IIntOutParamMethodInvocationsSetup
        {
            internal static IntOutParamMethodInvocationsSetup DefaultInvocationSetup = new IntOutParamMethodInvocationsSetup();
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
                return default;
            }

            public IntOutParamMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out int outValue)
            {
                outValue = default(int);
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Returns(IntOutParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    return value;
                };
                return this;
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw new TException();
                };
                return this;
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    InitializeOutParametersWithDefaultValues(out outValue);
                    throw exception;
                };
                return this;
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Throws(IntOutParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int outValue) =>
                {
                    throw exceptionGenerator(out outValue);
                };
                return this;
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.CallBefore(IntOutParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.CallAfter(IntOutParamCallbackDelegate callback)
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
                internal IntOutParamDelegate? ResultGenerator { get; set; }
                internal IntOutParamCallbackDelegate? CallBefore { get; set; }
                internal IntOutParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntOutParamMethodInvocationsSetup
        {
            IIntOutParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntOutParamMethodInvocationsSetup Throws(System.Exception exception);
            IIntOutParamMethodInvocationsSetup Throws(IntOutParamExceptionGeneratorDelegate exceptionGenerator);
            IIntOutParamMethodInvocationsSetup CallBefore(IntOutParamCallbackDelegate callback);
            IIntOutParamMethodInvocationsSetup CallAfter(IntOutParamCallbackDelegate callback);
            IIntOutParamMethodInvocationsSetup Returns(IntOutParamDelegate resultGenerator);
            IIntOutParamMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntOutParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntOutParam(out int outValue)
        public interface IIntOutParamMethodImposterBuilder : IIntOutParamMethodInvocationsSetup, IntOutParamMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntOutParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntOutParamMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntOutParamMethodInvocationsSetup>();
            private readonly IntOutParamMethodInvocationHistoryCollection _intOutParamMethodInvocationHistoryCollection;
            public IntOutParamMethodImposter(IntOutParamMethodInvocationHistoryCollection _intOutParamMethodInvocationHistoryCollection)
            {
                this._intOutParamMethodInvocationHistoryCollection = _intOutParamMethodInvocationHistoryCollection;
            }

            private static void InitializeOutParametersWithDefaultValues(out int outValue)
            {
                outValue = default(int);
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private IntOutParamMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public int Invoke(out int outValue)
            {
                var matchingSetup = FindMatchingSetup() ?? IntOutParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out outValue);
                    _intOutParamMethodInvocationHistoryCollection.Add(new IntOutParamMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intOutParamMethodInvocationHistoryCollection.Add(new IntOutParamMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntOutParamMethodImposterBuilder
            {
                private readonly IntOutParamMethodImposter _imposter;
                private readonly IntOutParamMethodInvocationHistoryCollection _intOutParamMethodInvocationHistoryCollection;
                private IntOutParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntOutParamMethodImposter _imposter, IntOutParamMethodInvocationHistoryCollection _intOutParamMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._intOutParamMethodInvocationHistoryCollection = _intOutParamMethodInvocationHistoryCollection;
                }

                private IIntOutParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntOutParamMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Throws(IntOutParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.CallBefore(IntOutParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.CallAfter(IntOutParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Returns(IntOutParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntOutParamMethodInvocationsSetup IIntOutParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntOutParamMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intOutParamMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntRefParam(ref int refValue)
        public delegate int IntRefParamDelegate(ref int refValue);
        // int IMethodSetupFeatureSut.IntRefParam(ref int refValue)
        public delegate void IntRefParamCallbackDelegate(ref int refValue);
        // int IMethodSetupFeatureSut.IntRefParam(ref int refValue)
        public delegate System.Exception IntRefParamExceptionGeneratorDelegate(ref int refValue);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntRefParamArguments
        {
            public int refValue;
            internal IntRefParamArguments(int refValue)
            {
                this.refValue = refValue;
            }
        }

        // int IMethodSetupFeatureSut.IntRefParam(ref int refValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntRefParamArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> refValue { get; }

            public IntRefParamArgumentsCriteria(Imposter.Abstractions.Arg<int> refValue)
            {
                this.refValue = refValue;
            }

            public bool Matches(IntRefParamArguments arguments)
            {
                return refValue.Matches(arguments.refValue);
            }
        }

        public interface IIntRefParamMethodInvocationHistory
        {
            bool Matches(IntRefParamArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntRefParamMethodInvocationHistory : IIntRefParamMethodInvocationHistory
        {
            internal IntRefParamArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntRefParamMethodInvocationHistory(IntRefParamArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntRefParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntRefParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntRefParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntRefParamMethodInvocationHistory>();
            internal void Add(IIntRefParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntRefParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.IntRefParam(ref int refValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntRefParamMethodInvocationsSetup : IIntRefParamMethodInvocationsSetup
        {
            internal static IntRefParamMethodInvocationsSetup DefaultInvocationSetup = new IntRefParamMethodInvocationsSetup(new IntRefParamArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal IntRefParamArgumentsCriteria ArgumentsCriteria { get; }

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
                return default;
            }

            public IntRefParamMethodInvocationsSetup(IntRefParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Returns(IntRefParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    return value;
                };
                return this;
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    throw exception;
                };
                return this;
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Throws(IntRefParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref int refValue) =>
                {
                    throw exceptionGenerator(ref refValue);
                };
                return this;
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.CallBefore(IntRefParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.CallAfter(IntRefParamCallbackDelegate callback)
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
                internal IntRefParamDelegate? ResultGenerator { get; set; }
                internal IntRefParamCallbackDelegate? CallBefore { get; set; }
                internal IntRefParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntRefParamMethodInvocationsSetup
        {
            IIntRefParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntRefParamMethodInvocationsSetup Throws(System.Exception exception);
            IIntRefParamMethodInvocationsSetup Throws(IntRefParamExceptionGeneratorDelegate exceptionGenerator);
            IIntRefParamMethodInvocationsSetup CallBefore(IntRefParamCallbackDelegate callback);
            IIntRefParamMethodInvocationsSetup CallAfter(IntRefParamCallbackDelegate callback);
            IIntRefParamMethodInvocationsSetup Returns(IntRefParamDelegate resultGenerator);
            IIntRefParamMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntRefParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntRefParam(ref int refValue)
        public interface IIntRefParamMethodImposterBuilder : IIntRefParamMethodInvocationsSetup, IntRefParamMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntRefParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntRefParamMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntRefParamMethodInvocationsSetup>();
            private readonly IntRefParamMethodInvocationHistoryCollection _intRefParamMethodInvocationHistoryCollection;
            public IntRefParamMethodImposter(IntRefParamMethodInvocationHistoryCollection _intRefParamMethodInvocationHistoryCollection)
            {
                this._intRefParamMethodInvocationHistoryCollection = _intRefParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IntRefParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IntRefParamMethodInvocationsSetup? FindMatchingSetup(IntRefParamArguments arguments)
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
                var arguments = new IntRefParamArguments(refValue);
                var matchingSetup = FindMatchingSetup(arguments) ?? IntRefParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ref refValue);
                    _intRefParamMethodInvocationHistoryCollection.Add(new IntRefParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intRefParamMethodInvocationHistoryCollection.Add(new IntRefParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntRefParamMethodImposterBuilder
            {
                private readonly IntRefParamMethodImposter _imposter;
                private readonly IntRefParamMethodInvocationHistoryCollection _intRefParamMethodInvocationHistoryCollection;
                private readonly IntRefParamArgumentsCriteria _argumentsCriteria;
                private IntRefParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntRefParamMethodImposter _imposter, IntRefParamMethodInvocationHistoryCollection _intRefParamMethodInvocationHistoryCollection, IntRefParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intRefParamMethodInvocationHistoryCollection = _intRefParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIntRefParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntRefParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Throws(IntRefParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.CallBefore(IntRefParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.CallAfter(IntRefParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Returns(IntRefParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntRefParamMethodInvocationsSetup IIntRefParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntRefParamMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intRefParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntParamsParam(params string[] paramsStrings)
        public delegate int IntParamsParamDelegate(string[] paramsStrings);
        // int IMethodSetupFeatureSut.IntParamsParam(params string[] paramsStrings)
        public delegate void IntParamsParamCallbackDelegate(string[] paramsStrings);
        // int IMethodSetupFeatureSut.IntParamsParam(params string[] paramsStrings)
        public delegate System.Exception IntParamsParamExceptionGeneratorDelegate(string[] paramsStrings);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntParamsParamArguments
        {
            public string[] paramsStrings;
            internal IntParamsParamArguments(string[] paramsStrings)
            {
                this.paramsStrings = paramsStrings;
            }
        }

        // int IMethodSetupFeatureSut.IntParamsParam(params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntParamsParamArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<string[]> paramsStrings { get; }

            public IntParamsParamArgumentsCriteria(Imposter.Abstractions.Arg<string[]> paramsStrings)
            {
                this.paramsStrings = paramsStrings;
            }

            public bool Matches(IntParamsParamArguments arguments)
            {
                return paramsStrings.Matches(arguments.paramsStrings);
            }
        }

        public interface IIntParamsParamMethodInvocationHistory
        {
            bool Matches(IntParamsParamArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntParamsParamMethodInvocationHistory : IIntParamsParamMethodInvocationHistory
        {
            internal IntParamsParamArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntParamsParamMethodInvocationHistory(IntParamsParamArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntParamsParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntParamsParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntParamsParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntParamsParamMethodInvocationHistory>();
            internal void Add(IIntParamsParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntParamsParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.IntParamsParam(params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntParamsParamMethodInvocationsSetup : IIntParamsParamMethodInvocationsSetup
        {
            internal static IntParamsParamMethodInvocationsSetup DefaultInvocationSetup = new IntParamsParamMethodInvocationsSetup(new IntParamsParamArgumentsCriteria(Imposter.Abstractions.Arg<string[]>.Any()));
            internal IntParamsParamArgumentsCriteria ArgumentsCriteria { get; }

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
                return default;
            }

            public IntParamsParamMethodInvocationsSetup(IntParamsParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Returns(IntParamsParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    return value;
                };
                return this;
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    throw exception;
                };
                return this;
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Throws(IntParamsParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (string[] paramsStrings) =>
                {
                    throw exceptionGenerator(paramsStrings);
                };
                return this;
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.CallBefore(IntParamsParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.CallAfter(IntParamsParamCallbackDelegate callback)
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
                internal IntParamsParamDelegate? ResultGenerator { get; set; }
                internal IntParamsParamCallbackDelegate? CallBefore { get; set; }
                internal IntParamsParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntParamsParamMethodInvocationsSetup
        {
            IIntParamsParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntParamsParamMethodInvocationsSetup Throws(System.Exception exception);
            IIntParamsParamMethodInvocationsSetup Throws(IntParamsParamExceptionGeneratorDelegate exceptionGenerator);
            IIntParamsParamMethodInvocationsSetup CallBefore(IntParamsParamCallbackDelegate callback);
            IIntParamsParamMethodInvocationsSetup CallAfter(IntParamsParamCallbackDelegate callback);
            IIntParamsParamMethodInvocationsSetup Returns(IntParamsParamDelegate resultGenerator);
            IIntParamsParamMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntParamsParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntParamsParam(params string[] paramsStrings)
        public interface IIntParamsParamMethodImposterBuilder : IIntParamsParamMethodInvocationsSetup, IntParamsParamMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntParamsParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntParamsParamMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntParamsParamMethodInvocationsSetup>();
            private readonly IntParamsParamMethodInvocationHistoryCollection _intParamsParamMethodInvocationHistoryCollection;
            public IntParamsParamMethodImposter(IntParamsParamMethodInvocationHistoryCollection _intParamsParamMethodInvocationHistoryCollection)
            {
                this._intParamsParamMethodInvocationHistoryCollection = _intParamsParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IntParamsParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IntParamsParamMethodInvocationsSetup? FindMatchingSetup(IntParamsParamArguments arguments)
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
                var arguments = new IntParamsParamArguments(paramsStrings);
                var matchingSetup = FindMatchingSetup(arguments) ?? IntParamsParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(paramsStrings);
                    _intParamsParamMethodInvocationHistoryCollection.Add(new IntParamsParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intParamsParamMethodInvocationHistoryCollection.Add(new IntParamsParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntParamsParamMethodImposterBuilder
            {
                private readonly IntParamsParamMethodImposter _imposter;
                private readonly IntParamsParamMethodInvocationHistoryCollection _intParamsParamMethodInvocationHistoryCollection;
                private readonly IntParamsParamArgumentsCriteria _argumentsCriteria;
                private IntParamsParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntParamsParamMethodImposter _imposter, IntParamsParamMethodInvocationHistoryCollection _intParamsParamMethodInvocationHistoryCollection, IntParamsParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intParamsParamMethodInvocationHistoryCollection = _intParamsParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIntParamsParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntParamsParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Throws(IntParamsParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.CallBefore(IntParamsParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.CallAfter(IntParamsParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Returns(IntParamsParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntParamsParamMethodInvocationsSetup IIntParamsParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntParamsParamMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intParamsParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntInParam(in string inStringValue)
        public delegate int IntInParamDelegate(in string inStringValue);
        // int IMethodSetupFeatureSut.IntInParam(in string inStringValue)
        public delegate void IntInParamCallbackDelegate(in string inStringValue);
        // int IMethodSetupFeatureSut.IntInParam(in string inStringValue)
        public delegate System.Exception IntInParamExceptionGeneratorDelegate(in string inStringValue);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntInParamArguments
        {
            public string inStringValue;
            internal IntInParamArguments(string inStringValue)
            {
                this.inStringValue = inStringValue;
            }
        }

        // int IMethodSetupFeatureSut.IntInParam(in string inStringValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntInParamArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<string> inStringValue { get; }

            public IntInParamArgumentsCriteria(Imposter.Abstractions.Arg<string> inStringValue)
            {
                this.inStringValue = inStringValue;
            }

            public bool Matches(IntInParamArguments arguments)
            {
                return inStringValue.Matches(arguments.inStringValue);
            }
        }

        public interface IIntInParamMethodInvocationHistory
        {
            bool Matches(IntInParamArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntInParamMethodInvocationHistory : IIntInParamMethodInvocationHistory
        {
            internal IntInParamArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntInParamMethodInvocationHistory(IntInParamArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntInParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntInParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntInParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntInParamMethodInvocationHistory>();
            internal void Add(IIntInParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntInParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.IntInParam(in string inStringValue)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntInParamMethodInvocationsSetup : IIntInParamMethodInvocationsSetup
        {
            internal static IntInParamMethodInvocationsSetup DefaultInvocationSetup = new IntInParamMethodInvocationsSetup(new IntInParamArgumentsCriteria(Imposter.Abstractions.Arg<string>.Any()));
            internal IntInParamArgumentsCriteria ArgumentsCriteria { get; }

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
                return default;
            }

            public IntInParamMethodInvocationsSetup(IntInParamArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Returns(IntInParamDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    return value;
                };
                return this;
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    throw new TException();
                };
                return this;
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    throw exception;
                };
                return this;
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Throws(IntInParamExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (in string inStringValue) =>
                {
                    throw exceptionGenerator(in inStringValue);
                };
                return this;
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.CallBefore(IntInParamCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.CallAfter(IntInParamCallbackDelegate callback)
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
                internal IntInParamDelegate? ResultGenerator { get; set; }
                internal IntInParamCallbackDelegate? CallBefore { get; set; }
                internal IntInParamCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntInParamMethodInvocationsSetup
        {
            IIntInParamMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntInParamMethodInvocationsSetup Throws(System.Exception exception);
            IIntInParamMethodInvocationsSetup Throws(IntInParamExceptionGeneratorDelegate exceptionGenerator);
            IIntInParamMethodInvocationsSetup CallBefore(IntInParamCallbackDelegate callback);
            IIntInParamMethodInvocationsSetup CallAfter(IntInParamCallbackDelegate callback);
            IIntInParamMethodInvocationsSetup Returns(IntInParamDelegate resultGenerator);
            IIntInParamMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntInParamMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntInParam(in string inStringValue)
        public interface IIntInParamMethodImposterBuilder : IIntInParamMethodInvocationsSetup, IntInParamMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntInParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntInParamMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntInParamMethodInvocationsSetup>();
            private readonly IntInParamMethodInvocationHistoryCollection _intInParamMethodInvocationHistoryCollection;
            public IntInParamMethodImposter(IntInParamMethodInvocationHistoryCollection _intInParamMethodInvocationHistoryCollection)
            {
                this._intInParamMethodInvocationHistoryCollection = _intInParamMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(IntInParamArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IntInParamMethodInvocationsSetup? FindMatchingSetup(IntInParamArguments arguments)
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
                var arguments = new IntInParamArguments(inStringValue);
                var matchingSetup = FindMatchingSetup(arguments) ?? IntInParamMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(in inStringValue);
                    _intInParamMethodInvocationHistoryCollection.Add(new IntInParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intInParamMethodInvocationHistoryCollection.Add(new IntInParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntInParamMethodImposterBuilder
            {
                private readonly IntInParamMethodImposter _imposter;
                private readonly IntInParamMethodInvocationHistoryCollection _intInParamMethodInvocationHistoryCollection;
                private readonly IntInParamArgumentsCriteria _argumentsCriteria;
                private IntInParamMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntInParamMethodImposter _imposter, IntInParamMethodInvocationHistoryCollection _intInParamMethodInvocationHistoryCollection, IntInParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intInParamMethodInvocationHistoryCollection = _intInParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIntInParamMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntInParamMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Throws(IntInParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.CallBefore(IntInParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.CallAfter(IntInParamCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Returns(IntInParamDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntInParamMethodInvocationsSetup IIntInParamMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void IntInParamMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intInParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodSetupFeatureSut.IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public delegate int IntAllRefKindsDelegate(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings);
        // int IMethodSetupFeatureSut.IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public delegate void IntAllRefKindsCallbackDelegate(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings);
        // int IMethodSetupFeatureSut.IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public delegate System.Exception IntAllRefKindsExceptionGeneratorDelegate(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntAllRefKindsArguments
        {
            public int refValue;
            public int inValue;
            public string valueAsString;
            public string[] paramsStrings;
            internal IntAllRefKindsArguments(int refValue, int inValue, string valueAsString, string[] paramsStrings)
            {
                this.refValue = refValue;
                this.inValue = inValue;
                this.valueAsString = valueAsString;
                this.paramsStrings = paramsStrings;
            }
        }

        // int IMethodSetupFeatureSut.IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntAllRefKindsArgumentsCriteria
        {
            public Imposter.Abstractions.OutArg<int> value { get; }
            public Imposter.Abstractions.Arg<int> refValue { get; }
            public Imposter.Abstractions.Arg<int> inValue { get; }
            public Imposter.Abstractions.Arg<string> valueAsString { get; }
            public Imposter.Abstractions.Arg<string[]> paramsStrings { get; }

            public IntAllRefKindsArgumentsCriteria(Imposter.Abstractions.OutArg<int> value, Imposter.Abstractions.Arg<int> refValue, Imposter.Abstractions.Arg<int> inValue, Imposter.Abstractions.Arg<string> valueAsString, Imposter.Abstractions.Arg<string[]> paramsStrings)
            {
                this.value = value;
                this.refValue = refValue;
                this.inValue = inValue;
                this.valueAsString = valueAsString;
                this.paramsStrings = paramsStrings;
            }

            public bool Matches(IntAllRefKindsArguments arguments)
            {
                return refValue.Matches(arguments.refValue) && inValue.Matches(arguments.inValue) && valueAsString.Matches(arguments.valueAsString) && paramsStrings.Matches(arguments.paramsStrings);
            }
        }

        public interface IIntAllRefKindsMethodInvocationHistory
        {
            bool Matches(IntAllRefKindsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntAllRefKindsMethodInvocationHistory : IIntAllRefKindsMethodInvocationHistory
        {
            internal IntAllRefKindsArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntAllRefKindsMethodInvocationHistory(IntAllRefKindsArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntAllRefKindsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntAllRefKindsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntAllRefKindsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntAllRefKindsMethodInvocationHistory>();
            internal void Add(IIntAllRefKindsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntAllRefKindsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodSetupFeatureSut.IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntAllRefKindsMethodInvocationsSetup : IIntAllRefKindsMethodInvocationsSetup
        {
            internal static IntAllRefKindsMethodInvocationsSetup DefaultInvocationSetup = new IntAllRefKindsMethodInvocationsSetup(new IntAllRefKindsArgumentsCriteria(Imposter.Abstractions.OutArg<int>.Any(), Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<string>.Any(), Imposter.Abstractions.Arg<string[]>.Any()));
            internal IntAllRefKindsArgumentsCriteria ArgumentsCriteria { get; }

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
                return default;
            }

            public IntAllRefKindsMethodInvocationsSetup(IntAllRefKindsArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out int value)
            {
                value = default(int);
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Returns(IntAllRefKindsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Returns(int value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    return value_1;
                };
                return this;
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw new TException();
                };
                return this;
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw exception;
                };
                return this;
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Throws(IntAllRefKindsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings) =>
                {
                    throw exceptionGenerator(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                };
                return this;
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.CallBefore(IntAllRefKindsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.CallAfter(IntAllRefKindsCallbackDelegate callback)
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
                internal IntAllRefKindsDelegate? ResultGenerator { get; set; }
                internal IntAllRefKindsCallbackDelegate? CallBefore { get; set; }
                internal IntAllRefKindsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntAllRefKindsMethodInvocationsSetup
        {
            IIntAllRefKindsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IIntAllRefKindsMethodInvocationsSetup Throws(System.Exception exception);
            IIntAllRefKindsMethodInvocationsSetup Throws(IntAllRefKindsExceptionGeneratorDelegate exceptionGenerator);
            IIntAllRefKindsMethodInvocationsSetup CallBefore(IntAllRefKindsCallbackDelegate callback);
            IIntAllRefKindsMethodInvocationsSetup CallAfter(IntAllRefKindsCallbackDelegate callback);
            IIntAllRefKindsMethodInvocationsSetup Returns(IntAllRefKindsDelegate resultGenerator);
            IIntAllRefKindsMethodInvocationsSetup Returns(int value_1);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntAllRefKindsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodSetupFeatureSut.IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, params string[] paramsStrings)
        public interface IIntAllRefKindsMethodImposterBuilder : IIntAllRefKindsMethodInvocationsSetup, IntAllRefKindsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntAllRefKindsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntAllRefKindsMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<IntAllRefKindsMethodInvocationsSetup>();
            private readonly IntAllRefKindsMethodInvocationHistoryCollection _intAllRefKindsMethodInvocationHistoryCollection;
            public IntAllRefKindsMethodImposter(IntAllRefKindsMethodInvocationHistoryCollection _intAllRefKindsMethodInvocationHistoryCollection)
            {
                this._intAllRefKindsMethodInvocationHistoryCollection = _intAllRefKindsMethodInvocationHistoryCollection;
            }

            private static void InitializeOutParametersWithDefaultValues(out int value)
            {
                value = default(int);
            }

            public bool HasMatchingSetup(IntAllRefKindsArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private IntAllRefKindsMethodInvocationsSetup? FindMatchingSetup(IntAllRefKindsArguments arguments)
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
                var arguments = new IntAllRefKindsArguments(refValue, inValue, valueAsString, paramsStrings);
                var matchingSetup = FindMatchingSetup(arguments) ?? IntAllRefKindsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out value, ref refValue, in inValue, valueAsString, paramsStrings);
                    _intAllRefKindsMethodInvocationHistoryCollection.Add(new IntAllRefKindsMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intAllRefKindsMethodInvocationHistoryCollection.Add(new IntAllRefKindsMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntAllRefKindsMethodImposterBuilder
            {
                private readonly IntAllRefKindsMethodImposter _imposter;
                private readonly IntAllRefKindsMethodInvocationHistoryCollection _intAllRefKindsMethodInvocationHistoryCollection;
                private readonly IntAllRefKindsArgumentsCriteria _argumentsCriteria;
                private IntAllRefKindsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(IntAllRefKindsMethodImposter _imposter, IntAllRefKindsMethodInvocationHistoryCollection _intAllRefKindsMethodInvocationHistoryCollection, IntAllRefKindsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intAllRefKindsMethodInvocationHistoryCollection = _intAllRefKindsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IIntAllRefKindsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new IntAllRefKindsMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Throws(IntAllRefKindsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.CallBefore(IntAllRefKindsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.CallAfter(IntAllRefKindsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Returns(IntAllRefKindsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IIntAllRefKindsMethodInvocationsSetup IIntAllRefKindsMethodInvocationsSetup.Returns(int value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void IntAllRefKindsMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intAllRefKindsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodSetupFeatureSut.GenericSingleParam<TValue>(TValue value)
        public delegate void GenericSingleParamDelegate<TValue>(TValue value);
        // void IMethodSetupFeatureSut.GenericSingleParam<TValue>(TValue value)
        public delegate void GenericSingleParamCallbackDelegate<TValue>(TValue value);
        // void IMethodSetupFeatureSut.GenericSingleParam<TValue>(TValue value)
        public delegate System.Exception GenericSingleParamExceptionGeneratorDelegate<TValue>(TValue value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericSingleParamArguments<TValue>
        {
            public TValue value;
            internal GenericSingleParamArguments(TValue value)
            {
                this.value = value;
            }

            public GenericSingleParamArguments<TValueTarget> As<TValueTarget>()
            {
                return new GenericSingleParamArguments<TValueTarget>(TypeCaster.Cast<TValue, TValueTarget>(value));
            }
        }

        // void IMethodSetupFeatureSut.GenericSingleParam<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericSingleParamArgumentsCriteria<TValue>
        {
            public Imposter.Abstractions.Arg<TValue> value { get; }

            public GenericSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<TValue> value)
            {
                this.value = value;
            }

            public bool Matches(GenericSingleParamArguments<TValue> arguments)
            {
                return value.Matches(arguments.value);
            }

            public GenericSingleParamArgumentsCriteria<TValueTarget> As<TValueTarget>()
            {
                return new GenericSingleParamArgumentsCriteria<TValueTarget>(Imposter.Abstractions.Arg<TValueTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget, TValue>(it, out TValue valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGenericSingleParamMethodInvocationHistory
        {
            bool Matches<TValueTarget>(GenericSingleParamArgumentsCriteria<TValueTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericSingleParamMethodInvocationHistory<TValue> : IGenericSingleParamMethodInvocationHistory
        {
            internal GenericSingleParamArguments<TValue> Arguments;
            internal System.Exception Exception;
            public GenericSingleParamMethodInvocationHistory(GenericSingleParamArguments<TValue> Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget>(GenericSingleParamArgumentsCriteria<TValueTarget> criteria)
            {
                return (typeof(TValueTarget) == typeof(TValue)) && criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericSingleParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericSingleParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericSingleParamMethodInvocationHistory>();
            internal void Add(IGenericSingleParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue>(GenericSingleParamArgumentsCriteria<TValue> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue>(argumentsCriteria));
            }
        }

        internal class GenericSingleParamMethodImposterCollection
        {
            private readonly GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection;
            public GenericSingleParamMethodImposterCollection(GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection)
            {
                this._genericSingleParamMethodInvocationHistoryCollection = _genericSingleParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericSingleParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericSingleParamMethodImposter>();
            internal GenericSingleParamMethodImposter<TValue> AddNew<TValue>()
            {
                var imposter = new GenericSingleParamMethodImposter<TValue>(_genericSingleParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericSingleParamMethodImposter<TValue> GetImposterWithMatchingSetup<TValue>(GenericSingleParamArguments<TValue> arguments)
            {
                return _imposters.Select(it => it.As<TValue>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue>();
            }
        }

        // void IMethodSetupFeatureSut.GenericSingleParam<TValue>(TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericSingleParamMethodInvocationsSetup<TValue> : IGenericSingleParamMethodInvocationsSetup<TValue>
        {
            internal static GenericSingleParamMethodInvocationsSetup<TValue> DefaultInvocationSetup = new GenericSingleParamMethodInvocationsSetup<TValue>(new GenericSingleParamArgumentsCriteria<TValue>(Imposter.Abstractions.Arg<TValue>.Any()));
            internal GenericSingleParamArgumentsCriteria<TValue> ArgumentsCriteria { get; }

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

            public GenericSingleParamMethodInvocationsSetup(GenericSingleParamArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.Throws(GenericSingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.CallBefore(GenericSingleParamCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.CallAfter(GenericSingleParamCallbackDelegate<TValue> callback)
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
                internal GenericSingleParamDelegate<TValue>? ResultGenerator { get; set; }
                internal GenericSingleParamCallbackDelegate<TValue>? CallBefore { get; set; }
                internal GenericSingleParamCallbackDelegate<TValue>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericSingleParamMethodInvocationsSetup<TValue>
        {
            IGenericSingleParamMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            IGenericSingleParamMethodInvocationsSetup<TValue> Throws(System.Exception exception);
            IGenericSingleParamMethodInvocationsSetup<TValue> Throws(GenericSingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator);
            IGenericSingleParamMethodInvocationsSetup<TValue> CallBefore(GenericSingleParamCallbackDelegate<TValue> callback);
            IGenericSingleParamMethodInvocationsSetup<TValue> CallAfter(GenericSingleParamCallbackDelegate<TValue> callback);
        }

        internal interface IGenericSingleParamMethodImposter
        {
            IGenericSingleParamMethodImposter<TValueTarget>? As<TValueTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericSingleParamMethodImposter<TValue> : IGenericSingleParamMethodImposter
        {
            void Invoke(TValue value);
            bool HasMatchingSetup(GenericSingleParamArguments<TValue> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericSingleParamMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.GenericSingleParam<TValue>(TValue value)
        public interface IGenericSingleParamMethodImposterBuilder<TValue> : IGenericSingleParamMethodInvocationsSetup<TValue>, GenericSingleParamMethodInvocationVerifier<TValue>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericSingleParamMethodImposter<TValue> : IGenericSingleParamMethodImposter<TValue>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericSingleParamMethodInvocationsSetup<TValue>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericSingleParamMethodInvocationsSetup<TValue>>();
            private readonly GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection;
            public GenericSingleParamMethodImposter(GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection)
            {
                this._genericSingleParamMethodInvocationHistoryCollection = _genericSingleParamMethodInvocationHistoryCollection;
            }

            IGenericSingleParamMethodImposter<TValueTarget>? IGenericSingleParamMethodImposter.As<TValueTarget>()
            {
                if (typeof(TValueTarget).IsAssignableTo(typeof(TValue)))
                {
                    return new Adapter<TValueTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget> : IGenericSingleParamMethodImposter<TValueTarget>
            {
                private readonly GenericSingleParamMethodImposter<TValue> _target;
                public Adapter(GenericSingleParamMethodImposter<TValue> target)
                {
                    _target = target;
                }

                public void Invoke(TValueTarget value)
                {
                    _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TValueTarget, TValue>(value));
                }

                public bool HasMatchingSetup(GenericSingleParamArguments<TValueTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue>());
                }

                IGenericSingleParamMethodImposter<TValueTarget1>? IGenericSingleParamMethodImposter.As<TValueTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericSingleParamArguments<TValue> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericSingleParamMethodInvocationsSetup<TValue>? FindMatchingSetup(GenericSingleParamArguments<TValue> arguments)
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
                var arguments = new GenericSingleParamArguments<TValue>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericSingleParamMethodInvocationsSetup<TValue>.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(value);
                    _genericSingleParamMethodInvocationHistoryCollection.Add(new GenericSingleParamMethodInvocationHistory<TValue>(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _genericSingleParamMethodInvocationHistoryCollection.Add(new GenericSingleParamMethodInvocationHistory<TValue>(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericSingleParamMethodImposterBuilder<TValue>
            {
                private readonly GenericSingleParamMethodImposterCollection _imposterCollection;
                private readonly GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection;
                private readonly GenericSingleParamArgumentsCriteria<TValue> _argumentsCriteria;
                private GenericSingleParamMethodInvocationsSetup<TValue>? _existingInvocationSetup;
                public Builder(GenericSingleParamMethodImposterCollection _imposterCollection, GenericSingleParamMethodInvocationHistoryCollection _genericSingleParamMethodInvocationHistoryCollection, GenericSingleParamArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericSingleParamMethodInvocationHistoryCollection = _genericSingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericSingleParamMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericSingleParamMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.Throws(GenericSingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.CallBefore(GenericSingleParamCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericSingleParamMethodInvocationsSetup<TValue> IGenericSingleParamMethodInvocationsSetup<TValue>.CallAfter(GenericSingleParamCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void GenericSingleParamMethodInvocationVerifier<TValue>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericSingleParamMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodSetupFeatureSut.GenericInnerSingleParam<TValue>(List<TValue> value)
        public delegate void GenericInnerSingleParamDelegate<TValue>(global::System.Collections.Generic.List<TValue> value);
        // void IMethodSetupFeatureSut.GenericInnerSingleParam<TValue>(List<TValue> value)
        public delegate void GenericInnerSingleParamCallbackDelegate<TValue>(global::System.Collections.Generic.List<TValue> value);
        // void IMethodSetupFeatureSut.GenericInnerSingleParam<TValue>(List<TValue> value)
        public delegate System.Exception GenericInnerSingleParamExceptionGeneratorDelegate<TValue>(global::System.Collections.Generic.List<TValue> value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericInnerSingleParamArguments<TValue>
        {
            public global::System.Collections.Generic.List<TValue> value;
            internal GenericInnerSingleParamArguments(global::System.Collections.Generic.List<TValue> value)
            {
                this.value = value;
            }

            public GenericInnerSingleParamArguments<TValueTarget> As<TValueTarget>()
            {
                return new GenericInnerSingleParamArguments<TValueTarget>(TypeCaster.Cast<global::System.Collections.Generic.List<TValue>, global::System.Collections.Generic.List<TValueTarget>>(value));
            }
        }

        // void IMethodSetupFeatureSut.GenericInnerSingleParam<TValue>(List<TValue> value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericInnerSingleParamArgumentsCriteria<TValue>
        {
            public Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>> value { get; }

            public GenericInnerSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>> value)
            {
                this.value = value;
            }

            public bool Matches(GenericInnerSingleParamArguments<TValue> arguments)
            {
                return value.Matches(arguments.value);
            }

            public GenericInnerSingleParamArgumentsCriteria<TValueTarget> As<TValueTarget>()
            {
                return new GenericInnerSingleParamArgumentsCriteria<TValueTarget>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValueTarget>>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<global::System.Collections.Generic.List<TValueTarget>, global::System.Collections.Generic.List<TValue>>(it, out global::System.Collections.Generic.List<TValue> valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGenericInnerSingleParamMethodInvocationHistory
        {
            bool Matches<TValueTarget>(GenericInnerSingleParamArgumentsCriteria<TValueTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerSingleParamMethodInvocationHistory<TValue> : IGenericInnerSingleParamMethodInvocationHistory
        {
            internal GenericInnerSingleParamArguments<TValue> Arguments;
            internal System.Exception Exception;
            public GenericInnerSingleParamMethodInvocationHistory(GenericInnerSingleParamArguments<TValue> Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget>(GenericInnerSingleParamArgumentsCriteria<TValueTarget> criteria)
            {
                return (typeof(global::System.Collections.Generic.List<TValueTarget>) == typeof(global::System.Collections.Generic.List<TValue>)) && criteria.As<TValue>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerSingleParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerSingleParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerSingleParamMethodInvocationHistory>();
            internal void Add(IGenericInnerSingleParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue>(GenericInnerSingleParamArgumentsCriteria<TValue> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue>(argumentsCriteria));
            }
        }

        internal class GenericInnerSingleParamMethodImposterCollection
        {
            private readonly GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection;
            public GenericInnerSingleParamMethodImposterCollection(GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection)
            {
                this._genericInnerSingleParamMethodInvocationHistoryCollection = _genericInnerSingleParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerSingleParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerSingleParamMethodImposter>();
            internal GenericInnerSingleParamMethodImposter<TValue> AddNew<TValue>()
            {
                var imposter = new GenericInnerSingleParamMethodImposter<TValue>(_genericInnerSingleParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericInnerSingleParamMethodImposter<TValue> GetImposterWithMatchingSetup<TValue>(GenericInnerSingleParamArguments<TValue> arguments)
            {
                return _imposters.Select(it => it.As<TValue>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue>();
            }
        }

        // void IMethodSetupFeatureSut.GenericInnerSingleParam<TValue>(List<TValue> value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericInnerSingleParamMethodInvocationsSetup<TValue> : IGenericInnerSingleParamMethodInvocationsSetup<TValue>
        {
            internal static GenericInnerSingleParamMethodInvocationsSetup<TValue> DefaultInvocationSetup = new GenericInnerSingleParamMethodInvocationsSetup<TValue>(new GenericInnerSingleParamArgumentsCriteria<TValue>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>>.Any()));
            internal GenericInnerSingleParamArgumentsCriteria<TValue> ArgumentsCriteria { get; }

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

            internal static void DefaultResultGenerator(global::System.Collections.Generic.List<TValue> value)
            {
            }

            public GenericInnerSingleParamMethodInvocationsSetup(GenericInnerSingleParamArgumentsCriteria<TValue> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.Throws(GenericInnerSingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.CallBefore(GenericInnerSingleParamCallbackDelegate<TValue> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.CallAfter(GenericInnerSingleParamCallbackDelegate<TValue> callback)
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

            public void Invoke(global::System.Collections.Generic.List<TValue> value)
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
                internal GenericInnerSingleParamDelegate<TValue>? ResultGenerator { get; set; }
                internal GenericInnerSingleParamCallbackDelegate<TValue>? CallBefore { get; set; }
                internal GenericInnerSingleParamCallbackDelegate<TValue>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericInnerSingleParamMethodInvocationsSetup<TValue>
        {
            IGenericInnerSingleParamMethodInvocationsSetup<TValue> Throws<TException>()
                where TException : Exception, new();
            IGenericInnerSingleParamMethodInvocationsSetup<TValue> Throws(System.Exception exception);
            IGenericInnerSingleParamMethodInvocationsSetup<TValue> Throws(GenericInnerSingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator);
            IGenericInnerSingleParamMethodInvocationsSetup<TValue> CallBefore(GenericInnerSingleParamCallbackDelegate<TValue> callback);
            IGenericInnerSingleParamMethodInvocationsSetup<TValue> CallAfter(GenericInnerSingleParamCallbackDelegate<TValue> callback);
        }

        internal interface IGenericInnerSingleParamMethodImposter
        {
            IGenericInnerSingleParamMethodImposter<TValueTarget>? As<TValueTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericInnerSingleParamMethodImposter<TValue> : IGenericInnerSingleParamMethodImposter
        {
            void Invoke(global::System.Collections.Generic.List<TValue> value);
            bool HasMatchingSetup(GenericInnerSingleParamArguments<TValue> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericInnerSingleParamMethodInvocationVerifier<TValue>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodSetupFeatureSut.GenericInnerSingleParam<TValue>(List<TValue> value)
        public interface IGenericInnerSingleParamMethodImposterBuilder<TValue> : IGenericInnerSingleParamMethodInvocationsSetup<TValue>, GenericInnerSingleParamMethodInvocationVerifier<TValue>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerSingleParamMethodImposter<TValue> : IGenericInnerSingleParamMethodImposter<TValue>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericInnerSingleParamMethodInvocationsSetup<TValue>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericInnerSingleParamMethodInvocationsSetup<TValue>>();
            private readonly GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection;
            public GenericInnerSingleParamMethodImposter(GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection)
            {
                this._genericInnerSingleParamMethodInvocationHistoryCollection = _genericInnerSingleParamMethodInvocationHistoryCollection;
            }

            IGenericInnerSingleParamMethodImposter<TValueTarget>? IGenericInnerSingleParamMethodImposter.As<TValueTarget>()
            {
                if (typeof(global::System.Collections.Generic.List<TValueTarget>).IsAssignableTo(typeof(global::System.Collections.Generic.List<TValue>)))
                {
                    return new Adapter<TValueTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget> : IGenericInnerSingleParamMethodImposter<TValueTarget>
            {
                private readonly GenericInnerSingleParamMethodImposter<TValue> _target;
                public Adapter(GenericInnerSingleParamMethodImposter<TValue> target)
                {
                    _target = target;
                }

                public void Invoke(List<TValueTarget> value)
                {
                    _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TValueTarget>, global::System.Collections.Generic.List<TValue>>(value));
                }

                public bool HasMatchingSetup(GenericInnerSingleParamArguments<TValueTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue>());
                }

                IGenericInnerSingleParamMethodImposter<TValueTarget1>? IGenericInnerSingleParamMethodImposter.As<TValueTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericInnerSingleParamArguments<TValue> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericInnerSingleParamMethodInvocationsSetup<TValue>? FindMatchingSetup(GenericInnerSingleParamArguments<TValue> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public void Invoke(global::System.Collections.Generic.List<TValue> value)
            {
                var arguments = new GenericInnerSingleParamArguments<TValue>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericInnerSingleParamMethodInvocationsSetup<TValue>.DefaultInvocationSetup;
                try
                {
                    matchingSetup.Invoke(value);
                    _genericInnerSingleParamMethodInvocationHistoryCollection.Add(new GenericInnerSingleParamMethodInvocationHistory<TValue>(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _genericInnerSingleParamMethodInvocationHistoryCollection.Add(new GenericInnerSingleParamMethodInvocationHistory<TValue>(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericInnerSingleParamMethodImposterBuilder<TValue>
            {
                private readonly GenericInnerSingleParamMethodImposterCollection _imposterCollection;
                private readonly GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection;
                private readonly GenericInnerSingleParamArgumentsCriteria<TValue> _argumentsCriteria;
                private GenericInnerSingleParamMethodInvocationsSetup<TValue>? _existingInvocationSetup;
                public Builder(GenericInnerSingleParamMethodImposterCollection _imposterCollection, GenericInnerSingleParamMethodInvocationHistoryCollection _genericInnerSingleParamMethodInvocationHistoryCollection, GenericInnerSingleParamArgumentsCriteria<TValue> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericInnerSingleParamMethodInvocationHistoryCollection = _genericInnerSingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericInnerSingleParamMethodInvocationsSetup<TValue> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericInnerSingleParamMethodInvocationsSetup<TValue>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.Throws(GenericInnerSingleParamExceptionGeneratorDelegate<TValue> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.CallBefore(GenericInnerSingleParamCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericInnerSingleParamMethodInvocationsSetup<TValue> IGenericInnerSingleParamMethodInvocationsSetup<TValue>.CallAfter(GenericInnerSingleParamCallbackDelegate<TValue> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                void GenericInnerSingleParamMethodInvocationVerifier<TValue>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericInnerSingleParamMethodInvocationHistoryCollection.Count<TValue>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.GenericOutParam<TValue, TResult>(out TValue value)
        public delegate TResult GenericOutParamDelegate<TValue, TResult>(out TValue value);
        // TResult IMethodSetupFeatureSut.GenericOutParam<TValue, TResult>(out TValue value)
        public delegate void GenericOutParamCallbackDelegate<TValue, TResult>(out TValue value);
        // TResult IMethodSetupFeatureSut.GenericOutParam<TValue, TResult>(out TValue value)
        public delegate System.Exception GenericOutParamExceptionGeneratorDelegate<TValue, TResult>(out TValue value);
        public interface IGenericOutParamMethodInvocationHistory
        {
            bool Matches<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericOutParamMethodInvocationHistory<TValue, TResult> : IGenericOutParamMethodInvocationHistory
        {
            internal TResult Result;
            internal System.Exception Exception;
            public GenericOutParamMethodInvocationHistory(TResult Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget, TResultTarget>()
            {
                return (typeof(TValueTarget) == typeof(TValue)) && (typeof(TResult) == typeof(TResultTarget));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericOutParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericOutParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericOutParamMethodInvocationHistory>();
            internal void Add(IGenericOutParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>()
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>());
            }
        }

        internal class GenericOutParamMethodImposterCollection
        {
            private readonly GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection;
            public GenericOutParamMethodImposterCollection(GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection)
            {
                this._genericOutParamMethodInvocationHistoryCollection = _genericOutParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericOutParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericOutParamMethodImposter>();
            internal GenericOutParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new GenericOutParamMethodImposter<TValue, TResult>(_genericOutParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericOutParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>()
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup()) ?? AddNew<TValue, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.GenericOutParam<TValue, TResult>(out TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericOutParamMethodInvocationsSetup<TValue, TResult> : IGenericOutParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static GenericOutParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new GenericOutParamMethodInvocationsSetup<TValue, TResult>();
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
                return default;
            }

            public GenericOutParamMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out TValue value)
            {
                value = default(TValue);
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericOutParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    return value_1;
                };
                return this;
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw new TException();
                };
                return this;
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw exception;
                };
                return this;
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericOutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out TValue value) =>
                {
                    throw exceptionGenerator(out value);
                };
                return this;
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericOutParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericOutParamCallbackDelegate<TValue, TResult> callback)
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
                internal GenericOutParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal GenericOutParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal GenericOutParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericOutParamMethodInvocationsSetup<TValue, TResult>
        {
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception exception);
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> Throws(GenericOutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> CallBefore(GenericOutParamCallbackDelegate<TValue, TResult> callback);
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> CallAfter(GenericOutParamCallbackDelegate<TValue, TResult> callback);
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> Returns(GenericOutParamDelegate<TValue, TResult> resultGenerator);
            IGenericOutParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value_1);
        }

        internal interface IGenericOutParamMethodImposter
        {
            IGenericOutParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericOutParamMethodImposter<TValue, TResult> : IGenericOutParamMethodImposter
        {
            TResult Invoke(out TValue value);
            bool HasMatchingSetup();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericOutParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.GenericOutParam<TValue, TResult>(out TValue value)
        public interface IGenericOutParamMethodImposterBuilder<TValue, TResult> : IGenericOutParamMethodInvocationsSetup<TValue, TResult>, GenericOutParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericOutParamMethodImposter<TValue, TResult> : IGenericOutParamMethodImposter<TValue, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericOutParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericOutParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection;
            public GenericOutParamMethodImposter(GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection)
            {
                this._genericOutParamMethodInvocationHistoryCollection = _genericOutParamMethodInvocationHistoryCollection;
            }

            IGenericOutParamMethodImposter<TValueTarget, TResultTarget>? IGenericOutParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(TValue).IsAssignableTo(typeof(TValueTarget)) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGenericOutParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly GenericOutParamMethodImposter<TValue, TResult> _target;
                public Adapter(GenericOutParamMethodImposter<TValue, TResult> target)
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

                IGenericOutParamMethodImposter<TValueTarget1, TResultTarget1>? IGenericOutParamMethodImposter.As<TValueTarget1, TResultTarget1>()
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

            private GenericOutParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public TResult Invoke(out TValue value)
            {
                var matchingSetup = FindMatchingSetup() ?? GenericOutParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out value);
                    _genericOutParamMethodInvocationHistoryCollection.Add(new GenericOutParamMethodInvocationHistory<TValue, TResult>(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericOutParamMethodInvocationHistoryCollection.Add(new GenericOutParamMethodInvocationHistory<TValue, TResult>(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericOutParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly GenericOutParamMethodImposterCollection _imposterCollection;
                private readonly GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection;
                private GenericOutParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(GenericOutParamMethodImposterCollection _imposterCollection, GenericOutParamMethodInvocationHistoryCollection _genericOutParamMethodInvocationHistoryCollection)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericOutParamMethodInvocationHistoryCollection = _genericOutParamMethodInvocationHistoryCollection;
                }

                private IGenericOutParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericOutParamMethodInvocationsSetup<TValue, TResult>();
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericOutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericOutParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericOutParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericOutParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericOutParamMethodInvocationsSetup<TValue, TResult> IGenericOutParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void GenericOutParamMethodInvocationVerifier<TValue, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericOutParamMethodInvocationHistoryCollection.Count<TValue, TResult>();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerOutParam<TValue, TResult>(out List<TValue> value)
        public delegate global::System.Collections.Generic.Stack<TResult> GenericInnerOutParamDelegate<TValue, TResult>(out global::System.Collections.Generic.List<TValue> value);
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerOutParam<TValue, TResult>(out List<TValue> value)
        public delegate void GenericInnerOutParamCallbackDelegate<TValue, TResult>(out global::System.Collections.Generic.List<TValue> value);
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerOutParam<TValue, TResult>(out List<TValue> value)
        public delegate System.Exception GenericInnerOutParamExceptionGeneratorDelegate<TValue, TResult>(out global::System.Collections.Generic.List<TValue> value);
        public interface IGenericInnerOutParamMethodInvocationHistory
        {
            bool Matches<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerOutParamMethodInvocationHistory<TValue, TResult> : IGenericInnerOutParamMethodInvocationHistory
        {
            internal global::System.Collections.Generic.Stack<TResult> Result;
            internal System.Exception Exception;
            public GenericInnerOutParamMethodInvocationHistory(global::System.Collections.Generic.Stack<TResult> Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget, TResultTarget>()
            {
                return (typeof(global::System.Collections.Generic.List<TValueTarget>) == typeof(global::System.Collections.Generic.List<TValue>)) && (typeof(global::System.Collections.Generic.Stack<TResult>) == typeof(global::System.Collections.Generic.Stack<TResultTarget>));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerOutParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerOutParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerOutParamMethodInvocationHistory>();
            internal void Add(IGenericInnerOutParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>()
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>());
            }
        }

        internal class GenericInnerOutParamMethodImposterCollection
        {
            private readonly GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection;
            public GenericInnerOutParamMethodImposterCollection(GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection)
            {
                this._genericInnerOutParamMethodInvocationHistoryCollection = _genericInnerOutParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerOutParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerOutParamMethodImposter>();
            internal GenericInnerOutParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new GenericInnerOutParamMethodImposter<TValue, TResult>(_genericInnerOutParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericInnerOutParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>()
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup()) ?? AddNew<TValue, TResult>();
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerOutParam<TValue, TResult>(out List<TValue> value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericInnerOutParamMethodInvocationsSetup<TValue, TResult> : IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static GenericInnerOutParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>();
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

            internal static global::System.Collections.Generic.Stack<TResult> DefaultResultGenerator(out global::System.Collections.Generic.List<TValue> value)
            {
                InitializeOutParametersWithDefaultValues(out value);
                return default;
            }

            public GenericInnerOutParamMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            private static void InitializeOutParametersWithDefaultValues(out global::System.Collections.Generic.List<TValue> value)
            {
                value = default(global::System.Collections.Generic.List<TValue>);
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericInnerOutParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Returns(global::System.Collections.Generic.Stack<TResult> value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out global::System.Collections.Generic.List<TValue> value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    return value_1;
                };
                return this;
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out global::System.Collections.Generic.List<TValue> value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw new TException();
                };
                return this;
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out global::System.Collections.Generic.List<TValue> value) =>
                {
                    InitializeOutParametersWithDefaultValues(out value);
                    throw exception;
                };
                return this;
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericInnerOutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (out global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw exceptionGenerator(out value);
                };
                return this;
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericInnerOutParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericInnerOutParamCallbackDelegate<TValue, TResult> callback)
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

            public global::System.Collections.Generic.Stack<TResult> Invoke(out global::System.Collections.Generic.List<TValue> value)
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
                internal GenericInnerOutParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal GenericInnerOutParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal GenericInnerOutParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>
        {
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception exception);
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> Throws(GenericInnerOutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> CallBefore(GenericInnerOutParamCallbackDelegate<TValue, TResult> callback);
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> CallAfter(GenericInnerOutParamCallbackDelegate<TValue, TResult> callback);
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> Returns(GenericInnerOutParamDelegate<TValue, TResult> resultGenerator);
            IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> Returns(global::System.Collections.Generic.Stack<TResult> value_1);
        }

        internal interface IGenericInnerOutParamMethodImposter
        {
            IGenericInnerOutParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericInnerOutParamMethodImposter<TValue, TResult> : IGenericInnerOutParamMethodImposter
        {
            global::System.Collections.Generic.Stack<TResult> Invoke(out global::System.Collections.Generic.List<TValue> value);
            bool HasMatchingSetup();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericInnerOutParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerOutParam<TValue, TResult>(out List<TValue> value)
        public interface IGenericInnerOutParamMethodImposterBuilder<TValue, TResult> : IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>, GenericInnerOutParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerOutParamMethodImposter<TValue, TResult> : IGenericInnerOutParamMethodImposter<TValue, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection;
            public GenericInnerOutParamMethodImposter(GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection)
            {
                this._genericInnerOutParamMethodInvocationHistoryCollection = _genericInnerOutParamMethodInvocationHistoryCollection;
            }

            IGenericInnerOutParamMethodImposter<TValueTarget, TResultTarget>? IGenericInnerOutParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(global::System.Collections.Generic.List<TValue>).IsAssignableTo(typeof(global::System.Collections.Generic.List<TValueTarget>)) && typeof(global::System.Collections.Generic.Stack<TResult>).IsAssignableTo(typeof(global::System.Collections.Generic.Stack<TResultTarget>)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGenericInnerOutParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly GenericInnerOutParamMethodImposter<TValue, TResult> _target;
                public Adapter(GenericInnerOutParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public global::System.Collections.Generic.Stack<TResultTarget> Invoke(out List<TValueTarget> value)
                {
                    global::System.Collections.Generic.List<TValue> valueAdapted;
                    var result = _target.Invoke(out valueAdapted);
                    value = Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TValue>, global::System.Collections.Generic.List<TValueTarget>>(valueAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.Stack<TResult>, global::System.Collections.Generic.Stack<TResultTarget>>(result);
                }

                public bool HasMatchingSetup()
                {
                    return _target.HasMatchingSetup();
                }

                IGenericInnerOutParamMethodImposter<TValueTarget1, TResultTarget1>? IGenericInnerOutParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            private static void InitializeOutParametersWithDefaultValues(out global::System.Collections.Generic.List<TValue> value)
            {
                value = default(global::System.Collections.Generic.List<TValue>);
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public global::System.Collections.Generic.Stack<TResult> Invoke(out global::System.Collections.Generic.List<TValue> value)
            {
                var matchingSetup = FindMatchingSetup() ?? GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(out value);
                    _genericInnerOutParamMethodInvocationHistoryCollection.Add(new GenericInnerOutParamMethodInvocationHistory<TValue, TResult>(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericInnerOutParamMethodInvocationHistoryCollection.Add(new GenericInnerOutParamMethodInvocationHistory<TValue, TResult>(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericInnerOutParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly GenericInnerOutParamMethodImposterCollection _imposterCollection;
                private readonly GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection;
                private GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(GenericInnerOutParamMethodImposterCollection _imposterCollection, GenericInnerOutParamMethodInvocationHistoryCollection _genericInnerOutParamMethodInvocationHistoryCollection)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericInnerOutParamMethodInvocationHistoryCollection = _genericInnerOutParamMethodInvocationHistoryCollection;
                }

                private IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericInnerOutParamMethodInvocationsSetup<TValue, TResult>();
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericInnerOutParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericInnerOutParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericInnerOutParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericInnerOutParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult> IGenericInnerOutParamMethodInvocationsSetup<TValue, TResult>.Returns(global::System.Collections.Generic.Stack<TResult> value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void GenericInnerOutParamMethodInvocationVerifier<TValue, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericInnerOutParamMethodInvocationHistoryCollection.Count<TValue, TResult>();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.GenericRefParam<TValue, TResult>(ref TValue value)
        public delegate TResult GenericRefParamDelegate<TValue, TResult>(ref TValue value);
        // TResult IMethodSetupFeatureSut.GenericRefParam<TValue, TResult>(ref TValue value)
        public delegate void GenericRefParamCallbackDelegate<TValue, TResult>(ref TValue value);
        // TResult IMethodSetupFeatureSut.GenericRefParam<TValue, TResult>(ref TValue value)
        public delegate System.Exception GenericRefParamExceptionGeneratorDelegate<TValue, TResult>(ref TValue value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericRefParamArguments<TValue, TResult>
        {
            public TValue value;
            internal GenericRefParamArguments(TValue value)
            {
                this.value = value;
            }

            public GenericRefParamArguments<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericRefParamArguments<TValueTarget, TResultTarget>(TypeCaster.Cast<TValue, TValueTarget>(value));
            }
        }

        // TResult IMethodSetupFeatureSut.GenericRefParam<TValue, TResult>(ref TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericRefParamArgumentsCriteria<TValue, TResult>
        {
            public Imposter.Abstractions.Arg<TValue> value { get; }

            public GenericRefParamArgumentsCriteria(Imposter.Abstractions.Arg<TValue> value)
            {
                this.value = value;
            }

            public bool Matches(GenericRefParamArguments<TValue, TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public GenericRefParamArgumentsCriteria<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericRefParamArgumentsCriteria<TValueTarget, TResultTarget>(Imposter.Abstractions.Arg<TValueTarget>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget, TValue>(it, out TValue valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGenericRefParamMethodInvocationHistory
        {
            bool Matches<TValueTarget, TResultTarget>(GenericRefParamArgumentsCriteria<TValueTarget, TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericRefParamMethodInvocationHistory<TValue, TResult> : IGenericRefParamMethodInvocationHistory
        {
            internal GenericRefParamArguments<TValue, TResult> Arguments;
            internal TResult Result;
            internal System.Exception Exception;
            public GenericRefParamMethodInvocationHistory(GenericRefParamArguments<TValue, TResult> Arguments, TResult Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget, TResultTarget>(GenericRefParamArgumentsCriteria<TValueTarget, TResultTarget> criteria)
            {
                return ((typeof(TValueTarget) == typeof(TValue)) && (typeof(TResult) == typeof(TResultTarget))) && criteria.As<TValue, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericRefParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericRefParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericRefParamMethodInvocationHistory>();
            internal void Add(IGenericRefParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>(GenericRefParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>(argumentsCriteria));
            }
        }

        internal class GenericRefParamMethodImposterCollection
        {
            private readonly GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection;
            public GenericRefParamMethodImposterCollection(GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection)
            {
                this._genericRefParamMethodInvocationHistoryCollection = _genericRefParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericRefParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericRefParamMethodImposter>();
            internal GenericRefParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new GenericRefParamMethodImposter<TValue, TResult>(_genericRefParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericRefParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>(GenericRefParamArguments<TValue, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.GenericRefParam<TValue, TResult>(ref TValue value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericRefParamMethodInvocationsSetup<TValue, TResult> : IGenericRefParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static GenericRefParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new GenericRefParamMethodInvocationsSetup<TValue, TResult>(new GenericRefParamArgumentsCriteria<TValue, TResult>(Imposter.Abstractions.Arg<TValue>.Any()));
            internal GenericRefParamArgumentsCriteria<TValue, TResult> ArgumentsCriteria { get; }

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
                return default;
            }

            public GenericRefParamMethodInvocationsSetup(GenericRefParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericRefParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    return value_1;
                };
                return this;
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericRefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref TValue value) =>
                {
                    throw exceptionGenerator(ref value);
                };
                return this;
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericRefParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericRefParamCallbackDelegate<TValue, TResult> callback)
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
                internal GenericRefParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal GenericRefParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal GenericRefParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericRefParamMethodInvocationsSetup<TValue, TResult>
        {
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception exception);
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> Throws(GenericRefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> CallBefore(GenericRefParamCallbackDelegate<TValue, TResult> callback);
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> CallAfter(GenericRefParamCallbackDelegate<TValue, TResult> callback);
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> Returns(GenericRefParamDelegate<TValue, TResult> resultGenerator);
            IGenericRefParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value_1);
        }

        internal interface IGenericRefParamMethodImposter
        {
            IGenericRefParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericRefParamMethodImposter<TValue, TResult> : IGenericRefParamMethodImposter
        {
            TResult Invoke(ref TValue value);
            bool HasMatchingSetup(GenericRefParamArguments<TValue, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericRefParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.GenericRefParam<TValue, TResult>(ref TValue value)
        public interface IGenericRefParamMethodImposterBuilder<TValue, TResult> : IGenericRefParamMethodInvocationsSetup<TValue, TResult>, GenericRefParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericRefParamMethodImposter<TValue, TResult> : IGenericRefParamMethodImposter<TValue, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericRefParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericRefParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection;
            public GenericRefParamMethodImposter(GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection)
            {
                this._genericRefParamMethodInvocationHistoryCollection = _genericRefParamMethodInvocationHistoryCollection;
            }

            IGenericRefParamMethodImposter<TValueTarget, TResultTarget>? IGenericRefParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(TValueTarget) == typeof(TValue) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGenericRefParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly GenericRefParamMethodImposter<TValue, TResult> _target;
                public Adapter(GenericRefParamMethodImposter<TValue, TResult> target)
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

                public bool HasMatchingSetup(GenericRefParamArguments<TValueTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue, TResult>());
                }

                IGenericRefParamMethodImposter<TValueTarget1, TResultTarget1>? IGenericRefParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericRefParamArguments<TValue, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericRefParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup(GenericRefParamArguments<TValue, TResult> arguments)
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
                var arguments = new GenericRefParamArguments<TValue, TResult>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericRefParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ref value);
                    _genericRefParamMethodInvocationHistoryCollection.Add(new GenericRefParamMethodInvocationHistory<TValue, TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericRefParamMethodInvocationHistoryCollection.Add(new GenericRefParamMethodInvocationHistory<TValue, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericRefParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly GenericRefParamMethodImposterCollection _imposterCollection;
                private readonly GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection;
                private readonly GenericRefParamArgumentsCriteria<TValue, TResult> _argumentsCriteria;
                private GenericRefParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(GenericRefParamMethodImposterCollection _imposterCollection, GenericRefParamMethodInvocationHistoryCollection _genericRefParamMethodInvocationHistoryCollection, GenericRefParamArgumentsCriteria<TValue, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericRefParamMethodInvocationHistoryCollection = _genericRefParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericRefParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericRefParamMethodInvocationsSetup<TValue, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericRefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericRefParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericRefParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericRefParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericRefParamMethodInvocationsSetup<TValue, TResult> IGenericRefParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void GenericRefParamMethodInvocationVerifier<TValue, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericRefParamMethodInvocationHistoryCollection.Count<TValue, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerRefParam<TValue, TResult>(ref List<TValue> value)
        public delegate global::System.Collections.Generic.Stack<TResult> GenericInnerRefParamDelegate<TValue, TResult>(ref global::System.Collections.Generic.List<TValue> value);
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerRefParam<TValue, TResult>(ref List<TValue> value)
        public delegate void GenericInnerRefParamCallbackDelegate<TValue, TResult>(ref global::System.Collections.Generic.List<TValue> value);
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerRefParam<TValue, TResult>(ref List<TValue> value)
        public delegate System.Exception GenericInnerRefParamExceptionGeneratorDelegate<TValue, TResult>(ref global::System.Collections.Generic.List<TValue> value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericInnerRefParamArguments<TValue, TResult>
        {
            public global::System.Collections.Generic.List<TValue> value;
            internal GenericInnerRefParamArguments(global::System.Collections.Generic.List<TValue> value)
            {
                this.value = value;
            }

            public GenericInnerRefParamArguments<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericInnerRefParamArguments<TValueTarget, TResultTarget>(TypeCaster.Cast<global::System.Collections.Generic.List<TValue>, global::System.Collections.Generic.List<TValueTarget>>(value));
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerRefParam<TValue, TResult>(ref List<TValue> value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericInnerRefParamArgumentsCriteria<TValue, TResult>
        {
            public Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>> value { get; }

            public GenericInnerRefParamArgumentsCriteria(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>> value)
            {
                this.value = value;
            }

            public bool Matches(GenericInnerRefParamArguments<TValue, TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public GenericInnerRefParamArgumentsCriteria<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericInnerRefParamArgumentsCriteria<TValueTarget, TResultTarget>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValueTarget>>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<global::System.Collections.Generic.List<TValueTarget>, global::System.Collections.Generic.List<TValue>>(it, out global::System.Collections.Generic.List<TValue> valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGenericInnerRefParamMethodInvocationHistory
        {
            bool Matches<TValueTarget, TResultTarget>(GenericInnerRefParamArgumentsCriteria<TValueTarget, TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerRefParamMethodInvocationHistory<TValue, TResult> : IGenericInnerRefParamMethodInvocationHistory
        {
            internal GenericInnerRefParamArguments<TValue, TResult> Arguments;
            internal global::System.Collections.Generic.Stack<TResult> Result;
            internal System.Exception Exception;
            public GenericInnerRefParamMethodInvocationHistory(GenericInnerRefParamArguments<TValue, TResult> Arguments, global::System.Collections.Generic.Stack<TResult> Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget, TResultTarget>(GenericInnerRefParamArgumentsCriteria<TValueTarget, TResultTarget> criteria)
            {
                return ((typeof(global::System.Collections.Generic.List<TValueTarget>) == typeof(global::System.Collections.Generic.List<TValue>)) && (typeof(global::System.Collections.Generic.Stack<TResult>) == typeof(global::System.Collections.Generic.Stack<TResultTarget>))) && criteria.As<TValue, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerRefParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerRefParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerRefParamMethodInvocationHistory>();
            internal void Add(IGenericInnerRefParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>(GenericInnerRefParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>(argumentsCriteria));
            }
        }

        internal class GenericInnerRefParamMethodImposterCollection
        {
            private readonly GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection;
            public GenericInnerRefParamMethodImposterCollection(GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection)
            {
                this._genericInnerRefParamMethodInvocationHistoryCollection = _genericInnerRefParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerRefParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerRefParamMethodImposter>();
            internal GenericInnerRefParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new GenericInnerRefParamMethodImposter<TValue, TResult>(_genericInnerRefParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericInnerRefParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>(GenericInnerRefParamArguments<TValue, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue, TResult>();
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerRefParam<TValue, TResult>(ref List<TValue> value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericInnerRefParamMethodInvocationsSetup<TValue, TResult> : IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static GenericInnerRefParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>(new GenericInnerRefParamArgumentsCriteria<TValue, TResult>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>>.Any()));
            internal GenericInnerRefParamArgumentsCriteria<TValue, TResult> ArgumentsCriteria { get; }

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

            internal static global::System.Collections.Generic.Stack<TResult> DefaultResultGenerator(ref global::System.Collections.Generic.List<TValue> value)
            {
                return default;
            }

            public GenericInnerRefParamMethodInvocationsSetup(GenericInnerRefParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericInnerRefParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Returns(global::System.Collections.Generic.Stack<TResult> value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref global::System.Collections.Generic.List<TValue> value) =>
                {
                    return value_1;
                };
                return this;
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericInnerRefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (ref global::System.Collections.Generic.List<TValue> value) =>
                {
                    throw exceptionGenerator(ref value);
                };
                return this;
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericInnerRefParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericInnerRefParamCallbackDelegate<TValue, TResult> callback)
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

            public global::System.Collections.Generic.Stack<TResult> Invoke(ref global::System.Collections.Generic.List<TValue> value)
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
                internal GenericInnerRefParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal GenericInnerRefParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal GenericInnerRefParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>
        {
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception exception);
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> Throws(GenericInnerRefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> CallBefore(GenericInnerRefParamCallbackDelegate<TValue, TResult> callback);
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> CallAfter(GenericInnerRefParamCallbackDelegate<TValue, TResult> callback);
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> Returns(GenericInnerRefParamDelegate<TValue, TResult> resultGenerator);
            IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> Returns(global::System.Collections.Generic.Stack<TResult> value_1);
        }

        internal interface IGenericInnerRefParamMethodImposter
        {
            IGenericInnerRefParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericInnerRefParamMethodImposter<TValue, TResult> : IGenericInnerRefParamMethodImposter
        {
            global::System.Collections.Generic.Stack<TResult> Invoke(ref global::System.Collections.Generic.List<TValue> value);
            bool HasMatchingSetup(GenericInnerRefParamArguments<TValue, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericInnerRefParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerRefParam<TValue, TResult>(ref List<TValue> value)
        public interface IGenericInnerRefParamMethodImposterBuilder<TValue, TResult> : IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>, GenericInnerRefParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerRefParamMethodImposter<TValue, TResult> : IGenericInnerRefParamMethodImposter<TValue, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection;
            public GenericInnerRefParamMethodImposter(GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection)
            {
                this._genericInnerRefParamMethodInvocationHistoryCollection = _genericInnerRefParamMethodInvocationHistoryCollection;
            }

            IGenericInnerRefParamMethodImposter<TValueTarget, TResultTarget>? IGenericInnerRefParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(global::System.Collections.Generic.List<TValueTarget>) == typeof(global::System.Collections.Generic.List<TValue>) && typeof(global::System.Collections.Generic.Stack<TResult>).IsAssignableTo(typeof(global::System.Collections.Generic.Stack<TResultTarget>)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGenericInnerRefParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly GenericInnerRefParamMethodImposter<TValue, TResult> _target;
                public Adapter(GenericInnerRefParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public global::System.Collections.Generic.Stack<TResultTarget> Invoke(ref List<TValueTarget> value)
                {
                    global::System.Collections.Generic.List<TValue> valueAdapted = Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TValueTarget>, global::System.Collections.Generic.List<TValue>>(value);
                    var result = _target.Invoke(ref valueAdapted);
                    value = Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TValue>, global::System.Collections.Generic.List<TValueTarget>>(valueAdapted);
                    return Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.Stack<TResult>, global::System.Collections.Generic.Stack<TResultTarget>>(result);
                }

                public bool HasMatchingSetup(GenericInnerRefParamArguments<TValueTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue, TResult>());
                }

                IGenericInnerRefParamMethodImposter<TValueTarget1, TResultTarget1>? IGenericInnerRefParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericInnerRefParamArguments<TValue, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup(GenericInnerRefParamArguments<TValue, TResult> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public global::System.Collections.Generic.Stack<TResult> Invoke(ref global::System.Collections.Generic.List<TValue> value)
            {
                var arguments = new GenericInnerRefParamArguments<TValue, TResult>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(ref value);
                    _genericInnerRefParamMethodInvocationHistoryCollection.Add(new GenericInnerRefParamMethodInvocationHistory<TValue, TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericInnerRefParamMethodInvocationHistoryCollection.Add(new GenericInnerRefParamMethodInvocationHistory<TValue, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericInnerRefParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly GenericInnerRefParamMethodImposterCollection _imposterCollection;
                private readonly GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection;
                private readonly GenericInnerRefParamArgumentsCriteria<TValue, TResult> _argumentsCriteria;
                private GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(GenericInnerRefParamMethodImposterCollection _imposterCollection, GenericInnerRefParamMethodInvocationHistoryCollection _genericInnerRefParamMethodInvocationHistoryCollection, GenericInnerRefParamArgumentsCriteria<TValue, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericInnerRefParamMethodInvocationHistoryCollection = _genericInnerRefParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericInnerRefParamMethodInvocationsSetup<TValue, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericInnerRefParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericInnerRefParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericInnerRefParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericInnerRefParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult> IGenericInnerRefParamMethodInvocationsSetup<TValue, TResult>.Returns(global::System.Collections.Generic.Stack<TResult> value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void GenericInnerRefParamMethodInvocationVerifier<TValue, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericInnerRefParamMethodInvocationHistoryCollection.Count<TValue, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.GenericParamsParam<TValue, TResult>(params TValue[] value)
        public delegate TResult GenericParamsParamDelegate<TValue, TResult>(TValue[] value);
        // TResult IMethodSetupFeatureSut.GenericParamsParam<TValue, TResult>(params TValue[] value)
        public delegate void GenericParamsParamCallbackDelegate<TValue, TResult>(TValue[] value);
        // TResult IMethodSetupFeatureSut.GenericParamsParam<TValue, TResult>(params TValue[] value)
        public delegate System.Exception GenericParamsParamExceptionGeneratorDelegate<TValue, TResult>(TValue[] value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericParamsParamArguments<TValue, TResult>
        {
            public TValue[] value;
            internal GenericParamsParamArguments(TValue[] value)
            {
                this.value = value;
            }

            public GenericParamsParamArguments<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericParamsParamArguments<TValueTarget, TResultTarget>(TypeCaster.Cast<TValue[], TValueTarget[]>(value));
            }
        }

        // TResult IMethodSetupFeatureSut.GenericParamsParam<TValue, TResult>(params TValue[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericParamsParamArgumentsCriteria<TValue, TResult>
        {
            public Imposter.Abstractions.Arg<TValue[]> value { get; }

            public GenericParamsParamArgumentsCriteria(Imposter.Abstractions.Arg<TValue[]> value)
            {
                this.value = value;
            }

            public bool Matches(GenericParamsParamArguments<TValue, TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public GenericParamsParamArgumentsCriteria<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericParamsParamArgumentsCriteria<TValueTarget, TResultTarget>(Imposter.Abstractions.Arg<TValueTarget[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<TValueTarget[], TValue[]>(it, out TValue[] valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGenericParamsParamMethodInvocationHistory
        {
            bool Matches<TValueTarget, TResultTarget>(GenericParamsParamArgumentsCriteria<TValueTarget, TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericParamsParamMethodInvocationHistory<TValue, TResult> : IGenericParamsParamMethodInvocationHistory
        {
            internal GenericParamsParamArguments<TValue, TResult> Arguments;
            internal TResult Result;
            internal System.Exception Exception;
            public GenericParamsParamMethodInvocationHistory(GenericParamsParamArguments<TValue, TResult> Arguments, TResult Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget, TResultTarget>(GenericParamsParamArgumentsCriteria<TValueTarget, TResultTarget> criteria)
            {
                return ((typeof(TValueTarget[]) == typeof(TValue[])) && (typeof(TResult) == typeof(TResultTarget))) && criteria.As<TValue, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericParamsParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericParamsParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericParamsParamMethodInvocationHistory>();
            internal void Add(IGenericParamsParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>(GenericParamsParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>(argumentsCriteria));
            }
        }

        internal class GenericParamsParamMethodImposterCollection
        {
            private readonly GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection;
            public GenericParamsParamMethodImposterCollection(GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection)
            {
                this._genericParamsParamMethodInvocationHistoryCollection = _genericParamsParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericParamsParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericParamsParamMethodImposter>();
            internal GenericParamsParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new GenericParamsParamMethodImposter<TValue, TResult>(_genericParamsParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericParamsParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>(GenericParamsParamArguments<TValue, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue, TResult>();
            }
        }

        // TResult IMethodSetupFeatureSut.GenericParamsParam<TValue, TResult>(params TValue[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericParamsParamMethodInvocationsSetup<TValue, TResult> : IGenericParamsParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static GenericParamsParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new GenericParamsParamMethodInvocationsSetup<TValue, TResult>(new GenericParamsParamArgumentsCriteria<TValue, TResult>(Imposter.Abstractions.Arg<TValue[]>.Any()));
            internal GenericParamsParamArgumentsCriteria<TValue, TResult> ArgumentsCriteria { get; }

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
                return default;
            }

            public GenericParamsParamMethodInvocationsSetup(GenericParamsParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericParamsParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    return value_1;
                };
                return this;
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (TValue[] value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericParamsParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericParamsParamCallbackDelegate<TValue, TResult> callback)
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
                internal GenericParamsParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal GenericParamsParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal GenericParamsParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericParamsParamMethodInvocationsSetup<TValue, TResult>
        {
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception exception);
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> Throws(GenericParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> CallBefore(GenericParamsParamCallbackDelegate<TValue, TResult> callback);
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> CallAfter(GenericParamsParamCallbackDelegate<TValue, TResult> callback);
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> Returns(GenericParamsParamDelegate<TValue, TResult> resultGenerator);
            IGenericParamsParamMethodInvocationsSetup<TValue, TResult> Returns(TResult value_1);
        }

        internal interface IGenericParamsParamMethodImposter
        {
            IGenericParamsParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericParamsParamMethodImposter<TValue, TResult> : IGenericParamsParamMethodImposter
        {
            TResult Invoke(TValue[] value);
            bool HasMatchingSetup(GenericParamsParamArguments<TValue, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericParamsParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodSetupFeatureSut.GenericParamsParam<TValue, TResult>(params TValue[] value)
        public interface IGenericParamsParamMethodImposterBuilder<TValue, TResult> : IGenericParamsParamMethodInvocationsSetup<TValue, TResult>, GenericParamsParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericParamsParamMethodImposter<TValue, TResult> : IGenericParamsParamMethodImposter<TValue, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericParamsParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericParamsParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection;
            public GenericParamsParamMethodImposter(GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection)
            {
                this._genericParamsParamMethodInvocationHistoryCollection = _genericParamsParamMethodInvocationHistoryCollection;
            }

            IGenericParamsParamMethodImposter<TValueTarget, TResultTarget>? IGenericParamsParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(TValueTarget[]).IsAssignableTo(typeof(TValue[])) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGenericParamsParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly GenericParamsParamMethodImposter<TValue, TResult> _target;
                public Adapter(GenericParamsParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(TValueTarget[] value)
                {
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<TValueTarget[], TValue[]>(value));
                    return Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(GenericParamsParamArguments<TValueTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue, TResult>());
                }

                IGenericParamsParamMethodImposter<TValueTarget1, TResultTarget1>? IGenericParamsParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericParamsParamArguments<TValue, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericParamsParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup(GenericParamsParamArguments<TValue, TResult> arguments)
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
                var arguments = new GenericParamsParamArguments<TValue, TResult>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericParamsParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value);
                    _genericParamsParamMethodInvocationHistoryCollection.Add(new GenericParamsParamMethodInvocationHistory<TValue, TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericParamsParamMethodInvocationHistoryCollection.Add(new GenericParamsParamMethodInvocationHistory<TValue, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericParamsParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly GenericParamsParamMethodImposterCollection _imposterCollection;
                private readonly GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection;
                private readonly GenericParamsParamArgumentsCriteria<TValue, TResult> _argumentsCriteria;
                private GenericParamsParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(GenericParamsParamMethodImposterCollection _imposterCollection, GenericParamsParamMethodInvocationHistoryCollection _genericParamsParamMethodInvocationHistoryCollection, GenericParamsParamArgumentsCriteria<TValue, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericParamsParamMethodInvocationHistoryCollection = _genericParamsParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericParamsParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericParamsParamMethodInvocationsSetup<TValue, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericParamsParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericParamsParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericParamsParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericParamsParamMethodInvocationsSetup<TValue, TResult> IGenericParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(TResult value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void GenericParamsParamMethodInvocationVerifier<TValue, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericParamsParamMethodInvocationHistoryCollection.Count<TValue, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value)
        public delegate global::System.Collections.Generic.Stack<TResult> GenericInnerParamsParamDelegate<TValue, TResult>(global::System.Collections.Generic.List<TValue>[] value);
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value)
        public delegate void GenericInnerParamsParamCallbackDelegate<TValue, TResult>(global::System.Collections.Generic.List<TValue>[] value);
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value)
        public delegate System.Exception GenericInnerParamsParamExceptionGeneratorDelegate<TValue, TResult>(global::System.Collections.Generic.List<TValue>[] value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericInnerParamsParamArguments<TValue, TResult>
        {
            public global::System.Collections.Generic.List<TValue>[] value;
            internal GenericInnerParamsParamArguments(global::System.Collections.Generic.List<TValue>[] value)
            {
                this.value = value;
            }

            public GenericInnerParamsParamArguments<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericInnerParamsParamArguments<TValueTarget, TResultTarget>(TypeCaster.Cast<global::System.Collections.Generic.List<TValue>[], global::System.Collections.Generic.List<TValueTarget>[]>(value));
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericInnerParamsParamArgumentsCriteria<TValue, TResult>
        {
            public Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>[]> value { get; }

            public GenericInnerParamsParamArgumentsCriteria(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>[]> value)
            {
                this.value = value;
            }

            public bool Matches(GenericInnerParamsParamArguments<TValue, TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public GenericInnerParamsParamArgumentsCriteria<TValueTarget, TResultTarget> As<TValueTarget, TResultTarget>()
            {
                return new GenericInnerParamsParamArgumentsCriteria<TValueTarget, TResultTarget>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValueTarget>[]>.Is(it => Imposter.Abstractions.TypeCaster.TryCast<global::System.Collections.Generic.List<TValueTarget>[], global::System.Collections.Generic.List<TValue>[]>(it, out global::System.Collections.Generic.List<TValue>[] valueTarget) && value.Matches(valueTarget)));
            }
        }

        public interface IGenericInnerParamsParamMethodInvocationHistory
        {
            bool Matches<TValueTarget, TResultTarget>(GenericInnerParamsParamArgumentsCriteria<TValueTarget, TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerParamsParamMethodInvocationHistory<TValue, TResult> : IGenericInnerParamsParamMethodInvocationHistory
        {
            internal GenericInnerParamsParamArguments<TValue, TResult> Arguments;
            internal global::System.Collections.Generic.Stack<TResult> Result;
            internal System.Exception Exception;
            public GenericInnerParamsParamMethodInvocationHistory(GenericInnerParamsParamArguments<TValue, TResult> Arguments, global::System.Collections.Generic.Stack<TResult> Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TValueTarget, TResultTarget>(GenericInnerParamsParamArgumentsCriteria<TValueTarget, TResultTarget> criteria)
            {
                return ((typeof(global::System.Collections.Generic.List<TValueTarget>[]) == typeof(global::System.Collections.Generic.List<TValue>[])) && (typeof(global::System.Collections.Generic.Stack<TResult>) == typeof(global::System.Collections.Generic.Stack<TResultTarget>))) && criteria.As<TValue, TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerParamsParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerParamsParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerParamsParamMethodInvocationHistory>();
            internal void Add(IGenericInnerParamsParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TValue, TResult>(GenericInnerParamsParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TValue, TResult>(argumentsCriteria));
            }
        }

        internal class GenericInnerParamsParamMethodImposterCollection
        {
            private readonly GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection;
            public GenericInnerParamsParamMethodImposterCollection(GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection)
            {
                this._genericInnerParamsParamMethodInvocationHistoryCollection = _genericInnerParamsParamMethodInvocationHistoryCollection;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericInnerParamsParamMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericInnerParamsParamMethodImposter>();
            internal GenericInnerParamsParamMethodImposter<TValue, TResult> AddNew<TValue, TResult>()
            {
                var imposter = new GenericInnerParamsParamMethodImposter<TValue, TResult>(_genericInnerParamsParamMethodInvocationHistoryCollection);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericInnerParamsParamMethodImposter<TValue, TResult> GetImposterWithMatchingSetup<TValue, TResult>(GenericInnerParamsParamArguments<TValue, TResult> arguments)
            {
                return _imposters.Select(it => it.As<TValue, TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TValue, TResult>();
            }
        }

        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> : IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>
        {
            internal static GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> DefaultInvocationSetup = new GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>(new GenericInnerParamsParamArgumentsCriteria<TValue, TResult>(Imposter.Abstractions.Arg<global::System.Collections.Generic.List<TValue>[]>.Any()));
            internal GenericInnerParamsParamArgumentsCriteria<TValue, TResult> ArgumentsCriteria { get; }

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

            internal static global::System.Collections.Generic.Stack<TResult> DefaultResultGenerator(global::System.Collections.Generic.List<TValue>[] value)
            {
                return default;
            }

            public GenericInnerParamsParamMethodInvocationsSetup(GenericInnerParamsParamArgumentsCriteria<TValue, TResult> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericInnerParamsParamDelegate<TValue, TResult> resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(global::System.Collections.Generic.Stack<TResult> value_1)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue>[] value) =>
                {
                    return value_1;
                };
                return this;
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue>[] value) =>
                {
                    throw new TException();
                };
                return this;
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue>[] value) =>
                {
                    throw exception;
                };
                return this;
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericInnerParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (global::System.Collections.Generic.List<TValue>[] value) =>
                {
                    throw exceptionGenerator(value);
                };
                return this;
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericInnerParamsParamCallbackDelegate<TValue, TResult> callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericInnerParamsParamCallbackDelegate<TValue, TResult> callback)
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

            public global::System.Collections.Generic.Stack<TResult> Invoke(global::System.Collections.Generic.List<TValue>[] value)
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
                internal GenericInnerParamsParamDelegate<TValue, TResult>? ResultGenerator { get; set; }
                internal GenericInnerParamsParamCallbackDelegate<TValue, TResult>? CallBefore { get; set; }
                internal GenericInnerParamsParamCallbackDelegate<TValue, TResult>? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>
        {
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> Throws<TException>()
                where TException : Exception, new();
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> Throws(System.Exception exception);
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> Throws(GenericInnerParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator);
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> CallBefore(GenericInnerParamsParamCallbackDelegate<TValue, TResult> callback);
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> CallAfter(GenericInnerParamsParamCallbackDelegate<TValue, TResult> callback);
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> Returns(GenericInnerParamsParamDelegate<TValue, TResult> resultGenerator);
            IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> Returns(global::System.Collections.Generic.Stack<TResult> value_1);
        }

        internal interface IGenericInnerParamsParamMethodImposter
        {
            IGenericInnerParamsParamMethodImposter<TValueTarget, TResultTarget>? As<TValueTarget, TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericInnerParamsParamMethodImposter<TValue, TResult> : IGenericInnerParamsParamMethodImposter
        {
            global::System.Collections.Generic.Stack<TResult> Invoke(global::System.Collections.Generic.List<TValue>[] value);
            bool HasMatchingSetup(GenericInnerParamsParamArguments<TValue, TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericInnerParamsParamMethodInvocationVerifier<TValue, TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // Stack<TResult> IMethodSetupFeatureSut.GenericInnerParamsParam<TValue, TResult>(params List<TValue>[] value)
        public interface IGenericInnerParamsParamMethodImposterBuilder<TValue, TResult> : IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>, GenericInnerParamsParamMethodInvocationVerifier<TValue, TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericInnerParamsParamMethodImposter<TValue, TResult> : IGenericInnerParamsParamMethodImposter<TValue, TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>>();
            private readonly GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection;
            public GenericInnerParamsParamMethodImposter(GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection)
            {
                this._genericInnerParamsParamMethodInvocationHistoryCollection = _genericInnerParamsParamMethodInvocationHistoryCollection;
            }

            IGenericInnerParamsParamMethodImposter<TValueTarget, TResultTarget>? IGenericInnerParamsParamMethodImposter.As<TValueTarget, TResultTarget>()
            {
                if (typeof(global::System.Collections.Generic.List<TValueTarget>[]).IsAssignableTo(typeof(global::System.Collections.Generic.List<TValue>[])) && typeof(global::System.Collections.Generic.Stack<TResult>).IsAssignableTo(typeof(global::System.Collections.Generic.Stack<TResultTarget>)))
                {
                    return new Adapter<TValueTarget, TResultTarget>(this);
                }

                return null;
            }

            private class Adapter<TValueTarget, TResultTarget> : IGenericInnerParamsParamMethodImposter<TValueTarget, TResultTarget>
            {
                private readonly GenericInnerParamsParamMethodImposter<TValue, TResult> _target;
                public Adapter(GenericInnerParamsParamMethodImposter<TValue, TResult> target)
                {
                    _target = target;
                }

                public global::System.Collections.Generic.Stack<TResultTarget> Invoke(List<TValueTarget>[] value)
                {
                    var result = _target.Invoke(Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.List<TValueTarget>[], global::System.Collections.Generic.List<TValue>[]>(value));
                    return Imposter.Abstractions.TypeCaster.Cast<global::System.Collections.Generic.Stack<TResult>, global::System.Collections.Generic.Stack<TResultTarget>>(result);
                }

                public bool HasMatchingSetup(GenericInnerParamsParamArguments<TValueTarget, TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TValue, TResult>());
                }

                IGenericInnerParamsParamMethodImposter<TValueTarget1, TResultTarget1>? IGenericInnerParamsParamMethodImposter.As<TValueTarget1, TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericInnerParamsParamArguments<TValue, TResult> arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>? FindMatchingSetup(GenericInnerParamsParamArguments<TValue, TResult> arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public global::System.Collections.Generic.Stack<TResult> Invoke(global::System.Collections.Generic.List<TValue>[] value)
            {
                var arguments = new GenericInnerParamsParamArguments<TValue, TResult>(value);
                var matchingSetup = FindMatchingSetup(arguments) ?? GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(value);
                    _genericInnerParamsParamMethodInvocationHistoryCollection.Add(new GenericInnerParamsParamMethodInvocationHistory<TValue, TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _genericInnerParamsParamMethodInvocationHistoryCollection.Add(new GenericInnerParamsParamMethodInvocationHistory<TValue, TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericInnerParamsParamMethodImposterBuilder<TValue, TResult>
            {
                private readonly GenericInnerParamsParamMethodImposterCollection _imposterCollection;
                private readonly GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection;
                private readonly GenericInnerParamsParamArgumentsCriteria<TValue, TResult> _argumentsCriteria;
                private GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>? _existingInvocationSetup;
                public Builder(GenericInnerParamsParamMethodImposterCollection _imposterCollection, GenericInnerParamsParamMethodInvocationHistoryCollection _genericInnerParamsParamMethodInvocationHistoryCollection, GenericInnerParamsParamArgumentsCriteria<TValue, TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericInnerParamsParamMethodInvocationHistoryCollection = _genericInnerParamsParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new GenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>(_argumentsCriteria);
                        _imposterCollection.AddNew<TValue, TResult>()._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Throws(GenericInnerParamsParamExceptionGeneratorDelegate<TValue, TResult> exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.CallBefore(GenericInnerParamsParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.CallAfter(GenericInnerParamsParamCallbackDelegate<TValue, TResult> callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(GenericInnerParamsParamDelegate<TValue, TResult> resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult> IGenericInnerParamsParamMethodInvocationsSetup<TValue, TResult>.Returns(global::System.Collections.Generic.Stack<TResult> value_1)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value_1);
                    return invocationSetup;
                }

                void GenericInnerParamsParamMethodInvocationVerifier<TValue, TResult>.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericInnerParamsParamMethodInvocationHistoryCollection.Count<TValue, TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // TResult IMethodSetupFeatureSut.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate TResult GenericAllRefKindDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        // TResult IMethodSetupFeatureSut.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
        public delegate void GenericAllRefKindCallbackDelegate<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues);
        // TResult IMethodSetupFeatureSut.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
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

        // TResult IMethodSetupFeatureSut.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
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

        // TResult IMethodSetupFeatureSut.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
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
        // TResult IMethodSetupFeatureSut.GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, params TParams[] paramsValues)
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

        // Task<int> IMethodSetupFeatureSut.AsyncTaskIntNoParams()
        public delegate global::System.Threading.Tasks.Task<int> AsyncTaskIntNoParamsDelegate();
        // Task<int> IMethodSetupFeatureSut.AsyncTaskIntNoParams()
        public delegate System.Threading.Tasks.Task AsyncTaskIntNoParamsCallbackDelegate();
        // Task<int> IMethodSetupFeatureSut.AsyncTaskIntNoParams()
        public delegate System.Exception AsyncTaskIntNoParamsExceptionGeneratorDelegate();
        public interface IAsyncTaskIntNoParamsMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AsyncTaskIntNoParamsMethodInvocationHistory : IAsyncTaskIntNoParamsMethodInvocationHistory
        {
            internal global::System.Threading.Tasks.Task<int> Result;
            internal System.Exception Exception;
            public AsyncTaskIntNoParamsMethodInvocationHistory(global::System.Threading.Tasks.Task<int> Result, System.Exception Exception)
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
        internal class AsyncTaskIntNoParamsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IAsyncTaskIntNoParamsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IAsyncTaskIntNoParamsMethodInvocationHistory>();
            internal void Add(IAsyncTaskIntNoParamsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // Task<int> IMethodSetupFeatureSut.AsyncTaskIntNoParams()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AsyncTaskIntNoParamsMethodInvocationsSetup : IAsyncTaskIntNoParamsMethodInvocationsSetup
        {
            internal static AsyncTaskIntNoParamsMethodInvocationsSetup DefaultInvocationSetup = new AsyncTaskIntNoParamsMethodInvocationsSetup();
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

            internal static async global::System.Threading.Tasks.Task<int> DefaultResultGenerator()
            {
                return default;
            }

            public AsyncTaskIntNoParamsMethodInvocationsSetup()
            {
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Returns(AsyncTaskIntNoParamsDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Returns(global::System.Threading.Tasks.Task<int> value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    return value;
                };
                return this;
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw new TException();
                };
                return this;
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exception;
                };
                return this;
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Throws(AsyncTaskIntNoParamsExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = () =>
                {
                    throw exceptionGenerator();
                };
                return this;
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.CallBefore(AsyncTaskIntNoParamsCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.CallAfter(AsyncTaskIntNoParamsCallbackDelegate callback)
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

            public async global::System.Threading.Tasks.Task<int> Invoke()
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    await nextSetup.CallBefore();
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = await nextSetup.ResultGenerator.Invoke();
                if (nextSetup.CallAfter != null)
                {
                    await nextSetup.CallAfter();
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal AsyncTaskIntNoParamsDelegate? ResultGenerator { get; set; }
                internal AsyncTaskIntNoParamsCallbackDelegate? CallBefore { get; set; }
                internal AsyncTaskIntNoParamsCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAsyncTaskIntNoParamsMethodInvocationsSetup
        {
            IAsyncTaskIntNoParamsMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IAsyncTaskIntNoParamsMethodInvocationsSetup Throws(System.Exception exception);
            IAsyncTaskIntNoParamsMethodInvocationsSetup Throws(AsyncTaskIntNoParamsExceptionGeneratorDelegate exceptionGenerator);
            IAsyncTaskIntNoParamsMethodInvocationsSetup CallBefore(AsyncTaskIntNoParamsCallbackDelegate callback);
            IAsyncTaskIntNoParamsMethodInvocationsSetup CallAfter(AsyncTaskIntNoParamsCallbackDelegate callback);
            IAsyncTaskIntNoParamsMethodInvocationsSetup Returns(AsyncTaskIntNoParamsDelegate resultGenerator);
            IAsyncTaskIntNoParamsMethodInvocationsSetup Returns(global::System.Threading.Tasks.Task<int> value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AsyncTaskIntNoParamsMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // Task<int> IMethodSetupFeatureSut.AsyncTaskIntNoParams()
        public interface IAsyncTaskIntNoParamsMethodImposterBuilder : IAsyncTaskIntNoParamsMethodInvocationsSetup, AsyncTaskIntNoParamsMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AsyncTaskIntNoParamsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AsyncTaskIntNoParamsMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<AsyncTaskIntNoParamsMethodInvocationsSetup>();
            private readonly AsyncTaskIntNoParamsMethodInvocationHistoryCollection _asyncTaskIntNoParamsMethodInvocationHistoryCollection;
            public AsyncTaskIntNoParamsMethodImposter(AsyncTaskIntNoParamsMethodInvocationHistoryCollection _asyncTaskIntNoParamsMethodInvocationHistoryCollection)
            {
                this._asyncTaskIntNoParamsMethodInvocationHistoryCollection = _asyncTaskIntNoParamsMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingSetup() != null;
            }

            private AsyncTaskIntNoParamsMethodInvocationsSetup? FindMatchingSetup()
            {
                if (_invocationSetups.TryPeek(out var setup))
                    return setup;
                else
                    return null;
            }

            public global::System.Threading.Tasks.Task<int> Invoke()
            {
                var matchingSetup = FindMatchingSetup() ?? AsyncTaskIntNoParamsMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke();
                    _asyncTaskIntNoParamsMethodInvocationHistoryCollection.Add(new AsyncTaskIntNoParamsMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _asyncTaskIntNoParamsMethodInvocationHistoryCollection.Add(new AsyncTaskIntNoParamsMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IAsyncTaskIntNoParamsMethodImposterBuilder
            {
                private readonly AsyncTaskIntNoParamsMethodImposter _imposter;
                private readonly AsyncTaskIntNoParamsMethodInvocationHistoryCollection _asyncTaskIntNoParamsMethodInvocationHistoryCollection;
                private AsyncTaskIntNoParamsMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(AsyncTaskIntNoParamsMethodImposter _imposter, AsyncTaskIntNoParamsMethodInvocationHistoryCollection _asyncTaskIntNoParamsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._asyncTaskIntNoParamsMethodInvocationHistoryCollection = _asyncTaskIntNoParamsMethodInvocationHistoryCollection;
                }

                private IAsyncTaskIntNoParamsMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new AsyncTaskIntNoParamsMethodInvocationsSetup();
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Throws(AsyncTaskIntNoParamsExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.CallBefore(AsyncTaskIntNoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.CallAfter(AsyncTaskIntNoParamsCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Returns(AsyncTaskIntNoParamsDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IAsyncTaskIntNoParamsMethodInvocationsSetup IAsyncTaskIntNoParamsMethodInvocationsSetup.Returns(global::System.Threading.Tasks.Task<int> value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void AsyncTaskIntNoParamsMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _asyncTaskIntNoParamsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.MethodSetup.IMethodSetupFeatureSut
        {
            IMethodSetupFeatureSutImposter _imposter;
            public ImposterTargetInstance(IMethodSetupFeatureSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void VoidNoParams()
            {
                _imposter._voidNoParamsMethodImposter.Invoke();
            }

            public int IntNoParams()
            {
                return _imposter._intNoParamsMethodImposter.Invoke();
            }

            public int IntSingleParam(int age)
            {
                return _imposter._intSingleParamMethodImposter.Invoke(age);
            }

            public int IntParams(int age, string name, global::System.Text.RegularExpressions.Regex regex)
            {
                return _imposter._intParamsMethodImposter.Invoke(age, name, regex);
            }

            public int IntOutParam(out int outValue)
            {
                return _imposter._intOutParamMethodImposter.Invoke(out outValue);
            }

            public int IntRefParam(ref int refValue)
            {
                return _imposter._intRefParamMethodImposter.Invoke(ref refValue);
            }

            public int IntParamsParam(string[] paramsStrings)
            {
                return _imposter._intParamsParamMethodImposter.Invoke(paramsStrings);
            }

            public int IntInParam(in string inStringValue)
            {
                return _imposter._intInParamMethodImposter.Invoke(in inStringValue);
            }

            public int IntAllRefKinds(out int value, ref int refValue, in int inValue, string valueAsString, string[] paramsStrings)
            {
                return _imposter._intAllRefKindsMethodImposter.Invoke(out value, ref refValue, in inValue, valueAsString, paramsStrings);
            }

            public void GenericSingleParam<TValue>(TValue value)
            {
                _imposter._genericSingleParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue>(new GenericSingleParamArguments<TValue>(value)).Invoke(value);
            }

            public void GenericInnerSingleParam<TValue>(global::System.Collections.Generic.List<TValue> value)
            {
                _imposter._genericInnerSingleParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue>(new GenericInnerSingleParamArguments<TValue>(value)).Invoke(value);
            }

            public TResult GenericOutParam<TValue, TResult>(out TValue value)
            {
                return _imposter._genericOutParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>().Invoke(out value);
            }

            public global::System.Collections.Generic.Stack<TResult> GenericInnerOutParam<TValue, TResult>(out global::System.Collections.Generic.List<TValue> value)
            {
                return _imposter._genericInnerOutParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>().Invoke(out value);
            }

            public TResult GenericRefParam<TValue, TResult>(ref TValue value)
            {
                return _imposter._genericRefParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>(new GenericRefParamArguments<TValue, TResult>(value)).Invoke(ref value);
            }

            public global::System.Collections.Generic.Stack<TResult> GenericInnerRefParam<TValue, TResult>(ref global::System.Collections.Generic.List<TValue> value)
            {
                return _imposter._genericInnerRefParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>(new GenericInnerRefParamArguments<TValue, TResult>(value)).Invoke(ref value);
            }

            public TResult GenericParamsParam<TValue, TResult>(TValue[] value)
            {
                return _imposter._genericParamsParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>(new GenericParamsParamArguments<TValue, TResult>(value)).Invoke(value);
            }

            public global::System.Collections.Generic.Stack<TResult> GenericInnerParamsParam<TValue, TResult>(global::System.Collections.Generic.List<TValue>[] value)
            {
                return _imposter._genericInnerParamsParamMethodImposterCollection.GetImposterWithMatchingSetup<TValue, TResult>(new GenericInnerParamsParamArguments<TValue, TResult>(value)).Invoke(value);
            }

            public TResult GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>(out TOut outValue, ref TRef refValue, in TIn inValue, TParams[] paramsValues)
            {
                return _imposter._genericAllRefKindMethodImposterCollection.GetImposterWithMatchingSetup<TOut, TRef, TIn, TParams, TResult>(new GenericAllRefKindArguments<TOut, TRef, TIn, TParams, TResult>(refValue, inValue, paramsValues)).Invoke(out outValue, ref refValue, in inValue, paramsValues);
            }

            public global::System.Threading.Tasks.Task<int> AsyncTaskIntNoParams()
            {
                return _imposter._asyncTaskIntNoParamsMethodImposter.Invoke();
            }
        }
    }
}