using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Helpers;

internal static class ClassDeclarationBuilderFactory
{
    internal static ClassDeclarationBuilder CreateForMethod(MethodModel method, string name)
    {
        return new ClassDeclarationBuilder(
            name,
            TypeParameterListSyntax(method.TypeParameters)
        ).WithTypeParameterConstraintClauses(TypeParameterConstraintClauses(method.TypeParameters));
    }
}
