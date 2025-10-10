using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ExpressionSyntax CastExpressionSyntax(string varName, TypeSyntax fromType, TypeSyntax toType)
    {
        return InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                WellKnownTypes.Imposter.Abstractions.TypeCaster,
                GenericName("Cast").WithTypeArgumentList(TypeArgumentList(SeparatedList([fromType, toType])))
            ),
            ArgumentList(SingletonSeparatedList(Argument(IdentifierName(varName))))
        );
    }
}