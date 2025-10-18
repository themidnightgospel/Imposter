using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{

    private static IEnumerable<MemberDeclarationSyntax> ImplementInvocationSetupBuilderInterface(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        foreach (var methodDeclaration in invocationSetupBuilderInterface.Members.OfType<MethodDeclarationSyntax>())
        {
            yield return methodDeclaration
                // TODO refactor
                .WithModifiers(TokenList())
                .WithSemicolonToken(default)
                .WithConstraintClauses(List<TypeParameterConstraintClauseSyntax>())
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(
                    method.InvocationSetup.Interface.Syntax
                ))
                .WithBody(Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            IdentifierName("var"),
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("invocationSetup"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            InvocationExpression(IdentifierName("GetOrAddInvocationSetup"))
                                        )
                                    )
                            )
                        )
                    ),
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("invocationSetup"),
                                methodDeclaration.TypeParameterList?.Parameters.Count > 0
                                    ? GenericName(methodDeclaration.Identifier)
                                        .WithTypeArgumentList(TypeArgumentList(
                                            SeparatedList<TypeSyntax>(methodDeclaration.TypeParameterList.Parameters.Select(p => IdentifierName(p.Identifier.ValueText)))
                                        ))
                                    : IdentifierName(methodDeclaration.Identifier)
                            )
                        ).WithArgumentList(
                            ArgumentList(SeparatedList(methodDeclaration
                                .ParameterList
                                .Parameters
                                .Select(p => Argument(IdentifierName(p.Identifier.ValueText)))
                                .ToArray()))
                        )
                    ),
                    ReturnStatement(
                        IdentifierName("invocationSetup")
                    )
                ));
        }
    }

    internal static ClassDeclarationSyntax Build(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        var fields = GetFields(method).ToArray();

        return new ClassDeclarationBuilder(method.MethodImposter.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.MethodImposter.BuilderInterface.Syntax))
            .AddMembers(fields)
            .AddMember(SyntaxFactoryHelper
                .SingleVariableField(NullableType(method.InvocationSetup.Syntax), MethodImposterMetadata.ExistingInvocationSetupFieldName, TokenList(Token(SyntaxKind.PrivateKeyword))))
            .AddMember(SyntaxFactoryHelper.DeclareConstructorAndInitializeMembers(method.MethodImposter.Builder.Name, fields))
            .AddMember(GetOrAddInvocationSetupMethod(method))
            .AddMembers(ImplementInvocationSetupBuilderInterface(method, invocationSetupBuilderInterface))
            .AddMember(BuildCalledMethod(method))
            .Build();
    }
}