using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MemberDeclarationSyntax BuildGetImposterWithMatchingSetup(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(method.MethodImposter.GenericInterface.Syntax, "GetImposterWithMatchingSetup")
            .AddParameter(GetParameter(method))
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddTypeParameters(TypeParametersSyntax(method.Symbol).ToArray())
            .WithBody(Block(ReturnStatement(
                IdentifierName("_imposters")
                    .Dot(IdentifierName("Select"))
                    .Call(Identifier("it")
                        .Lambda(IdentifierName("it")
                            .Dot(GenericName(Identifier("As"), method.GenericTypeArguments.AsTypeArguments()))
                            .Call())
                        .ToSingleArgumentList()
                    )
                    .Dot(IdentifierName("Where"))
                    .Call(
                        Identifier("it")
                            .Lambda(IdentifierName("it").IsNotNull())
                            .ToSingleArgumentList())
                    .Dot(IdentifierName("Select"))
                    .Call(Identifier("it")
                        .Lambda(PostfixUnaryExpression(
                                SyntaxKind.SuppressNullableWarningExpression,
                                IdentifierName("it")
                            )
                        )
                        .ToSingleArgumentList()
                    )
                    .Dot(IdentifierName("FirstOrDefault"))
                    .Call(
                        Identifier("it")
                            .Lambda(
                                It
                                    .Dot(IdentifierName("HasMatchingSetup"))
                                    .Call(method.Parameters.HasInputParameters ? Argument(IdentifierName("arguments")).AsSingleArgumentListSyntax() : EmptyArgumentListSyntax)
                            )
                            .ToSingleArgumentList()
                    )
                    .QuestionMarkQuestionMark(
                        GenericName(Identifier("AddNew"), method.GenericTypeArguments.AsTypeArguments())
                            .Call()
                    )
            )))
            .Build();
        
        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? Parameter(Identifier("arguments")).WithType(method.Arguments.Syntax)
                : null;
    }
}