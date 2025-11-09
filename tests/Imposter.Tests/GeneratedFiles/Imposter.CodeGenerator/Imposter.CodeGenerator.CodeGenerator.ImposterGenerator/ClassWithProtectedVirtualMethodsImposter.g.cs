#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.ClassImposter;

namespace Imposter.Tests.Features.ClassImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ClassWithProtectedVirtualMethodsImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.ClassWithProtectedVirtualMethods>
    {
        private readonly ProtectedVirtualMethodMethodImposter _protectedVirtualMethodMethodImposter;
        private readonly ProtectedVirtualMethodMethodInvocationHistoryCollection _protectedVirtualMethodMethodInvocationHistoryCollection = new ProtectedVirtualMethodMethodInvocationHistoryCollection();
        public IProtectedVirtualMethodMethodImposterBuilder ProtectedVirtualMethod()
        {
            return new ProtectedVirtualMethodMethodImposter.Builder(_protectedVirtualMethodMethodImposter, _protectedVirtualMethodMethodInvocationHistoryCollection);
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.ClassImposter.ClassWithProtectedVirtualMethods Instance() => _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.ClassWithProtectedVirtualMethods global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.ClassWithProtectedVirtualMethods>.Instance()
        {
            return _imposterInstance;
        }

        // virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()
        public delegate void ProtectedVirtualMethodDelegate();
        // virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()
        public delegate void ProtectedVirtualMethodCallbackDelegate();
        // virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()
        public delegate System.Exception ProtectedVirtualMethodExceptionGeneratorDelegate();
        public interface IProtectedVirtualMethodMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ProtectedVirtualMethodMethodInvocationHistory : IProtectedVirtualMethodMethodInvocationHistory
        {
            internal System.Exception Exception;
            public ProtectedVirtualMethodMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ProtectedVirtualMethodMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IProtectedVirtualMethodMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IProtectedVirtualMethodMethodInvocationHistory>();
            internal void Add(IProtectedVirtualMethodMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ProtectedVirtualMethodMethodInvocationImposterGroup
        {
            internal static ProtectedVirtualMethodMethodInvocationImposterGroup Default = new ProtectedVirtualMethodMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ProtectedVirtualMethodMethodInvocationImposterGroup()
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ProtectedVirtualMethodDelegate baseImplementation = null)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, baseImplementation);
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

                private ProtectedVirtualMethodDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ProtectedVirtualMethodCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ProtectedVirtualMethodCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && _resultGenerator == null && _callbacks.Count == 0;

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ProtectedVirtualMethodDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }
                }

                internal void Callback(ProtectedVirtualMethodCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ProtectedVirtualMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static void DefaultResultGenerator()
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IProtectedVirtualMethodMethodInvocationImposterGroupCallback
        {
            IProtectedVirtualMethodMethodInvocationImposterGroupContinuation Callback(ProtectedVirtualMethodCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IProtectedVirtualMethodMethodInvocationImposterGroupContinuation : IProtectedVirtualMethodMethodInvocationImposterGroupCallback
        {
            IProtectedVirtualMethodMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IProtectedVirtualMethodMethodInvocationImposterGroup : IProtectedVirtualMethodMethodInvocationImposterGroupCallback
        {
            IProtectedVirtualMethodMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IProtectedVirtualMethodMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IProtectedVirtualMethodMethodInvocationImposterGroupContinuation Throws(ProtectedVirtualMethodExceptionGeneratorDelegate exceptionGenerator);
            IProtectedVirtualMethodMethodInvocationImposterGroupContinuation UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ProtectedVirtualMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()
        public interface IProtectedVirtualMethodMethodImposterBuilder : IProtectedVirtualMethodMethodInvocationImposterGroup, IProtectedVirtualMethodMethodInvocationImposterGroupCallback, ProtectedVirtualMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ProtectedVirtualMethodMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ProtectedVirtualMethodMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ProtectedVirtualMethodMethodInvocationImposterGroup>();
            private readonly ProtectedVirtualMethodMethodInvocationHistoryCollection _protectedVirtualMethodMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ProtectedVirtualMethodMethodImposter(ProtectedVirtualMethodMethodInvocationHistoryCollection _protectedVirtualMethodMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._protectedVirtualMethodMethodInvocationHistoryCollection = _protectedVirtualMethodMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ProtectedVirtualMethodMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public void Invoke(ProtectedVirtualMethodDelegate baseImplementation = null)
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()");
                    }

                    matchingInvocationImposterGroup = ProtectedVirtualMethodMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual void ClassWithProtectedVirtualMethods.ProtectedVirtualMethod()", baseImplementation);
                    _protectedVirtualMethodMethodInvocationHistoryCollection.Add(new ProtectedVirtualMethodMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _protectedVirtualMethodMethodInvocationHistoryCollection.Add(new ProtectedVirtualMethodMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IProtectedVirtualMethodMethodImposterBuilder, IProtectedVirtualMethodMethodInvocationImposterGroupContinuation
            {
                private readonly ProtectedVirtualMethodMethodImposter _imposter;
                private readonly ProtectedVirtualMethodMethodInvocationHistoryCollection _protectedVirtualMethodMethodInvocationHistoryCollection;
                private readonly ProtectedVirtualMethodMethodInvocationImposterGroup _invocationImposterGroup;
                private ProtectedVirtualMethodMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ProtectedVirtualMethodMethodImposter _imposter, ProtectedVirtualMethodMethodInvocationHistoryCollection _protectedVirtualMethodMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._protectedVirtualMethodMethodInvocationHistoryCollection = _protectedVirtualMethodMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ProtectedVirtualMethodMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IProtectedVirtualMethodMethodInvocationImposterGroupContinuation IProtectedVirtualMethodMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IProtectedVirtualMethodMethodInvocationImposterGroupContinuation IProtectedVirtualMethodMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IProtectedVirtualMethodMethodInvocationImposterGroupContinuation IProtectedVirtualMethodMethodInvocationImposterGroup.Throws(ProtectedVirtualMethodExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IProtectedVirtualMethodMethodInvocationImposterGroupContinuation IProtectedVirtualMethodMethodInvocationImposterGroupCallback.Callback(ProtectedVirtualMethodCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IProtectedVirtualMethodMethodInvocationImposterGroupContinuation IProtectedVirtualMethodMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IProtectedVirtualMethodMethodInvocationImposterGroup IProtectedVirtualMethodMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ProtectedVirtualMethodInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _protectedVirtualMethodMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public ClassWithProtectedVirtualMethodsImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._protectedVirtualMethodMethodImposter = new ProtectedVirtualMethodMethodImposter(_protectedVirtualMethodMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.ClassWithProtectedVirtualMethods
        {
            ClassWithProtectedVirtualMethodsImposter _imposter;
            internal void InitializeImposter(ClassWithProtectedVirtualMethodsImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            protected override void ProtectedVirtualMethod()
            {
                _imposter._protectedVirtualMethodMethodImposter.Invoke(base.ProtectedVirtualMethod);
            }
        }
    }
}