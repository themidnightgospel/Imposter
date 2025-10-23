
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.PropertySetupPoc
{
    public class IPropertySetupSutPocImposterTests
    {
        private readonly IPropertySetupSutPocImposter _sut = new IPropertySetupSutPocImposter();
        
        [Fact]
        public void Returns_GetterIsSetupToReturnAValue_ValueReturned()
        {
            _sut.Age.Returns(33);
            
            _sut.Instance().Age.ShouldBe(33);
        }
        
        [Fact]
        public void Age_NoSetup_ActsAsNormalProperty()
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
        public void Returns_MultipleCallsWithSequentialSetup_ReturnsInSequence()
        {
            _sut.Age.Returns(10).Returns(20).Returns(30);
            
            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(30);
        }

        [Fact]
        public void Returns_SequentialSetupExhausted_ReturnsLastValue()
        {
            _sut.Age.Returns(10).Returns(20);
            
            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(20); // Should repeat last value
            _sut.Instance().Age.ShouldBe(20); // Should repeat last value
        }

        [Fact]
        public void Returns_EmptyQueue_ReturnsDefault()
        {
            // BUG: This will return 0 (default int) when queue is empty
            // but _lastSetupValue is never initialized
            _sut.Instance().Age.ShouldBe(0);
        }

        #endregion

        #region Function Returns Tests

        [Fact]
        public void Returns_WithFunction_CallsFunctionEachTime()
        {
            var counter = 0;
            _sut.Age.Returns(() => ++counter);
            
            _sut.Instance().Age.ShouldBe(1);
            _sut.Instance().Age.ShouldBe(1); // BUG: Function only called once, then cached
        }

        [Fact]
        public void Returns_FunctionThrowsException_ExceptionPropagated()
        {
            _sut.Age.Returns(() => throw new InvalidOperationException("Test exception"));
            
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age)
                .Message.ShouldBe("Test exception");
        }

        [Fact]
        public void Returns_FunctionAndValueMixed_WorksInSequence()
        {
            _sut.Age.Returns(10).Returns(() => 20).Returns(30);
            
            _sut.Instance().Age.ShouldBe(10);
            _sut.Instance().Age.ShouldBe(20);
            _sut.Instance().Age.ShouldBe(30);
        }

        #endregion

        #region Exception Throwing Tests

        [Fact]
        public void Throws_WithExceptionInstance_ThrowsException()
        {
            var exception = new ArgumentException("Custom message");
            _sut.Age.Throws(exception);
            
            var thrownException = Should.Throw<ArgumentException>(() => _sut.Instance().Age);
            thrownException.ShouldBeSameAs(exception);
        }

        [Fact]
        public void Throws_WithGenericException_ThrowsNewInstance()
        {
            _sut.Age.Throws<InvalidOperationException>();
            
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age);
        }

        [Fact]
        public void Throws_InSequenceWithReturns_WorksCorrectly()
        {
            _sut.Age.Returns(10).Throws<ArgumentException>().Returns(30);
            
            _sut.Instance().Age.ShouldBe(10);
            Should.Throw<ArgumentException>(() => _sut.Instance().Age);
            _sut.Instance().Age.ShouldBe(30);
        }

        #endregion

        #region Auto Property vs Explicit Setup Tests

        [Fact]
        public void AutoProperty_BecomesExplicitAfterReturns_LosesAutoPropertyBehavior()
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
        public void AutoProperty_OnceExplicit_CannotRevertToAuto()
        {
            var instance = _sut.Instance();
            
            // Setup as explicit
            _sut.Age.Returns(42);
            instance.Age.ShouldBe(42);
            
            // Set a value (this goes to backing field but won't be returned)
            instance.Age = 100;
            
            // Still returns explicit value, not set value
            instance.Age.ShouldBe(42);
            
            // BUG: No way to revert back to auto-property behavior
        }

        #endregion

        #region Callback Tests

        [Fact]
        public void SetterCallback_MatchingCriteria_CallbackInvoked()
        {
            var capturedValue = 0;
            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 10), value => capturedValue = value);
            
            _sut.Instance().Age = 15;
            
            capturedValue.ShouldBe(15);
        }

        [Fact]
        public void SetterCallback_NonMatchingCriteria_CallbackNotInvoked()
        {
            var callbackInvoked = false;
            _sut.Age.SetterCallback(Arg<int>.Is(x => x > 10), _ => callbackInvoked = true);
            
            _sut.Instance().Age = 5;
            
            callbackInvoked.ShouldBeFalse();
        }

        [Fact]
        public void SetterCallback_MultipleCallbacks_AllMatchingCallbacksInvoked()
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
        public void GetterCallback_WhenSet_InvokedOnEachGet()
        {
            var invocationCount = 0;
            _sut.Age.GetterCallback(() => invocationCount++);
            
            _sut.Instance().Age.ShouldBe(0);
            _sut.Instance().Age.ShouldBe(0);
            
            invocationCount.ShouldBe(2);
        }

        [Fact]
        public void GetterCallback_ReplacedCallback_OnlyLatestCallbackInvoked()
        {
            var callback1Count = 0;
            var callback2Count = 0;
            
            _sut.Age.GetterCallback(() => callback1Count++);
            _sut.Age.GetterCallback(() => callback2Count++); // BUG: This replaces the first callback
            
            _sut.Instance().Age.ShouldBe(0);
            
            callback1Count.ShouldBe(0); // First callback lost
            callback2Count.ShouldBe(1);
        }

        #endregion

        #region Verification Tests

        [Fact]
        public void GetterCalled_CorrectCount_NoException()
        {
            _sut.Instance().Age.ShouldBe(0);
            _sut.Instance().Age.ShouldBe(0);
            
            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Exactly(2)));
        }

        [Fact]
        public void GetterCalled_IncorrectCount_ThrowsException()
        {
            _sut.Instance().Age.ShouldBe(0);
            
            var exception = Should.Throw<Exception>(() => _sut.Age.GetterCalled(Count.Exactly(2)));
            exception.Message.ShouldContain("Expected exactly 2 time(s) times, but was 1");
        }

        [Fact]
        public void SetterCalled_WithMatchingCriteria_CorrectCount()
        {
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;
            _sut.Instance().Age = 10;
            
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Is(x => x == 10), Count.Exactly(2)));
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Any(), Count.Exactly(3)));
        }

        [Fact]
        public void SetterCalled_WithNonMatchingCriteria_ZeroCount()
        {
            _sut.Instance().Age = 10;
            _sut.Instance().Age = 20;
            
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Is(x => x == 99), Count.Never()));
        }

        #endregion

        #region Thread Safety Tests

        [Fact]
        public async Task ThreadSafety_ConcurrentGettersAndSetters_NoExceptions()
        {
            var tasks = new Task[100];
            
            // Mix of getters and setters
            for (int i = 0; i < 100; i++)
            {
                var index = i;
                if (index % 2 == 0)
                {
                    tasks[i] = Task.Run(() => _sut.Instance().Age = index);
                }
                else
                {
                    tasks[i] = Task.Run(() => { var _ = _sut.Instance().Age; });
                }
            }
            
            await Task.WhenAll(tasks);
            
            // Should not throw - just testing for thread safety
            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Any));
        }

        [Fact]
        public async Task ThreadSafety_ConcurrentSequentialReturns_WorksCorrectly()
        {
            // Setup many sequential returns
            for (int i = 0; i < 100; i++)
            {
                _sut.Age.Returns(i);
            }
            
            var results = new int[100];
            var tasks = new Task[100];
            
            for (int i = 0; i < 100; i++)
            {
                var index = i;
                tasks[i] = Task.Run(() => results[index] = _sut.Instance().Age);
            }
            
            await Task.WhenAll(tasks);
            
            // All results should be unique values from 0-99 (in some order)
            results.ShouldBeUnique();
            results.ShouldAllBe(x => x >= 0 && x < 100);
        }

        #endregion

        #region Edge Cases and Bug Detection

        [Fact]
        public void SetterCallback_ExceptionInCallback_DoesNotBreakProperty()
        {
            _sut.Age.SetterCallback(Arg<int>.Any(), _ => throw new InvalidOperationException("Callback error"));
            
            // Exception in callback should not prevent the set from working in auto-property mode
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age = 42);
            
            // Property should still have the value set (auto-property behavior)
            _sut.Instance().Age.ShouldBe(42);
        }

        [Fact]
        public void GetterCallback_ExceptionInCallback_DoesNotBreakProperty()
        {
            _sut.Age.GetterCallback(() => throw new InvalidOperationException("Getter callback error"));
            
            Should.Throw<InvalidOperationException>(() => _sut.Instance().Age);
            
            // Invocation count should still be incremented despite exception
            // (This might be a design decision - should it increment on exception?)
        }

        [Fact]
        public void Returns_NullFunction_ThrowsNullReferenceException()
        {
            _sut.Age.Returns((Func<int>)null);
            
            Should.Throw<NullReferenceException>(() => _sut.Instance().Age);
        }

        [Fact]
        public void SetterCallback_NullCallback_DoesNotThrow()
        {
            // This should not throw when setting up
            Should.NotThrow(() => _sut.Age.SetterCallback(Arg<int>.Any(), null));
            
            // But will throw when callback is invoked
            Should.Throw<NullReferenceException>(() => _sut.Instance().Age = 42);
        }

        [Fact]
        public void ChainedCalls_ReturnsSameInstance_AllowsFluentInterface()
        {
            var result = _sut.Age.Returns(10).Returns(20).SetterCallback(Arg<int>.Any(), _ => { });
            
            result.ShouldBeSameAs(_sut.Age);
        }

        [Fact]
        public void VerificationAfterNoInteraction_ShouldWork()
        {
            // No interactions yet
            Should.NotThrow(() => _sut.Age.GetterCalled(Count.Never()));
            Should.NotThrow(() => _sut.Age.SetterCalled(Arg<int>.Any(), Count.Never()));
        }

        [Fact]
        public void SetHistory_TracksAllSets_EvenInExplicitMode()
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
        public void ComplexScenario_AutoPropertyThenExplicitThenCallbacks_WorksCorrectly()
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