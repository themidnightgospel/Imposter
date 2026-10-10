using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.ArgumentsMatching;

[assembly: GenerateImposter(typeof(IRefStructArgumentService))]

namespace Imposter.Tests.Features.Docs.ArgumentsMatching
{
    public ref struct Cursor
    {
        public int Position;

        public Cursor(int position) => Position = position;
    }

    public interface IRefStructArgumentService
    {
        int Read(int id, Cursor cursor);
    }
}
