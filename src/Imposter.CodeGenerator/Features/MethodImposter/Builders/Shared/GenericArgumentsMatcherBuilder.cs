using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.Shared;

internal static class GenericArgumentsMatcherBuilder
{
    internal static ExpressionSyntax GenerateExactMatchCriteria(in ImposterTargetMethodMetadata method)
    {
        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, method.TargetGenericTypeArguments);

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

        return conditions.Count > 0
            ? conditions.Aggregate((current, next) => current.And(next))
            : True;
    }
}