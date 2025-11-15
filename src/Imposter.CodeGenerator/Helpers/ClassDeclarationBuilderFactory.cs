using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Helpers;

internal static class ClassDeclarationBuilderFactory
{
    internal static ClassDeclarationBuilder CreateForMethod(IMethodSymbol method, string name)
    {
        return new ClassDeclarationBuilder(name, TypeParameterListSyntax(method));
    }
}
