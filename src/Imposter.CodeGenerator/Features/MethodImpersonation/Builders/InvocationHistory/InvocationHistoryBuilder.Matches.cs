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
    static MethodDeclarationSyntax BuildMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            InvocationHistoryMatchesMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .AddParameter(GetParameter(method))
            .WithBody(Block(ReturnStatement(CallMatchesMethod(method))))
            .Build();

        ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? ParameterSyntax(
                    method.ArgumentsCriteria.SyntaxWithTargetGenericTypeArguments,
                    InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName
                )
                : null;

        static ExpressionSyntax CallMatchesMethod(in ImposterTargetMethodMetadata method)
        {
            if (method.Model.IsGenericMethod)
            {
                var genericArgumentsMatchCriteria =
                    GenericArgumentsMatcherBuilder.GenerateExactMatchCriteria(method);

                if (!method.Parameters.HasInputParameters)
                {
                    return genericArgumentsMatchCriteria;
                }

                // Equality of every type argument proves this cast; no criteria adapters or lambdas are needed.
                var argumentsMatchCriteria = ParenthesizedExpression(
                        CastExpression(
                            method.ArgumentsCriteria.Syntax,
                            CastExpression(
                                WellKnownTypes.Object,
                                IdentifierName(
                                    InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName
                                )
                            )
                        )
                    )
                    .Dot(IdentifierName(method.ArgumentsCriteria.MatchesCapturedMethodName))
                    .Call(CapturedArgumentsBuilder.Fields(method));

                return genericArgumentsMatchCriteria.And(argumentsMatchCriteria);
            }

            if (!method.Parameters.HasInputParameters)
            {
                return True;
            }

            return IdentifierName(
                    InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName
                )
                .Dot(IdentifierName(method.ArgumentsCriteria.MatchesCapturedMethodName))
                .Call(CapturedArgumentsBuilder.Fields(method));
        }
    }
}
