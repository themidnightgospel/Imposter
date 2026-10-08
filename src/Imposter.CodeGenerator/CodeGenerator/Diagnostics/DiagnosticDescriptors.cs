using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Diagnostics;

public static class DiagnosticDescriptors
{
    // Each diagnostic links to its own section, whose anchor is the lower-case diagnostic ID.
    private const string HelpUrl =
        "https://themidnightgospel.github.io/Imposter/latest/diagnostics/";

    public static readonly DiagnosticDescriptor InvalidImposterTarget = new(
        "IMP002",
        "Imposter target must be an interface or non-sealed class",
        "Apply [GenerateImposter] to an interface or make '{0}' non-sealed (currently a '{1}')",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Unsupported target type. Imposters can target interfaces or extensible classes only.",
        helpLinkUri: HelpUrl + "#imp002"
    );

    public static readonly DiagnosticDescriptor NotSupportedCSharpVersion = new(
        "IMP003",
        "C# version not supported",
        "Current C# version '{0}' is not supported, version '{1}' or higher is required",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "The generator relies on C# 9.0 features, so it generates no imposters for older language versions.",
        helpLinkUri: HelpUrl + "#imp003"
    );

    public static readonly DiagnosticDescriptor ImposterTargetMustHaveAccessibleConstructor = new(
        "IMP004",
        "Imposter target must expose an accessible constructor",
        "Declare a public, internal, or protected constructor on '{0}' that the generated imposter can call",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Accessible constructor required so generated imposters can instantiate the target.",
        helpLinkUri: HelpUrl + "#imp004"
    );

    public static readonly DiagnosticDescriptor ClosedGenericImposterTarget = new(
        "IMP006",
        "Closed generic imposter target",
        "Register the open generic type 'typeof({1})' instead of '{0}'; the generated imposter keeps the type parameters and ignores these type arguments",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Warning,
        true,
        description: "A closed generic target generates the same generic imposter as its open type, with type parameters that have no effect.",
        helpLinkUri: HelpUrl + "#imp006"
    );

    public static readonly DiagnosticDescriptor GeneratorCrash = new(
        "IMP005",
        "Generator crash",
        "Unhandled exception while generating imposters: '{0}'",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An unexpected exception bubbled out of the source generator.",
        helpLinkUri: HelpUrl + "#imp005"
    );

    public static readonly DiagnosticDescriptor GeneratorLog = new(
        "IMPLOG001",
        "Imposter generator log",
        "{0}",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Info,
        true,
        description: "Generator log message, written when the IMPOSTER_LOG MSBuild property is true.",
        helpLinkUri: HelpUrl + "#implog001"
    );
}
