using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// An imposter can't keep or match a custom ref struct argument, so setups, verification and the invocation history
// leave it out, and the delegates passed to Returns, Callback and Throws receive it.
public class RefStructParameterCompilationTests
{
    private const string Token = "public ref struct Token { public int Value; } ";

    [Fact]
    public async Task GivenMethodWithRefStructParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { int Get(int id, Token token); }",
            "imposter.Get(Arg<int>.Is(1)).Returns((id, token) => token.Value).Callback((id, token) => { }).Then().Throws((id, token) => new System.Exception()); imposter.Instance().Get(1, new Sample.Token { Value = 2 }); imposter.Get(Arg<int>.Any()).Called(Count.Once());",
            nameof(RefStructParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodWhoseOnlyParameterIsARefStruct_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { void Write(Token token); }",
            "imposter.Write().Callback(token => { }); imposter.Instance().Write(new Sample.Token()); imposter.Write().Called(Count.Once());",
            nameof(RefStructParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodWithRefStructParametersOfEveryRefKind_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { int Read(in Token a, ref Token b, out Token c); }",
            "imposter.Read().Returns((in Sample.Token a, ref Sample.Token b, out Sample.Token c) => { c = a; return b.Value; }); var b = new Sample.Token(); imposter.Instance().Read(new Sample.Token(), ref b, out var c);",
            nameof(RefStructParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenAsyncMethodWithRefStructParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token
                + "public interface IService { System.Threading.Tasks.Task<int> GetAsync(Token token); }",
            "imposter.GetAsync().ReturnsAsync(1); imposter.Instance().GetAsync(new Sample.Token()).GetAwaiter().GetResult();",
            nameof(RefStructParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithRefStructParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { int Get<T>(T value, Token token); }",
            "imposter.Get<int>(Arg<int>.Any()).Returns((value, token) => token.Value); imposter.Instance().Get(1, new Sample.Token());",
            nameof(RefStructParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassMethodWithRefStructParameter_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            Token + "public class Service { public virtual int Get(Token token) => token.Value; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get().UseBaseImplementation(); imposter.Instance().Get(new Sample.Token { Value = 1 });",
            nameof(RefStructParameterCompilationTests)
        );
    }

    // Leaving the ref struct out makes these overloads' setups alike, so they get numbered names as ref kind overloads do.
    [Fact]
    public async Task GivenOverloadsThatDifferOnlyInRefStructParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token
                + "public ref struct Other { } public interface IService { int Get(int id); int Get(int id, Token token); int Get(int id, Other other); }",
            "imposter.Get(Arg<int>.Any()).Returns(1); imposter.Get_1(Arg<int>.Any()).Returns(2); imposter.Get_2(Arg<int>.Any()).Returns(3); imposter.Instance().Get(1, new Sample.Token());",
            nameof(RefStructParameterCompilationTests)
        );
    }

    // Methods of different interfaces whose setups would collide on the imposter get numbered setups there, and keep
    // their names in their own interfaces' views.
    [Fact]
    public async Task GivenOverloadsThatDifferOnlyInRefStructParametersDeclaredByDifferentInterfaces_WhenSetUpThroughTheirViews_ShouldKeepTheirNames()
    {
        await AssertCompiles(
            "Sample.IDerived",
            Token
                + "public interface IBase { int Get(int id); } public interface IDerived : IBase { int Get(int id, Token token); }",
            "var imposter = new Sample.IDerivedImposter(); imposter.For(default(Sample.IBase)).Get(Arg<int>.Any()).Returns(1); imposter.For(default(Sample.IDerived)).Get(Arg<int>.Any()).Returns(2); imposter.Instance().Get(1, new Sample.Token());",
            nameof(RefStructParameterCompilationTests)
        );
    }

    [Fact]
    public async Task GivenOverloadsThatDifferOnlyInRefStructParameters_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            Token + "public interface IService { int Get(int id); int Get(int id, Token token); }",
            "var view = imposter.For(default(Sample.IService)); view.Get(Arg<int>.Any()).Returns(1); view.Get_1(Arg<int>.Any()).Returns(2);",
            nameof(RefStructParameterCompilationTests)
        );
    }
}
