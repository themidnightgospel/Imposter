using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MethodDeclarationSyntax BuildCountMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.IntKeyword)), InvocationHistoryTypeMetadata.CollectionMetadata.CountMethodMetadata.Name)
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(GetParameter(method))
            .WithBody(Block(ReturnStatement(
                IdentifierName(InvocationHistoryTypeMetadata.CollectionMetadata.InvocationHistoryCollectionFieldName)
                    .Dot(IdentifierName("Count"))
                    .Call(Parameter(Identifier("it"))
                        .GoesTo(It
                            .Dot(method.Symbol.IsGenericMethod
                                ? GenericName(Identifier(InvocationHistoryTypeMetadata.MatchesMethod.Name), method.GenericTypeArguments.AsTypeArguments())
                                : IdentifierName(InvocationHistoryTypeMetadata.MatchesMethod.Name)
                            )
                            .Call(GetMatchesMethodArguments(method)?.AsSingleArgumentList()))
                        .AsSingleArgumentList()
                    ))))
            .Build();

        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters
                ? ParameterSyntax(method.ArgumentsCriteria.Syntax, InvocationHistoryTypeMetadata.CollectionMetadata.CountMethodMetadata.ArgumentsCriteriaParameterName)
                : null;
        }

        static ArgumentSyntax? GetMatchesMethodArguments(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? Argument(IdentifierName(InvocationHistoryTypeMetadata.CollectionMetadata.CountMethodMetadata.ArgumentsCriteriaParameterName))
                : null;
    }
}