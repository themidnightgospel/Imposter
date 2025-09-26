using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    internal const string MethodInvocationSetupTypeName = "MethodInvocationSetup";

    internal static ConstructorDeclarationSyntax Constructor(ImposterTargetMethod method) =>
        ConstructorDeclaration(Identifier(method.InvocationSetup))
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
                .AddStatement(ExpressionStatement(
                        InvocationExpression(
                            IdentifierName("GetMethodCallSetup"),
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        SimpleLambdaExpression(
                                            Parameter(Identifier("it")),
                                            LiteralExpression(SyntaxKind.TrueLiteralExpression)
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
                .Build());

    private static InterfaceDeclarationSyntax GetInvocationSetupBuilderInterface(ImposterTargetMethod method, ClassDeclarationSyntax invocationSetupBuilderClass) =>
        new InterfaceDeclarationBuilder(method.InvocationsSetupInterface)
            .AddMembers(invocationSetupBuilderClass
                .Members
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword) && m.Identifier.ValueText != "Invoke")
                .Select(publicMethod => publicMethod
                    .WithBody(null)
                    .WithReturnType(IdentifierName(method.InvocationsSetupInterface))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))))
            .Build(SyntaxTokenList.Create(Token(SyntaxKind.PublicKeyword)));

    private static ClassDeclarationSyntax GetInvocationSetup(ImposterTargetMethod method)
    {
        return new ClassDeclarationBuilder(method.InvocationSetup)
            .AddBaseType(SimpleBaseType(IdentifierName(method.InvocationsSetupInterface)))
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
            .Build()
            // TODO not good for performance
            .WithLeadingTriviaComment(method.DisplayName);
    }

    internal static (MemberDeclarationSyntax InvocationSetupBuilder, InterfaceDeclarationSyntax InvocationSetupBuilderInterface) Build(ImposterTargetMethod method)
    {
        var invocationSetupBuilderClass = GetInvocationSetup(method);
        return (invocationSetupBuilderClass, GetInvocationSetupBuilderInterface(method, invocationSetupBuilderClass));
    }
}