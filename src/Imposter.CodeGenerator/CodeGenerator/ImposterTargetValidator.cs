using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator;

internal static class ImposterTargetValidator
{
    public static bool Validate(
        SourceProductionContext sourceProductionContext,
        GenerateImposterDeclaration generateImposterDeclaration)
    {
        var target = generateImposterDeclaration.ImposterTarget;

        if (!IsInterfaceOrNonSealedClass(target))
        {
            ReportImposterTargetMustBeInterface(sourceProductionContext, target);
            return false;
        }

        if (target.TypeKind == TypeKind.Class && !HasAccessibleConstructor(target))
        {
            ReportImposterTargetMustHaveAccessibleConstructor(sourceProductionContext, target);
            return false;
        }

        return true;
    }

    private static void ReportImposterTargetMustBeInterface(
        SourceProductionContext sourceProductionContext,
        INamedTypeSymbol target)
    {
        var targetLocation = GetPreferredLocation(target);
        var targetDisplayName = target.ToDisplayString();

        sourceProductionContext.ReportDiagnostic(
            Diagnostic.Create(
                DiagnosticDescriptors.InvalidImposterTarget,
                targetLocation,
                targetDisplayName,
                target.TypeKind));
    }

    private static void ReportImposterTargetMustHaveAccessibleConstructor(
        SourceProductionContext sourceProductionContext,
        INamedTypeSymbol target)
    {
        var targetLocation = GetPreferredLocation(target);

        sourceProductionContext.ReportDiagnostic(
            Diagnostic.Create(
                DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor,
                targetLocation,
                target.ToDisplayString()));
    }

    private static bool IsInterfaceOrNonSealedClass(INamedTypeSymbol typeSymbol)
        => typeSymbol.TypeKind == TypeKind.Interface ||
           typeSymbol is { TypeKind: TypeKind.Class, IsSealed: false };

    private static bool HasAccessibleConstructor(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.InstanceConstructors.Length == 0)
        {
            return true;
        }

        foreach (var constructor in typeSymbol.InstanceConstructors)
        {
            if (constructor.DeclaredAccessibility is Accessibility.Private)
            {
                continue;
            }

            return true;
        }

        return false;
    }

    private static Location GetPreferredLocation(INamedTypeSymbol typeSymbol)
    {
        foreach (var location in typeSymbol.Locations)
        {
            if (location.IsInSource)
            {
                return location;
            }
        }

        return Location.None;
    }
}
