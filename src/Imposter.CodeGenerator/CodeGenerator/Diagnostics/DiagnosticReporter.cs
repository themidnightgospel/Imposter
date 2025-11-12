using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Diagnostics;

public static class DiagnosticReporter
{
public static void ReportDiagnostics(
        this in IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Diagnostic> diagnostic
    )
    {
        context.RegisterSourceOutput(diagnostic, static (context, diagnostic) => context.ReportDiagnostic(diagnostic));
    }

}
