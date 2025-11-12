using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.EventImposterPoc
{
    public class EventImposterPocTests
    {
        [Fact]
        public void Handler_is_invoked_and_history_is_tracked()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();
            var hits = 0;
            object? lastSender = null;

            EventHandler handler = (sender, args) =>
            {
                hits++;
                lastSender = sender;
            };

            instance.SomethingHappened += handler;

            poc.SomethingHappened.Subscribed(handler, Count.Once());

            poc.SomethingHappened
                .Raise(instance, EventArgs.Empty)
                .Raised(Arg<object?>.Is(instance), Arg<EventArgs>.Any(), Count.Once())
                .HandlerInvoked(handler, Count.Once());

            hits.ShouldBe(1);
            lastSender.ShouldBe(instance);

        }

        [Fact]
        public void Callbacks_run_before_handlers_and_chain_is_respected()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();
            var order = new List<string>();

            poc.SomethingHappened.Callback((sender, args) => order.Add("callback"));
            instance.SomethingHappened += (sender, args) => order.Add("handler");

            poc.SomethingHappened.Raise(instance, EventArgs.Empty);

            order.ShouldBe(new[] { "callback", "handler" });
        }

        [Fact]
        public void Raises_can_be_verified_with_sender_and_args()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();
            object? seen = null;

            instance.SomethingHappened += (sender, _) => seen = sender;

            var args = EventArgs.Empty;
            poc.SomethingHappened
                .Raise(instance, args)
                .Raised(Arg<object?>.Is(instance), Arg<EventArgs>.Is(args), Count.Once());

            seen.ShouldBe(instance);
        }

        [Fact]
        public void Raising_without_handlers_is_allowed_even_in_explicit_mode()
        {
            var poc = new EventImposterPoc(ImposterMode.Explicit);

            Should.NotThrow(() => poc.SomethingHappened.Raise(null, EventArgs.Empty));
        }

        [Fact]
        public void Subscribed_and_Unsubscribed_can_be_verified()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();

            EventHandler handlerA = (sender, args) => { };
            EventHandler handlerB = (sender, args) => { };

            instance.SomethingHappened += handlerA;
            instance.SomethingHappened += handlerB;
            instance.SomethingHappened -= handlerA;

            poc.SomethingHappened
                .Subscribed(handlerA, Count.Once())
                .Subscribed(handlerB, Count.Once())
                .Unsubscribed(handlerA, Count.Once());

            Should.Throw<VerificationFailedException>(() =>
                poc.SomethingHappened.Unsubscribed(handlerB, Count.Once()));
        }

        [Fact]
        public void OnSubscribe_and_OnUnsubscribe_intercept_handlers_in_real_time()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();

            EventHandler? subscribedHandler = null;
            EventHandler? unsubscribedHandler = null;

            poc.SomethingHappened
                .OnSubscribe(handler => subscribedHandler = handler)
                .OnUnsubscribe(handler => unsubscribedHandler = handler);

            EventHandler handlerRef = (sender, args) => { };

            instance.SomethingHappened += handlerRef;
            subscribedHandler.ShouldBe(handlerRef);

            instance.SomethingHappened -= handlerRef;
            unsubscribedHandler.ShouldBe(handlerRef);
        }

        [Fact]
        public void HandlerInvoked_verification_counts_specific_handler()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();

            EventHandler handlerA = (sender, args) => { };
            EventHandler handlerB = (sender, args) => { };

            instance.SomethingHappened += handlerA;
            instance.SomethingHappened += handlerB;

            poc.SomethingHappened.Raise(instance, EventArgs.Empty);
            poc.SomethingHappened.Raise(instance, EventArgs.Empty);

            poc.SomethingHappened
                .HandlerInvoked(handlerA, Count.Exactly(2))
                .HandlerInvoked(handlerB, Count.Exactly(2));
        }

        [Fact]
        public async Task Async_handlers_are_awaited()
        {
            var poc = new EventImposterPoc();
            var instance = poc.Instance();
            var observed = false;

            Func<object?, EventArgs, Task> handler = async (sender, args) =>
            {
                await Task.Delay(10);
                observed = true;
            };

            instance.AsyncSomethingHappened += handler;

            await poc.AsyncSomethingHappened.RaiseAsync(instance, EventArgs.Empty);

            observed.ShouldBeTrue();
            poc.AsyncSomethingHappened.HandlerInvoked(handler, Count.Once());
        }
    }
}
