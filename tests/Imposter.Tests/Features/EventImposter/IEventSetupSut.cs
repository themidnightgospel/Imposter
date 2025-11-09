using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImposter;

[assembly: GenerateImposter(typeof(IEventSetupSut))]

namespace Imposter.Tests.Features.EventImposter
{
    public interface IEventSetupSut
    {
        event EventHandler SomethingHappened;

        event Func<object?, EventArgs, Task>? AsyncSomethingHappened;

        event Func<object?, EventArgs, ValueTask>? ValueTaskSomethingHappened;

        event AsyncEventHandler<EventArgs>? CustomAsyncSomethingHappened;
    }

    public delegate Task AsyncEventHandler<in TEventArgs>(object? sender, TEventArgs args) where TEventArgs : EventArgs;
}