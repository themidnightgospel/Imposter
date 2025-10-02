using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax TypeSyntax(ITypeSymbol typeSymbol)
    {
        return ParseTypeName(
            typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
        );
    }

    internal static TypeParameterSyntax TypeParameter(ITypeParameterSymbol typeParameterSymbol) => SyntaxFactory.TypeParameter(typeParameterSymbol.Name);

    internal static TypeParameterListSyntax? TypeParameterList(IMethodSymbol method) =>
        method.TypeParameters.Length > 0
            ? SyntaxFactory.TypeParameterList(
                SeparatedList(
                    method.TypeParameters.Select(TypeParameter)
                )
            )
            : default;

    internal static NameSyntax TypeSyntaxWithGenericArguments(IMethodSymbol method, string typeName)
    {
        if (method.IsGenericMethod)
        {
            return GenericName(
                Identifier(typeName),
                TypeArgumentList(
                    SeparatedList(method.TypeArguments.Select(TypeSyntax))
                )
            );
        }

        return IdentifierName(typeName);
    }
}