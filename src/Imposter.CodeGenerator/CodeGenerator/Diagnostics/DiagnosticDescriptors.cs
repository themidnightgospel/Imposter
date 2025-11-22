using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Diagnostics;

public static class DiagnosticDescriptors
{
    private const string LanguageSupportHelpUrl =
        "https://themidnightgospel.github.io/Imposter/latest/";

    private const string TargetHelpUrl = "https://themidnightgospel.github.io/Imposter/latest/";

    private const string AccessibleConstructorHelpUrl =
        "https://github.com/themidnightgospel/Imposter/blob/master/Imposter/modules/Imposter.CodeGenerator.md#accessible-constructors";

    private const string CrashIssueUrl =
        "https://github.com/themidnightgospel/Imposter/issues/new?labels=bug&title=Generator%20crash:%20IMP005";

    public static readonly DiagnosticDescriptor UnsupportedLanguage = new(
        "IMP001",
        "Unsupported language",
        "Imposter only supports 'C#' language version '{1}' or higher. Current language is '{0}'.",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "The generator runs only for C# projects targeting language version 8.0 or newer.",
        helpLinkUri: LanguageSupportHelpUrl
    );

    public static readonly DiagnosticDescriptor InvalidImposterTarget = new(
        "IMP002",
        "Imposter target must be an interface or non-sealed class",
        "Apply [GenerateImposter] to an interface or make '{0}' non-sealed (currently a '{1}')",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Unsupported target type. Imposters can target interfaces or extensible classes only.",
        helpLinkUri: TargetHelpUrl
    );

    public static readonly DiagnosticDescriptor NotSupportedCSharpVersion = new(
        "IMP003",
        "C# version not supported",
        "Current C# version '{0}' is not supported, version '{1}' or higher is required",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "The generator relies on C# 9.0 features. Update the consuming project to a supported language version.",
        helpLinkUri: LanguageSupportHelpUrl
    );

    public static readonly DiagnosticDescriptor ImposterTargetMustHaveAccessibleConstructor = new(
        "IMP004",
        "Imposter target must expose an accessible constructor",
        "Declare a public, internal, or protected constructor on '{0}' that the generated imposter can call",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "Accessible constructor required so generated imposters can instantiate the target.",
        helpLinkUri: AccessibleConstructorHelpUrl
    );

    public static readonly DiagnosticDescriptor GeneratorCrash = new(
        "IMP005",
        "Generator crash",
        "Unhandled exception while generating imposters: '{0}'",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true,
        description: "An unexpected exception bubbled out of the source generator.",
        helpLinkUri: CrashIssueUrl
    );
}
