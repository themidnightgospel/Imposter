using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetupBuilder;

internal static partial class InvocationSetupBuilder
{
    internal const string MethodInvocationSetupTypeName = "MethodInvocationSetup";

    internal static ConstructorDeclarationSyntax Constructor(ImposterTargetMethod method) =>
        ConstructorDeclaration(Identifier(method.InvocationsSetupBuilder))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                method.ParametersExceptOut.Count > 0
                    ? ParameterList(
                        SingletonSeparatedList(
                            Parameter(Identifier("argumentsCriteria"))
                                .WithType(IdentifierName(method.ArgumentsCriteriaClassName))
                        )
                    )
                    : ParameterList()
            )
            .WithBody(new BlockBuilder()
                .AddStatementsIf(method.ParametersExceptOut.Count > 0,
                    () => ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("ArgumentsCriteria"),
                            IdentifierName("argumentsCriteria"))))
                .Build());

    private static InterfaceDeclarationSyntax GetInvocationSetupBuilderInterface(ImposterTargetMethod method, ClassDeclarationSyntax invocationSetupBuilderClass)
    {
        // TODO use builder
        var invocationSetupBuilderInterface = InterfaceDeclaration(method.InvocationsSetupBuilderInterface)
            .WithModifiers(SyntaxTokenList.Create(Token(SyntaxKind.PublicKeyword)));

        foreach (var publicMethod in invocationSetupBuilderClass
                     .Members
                     .OfType<MethodDeclarationSyntax>()
                     .Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword) && m.Identifier.ValueText != "Invoke"))
        {
            invocationSetupBuilderInterface = invocationSetupBuilderInterface
                .AddMembers(publicMethod
                    .WithBody(null)
                    .WithReturnType(IdentifierName(method.InvocationsSetupBuilderInterface))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
        }

        return invocationSetupBuilderInterface;
    }

    private static ClassDeclarationSyntax GetInvocationSetupBuilder(ImposterTargetMethod method)
    {
        return new ClassDeclarationBuilder(method.InvocationsSetupBuilder)
            .AddMember(DefaultInstanceLazyInitializer(method))
            .AddMemberIf(method.ParametersExceptOut.Count > 0, () => SyntaxFactoryHelper.ArgumentsCriteriaProperty(method.ArgumentsCriteriaClassName))
            .AddMember(CallSetupsFieldDeclaration)
            .AddMember(CurrentlySetupCallFieldDeclaration)
            .AddMember(GetMethodCallSetupDeclarationSyntax)
            .AddMember(DefaultResultGenerator(method))
            .AddMember(Constructor(method))
            .AddMemberIf(method.HasOutParameters, () => SyntaxFactoryHelper.InitializeOutParametersWithDefaultsMethod(method.Symbol.Parameters))
            .AddMemberIf(method.HasReturnValue, () => ReturnsMethodDeclarationSyntax(method))
            .AddMemberIf(method.HasReturnValue, () => ReturnsValueMethodDeclarationSyntax(method))
            .AddMember(ThrowsTExceptionMethodDeclarationSyntax(method))
            .AddMember(ThrowsExceptionInstanceMethodDeclarationSyntax(method))
            .AddMember(ThrowsExceptionWithGeneratorMethodDeclarationSyntax(method))
            .AddMember(CallBeforeReturnMethodDeclarationSyntax(method))
            .AddMember(CallAfterReturnMethodDeclarationSyntax(method))
            .AddMember(NextMethodCallSetupFieldDeclaration)
            .AddMember(GetMethodDeclarationSyntax)
            .AddMember(InvokeMethodDeclarationSyntax(method))
            .AddMember(NestedMethodInvocationSetupType(method))
            .Build(baseList: BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(method.InvocationsSetupBuilderInterface)))))
            .WithLeadingTrivia(
                Comment(method.Comment),
                CarriageReturnLineFeed
            );
    }

    internal static (MemberDeclarationSyntax InvocationSetupBuilder, InterfaceDeclarationSyntax InvocationSetupBuilderInterface) Build(ImposterTargetMethod method)
    {
        var invocationSetupBuilderClass = GetInvocationSetupBuilder(method);
        return (invocationSetupBuilderClass, GetInvocationSetupBuilderInterface(method, invocationSetupBuilderClass));
    }
}