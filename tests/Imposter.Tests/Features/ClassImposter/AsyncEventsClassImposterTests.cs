using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Imposter.Tests.Features.EventImposter;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class AsyncEventsClassImposterTests
    {
        [Fact]
        public async Task GivenTaskBasedEvent_WhenRaisedAsync_ShouldAwaitHandlersAndRecordRaise()
        {
            var imposter = new ClassWithAsyncEventsImposter();
            var instance = imposter.Instance();

            var handlerInvoked = false;
            Func<object?, EventArgs, Task> handler = async (sender, args) =>
            {
                await Task.Delay(1);
                handlerInvoked = true;
            };

            instance.SubscribeToTaskEvent(handler);

            await imposter.TaskBasedEvent.RaiseAsync(instance, EventArgs.Empty);

            handlerInvoked.ShouldBeTrue();
            imposter.TaskBasedEvent.Raised(Arg<object>.Is(instance), Arg<EventArgs>.Any(), Count.Once());
            imposter.TaskBasedEvent.HandlerInvoked(Arg<Func<object, EventArgs, Task>>.Is(h => h == handler), Count.Once());
        }

        [Fact]
        public async Task GivenValueTaskBasedEvent_WhenRaisedAsync_ShouldAwaitHandlersAndSupportVerification()
        {
            var imposter = new ClassWithAsyncEventsImposter();
            var instance = imposter.Instance();

            var handlerInvocationCount = 0;
            Func<object?, EventArgs, ValueTask> handler = async (sender, args) =>
            {
                handlerInvocationCount++;
                await Task.Yield();
            };

            instance.SubscribeToValueTaskEvent(handler);

            await imposter.ValueTaskBasedEvent.RaiseAsync(instance, EventArgs.Empty);

            handlerInvocationCount.ShouldBe(1);
            imposter.ValueTaskBasedEvent.Raised(Arg<object>.Is(instance), Arg<EventArgs>.Any(), Count.Once());
            imposter.ValueTaskBasedEvent.HandlerInvoked(Arg<Func<object, EventArgs, ValueTask>>.Is(h => h == handler), Count.Once());
        }

        [Fact]
        public async Task GivenCustomAsyncDelegateEvent_WhenRaisedAsync_ShouldAwaitHandlersAndTrackInvocations()
        {
            var imposter = new ClassWithAsyncEventsImposter();
            var instance = imposter.Instance();

            var callbackCount = 0;
            AsyncEventHandler<EventArgs> handler = async (sender, args) =>
            {
                await Task.Delay(1);
                callbackCount++;
            };

            instance.SubscribeToCustomAsyncEvent(handler);

            await imposter.CustomAsyncEvent.RaiseAsync(instance, EventArgs.Empty);

            callbackCount.ShouldBe(1);
            imposter.CustomAsyncEvent.Raised(Arg<object>.Is(instance), Arg<EventArgs>.Any(), Count.Once());
            imposter.CustomAsyncEvent.HandlerInvoked(Arg<AsyncEventHandler<EventArgs>>.Is(h => h == handler), Count.Once());
        }
    }
}
