using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

// The event builder's raise methods take the delegate's parameters and refer to the builder's own members by name.
public class EventBuilderMemberNameCollisionTests
{
    [Fact]
    public async Task GivenDelegateParametersNamedLikeTheBuilderFields_WhenEventIsRaisedAndVerified_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public delegate void Handler(int _callbacks, int _history, int _handlerInvocations); public interface IService { event Handler Happened; }",
            "imposter.Instance().Happened += (_callbacks, _history, _handlerInvocations) => { }; imposter.Happened.Callback((_callbacks, _history, _handlerInvocations) => { }).Raise(1, 2, 3); imposter.Happened.Raised(Arg<int>.Is(1), Arg<int>.Is(2), Arg<int>.Is(3), Count.Once());"
        );
    }

    [Fact]
    public async Task GivenDelegateParametersNamedLikeTheBuilderMethods_WhenEventIsRaised_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public delegate void Handler(int RaiseInternal, int EnumerateActiveHandlers); public interface IService { event Handler Happened; }",
            "imposter.Instance().Happened += (RaiseInternal, EnumerateActiveHandlers) => { }; imposter.Happened.Raise(1, 2);"
        );
    }

    [Fact]
    public async Task GivenAsyncDelegateParameterNamedLikeTheBuilderMethod_WhenEventIsRaised_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public delegate System.Threading.Tasks.Task Handler(int RaiseCoreAsync, int EnumerateActiveHandlers); public interface IService { event Handler Happened; }",
            "imposter.Instance().Happened += (RaiseCoreAsync, EnumerateActiveHandlers) => System.Threading.Tasks.Task.CompletedTask; imposter.Happened.RaiseAsync(1, 2).GetAwaiter().GetResult();"
        );
    }

    private static Task AssertServiceCompiles(string targetDeclaration, string usage) =>
        AssertCompiles(
            "Sample.IService",
            targetDeclaration,
            "var imposter = new Sample.IServiceImposter(); " + usage,
            nameof(EventBuilderMemberNameCollisionTests)
        );
}
