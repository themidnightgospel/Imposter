using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal static class ImposterTypeCollisions
{
    // Same-named types nested in different classes, for example, would get imposters of the same type in the same
    // namespace. Each such declaration is marked with another target of its group.
    internal static IEnumerable<GenerateImposterDeclaration> Mark(
        IReadOnlyList<GenerateImposterDeclaration> declarations
    )
    {
        var sameImposterTypeAs = new Dictionary<GenerateImposterDeclaration, INamedTypeSymbol>();

        foreach (
            var group in declarations
                .Where(it => ImposterTargetValidator.IsInterfaceOrNonSealedClass(it.ImposterTarget))
                .GroupBy(GetImposterTypeName)
                .Select(group => group.ToArray())
                .Where(group => group.Length > 1)
        )
        {
            for (var index = 0; index < group.Length; index++)
            {
                sameImposterTypeAs[group[index]] = group[(index + 1) % group.Length].ImposterTarget;
            }
        }

        return declarations.Select(declaration =>
            sameImposterTypeAs.TryGetValue(declaration, out var otherTarget)
                ? declaration with
                {
                    SameImposterTypeAs = otherTarget,
                }
                : declaration
        );
    }

    // The imposter's namespace-qualified name with its arity, e.g. Sample.IRepositoryImposter`1: imposters with
    // differently named type parameters are still the same type.
    private static string GetImposterTypeName(GenerateImposterDeclaration declaration)
    {
        var arity = declaration.ImposterTarget.Arity;
        return QualifiedImposterName(declaration, arity > 0 ? $"`{arity}" : "");
    }

    // The imposter type as C# writes it, e.g. Sample.IRepositoryImposter<T>.
    internal static string GetImposterTypeDisplayName(GenerateImposterDeclaration declaration)
    {
        var typeParameters = declaration.ImposterTarget.OriginalDefinition.TypeParameters;
        return QualifiedImposterName(
            declaration,
            typeParameters.Length > 0
                ? $"<{string.Join(", ", typeParameters.Select(it => it.Name))}>"
                : ""
        );
    }

    private static string QualifiedImposterName(
        GenerateImposterDeclaration declaration,
        string typeParameterSuffix
    )
    {
        var typeName =
            ImposterTargetMetadata.GetImposterName(declaration.ImposterTarget.Name)
            + typeParameterSuffix;

        return ImposterGenerationContext.GetImposterNamespaceName(declaration) is { } namespaceName
            ? $"{namespaceName}.{typeName}"
            : typeName;
    }
}
