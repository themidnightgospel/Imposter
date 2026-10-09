using System.Collections.Immutable;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Features.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Supplies the generator options and the diagnostic for unsupported C# versions.
/// </summary>
internal static class GeneratorOptionsProvider
{
    internal static IncrementalValueProvider<GeneratorOptions> GetGeneratorOptions(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        var loggingEnabledProvider = context.AnalyzerConfigOptionsProvider.Select(
            static (options, _) =>
                options.GlobalOptions.TryGetValue(
                    "build_property.IMPOSTER_LOG",
                    out var imposterLogPoperty
                )
                && string.Equals(
                    imposterLogPoperty.Trim(),
                    "true",
                    System.StringComparison.OrdinalIgnoreCase
                )
        );

        // Only the language version is taken from the compilation: an edit that keeps it leaves the options equal.
        // Keeping the compilation itself would make every later step run again after every edit.
        return context
            .CompilationProvider.Select(
                static (compilation, _) => ((CSharpCompilation)compilation).LanguageVersion
            )
            .Combine(loggingEnabledProvider)
            .Select(static (pair, _) => new GeneratorOptions(pair.Left, pair.Right))
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("GeneratorOptions")
#endif
        ;
    }

    internal static IncrementalValuesProvider<Diagnostic> GetLanguageVersionDiagnostics(
        this IncrementalValueProvider<GeneratorOptions> optionsProvider
    )
    {
        return optionsProvider
            .SelectMany(static (options, _) => ValidateLanguageVersion(options))
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("LanguageVersionDiagnostics")
#endif
        ;
    }

    private static ImmutableArray<Diagnostic> ValidateLanguageVersion(GeneratorOptions options) =>
        options.IsLanguageVersionSupported
            ? ImmutableArray<Diagnostic>.Empty
            : ImmutableArray.Create(
                Diagnostic.Create(
                    DiagnosticDescriptors.NotSupportedCSharpVersion,
                    Location.None,
                    options.LanguageVersion.ToDisplayString(),
                    GeneratorOptions.MinimumLanguageVersion.ToDisplayString()
                )
            );
}
