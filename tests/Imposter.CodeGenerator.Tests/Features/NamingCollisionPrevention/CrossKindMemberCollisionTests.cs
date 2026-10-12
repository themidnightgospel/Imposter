using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// One interface can't declare a property, an event and a method of one name, but two interfaces of the target can, and
// the imposter and its instance would then declare both. They collide like members of one kind: each is implemented
// explicitly and set up by its unique name.
public class CrossKindMemberCollisionTests
{
    [Fact]
    public async Task GivenPropertyAndMethodOfOneNameFromTwoInterfaces_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IC",
            "public interface IA { int Current { get; } } public interface IB { int Current(); } public interface IC : IA, IB { }",
            "var imposter = new Sample.ICImposter(); imposter.For(default(Sample.IA)).Current.Getter().Returns(1); imposter.For(default(Sample.IB)).Current().Returns(2); _ = ((Sample.IA)imposter.Instance()).Current + ((Sample.IB)imposter.Instance()).Current();",
            nameof(CrossKindMemberCollisionTests)
        );
    }

    [Fact]
    public async Task GivenEventAndMethodOfOneNameFromTwoInterfaces_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IC",
            "public interface IA { event System.Action Changed; } public interface IB { void Changed(); } public interface IC : IA, IB { }",
            "var imposter = new Sample.ICImposter(); imposter.For(default(Sample.IB)).Changed().Callback(() => { }); ((Sample.IB)imposter.Instance()).Changed();",
            nameof(CrossKindMemberCollisionTests)
        );
    }

    [Fact]
    public async Task GivenPropertyAndEventOfOneNameFromTwoInterfaces_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IC",
            "public interface IA { int Changed { get; } } public interface IB { event System.Action Changed; } public interface IC : IA, IB { }",
            "var imposter = new Sample.ICImposter(); imposter.For(default(Sample.IA)).Changed.Getter().Returns(1); _ = ((Sample.IA)imposter.Instance()).Changed;",
            nameof(CrossKindMemberCollisionTests)
        );
    }
}
