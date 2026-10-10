using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// Methods of two interfaces collide on the instance when their parameter types are the same at runtime, whatever the
// interfaces call them: dynamic is object.
public class RuntimeSignatureCollisionCompilationTests
{
    [Fact]
    public async Task GivenMethodsOfTwoInterfacesThatDifferOnlyInObjectAndDynamic_WhenSetUpThroughTheirViews_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IBoth",
            "public interface IFirst { void Use(object value); } public interface ISecond { void Use(dynamic value); } public interface IBoth : IFirst, ISecond { }",
            "var imposter = new Sample.IBothImposter(); imposter.For(default(Sample.IFirst)).Use(Arg<object>.Any()).Callback(value => { }); imposter.For(default(Sample.ISecond)).Use(Arg<dynamic>.Any()).Callback(value => { }); ((Sample.IFirst)imposter.Instance()).Use(1); ((Sample.ISecond)imposter.Instance()).Use(1);",
            nameof(RuntimeSignatureCollisionCompilationTests)
        );
    }

    [Fact]
    public async Task GivenMethodsOfTwoInterfacesThatDifferOnlyInADynamicTypeArgument_WhenSetUpThroughTheirViews_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IBoth",
            "public interface IFirst { void Use(System.Collections.Generic.List<object> values); } public interface ISecond { void Use(System.Collections.Generic.List<dynamic> values); } public interface IBoth : IFirst, ISecond { }",
            "var imposter = new Sample.IBothImposter(); imposter.For(default(Sample.IFirst)).Use(Arg<System.Collections.Generic.List<object>>.Any()).Callback(values => { }); imposter.For(default(Sample.ISecond)).Use(Arg<System.Collections.Generic.List<dynamic>>.Any()).Callback(values => { }); ((Sample.IFirst)imposter.Instance()).Use(new System.Collections.Generic.List<object>());",
            nameof(RuntimeSignatureCollisionCompilationTests)
        );
    }

    // A derived interface's method that hides an inherited one under dynamic declares itself new in the setup view.
    [Fact]
    public async Task GivenMethodHidingAnInheritedOneUnderDynamic_WhenSetUpThroughTheViews_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IBase { void Use(object value); } public interface IService : IBase { new void Use(dynamic value); }",
            "imposter.For(default(Sample.IBase)).Use(Arg<object>.Any()).Callback(value => { }); imposter.For(default(Sample.IService)).Use(Arg<dynamic>.Any()).Callback(value => { }); ((Sample.IBase)imposter.Instance()).Use(1);",
            nameof(RuntimeSignatureCollisionCompilationTests)
        );
    }
}
