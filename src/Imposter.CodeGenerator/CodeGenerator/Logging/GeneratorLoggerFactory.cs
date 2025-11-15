using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Logging;

internal static class GeneratorLoggerFactory
{
    internal static IGeneratorLogger Create(SourceProductionContext context, bool isEnabled) =>
        isEnabled ? new DiagnosticLogger(context, true) : new NoOpDiagnosticLogger();
}
