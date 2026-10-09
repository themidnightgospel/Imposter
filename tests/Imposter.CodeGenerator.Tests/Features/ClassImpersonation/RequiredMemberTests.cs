#if ROSLYN4_4_OR_GREATER
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

// The imposter creates its instance with new and overrides virtual members, so required members need
// [SetsRequiredMembers] on the instance's constructors and the required modifier on the overrides.
public class RequiredMemberTests
{
    [Fact]
    public async Task GivenClassWithRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required string Name { get; set; } public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get().Returns(1); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenClassWithRequiredField_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required int Id; public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenClassWithVirtualRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required virtual string Name { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(\"a\"); _ = imposter.Instance().Name;"
        );
    }

    [Fact]
    public async Task GivenClassWithAbstractRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public abstract class Service { public required abstract string Name { get; set; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(\"a\"); _ = imposter.Instance().Name;"
        );
    }

    [Fact]
    public async Task GivenClassWithVirtualRequiredInitOnlyProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public required virtual string Name { get; init; } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Name.Getter().Returns(\"a\"); _ = imposter.Instance().Name;"
        );
    }

    [Fact]
    public async Task GivenClassInheritingRequiredProperty_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Entity { public required string Id { get; set; } } public class Service : Entity { public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Instance().Get();"
        );
    }

    [Fact]
    public async Task GivenClassWithRequiredPropertyAndConstructorParameters_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertRequiredTargetCompiles(
            "public class Service { public Service(int seed) { } public required string Name { get; set; } public virtual int Get() => 0; }",
            "var imposter = new Sample.ServiceImposter(1); imposter.Instance().Get();"
        );
    }

    private static Task AssertRequiredTargetCompiles(string targetDeclaration, string usage) =>
        AssertCompiles(
            "Sample.Service",
            targetDeclaration,
            usage,
            nameof(RequiredMemberTests),
            LanguageVersion.CSharp11
        );
}
#endif
