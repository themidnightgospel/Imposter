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

    [Fact]
    public async Task GivenGenericMethodParameterNamedLikeTheArgumentsAsMethod_WhenMethodIsInvokedAndVerified_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { T Get<T>(T As, int key); }",
            "imposter.Get<int>(Arg<int>.Any(), Arg<int>.Is(2)).Returns(1); imposter.Instance().Get(1, 2); imposter.Get<int>(Arg<int>.Any(), Arg<int>.Is(2)).Called(Count.Once());",
            nameof(MethodArgumentsNameCollisionTests)
        );
    }

    // The criteria's As method converts each matcher with a lambda, whose parameter can't hide the criteria's field.
    [Fact]
    public async Task GivenGenericMethodParameterNamedIt_WhenMethodIsInvokedAndVerified_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void M<T>(T it); }",
            "imposter.M<int>(Arg<int>.Is(1)).Callback(it => { }); imposter.Instance().M(1); imposter.M<int>(Arg<int>.Is(1)).Called(Count.Once());",
            nameof(MethodArgumentsNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithParameterNamedItAfterAnother_WhenMethodIsInvokedAndVerified_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int M<T>(int x, T it); }",
            "imposter.M<string>(Arg<int>.Any(), Arg<string>.Any()).Returns(1); imposter.Instance().M(1, \"a\"); imposter.M<string>(Arg<int>.Is(1), Arg<string>.Any()).Called(Count.Once());",
            nameof(MethodArgumentsNameCollisionTests)
        );
    }
}
