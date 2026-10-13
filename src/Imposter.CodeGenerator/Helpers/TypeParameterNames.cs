using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Helpers;

// Generated code writes a type parameter's name bare, where a contextual keyword can change what the code means: its
// locals are declared with var and its discards with _, a member declaration that starts with record, partial, file,
// required or extension reads as a record, a partial member, that modifier or an extension block, scoped can't name a
// type, and await can't in an async lambda. So a type parameter named like any contextual keyword of the compiler that
// runs the generator takes another name in generated code, one that no name declared beside it there has. A nested
// type's parameters also share the imposter's scope with its containing types' parameters, so names that shadow a
// containing parameter are renamed too.
// A target's type parameter also cannot share the name of its generated imposter class.
// A method's type parameters cannot share the fixed member names of the helper types that repeat them.
internal static class TypeParameterNames
{
    // Imposter identities, which name dedicated namespaces and files, rename only these, as they have since these were
    // first renamed. The other names stay in the identities, so their imposters' namespaces don't change.
    private static readonly HashSet<string> NamesRenamedInImposterIdentity =
    [
        "var",
        "_",
        "record",
        "partial",
    ];

    internal static string NameInGeneratedCode(this ITypeParameterSymbol typeParameter)
    {
        var containingType =
            typeParameter.DeclaringMethod?.ContainingType
            ?? typeParameter.DeclaringType?.ContainingType;
        var containingNames = containingType is null
            ? []
            : containingType.TypeParametersIn().Select(it => it.NameInGeneratedCode()).ToArray();
        var imposterNames = typeParameter.DeclaringType is { } declaringType
            ? NamesInImposters(declaringType).ToArray()
            : [];
        var clashesWithMethodMember =
            typeParameter.DeclaringMethod is not null
            && GeneratedMethodMemberNames.Reserved.Contains(typeParameter.Name);

        return
            SyntaxFacts.GetContextualKeywordKind(typeParameter.Name) != SyntaxKind.None
            || containingNames.Contains(typeParameter.Name)
            || imposterNames.Contains(typeParameter.Name)
            || clashesWithMethodMember
            ? new NameSet(
                NamesBeside(typeParameter)
                    .Concat(containingNames)
                    .Concat(imposterNames)
                    .Concat(
                        typeParameter.DeclaringMethod is not null
                            ? GeneratedMethodMemberNames.Reserved
                            : Enumerable.Empty<string>()
                    )
            ).Use(typeParameter.Name)
            : typeParameter.Name;
    }

    // The type as ToDisplayString writes it, with its type parameters named as in generated code.
    internal static string ToGeneratedCodeString(
        this ITypeSymbol type,
        SymbolDisplayFormat format
    ) => string.Concat(type.ToDisplayParts(format).Select(InGeneratedCode));

    // Public namespaces and file names keep the target's identity independently of collision-driven renaming.
    // Preserve the keyword substitutions already used in dedicated namespaces before this collision was handled.
    internal static string ToImposterIdentityString(this INamedTypeSymbol type) =>
        string.Concat(
            type.ToDisplayParts(SymbolDisplayFormat.FullyQualifiedFormat)
                .Select(part =>
                    part.Symbol is ITypeParameterSymbol parameter
                    && NamesRenamedInImposterIdentity.Contains(parameter.Name)
                        ? new NameSet(NamesBeside(parameter)).Use(parameter.Name)
                        : part.ToString()
                )
        );

    private static string InGeneratedCode(SymbolDisplayPart part)
    {
        if (part.Symbol is ITypeParameterSymbol typeParameter)
        {
            var name = typeParameter.NameInGeneratedCode();
            if (name != typeParameter.Name)
            {
                return name;
            }
        }

        return part.ToString();
    }

    // A method's type parameter meets the method's others, its parameters and, under their names in generated code, the
    // type parameters of the types it's a member of. A type's meets the type's others, those of its containing types,
    // and the members of the type, its base types and interfaces, with their type parameters and parameters.
    private static IEnumerable<string> NamesBeside(ITypeParameterSymbol typeParameter) =>
        typeParameter.DeclaringMethod is { } method
            ? NamesDeclaredBy(method)
                .Concat(
                    TypeParametersIn(method.ContainingType).Select(it => it.NameInGeneratedCode())
                )
            : TypeParametersIn(typeParameter.DeclaringType!)
                .Select(it => it.Name)
                .Concat(MembersOf(typeParameter.DeclaringType!).SelectMany(NamesDeclaredBy));

    // The type parameters a type refers to, from outer to inner. A constructed containing type contributes only
    // parameters still present in its arguments, so closed registrations keep their concrete containing types.
    internal static IEnumerable<ITypeParameterSymbol> TypeParametersIn(this ITypeSymbol type) =>
        type switch
        {
            ITypeParameterSymbol typeParameter => [typeParameter],
            IArrayTypeSymbol array => TypeParametersIn(array.ElementType),
            IPointerTypeSymbol pointer => TypeParametersIn(pointer.PointedAtType),
            INamedTypeSymbol named => (
                named.ContainingType is { } containingType ? TypeParametersIn(containingType) : []
            ).Concat(named.TypeArguments.SelectMany(TypeParametersIn)),
            _ => [],
        };

    private static IEnumerable<ISymbol> MembersOf(INamedTypeSymbol type) =>
        type.AllInterfaces.Concat(TypeAndBaseTypes(type)).SelectMany(it => it.GetMembers());

    // A parameter must differ from its imposter's class name. A containing parameter also appears on the imposters
    // of nested targets, where it meets their class names and members. Include deeper targets and inherited members,
    // and choose the same name wherever the parameter appears (including references from inherited interfaces).
    private static IEnumerable<string> NamesInImposters(INamedTypeSymbol type) =>
        type.GetTypeMembers()
            .SelectMany(nested =>
                MembersOf(nested).SelectMany(NamesDeclaredBy).Concat(NamesInImposters(nested))
            )
            .Prepend(ImposterTargetMetadata.GetImposterName(type.Name));

    private static IEnumerable<string> NamesDeclaredBy(ISymbol member) =>
        member is IMethodSymbol method
            ? method
                .TypeParameters.Select(it => it.Name)
                .Concat(method.Parameters.Select(it => it.Name))
                .Prepend(method.Name)
            : [member.Name];

    private static IEnumerable<INamedTypeSymbol> TypeAndBaseTypes(INamedTypeSymbol type)
    {
        for (INamedTypeSymbol? current = type; current is not null; current = current.BaseType)
        {
            yield return current;
        }
    }
}
