using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(IInParameterAsyncEventSut))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public delegate Task InParameterTaskHandler(in int value);

    public delegate ValueTask InParameterValueTaskHandler(in int value);

    public interface IInParameterAsyncEventSut
    {
        event InParameterTaskHandler TaskHappened;

        event InParameterValueTaskHandler ValueTaskHappened;
    }
}
