using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

// The method's arguments and argument-criteria classes keep each parameter in a field of the same name.
public class MethodArgumentsNameCollisionTests
{
    [Fact]
    public async Task GivenParametersNamedLikeTheArgumentsClasses_WhenMethodIsInvokedAndVerified_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int GetArguments, int GetArgumentsCriteria); }",
            "imposter.Get(Arg<int>.Any(), Arg<int>.Is(2)).Returns(1); imposter.Instance().Get(1, 2); imposter.Get(Arg<int>.Any(), Arg<int>.Is(2)).Called(Count.Once());",
            nameof(MethodArgumentsNameCollisionTests)
        );
    }
}
