using System.ComponentModel;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Imposter
{
    public class CalculatorMockTests
    {
        private readonly ITestOutputHelper _output;

        public CalculatorMockTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void WhenParameterCriteriaMatchesAnyInput_InvocationMadeWithRandomParameterCriteria_SetupResultIsReturned()
        {
            var imposter = new CalculatorImposter();

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Returns((left, right) => left * right);

            imposter.ImposterInstance().Add(10, 9).ShouldBe(10 * 9);
        }

        [Fact]
        public void WhenParameterCriteriaMatchesSpecificInput_InvocationMadeWithRandomParameterCriteria_SetupResultIsReturned()
        {
            var imposter = new CalculatorImposter();

            imposter
                .Add(4, 1)
                .Returns(5);

            imposter.ImposterInstance().Add(4, 1).ShouldBe(5);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(4, 6)]
        public void WhenMethodInvocationIsMocked_InvocationMadeWithNonMatchingParameterCriteria_DefaultResultIsReturned(int leftArgument, int rightArgument)
        {
            var mock = new CalculatorImposter();

            mock
                .Add(ParameterCriteria<int>.Is(it => it > 5), ParameterCriteria<int>.Is(it => it < 5))
                .Returns((left, right) => left * right);

            mock.ImposterInstance().Add(leftArgument, rightArgument).ShouldBe(default);
        }

        [Fact]
        public void WhenMethodInvocationIsMockedToThrowExceptionInstance_InvocationMadeWithMatchingParameterCriteria_ExceptionIsThrown()
        {
            var imposter = new CalculatorImposter();
            var exception = new InvalidOperationException();

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Throws(exception);

            Should.Throw<InvalidOperationException>(() => imposter.ImposterInstance().Add(10, 9)).ShouldBe(exception);
        }

        [Fact]
        public void WhenMethodInvocationIsMockedToThrowExceptionByType_InvocationMadeWithMatchingParameterCriteria_ExceptionIsThrown()
        {
            var imposter = new CalculatorImposter();

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => imposter.ImposterInstance().Add(10, 9));
        }

        [Fact]
        public void WhenMethodInvocationIsSetupToThrowExceptionSequence_InvocationMadeWithMatchingParameterCriteria_ExceptionsThrownInSequence()
        {
            var imposter = new CalculatorImposter();

            var exceptions = new Exception[]
            {
                new InvalidOperationException("First exception"),
                new InvalidEnumArgumentException()
            };

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .ThrowsSequence(exceptions);

            Should.Throw<InvalidOperationException>(() => imposter.ImposterInstance().Add(10, 9)).ShouldBe(exceptions[0]);
            Should.Throw<InvalidEnumArgumentException>(() => imposter.ImposterInstance().Add(10, 9)).ShouldBe(exceptions[1]);
        }

        [Fact]
        public void WhenCallbackBeforeReturnIsSetup_InvocationMadeWithMatchingParameterCriteria_CallbackIsInvokedBeforeReturn()
        {
            var imposter = new CalculatorImposter();
            var returnInvoked = false;
            var callbackInvokedBeforeReturn = false;

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Returns((left, right) =>
                {
                    returnInvoked = true;
                    return default;
                })
                .CallBeforeReturn((left, right) => callbackInvokedBeforeReturn = !returnInvoked);

            imposter.ImposterInstance().Add(10, 9);

            callbackInvokedBeforeReturn.ShouldBeTrue();
        }

        [Fact]
        public void WhenCallbackAfterReturnIsSetup_InvocationMadeWithMatchingParameterCriteria_CallbackIsInvokedAfterReturn()
        {
            var imposter = new CalculatorImposter();
            var returnInvoked = false;
            var callbackInvokedAfterReturn = false;

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Returns((left, right) =>
                {
                    returnInvoked = true;
                    return default;
                })
                .CallAfterReturn((left, right) => callbackInvokedAfterReturn = returnInvoked);

            imposter.ImposterInstance().Add(10, 9);

            callbackInvokedAfterReturn.ShouldBeTrue();

        }

        [Fact]
        public void WhenCallbackBeforeAndAfterReturnIsSetup_InvocationMadeWithMatchingParameterCriteria_CallbackIsInvokedBeforeAndAfter()
        {
            var imposter = new CalculatorImposter();
            var returnInvoked = false;
            var callbackInvokedBeforeReturn = false;
            var callbackInvokedAfterReturn = false;

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Returns((left, right) =>
                {
                    returnInvoked = true;
                    return default;
                })
                .CallBeforeReturn((left, right) => callbackInvokedBeforeReturn = !returnInvoked)
                .CallAfterReturn((left, right) => callbackInvokedAfterReturn = returnInvoked);

            imposter.ImposterInstance().Add(10, 9);

            callbackInvokedBeforeReturn.ShouldBeTrue();
            callbackInvokedAfterReturn.ShouldBeTrue();
        }

        [Fact]
        public void WhenParameterCriteriaMatchesAnyInput_InvocationMadeWithRandomParameterCriteria_ShouldBeAbleToSeeInvocationDetails()
        {
            var imposter = new CalculatorImposter();

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .Returns((left, right) => left * right);

            imposter.ImposterInstance().Add(10, 9);

            imposter
                .VerifyAdd(ParameterCriteria<int>.Is(it => it == 10), ParameterCriteria<int>.Is(it => it == 9))
                .WasInvoked(InvocationCount.Exactly(1));
        }

        [Fact]
        public void WhenMethodInvocationIsSetupToReturnSequence_InvocationMadeWithMatchingParameterCriteria_ResultsReturnedInSequence()
        {
            var imposter = new CalculatorImposter();

            var results = new[] { 10, 20, 30 };

            imposter
                .Add(ParameterCriteria<int>.IsAny(), ParameterCriteria<int>.IsAny())
                .ReturnsSequence(results);

            imposter.ImposterInstance().Add(1, 2).ShouldBe(10);
            imposter.ImposterInstance().Add(3, 4).ShouldBe(20);
            imposter.ImposterInstance().Add(5, 6).ShouldBe(30);
            // After the sequence, the last value should repeat
            imposter.ImposterInstance().Add(7, 8).ShouldBe(30);
        }

        [Fact]
        public void Mq()
        {
            var mock = new Mock<ICalculator>();

            mock.Setup(m => m.Add(It.IsAny<int>(), It.IsAny<int>()))
                         .Callback<int, int>((a, b) => _output.WriteLine("Callback generic"))
                         .Returns((int a, int b) =>
                         {
                             throw new Exception();
                         })
                         .Callback<int, int>((a, b) => _output.WriteLine("Callback generic 2"));


            Should.Throw<Exception>(() => mock.Object.Add(1, 2));
        }
    }
}
