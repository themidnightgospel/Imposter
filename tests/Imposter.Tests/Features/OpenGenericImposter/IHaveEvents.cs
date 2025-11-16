using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.OpenGenericImposter;

[assembly: GenerateImposter(typeof(IHaveEvents<>))]

namespace Imposter.Tests.Features.OpenGenericImposter
{
    public interface IHaveEvents<T>
        where T : class
    {
        event EventHandler<GenericEventArgs<T>> PayloadAvailable;

        event EventHandler LegacyPayloadAvailable;

        event EventHandler<GenericEventArgs<T>> DiagnosticPayloadPublished;
    }

    public sealed class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; }
    }
}
