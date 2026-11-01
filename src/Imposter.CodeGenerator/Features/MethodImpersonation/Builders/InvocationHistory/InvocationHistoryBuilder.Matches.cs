using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    private static MethodDeclarationSyntax BuildMatchesMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            InvocationHistoryMatchesMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .AddParameter(InvocationHistoryInterfaceBuilder.MatchesParameter(method))
            .WithBody(Block(ReturnStatement(CallMatchesMethod(method))))
            .Build();

    private static ExpressionSyntax CallMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        ExpressionSyntax? argumentsMatchCriteria = method.Parameters.HasInputParameters
            ? Criteria(method)
                .Dot(IdentifierName(method.ArgumentsCriteria.MatchesCapturedMethodName))
                .Call(CapturedArgumentsBuilder.CapturedValues(method))
            : null;

        if (!method.Model.IsGenericMethod)
        {
            return argumentsMatchCriteria ?? True;
        }

        var genericArgumentsMatchCriteria =
            GenericArgumentsMatcherBuilder.GenerateExactMatchCriteria(method);

        return argumentsMatchCriteria is null
            ? genericArgumentsMatchCriteria
            : genericArgumentsMatchCriteria.And(argumentsMatchCriteria);
    }

    // A generic method's criteria are matched only once every type argument is equal, which proves this cast; no
    // criteria adapters or lambdas are needed.
    private static ExpressionSyntax Criteria(in ImposterTargetMethodMetadata method)
    {
        var criteria = IdentifierName(
            InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName
        );

        return method.Model.IsGenericMethod
            ? ParenthesizedExpression(
                CastExpression(
                    method.ArgumentsCriteria.Syntax,
                    CastExpression(WellKnownTypes.Object, criteria)
                )
            )
            : criteria;
    }
}
