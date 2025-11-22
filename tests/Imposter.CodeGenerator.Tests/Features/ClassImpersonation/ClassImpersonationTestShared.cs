using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public static class ClassImpersonationTestShared
{
    public sealed record GeneratorArtifacts(
        GeneratorRunResult Result,
        CSharpCompilation Compilation
    );

    public static async Task<CSharpCompilation> CreateCompilationAsync(
        LanguageVersion languageVersion,
        string source,
        string assemblyName
    )
    {
        var parseOptions = new CSharpParseOptions(languageVersion);

        var references = (
            await ReferenceAssemblies.Net.Net90.ResolveAsync(null, CancellationToken.None)
        ).Concat([
            MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location),
        ]);

        return CSharpCompilation.Create(
            assemblyName: assemblyName,
            syntaxTrees: [CSharpSyntaxTree.ParseText(source, parseOptions)],
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
    }

    public static async Task<GeneratorArtifacts> RunGeneratorAsync(
        LanguageVersion languageVersion,
        string source,
        string assemblyName
    )
    {
        var compilation = await CreateCompilationAsync(languageVersion, source, assemblyName);
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

    public static (
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

    public static string FormatDiagnostics(Diagnostic[] diagnostics)
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
}
