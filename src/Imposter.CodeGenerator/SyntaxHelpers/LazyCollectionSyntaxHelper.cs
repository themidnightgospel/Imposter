using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class LazyCollectionSyntaxHelper
{
    internal static FieldDeclarationSyntax Field(in FieldMetadata field) =>
        SingleVariableField(
            field.Type.ToNullableType(),
            field.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword))
        );

    internal static ExpressionSyntax CountOrZero(
        ExpressionSyntax history,
        ExpressionSyntax count
    ) =>
        ConditionalExpression(
            history.IsNotNull(),
            count,
            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
        );

    internal static StatementSyntax UnlessNull(CommonForEachStatementSyntax loop) =>
        IfStatement(loop.Expression.IsNotNull(), Block(loop));

    internal static InvocationExpressionSyntax EnsureInitialized(string name) =>
        WellKnownTypes
            .System.Threading.LazyInitializer.Dot(IdentifierName("EnsureInitialized"))
            .Call(RefArgument(name));
}
