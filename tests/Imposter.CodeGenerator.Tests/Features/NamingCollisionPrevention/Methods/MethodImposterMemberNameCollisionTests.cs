using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

// The method imposter and its invocation imposters take the method's parameters and refer to their own members by name.
public class MethodImposterMemberNameCollisionTests
{
    [Fact]
    public async Task GivenParameterNamedLikeTheResultGeneratorField_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int _resultGenerator); }",
            "imposter.Get(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheCallbacksField_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int _callbacks); }",
            "imposter.Get(Arg<int>.Any()).Callback(_callbacks => { }); imposter.Instance().Get(1);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheInvocationBehaviorField_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int _invocationBehavior); }",
            "imposter.Get(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParameterNamedLikeTheUseBaseImplementationField_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int Get(int _useBaseImplementation) => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get(Arg<int>.Any()).UseBaseImplementation(); imposter.Instance().Get(1);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodParametersNamedLikeTheImposterFields_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get<T>(T _resultGenerator, int _callbacks, int _invocationBehavior); }",
            "imposter.Get<int>(Arg<int>.Any(), Arg<int>.Any(), Arg<int>.Any()).Returns(1).Callback((a, b, c) => { }); imposter.Instance().Get(1, 2, 3);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenTypeParameterNamedLikeTheInvocationBehaviorField_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get<_invocationBehavior>(_invocationBehavior value); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParametersNamedLikeTheImposterMethods_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int DefaultResultGenerator, int FindMatchingInvocationImposterGroup, int InitializeOutParametersWithDefaultValues, out int result); }",
            "imposter.Get(Arg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), OutArg<int>.Any()).Returns(1); imposter.Instance().Get(1, 2, 3, out var result);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenParametersNamedLikeTheInvocationImposterGroupMembers_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get(int GetInvocationImposter, int MethodInvocationImposter); }",
            "imposter.Get(Arg<int>.Any(), Arg<int>.Any()).Returns(1); imposter.Instance().Get(1, 2);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenTypeParameterNamedLikeTheInvocationImposterType_WhenMethodIsInvoked_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Get<MethodInvocationImposter>(MethodInvocationImposter value); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns(1); imposter.Instance().Get(1);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenVoidMethodParametersNamedLikeTheImposterMethods_WhenMethodIsInvokedWithoutSetup_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Run(int DefaultResultGenerator, int FindMatchingInvocationImposterGroup); }",
            "imposter.Instance().Run(1, 2);",
            nameof(MethodImposterMemberNameCollisionTests)
        );
    }
}
