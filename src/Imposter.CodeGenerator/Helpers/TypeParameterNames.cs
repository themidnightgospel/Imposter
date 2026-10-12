using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Helpers;

// Generated code declares its locals with var and its discards with _, and a member declaration that starts with record
// or partial reads as a record or a partial member. A type parameter of one of these names takes another in generated
// code, one that no name declared beside it there has.
internal static class TypeParameterNames
{
    private static readonly HashSet<string> NamesGeneratedCodeCantUse =
    [
        "var",
        "_",
        "record",
        "partial",
    ];

    internal static string NameInGeneratedCode(this ITypeParameterSymbol typeParameter) =>
        IsRenamed(typeParameter)
            ? new NameSet(NamesBeside(typeParameter)).Use(typeParameter.Name)
            : typeParameter.Name;

    // The type as ToDisplayString writes it, with its type parameters named as in generated code.
    internal static string ToGeneratedCodeString(
        this ITypeSymbol type,
        SymbolDisplayFormat format
    ) => string.Concat(type.ToDisplayParts(format).Select(InGeneratedCode));

    private static string InGeneratedCode(SymbolDisplayPart part) =>
        part.Symbol is ITypeParameterSymbol typeParameter && IsRenamed(typeParameter)
            ? typeParameter.NameInGeneratedCode()
            : part.ToString();

    private static bool IsRenamed(ITypeParameterSymbol typeParameter) =>
        NamesGeneratedCodeCantUse.Contains(typeParameter.Name);

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

    // The type parameters a type refers to, the type arguments of a constructed one included.
    private static IEnumerable<ITypeParameterSymbol> TypeParametersIn(ITypeSymbol type) =>
        type switch
        {
            ITypeParameterSymbol typeParameter => [typeParameter],
            IArrayTypeSymbol array => TypeParametersIn(array.ElementType),
            IPointerTypeSymbol pointer => TypeParametersIn(pointer.PointedAtType),
            INamedTypeSymbol named => named
                .TypeArguments.SelectMany(TypeParametersIn)
                .Concat(
                    named.ContainingType is { } containingType
                        ? TypeParametersIn(containingType)
                        : []
                ),
            _ => [],
        };

    private static IEnumerable<ISymbol> MembersOf(INamedTypeSymbol type) =>
        type.AllInterfaces.Concat(TypeAndBaseTypes(type)).SelectMany(it => it.GetMembers());

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
