using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static readonly IdentifierNameSyntax Var = IdentifierName("var");
    
    public static readonly IdentifierNameSyntax It = IdentifierName("it");
    
    public static readonly LiteralExpressionSyntax Null = LiteralExpression(SyntaxKind.DefaultLiteralExpression);
    
    public static readonly LiteralExpressionSyntax True = LiteralExpression(SyntaxKind.TrueLiteralExpression);
}