using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

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
                sut.SomethingHappened.Raised(
                    Arg<object?>.Any(),
                    Arg<EventArgs>.Any(),
                    expectedCount
                )
            );

            var entries = exception.ReadEntries();
            var expectedEntries = new[]
            {
                $"SomethingHappened raised sender: <{typeof(EventPerformedInvocationExceptionMessageTests).FullName}> e: <{typeof(EventArgs).FullName}>",
                $"SomethingHappened raised sender: <{typeof(object).FullName}> e: <{typeof(EventArgs).FullName}>",
            };
            entries.ShouldBe(expectedEntries);
            exception.Expected.ShouldBe("SomethingHappened raised(sender: any, e: any)");
            exception.Message.ShouldBe(
                "Expected SomethingHappened raised(sender: any, e: any) exactly 3 time(s), performed 2 times."
                    + Environment.NewLine
                    + "Performed invocations of SomethingHappened:"
                    + Environment.NewLine
                    + string.Join(Environment.NewLine, expectedEntries)
            );
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
                    $"SomethingHappened handler invoked handler: <{typeof(EventHandler).FullName}> sender: <{typeof(EventPerformedInvocationExceptionMessageTests).FullName}> e: <{typeof(EventArgs).FullName}>",
                    $"SomethingHappened handler invoked handler: <{typeof(EventHandler).FullName}> sender: <{typeof(EventPerformedInvocationExceptionMessageTests).FullName}> e: <{typeof(EventArgs).FullName}>",
                }
            );
            exception.Expected.ShouldBe(
                "SomethingHappened handler invoked(handler: <System.EventHandler>)"
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
                new[] { $"SomethingHappened subscribed handler: <{typeof(EventHandler).FullName}>" }
            );
            exception.Expected.ShouldBe(
                "SomethingHappened subscribed(handler: <System.EventHandler>)"
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
                    $"SomethingHappened unsubscribed handler: <{typeof(EventHandler).FullName}>",
                }
            );
            exception.Expected.ShouldBe(
                "SomethingHappened unsubscribed(handler: <System.EventHandler>)"
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public void GivenFilteredRaises_WhenVerificationFails_ShouldShowHistoryWithoutReevaluatingMatcher()
        {
            var sut = PerformedInvocationSutFactory.CreateEventSut();
            sut.SomethingHappened.Raise("wanted", EventArgs.Empty);
            sut.SomethingHappened.Raise("other", EventArgs.Empty);
            var evaluations = 0;
            var criterion = Arg<object?>.Is(sender =>
            {
                evaluations++;
                return Equals(sender, "wanted");
            });
            var expectedCount = Count.Twice();
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.SomethingHappened.Raised(criterion, Arg<EventArgs>.Any(), expectedCount)
            );

            evaluations.ShouldBe(2);
            exception
                .ReadEntries()
                .ShouldBe(
                    new[]
                    {
                        $"SomethingHappened raised sender: <wanted> e: <{typeof(EventArgs).FullName}>",
                        $"SomethingHappened raised sender: <other> e: <{typeof(EventArgs).FullName}>",
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
                    Arg<object?>.Any(),
                    Arg<EventArgs>.Any(),
                    expectedCount
                )
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    $"AsyncSomethingHappened raised arg1: <{typeof(EventPerformedInvocationExceptionMessageTests).FullName}> arg2: <{typeof(EventArgs).FullName}>",
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }
    }
}
