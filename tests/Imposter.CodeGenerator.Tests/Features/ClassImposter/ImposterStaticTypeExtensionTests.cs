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

public class ImposterStaticTypeExtensionTests
{
    private const string Source = /*lang=csharp*/"""
                                                 using Imposter.Abstractions;

                                                 [assembly: GenerateImposter(typeof(Sample.IOrderService))]

                                                 namespace Sample;

                                                 public interface IOrderService
                                                 {
                                                     int Sum(int left, int right);
                                                 }
                                                 """;
    private const string ClassSource = /*lang=csharp*/"""
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

    private sealed record GeneratorArtifacts(GeneratorRunResult Result, CSharpCompilation Compilation);

    [Fact]
    public void GivenPreviewLanguageVersion_WhenGeneratorRuns_ShouldEmitStaticTypeExtension()
    {
        var artifacts = RunGenerator(LanguageVersion.Preview);

        const string snippet = /*lang=csharp*/"""
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
    public void GivenCSharpThirteen_WhenGeneratorRuns_ShouldNotEmitStaticTypeExtension()
    {
        var artifacts = RunGenerator(LanguageVersion.CSharp13);
        var generatedSource = string.Join(Environment.NewLine, artifacts.Result.GeneratedSources.Select(static source => source.SourceText.ToString()));

        generatedSource.ShouldNotContain("IOrderServiceImposterExtensions");

        const string snippet = /*lang=csharp*/"""
namespace Sample;

public static class InterfaceUsage
{
    public static void Call()
    {
        var imposter = IOrderService.Imposter();
    }
}
""";

        AssertSnippetFailsWithDiagnostic(LanguageVersion.CSharp13, artifacts, snippet, WellKnownCsCompilerErrorCodes.TypeDoesNotContainDefinition);
    }

    [Fact]
    public void GivenClassWithMultipleConstructors_WhenUsingImposterExtension_ShouldExposeAllOverloads()
    {
        var artifacts = RunGenerator(LanguageVersion.Preview, ClassSource);

        const string snippet = /*lang=csharp*/"""
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

    private static void AssertSnippetCompiles(LanguageVersion languageVersion, GeneratorArtifacts artifacts, string snippet)
    {
        var (compilation, parseOptions) = InitializeCompilation(languageVersion, artifacts);

        var snippetTree = CSharpSyntaxTree.ParseText(snippet, options: parseOptions, path: "Snippet.cs");
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

    private static void AssertSnippetFailsWithDiagnostic(LanguageVersion languageVersion, GeneratorArtifacts artifacts, string snippet, string expectedDiagnosticId)
    {
        var (compilation, parseOptions) = InitializeCompilation(languageVersion, artifacts);

        var snippetTree = CSharpSyntaxTree.ParseText(snippet, options: parseOptions, path: "Snippet.cs");
        compilation = compilation.AddSyntaxTrees(snippetTree);

        var diagnostics = compilation
            .GetDiagnostics()
            .Where(static diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ToArray();

        diagnostics.ShouldNotBeEmpty("Expected snippet to produce compile errors.");
        diagnostics.Any(diagnostic => diagnostic.Id == expectedDiagnosticId)
            .ShouldBeTrue($"Expected diagnostic {expectedDiagnosticId} but saw:{Environment.NewLine}{FormatDiagnostics(diagnostics)}");
    }

    private static (CSharpCompilation Compilation, CSharpParseOptions ParseOptions) InitializeCompilation(
        LanguageVersion languageVersion,
        GeneratorArtifacts artifacts)
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
            }));
    }

    private static GeneratorArtifacts RunGenerator(LanguageVersion languageVersion, string source = Source)
    {
        var compilation = CreateCompilation(languageVersion, source);
        var parseOptions = new CSharpParseOptions(languageVersion);
        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator())
            .WithUpdatedParseOptions(parseOptions);

        var updatedDriver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);
        diagnostics.ShouldBeEmpty();

        var runResult = updatedDriver.GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return new GeneratorArtifacts(generatorResult, (CSharpCompilation)updatedCompilation);
    }

    private static CSharpCompilation CreateCompilation(LanguageVersion languageVersion, string source)
    {
        var parseOptions = new CSharpParseOptions(languageVersion);

        var references = ReferenceAssemblies.Net.Net90
            .ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat(new[]
            {
                MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location)
            });

        return CSharpCompilation.Create(
            assemblyName: "ImposterGeneratorExtensionsTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, parseOptions) },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }
}
