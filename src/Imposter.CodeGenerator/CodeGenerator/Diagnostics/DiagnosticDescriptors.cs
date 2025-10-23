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

    public static readonly DiagnosticDescriptor HigherCSharpVersionIsRequired = new(
        "IMP003",
        "Higher C# version is required",
        "Current C# version {0} is not supported, version {1} or higher is required",
        DiagnosticCategories.Imposter,
        DiagnosticSeverity.Error,
        true
    );

}