using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    internal const string MethodInvocationSetupTypeName = "MethodInvocationSetup";

    internal static ConstructorDeclarationSyntax Constructor(in ImposterTargetMethodMetadata method) =>
        ConstructorDeclaration(method.InvocationSetup.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                method.InputParameters.Count > 0
                    ? ParameterList(
                        SingletonSeparatedList(
                            Parameter(Identifier("argumentsCriteria"))
                                .WithType(method.ArgumentsCriteria.Syntax)
                        )
                    )
                    : ParameterList()
            )
            .WithBody(new BlockBuilder()
                .AddStatementsIf(method.InputParameters.Count > 0,
                    () => ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("ArgumentsCriteria"),
                            IdentifierName("argumentsCriteria"))))
                .AddStatement(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("_nextSetup"),
                            InvocationExpression(
                                IdentifierName(InvocationSetupType.GetOrAddMethodSetupMethodName),
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
                )
                .Build());

    private static ClassDeclarationSyntax GetInvocationSetup(in ImposterTargetMethodMetadata method)
    {
        return SyntaxFactoryHelper
            .ClassDeclarationBuilder(method.Symbol, method.InvocationSetup.Name)
            .AddBaseType(SimpleBaseType(method.InvocationSetup.Interface.Syntax))
            .AddMember(DefaultInstanceLazyInitializer(method))
            .AddMemberIf(method.InputParameters.Count > 0, () => SyntaxFactoryHelper.ArgumentsCriteriaProperty(method.ArgumentsCriteria.Syntax))
            .AddMember(CallSetupsFieldDeclaration)
            .AddMember(CurrentlySetupCallFieldDeclaration)
            .AddMember(GetMethodCallSetupDeclarationSyntax)
            .AddMember(DefaultResultGenerator(method))
            .AddMember(Constructor(method))
            .AddMemberIf(method.HasOutputParameters, () => SyntaxFactoryHelper.InitializeOutParametersWithDefaultsMethod(method.Symbol.Parameters))
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

    internal static (MemberDeclarationSyntax InvocationSetupBuilder, InterfaceDeclarationSyntax InvocationSetupBuilderInterface) Build(ImposterTargetMethodMetadata method)
    {
        var invocationSetupBuilderClass = GetInvocationSetup(method);
        return (invocationSetupBuilderClass, BuildInvocationSetupInterface(method, invocationSetupBuilderClass));
    }
}