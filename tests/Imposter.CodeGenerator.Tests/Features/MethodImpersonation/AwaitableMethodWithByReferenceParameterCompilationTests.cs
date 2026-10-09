using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// An awaitable method can take ref, out and in parameters, but an async lambda or method can't (CS1988), so the async
// result generators of such a method can't declare its parameters.
public class AwaitableMethodWithByReferenceParameterCompilationTests
{
    [Fact]
    public async Task GivenAwaitableMethodWithOutParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<int> GetAsync(out int count); }",
            "imposter.GetAsync(OutArg<int>.Any()).ReturnsAsync(1); imposter.Instance().GetAsync(out var count);",
            nameof(AwaitableMethodWithByReferenceParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAwaitableMethodWithRefParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task<int> GetAsync(ref int position); }",
            "imposter.GetAsync(Arg<int>.Any()).ThrowsAsync(new System.Exception()); var position = 0; imposter.Instance().GetAsync(ref position);",
            nameof(AwaitableMethodWithByReferenceParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAwaitableMethodWithInParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.ValueTask<int> GetAsync(in int key); }",
            "imposter.GetAsync(Arg<int>.Any()).ReturnsAsync(1); var key = 0; imposter.Instance().GetAsync(in key);",
            nameof(AwaitableMethodWithByReferenceParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenTaskMethodWithOutParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { System.Threading.Tasks.Task FlushAsync(out bool flushed); }",
            "imposter.FlushAsync(OutArg<bool>.Any()).ThrowsAsync(new System.Exception()); imposter.Instance().FlushAsync(out var flushed);",
            nameof(AwaitableMethodWithByReferenceParameterCompilationTests)
        );
    }
}
