using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Diagnostics;

public static class DiagnosticReporter
{
    public static void ReportDiagnostics(
        this IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Diagnostic> diagnostic
    )
    {
        context.RegisterSourceOutput(diagnostic, static (context, diagnostic) => context.ReportDiagnostic(diagnostic));
    }

}