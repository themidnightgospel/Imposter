using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;
using static Imposter.Tests.Features.Verification.PerformedInvocationTestHelper;

namespace Imposter.Tests.Features.Verification
{
    public class EventPerformedInvocationExceptionMessageTests
    {
        [Fact]
        public void GivenEventRaisedTooFewTimes_WhenVerificationFails_ShouldListEachRaise()
        {
            var sut = PerformedInvocationSutFactory.CreateEventSut();
            var firstSender = this;
            var secondSender = new object();
            var eventArgs = EventArgs.Empty;
            sut.SomethingHappened.Raise(firstSender, eventArgs);
            sut.SomethingHappened.Raise(secondSender, eventArgs);

            var expectedCount = Count.Exactly(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.SomethingHappened.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), expectedCount)
            );

            var entries = exception.ReadEntries();
            var expectedEntries = new[]
            {
                $"SomethingHappened raised {FormatValue("SomethingHappened")} sender: {FormatValue(firstSender)} e: {FormatValue(eventArgs)}",
                $"SomethingHappened raised {FormatValue("SomethingHappened")} sender: {FormatValue(secondSender)} e: {FormatValue(eventArgs)}",
            };
            entries.ShouldBe(expectedEntries);
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenHandlerVerificationFails_WhenVerificationFails_ShouldListEachHandlerInvocation()
        {
            var sut = PerformedInvocationSutFactory.CreateEventSut();
            EventHandler handler = (s, e) => { };
            EventHandler other = (s, e) => { };
            sut.Instance().SomethingHappened += handler;
            sut.Instance().SomethingHappened += other;

            var sender = this;
            var eventArgs = EventArgs.Empty;
            sut.SomethingHappened.Raise(sender, eventArgs);

            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(handler), expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    $"SomethingHappened handler invoked {FormatValue("SomethingHappened")} handler: {FormatValue(handler)} sender: {FormatValue(sender)} e: {FormatValue(eventArgs)}",
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public void GivenSubscriptionVerificationFails_WhenVerificationFails_ShouldIncludeAllSubscriptionActions()
        {
            var sut = PerformedInvocationSutFactory.CreateEventSut();
            EventHandler handler = (s, e) => { };
            sut.Instance().SomethingHappened += handler;

            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.SomethingHappened.Subscribed(Arg<EventHandler>.Is(handler), expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    $"SomethingHappened subscribed {FormatValue("SomethingHappened")} handler: {FormatValue(handler)}",
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public void GivenUnsubscriptionVerificationFails_WhenVerificationFails_ShouldIncludeAllActions()
        {
            var sut = PerformedInvocationSutFactory.CreateEventSut();
            EventHandler handler = (s, e) => { };
            sut.Instance().SomethingHappened += handler;
            sut.Instance().SomethingHappened -= handler;

            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(handler), expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    $"SomethingHappened unsubscribed {FormatValue("SomethingHappened")} handler: {FormatValue(handler)}",
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public async Task GivenAsyncEventRaisedTooFewTimes_WhenVerificationFails_ShouldListEachAsyncRaise()
        {
            var sut = PerformedInvocationSutFactory.CreateEventSut();
            var sender = this;
            var eventArgs = EventArgs.Empty;
            await sut.AsyncSomethingHappened.RaiseAsync(sender, eventArgs);

            var expectedCount = Count.AtLeast(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.AsyncSomethingHappened.Raised(
                    Arg<object>.Any(),
                    Arg<EventArgs>.Any(),
                    expectedCount
                )
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    $"AsyncSomethingHappened raised {FormatValue("AsyncSomethingHappened")} arg1: {FormatValue(sender)} arg2: {FormatValue(eventArgs)}",
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }
    }
}
