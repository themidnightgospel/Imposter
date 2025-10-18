using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MemberDeclarationSyntax BuildGetImposterWithMatchingSetup(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(method.MethodImposter.GenericInterface.Syntax, "GetImposterWithMatchingSetup")
            .AddParameterIf(method.HasInputParameters, () => Parameter(Identifier("arguments")).WithType(method.Arguments.Syntax))
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddTypeParameters(TypeParametersSyntax(method.Symbol).ToArray())
            .WithBody(Block(ReturnStatement(
                IdentifierName("_imposters")
                    .Dot(IdentifierName("Select"))
                    .Call(Identifier("it")
                        .GoesTo(IdentifierName("it")
                            .Dot(GenericName(Identifier("As"), method.GenericTypeArguments.AsTypeArguments()))
                            .Call())
                        .AsSingleArgumentList()
                    )
                    .Dot(IdentifierName("Where"))
                    .Call(
                        Identifier("it")
                            .GoesTo(IdentifierName("it").IsNotNull())
                            .AsSingleArgumentList())
                    .Dot(IdentifierName("Select"))
                    .Call(Identifier("it")
                        .GoesTo(PostfixUnaryExpression(
                                SyntaxKind.SuppressNullableWarningExpression,
                                IdentifierName("it")
                            )
                        )
                        .AsSingleArgumentList()
                    )
                    .Dot(IdentifierName("FirstOrDefault"))
                    .Call(
                        Identifier("it")
                            .GoesTo(
                                It
                                    .Dot(IdentifierName("HasMatchingSetup"))
                                    .Call(method.HasInputParameters ? Argument(IdentifierName("arguments")).AsSingleArgumentListSyntax() : EmptyArgumentListSyntax)
                            )
                            .AsSingleArgumentList()
                    )
                    .QuestionMarkQuestionMark(
                        GenericName(Identifier("AddNew"), method.GenericTypeArguments.AsTypeArguments())
                            .Call()
                    )
            )))
            .Build();
    }
}