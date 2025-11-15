using System;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.ClassImposter;

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
    public void GivenStaticMembers_WhenImposterGenerated_ThenStaticsAreNotConfigurableAndBaseBehaviorRemains()
    {
        var artifacts = RunGenerator(LanguageVersion.CSharp10, ClassSource);

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

    private sealed record GeneratorArtifacts(
        GeneratorRunResult Result,
        CSharpCompilation Compilation
    );

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

    private static (
        CSharpCompilation Compilation,
        CSharpParseOptions ParseOptions
    ) InitializeCompilation(LanguageVersion languageVersion, GeneratorArtifacts artifacts)
    {
        var parseOptions = new CSharpParseOptions(languageVersion);
        var compilation = artifacts.Compilation;

        var baseDiagnostics = compilation
            .GetDiagnostics()
            .Where(static diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ToArray();

        if (baseDiagnostics.Length > 0)
        {
            baseDiagnostics.ShouldBeEmpty(FormatDiagnostics(baseDiagnostics));
        }

        return (compilation, parseOptions);
    }

    private static string FormatDiagnostics(Diagnostic[] diagnostics)
    {
        return string.Join(
            Environment.NewLine,
            diagnostics.Select(diagnostic =>
            {
                var span = diagnostic.Location.GetLineSpan();
                return $"{diagnostic.Id}: {diagnostic.GetMessage()} ({span.Path}:{span.StartLinePosition.Line + 1},{span.StartLinePosition.Character + 1})";
            })
        );
    }

    private static GeneratorArtifacts RunGenerator(LanguageVersion languageVersion, string source)
    {
        var compilation = CreateCompilation(languageVersion, source);
        var parseOptions = new CSharpParseOptions(languageVersion);
        var driver = CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .WithUpdatedParseOptions(parseOptions);

        var updatedDriver = driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out var updatedCompilation,
            out var diagnostics
        );
        diagnostics.ShouldBeEmpty();

        var runResult = updatedDriver.GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return new GeneratorArtifacts(generatorResult, (CSharpCompilation)updatedCompilation);
    }

    private static CSharpCompilation CreateCompilation(
        LanguageVersion languageVersion,
        string source
    )
    {
        var parseOptions = new CSharpParseOptions(languageVersion);

        var references = ReferenceAssemblies
            .Net.Net90.ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat(
                new[]
                {
                    MetadataReference.CreateFromFile(
                        typeof(GenerateImposterAttribute).Assembly.Location
                    ),
                }
            );

        return CSharpCompilation.Create(
            assemblyName: "ImposterStaticMembersTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, parseOptions) },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
    }
}
