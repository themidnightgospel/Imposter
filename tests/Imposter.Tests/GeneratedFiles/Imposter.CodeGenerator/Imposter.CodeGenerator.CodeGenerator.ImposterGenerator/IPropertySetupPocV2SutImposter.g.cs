using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.CodeGenerator.Tests.Features.PropertySetup;

#pragma warning disable nullable
namespace Imposter.CodeGenerator.Tests.Features.PropertySetup
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IPropertySetupPocV2SutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut>
    {
        private readonly AgeProperty _Age;
        public IAgeProperty Age => _Age;

        private ImposterTargetInstance _imposterInstance;
        public IPropertySetupPocV2SutImposter()
        {
            this._Age = new AgeProperty();
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut
        {
            IPropertySetupPocV2SutImposter _imposter;
            public ImposterTargetInstance(IPropertySetupPocV2SutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Age
            {
                get
                {
                    return _imposter._Age.Get();
                }

                set
                {
                    _imposter._Age.Set(value);
                }
            }
        }

        global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.CodeGenerator.Tests.Features.PropertySetup.IPropertySetupPocV2Sut>.Instance()
        {
            return _imposterInstance;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAgeProperty
        {
            IAgeProperty Returns(int value);
            IAgeProperty Returns(System.Func<int> valueGenerator);
            IAgeProperty Throws(System.Exception exception);
            IAgeProperty Throws<TException>()
                where TException : Exception, new();
            IAgeProperty SetterCallback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback);
            IAgeProperty GetterCallback(System.Action callback);
            void GetterCalled(Imposter.Abstractions.Count count);
            void SetterCalled(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AgeProperty : IAgeProperty
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<int>> _getterReturnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<int>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _getterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
            private System.Func<int> _lastGetterReturnValue = () => default;
            private int _getterInvocationCount;
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>> _setterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>>();
            private readonly System.Collections.Concurrent.ConcurrentBag<int> _setterInvocationHistory = new System.Collections.Concurrent.ConcurrentBag<int>();
            private bool _useAutoPropertyBehaviour = true;
            private int _backingField = default;
            private void AddGetterReturnValue(System.Func<int> valueGenerator)
            {
                _useAutoPropertyBehaviour = false;
                _getterReturnValues.Enqueue(valueGenerator);
            }

            IAgeProperty IAgeProperty.Returns(int value)
            {
                AddGetterReturnValue(() => value);
                return this;
            }

            IAgeProperty IAgeProperty.Returns(System.Func<int> valueGenerator)
            {
                AddGetterReturnValue(valueGenerator);
                return this;
            }

            IAgeProperty IAgeProperty.Throws(System.Exception exception)
            {
                AddGetterReturnValue(() => throw exception);
                return this;
            }

            IAgeProperty IAgeProperty.Throws<TException>()
            {
                AddGetterReturnValue(() => throw new TException());
                return this;
            }

            IAgeProperty IAgeProperty.SetterCallback(Imposter.Abstractions.Arg<int> criteria, System.Action<int> callback)
            {
                _setterCallbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<int>, System.Action<int>>(criteria, callback));
                return this;
            }

            IAgeProperty IAgeProperty.GetterCallback(System.Action callback)
            {
                _getterCallbacks.Enqueue(callback);
                return this;
            }

            void IAgeProperty.GetterCalled(Imposter.Abstractions.Count count)
            {
                if (!count.Matches(_getterInvocationCount))
                    throw new Imposter.Abstractions.VerificationFailedException(count, _getterInvocationCount);
            }

            void IAgeProperty.SetterCalled(Imposter.Abstractions.Arg<int> criteria, Imposter.Abstractions.Count count)
            {
                var setterInvocationCount = _setterInvocationHistory.Count(criteria.Matches);
                if (!count.Matches(setterInvocationCount))
                    throw new Imposter.Abstractions.VerificationFailedException(count, setterInvocationCount);
            }

            internal int Get()
            {
                System.Threading.Interlocked.Increment(ref _getterInvocationCount);
                foreach (var getterCallback in _getterCallbacks)
                {
                    getterCallback();
                }

                if (_useAutoPropertyBehaviour)
                    return _backingField;
                if (_getterReturnValues.TryDequeue(out var returnValue))
                    _lastGetterReturnValue = returnValue;
                return _lastGetterReturnValue();
            }

            internal void Set(int value)
            {
                _setterInvocationHistory.Add(value);
                foreach (var(criteria, setterCallback)in _setterCallbacks)
                {
                    if (criteria.Matches(value))
                        setterCallback(value);
                }

                if (_useAutoPropertyBehaviour)
                    _backingField = value;
            }
        }
    }
}