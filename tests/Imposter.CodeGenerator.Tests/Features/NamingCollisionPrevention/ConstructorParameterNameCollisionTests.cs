using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// The imposter's constructors and the C# 14 Imposter() extension take the target constructor's parameters next to
// the parameters they add.
public class ConstructorParameterNameCollisionTests
{
    [Fact]
    public async Task GivenConstructorParameterNamedInvocationBehavior_WhenImposterIsCreated_ShouldCompile()
    {
        await AssertCompiles(
            "public Service(int invocationBehavior) { }",
            "new Sample.ServiceImposter(1, ImposterMode.Explicit).Instance().Get();",
            LanguageVersion.CSharp9
        );
    }

#if ROSLYN4_14_OR_GREATER
    [Fact]
    public async Task GivenConstructorParameterNamedInvocationBehavior_WhenImposterIsCreatedFromTheExtension_ShouldCompile()
    {
        await AssertCompiles(
            "public Service(int invocationBehavior) { }",
            "Sample.Service.Imposter(1, ImposterMode.Explicit).Instance().Get();",
            LanguageVersion.Preview
        );
    }

    [Fact]
    public async Task GivenConstructorParameterNamedLikeTheExtensionParameter_WhenImposterIsCreatedFromTheExtension_ShouldCompile()
    {
        await AssertCompiles(
            "public Service(int imposter) { }",
            "Sample.Service.Imposter(1).Instance().Get();",
            LanguageVersion.Preview
        );
    }
#endif

    private static async Task AssertCompiles(
        string constructorDeclaration,
        string usage,
        LanguageVersion languageVersion
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public class Service
                {
                    {{constructorDeclaration}}

                    public virtual int Get() => 0;
                }
            }
            """,
            baseSourceFileName: "ConstructorParameterNameCollision.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(ConstructorParameterNameCollisionTests),
            languageVersion
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                /*lang=csharp*/
                $$"""
                using Imposter.Abstractions;
                using Sample;

                public static class Usage
                {
                    public static void Run()
                    {
                        {{usage}}
                    }
                }
                """
            )
        );
    }
}
