using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static readonly LiteralExpressionSyntax Default = LiteralExpression(SyntaxKind.DefaultLiteralExpression);

    internal static readonly ReturnStatementSyntax ReturnDefault = ReturnStatement(Default);

    internal static readonly ReturnStatementSyntax ReturnVoid = ReturnStatement();
}
