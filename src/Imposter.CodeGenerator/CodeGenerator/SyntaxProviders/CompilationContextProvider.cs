using System.Collections.Immutable;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Supplies the compilation context and the diagnostic for unsupported C# versions.
/// </summary>
internal static class CompilationContextProvider
{
    internal static IncrementalValueProvider<CompilationContext> GetCompilationContext(
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

        return context
            .CompilationProvider.Combine(loggingEnabledProvider)
            .Select(
                static (pair, _) => new CompilationContext((CSharpCompilation)pair.Left, pair.Right)
            )
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("CompilationContext")
#endif
        ;
    }

    internal static IncrementalValuesProvider<Diagnostic> GetCompilationDiagnostics(
        this IncrementalValueProvider<CompilationContext> compilationContextProvider
    )
    {
        return compilationContextProvider
            .SelectMany(
                static (compilationContext, _) => ValidateLanguageVersion(compilationContext)
            )
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("CompilationDiagnostics")
#endif
        ;
    }

    private static ImmutableArray<Diagnostic> ValidateLanguageVersion(
        CompilationContext compilationContext
    ) =>
        compilationContext.IsLanguageVersionSupported
            ? ImmutableArray<Diagnostic>.Empty
            : ImmutableArray.Create(
                Diagnostic.Create(
                    DiagnosticDescriptors.NotSupportedCSharpVersion,
                    Location.None,
                    compilationContext.Compilation.LanguageVersion.ToDisplayString(),
                    CompilationContext.MinimumLanguageVersion.ToDisplayString()
                )
            );
}
