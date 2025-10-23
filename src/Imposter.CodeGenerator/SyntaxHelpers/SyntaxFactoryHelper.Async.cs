using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static AwaitExpressionSyntax Await(this ExpressionSyntax expression)
        => AwaitExpression(expression);

    internal static ExpressionSyntax AwaitIf(this ExpressionSyntax expression, bool condition)
        => condition ? AwaitExpression(expression) : expression;
}