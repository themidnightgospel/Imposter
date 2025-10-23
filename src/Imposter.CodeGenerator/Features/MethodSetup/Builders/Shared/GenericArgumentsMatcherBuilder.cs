using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.Shared;

internal static class GenericArgumentsMatcherBuilder
{
    // TODO we generate conditions for every parameter and return type irregardless of whether they contain generic or not.
    // For simplicity
    // TODO: Refactor
    internal static ExpressionSyntax GenerateCriteria(in ImposterTargetMethodMetadata method)
    {
        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");

        var conditions = new List<ExpressionSyntax>();

        foreach (var parameter in method.Symbol.Parameters)
        {
            var sourceTypeSyntax = TypeSyntax(parameter.Type);
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
            var targetTypeOf = TypeOfExpression(targetTypeSyntax);

            switch (parameter.RefKind)
            {
                // ref parameter are read/write, hence types must match
                case RefKind.Ref:
                    conditions.Add(BinaryExpression(SyntaxKind.EqualsExpression, targetTypeOf, sourceTypeOf));
                    break;
                // out parameters are write-only
                case RefKind.Out:
                    conditions.Add(sourceTypeOf.IsAssignableTo(targetTypeOf));
                    break;
                // in and normal parameters are read-only
                // RefKind.In is same as case RefKind.RefReadOnly:
                case RefKind.In:
                case RefKind.None:
                    // In and None
                    conditions.Add(targetTypeOf.IsAssignableTo(sourceTypeOf));
                    break;
            }
        }

        if (method.HasReturnValue)
        {
            var returnType = method.Symbol.ReturnType;

            var sourceTypeSyntax = TypeSyntax(returnType);
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            conditions
                .Add(TypeOfExpression(sourceTypeSyntax).IsAssignableTo(TypeOfExpression(targetTypeSyntax)));
        }

        return conditions.Any()
            ? conditions.Aggregate((current, next) => BinaryExpression(SyntaxKind.LogicalAndExpression, current, next))
            : LiteralExpression(SyntaxKind.TrueLiteralExpression);
    }

    // TODO duplicate of above
    internal static ExpressionSyntax GenerateExactMatchCriteria(in ImposterTargetMethodMetadata method)
    {
        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");

        var conditions = new List<ExpressionSyntax>();

        foreach (var parameter in method.Symbol.Parameters)
        {
            var sourceTypeSyntax = TypeSyntax(parameter.Type);
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            conditions.Add(BinaryExpression(SyntaxKind.EqualsExpression, TypeOfExpression(targetTypeSyntax), TypeOfExpression(sourceTypeSyntax)));
        }

        if (method.HasReturnValue)
        {
            var returnType = method.Symbol.ReturnType;

            var sourceTypeSyntax = TypeSyntax(returnType);
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            conditions.Add(BinaryExpression(SyntaxKind.EqualsExpression, TypeOfExpression(sourceTypeSyntax), TypeOfExpression(targetTypeSyntax)));
        }

        return conditions.Any()
            ? conditions.Aggregate((current, next) => current.And(next))
            : LiteralExpression(SyntaxKind.TrueLiteralExpression);
    }
}