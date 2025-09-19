// TODO Support [GenerateImposter] attribute locally on a method, class or globally
// Verify
// Void return type
// out and ref arguments
// Task and ValueTask - should just work ?
// Callbase
// Mocking property getter
// mocking property setter
// Multithreaded access

namespace Imposter.Ideation
{
    // TODO support for ref and out parameters

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

    public interface ICalculatorImposter
    {
        CalculatorImposter.CalculatorImposterInstance ImposterInstance { get; }

        CalculatorImposterVerifier Verify();
    }

    public class CalculatorImposterVerifier
    {
        private readonly AddMethodBehaviour _addMethodBehaviour;

        public CalculatorImposterVerifier(AddMethodBehaviour addMethodBehaviour)
        {
            _addMethodBehaviour = addMethodBehaviour;
        }

        public AddMethodInvocationVerifier Add(TestArg<int> leftCriteria, TestArg<int> rightCriteria)
            => new(_addMethodBehaviour, leftCriteria, rightCriteria);
    }

    // Put this generated class in the same or derived namespace to avoid conflict
    public class CalculatorImposter : ICalculatorImposter
    {
        // TODO name of this should be postfixed 1,2,3 if there is a conflict
        private readonly CalculatorImposterInstance _imposterInstance;

        CalculatorImposterInstance ICalculatorImposter.ImposterInstance => _imposterInstance;

        private readonly CalculatorImposterVerifier _verifier;

        CalculatorImposterVerifier ICalculatorImposter.Verify() => _verifier;

        private readonly AddMethodBehaviour _addMethodBehaviour;

        public CalculatorImposter()
        {
            _imposterInstance = new CalculatorImposterInstance(this);
            _addMethodBehaviour = new AddMethodBehaviour();
            _verifier = new CalculatorImposterVerifier(_addMethodBehaviour);
        }

        public AddMethodInvocationSetupBuilder Add(TestArg<int> leftCriteria, TestArg<int> rightCriteria)
        {
            var addMethodInvocationBehaviour = new AddMethodInvocationSetupBuilder((left, right) => leftCriteria.Predicate(left) && rightCriteria.Predicate(right));
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
}
