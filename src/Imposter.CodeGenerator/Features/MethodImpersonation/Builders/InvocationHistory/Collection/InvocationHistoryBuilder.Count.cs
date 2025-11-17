using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MethodDeclarationSyntax BuildCountMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(
            WellKnownTypes.Int,
            InvocationHistoryCollectionCountMethodMetadata.Name
        )
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(GetParameter(method))
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(
                                InvocationHistoryCollectionMetadata.InvocationHistoryCollectionFieldName
                            )
                            .Dot(IdentifierName("Count"))
                            .Call(
                                Parameter(Identifier("it"))
                                    .Lambda(
                                        It.Dot(
                                                method.Symbol.IsGenericMethod
                                                    ? GenericName(
                                                        Identifier(
                                                            InvocationHistoryMatchesMethodMetadata.Name
                                                        ),
                                                        method.GenericTypeArguments.ToTypeArguments()
                                                    )
                                                    : IdentifierName(
                                                        InvocationHistoryMatchesMethodMetadata.Name
                                                    )
                                            )
                                            .Call(
                                                GetMatchesMethodArguments(method)
                                                    ?.ToSingleArgumentList()
                                            )
                                    )
                                    .ToSingleArgumentList()
                            )
                    )
                )
            )
            .Build();

        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters
                ? ParameterSyntax(
                    method.ArgumentsCriteria.Syntax,
                    InvocationHistoryCollectionCountMethodMetadata.ArgumentsCriteriaParameterName
                )
                : null;
        }

        static ArgumentSyntax? GetMatchesMethodArguments(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? Argument(
                    IdentifierName(
                        InvocationHistoryCollectionCountMethodMetadata.ArgumentsCriteriaParameterName
                    )
                )
                : null;
    }
}
