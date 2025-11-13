using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Logging;

internal readonly struct DiagnosticLogger : IGeneratorLogger
{
    private static readonly DiagnosticDescriptor LogDescriptor = new(
        id: "IMPLOG001",
        title: "Imposter generator log",
        messageFormat: "{0}",
        category: Diagnostics.DiagnosticCategories.Imposter,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Internal generator log message. Enable with IMPOSTER_LOG=true.");

    private readonly SourceProductionContext _context;
    private readonly bool _enabled;

    internal DiagnosticLogger(SourceProductionContext context, bool enabled)
    {
        _context = context;
        _enabled = enabled;
    }

    public void Log(string message)
    {
        if (!_enabled || string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        _context.ReportDiagnostic(Diagnostic.Create(LogDescriptor, Location.None, message));
    }

    public void Log(string label, string value)
        => Log($"{label}: {value}");
}

