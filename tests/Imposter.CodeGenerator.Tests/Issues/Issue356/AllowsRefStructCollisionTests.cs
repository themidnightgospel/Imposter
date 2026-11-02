#if ROSLYN4_14_OR_GREATER
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Issues.Issue356;

public class AllowsRefStructCollisionTests
{
    [Theory]
    [InlineData(false, false, "T")]
    [InlineData(false, true, "T")]
    [InlineData(true, false, "T")]
    [InlineData(true, true, "T")]
    [InlineData(false, false, "U")]
    [InlineData(false, true, "U")]
    [InlineData(true, false, "U")]
    [InlineData(true, true, "U")]
    public async Task Given_ConstraintChangesSetupSignature_When_InterfacesShareImplementationSignature_Should_Compile(
        bool hasMatcher,
        bool reverseInterfaces,
        string otherTypeParameter
    )
    {
        var extraParameter = hasMatcher ? ", int x" : "";
        var matcher = hasMatcher ? "Arg<int>.Any()" : "";
        var extraMatcher = hasMatcher ? ", Arg<int>.Any()" : "";
        var extraArgument = hasMatcher ? ", 2" : "";

        await AssertInterfaceCompiles(
            $$"""
            public ref struct Slot { }
            public interface IAllows { int M<T>(T t{{extraParameter}}) where T : allows ref struct; }
            public interface IRegular { int M<{{otherTypeParameter}}>({{otherTypeParameter}} t{{extraParameter}}); }
            public interface IService : {{(
                reverseInterfaces ? "IRegular, IAllows" : "IAllows, IRegular"
            )}} { }
            """,
            $$"""
            imposter.For(default(IAllows)).M<Slot>({{matcher}}).Returns(1);
            imposter.For(default(IAllows)).M<int>({{matcher}}).Returns(2);
            imposter.For(default(IRegular)).M<int>(Arg<int>.Any(){{extraMatcher}}).Returns(3);
            IAllows allows = imposter.Instance();
            IRegular regular = imposter.Instance();
            _ = allows.M(new Slot(){{extraArgument}});
            _ = allows.M(1{{extraArgument}});
            _ = regular.M(1{{extraArgument}});
            imposter.For(default(IAllows)).M<Slot>({{matcher}}).Called(Count.Once());
            imposter.For(default(IRegular)).M<int>(Arg<int>.Any(){{extraMatcher}}).Called(Count.Once());
            """,
            nameof(AllowsRefStructCollisionTests),
            LanguageVersion.CSharp13
        );
    }

    [Theory]
    [InlineData("in")]
    [InlineData("ref")]
    [InlineData("out")]
    public async Task Given_ByRefSignaturesDifferingInConstraint_When_SetUpThroughViews_Should_Compile(
        string refKind
    )
    {
        await AssertInterfaceCompiles(
            $$"""
            public ref struct Slot { }
            public interface IAllows { int M<T>({{refKind}} T value) where T : allows ref struct; }
            public interface IRegular { int M<U>({{refKind}} U value); }
            public interface IService : IAllows, IRegular { }
            """,
            $$"""
            imposter.For(default(IAllows)).M<Slot>().Returns(1);
            imposter.For(default(IRegular)).M<int>({{(
                refKind == "out" ? "OutArg<int>.Any()" : "Arg<int>.Any()"
            )}}).Returns(2);
            var slot = new Slot();
            var value = 1;
            _ = ((IAllows)imposter.Instance()).M<Slot>({{refKind}} slot);
            _ = ((IRegular)imposter.Instance()).M<int>({{refKind}} value);
            """,
            nameof(AllowsRefStructCollisionTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task Given_ConstraintsOnDifferentTypeParameters_When_SetUpThroughViews_Should_Compile()
    {
        await AssertInterfaceCompiles(
            """
            public ref struct Slot { }
            public interface IFirst { int M<T, U>(T first, U second) where T : allows ref struct; }
            public interface ISecond { int M<X, Y>(X first, Y second) where Y : allows ref struct; }
            public interface IService : IFirst, ISecond { }
            """,
            """
            imposter.For(default(IFirst)).M<Slot, int>(Arg<int>.Any()).Returns(1);
            imposter.For(default(ISecond)).M<int, Slot>(Arg<int>.Any()).Returns(2);
            _ = ((IFirst)imposter.Instance()).M(new Slot(), 1);
            _ = ((ISecond)imposter.Instance()).M(1, new Slot());
            """,
            nameof(AllowsRefStructCollisionTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task Given_GenericReturnWithDifferentConstraints_When_SetUpThroughViews_Should_Compile()
    {
        await AssertInterfaceCompiles(
            """
            public ref struct Slot { public int Value; }
            public interface IAllows { T Echo<T>(T value) where T : allows ref struct; }
            public interface IRegular { U Echo<U>(U value); }
            public interface IService : IAllows, IRegular { }
            """,
            """
            imposter.For(default(IAllows)).Echo<Slot>().Returns(value => new Slot { Value = value.Value + 1 });
            imposter.For(default(IRegular)).Echo<int>(Arg<int>.Any()).Returns(value => value + 2);
            _ = ((IAllows)imposter.Instance()).Echo(new Slot { Value = 1 }).Value;
            _ = ((IRegular)imposter.Instance()).Echo(1);
            """,
            nameof(AllowsRefStructCollisionTests),
            LanguageVersion.CSharp13
        );
    }

    [Fact]
    public async Task Given_DifferentValueAndRefSignatures_When_SetUpOnImposter_Should_KeepOverloadNames()
    {
        await AssertInterfaceCompiles(
            """
            public ref struct Slot { }
            public interface IAllows { int M<T>(T value) where T : allows ref struct; }
            public interface IRegular { int M<U>(ref U value); }
            public interface IService : IAllows, IRegular { }
            """,
            """
            imposter.M<Slot>().Returns(1);
            imposter.M<int>(Arg<int>.Any()).Returns(2);
            var value = 1;
            _ = ((IAllows)imposter.Instance()).M(new Slot());
            _ = ((IRegular)imposter.Instance()).M(ref value);
            """,
            nameof(AllowsRefStructCollisionTests),
            LanguageVersion.CSharp13
        );
    }
}
#endif
