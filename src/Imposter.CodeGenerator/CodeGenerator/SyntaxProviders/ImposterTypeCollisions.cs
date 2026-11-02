using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal static class ImposterTypeCollisions
{
    internal static IEnumerable<GenerateImposterDeclaration> Mark(
        IReadOnlyList<GenerateImposterDeclaration> declarations
    ) =>
        NameFilesApart(
            NameExtensionClassesApart(MarkSameImposterTypes(declarations).ToArray()).ToArray()
        );

    // Same-named types nested in different classes, for example, would get imposters of the same type in the same
    // namespace. Each such declaration reports IMP007, naming another target of its group, and is not generated.
    // Registrations of one target, such as closed and open ones of a generic type, share its imposter instead: the
    // first generates it, and the others only report their diagnostics.
    private static IEnumerable<GenerateImposterDeclaration> MarkSameImposterTypes(
        IReadOnlyList<GenerateImposterDeclaration> declarations
    )
    {
        var sameImposterTypeAs =
            new Dictionary<GenerateImposterDeclaration, GenerateImposterDeclaration>();
        var repeatedRegistrations = new HashSet<GenerateImposterDeclaration>();

        foreach (
            var group in declarations
                .Where(it => it.CanCollide)
                .GroupBy(it => it.ImposterTypeName)
                .Select(group => group.ToArray())
                .Where(group => group.Length > 1)
        )
        {
            if (group.Select(it => it.Target).Distinct().Count() == 1)
            {
                repeatedRegistrations.UnionWith(group.Skip(1));
                continue;
            }

            for (var index = 0; index < group.Length; index++)
            {
                sameImposterTypeAs[group[index]] = group[(index + 1) % group.Length];
            }
        }

        return declarations.Select(declaration =>
            sameImposterTypeAs.TryGetValue(declaration, out var other)
                ? declaration with
                {
                    Diagnostics = new([
                        DiagnosticModel.Create(
                            DiagnosticDescriptors.ImposterTypeNameCollision,
                            declaration.TargetLocation,
                            declaration.TargetDisplayName,
                            other.TargetDisplayName,
                            declaration.ImposterTypeDisplayName
                        ),
                    ]),
                    Target = null,
                }
            : repeatedRegistrations.Contains(declaration) ? declaration with { Target = null }
            : declaration
        );
    }

    // A target's Imposter() extensions go into a non-generic static class named after it, so same-named targets of
    // different arity in one namespace, such as IFoo and IFoo<T>, would declare the same class. The generic ones end
    // its name in their arity.
    private static IEnumerable<GenerateImposterDeclaration> NameExtensionClassesApart(
        IReadOnlyList<GenerateImposterDeclaration> declarations
    )
    {
        var imposterTypeNamesWithArity = new HashSet<string>(
            declarations
                .Where(it => it.Target is not null)
                .Select(it => it.ImposterTypeName)
                .Distinct()
                .GroupBy(name => name.Split(ArityMark)[0])
                .Where(group => group.Count() > 1)
                .SelectMany(group => group.Where(name => name.IndexOf(ArityMark) >= 0))
        );

        return declarations.Select(declaration =>
            imposterTypeNamesWithArity.Contains(declaration.ImposterTypeName)
                ? declaration with
                {
                    ExtensionClassNameIncludesArity = true,
                }
                : declaration
        );
    }

    // Roslyn compares hint names ignoring case, so the files of targets such as IFoo and Ifoo, whose names differ only
    // in letter case, would make it drop every generated source. Their files are named with a hash of the target name.
    private static IEnumerable<GenerateImposterDeclaration> NameFilesApart(
        IReadOnlyList<GenerateImposterDeclaration> declarations
    )
    {
        var hintNamesDifferingOnlyInCase = new HashSet<string>(
            declarations
                .Where(it => it.Target is not null)
                .Select(PlainHintName)
                .Distinct()
                .GroupBy(name => name, StringComparer.OrdinalIgnoreCase)
                .Where(group => group.Count() > 1)
                .SelectMany(group => group)
        );

        return declarations.Select(declaration =>
            declaration.Target is not null
            && hintNamesDifferingOnlyInCase.Contains(PlainHintName(declaration))
                ? declaration with
                {
                    HintNameIncludesHash = true,
                }
                : declaration
        );

        static string PlainHintName(GenerateImposterDeclaration declaration) =>
            ImposterGenerationContext.GetHintName(
                declaration.Target!.IdentityName,
                declaration.PutInTheSameNamespace,
                includesHash: false
            );
    }

    private const char ArityMark = '`';

    // The imposter's namespace-qualified name with its arity, e.g. Sample.IRepositoryImposter`1: imposters with
    // differently named type parameters are still the same type.
    internal static string GetImposterTypeName(
        INamedTypeSymbol target,
        string? imposterNamespaceName
    )
    {
        var arity = target.TypeParametersIn().Count();
        return QualifiedImposterName(
            target,
            imposterNamespaceName,
            arity > 0 ? $"{ArityMark}{arity}" : ""
        );
    }

    // The imposter type as C# writes it, e.g. Sample.IRepositoryImposter<T>.
    internal static string GetImposterTypeDisplayName(
        INamedTypeSymbol target,
        string? imposterNamespaceName
    )
    {
        var typeParameters = target.TypeParametersIn().ToArray();
        return QualifiedImposterName(
            target,
            imposterNamespaceName,
            typeParameters.Length > 0
                ? $"<{string.Join(", ", typeParameters.Select(it => it.NameInGeneratedCode()))}>"
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
