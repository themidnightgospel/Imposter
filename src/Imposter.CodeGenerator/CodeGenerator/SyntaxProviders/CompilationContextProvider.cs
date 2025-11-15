using System.Collections.Generic;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Supplies compilation context and early diagnostics for language/version.
/// </summary>
internal static class CompilationContextProvider
{
    internal static IncrementalValueProvider<CompilationContext> GetCompilationContext(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        var loggingEnabledProvider = context.AnalyzerConfigOptionsProvider.Select(
            static (options, _) =>
                options.GlobalOptions.TryGetValue("build_property.IMPOSTER_LOG", out var v)
                && string.Equals(v?.Trim(), "true", System.StringComparison.OrdinalIgnoreCase)
        );

        return context
            .CompilationProvider.Combine(loggingEnabledProvider)
            .Select(
                static (pair, _) =>
                    new CompilationContext(
                        (CSharpCompilation)pair.Left,
                        new NameSet([]),
                        pair.Right
                    )
            )
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("CompilationContext")
#endif
        ;
    }

    internal static IncrementalValuesProvider<Diagnostic> GetCompilationDiagnostics(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        return context
            .CompilationProvider.SelectMany(
                static (compilation, _) => ValidateCSharpCompilation(compilation)
            )
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("CompilationDiagnostics")
#endif
        ;
    }

    private static IEnumerable<Diagnostic> ValidateCSharpCompilation(Compilation compilation)
    {
        if (compilation is not CSharpCompilation csCompilation)
        {
            yield return Diagnostic.Create(
                DiagnosticDescriptors.UnsupportedLanguage,
                Location.None,
                compilation.Language,
                LanguageVersion.CSharp8.ToDisplayString()
            );
            yield break;
        }

        if (csCompilation.LanguageVersion < LanguageVersion.CSharp8)
        {
            yield return Diagnostic.Create(
                DiagnosticDescriptors.NotSupportedCSharpVersion,
                Location.None,
                csCompilation.LanguageVersion.ToDisplayString(),
                LanguageVersion.CSharp8.ToDisplayString()
            );
        }
    }
}
