using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.Diagnostics;

public static class DiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor OnlyCSharpIsSupported = new(
        "IMP001",
        "Only C# language is supported",
        "Imposter does not support {0} but C# is required",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true
    );

    public static readonly DiagnosticDescriptor ImposterTargetMustBeInterface = new(
        "IMP002",
        "Imposter target must be an interface or non-sealed class",
        "GenerateImposterAttribute can only target interfaces or non-sealed classes. '{0}' is a {1}.",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true
    );

    public static readonly DiagnosticDescriptor HigherCSharpVersionIsRequired = new(
        "IMP003",
        "Higher C# version is required",
        "Current C# version {0} is not supported, version {1} or higher is required",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true
    );

    public static readonly DiagnosticDescriptor ImposterTargetMustHaveAccessibleConstructor = new(
        "IMP004",
        "Imposter target must expose an accessible constructor",
        "Type '{0}' must declare at least one constructor accessible to generated imposters",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true
    );

    public static readonly DiagnosticDescriptor GeneratorCrash = new(
        "IMP005",
        "Generator crash",
        "Unhandled exception while generating imposters: {0}",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true
    );

}
