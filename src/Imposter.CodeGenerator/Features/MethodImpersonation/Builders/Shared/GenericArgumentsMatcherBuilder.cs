using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;

internal static class GenericArgumentsMatcherBuilder
{
    internal static ExpressionSyntax GenerateExactMatchCriteria(
        in ImposterTargetMethodMetadata method
    )
    {
        var typeParamRenamer = new TypeParameterRenamer(
            method.Model.TypeParameters,
            method.TargetGenericTypeArguments
        );

        var conditions = new List<ExpressionSyntax>();

        foreach (var parameter in method.Parameters.AllParameterMetadata)
        {
            var sourceTypeSyntax = parameter.TypeSyntax;
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            conditions.Add(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    RuntimeTypeOf(targetTypeSyntax),
                    RuntimeTypeOf(sourceTypeSyntax)
                )
            );
        }

        if (method.HasReturnValue)
        {
            var sourceTypeSyntax = method.ReturnTypeSyntax;
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            conditions.Add(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    RuntimeTypeOf(sourceTypeSyntax),
                    RuntimeTypeOf(targetTypeSyntax)
                )
            );
        }

        return conditions.Count > 0
            ? conditions.Aggregate((current, next) => current.And(next))
            : True;
    }
}
