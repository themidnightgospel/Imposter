using System;
using System.Threading.Tasks;
using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.EventImposter
{
    [GenerateImposter(typeof(IEventSetupSut))]
    public interface IEventSetupSut
    {
        event EventHandler SomethingHappened;

        event Func<object?, EventArgs, Task>? AsyncSomethingHappened;

        event Func<object?, EventArgs, ValueTask>? ValueTaskSomethingHappened;

        event AsyncEventHandler<EventArgs>? CustomAsyncSomethingHappened;
    }
    
    public delegate Task AsyncEventHandler<in TEventArgs>(object? sender, TEventArgs args) where TEventArgs : EventArgs;

}
