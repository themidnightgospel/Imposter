using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.CodeGenerator.Logging;

// Writes the IMPOSTER_LOG messages. They are Info diagnostics, which builds show only at detailed verbosity (-v:d).
internal readonly struct DiagnosticLogger
{
    private static readonly DiagnosticDescriptor LogDescriptor = new(
        id: "IMPLOG001",
        title: "Imposter generator log",
        messageFormat: "{0}",
        category: Diagnostics.DiagnosticCategories.Imposter,
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Generator log message, written when the IMPOSTER_LOG MSBuild property is true."
    );

    private readonly SourceProductionContext _context;
    private readonly bool _enabled;

    internal DiagnosticLogger(SourceProductionContext context, bool enabled)
    {
        _context = context;
        _enabled = enabled;
    }

    internal void LogCompilation(CSharpCompilation compilation)
    {
        if (_enabled)
        {
            var languageVersion = compilation.LanguageVersion.ToDisplayString();
            var imposterExtensions = DescribeImposterExtensions(
                new SupportedCSharpFeatures(compilation)
            );

            Log($"C# {languageVersion}: {imposterExtensions}");
        }
    }

    internal void LogImposter(in ImposterGenerationContext imposterGenerationContext)
    {
        if (_enabled)
        {
            var target = imposterGenerationContext.TargetSymbol.ToDisplayString();

            Log($"Generated {imposterGenerationContext.HintName} for {target}");
        }
    }

    // Mirrors the condition under which ImposterGenerator emits the extensions.
    private static string DescribeImposterExtensions(in SupportedCSharpFeatures features) =>
#if ROSLYN4_14_OR_GREATER
        features.SupportsTypeExtensions
            ? "generating the static Imposter() extensions"
            : "not generating the static Imposter() extensions, which need C# 14 or later";
#else
        "not generating the static Imposter() extensions, which need Roslyn 4.14 or later";
#endif

    private void Log(string message) =>
        _context.ReportDiagnostic(Diagnostic.Create(LogDescriptor, Location.None, message));
}
