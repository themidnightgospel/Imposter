using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public class OpenGenericEventImposterTests
    {
        public static IEnumerable<object[]> EventImposterFactories()
        {
#if USE_CSHARP14
            yield return new object[]
            {
                (Func<IHaveEventsImposter<string>>)(() => IHaveEvents<string>.Imposter()),
            };
#endif
            yield return new object[]
            {
                (Func<IHaveEventsImposter<string>>)(() => new IHaveEventsImposter<string>()),
            };
        }

        [Theory]
        [MemberData(nameof(EventImposterFactories))]
        public void GivenGenericEvent_WhenRaised_ShouldDeliverPayload(
            Func<IHaveEventsImposter<string>> factory
        )
        {
            var sut = factory();
            var received = new List<string>();
            sut.Instance().PayloadAvailable += (sender, args) => received.Add(args.Payload);

            sut.PayloadAvailable.Raise(this, new GenericEventArgs<string>("alpha"));

            received.ShouldBe(new[] { "alpha" });
            Should.NotThrow(() =>
                sut.PayloadAvailable.Raised(
                    Arg<object>.Any(),
                    Arg<GenericEventArgs<string>>.Is(args => args.Payload == "alpha"),
                    Count.Once()
                )
            );
        }

        [Fact]
        public void GivenNonGenericEvent_WhenRaised_ShouldSurfaceGenericPayload()
        {
            var sut = CreateEventImposter<string>();
            var received = new List<string>();
            sut.Instance().LegacyPayloadAvailable += (sender, args) =>
            {
                if (args is GenericEventArgs<string> payload)
                {
                    received.Add(payload.Payload);
                }
            };

            sut.LegacyPayloadAvailable.Raise(this, new GenericEventArgs<string>("beta"));

            received.ShouldBe(new[] { "beta" });
        }

        [Fact]
        public void GivenMultipleHandlers_WhenRaised_ShouldInvokeAllAndTrackCounts()
        {
            var sut = CreateEventImposter<string>();
            var first = 0;
            var second = 0;

            EventHandler<GenericEventArgs<string>> handlerA = (sender, args) => first++;
            EventHandler<GenericEventArgs<string>> handlerB = (sender, args) => second++;

            var instance = sut.Instance();
            instance.PayloadAvailable += handlerA;
            instance.PayloadAvailable += handlerB;

            sut.PayloadAvailable.Raise(this, new GenericEventArgs<string>("first"));

            instance.PayloadAvailable -= handlerB;
            sut.PayloadAvailable.Raise(this, new GenericEventArgs<string>("second"));

            first.ShouldBe(2);
            second.ShouldBe(1);
        }

        [Theory]
        [MemberData(nameof(EventImposterFactories))]
        public void GivenEventVerification_WhenPayloadPredicateDoesNotMatch_ShouldThrow(
            Func<IHaveEventsImposter<string>> factory
        )
        {
            var sut = factory();
            sut.PayloadAvailable.Raise(this, new GenericEventArgs<string>("alpha"));

            Should.Throw<VerificationFailedException>(() =>
                sut.PayloadAvailable.Raised(
                    Arg<object>.Any(),
                    Arg<GenericEventArgs<string>>.Is(args => args.Payload == "beta"),
                    Count.Once()
                )
            );
        }

        [Fact]
        public void GivenDifferentClosedGenericEvents_WhenRaised_ShouldTrackIndependently()
        {
            var stringEvents = CreateEventImposter<string>();
            var customEvents = CreateEventImposter<EventPayload>();

            stringEvents.PayloadAvailable.Raise(this, new GenericEventArgs<string>("alpha"));
            customEvents.PayloadAvailable.Raise(
                this,
                new GenericEventArgs<EventPayload>(new EventPayload("custom"))
            );

            Should.NotThrow(() =>
                stringEvents.PayloadAvailable.Raised(
                    Arg<object>.Any(),
                    Arg<GenericEventArgs<string>>.Is(args => args.Payload == "alpha"),
                    Count.Once()
                )
            );
            Should.NotThrow(() =>
                customEvents.PayloadAvailable.Raised(
                    Arg<object>.Any(),
                    Arg<GenericEventArgs<EventPayload>>.Is(args => args.Payload.Value == "custom"),
                    Count.Once()
                )
            );
            Should.Throw<VerificationFailedException>(() =>
                stringEvents.PayloadAvailable.Raised(
                    Arg<object>.Any(),
                    Arg<GenericEventArgs<string>>.Is(args => args.Payload == "missing"),
                    Count.Once()
                )
            );
        }

        [Fact]
        public void GivenDiagnosticEvent_WhenRaised_ShouldSupportVerificationCounts()
        {
            var sut = CreateEventImposter<string>();

            sut.DiagnosticPayloadPublished.Raise(this, new GenericEventArgs<string>("one"));
            sut.DiagnosticPayloadPublished.Raise(this, new GenericEventArgs<string>("two"));

            Should.NotThrow(() =>
                sut.DiagnosticPayloadPublished.Raised(
                    Arg<object>.Any(),
                    Arg<GenericEventArgs<string>>.Any(),
                    Count.Exactly(2)
                )
            );
        }

        private static IHaveEventsImposter<T> CreateEventImposter<T>()
            where T : class =>
#if USE_CSHARP14
            IHaveEvents<T>.Imposter();
#else
            new IHaveEventsImposter<T>();
#endif

        private sealed class EventPayload
        {
            public EventPayload(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }
    }
}
