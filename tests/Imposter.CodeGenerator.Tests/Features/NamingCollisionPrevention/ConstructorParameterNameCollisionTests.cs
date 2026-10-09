using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter's constructors and the C# 14 Imposter() extension take the target constructor's parameters next to
// the parameters they add.
public class ConstructorParameterNameCollisionTests
{
    [Fact]
    public async Task GivenConstructorParameterNamedInvocationBehavior_WhenImposterIsCreated_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public Service(int invocationBehavior) { }",
            "new Sample.ServiceImposter(1, ImposterMode.Explicit).Instance().Get();",
            LanguageVersion.CSharp9
        );
    }

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task GivenConstructorParameterNamedInvocationBehavior_WhenImposterIsCreatedFromTheExtension_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public Service(int invocationBehavior) { }",
            "Sample.Service.Imposter(1, ImposterMode.Explicit).Instance().Get();",
            LanguageVersion.Preview
        );
    }

    [Fact]
    public async Task GivenConstructorParameterNamedLikeTheExtensionParameter_WhenImposterIsCreatedFromTheExtension_ShouldCompile()
    {
        await AssertServiceCompiles(
            "public Service(int imposter) { }",
            "Sample.Service.Imposter(1).Instance().Get();",
            LanguageVersion.Preview
        );
    }
#endif

    private static Task AssertServiceCompiles(
        string constructorDeclaration,
        string usage,
        LanguageVersion languageVersion
    ) =>
        AssertCompiles(
            "Sample.Service",
            $"public class Service {{ {constructorDeclaration} public virtual int Get() => 0; }}",
            usage,
            nameof(ConstructorParameterNameCollisionTests),
            languageVersion
        );
}
