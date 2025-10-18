using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static InterfaceDeclarationBuilder InterfaceDeclarationBuilder(IMethodSymbol method, string name)
    {
        return new InterfaceDeclarationBuilder(name, TypeParameterListSyntax(method));
    }
    
    internal static ClassDeclarationBuilder ClassDeclarationBuilder(IMethodSymbol method, string name)
    {
        return new ClassDeclarationBuilder(name, TypeParameterListSyntax(method));
    }
}