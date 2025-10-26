using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class TypeCasterSyntaxHelper
{
    internal static ExpressionSyntax CastExpression(string varName, TypeSyntax fromType, TypeSyntax toType)
    {
        return WellKnownTypes.Imposter.Abstractions.TypeCaster
            .Dot(GenericName("Cast").WithTypeArgumentList(TypeArgumentList(SeparatedList([fromType, toType]))))
            .Call(Argument(IdentifierName(varName)));
    }
}