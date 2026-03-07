using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IConflictingEventSut))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public interface IHasActionChanged
    {
        event Action Changed;
    }

    public interface IHasEventHandlerChanged
    {
        event EventHandler Changed;
    }

    public interface IConflictingEventSut : IHasActionChanged, IHasEventHandlerChanged
    {
        event Action CustomEvent;
    }
}
