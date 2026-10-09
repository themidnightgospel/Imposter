using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;
#if ROSLYN4_14_OR_GREATER
using Microsoft.CodeAnalysis.CSharp;
#endif

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// A setup matches a parameter passed by value, in, ref or ref readonly with the same Arg<T>, so overloads that differ
// only in that get setups named by their unique names, on the imposter and in its setup views.
public class RefKindOverloadCompilationTests
{
    [Fact]
    public async Task GivenInterfaceOverloadsOnValueAndIn_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Count(int value); int Count(in int value); }",
            "imposter.Count(Arg<int>.Any()).Returns(1); imposter.Count_1(Arg<int>.Any()).Returns(2); var value = 0; imposter.Instance().Count(in value);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInterfaceOverloadsOnValueAndIn_WhenSetUpThroughTheView_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Count(int value); int Count(in int value); }",
            "var view = imposter.For(default(Sample.IService)); view.Count(Arg<int>.Any()).Returns(1); view.Count_1(Arg<int>.Any()).Returns(2);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenOverloadsOnValueAndInDeclaredByDifferentInterfaces_WhenSetUpThroughTheirViews_ShouldKeepTheirNames()
    {
        await AssertCompiles(
            "Sample.IDerived",
            "public interface IBase { int Count(int value); } public interface IDerived : IBase { int Count(in int value); }",
            "var imposter = new Sample.IDerivedImposter(); imposter.For(default(Sample.IBase)).Count(Arg<int>.Any()).Returns(1); imposter.For(default(Sample.IDerived)).Count(Arg<int>.Any()).Returns(2);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualOverloadsOnValueAndInDeclaredByDifferentClasses_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Derived",
            "public class Base { public virtual int Count(int value) => 0; } public class Derived : Base { public virtual int Count(in int value) => 0; }",
            "var imposter = new Sample.DerivedImposter(); imposter.Count(Arg<int>.Any()).Returns(1); imposter.Count_1(Arg<int>.Any()).Returns(2);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInterfaceOverloadsOnValueAndRef_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Fill(string text); void Fill(ref string text); }",
            "imposter.Fill(Arg<string>.Any()).Called(Count.Never()); imposter.Fill_1(Arg<string>.Any()).Called(Count.Never()); var text = \"\"; imposter.Instance().Fill(ref text);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenOverloadsPassingDifferentParametersByReference_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Add(in int left, int right); int Add(int left, in int right); }",
            "imposter.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(1); imposter.Add_1(Arg<int>.Any(), Arg<int>.Any()).Returns(2);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericOverloadsOnValueAndIn_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Count<T>(T value); int Count<TOther>(in TOther value); }",
            "imposter.Count<int>(Arg<int>.Any()).Returns(1); imposter.Count_1<int>(Arg<int>.Any()).Returns(2); imposter.For(default(Sample.IService)).Count_1<int>(Arg<int>.Any());",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenVirtualClassOverloadsOnValueAndIn_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual int Count(int value) => 0; public virtual int Count(in int value) => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Count(Arg<int>.Any()).Returns(1); imposter.Count_1(Arg<int>.Any()).UseBaseImplementation(); var value = 0; imposter.Instance().Count(in value);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

    [Fact]
    public async Task GivenInterfaceOverloadsOnValueAndOut_WhenSetUpThroughTheView_ShouldKeepTheirNames()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int Count(int value); int Count(out int value); }",
            "var view = imposter.For(default(Sample.IService)); view.Count(Arg<int>.Any()).Returns(1); view.Count(OutArg<int>.Any()).Returns(2);",
            nameof(RefKindOverloadCompilationTests)
        );
    }

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task GivenInterfaceOverloadsOnValueAndRefReadOnly_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            "public interface IService { int Count(int value); int Count(ref readonly int value); }",
            "var imposter = new Sample.IServiceImposter(); imposter.Count(Arg<int>.Any()).Returns(1); imposter.Count_1(Arg<int>.Any()).Returns(2); var value = 0; imposter.Instance().Count(in value);",
            nameof(RefKindOverloadCompilationTests),
            LanguageVersion.CSharp12
        );
    }
#endif
}
