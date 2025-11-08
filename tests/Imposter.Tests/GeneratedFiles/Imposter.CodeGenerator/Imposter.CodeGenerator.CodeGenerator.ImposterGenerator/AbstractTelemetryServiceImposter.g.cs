using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.ClassImposter;

#pragma warning disable nullable
namespace Imposter.Tests.Features.ClassImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class AbstractTelemetryServiceImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.AbstractTelemetryService>
    {
        private readonly ComputeMethodImposter _computeMethodImposter;
        private readonly ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection = new ComputeMethodInvocationHistoryCollection();
        public IComputeMethodImposterBuilder Compute(Imposter.Abstractions.Arg<int> value)
        {
            return new ComputeMethodImposter.Builder(_computeMethodImposter, _computeMethodInvocationHistoryCollection, new ComputeArgumentsCriteria(value));
        }

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.ClassImposter.AbstractTelemetryService Instance() => _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.AbstractTelemetryService Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.AbstractTelemetryService>.Instance()
        {
            return _imposterInstance;
        }

        // abstract int AbstractTelemetryService.Compute(int value)
        public delegate int ComputeDelegate(int value);
        // abstract int AbstractTelemetryService.Compute(int value)
        public delegate void ComputeCallbackDelegate(int value);
        // abstract int AbstractTelemetryService.Compute(int value)
        public delegate System.Exception ComputeExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ComputeArguments
        {
            public int value;
            internal ComputeArguments(int value)
            {
                this.value = value;
            }
        }

        // abstract int AbstractTelemetryService.Compute(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ComputeArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> value { get; }

            public ComputeArgumentsCriteria(Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(ComputeArguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        public interface IComputeMethodInvocationHistory
        {
            bool Matches(ComputeArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ComputeMethodInvocationHistory : IComputeMethodInvocationHistory
        {
            internal ComputeArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public ComputeMethodInvocationHistory(ComputeArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(ComputeArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ComputeMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IComputeMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IComputeMethodInvocationHistory>();
            internal void Add(IComputeMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ComputeArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // abstract int AbstractTelemetryService.Compute(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ComputeMethodInvocationImposterGroup
        {
            internal static ComputeMethodInvocationImposterGroup Default = new ComputeMethodInvocationImposterGroup(new ComputeArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal ComputeArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ComputeMethodInvocationImposterGroup(ComputeArgumentsCriteria argumentsCriteria)
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

            public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value);
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

                private ComputeDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ComputeCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ComputeCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(value);
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }

                    return result;
                }

                internal void Callback(ComputeCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ComputeDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value_1)
                {
                    _resultGenerator = (int value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(ComputeExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal static int DefaultResultGenerator(int value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IComputeMethodInvocationImposterGroup
        {
            IComputeMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IComputeMethodInvocationImposterGroup Throws(System.Exception exception);
            IComputeMethodInvocationImposterGroup Throws(ComputeExceptionGeneratorDelegate exceptionGenerator);
            IComputeMethodInvocationImposterGroup Callback(ComputeCallbackDelegate callback);
            IComputeMethodInvocationImposterGroup Returns(ComputeDelegate resultGenerator);
            IComputeMethodInvocationImposterGroup Returns(int value_1);
            IComputeMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ComputeInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // abstract int AbstractTelemetryService.Compute(int value)
        public interface IComputeMethodImposterBuilder : IComputeMethodInvocationImposterGroup, ComputeInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ComputeMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ComputeMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ComputeMethodInvocationImposterGroup>();
            private readonly ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ComputeMethodImposter(ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._computeMethodInvocationHistoryCollection = _computeMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(ComputeArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ComputeMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(ComputeArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int value)
            {
                var arguments = new ComputeArguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("abstract int AbstractTelemetryService.Compute(int value)");
                    }

                    matchingInvocationImposterGroup = ComputeMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "abstract int AbstractTelemetryService.Compute(int value)", value);
                    _computeMethodInvocationHistoryCollection.Add(new ComputeMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _computeMethodInvocationHistoryCollection.Add(new ComputeMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IComputeMethodImposterBuilder
            {
                private readonly ComputeMethodImposter _imposter;
                private readonly ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection;
                private readonly ComputeArgumentsCriteria _argumentsCriteria;
                private readonly ComputeMethodInvocationImposterGroup _invocationImposterGroup;
                private ComputeMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ComputeMethodImposter _imposter, ComputeMethodInvocationHistoryCollection _computeMethodInvocationHistoryCollection, ComputeArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._computeMethodInvocationHistoryCollection = _computeMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ComputeMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Throws(ComputeExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Callback(ComputeCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Returns(ComputeDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IComputeMethodInvocationImposterGroup IComputeMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ComputeInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _computeMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public INamePropertyBuilder Name => _Name;

        private readonly NamePropertyBuilder _Name;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyGetterBuilder
        {
            INamePropertyGetterBuilder Returns(string value);
            INamePropertyGetterBuilder Returns(System.Func<string> valueGenerator);
            INamePropertyGetterBuilder Throws(System.Exception exception);
            INamePropertyGetterBuilder Throws<TException>()
                where TException : Exception, new();
            INamePropertyGetterBuilder Callback(System.Action callback);
            void Called(Imposter.Abstractions.Count count);
            INamePropertyGetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertySetterBuilder
        {
            INamePropertySetterBuilder Callback(System.Action<string> callback);
            void Called(Imposter.Abstractions.Count count);
            INamePropertySetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface INamePropertyBuilder
        {
            INamePropertyGetterBuilder Getter();
            INamePropertySetterBuilder Setter(Imposter.Abstractions.Arg<string> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class NamePropertyBuilder : INamePropertyBuilder
        {
            private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            internal SetterImposter _setterImposter;
            internal GetterImposterBuilder _getterImposterBuilder;
            internal NamePropertyBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior)
            {
                _defaultPropertyBehaviour = new DefaultPropertyBehaviour();
                _invocationBehavior = invocationBehavior;
                _getterImposterBuilder = new GetterImposterBuilder(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.ClassImposter.AbstractTelemetryService.Name");
                _setterImposter = new SetterImposter(_defaultPropertyBehaviour, _invocationBehavior, "Imposter.Tests.Features.ClassImposter.AbstractTelemetryService.Name");
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultPropertyBehaviour
            {
                internal bool IsOn = true;
                internal string BackingField = default;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class GetterImposterBuilder : INamePropertyGetterBuilder
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<string>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<string>>();
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Action>();
                private System.Func<string> _lastReturnValue = () => default;
                private int _invocationCount;
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposterBuilder(DefaultPropertyBehaviour _defaultPropertyBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                private void AddReturnValue(System.Func<string> valueGenerator)
                {
                    _defaultPropertyBehaviour.IsOn = false;
                    _returnValues.Enqueue(valueGenerator);
                    _hasConfiguredReturn = true;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Returns(string value)
                {
                    AddReturnValue(() => value);
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Returns(System.Func<string> valueGenerator)
                {
                    AddReturnValue(valueGenerator);
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Throws(System.Exception exception)
                {
                    AddReturnValue(() => throw exception);
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Throws<TException>()
                {
                    AddReturnValue(() => throw new TException());
                    return this;
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Callback(System.Action callback)
                {
                    _callbacks.Enqueue(callback);
                    return this;
                }

                void INamePropertyGetterBuilder.Called(Imposter.Abstractions.Count count)
                {
                    if (!count.Matches(_invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, _invocationCount);
                }

                INamePropertyGetterBuilder INamePropertyGetterBuilder.Then()
                {
                    return this;
                }

                internal string Get()
                {
                    EnsureGetterConfigured();
                    System.Threading.Interlocked.Increment(ref _invocationCount);
                    foreach (var getterCallback in _callbacks)
                    {
                        getterCallback();
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        return _defaultPropertyBehaviour.BackingField;
                    if (_returnValues.TryDequeue(out var returnValue))
                        _lastReturnValue = returnValue;
                    return _lastReturnValue();
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !_hasConfiguredReturn)
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<string>, System.Action<string>>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<System.Tuple<Imposter.Abstractions.Arg<string>, System.Action<string>>>();
                private readonly System.Collections.Concurrent.ConcurrentBag<string> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<string>();
                private readonly DefaultPropertyBehaviour _defaultPropertyBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultPropertyBehaviour _defaultPropertyBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultPropertyBehaviour = _defaultPropertyBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal void Callback(Imposter.Abstractions.Arg<string> criteria, System.Action<string> callback)
                {
                    _callbacks.Enqueue(new System.Tuple<Imposter.Abstractions.Arg<string>, System.Action<string>>(criteria, callback));
                }

                void Called(Imposter.Abstractions.Arg<string> criteria, Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                }

                internal void Set(string value)
                {
                    EnsureSetterConfigured();
                    _invocationHistory.Add(value);
                    foreach (var(criteria, setterCallback)in _callbacks)
                    {
                        if (criteria.Matches(value))
                            setterCallback(value);
                    }

                    if (_defaultPropertyBehaviour.IsOn)
                        _defaultPropertyBehaviour.BackingField = value;
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !_hasConfiguredSetter)
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                }

                internal void MarkConfigured()
                {
                    _hasConfiguredSetter = true;
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : INamePropertySetterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly Imposter.Abstractions.Arg<string> _criteria;
                    internal Builder(SetterImposter _setterImposter, Imposter.Abstractions.Arg<string> _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    INamePropertySetterBuilder INamePropertySetterBuilder.Callback(System.Action<string> callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    void INamePropertySetterBuilder.Called(Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    INamePropertySetterBuilder INamePropertySetterBuilder.Then()
                    {
                        return this;
                    }
                }
            }

            INamePropertyGetterBuilder INamePropertyBuilder.Getter()
            {
                return _getterImposterBuilder;
            }

            INamePropertySetterBuilder INamePropertyBuilder.Setter(Imposter.Abstractions.Arg<string> criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }
        }

        public IStreamAdvancedEventImposterBuilder StreamAdvanced => _StreamAdvanced;

        private readonly StreamAdvancedEventImposterBuilder _StreamAdvanced;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IStreamAdvancedEventImposterBuilder
        {
            IStreamAdvancedEventImposterBuilder Callback(global::System.EventHandler callback);
            IStreamAdvancedEventImposterBuilder Raise(object sender, global::System.EventArgs e);
            IStreamAdvancedEventImposterBuilder Subscribed(Imposter.Abstractions.Arg<global::System.EventHandler> criteria, Imposter.Abstractions.Count count);
            IStreamAdvancedEventImposterBuilder Unsubscribed(Imposter.Abstractions.Arg<global::System.EventHandler> criteria, Imposter.Abstractions.Count count);
            IStreamAdvancedEventImposterBuilder OnSubscribe(System.Action<global::System.EventHandler> interceptor);
            IStreamAdvancedEventImposterBuilder OnUnsubscribe(System.Action<global::System.EventHandler> interceptor);
            IStreamAdvancedEventImposterBuilder Raised(Imposter.Abstractions.Arg<object> senderCriteria, Imposter.Abstractions.Arg<global::System.EventArgs> eCriteria, Imposter.Abstractions.Count count);
            IStreamAdvancedEventImposterBuilder HandlerInvoked(Imposter.Abstractions.Arg<global::System.EventHandler> handlerCriteria, Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class StreamAdvancedEventImposterBuilder : IStreamAdvancedEventImposterBuilder
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _handlerOrder = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentDictionary<global::System.EventHandler, int> _handlerCounts = new System.Collections.Concurrent.ConcurrentDictionary<global::System.EventHandler, int>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs e)> _history = new System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs e)>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _subscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _unsubscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>> _subscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>> _unsubscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(global::System.EventHandler Handler, object sender, global::System.EventArgs e)> _handlerInvocations = new System.Collections.Concurrent.ConcurrentQueue<(global::System.EventHandler Handler, object sender, global::System.EventArgs e)>();
            internal StreamAdvancedEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.EventHandler handler)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerOrder.Enqueue(handler);
                _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
                _subscribeHistory.Enqueue(handler);
                foreach (var interceptor in _subscribeInterceptors.ToArray())
                {
                    interceptor(handler);
                }
            }

            internal void Unsubscribe(global::System.EventHandler handler)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerCounts.AddOrUpdate(handler, 0, (_, count) =>
                {
                    if (count > 0)
                    {
                        return count - 1;
                    }

                    return 0;
                });
                _unsubscribeHistory.Enqueue(handler);
                foreach (var interceptor in _unsubscribeInterceptors.ToArray())
                {
                    interceptor(handler);
                }
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.Callback(global::System.EventHandler callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.Raise(object sender, global::System.EventArgs e)
            {
                RaiseInternal(sender, e);
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.Subscribed(Imposter.Abstractions.Arg<global::System.EventHandler> criteria, Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.Unsubscribed(Imposter.Abstractions.Arg<global::System.EventHandler> criteria, Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.OnSubscribe(System.Action<global::System.EventHandler> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.OnUnsubscribe(System.Action<global::System.EventHandler> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.Raised(Imposter.Abstractions.Arg<object> senderCriteria, Imposter.Abstractions.Arg<global::System.EventArgs> eCriteria, Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(senderCriteria);
                ArgumentNullException.ThrowIfNull(eCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => senderCriteria.Matches(entry.sender) && eCriteria.Matches(entry.e));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            IStreamAdvancedEventImposterBuilder IStreamAdvancedEventImposterBuilder.HandlerInvoked(Imposter.Abstractions.Arg<global::System.EventHandler> handlerCriteria, Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(handlerCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
                EnsureCountMatches(actual, count, "invoked");
                return this;
            }

            private void RaiseInternal(object sender, global::System.EventArgs e)
            {
                _history.Enqueue((sender, e));
                foreach (var callback in _callbacks.ToArray())
                {
                    callback(sender, e);
                }

                foreach (var handler in EnumerateActiveHandlers())
                {
                    _handlerInvocations.Enqueue((handler, sender, e));
                    handler(sender, e);
                }
            }

            private System.Collections.Generic.IEnumerable<global::System.EventHandler> EnumerateActiveHandlers()
            {
                System.Collections.Generic.Dictionary<global::System.EventHandler, int> budgets = new System.Collections.Generic.Dictionary<global::System.EventHandler, int>(_handlerCounts);
                foreach (var handler in _handlerOrder.ToArray())
                {
                    int remaining;
                    if (budgets.TryGetValue(handler, out remaining))
                    {
                        if (remaining > 0)
                        {
                            budgets[handler] = remaining - 1;
                            yield return handler;
                        }
                    }
                }
            }

            private static int CountMatches<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
            {
                int count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }

                return count;
            }

            private static void EnsureCountMatches(int actual, Imposter.Abstractions.Count expected, string action)
            {
                if (!expected.Matches(actual))
                {
                    throw new Imposter.Abstractions.VerificationFailedException(expected, actual);
                }
            }
        }

        private readonly IndexerIndexerBuilder _IndexerIndexer;
        public IIndexerIndexerBuilder this[Imposter.Abstractions.Arg<int> index] => new IndexerIndexerBuilder.InvocationBuilder(_IndexerIndexer, new IndexerIndexerArgumentsCriteria(index));
        public delegate int IndexerIndexerDelegate(int index);
        public delegate void IndexerIndexerGetterCallback(int index);
        public delegate void IndexerIndexerSetterCallback(int index, int value);
        public delegate System.Exception IndexerIndexerExceptionGenerator(int index);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArguments : global::System.IEquatable<IndexerIndexerArguments>
        {
            public int index;
            internal IndexerIndexerArguments(int index)
            {
                this.index = index;
            }

            public bool Equals(IndexerIndexerArguments other)
            {
                return true && index == other.index;
            }

            public override bool Equals(object obj)
            {
                return obj is IndexerIndexerArguments other && Equals(other);
            }

            public override int GetHashCode()
            {
                global::System.HashCode hash = new global::System.HashCode();
                hash.Add(index);
                return hash.ToHashCode();
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> index;
            internal IndexerIndexerArgumentsCriteria(Imposter.Abstractions.Arg<int> index)
            {
                this.index = index;
            }

            public bool Matches(IndexerIndexerArguments arguments)
            {
                return index.Matches(arguments.index);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IndexerIndexerBuilder
        {
            private readonly DefaultIndexerIndexerBehaviour _IndexerDefaultIndexerBehaviour = new DefaultIndexerIndexerBehaviour();
            private readonly GetterImposter _getterImposter;
            private readonly SetterImposter _setterImposter;
            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class DefaultIndexerIndexerBehaviour
            {
                private bool _isOn = true;
                internal bool IsOn
                {
                    get
                    {
                        return System.Threading.Volatile.Read(ref _isOn);
                    }

                    set
                    {
                        System.Threading.Volatile.Write(ref _isOn, value);
                    }
                }

                internal System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArguments, int> BackingField = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArguments, int>();
                internal int Get(IndexerIndexerArguments arguments)
                {
                    int value = default(int);
                    if (BackingField.TryGetValue(arguments, out value))
                        return value;
                    return default(int);
                }

                internal void Set(IndexerIndexerArguments arguments, int value)
                {
                    BackingField[arguments] = value;
                }
            }

            internal IndexerIndexerBuilder(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
            {
                this._getterImposter = new GetterImposter(_IndexerDefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
                this._setterImposter = new SetterImposter(_IndexerDefaultIndexerBehaviour, invocationBehavior, propertyDisplayName);
            }

            internal IIndexerIndexerGetterBuilder CreateGetter(IndexerIndexerArgumentsCriteria criteria)
            {
                return new GetterImposter.Builder(_getterImposter, criteria);
            }

            internal IIndexerIndexerSetterBuilder CreateSetter(IndexerIndexerArgumentsCriteria criteria)
            {
                _setterImposter.MarkConfigured();
                return new SetterImposter.Builder(_setterImposter, criteria);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class InvocationBuilder : IIndexerIndexerBuilder
            {
                private readonly IndexerIndexerBuilder _builder;
                private readonly IndexerIndexerArgumentsCriteria _criteria;
                internal InvocationBuilder(IndexerIndexerBuilder builder, IndexerIndexerArgumentsCriteria criteria)
                {
                    this._builder = builder;
                    this._criteria = criteria;
                }

                public IIndexerIndexerGetterBuilder Getter()
                {
                    return _builder.CreateGetter(_criteria);
                }

                public IIndexerIndexerSetterBuilder Setter()
                {
                    return _builder.CreateSetter(_criteria);
                }
            }

            internal int Get(int index)
            {
                return _getterImposter.Get(index);
            }

            internal void Set(int index, int value)
            {
                _setterImposter.Set(index, value);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class GetterImposter
            {
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter> _setups = new System.Collections.Concurrent.ConcurrentStack<GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter> _setupLookup = new System.Collections.Concurrent.ConcurrentDictionary<IndexerIndexerArgumentsCriteria, GetterInvocationImposter>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredReturn;
                internal GetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                internal int Get(int index)
                {
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(index);
                    try
                    {
                        var setup = FindMatchingSetup(arguments);
                        if (setup is null)
                        {
                            EnsureGetterConfigured();
                            if (_defaultBehaviour.IsOn)
                                return _defaultBehaviour.Get(arguments);
                            throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return setup.Invoke(arguments);
                    }
                    finally
                    {
                        _invocationHistory.Add(arguments);
                    }
                }

                private GetterInvocationImposter FindMatchingSetup(IndexerIndexerArguments arguments)
                {
                    foreach (var setup in _setups)
                    {
                        if (setup.Criteria.Matches(arguments))
                        {
                            return setup;
                        }
                    }

                    return null;
                }

                private GetterInvocationImposter GetOrCreate(IndexerIndexerArgumentsCriteria criteria)
                {
                    return _setupLookup.GetOrAdd(criteria, CreateSetup);
                    GetterInvocationImposter CreateSetup(IndexerIndexerArgumentsCriteria key)
                    {
                        GetterInvocationImposter setup = new GetterInvocationImposter(this, _defaultBehaviour, key);
                        _setups.Push(setup);
                        return setup;
                    }
                }

                private void Called(IndexerIndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal string PropertyDisplayName => _propertyDisplayName;

                internal void MarkReturnConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredReturn, true);
                }

                private void EnsureGetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredReturn))
                    {
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerGetterBuilder
                {
                    private readonly GetterImposter _imposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(GetterImposter _imposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._imposter = _imposter;
                        this._criteria = _criteria;
                    }

                    private GetterInvocationImposter InvocationImposter => _imposter.GetOrCreate(_criteria);

                    public IIndexerIndexerGetterBuilder Returns(int value)
                    {
                        InvocationImposter.AddReturnValue(arguments => value);
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Returns(System.Func<int> valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator());
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Returns(IndexerIndexerDelegate valueGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => valueGenerator(arguments.index));
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Throws(System.Exception exception)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exception);
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Throws<TException>()
                        where TException : Exception, new()
                    {
                        InvocationImposter.AddReturnValue(arguments => throw new TException());
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Throws(IndexerIndexerExceptionGenerator exceptionGenerator)
                    {
                        InvocationImposter.AddReturnValue(arguments => throw exceptionGenerator(arguments.index));
                        return this;
                    }

                    public IIndexerIndexerGetterBuilder Callback(IndexerIndexerGetterCallback callback)
                    {
                        InvocationImposter.AddCallback(callback);
                        return this;
                    }

                    public void Called(Imposter.Abstractions.Count count)
                    {
                        _imposter.Called(_criteria, count);
                    }

                    public IIndexerIndexerGetterBuilder Then()
                    {
                        return this;
                    }
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                private sealed class GetterInvocationImposter
                {
                    private readonly GetterImposter _parent;
                    private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                    private readonly System.Collections.Concurrent.ConcurrentQueue<System.Func<IndexerIndexerArguments, int>> _returnValues = new System.Collections.Concurrent.ConcurrentQueue<System.Func<IndexerIndexerArguments, int>>();
                    private readonly System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerGetterCallback> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IndexerIndexerGetterCallback>();
                    private System.Func<IndexerIndexerArguments, int>? _lastReturnValue;
                    private int _invocationCount;
                    private string _propertyDisplayName;
                    internal IndexerIndexerArgumentsCriteria Criteria { get; private set; }
                    internal int InvocationCount => System.Threading.Volatile.Read(ref _invocationCount);

                    internal GetterInvocationImposter(GetterImposter parent, DefaultIndexerIndexerBehaviour defaultBehaviour, IndexerIndexerArgumentsCriteria criteria)
                    {
                        this._parent = parent;
                        this._defaultBehaviour = defaultBehaviour;
                        this._propertyDisplayName = parent.PropertyDisplayName;
                        this.Criteria = criteria;
                    }

                    internal void AddReturnValue(System.Func<IndexerIndexerArguments, int> generator)
                    {
                        _defaultBehaviour.IsOn = false;
                        _returnValues.Enqueue(generator);
                        _parent.MarkReturnConfigured();
                    }

                    internal void AddCallback(IndexerIndexerGetterCallback callback)
                    {
                        _callbacks.Enqueue(callback);
                    }

                    internal int Invoke(IndexerIndexerArguments arguments)
                    {
                        System.Threading.Interlocked.Increment(ref _invocationCount);
                        foreach (var callback in _callbacks)
                        {
                            callback(arguments.index);
                        }

                        System.Func<IndexerIndexerArguments, int> generator = ResolveNextGenerator(arguments);
                        return generator(arguments);
                    }

                    private System.Func<IndexerIndexerArguments, int> ResolveNextGenerator(IndexerIndexerArguments arguments)
                    {
                        if (_defaultBehaviour.IsOn)
                        {
                            return arguments => _defaultBehaviour.Get(arguments);
                        }

                        if (_returnValues.TryDequeue(out System.Func<IndexerIndexerArguments, int> returnValue))
                        {
                            _lastReturnValue = returnValue;
                        }

                        if (_lastReturnValue == null)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (getter)");
                        }

                        return _lastReturnValue;
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private sealed class SetterImposter
            {
                private readonly System.Collections.Concurrent.ConcurrentQueue<(IndexerIndexerArgumentsCriteria Criteria, IndexerIndexerSetterCallback Callback)> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<(IndexerIndexerArgumentsCriteria Criteria, IndexerIndexerSetterCallback Callback)>();
                private readonly System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments> _invocationHistory = new System.Collections.Concurrent.ConcurrentBag<IndexerIndexerArguments>();
                private readonly DefaultIndexerIndexerBehaviour _defaultBehaviour;
                private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
                private readonly string _propertyDisplayName;
                private bool _hasConfiguredSetter;
                internal SetterImposter(DefaultIndexerIndexerBehaviour defaultBehaviour, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string propertyDisplayName)
                {
                    this._defaultBehaviour = defaultBehaviour;
                    this._invocationBehavior = invocationBehavior;
                    this._propertyDisplayName = propertyDisplayName;
                }

                public void Callback(IndexerIndexerArgumentsCriteria criteria, IndexerIndexerSetterCallback callback)
                {
                    _callbacks.Enqueue((criteria, callback));
                }

                public void Called(IndexerIndexerArgumentsCriteria criteria, Imposter.Abstractions.Count count)
                {
                    int invocationCount = _invocationHistory.Count(criteria.Matches);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }

                internal void Set(int index, int value)
                {
                    EnsureSetterConfigured();
                    IndexerIndexerArguments arguments = new IndexerIndexerArguments(index);
                    _invocationHistory.Add(arguments);
                    foreach (var registration in _callbacks)
                    {
                        if (registration.Criteria.Matches(arguments))
                        {
                            registration.Callback(arguments.index, value);
                        }
                    }

                    if (_defaultBehaviour.IsOn)
                    {
                        _defaultBehaviour.Set(arguments, value);
                    }
                }

                private void EnsureSetterConfigured()
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit && !System.Threading.Volatile.Read(ref _hasConfiguredSetter))
                    {
                        throw new Imposter.Abstractions.MissingImposterException(_propertyDisplayName + " (setter)");
                    }
                }

                internal void MarkConfigured()
                {
                    System.Threading.Volatile.Write(ref _hasConfiguredSetter, true);
                }

                [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
                internal class Builder : IIndexerIndexerSetterBuilder
                {
                    private readonly SetterImposter _setterImposter;
                    private readonly IndexerIndexerArgumentsCriteria _criteria;
                    internal Builder(SetterImposter _setterImposter, IndexerIndexerArgumentsCriteria _criteria)
                    {
                        this._setterImposter = _setterImposter;
                        this._criteria = _criteria;
                    }

                    public IIndexerIndexerSetterBuilder Callback(IndexerIndexerSetterCallback callback)
                    {
                        _setterImposter.Callback(_criteria, callback);
                        return this;
                    }

                    public void Called(Imposter.Abstractions.Count count)
                    {
                        _setterImposter.Called(_criteria, count);
                    }

                    public IIndexerIndexerSetterBuilder Then()
                    {
                        return this;
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerGetterBuilder
        {
            IIndexerIndexerGetterBuilder Returns(int value);
            IIndexerIndexerGetterBuilder Returns(System.Func<int> valueGenerator);
            IIndexerIndexerGetterBuilder Returns(IndexerIndexerDelegate valueGenerator);
            IIndexerIndexerGetterBuilder Throws(System.Exception exception);
            IIndexerIndexerGetterBuilder Throws<TException>()
                where TException : Exception, new();
            IIndexerIndexerGetterBuilder Throws(IndexerIndexerExceptionGenerator exceptionGenerator);
            IIndexerIndexerGetterBuilder Callback(IndexerIndexerGetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexerIndexerGetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerSetterBuilder
        {
            IIndexerIndexerSetterBuilder Callback(IndexerIndexerSetterCallback callback);
            void Called(Imposter.Abstractions.Count count);
            IIndexerIndexerSetterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIndexerIndexerBuilder
        {
            IIndexerIndexerGetterBuilder Getter();
            IIndexerIndexerSetterBuilder Setter();
        }

        public AbstractTelemetryServiceImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._computeMethodImposter = new ComputeMethodImposter(_computeMethodInvocationHistoryCollection, invocationBehavior);
            this._Name = new NamePropertyBuilder(invocationBehavior);
            this._StreamAdvanced = new StreamAdvancedEventImposterBuilder();
            this._IndexerIndexer = new IndexerIndexerBuilder(invocationBehavior, "Imposter.Tests.Features.ClassImposter.AbstractTelemetryService.this[int index]");
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.AbstractTelemetryService
        {
            AbstractTelemetryServiceImposter _imposter;
            internal void InitializeImposter(AbstractTelemetryServiceImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            public override int Compute(int value)
            {
                return _imposter._computeMethodImposter.Invoke(value);
            }

            public override string Name
            {
                get
                {
                    return _imposter._Name._getterImposterBuilder.Get();
                }

                set
                {
                    _imposter._Name._setterImposter.Set(value);
                }
            }

            public override event global::System.EventHandler StreamAdvanced
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._StreamAdvanced.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._StreamAdvanced.Unsubscribe(value);
                }
            }

            public override int this[int index]
            {
                get
                {
                    return _imposter._IndexerIndexer.Get(index);
                }

                set
                {
                    _imposter._IndexerIndexer.Set(index, value);
                }
            }
        }
    }
}