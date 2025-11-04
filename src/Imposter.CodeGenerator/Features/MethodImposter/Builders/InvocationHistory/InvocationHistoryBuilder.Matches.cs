using Imposter.CodeGenerator.Features.MethodImposter.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    static MethodDeclarationSyntax BuildMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), InvocationHistoryMatchesMethodMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddParameter(GetParameter(method))
            .WithBody(Block(ReturnStatement(CallMatchesMethod(method))))
            .Build();

        ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? ParameterSyntax(method.ArgumentsCriteria.SyntaxWithTargetGenericTypeArguments, InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName)
                : null;

        static ExpressionSyntax CallMatchesMethod(in ImposterTargetMethodMetadata method)
        {
            if (method.Symbol.IsGenericMethod)
            {
                var genericArgumentsMatchCriteria = GenericArgumentsMatcherBuilder.GenerateExactMatchCriteria(method);

                if (!method.Parameters.HasInputParameters)
                {
                    return genericArgumentsMatchCriteria;
                }

                var argumentsMatchCriteria = IdentifierName(InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName)
                    .Dot(GenericName(Identifier(ArgumentCriteriaTypeMetadata.AsMethodMetadata.Name), method.GenericTypeArguments.AsTypeArguments()))
                    .Call()
                    .Dot(IdentifierName(ArgumentCriteriaTypeMetadata.MatchesMethodMetadata.Name))
                    .Call(IdentifierName("Arguments").ToSingleArgumentList());

                return genericArgumentsMatchCriteria.And(argumentsMatchCriteria);
            }

            if (!method.Parameters.HasInputParameters)
            {
                return True;
            }

            return IdentifierName(InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName)
                .Dot(IdentifierName("Matches"))
                .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("Arguments")))));
        }
    }
}
