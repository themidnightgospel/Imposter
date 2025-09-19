using System.Collections.Generic;
using Imposter.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.SyntaxProviders;

internal static class CompilationDiagnosticsProvider
{
    internal static IncrementalValuesProvider<Diagnostic> GetCompilationDiagnostics(this IncrementalGeneratorInitializationContext context)
    {
        return context
            .CompilationProvider
            .SelectMany(static (compilation, _) => ValidateCSharpCompilation(compilation));
    }

    private static IEnumerable<Diagnostic> ValidateCSharpCompilation(Compilation compilation)
    {
        if (compilation is not CSharpCompilation csCompilation)
        {
            yield return Diagnostic.Create(
                DiagnosticDescriptors.OnlyCSharpIsSupported,
                null,
                compilation.Language,
                LanguageVersion.CSharp9.ToDisplayString()
            );
            yield break;
        }

        if (csCompilation.LanguageVersion < LanguageVersion.CSharp8)
        {
            yield return Diagnostic.Create(
                DiagnosticDescriptors.HigherCSharpVersionIsRequired,
                null,
                csCompilation.LanguageVersion.ToDisplayString(), LanguageVersion.CSharp8.ToDisplayString()
            );
        }
    }
}