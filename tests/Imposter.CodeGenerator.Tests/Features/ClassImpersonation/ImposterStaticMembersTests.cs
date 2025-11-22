using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;
using GeneratorArtifacts = Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared.GeneratorArtifacts;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class ImposterStaticMembersTests
{
    private const string ClassSource = /*lang=csharp*/
        """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.ClassWithStatics))]

namespace Sample;

public class ClassWithStatics
{
    public static int StaticAdd(int a, int b) => a + b;
}
""";

    [Fact]
    public async Task GivenStaticMembers_WhenImposterGenerated_ThenStaticsAreNotConfigurableAndBaseBehaviorRemains()
    {
        var artifacts = await RunGeneratorAsync(
            LanguageVersion.CSharp10,
            ClassSource,
            "ImposterStaticMembersTests"
        );

        // Trying to access a static member on the Imposter type should fail to compile.
        const string failSnippet = /*lang=csharp*/
            """
namespace Sample;

public static class UseImposterStatics
{
    public static void Call()
    {
        // Expect compile error: ClassWithStaticsImposter does not define StaticAdd
        var x = ClassWithStaticsImposter.StaticAdd(1, 2);
    }
}
""";
        AssertSnippetFailsWithDiagnostic(
            LanguageVersion.CSharp10,
            artifacts,
            failSnippet,
            WellKnownCsCompilerErrorCodes.TypeDoesNotContainDefinition
        );

        // Calling the original type's static member should compile fine (base behavior remains).
        const string okSnippet = /*lang=csharp*/
            """
namespace Sample;

public static class UseOriginalStatics
{
    public static void Call()
    {
        var x = ClassWithStatics.StaticAdd(1, 2);
    }
}
""";
        AssertSnippetCompiles(LanguageVersion.CSharp10, artifacts, okSnippet);
    }

    private static void AssertSnippetCompiles(
        LanguageVersion languageVersion,
        GeneratorArtifacts artifacts,
        string snippet
    )
    {
        var (compilation, parseOptions) = InitializeCompilation(languageVersion, artifacts);

        var snippetTree = CSharpSyntaxTree.ParseText(
            snippet,
            options: parseOptions,
            path: "Snippet.cs"
        );
        compilation = compilation.AddSyntaxTrees(snippetTree);

        var diagnostics = compilation
            .GetDiagnostics()
            .Where(static diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ToArray();

        if (diagnostics.Length > 0)
        {
            diagnostics.ShouldBeEmpty(FormatDiagnostics(diagnostics));
        }
    }

    private static void AssertSnippetFailsWithDiagnostic(
        LanguageVersion languageVersion,
        GeneratorArtifacts artifacts,
        string snippet,
        string expectedDiagnosticId
    )
    {
        var (compilation, parseOptions) = InitializeCompilation(languageVersion, artifacts);

        var snippetTree = CSharpSyntaxTree.ParseText(
            snippet,
            options: parseOptions,
            path: "Snippet.cs"
        );
        compilation = compilation.AddSyntaxTrees(snippetTree);

        var diagnostics = compilation
            .GetDiagnostics()
            .Where(static diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ToArray();

        diagnostics.ShouldNotBeEmpty("Expected snippet to produce compile errors.");
        diagnostics
            .Any(diagnostic => diagnostic.Id == expectedDiagnosticId)
            .ShouldBeTrue(
                $"Expected diagnostic {expectedDiagnosticId} but saw:{Environment.NewLine}{FormatDiagnostics(diagnostics)}"
            );
    }
}
