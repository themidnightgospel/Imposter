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

public class ImposterStaticTypeExtensionTests
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IOrderService))]

        namespace Sample;

        public interface IOrderService
        {
            int Sum(int left, int right);
        }
        """;
    private const string ClassSource = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.MultiCtorClass))]

        namespace Sample;

        public class MultiCtorClass
        {
            public MultiCtorClass()
            {
            }

            public MultiCtorClass(int value, string label)
            {
            }
        }
        """;

    [Fact]
    public async Task GivenPreviewLanguageVersion_WhenGeneratorRuns_ShouldEmitStaticTypeExtension()
    {
        var artifacts = await RunGeneratorAsync(
            LanguageVersion.Preview,
            Source,
            "ImposterGeneratorExtensionsTests"
        );

        const string snippet = /*lang=csharp*/
            """
            namespace Sample;

            public static class InterfaceUsage
            {
                public static void Call()
                {
                    var imposter = IOrderService.Imposter();
                }
            }
            """;

        AssertSnippetCompiles(LanguageVersion.Preview, artifacts, snippet);
    }

    [Fact]
    public async Task GivenCSharpThirteen_WhenGeneratorRuns_ShouldNotEmitStaticTypeExtension()
    {
        var artifacts = await RunGeneratorAsync(
            LanguageVersion.CSharp10,
            Source,
            "ImposterGeneratorExtensionsTests"
        );
        var generatedSource = string.Join(
            Environment.NewLine,
            artifacts.Result.GeneratedSources.Select(static source => source.SourceText.ToString())
        );

        generatedSource.ShouldNotContain("IOrderServiceImposterExtensions");

        const string snippet = /*lang=csharp*/
            """
namespace Sample;

public static class InterfaceUsage
{
    public static void Call()
    {
        var imposter = IOrderService.Imposter();
    }
}
""";

        AssertSnippetFailsWithDiagnostic(
            LanguageVersion.CSharp10,
            artifacts,
            snippet,
            WellKnownCsCompilerErrorCodes.TypeDoesNotContainDefinition
        );
    }

    [Fact]
    public async Task GivenClassWithMultipleConstructors_WhenUsingImposterExtension_ShouldExposeAllOverloads()
    {
        var artifacts = await RunGeneratorAsync(
            LanguageVersion.Preview,
            ClassSource,
            "ImposterGeneratorExtensionsTests"
        );

        const string snippet = /*lang=csharp*/
            """
            namespace Sample;

            public static class ClassUsage
            {
                public static void Call()
                {
                    var defaultCtor = MultiCtorClass.Imposter();
                    var overload = MultiCtorClass.Imposter(42, "label");
                }
            }
            """;

        AssertSnippetCompiles(LanguageVersion.Preview, artifacts, snippet);
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
