using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax GetInvocationSetupsField(ImposterTargetMethod method)
    {
        var invocationSetupsFieldType = SyntaxFactoryHelper.List(IdentifierName(method.InvocationSetup));
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

    private static MemberDeclarationSyntax BuildMethodImposterBuilderInterface(ImposterTargetMethod method) =>
        new InterfaceDeclarationBuilder(method.MethodImposter.BuilderInterfaceName)
            .AddBaseType(SimpleBaseType(IdentifierName(method.InvocationsSetupInterface)))
            .AddBaseType(SimpleBaseType(IdentifierName(method.MethodInvocationVerifierInterfaceName)))
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)
                .WithLeadingTriviaComment(method.DisplayName)));

    private static ClassDeclarationSyntax BuildMethodImposter(ImposterTargetMethod method, InterfaceDeclarationSyntax invocationSetupBuilderInterface) =>
        new ClassDeclarationBuilder(method.MethodImposter.Name)
            .AddMember(GetInvocationSetupsField(method))
            .AddMember(GetInvocationHistoriesField(method))
            .AddMemberIf(method.HasOutParameters, () => SyntaxFactoryHelper.InitializeOutParametersWithDefaultsMethod(method.Symbol.Parameters))
            .AddMember(InvokeMethod(method))
            .AddMember(BuildMethodImposterBuilderClass(method, invocationSetupBuilderInterface))
            .Build()
            .WithLeadingTriviaComment(method.DisplayName);

    internal static IEnumerable<MemberDeclarationSyntax> Build(ImposterTargetMethod method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        yield return BuildMethodInvocationVerifierInterface(method);
        yield return BuildMethodImposterBuilderInterface(method);
        yield return BuildMethodImposter(method, invocationSetupBuilderInterface);
    }
}