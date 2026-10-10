using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(IRefOutEventSut))]
[assembly: GenerateImposter(typeof(RefEventClass))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public delegate void RefHandler(ref int value);

    public delegate void OutHandler(out int value);

    public delegate Task RefTaskHandler(ref int value);

    public interface IRefOutEventSut
    {
        event RefHandler Changed;

        event OutHandler Requested;

        event RefTaskHandler ChangedAsync;
    }

    public class RefEventClass
    {
        public virtual event RefHandler? Changed;

        public int Change(int value)
        {
            Changed?.Invoke(ref value);
            return value;
        }
    }
}
