using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.CodeGenerator.Logging;

// Writes the IMPOSTER_LOG messages. They are Info diagnostics, which builds show only at detailed verbosity (-v:d).
internal readonly struct DiagnosticLogger
{
    private readonly SourceProductionContext _context;
    private readonly bool _enabled;

    internal DiagnosticLogger(SourceProductionContext context, bool enabled)
    {
        _context = context;
        _enabled = enabled;
    }

    internal void LogLanguageVersion(LanguageVersion languageVersion)
    {
        if (_enabled)
        {
            var imposterExtensions = DescribeImposterExtensions(
                new SupportedCSharpFeatures(languageVersion)
            );

            Log($"C# {languageVersion.ToDisplayString()}: {imposterExtensions}");
        }
    }

    internal void LogImposter(in ImposterGenerationContext imposterGenerationContext)
    {
        if (_enabled)
        {
            var target = imposterGenerationContext.Target.DisplayName;

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
        _context.ReportDiagnostic(
            Diagnostic.Create(DiagnosticDescriptors.GeneratorLog, Location.None, message)
        );
}
