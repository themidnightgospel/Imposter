using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class CallbackTests
    {
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
        IPropertySetupSut.Imposter();
#else
        new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenSetterCallbackWithMatchingCriteria_WhenPropertyIsSet_ShouldInvokeCallback()
        {
            var capturedValue = 0;
            _sut.Age.Setter(Arg<int>.Is(x => x > 10)).Callback(value => capturedValue = value);

            _sut.Instance().Age = 15;

            capturedValue.ShouldBe(15);
        }

        [Fact]
        public void GivenSetterCallbackWithNonMatchingCriteria_WhenPropertyIsSet_ShouldNotInvokeCallback()
        {
            var callbackInvoked = false;
            _sut.Age.Setter(Arg<int>.Is(x => x > 10)).Callback(_ => callbackInvoked = true);

            _sut.Instance().Age = 5;

            callbackInvoked.ShouldBeFalse();
        }

        [Fact]
        public void GivenMultipleSetterCallbacks_WhenPropertyIsSet_ShouldInvokeEachMatchingSetupCallback()
        {
            var callback1Invoked = false;
            var callback2Invoked = false;

            _sut.Age.Setter(Arg<int>.Is(x => x == 15))
                .Callback(_ => callback1Invoked = true)
                .Then()
                .Callback(_ => callback2Invoked = true);

            _sut.Instance().Age = 15;

            callback1Invoked.ShouldBeTrue();
            callback2Invoked.ShouldBeTrue();
        }

        [Fact]
        public void GivenGetterCallback_WhenPropertyIsAccessedMultipleTimes_ShouldInvokeCallbackEachTime()
        {
            var invocationCount = 0;
            _sut.Age.Getter().Callback(() => invocationCount++);

            _sut.Instance().Age.ShouldBe(0);
            _sut.Instance().Age.ShouldBe(0);

            invocationCount.ShouldBe(2);
        }

        [Fact]
        public void GivenMultipleGetterCallback_WhenPropertyIsAccessed_ShouldInvokeEachCallback()
        {
            var callback1Count = 0;
            var callback2Count = 0;

            _sut.Age.Getter()
                .Callback(() => callback1Count++)
                .Then()
                .Callback(() => callback2Count++);

            _sut.Instance().Age.ShouldBe(0);

            callback1Count.ShouldBe(1);
            callback2Count.ShouldBe(1);
        }
    }
}
