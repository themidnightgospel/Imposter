using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Imposter.Tests.Features.EventImposter;

[assembly: GenerateImposter(typeof(ClassWithAsyncEvents))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    public class ClassWithAsyncEvents
    {
#pragma warning disable CS0067
        protected virtual event Func<object?, EventArgs, Task>? TaskBasedEvent;

        protected virtual event Func<object?, EventArgs, ValueTask>? ValueTaskBasedEvent;

        protected virtual event AsyncEventHandler<EventArgs>? CustomAsyncEvent;
#pragma warning restore CS0067

        public void SubscribeToTaskEvent(Func<object?, EventArgs, Task> handler) =>
            TaskBasedEvent += handler;

        public void UnsubscribeFromTaskEvent(Func<object?, EventArgs, Task> handler) =>
            TaskBasedEvent -= handler;

        public void SubscribeToValueTaskEvent(Func<object?, EventArgs, ValueTask> handler) =>
            ValueTaskBasedEvent += handler;

        public void UnsubscribeFromValueTaskEvent(Func<object?, EventArgs, ValueTask> handler) =>
            ValueTaskBasedEvent -= handler;

        public void SubscribeToCustomAsyncEvent(AsyncEventHandler<EventArgs> handler) =>
            CustomAsyncEvent += handler;

        public void UnsubscribeFromCustomAsyncEvent(AsyncEventHandler<EventArgs> handler) =>
            CustomAsyncEvent -= handler;
    }
}
