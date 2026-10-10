using Imposter.Abstractions;
using Imposter.Tests.Features.EventImpersonation;

[assembly: GenerateImposter(typeof(IRefStructEventSut))]

namespace Imposter.Tests.Features.EventImpersonation
{
    public ref struct Position
    {
        public int Offset;

        public Position(int offset) => Offset = offset;
    }

    public delegate void MovedHandler(int id, Position position);

    public delegate void AdvancedHandler(ref Position position);

    public interface IRefStructEventSut
    {
        event MovedHandler Moved;

        event AdvancedHandler Advanced;
    }
}
