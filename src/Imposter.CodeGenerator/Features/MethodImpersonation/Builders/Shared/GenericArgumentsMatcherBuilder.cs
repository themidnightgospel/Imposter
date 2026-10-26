using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;

internal static class GenericArgumentsMatcherBuilder
{
    // Compares the type arguments themselves: the same ones give the same parameter and return types, and comparing
    // those instead would miss a type parameter neither uses.
    internal static ExpressionSyntax GenerateExactMatchCriteria(
        in ImposterTargetMethodMetadata method
    ) =>
        method
            .TargetGenericTypeArguments.Zip(method.GenericTypeArguments, SameType)
            .Aggregate((current, next) => current.And(next));

    internal static ExpressionSyntax SameType(TypeSyntax targetType, TypeSyntax sourceType) =>
        BinaryExpression(
            SyntaxKind.EqualsExpression,
            TypeOfExpression(targetType),
            TypeOfExpression(sourceType)
        );

    // When the target type arguments are the method's own, `this` already is the target type: it's returned through a
    // cast instead of being converted. The JIT folds the typeof comparisons for value types.
    internal static IfStatementSyntax ReturnThisWhenSameTypeArguments(
        in ImposterTargetMethodMetadata method,
        TypeSyntax targetType
    ) =>
        IfStatement(
            GenerateExactMatchCriteria(method),
            ReturnStatement(
                CastExpression(targetType, CastExpression(WellKnownTypes.Object, ThisExpression()))
            )
        );
}
