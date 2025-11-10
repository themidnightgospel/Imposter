using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class ProtectedOverrideableMembersClassImposterTests
    {
        [Fact]
        public void GivenProtectedMethodOverride_WhenInvokedThroughPublicWrapper_ThenConfiguredResultIsReturned()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.ProtectedVirtualMethod(Arg<int>.Is(5)).Returns(42);
            imposter.InvokeProtectedMethod(Arg<int>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();

            instance.InvokeProtectedMethod(5).ShouldBe(42);
        }

        [Fact]
        public void GivenProtectedPropertyGetterOverride_WhenReadThroughPublicAccessor_ThenConfiguredValueIsReturned()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.ProtectedVirtualProperty.Getter().Returns("overridden");
            imposter.ReadProtectedProperty().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.ReadProtectedProperty().ShouldBe("overridden");
        }

        [Fact]
        public void GivenProtectedPropertySetter_WhenWrittenThroughPublicAccessor_ThenSetterInvocationIsTracked()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            var propertySetter = imposter.ProtectedVirtualProperty.Setter(Arg<string>.Any());
            imposter.WriteProtectedProperty(Arg<string>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();
            instance.WriteProtectedProperty("changed");

            Should.NotThrow(() => propertySetter.Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenProtectedIndexerGetterOverride_WhenReadThroughPublicAccessor_ThenConfiguredValueIsReturned()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter[Arg<int>.Any()].Getter().Returns(index => index * 10);
            imposter.ReadProtectedValue(Arg<int>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();

            instance.ReadProtectedValue(3).ShouldBe(30);
        }

        [Fact]
        public void GivenProtectedIndexerSetter_WhenWrittenThroughPublicAccessor_ThenSetterInvocationIsTracked()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            var indexerSetter = imposter[Arg<int>.Is(7)].Setter();
            imposter.WriteProtectedValue(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();
            instance.WriteProtectedValue(7, 123);

            Should.NotThrow(() => indexerSetter.Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenProtectedEvent_WhenRaised_ThenSubscribersAreInvoked()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            var instance = imposter.Instance();

            var eventRaised = false;
            imposter.SubscribeToProtectedEvent(Arg<EventHandler>.Any()).UseBaseImplementation();
            instance.SubscribeToProtectedEvent((sender, args) => eventRaised = true);

            imposter.ProtectedVirtualEvent.Raise(instance, EventArgs.Empty);

            eventRaised.ShouldBeTrue();
        }

        [Fact]
        public void GivenProtectedMethodUseBaseImplementation_WhenInvoked_ThenReturnsBaseResult()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();
            var callbackCount = 0;

            imposter.ProtectedVirtualMethod(Arg<int>.Any()).UseBaseImplementation();

            imposter.InvokeProtectedMethod(Arg<int>.Any())
                .UseBaseImplementation()
                .Callback((int _) => callbackCount++);

            var instance = imposter.Instance();

            instance.InvokeProtectedMethod(6).ShouldBe(12);
            callbackCount.ShouldBe(1);

            Should.NotThrow(() => imposter.InvokeProtectedMethod(Arg<int>.Any()).Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenProtectedPropertyUseBaseImplementation_WhenInvoked_ThenReadsAndWritesBaseValue()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.ReadProtectedProperty().UseBaseImplementation();
            imposter.WriteProtectedProperty(Arg<string>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();

            instance.ReadProtectedProperty().ShouldBe("protected-default");

            instance.WriteProtectedProperty("updated");
            instance.ReadProtectedProperty().ShouldBe("updated");

            Should.NotThrow(() => imposter.ReadProtectedProperty().Called(Count.Exactly(2)));
            Should.NotThrow(() => imposter.WriteProtectedProperty(Arg<string>.Any()).Called(Count.Exactly(1)));
        }

        [Fact]
        public void GivenProtectedIndexerUseBaseImplementation_WhenInvoked_ThenUsesBaseAccessors()
        {
            var imposter = new ClassWithProtectedOverrideableMembersImposter();

            imposter.ReadProtectedValue(Arg<int>.Any()).UseBaseImplementation();
            imposter.WriteProtectedValue(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();

            instance.ReadProtectedValue(4).ShouldBe(4);
            instance.WriteProtectedValue(2, 99);
            instance.ReadProtectedValue(2).ShouldBe(99);

            Should.NotThrow(() => imposter.ReadProtectedValue(Arg<int>.Any()).Called(Count.Exactly(2)));
            Should.NotThrow(() => imposter.WriteProtectedValue(Arg<int>.Any(), Arg<int>.Any()).Called(Count.Exactly(1)));
        }
    }
}