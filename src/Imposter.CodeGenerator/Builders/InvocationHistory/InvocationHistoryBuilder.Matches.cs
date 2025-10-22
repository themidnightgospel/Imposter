using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    static MemberDeclarationSyntax BuildMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), InvocationHistoryTypeMetadata.MatchesMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddParameter(GetParameter(method))
            .WithBody(Block(ReturnStatement(CallMatchesMethod(method))))
            .Build();

        ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? SyntaxFactoryHelper.ParameterSyntax(method.ArgumentsCriteria.SyntaxWithTargetGenericTypeArguments, InvocationHistoryTypeMetadata.MatchesMethod.ArgumentsCriteriaParameterName)
                : null;

        static ExpressionSyntax CallMatchesMethod(in ImposterTargetMethodMetadata method)
        {
            if (!method.Parameters.HasInputParameters)
            {
                return True;
            }

            if (method.Symbol.IsGenericMethod)
            {
                return IdentifierName(InvocationHistoryTypeMetadata.MatchesMethod.ArgumentsCriteriaParameterName)
                    .Dot(GenericName(Identifier(ArgumentCriteriaTypeMetadata.AsMethod.Name), method.GenericTypeArguments.AsTypeArguments()))
                    .Call()
                    .Dot(IdentifierName(ArgumentCriteriaTypeMetadata.MatchesMethod.Name))
                    .Call(IdentifierName("Arguments").ToSingleArgumentList());
            }

            return IdentifierName("criteria")
                .Dot(IdentifierName("Matches"))
                .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("Arguments")))));
        }
    }
}