using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax TypeSyntax(ITypeSymbol typeSymbol) =>
        ParseTypeName(
            typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
        );

    internal static TypeParameterSyntax TypeParameterSyntax(ITypeParameterSymbol typeParameterSymbol)
        => TypeParameter(typeParameterSymbol.Name);

    internal static IEnumerable<TypeParameterSyntax> TypeParametersSyntax(IMethodSymbol method) =>
        method.TypeParameters.Length > 0
            ? method.TypeParameters.Select(TypeParameterSyntax)
            : [];

    internal static TypeParameterListSyntax? TypeParameterListSyntax(IMethodSymbol method) =>
        method.TypeParameters.Length > 0
            ? TypeParameterList(
                SeparatedList(
                    TypeParametersSyntax(method)
                )
            )
            : null;

    internal static SimpleNameSyntax WithMethodGenericArguments(string identifier, in ImposterTargetMethodMetadata method) =>
        method.GenericTypeArgumentListSyntax is not null
            ? GenericName(Identifier(identifier), method.GenericTypeArgumentListSyntax)
            : IdentifierName(identifier);

    internal static NameSyntax WithMethodGenericArguments(IReadOnlyList<NameSyntax> genericArguments, string typeName)
    {
        if (genericArguments.Count > 0)
        {
            return GenericName(
                Identifier(typeName),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(genericArguments)
                )
            );
        }

        return IdentifierName(typeName);
    }
}