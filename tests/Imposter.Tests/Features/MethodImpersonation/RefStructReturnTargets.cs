using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IRefStructReturnSut))]
[assembly: GenerateImposter(typeof(RefStructReturnClass))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public ref struct Slot
    {
        public int Index;

        public Slot(int index) => Index = index;
    }

    public interface IRefStructReturnSut
    {
        Slot Rent(int size);

        Slot Next(Slot current);
    }

    public class RefStructReturnClass
    {
        public virtual Slot Rent(int size) => new Slot(size * 10);
    }
}
