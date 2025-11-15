using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Helpers;

internal static class GeneratorTestHelper
{
    private static readonly Lazy<Task<MetadataReference[]>> CachedReferences = new(
        ResolveReferencesAsync
    );

    internal static async Task<GeneratorTestContext> CreateContext(
        string source,
        string baseSourceFileName,
        string snippetFileName,
        string assemblyName,
        LanguageVersion languageVersion = LanguageVersion.CSharp9
    )
    {
        return new GeneratorTestContext(
            source,
            baseSourceFileName,
            snippetFileName,
            assemblyName,
            languageVersion,
            await CachedReferences.Value.ConfigureAwait(false)
        );
    }

    internal static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics)
    {
        diagnostics.ShouldBeEmpty();
    }

    internal static Diagnostic AssertSingleDiagnostic(
        ImmutableArray<Diagnostic> diagnostics,
        string expectedId,
        int expectedLine,
        string snippetFileName = "Snippet.cs"
    )
    {
        diagnostics.Length.ShouldBe(1, FormatDiagnostics(diagnostics));
        var diagnostic = diagnostics[0];
        diagnostic.Id.ShouldBe(expectedId);

        var span = diagnostic.Location.GetLineSpan();
        if (!string.IsNullOrEmpty(snippetFileName))
        {
            (span.Path ?? string.Empty)
                .EndsWith(snippetFileName, StringComparison.Ordinal)
                .ShouldBeTrue();
        }

        span.StartLinePosition.Line.ShouldBe(expectedLine - 1);
        return diagnostic;
    }

    private static string FormatDiagnostics(ImmutableArray<Diagnostic> diagnostics)
    {
        if (diagnostics.IsDefaultOrEmpty)
        {
            return "No diagnostics reported.";
        }

        return string.Join(
            Environment.NewLine,
            diagnostics.Select(diagnostic =>
            {
                var span = diagnostic.Location.GetLineSpan();
                return $"{diagnostic.Id}: {diagnostic.GetMessage()} ({span.Path}:{span.StartLinePosition.Line + 1},{span.StartLinePosition.Character + 1})";
            })
        );
    }

    private static async Task<MetadataReference[]> ResolveReferencesAsync()
    {
        var references = await ReferenceAssemblies
            .Net.Net90.ResolveAsync(null, CancellationToken.None)
            .ConfigureAwait(false);

        return references
            .Concat([
                MetadataReference.CreateFromFile(
                    typeof(GenerateImposterAttribute).Assembly.Location
                ),
            ])
            .ToArray();
    }
}

internal sealed class GeneratorTestContext
{
    private readonly string _source;
    private readonly string _baseSourceFileName;
    private readonly string _snippetFileName;
    private readonly string _assemblyName;
    private readonly CSharpParseOptions _parseOptions;
    private readonly MetadataReference[] _references;
    private readonly Lazy<CSharpCompilation> _compilation;
    private readonly Lazy<GeneratorRunResult> _generatorResult;

    internal GeneratorTestContext(
        string source,
        string baseSourceFileName,
        string snippetFileName,
        string assemblyName,
        LanguageVersion languageVersion,
        MetadataReference[] references
    )
    {
        _source = source;
        _baseSourceFileName = baseSourceFileName;
        _snippetFileName = snippetFileName;
        _assemblyName = assemblyName;
        _parseOptions = new CSharpParseOptions(languageVersion);
        _references = references;
        _compilation = new Lazy<CSharpCompilation>(CreateCompilation);
        _generatorResult = new Lazy<GeneratorRunResult>(CreateGeneratorResult);
    }

    internal ImmutableArray<Diagnostic> CompileSnippet(string snippet)
    {
        var generatorResult = _generatorResult.Value;

        var baseTree = CSharpSyntaxTree.ParseText(
            _source,
            _parseOptions,
            path: _baseSourceFileName
        );
        var snippetTree = CSharpSyntaxTree.ParseText(
            snippet,
            _parseOptions,
            path: _snippetFileName
        );
        var generatedTrees = generatorResult.GeneratedSources.Select(gs =>
            CSharpSyntaxTree.ParseText(gs.SourceText.ToString(), _parseOptions, path: gs.HintName)
        );

        var compilation = CSharpCompilation.Create(
            assemblyName: $"{_assemblyName}.Snippet",
            syntaxTrees: new[] { baseTree, snippetTree }.Concat(generatedTrees),
            references: _references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        return
        [
            .. compilation
                .GetDiagnostics()
                .Where(static diagnostic => diagnostic.Severity == DiagnosticSeverity.Error),
        ];
    }

    internal GeneratorRunResult RunGenerator() => _generatorResult.Value;

    internal CSharpCompilation Compilation => _compilation.Value;

    private GeneratorRunResult CreateGeneratorResult()
    {
        var compilation = Compilation;

        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());
        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return generatorResult;
    }

    private CSharpCompilation CreateCompilation() =>
        CSharpCompilation.Create(
            assemblyName: _assemblyName,
            syntaxTrees:
            [
                CSharpSyntaxTree.ParseText(_source, _parseOptions, path: _baseSourceFileName),
            ],
            references: _references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
}
