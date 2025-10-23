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
    public class IPropertySetupSutPocImposter : Imposter.Abstractions.IHaveImposterInstance<IPropertySetupSutPoc>
    {
        private ImposterTargetInstance _imposterInstance;
        private readonly AgePropertyImposter _agePropertyImposter;

        public IPropertySetupSutPocImposter()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
            this._agePropertyImposter = new AgePropertyImposter();
        }

        public IAgePropertyImposter Age => _agePropertyImposter;

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : IPropertySetupSutPoc
        {
            IPropertySetupSutPocImposter _imposter;

            public ImposterTargetInstance(IPropertySetupSutPocImposter _imposter)
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

            void GetterCalled(Count times);

            void SetterCalled(Arg<int> criteria, Count times);
        }

        class AgePropertyImposter : IAgePropertyImposter
        {
            private readonly ConcurrentQueue<Func<int>> _setupValue = new ConcurrentQueue<Func<int>>();
            private readonly ConcurrentStack<(Arg<int> criteria, Action<int> callback)> _setterCallbacks = new ConcurrentStack<(Arg<int> criteria, Action<int> callback)>();
            private Action? _getterCallback;
            private readonly ConcurrentBag<int> _setHistory = new ConcurrentBag<int>();
            private bool _isAutoProperty = true;
            private int _backingField;
            private int _lastSetupValue;
            private int _invocationCount;

            private void AddSetupValue(Func<int> setupValue)
            {
                _isAutoProperty = false;
                _setupValue.Enqueue(setupValue);
            }

            IAgePropertyImposter IAgePropertyImposter.Returns(int value)
            {
                AddSetupValue(() => value);
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.Returns(Func<int> resultGenerator)
            {
                AddSetupValue(resultGenerator);
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.Throws(Exception exception)
            {
                AddSetupValue(() => throw exception);
                return this;
            }

            public IAgePropertyImposter Throws<TException>() where TException : Exception, new()
            {
                AddSetupValue(() => throw new TException());
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.SetterCallback(Arg<int> criteria, Action<int> callback)
            {
                _setterCallbacks.Push((criteria, callback));
                return this;
            }

            IAgePropertyImposter IAgePropertyImposter.GetterCallback(Action callback)
            {
                _getterCallback = callback;
                return this;
            }

            void IAgePropertyImposter.GetterCalled(Count times)
            {
                if (!times.Matches(_invocationCount))
                {
                    throw new System.Exception($"Expected {times} times, but was {_invocationCount}");
                }
            }

            void IAgePropertyImposter.SetterCalled(Arg<int> criteria, Count times)
            {
                var setCount = _setHistory.Count(criteria.Matches);

                if (!times.Matches(setCount))
                {
                    throw new System.Exception($"Expected {times} times, but was {setCount}");
                }
            }

            internal int Get()
            {
                Interlocked.Increment(ref _invocationCount);

                if (_getterCallback != null)
                {
                    _getterCallback();
                }

                if (_isAutoProperty)
                {
                    return _backingField;
                }

                if (_setupValue.TryDequeue(out var setupValue))
                {
                    _lastSetupValue = setupValue();
                }

                return _lastSetupValue;
            }

            internal void Set(int value)
            {
                _setHistory.Add(value);

                if (_isAutoProperty)
                {
                    _backingField = value;
                }

                foreach (var (criteria, setterCallback) in _setterCallbacks)
                {
                    if (criteria.Matches(value))
                    {
                        setterCallback(value);
                    }
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
        var imposter = new IPropertySetupSutPocImposter();
        imposter.Age.Returns(11).Returns(22).Returns(44);
        
        imposter.Age.Returns(() => imposter.Instance().Age == 22 ? 11 : 44);

        int captured;
        imposter.Age.SetterCallback(Arg<int>.Is(it => it == 10), value => captured = value);

        imposter.Age.GetterCalled(Count.Any);

        imposter.Age.SetterCalled(Arg<int>.Any(), Count.Any);
    }
}