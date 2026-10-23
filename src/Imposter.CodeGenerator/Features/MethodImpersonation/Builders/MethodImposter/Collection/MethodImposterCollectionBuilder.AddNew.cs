using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
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
    // A setup joins the newest imposter when that one has the same type arguments, so repeated setups of one
    // instantiation share an imposter. Only the newest is reused, which keeps every setup in newest-first order.
    private static MethodDeclarationSyntax BuildAddNewMethod(in ImposterTargetMethodMetadata method)
    {
        var localNames = new NameSet(
            method.Model.TypeParameters.Select(typeParameter => typeParameter.Name)
        );
        var imposter = IdentifierName(localNames.Use("imposter"));
        var newest = IdentifierName(localNames.Use("newest"));
        var sameTypeImposter = Identifier(localNames.Use("sameTypeImposter"));
        var methodBuilder = new MethodDeclarationBuilder(
            method.MethodImposter.Syntax,
            MethodImposterCollectionMetadata.AddNewMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .WithBody(
                Block(
                    IfStatement(
                        IdentifierName(MethodImposterCollectionMetadata.ImpostersFieldName)
                            .Dot(SetupCollectionSyntaxHelper.TryPeek)
                            .Call(OutVarArgument(newest.Identifier.Text))
                            .And(
                                IsPatternExpression(
                                    newest,
                                    DeclarationPattern(
                                        method.MethodImposter.Syntax,
                                        SingleVariableDesignation(sameTypeImposter)
                                    )
                                )
                            ),
                        ReturnStatement(IdentifierName(sameTypeImposter))
                    ),
                    LocalVariableDeclarationSyntax(
                        Var,
                        imposter.Identifier.Text,
                        NewMethodImposterExpression(method)
                    ),
                    IdentifierName(MethodImposterCollectionMetadata.ImpostersFieldName)
                        .Dot(SetupCollectionSyntaxHelper.Push)
                        .Call(Argument(imposter))
                        .ToStatementSyntax(),
                    ReturnStatement(imposter)
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
