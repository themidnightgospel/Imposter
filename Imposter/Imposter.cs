// TODO Support [GenerateImposter] attribute locally on a method, class or globally
// Verify
// Void return type
// out and ref arguments
// Task and ValueTask - should just work ?
// Callbase
// Mocking property getter
// mocking property setter
// Multithreaded access

namespace Imposter
{
    public interface ICalculator
    {
        // TODO what if it has default impl
        int Add(int left, int right);

        int Age { get; set; }
    }

    public class AddMethodBehaviour
    {
        public List<AddMethodInvocation> Invocations { get; } = new();

        // Support multiple return values,
        public List<AddMethodInvocationBehaviour> InvocationBehaviours { get; } = new();

        public int Invoke(int left, int right)
        {
            // Most recently setup behaviour is invoked first
            // Imagine if you return a mock from a method but you want to override it's setup, this will allow it
            var invocationBehaviour = Enumerable.Reverse(InvocationBehaviours).FirstOrDefault(it => it.Matches(left, right));
            int result = default;

            try
            {
                result = invocationBehaviour?.Invoke(left, right) ?? default;
                Invocations.Add(new AddMethodInvocation((left, right), result, null));
            }
            catch (Exception ex)
            {
                Invocations.Add(new AddMethodInvocation((left, right), result, ex));
                throw;
            }

            return result;
        }

        public int CountInvocation(ParameterCriteria<int> leftCriteria, ParameterCriteria<int> rightCriteria)
        {
            return Invocations
                .Count(it => leftCriteria.Predicate(it.Arguments.left) && rightCriteria.Predicate(it.Arguments.right));
        }
    }

    // TODO support for ref and out parameters
    public class AddMethodInvocationBehaviour(Func<int, int, bool> parameterCriteria)
    {
        public int InvocationCounter;

        private readonly List<Func<int, int, int>> _returns = new();
        private Action<int, int>? _callbackBeforeReturn;
        private Action<int, int>? _callbackAfterReturn;

        internal bool Matches(int left, int right) => parameterCriteria(left, right);

        public AddMethodInvocationBehaviour Returns(Func<int, int, int> returns)
        {
            _returns.Add(returns);
            return this;
        }

        public AddMethodInvocationBehaviour Throws<TException>()
            where TException : Exception, new()
        {
            _returns.Add((_, _) => throw new TException());
            return this;
        }

        public AddMethodInvocationBehaviour Throws(Exception exception)
        {
            _returns.Add((_, _) => throw exception);
            return this;
        }

        public AddMethodInvocationBehaviour ThrowsSequence(IEnumerable<Exception> exceptions)
        {
            foreach (var exception in exceptions)
            {
                _returns.Add((_, _) => throw exception);
            }

            return this;
        }

        public AddMethodInvocationBehaviour Returns(int result)
        {
            _returns.Add((_, _) => result);
            return this;
        }

        public AddMethodInvocationBehaviour ReturnsSequence(IEnumerable<int> results)
        {
            foreach (var result in results)
            {
                _returns.Add((_, _) => result);
            }

            return this;
        }

        public AddMethodInvocationBehaviour CallBeforeReturn(Action<int, int> callbackBeforeReturn)
        {
            _callbackBeforeReturn = callbackBeforeReturn;
            return this;
        }

        public AddMethodInvocationBehaviour CallAfterReturn(Action<int, int> callbackAfterReturn)
        {
            _callbackAfterReturn = callbackAfterReturn;
            return this;
        }

        public int Invoke(int left, int right)
        {
            ++InvocationCounter;

            if (_callbackBeforeReturn is not null)
            {
                _callbackBeforeReturn.Invoke(left, right);
            }

            // TODO If exception happens
            var result = _returns.Count >= InvocationCounter
                ? _returns[InvocationCounter - 1].Invoke(left, right)
                : _returns.Last().Invoke(left, right);

            if (_callbackAfterReturn is not null)
            {
                _callbackAfterReturn.Invoke(left, right);
            }

            return result;
        }
    }

    public class AddMethodInvocation
    {
        public AddMethodInvocation((int left, int right) arguments, int result, Exception? exception)
        {
            Arguments = arguments;
            Result = result;
            Exception = exception;
        }

        public (int left, int right) Arguments { get; }

        public int? Result { get; }

        public Exception? Exception { get; }
    }

    public class AddMethodInvocationVerifier(
        AddMethodBehaviour addMethodBehaviour,
        ParameterCriteria<int> leftCriteria,
        ParameterCriteria<int> rightCriteria)
    {
        public void WasInvoked(InvocationCount count)
        {
            var invocationCount = addMethodBehaviour.CountInvocation(leftCriteria, rightCriteria);

            if (!count.Matches(invocationCount))
            {
                throw new VerificationFailedException("TODO");
            }
        }
    }

    public class PropertyBehaviour
    {
        public List<PropertyGetterBehaviour> GetterBehaviours { get; } = new();

        public List<PropertySetterBehaviour> SetterBehaviours { get; } = new();
    }

    public class PropertyGetterBehaviour
    {
        public Func<int> ResultGenerator { get; }
    }

    public class PropertySetterBehaviour
    {
        public Func<int> ResultGenerator { get; }
    }

    public class ParameterCriteria<T>(Func<T, bool> predicate)
    {
        public Func<T, bool> Predicate = predicate;

        public static implicit operator ParameterCriteria<T>(T value)
        {
            // TODO possible null check
            return new ParameterCriteria<T>(t => t.Equals(value));
        }

        public static ParameterCriteria<T> Is(Func<T, bool> predicate) => new(predicate);

        public static ParameterCriteria<T> IsAny() => new(_ => true);

        // TODO Add more utility factory methods similar to Moq.It
    }

    public static class CalculatorImposterExtensions
    {
        public static CalculatorImposter.CalculatorImposterInstance ImposterInstance(this ICalculatorImposter calculatorImposter)
            => calculatorImposter.ImposterInstance;

        public static AddMethodInvocationVerifier VerifyAdd(this ICalculatorImposter calculatorImposter, ParameterCriteria<int> leftCriteria, ParameterCriteria<int> rightCriteria)
            => calculatorImposter.VerifyAdd(leftCriteria, rightCriteria);
    }

    public interface ICalculatorImposter
    {
        CalculatorImposter.CalculatorImposterInstance ImposterInstance { get; }

        AddMethodInvocationVerifier VerifyAdd(ParameterCriteria<int> leftCriteria, ParameterCriteria<int> rightCriteria);
    }

    // Put this generated class in the same or derived namespace to avoid conflict
    public class CalculatorImposter : ICalculatorImposter
    {
        // TODO name of this should be postfixed 1,2,3 if there is a conflict
        private CalculatorImposterInstance _imposterInstance;

        CalculatorImposterInstance ICalculatorImposter.ImposterInstance => _imposterInstance;

        AddMethodInvocationVerifier ICalculatorImposter.VerifyAdd(ParameterCriteria<int> leftCriteria, ParameterCriteria<int> rightCriteria)
            => new(_addMethodBehaviour, leftCriteria, rightCriteria);

        private readonly AddMethodBehaviour _addMethodBehaviour;

        public CalculatorImposter()
        {
            _imposterInstance = new CalculatorImposterInstance(this);
            _addMethodBehaviour = new AddMethodBehaviour();
        }

        public AddMethodInvocationBehaviour Add(ParameterCriteria<int> leftCriteria, ParameterCriteria<int> rightCriteria)
        {
            var addMethodInvocationBehaviour = new AddMethodInvocationBehaviour((left, right) => leftCriteria.Predicate(left) && rightCriteria.Predicate(right));
            _addMethodBehaviour.InvocationBehaviours.Add(addMethodInvocationBehaviour);

            return addMethodInvocationBehaviour;
        }

        // - Move behaviour as explicit interface impl
        public class CalculatorImposterInstance(CalculatorImposter behaviour) : ICalculator
        {
            public int Multiply(int left, int right)
            {
                return left * right;
            }

            public int Add(int left, int right)
            {
                return behaviour._addMethodBehaviour.Invoke(left, right);
            }

            public int Add(int left, int right, double third)
            {
                return 1;
            }

            public int Age
            {
                get
                {
                    return 1;
                }
                set
                {

                }
            }
        }
    }

    public class VerificationFailedException : Exception
    {
        public VerificationFailedException(string message) : base(message)
        { }

        public VerificationFailedException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
