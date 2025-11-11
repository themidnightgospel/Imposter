using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MethodDeclarationSyntax BuildGetImposterWithMatchingSetup(in ImposterTargetMethodMetadata method)
    {
        var methodBuilder = new MethodDeclarationBuilder(method.MethodImposter.GenericInterface.Syntax, "GetImposterWithMatchingSetup")
            .AddParameter(GetParameter(method))
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddTypeParameters(TypeParametersSyntax(method.Symbol).ToArray())
            .WithBody(Block(ReturnStatement(
                IdentifierName("_imposters")
                    .Dot(IdentifierName("Select"))
                    .Call(Identifier("it")
                        .Lambda(IdentifierName("it")
                            .Dot(GenericName(Identifier("As"), method.GenericTypeArguments.ToTypeArguments()))
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
                        GenericName(Identifier("AddNew"), method.GenericTypeArguments.ToTypeArguments())
                            .Call()
                    )
            )));

        return methodBuilder.Build();
        
        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? Parameter(Identifier("arguments")).WithType(method.Arguments.Syntax)
                : null;
    }
}
