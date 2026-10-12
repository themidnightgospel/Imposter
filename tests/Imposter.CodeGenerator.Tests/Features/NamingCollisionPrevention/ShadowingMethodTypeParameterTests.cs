using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// An inherited method's signature can refer both to its own type parameter and to the target's type parameter of the
// same name, which the base type is constructed with. Generated code under one name would hide the target's, so the
// method's takes another.
public class ShadowingMethodTypeParameterTests
{
    [Fact]
    public async Task GivenInheritedInterfaceMethodWithTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IKw<>",
            "public interface IBase<T> { T M<U>(T x, U y); } public interface IKw<U> : IBase<U> { }",
            "var imposter = new Sample.IKwImposter<string>(); imposter.M<int>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"a\"); _ = imposter.Instance().M(\"b\", 1);",
            nameof(
                GivenInheritedInterfaceMethodWithTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile
            )
        );
    }

    [Fact]
    public async Task GivenInheritedClassMethodWithTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Derived<>",
            "public class Base<T> { public virtual T M<U>(T x, U y) => x; } public class Derived<U> : Base<U> { }",
            "var imposter = new Sample.DerivedImposter<string>(); imposter.M<int>(Arg<string>.Any(), Arg<int>.Any()).Returns(\"a\"); _ = imposter.Instance().M(\"b\", 1);",
            nameof(
                GivenInheritedClassMethodWithTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile
            )
        );
    }

    [Fact]
    public async Task GivenInheritedMethodTypeParameterConstrainedToTheTargetsOfItsName_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IKw<>",
            "public interface IBase<T> { U M<U>(T x) where U : T; } public interface IKw<U> : IBase<U> where U : class { }",
            "var imposter = new Sample.IKwImposter<object>(); imposter.M<string>(Arg<object>.Any()).Returns(\"a\"); _ = imposter.Instance().M<string>(\"b\");",
            nameof(
                GivenInheritedMethodTypeParameterConstrainedToTheTargetsOfItsName_WhenImposterIsUsed_ShouldCompile
            )
        );
    }

    [Fact]
    public async Task GivenMethodTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IKw<>",
            "#pragma warning disable CS0693\n public interface IKw<U> { U Get(); U M<U>(U x); }",
            "var imposter = new Sample.IKwImposter<string>(); imposter.M<int>(Arg<int>.Any()).Returns(1); _ = imposter.Instance().M(1);",
            nameof(GivenMethodTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile)
        );
    }

    [Fact]
    public async Task GivenInheritedMethodWithUnusedTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IKw<>",
            "public interface IBase<T> { void M<U>(T x); } public interface IKw<U> : IBase<U> { }",
            "var imposter = new Sample.IKwImposter<string>(); imposter.M<int>(Arg<string>.Any()); imposter.Instance().M<int>(\"b\");",
            nameof(
                GivenInheritedMethodWithUnusedTypeParameterNamedLikeTheTargets_WhenImposterIsUsed_ShouldCompile
            )
        );
    }
}
