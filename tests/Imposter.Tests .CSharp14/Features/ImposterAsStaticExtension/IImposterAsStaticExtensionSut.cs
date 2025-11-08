using Imposter.Abstractions;

namespace Imposter.Tests.CSharp14.Features.ImposterAsStaticExtension
{
    [GenerateImposter(typeof(IImposterAsStaticExtensionSut))]
    public interface IImposterAsStaticExtensionSut
    {
        int Add(int a, int b);
    }
}