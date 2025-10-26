using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.PropertySetupPoc
{
    public class IndexerSetupPocTests
    {
        private readonly IndexerSetupPoc _sut = new IndexerSetupPoc();

        [Fact]
        public void GivenSetupToReturnAValue_WhenPropertyIsAccessed_ShouldReturnSetupValue()
        {
            _sut.Age.Returns(33);

            _sut.Instance().Age.ShouldBe(33);
        }

        [Fact]
        public void GivenSetupToReturnAValue_WhenPropertyIsAccessedMultipleTimes_ShouldReturnSetupValue()
        {
            _sut.Age.Returns(33);

            _sut.Instance().Age.ShouldBe(33);
            _sut.Instance().Age.ShouldBe(33);
            _sut.Instance().Age.ShouldBe(33);
            _sut.Instance().Age.ShouldBe(33);
        }

        [Fact]
        public void GivenNoSetup_WhenPropertyIsAccessed_ShouldActAsNormalProperty()
        {
            var sutInstance = _sut.Instance();

            sutInstance.Age.ShouldBe(0);

            sutInstance.Age = 33;
            sutInstance.Age.ShouldBe(33);

            sutInstance.Age = 44;
            sutInstance.Age.ShouldBe(44);
        }

        #region Sequential Returns Tests

        [Fact]
        public void GivenSequentialSetup_WhenMultipleCallsMade_ShouldReturnInSequence()
        {
            _sut.Age.Returns(10).Returns(20).Returns(30);

            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(30);
        }

        [Fact]
        public void GivenSequentialSetupExhausted_WhenPropertyIsAccessed_ShouldReturnLastValue()
        {
            _sut.Age.Returns(10).Returns(20);

            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(20); // Should repeat last value
            _sut.Instance().Age.ShouldBe(20); // Should repeat last value
        }

        #endregion

        #region Function Returns Tests

        [Fact]
        public void GivenFunctionSetup_WhenPropertyIsAccessedMultipleTimes_ShouldCallFunctionEachTime()
        {
            var counter = 0;
            _sut.Age.Returns(() => ++counter);

            _sut.Instance().Age.ShouldBe(1);
            _sut.Instance().Age.ShouldBe(2);
        }

        [Fact]
        public void GivenMixedFunctionAndValueSetup_WhenPropertyIsAccessedSequentially_ShouldWorkInSequence()
        {
            _sut.Age.Returns(10).Returns(() => 20).Returns(30);

            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(30);
        }

        #endregion

        #region Exception Throwing Tests

        [Fact]
        public void GivenSetupToThrowExceptionInstance_WhenPropertyIsAccessed_ShouldThrowSameException()
        {
            var exception = new ArgumentException("Custom message");
            _sut.Age.Throws(exception);

            var thrownException = Should.Throw<ArgumentException>(() => _sut.Instance().Age);
            thrownException.ShouldBeSameAs(exception);
        }

        [Fact]
        public void GivenSetupToThrowGenericException_WhenPropertyIsAccessed_ShouldThrowNewInstance()
        {
            _sut.Age.Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age);
        }

        [Fact]
        public void GivenSequenceWithException_WhenPropertyIsAccessedSequentially_ShouldWorkCorrectly()
        {
            _sut.Age.Returns(10).Throws<ArgumentException>().Returns(30);

            _sut.Instance().Age.ShouldBe(10);
            Should.Throw<ArgumentException>(() => _sut.Instance().Age);
            _sut.Instance().Age.ShouldBe(30);
        }

        #endregion

        #region Auto Property vs Explicit Setup Tests

        [Fact]
        public void GivenAutoPropertyThenExplicitSetup_WhenPropertyIsAccessed_ShouldLoseAutoPropertyBehavior()
        {
            var instance = _sut.Instance();

            // Initially auto-property
            instance.Age = 25;
            instance.Age.ShouldBe(25);

            // Setup explicit return
            _sut.Age.Returns(42);

            // Now it's explicit - setting should not affect getter
            instance.Age = 100;
            instance.Age.ShouldBe(42); // Should still return 42, ignoring the set
        }

        [Fact]
        public void GivenExplicitSetup_WhenPropertyIsModified_ShouldNotRevertToAutoProperty()
        {
            var instance = _sut.Instance();

            // Setup as explicit
            _sut.Age.Returns(42);
            instance.Age.ShouldBe(42);

            // Set a value (this goes to backing field but won't be returned)
            instance.Age = 100;

            // Still returns explicit value, not set value
            instance.Age.ShouldBe(42);
        }

        #endregion

        #region Callback Tests

        [Fact]
        public void GivenSetterCallbackWithMatchingCriteria_WhenPropertyIsSet_ShouldInvokeCallback()
        {
            var capturedValue = 0;
            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 10), value => capturedValue = value);

            _sut.Instance().Age = 15;

            capturedValue.ShouldBe(15);
        }

        [Fact]
        public void GivenSetterCallbackWithNonMatchingCriteria_WhenPropertyIsSet_ShouldNotInvokeCallback()
        {
            var callbackInvoked = false;
            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 10), _ => callbackInvoked = true);

            _sut.Instance().Age = 5;

            callbackInvoked.ShouldBeFalse();
        }

        [Fact]
        public void GivenMultipleSetterCallbacks_WhenPropertyIsSet_ShouldInvokeAllMatchingCallbacks()
        {
            var callback1Invoked = false;
            var callback2Invoked = false;

            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 0), _ => callback1Invoked = true);
            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 10), _ => callback2Invoked = true);

            _sut.Instance().Age = 15;

            callback1Invoked.ShouldBeTrue();
            callback2Invoked.ShouldBeTrue();
        }

        [Fact]
        public void GivenGetterCallback_WhenPropertyIsAccessedMultipleTimes_ShouldInvokeCallbackEachTime()
        {
            var invocationCount = 0;
            _sut.Age.GetterCallback(() => invocationCount++);

            _sut.Instance().Age.ShouldBe(0);
            _sut.Instance().Age.ShouldBe(0);

            invocationCount.ShouldBe(2);
        }

        [Fact]
        public void GivenMultipleGetterCallback_WhenPropertyIsAccessed_ShouldInvokeEachCallback()
        {
            var callback1Count = 0;
            var callback2Count = 0;

            _sut.Age.GetterCallback(() => callback1Count++);
            _sut.Age.GetterCallback(() => callback2Count++);

            _sut.Instance().Age.ShouldBe(0);

            callback1Count.ShouldBe(1);
            callback2Count.ShouldBe(1);
        }

        #endregion

        #region Verification Tests

        [Fact]
        public void GivenPropertyInteractions_WhenVerifyingCorrectGetterCount_ShouldNotThrow()
        {
            _sut.Instance().Age.ShouldBe(0);
            _sut.Instance().Age.ShouldBe(0);

            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Exactly(2)));
        }

        [Fact]
        public void GivenPropertyInteractions_WhenVerifyingIncorrectGetterCount_ShouldThrowException()
        {
            _sut.Instance().Age.ShouldBe(0);

            var exception = Should.Throw<Exception>(() => _sut.Age.GetterCalled(Count.Exactly(2)));
            exception.Message.ShouldContain("Expected exactly 2 time(s) times, but was 1");
        }

        [Fact]
        public void GivenPropertySetterCalls_WhenVerifyingWithMatchingCriteria_ShouldVerifyCorrectCount()
        {
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;
            _sut.Instance().Age = 10;

            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Is(x => x == 20), Count.Exactly(1)));
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Is(x => x == 10), Count.Exactly(2)));
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Any(), Count.Exactly(3)));
        }

        [Fact]
        public void GivenPropertySetterCalls_WhenVerifyingWithNonMatchingCriteria_ShouldVerifyZeroCount()
        {
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;

            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Is(x => x == 99), Count.Never()));
        }

        #endregion

        #region Thread Safety Tests

        [Fact]
        public void GivenConcurrentOperations_WhenMixingGettersAndSetters_ShouldNotThrowExceptions()
        {
            var threads = new Thread[100];

            // Mix of getters and setters
            for (int i = 0; i < 100; i++)
            {
                var index = i;
                if (index % 2 == 0)
                {
                    threads[i] = new Thread(() => _sut.Instance().Age = index);
                }
                else
                {
                    threads[i] = new Thread(() =>
                    {
                        var _ = _sut.Instance().Age;
                    });
                }
            }
            
            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Should not throw - just testing for thread safety
            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Exactly(50)));
            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Exactly(50)));
        }

        [Fact]
        public async Task GivenSequentialReturnsSetup_WhenAccessedConcurrently_ShouldWorkCorrectly()
        {
            // Setup many sequential returns
            for (int i = 0; i < 100; i++)
            {
                _sut.Age.Returns(i);
            }

            var results = new int[100];
            var threads = new Thread[100];

            for (int i = 0; i < 100; i++)
            {
                var index = i;
                threads[i] = new Thread(() => results[index] = _sut.Instance().Age);
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
            
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // All results should be unique values from 0-99 (in some order)
            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < 100);
        }

        #endregion

        #region Edge Cases and Bug Detection

        [Fact]
        public void GivenCallbackThatThrows_WhenPropertyIsSet_ValueShouldNotBeSet()
        {
            _sut.Age.SetterCallback(Arg<int>.Any(), _ => throw new InvalidOperationException("Callback error"));
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age = 42);

            _sut.Instance().Age.ShouldBe(default);
        }
        
        [Fact]
        public void GivenCallbackThatThrows_WhenPropertyIsSet_ShouldTrackSetHistory()
        {
            _sut.Age.SetterCallback(Arg<int>.Any(), _ => throw new InvalidOperationException("Callback error"));

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age = 42);

            _sut.Age.SetterCalled(Arg<int>.Is(it => it == 42), Count.Exactly(1));
        }

        [Fact]
        public void GivenGetterCallbackThatThrows_WhenPropertyIsAccessed_ShouldTrackGetHistory()
        {
            _sut.Age.GetterCallback(() => throw new InvalidOperationException("Getter callback error"));

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age);
            
            _sut.Age.GetterCalled(Count.Exactly(1));
        }

        [Fact]
        public void GivenNullFunction_WhenPropertyIsAccessed_ShouldThrowNullReferenceException()
        {
            _sut.Age.Returns((Func<int>)null);

            Should.Throw<NullReferenceException>(() => _sut.Instance().Age);
        }

        [Fact]
        public void GivenNullCallback_WhenPropertyIsSet_ShouldThrowOnInvocation()
        {
            // This should not throw when setting up
            Should.NotThrow(() => _sut.Age.SetterCallback(Arg<int>.Any(), null));

            // But will throw when callback is invoked
            Should.Throw<NullReferenceException>(() => _sut.Instance().Age = 42);
        }

        [Fact]
        public void GivenNoInteractions_WhenVerifying_ShouldWork()
        {
            // No interactions yet
            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Never()));
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Any(), Count.Never()));
        }

        [Fact]
        public void GivenExplicitMode_WhenPropertyIsSet_ShouldStillTrackSetHistory()
        {
            // Set up explicit mode
            _sut.Age.Returns(42);

            // Set some values (these should be tracked even though they don't affect getter)
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;
            _sut.Instance().Age = 10;

            // Verification should work based on set history
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Is(x => x == 10), Count.Exactly(2)));
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Any(), Count.Exactly(3)));
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void GivenComplexScenario_WhenTransitioningFromAutoToExplicitWithCallbacks_ShouldWorkCorrectly()
        {
            var instance = _sut.Instance();
            var callbackValues = new List<int>();

            // Start as auto property
            instance.Age = 25;
            instance.Age.ShouldBe(25);

            // Add setter callback
            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 30), value => callbackValues.Add(value));

            // Still auto property
            instance.Age = 35;
            instance.Age.ShouldBe(35);
            callbackValues.ShouldContain(35);

            // Switch to explicit
            _sut.Age.Returns(100);

            // Now getter returns explicit value
            instance.Age.ShouldBe(100);

            // But setter still triggers callbacks
            instance.Age = 40;
            instance.Age.ShouldBe(100); // Still explicit return
            callbackValues.ShouldContain(40);

            // Verification works for all interactions
            _sut.Age.SetterCalled(Arg<int>.Any(), Count.Exactly(3));
            _sut.Age.GetterCalled(Count.AtLeast(4));
        }

        #endregion
    }
}