using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// Methods compare by their type parameter count, and by their parameter types with their own type parameters taken by
// position: overloads that differ only in the count keep their setup names, and methods that differ only in what they
// name their type parameters collide.
public class GenericOverloadCompilationTests
{
    [Fact]
    public async Task GivenInterfaceOverloadsThatDifferOnlyInTypeParameterCount_WhenImposterIsUsed_ShouldKeepTheirSetupNames()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Use(); void Use<T>(); int Get(int x); T Get<T>(int x); }",
            "imposter.Use().Callback(() => { }); imposter.Use<int>().Callback(() => { }); imposter.Get(Arg<int>.Any()).Returns(1); imposter.Get<string>(Arg<int>.Any()).Returns(\"a\"); var service = imposter.Instance(); service.Use(); service.Use<int>(); _ = service.Get(1) + service.Get<string>(1);",
            nameof(GenericOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInterfaceOverloadsThatDifferOnlyInTypeParameterCount_WhenSetUpThroughTheView_ShouldKeepTheirSetupNames()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Use(); void Use<T>(); }",
            "var view = imposter.For(default(Sample.IService)); view.Use().Callback(() => { }); view.Use<int>().Callback(() => { });",
            nameof(GenericOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodsOfTwoInterfacesThatDifferOnlyInTypeParameterCount_WhenImposterIsUsed_ShouldKeepTheirSetupNames()
    {
        await AssertCompiles(
            "Sample.IBoth",
            "public interface IFirst { void Use(); } public interface ISecond { void Use<T>(); } public interface IBoth : IFirst, ISecond { }",
            "var imposter = new Sample.IBothImposter(); imposter.Use().Callback(() => { }); imposter.Use<int>().Callback(() => { }); imposter.Instance().Use(); imposter.Instance().Use<int>();",
            nameof(GenericOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodsOfTwoInterfacesThatNameTheirTypeParametersDifferently_WhenSetUpThroughTheirViews_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IBoth",
            "public interface IFirst { void Use<T>(T value); } public interface ISecond { void Use<U>(U value); } public interface IBoth : IFirst, ISecond { }",
            "var imposter = new Sample.IBothImposter(); imposter.For(default(Sample.IFirst)).Use<int>(Arg<int>.Any()).Callback(value => { }); imposter.For(default(Sample.ISecond)).Use<int>(Arg<int>.Any()).Callback(value => { }); ((Sample.IFirst)imposter.Instance()).Use(1); ((Sample.ISecond)imposter.Instance()).Use(1);",
            nameof(GenericOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodHidingAnInheritedOneUnderAnotherTypeParameterName_WhenSetUpThroughTheViews_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IBase { void Use<T>(T value); } public interface IService : IBase { new void Use<U>(U value); }",
            "imposter.For(default(Sample.IBase)).Use<int>(Arg<int>.Any()).Callback(value => { }); imposter.For(default(Sample.IService)).Use<int>(Arg<int>.Any()).Callback(value => { }); ((Sample.IBase)imposter.Instance()).Use(1); imposter.Instance().Use(1);",
            nameof(GenericOverloadCompilationTests)
        );
    }
}
