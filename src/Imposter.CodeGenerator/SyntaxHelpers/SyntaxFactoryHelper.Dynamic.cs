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

    // A dynamic passed by value, or to an in parameter, which takes a value too, passes as an object. One passed by
    // ref or out stays dynamic.
    internal static bool PassesAsObject(ParameterModel parameter, bool includeRefKind = true) =>
        parameter.Type.IsDynamic
        && (!includeRefKind || parameter.RefKind is RefKind.None or RefKind.In);

    // A value as the imposter keeps it: a copy of a span's elements, the object a dynamic value is, or the value
    // itself.
    internal static ExpressionSyntax StoredValue(
        ExpressionSyntax value,
        SpanModel? span,
        TypeModel type
    ) =>
        span is not null ? SpanElementsCopy(value)
        : type.IsDynamic ? AsObject(value, type)
        : value;

    // typeof doesn't take dynamic (CS1962). Only a type the model says contains dynamic is rewritten: written in
    // syntax, a type parameter named dynamic looks the same.
    internal static TypeOfExpressionSyntax RuntimeTypeOf(TypeSyntax typeSyntax, TypeModel type) =>
        TypeOfExpression(
            type.ContainsDynamic
                ? (TypeSyntax)new DynamicAsObjectRewriter().Visit(typeSyntax)
                : typeSyntax
        );

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
