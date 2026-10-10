using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// An imposter can't keep a custom ref struct result, so only a Returns delegate or the base implementation produces it,
// there's no Returns(value), and the invocation history leaves it out. Without a setup the method returns the default.
public class RefStructReturnCompilationTests
{
    private const string Token = "public ref struct Token { public int Value; } ";

    [Fact]
    public async Task GivenMethodReturningRefStruct_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { Token Make(int id); }",
            "imposter.Make(Arg<int>.Is(1)).Returns(id => new Sample.Token { Value = id }).Callback(id => { }).Then().Throws<System.InvalidOperationException>(); var token = imposter.Instance().Make(1); imposter.Make(Arg<int>.Any()).Called(Count.Once());",
            nameof(RefStructReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodTakingAndReturningRefStructs_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { Token Next(Token current); }",
            "imposter.Next().Returns(current => new Sample.Token { Value = current.Value + 1 }); var next = imposter.Instance().Next(new Sample.Token());",
            nameof(RefStructReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodReturningRefStruct_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { Token Make<T>(T value); }",
            "imposter.Make<int>(Arg<int>.Any()).Returns(value => new Sample.Token { Value = value }); var token = imposter.Instance().Make(1);",
            nameof(RefStructReturnCompilationTests)
        );
    }

    // The generic method's adapter forwards out arguments and ref struct arguments without locals the result could
    // refer to.
    [Fact]
    public async Task GivenGenericMethodReturningRefStructWithOutAndRefStructParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token
                + "public interface IService { Token Make<T>(T value, out int count, ref Token seed); }",
            "imposter.Make<int>(Arg<int>.Any(), OutArg<int>.Any()).Returns((int value, out int count, ref Sample.Token seed) => { count = 1; return seed; }); var seed = new Sample.Token(); imposter.Instance().Make(1, out var count, ref seed);",
            nameof(RefStructReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassMethodReturningRefStruct_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Token
                + "public class Service { public virtual Token Make() => new Token { Value = 1 }; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Make().UseBaseImplementation(); var token = imposter.Instance().Make();",
            nameof(RefStructReturnCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodReturningRefStruct_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { Token Make(int id); }",
            "var view = imposter.For(default(Sample.IService)); view.Make(Arg<int>.Any()).Returns(id => new Sample.Token());",
            nameof(RefStructReturnCompilationTests)
        );
    }
}
