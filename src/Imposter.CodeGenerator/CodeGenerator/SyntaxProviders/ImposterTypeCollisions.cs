using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal static class ImposterTypeCollisions
{
    // Same-named types nested in different classes, for example, would get imposters of the same type in the same
    // namespace. Each such declaration reports IMP007, naming another target of its group, and is not generated.
    internal static IEnumerable<GenerateImposterDeclaration> Mark(
        IReadOnlyList<GenerateImposterDeclaration> declarations
    )
    {
        var sameImposterTypeAs =
            new Dictionary<GenerateImposterDeclaration, GenerateImposterDeclaration>();

        foreach (
            var group in declarations
                .Where(it => it.CanCollide)
                .GroupBy(it => it.ImposterTypeName)
                .Select(group => group.ToArray())
                .Where(group => group.Length > 1)
        )
        {
            for (var index = 0; index < group.Length; index++)
            {
                sameImposterTypeAs[group[index]] = group[(index + 1) % group.Length];
            }
        }

        return declarations.Select(declaration =>
            sameImposterTypeAs.TryGetValue(declaration, out var other)
                ? declaration with
                {
                    Diagnostics = new[]
                    {
                        DiagnosticModel.Create(
                            DiagnosticDescriptors.ImposterTypeNameCollision,
                            declaration.TargetLocation,
                            declaration.TargetDisplayName,
                            other.TargetDisplayName,
                            declaration.ImposterTypeDisplayName
                        ),
                    }.ToEquatableArray(),
                    Target = null,
                }
                : declaration
        );
    }

    // The imposter's namespace-qualified name with its arity, e.g. Sample.IRepositoryImposter`1: imposters with
    // differently named type parameters are still the same type.
    internal static string GetImposterTypeName(
        INamedTypeSymbol target,
        string? imposterNamespaceName
    )
    {
        var arity = target.Arity;
        return QualifiedImposterName(target, imposterNamespaceName, arity > 0 ? $"`{arity}" : "");
    }

    // The imposter type as C# writes it, e.g. Sample.IRepositoryImposter<T>.
    internal static string GetImposterTypeDisplayName(
        INamedTypeSymbol target,
        string? imposterNamespaceName
    )
    {
        var typeParameters = target.OriginalDefinition.TypeParameters;
        return QualifiedImposterName(
            target,
            imposterNamespaceName,
            typeParameters.Length > 0
                ? $"<{string.Join(", ", typeParameters.Select(it => it.Name))}>"
                : ""
        );
    }

    private static string QualifiedImposterName(
        INamedTypeSymbol target,
        string? imposterNamespaceName,
        string typeParameterSuffix
    )
    {
        var typeName = ImposterTargetMetadata.GetImposterName(target.Name) + typeParameterSuffix;

        return imposterNamespaceName is { } namespaceName
            ? $"{namespaceName}.{typeName}"
            : typeName;
    }
}
