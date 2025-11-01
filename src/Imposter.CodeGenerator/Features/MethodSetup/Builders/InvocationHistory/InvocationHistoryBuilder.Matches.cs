using Imposter.CodeGenerator.Features.MethodSetup.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    static MethodDeclarationSyntax BuildMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), InvocationHistoryTypeMetadata.MatchesMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddParameter(GetParameter(method))
            .WithBody(Block(ReturnStatement(CallMatchesMethod(method))))
            .Build();

        ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? ParameterSyntax(method.ArgumentsCriteria.SyntaxWithTargetGenericTypeArguments, InvocationHistoryTypeMetadata.MatchesMethod.ArgumentsCriteriaParameterName)
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

                var argumentsMatchCriteria = IdentifierName(InvocationHistoryTypeMetadata.MatchesMethod.ArgumentsCriteriaParameterName)
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

            return IdentifierName("criteria")
                .Dot(IdentifierName("Matches"))
                .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("Arguments")))));
        }
    }
}