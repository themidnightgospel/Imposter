using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Helpers;

internal static class InterfaceDeclarationBuilderFactory
{
    internal static InterfaceDeclarationBuilder CreateForMethod(IMethodSymbol method, string name)
    {
        return new InterfaceDeclarationBuilder(name, TypeParameterListSyntax(method));
    }
}