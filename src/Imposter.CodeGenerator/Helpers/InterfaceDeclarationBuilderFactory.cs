using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Helpers;

internal static class InterfaceDeclarationBuilderFactory
{
    internal static InterfaceDeclarationBuilder CreateForMethod(MethodModel method, string name)
    {
        return new InterfaceDeclarationBuilder(
            name,
            TypeParameterListSyntax(method.TypeParameters)
        ).AddConstraintClauses(TypeParameterConstraintClauses(method.TypeParameters));
    }
}
