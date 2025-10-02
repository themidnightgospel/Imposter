using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Builders.Imposter.ImposterTargetInstance;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Imposter;

internal static class ImposterBuilder
{
    // TODO this can also collide
    private const string ImposterInstanceFieldName = "_imposterInstance";

    internal static ClassDeclarationSyntax Build(ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = new ClassDeclarationBuilder(imposterGenerationContext.ImposterType.Name)
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))))
            .AddMembers(MethodImposterFields(imposterGenerationContext))
            .AddMembers(ImposterMethods(imposterGenerationContext));

        var imposterClassMemberUniqueName = new ClassMemberUniqueName(imposterBuilder.Members);
        var imposterTargetInstanceClassName = imposterClassMemberUniqueName.New("ImposterTargetInstance");

        return imposterBuilder
            .AddMember(ImposterInstanceField(imposterTargetInstanceClassName))
            .AddMembers(Constructor(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(ImposterTargetInstanceBuilder.Build(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(InstanceMethod(imposterGenerationContext, imposterTargetInstanceClassName))
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)));
    }

    private static MemberDeclarationSyntax InstanceMethod(ImposterGenerationContext imposterGenerationContext, string imposterTargetInstanceClassName)
    {
        return MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol),
                Identifier("Instance")
            )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))
                )
            )
            .WithBody(Block(
                ReturnStatement(IdentifierName(ImposterInstanceFieldName))
            ));
    }

    private static IEnumerable<MethodDeclarationSyntax> ImposterMethods(ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext.Methods
            .Select(imposterMethod => MethodDeclaration(
                    imposterMethod.MethodImposter.BuilderInterface.Syntax,
                    Identifier(imposterMethod.Symbol.Name)
                )
                .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(imposterMethod.Symbol))
                .WithParameterList(SyntaxFactoryHelper.ArgParameters(imposterMethod.Symbol.Parameters))
                .WithBody(Block(
                        ReturnStatement(
                            ObjectCreationExpression(
                                IdentifierName($"{imposterMethod.MethodImposter.Name}.Builder"),
                                new ArgumentListBuilder()
                                    .AddArgument(Argument(IdentifierName(imposterMethod.MethodImposter.AsField.Name)))
                                    .AddArgumentIf(imposterMethod.ParametersExceptOut.Count > 0, () => Argument(SyntaxFactoryHelper.ArgumentCriteriaCreationExpression(imposterMethod)))
                                    .Build(),
                                null
                            )
                        )
                    )
                )
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword))));

    private static IEnumerable<ConstructorDeclarationSyntax> Constructor(ImposterGenerationContext imposterGenerationContext, string imposterTargetInstanceClassName)
    {
        yield return ConstructorDeclaration(imposterGenerationContext.ImposterType.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithBody(new BlockBuilder()
                .AddStatements(
                    imposterGenerationContext
                        .Methods
                        .Select(method => ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        ThisExpression(),
                                        IdentifierName(method.MethodImposter.AsField.Name)
                                    ),
                                    ObjectCreationExpression(method.MethodImposter.Syntax)
                                        .WithArgumentList(ArgumentList())
                                )
                            )
                        )
                )
                .AddStatement(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            ThisExpression(),
                            IdentifierName(ImposterInstanceFieldName)
                        ),
                        ObjectCreationExpression(IdentifierName(imposterTargetInstanceClassName))
                            .WithArgumentList(ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        ThisExpression()
                                    )
                                )
                            ))
                    )
                ))
                .Build());
    }

    private static FieldDeclarationSyntax ImposterInstanceField(string imposterTargetInstanceClassName) =>
        FieldDeclaration(
                VariableDeclaration(IdentifierName(imposterTargetInstanceClassName))
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier(ImposterInstanceFieldName))
                        ))
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword)
            );

    private static IEnumerable<FieldDeclarationSyntax> MethodImposterFields(ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext.Methods.Select(method => FieldDeclaration(
                VariableDeclaration(method.MethodImposter.Syntax)
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier(method.MethodImposter.AsField.Name))
                        ))
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword)
            ));
}