using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Imposter.Playground;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.MethodSetup
{
    public class OrdinaryMethodImposterTests
    {
        private readonly IOrdinaryMethodPocImposter _sut = new IOrdinaryMethodPocImposter();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenMethodSetupWithCallBefore_WhenMethodIsInvoked_ShouldInvokeCallback(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => ++callBeforeCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Add(10, 20);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenMethodSetupWithCallBefore_WhenMethodIsInvoked_ShouldInvokeCallbackWithParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var aParametersPassedToCallback = new List<int>();
            var bParametersPassedToCallback = new List<int>();

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) =>
                {
                    callBeforeCallbackInvokedCount++;
                    aParametersPassedToCallback.Add(a);
                    bParametersPassedToCallback.Add(b);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Add(42 + i, 100 + i);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            aParametersPassedToCallback.ShouldBe(Enumerable.Range(42, invocationCount).ToList());
            bParametersPassedToCallback.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
        }

        [Fact]
        public void GivenCallBeforeChainedWithReturns_WhenMethodIsInvoked_ShouldInvokeCallbackBeforeReturn()
        {
            var callbackInvokedCount = 0;
            var returnValue = 0;

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => { callbackInvokedCount++; })
                .Returns(42);

            returnValue = _sut.Instance().Add(1, 2);

            callbackInvokedCount.ShouldBe(1);
            returnValue.ShouldBe(42);
        }

        [Fact]
        public void GivenCallBeforeChainedWithMultipleReturns_WhenMethodIsInvokedMultipleTimes_ShouldInvokeCallbackBeforeEachReturn()
        {
            var callbackInvokedCount = 0;
            var capturedReturnValues = new List<int>();

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => callbackInvokedCount++)
                .Returns(1)
                .Then()
                .Callback((a, b) => callbackInvokedCount++)
                .Returns(2)
                .Then()
                .Callback((a, b) => callbackInvokedCount++)
                .Returns(3);

            capturedReturnValues.Add(_sut.Instance().Add(1, 1));
            capturedReturnValues.Add(_sut.Instance().Add(1, 1));
            capturedReturnValues.Add(_sut.Instance().Add(1, 1));
            capturedReturnValues.Add(_sut.Instance().Add(1, 1)); // Should repeat last

            callbackInvokedCount.ShouldBe(4);
            capturedReturnValues.ShouldBe(new[] { 1, 2, 3, 3 });
        }

        [Fact]
        public void GivenCallBeforeThatThrows_WhenMethodIsInvoked_ShouldPropagateException()
        {
            var expectedException = new InvalidOperationException("Test exception");

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => throw expectedException);

            var thrownException = Should.Throw<InvalidOperationException>(() => _sut.Instance().Add(1, 2));
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GivenCallBeforeThatThrowsWithReturnValue_WhenMethodIsInvoked_ShouldPropagateExceptionAndNotReturnValue()
        {
            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => throw new ArgumentException($"Invalid values: {a}, {b}"))
                .Returns(100);

            Should.Throw<ArgumentException>(() => _sut.Instance().Add(42, 24))
                .Message.ShouldBe("Invalid values: 42, 24");
        }

        [Fact]
        public void GivenSpecificValueMatchSetupWithCallBefore_WhenMethodIsInvokedWithValues_ShouldOnlyInvokeCallbackForMatchingCalls()
        {
            var callBeforeCount = 0;

            _sut
                .Add(Arg<int>.Is(42), Arg<int>.Is(24))
                .Callback((a, b) => callBeforeCount++)
                .Returns(100);

            _sut.Instance().Add(42, 24);
            _sut.Instance().Add(43, 25); // Should not invoke callback
            _sut.Instance().Add(42, 24);

            callBeforeCount.ShouldBe(2);
        }

        [Fact]
        public void GivenPredicateMatchSetupWithCallBefore_WhenMethodIsInvokedWithValues_ShouldOnlyInvokeCallbackForMatchingCalls()
        {
            var callBeforeCount = 0;
            var capturedAValues = new List<int>();
            var capturedBValues = new List<int>();

            _sut
                .Add(Arg<int>.Is(x => x > 10), Arg<int>.Is(x => x < 50))
                .Callback((a, b) =>
                {
                    callBeforeCount++;
                    capturedAValues.Add(a);
                    capturedBValues.Add(b);
                })
                .Returns(999);

            _sut.Instance().Add(5, 40);   // Should not match (a <= 10)
            _sut.Instance().Add(15, 45);  // Should match
            _sut.Instance().Add(20, 60);  // Should not match (b >= 50)
            _sut.Instance().Add(25, 30);  // Should match

            callBeforeCount.ShouldBe(2);
            capturedAValues.ShouldBe(new[] { 15, 25 });
            capturedBValues.ShouldBe(new[] { 45, 30 });
        }

        [Fact]
        public void GivenCallBeforeAndCallAfterChained_WhenMethodIsInvoked_ShouldInvokeBothCallbacks()
        {
            var callBeforeCount = 0;

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => callBeforeCount++);

            _sut.Instance().Add(1, 2);

            callBeforeCount.ShouldBe(1);
        }

        [Fact]
        public void GivenCallBeforeAndCallAfterWithParameters_WhenMethodIsInvoked_ShouldInvokeBothCallbacksWithCorrectParameters()
        {
            var callBeforeValues = new List<(int a, int b)>();
            var callAfterValues = new List<(int a, int b)>();

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => callBeforeValues.Add((a, b)))
                .Returns((a, b) => a + b)
                .Callback((a, b) => callAfterValues.Add((a, b)));

            var result = _sut.Instance().Add(42, 24);

            result.ShouldBe(66);
            callBeforeValues.ShouldBe(new[] { (42, 24) });
            callAfterValues.ShouldBe(new[] { (42, 24) });
        }

        [Fact]
        public void GivenMultipleCallBeforeCallbacks_WhenMethodIsInvoked_ShouldInvokeOnlyLastCallback()
        {
            var firstCallbackInvokedCount = 0;
            var secondCallbackInvokedCount = 0;

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => ++firstCallbackInvokedCount)
                .Callback((a, b) => ++secondCallbackInvokedCount);

            _sut.Instance().Add(1, 2);
            _sut.Instance().Add(1, 2);

            firstCallbackInvokedCount.ShouldBe(0); // Should be overwritten
            secondCallbackInvokedCount.ShouldBe(2);
        }

        [Fact]
        public void GivenReturnValueSetup_WhenMethodIsInvoked_ShouldReturnConfiguredValue()
        {
            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Returns(100);

            var result = _sut.Instance().Add(1, 2);

            result.ShouldBe(100);
        }

        [Fact]
        public void GivenReturnDelegateSetup_WhenMethodIsInvoked_ShouldReturnValueFromDelegate()
        {
            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Returns((a, b) => a * b);

            var result = _sut.Instance().Add(6, 7);

            result.ShouldBe(42);
        }

        [Fact]
        public void GivenSequentialReturns_WhenMethodIsInvokedMultipleTimes_ShouldReturnValuesInSequence()
        {
            var results = new List<int>();

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Returns(10)
                .Then()
                .Returns(20)
                .Then()
                .Returns(30);

            results.Add(_sut.Instance().Add(1, 1));
            results.Add(_sut.Instance().Add(1, 1));
            results.Add(_sut.Instance().Add(1, 1));
            results.Add(_sut.Instance().Add(1, 1)); // Should repeat last

            results.ShouldBe(new[] { 10, 20, 30, 30 });
        }

        [Fact]
        public void GivenThrowsSetup_WhenMethodIsInvoked_ShouldThrowException()
        {
            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Add(1, 2));
        }

        [Fact]
        public void GivenThrowsWithInstanceSetup_WhenMethodIsInvoked_ShouldThrowSpecificException()
        {
            var expectedException = new ArgumentException("Test message");

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Throws(expectedException);

            var thrownException = Should.Throw<ArgumentException>(() => _sut.Instance().Add(1, 2));
            thrownException.ShouldBe(expectedException);
        }

        [Fact]
        public void GivenThrowsWithDelegateSetup_WhenMethodIsInvoked_ShouldThrowExceptionFromDelegate()
        {
            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Throws((a, b) => new ArgumentException($"Sum {a + b} is invalid"));

            var thrownException = Should.Throw<ArgumentException>(() => _sut.Instance().Add(10, 15));
            thrownException.Message.ShouldBe("Sum 25 is invalid");
        }

        [Fact]
        public void GivenInvocationVerification_WhenMethodIsCalledSpecificTimes_ShouldVerifyCorrectly()
        {
            var setup = _sut.Add(Arg<int>.Any(), Arg<int>.Any());

            _sut.Instance().Add(1, 2);
            _sut.Instance().Add(3, 4);

            setup.Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenInvocationVerificationWithCriteria_WhenMethodIsCalledWithDifferentArgs_ShouldVerifyCorrectly()
        {
            var specificSetup = _sut.Add(Arg<int>.Is(42), Arg<int>.Any());
            
            _sut.Instance().Add(42, 1);
            _sut.Instance().Add(43, 2); // Different first arg
            _sut.Instance().Add(42, 3);

            specificSetup.Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenInvocationVerificationFails_WhenWrongCount_ShouldThrowVerificationException()
        {
            var setup = _sut.Add(Arg<int>.Any(), Arg<int>.Any());

            _sut.Instance().Add(1, 2);

            Should.Throw<VerificationFailedException>(() => setup.Called(Count.Exactly(2)));
        }

        [Fact]
        public void GivenNoSetup_WhenMethodIsInvoked_ShouldReturnDefault()
        {
            var result = _sut.Instance().Add(1, 2);

            result.ShouldBe(0); // default(int)
        }

        [Fact]
        public void GivenDifferentArgumentCriteria_WhenMethodIsInvoked_ShouldMatchCorrectSetup()
        {
            _sut.Add(Arg<int>.Is(1), Arg<int>.Any()).Returns(100);
            _sut.Add(Arg<int>.Is(2), Arg<int>.Any()).Returns(200);
            _sut.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(999);

            _sut.Instance().Add(1, 5).ShouldBe(100);
            _sut.Instance().Add(2, 10).ShouldBe(200);
            _sut.Instance().Add(3, 15).ShouldBe(999);
        }

        [Fact]
        public void GivenComplexSequentialSetup_WhenMethodIsInvoked_ShouldExecuteInOrder()
        {
            var executionOrder = new List<string>();

            _sut
                .Add(Arg<int>.Any(), Arg<int>.Any())
                .Callback((a, b) => executionOrder.Add("before-1"))
                .Returns((a, b) => { executionOrder.Add("during-1"); return 10; })
                .Callback((a, b) => executionOrder.Add("after-1"))
                .Then()
                .Callback((a, b) => executionOrder.Add("before-2"))
                .Returns((a, b) => { executionOrder.Add("during-2"); return 20; })
                .Callback((a, b) => executionOrder.Add("after-2"));

            _sut.Instance().Add(1, 1);
            _sut.Instance().Add(1, 1);

            executionOrder.ShouldBe(new[] { 
                "before-1", "during-1", "after-1",
                "before-2", "during-2", "after-2"
            });
        }
    }
}