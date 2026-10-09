using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MethodDeclarationSyntax BuildAddNewMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParameters = TypeParametersSyntax(method.Model.TypeParameters).ToArray();

        var methodBuilder = new MethodDeclarationBuilder(method.MethodImposter.Syntax, "AddNew")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithTypeParameters(
                typeParameters.Length > 0 ? TypeParameterList(SeparatedList(typeParameters)) : null
            )
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        "imposter",
                        NewMethodImposterExpression(method)
                    ),
                    IdentifierName("_imposters")
                        .Dot(ConcurrentStackSyntaxHelper.Push)
                        .Call(Argument(IdentifierName("imposter")).AsSingleArgumentListSyntax())
                        .ToStatementSyntax(),
                    ReturnStatement(IdentifierName("imposter"))
                )
            );

        return methodBuilder.Build();
    }

    private static ObjectCreationExpressionSyntax NewMethodImposterExpression(
        in ImposterTargetMethodMetadata method
    ) =>
        method.MethodImposter.Syntax.New(
            ArgumentList(
                SeparatedList<ArgumentSyntax>(
                    new SyntaxNodeOrToken[]
                    {
                        Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name)),
                        Token(SyntaxKind.CommaToken),
                        Argument(IdentifierName(method.MethodImposter.InvocationBehaviorFieldName)),
                    }
                )
            )
        );
}
