using Imposter.Abstractions;
using Imposter.Tests.Features.MethodImpersonation;

[assembly: GenerateImposter(typeof(IRefStructParameterSut))]
[assembly: GenerateImposter(typeof(RefStructParameterClass))]
[assembly: GenerateImposter(typeof(IRefStructOverloadDerived))]

namespace Imposter.Tests.Features.MethodImpersonation
{
    public ref struct Cursor
    {
        public int Position;

        public Cursor(int position) => Position = position;
    }

    public interface IRefStructParameterSut
    {
        int Read(int id, Cursor cursor);

        void Advance(ref Cursor cursor);

        bool TryOpen(out Cursor cursor);
    }

    public class RefStructParameterClass
    {
        public virtual int Read(Cursor cursor) => cursor.Position;
    }

    public interface IRefStructOverloadBase
    {
        int Read(int id);
    }

    public interface IRefStructOverloadDerived : IRefStructOverloadBase
    {
        int Read(int id, Cursor cursor);
    }
}
