using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class TypeCasterSyntaxHelper
{
    internal static ExpressionSyntax CastExpression(
        string varName,
        TypeSyntax fromType,
        TypeSyntax toType
    ) => CastExpression(IdentifierName(varName), fromType, toType);

    internal static ExpressionSyntax CastExpression(
        ExpressionSyntax value,
        TypeSyntax fromType,
        TypeSyntax toType
    ) =>
        WellKnownTypes
            .Imposter.Abstractions.TypeCaster.Dot(
                GenericName(
                    Identifier("Cast"),
                    SyntaxFactoryHelper.TypeArguments([fromType, toType])
                )
            )
            .Call(Argument(value));
}
