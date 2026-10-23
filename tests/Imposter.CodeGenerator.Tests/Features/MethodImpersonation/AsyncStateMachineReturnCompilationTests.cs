using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// A method's result is awaitable only when it's a task: a type implementing IAsyncStateMachine isn't one, so the
// method is impersonated like any other.
public class AsyncStateMachineReturnCompilationTests
{
    private const string StateMachine =
        "public struct StateMachine : System.Runtime.CompilerServices.IAsyncStateMachine { public void MoveNext() { } public void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { } }";

    [Fact]
    public async Task GivenMethodReturningAsyncStateMachine_WhenReturningAndCallingBack_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            StateMachine + " public interface IService { StateMachine Start(int value); }",
            "imposter.Start(Arg<int>.Any()).Returns(new Sample.StateMachine()).Callback(value => { }); _ = imposter.Instance().Start(1);",
            nameof(GivenMethodReturningAsyncStateMachine_WhenReturningAndCallingBack_ShouldCompile)
        );
    }
}
