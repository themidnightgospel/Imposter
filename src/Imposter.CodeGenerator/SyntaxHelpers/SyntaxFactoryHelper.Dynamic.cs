using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

// dynamic is object at runtime. Generated code passes a dynamic value between its own members as an object: a dynamic
// argument would bind the call at runtime, which a base access, a method group or a lambda argument can't take, and a
// default literal argument has no type to bind to there.
internal static partial class SyntaxFactoryHelper
{
    // As object, or object? for a dynamic?, so the value keeps its nullability.
    internal static ExpressionSyntax AsObject(
        ExpressionSyntax dynamicValue,
        TypeModel dynamicType
    ) =>
        CastExpression(
            (TypeSyntax)
                new DynamicAsObjectRewriter().Visit(TypeSyntaxIncludingNullable(dynamicType)),
            dynamicValue
        );

    // typeof doesn't take dynamic (CS1962).
    internal static TypeOfExpressionSyntax RuntimeTypeOf(TypeSyntax type) =>
        TypeOfExpression((TypeSyntax)new DynamicAsObjectRewriter().Visit(type));

    // Writes the dynamic keyword as object. A type named dynamic is written qualified, so it keeps its name.
    private sealed class DynamicAsObjectRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node) =>
            node.Identifier.ValueText == "dynamic"
            && node.Parent is not (QualifiedNameSyntax or AliasQualifiedNameSyntax)
                ? PredefinedType(Token(SyntaxKind.ObjectKeyword)).WithTriviaFrom(node)
                : node;
    }
}
