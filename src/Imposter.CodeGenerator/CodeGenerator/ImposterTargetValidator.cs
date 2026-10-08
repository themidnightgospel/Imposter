using System.Linq;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator;

internal static class ImposterTargetValidator
{
    public static bool Validate(
        in SourceProductionContext sourceProductionContext,
        GenerateImposterDeclaration generateImposterDeclaration,
        MemberAccess memberAccess
    )
    {
        var target = generateImposterDeclaration.ImposterTarget;

        if (!IsInterfaceOrNonSealedClass(target))
        {
            ReportImposterTargetMustBeInterface(sourceProductionContext, target);
            return false;
        }

        if (generateImposterDeclaration.SameImposterTypeAs is { } otherTarget)
        {
            ReportImposterTypeNameCollision(
                sourceProductionContext,
                generateImposterDeclaration,
                otherTarget
            );
            return false;
        }

        if (target.TypeKind == TypeKind.Class && !HasAccessibleConstructor(target, memberAccess))
        {
            ReportImposterTargetMustHaveAccessibleConstructor(sourceProductionContext, target);
            return false;
        }

        if (IsClosedGenericType(target))
        {
            ReportClosedGenericTarget(sourceProductionContext, target);
        }

        return true;
    }

    private static bool IsClosedGenericType(INamedTypeSymbol typeSymbol) =>
        typeSymbol.IsGenericType
        && !SymbolEqualityComparer.Default.Equals(typeSymbol, typeSymbol.OriginalDefinition);

    private static void ReportClosedGenericTarget(
        in SourceProductionContext sourceProductionContext,
        INamedTypeSymbol target
    )
    {
        sourceProductionContext.ReportDiagnostic(
            Diagnostic.Create(
                DiagnosticDescriptors.ClosedGenericImposterTarget,
                GetPreferredLocation(target),
                target.ToDisplayString(),
                target.ConstructUnboundGenericType().ToDisplayString()
            )
        );
    }

    private static void ReportImposterTargetMustBeInterface(
        in SourceProductionContext sourceProductionContext,
        INamedTypeSymbol target
    )
    {
        var targetLocation = GetPreferredLocation(target);
        var targetDisplayName = target.ToDisplayString();

        sourceProductionContext.ReportDiagnostic(
            Diagnostic.Create(
                DiagnosticDescriptors.InvalidImposterTarget,
                targetLocation,
                targetDisplayName,
                target.TypeKind
            )
        );
    }

    private static void ReportImposterTargetMustHaveAccessibleConstructor(
        in SourceProductionContext sourceProductionContext,
        INamedTypeSymbol target
    )
    {
        var targetLocation = GetPreferredLocation(target);

        sourceProductionContext.ReportDiagnostic(
            Diagnostic.Create(
                DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor,
                targetLocation,
                target.ToDisplayString()
            )
        );
    }

    private static void ReportImposterTypeNameCollision(
        in SourceProductionContext sourceProductionContext,
        GenerateImposterDeclaration declaration,
        INamedTypeSymbol otherTarget
    )
    {
        sourceProductionContext.ReportDiagnostic(
            Diagnostic.Create(
                DiagnosticDescriptors.ImposterTypeNameCollision,
                GetPreferredLocation(declaration.ImposterTarget),
                declaration.ImposterTarget.ToDisplayString(),
                otherTarget.ToDisplayString(),
                ImposterTypeCollisions.GetImposterTypeDisplayName(declaration)
            )
        );
    }

    internal static bool IsInterfaceOrNonSealedClass(INamedTypeSymbol typeSymbol) =>
        typeSymbol.TypeKind == TypeKind.Interface
        || typeSymbol is { TypeKind: TypeKind.Class, IsSealed: false };

    // A source class always has at least its implicit constructor. A class from another assembly can show none, when
    // the build imports only public and protected metadata and every constructor is internal.
    private static bool HasAccessibleConstructor(
        INamedTypeSymbol typeSymbol,
        MemberAccess memberAccess
    ) => typeSymbol.InstanceConstructors.Any(memberAccess.IsAccessible);

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
