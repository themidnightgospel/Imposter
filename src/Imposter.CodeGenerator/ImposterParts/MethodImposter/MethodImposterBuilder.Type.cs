using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax GetInvocationSetupsField(ImposterTargetMethod method)
    {
        var invocationSetupsFieldType = SyntaxFactoryHelper.List(IdentifierName(method.InvocationsSetupBuilder));
        return FieldDeclaration(
                VariableDeclaration(invocationSetupsFieldType)
                    .AddVariables(
                        VariableDeclarator(Identifier("_invocationSetups"))
                            .WithInitializer(
                                EqualsValueClause(ObjectCreationExpression(invocationSetupsFieldType).WithArgumentList(ArgumentList()))
                            )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword),
                Token(SyntaxKind.ReadOnlyKeyword)
            );
    }

    internal static FieldDeclarationSyntax GetInvocationHistoriesField(ImposterTargetMethod method)
    {
        var invocationHistoryFieldType = SyntaxFactoryHelper.List(IdentifierName(method.MethodInvocationHistoryClassName));

        return FieldDeclaration(
                VariableDeclaration(invocationHistoryFieldType)
                    .AddVariables(
                        VariableDeclarator(Identifier("_invocationHistory"))
                            .WithInitializer(
                                EqualsValueClause(ObjectCreationExpression(invocationHistoryFieldType).WithArgumentList(ArgumentList()))
                            )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword),
                Token(SyntaxKind.ReadOnlyKeyword)
            );
    }

    private static MemberDeclarationSyntax BuildMethodImposterBuilderInterface(ImposterTargetMethod method)
    {
        return InterfaceDeclaration(method.MethodImposter.BuilderInterfaceName)
            .WithBaseList(BaseList(SeparatedList<BaseTypeSyntax>(
                [
                    SimpleBaseType(IdentifierName(method.InvocationsSetupBuilderInterface)),
                    SimpleBaseType(IdentifierName(method.MethodInvocationVerifierInterfaceName))
                ]
            )))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithLeadingTrivia(
                Comment(method.Comment),
                CarriageReturnLineFeed
            );
    }

    private static ClassDeclarationSyntax BuildMethodImposter(ImposterTargetMethod method, InterfaceDeclarationSyntax invocationSetupBuilderInterface) =>
        new ClassDeclarationBuilder(method.MethodImposter.Name)
            .AddMember(GetInvocationSetupsField(method))
            .AddMember(GetInvocationHistoriesField(method))
            .AddMemberIf(method.HasOutParameters, () => SyntaxFactoryHelper.InitializeOutParametersWithDefaultsMethod(method.Symbol.Parameters))
            .AddMember(InvokeMethod(method))
            .AddMember(BuildMethodImposterBuilderClass(method, invocationSetupBuilderInterface))
            .Build()
            .WithLeadingTrivia(
                Comment(method.Comment),
                CarriageReturnLineFeed
            );

    internal static IEnumerable<MemberDeclarationSyntax> Build(ImposterTargetMethod method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        yield return BuildMethodInvocationVerifierInterface(method);
        yield return BuildMethodImposterBuilderInterface(method);
        yield return BuildMethodImposter(method, invocationSetupBuilderInterface);
    }
}