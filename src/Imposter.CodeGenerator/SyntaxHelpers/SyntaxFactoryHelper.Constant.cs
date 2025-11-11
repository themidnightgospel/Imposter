using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal partial class SyntaxFactoryHelper
{
    public static readonly LiteralExpressionSyntax True = LiteralExpression(SyntaxKind.TrueLiteralExpression);
    
    public static readonly LiteralExpressionSyntax False = LiteralExpression(SyntaxKind.FalseLiteralExpression);
}