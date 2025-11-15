using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.Ideation.PropertySetupPoc;

#pragma warning disable nullable
namespace Imposter.Ideation.PropertySetupPoc
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IndexerSetupPoc : Imposter.Abstractions.IHaveImposterInstance<IPropertySetupSutPoc>
    {
        private ImposterTargetInstance _imposterInstance;
        private readonly AgePropertyImposter _agePropertyImposter;

        public IndexerSetupPoc()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
            this._agePropertyImposter = new AgePropertyImposter();
        }

        public IAgePropertyImposter Age => _agePropertyImposter;

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : IPropertySetupSutPoc
        {
            IndexerSetupPoc _imposter;

            public ImposterTargetInstance(IndexerSetupPoc _imposter)
            {
                this._imposter = _imposter;
            }

            public int Age
            {
                get => _imposter._agePropertyImposter.Get();
                set => _imposter._agePropertyImposter.Set(value);
            }
        }

        IPropertySetupSutPoc IHaveImposterInstance<IPropertySetupSutPoc>.Instance()
        {
            return _imposterInstance;
        }

        public interface IAgePropertyImposter
        {
            IAgePropertyImposter Returns(int value);

            IAgePropertyImposter Returns(Func<int> resultGenerator);

            IAgePropertyImposter Throws(Exception exception);

            IAgePropertyImposter Throws<TException>()
                where TException : Exception, new();

            IAgePropertyImposter SetterCallback(Arg<int> criteria, Action<int> callback);

            IAgePropertyImposter GetterCallback(Action callback);

            void GetterCalled(Count count);

            void SetterCalled(Arg<int> criteria, Count count);
        }

        class AgePropertyImposter : IAgePropertyImposter
        {
            private readonly ConcurrentQueue<Func<int>> _getterReturnValues =
                new ConcurrentQueue<Func<int>>();
            private readonly ConcurrentQueue<Action> _getterCallbacks =
                new ConcurrentQueue<Action>();
            private Func<int> _lastGetterReturnValue = () => default(int);
            private int _getterInvocationCount;

            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<
                Imposter.Abstractions.Arg<int>,
                System.Action<int>
            >> _setterCallbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<
                Imposter.Abstractions.Arg<int>,
                System.Action<int>
            >>();
            private readonly ConcurrentBag<int> _setterInvocationHistory = new ConcurrentBag<int>();

            private bool _useAutoPropertyBehaviour = true;
            private int _backingField;

            private void AddGetterReturnValue(Func<int> valueGenerator)
            {
                _useAutoPropertyBehaviour = false;
                _getterReturnValues.Enqueue(valueGenerator);
            }

            IAgePropertyImposter IAgePropertyImposter.Returns(int value)
            {
                AddGetterReturnValue(() => value);
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.Returns(Func<int> valueGenerator)
            {
                AddGetterReturnValue(valueGenerator);
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.Throws(Exception exception)
            {
                AddGetterReturnValue(() => throw exception);
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.Throws<TException>()
            {
                AddGetterReturnValue(() => throw new TException());
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.SetterCallback(
                Arg<int> criteria,
                Action<int> callback
            )
            {
                _setterCallbacks.Enqueue(new Tuple<Arg<int>, Action<int>>(criteria, callback));
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.GetterCallback(Action callback)
            {
                _getterCallbacks.Enqueue(callback);
                return this;
            }

            void IAgePropertyImposter.GetterCalled(Count count)
            {
                if (!count.Matches(_getterInvocationCount))
                {
                    throw new VerificationFailedException(count, _getterInvocationCount);
                }
            }

            void IAgePropertyImposter.SetterCalled(Arg<int> criteria, Count count)
            {
                var setterInvocationCount = _setterInvocationHistory.Count(criteria.Matches);

                if (!count.Matches(setterInvocationCount))
                {
                    throw new VerificationFailedException(count, setterInvocationCount);
                }
            }

            internal int Get()
            {
                Interlocked.Increment(ref _getterInvocationCount);

                foreach (var getterCallback in _getterCallbacks)
                {
                    getterCallback();
                }

                if (_useAutoPropertyBehaviour)
                {
                    return _backingField;
                }

                if (_getterReturnValues.TryDequeue(out var returnValue))
                {
                    _lastGetterReturnValue = returnValue;
                }

                return _lastGetterReturnValue();
            }

            internal void Set(int value)
            {
                _setterInvocationHistory.Add(value);

                foreach (var (criteria, setterCallback) in _setterCallbacks)
                {
                    if (criteria.Matches(value))
                    {
                        setterCallback(value);
                    }
                }

                if (_useAutoPropertyBehaviour)
                {
                    _backingField = value;
                }
            }
        }
    }
}

public interface IPropertySetupSutPoc
{
    int Age { get; set; }
}

public static class Usage
{
    static void SutUsage()
    {
        var imposter = new IndexerSetupPoc();
        imposter.Age.Returns(11).Returns(22).Returns(44);

        imposter.Age.Returns(() => imposter.Instance().Age == 22 ? 11 : 44);

        int captured;
        imposter.Age.SetterCallback(Arg<int>.Is(it => it == 10), value => captured = value);

        imposter.Age.GetterCalled(Count.Any);

        imposter.Age.SetterCalled(Arg<int>.Any(), Count.Any);
    }
}
