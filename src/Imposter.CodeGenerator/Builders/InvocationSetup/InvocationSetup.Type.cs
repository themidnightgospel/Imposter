using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal const string MethodInvocationSetupTypeName = "MethodInvocationSetup";

    internal static ConstructorDeclarationSyntax Constructor(in ImposterTargetMethodMetadata method) =>
        ConstructorDeclaration(method.InvocationSetup.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                method.Parameters.InputParameters.Count > 0
                    ? ParameterList(
                        SingletonSeparatedList(
                            Parameter(Identifier("argumentsCriteria"))
                                .WithType(method.ArgumentsCriteria.Syntax)
                        )
                    )
                    : ParameterList()
            )
            .WithBody(new BlockBuilder()
                .AddStatementsIf(method.Parameters.InputParameters.Count > 0,
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
                                IdentifierName(InvocationSetupMetadata.GetOrAddMethodSetupMethodName),
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

    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        return SyntaxFactoryHelper
                .ClassDeclarationBuilder(method.Symbol, method.InvocationSetup.Name)
                .AddBaseType(SimpleBaseType(method.InvocationSetup.Interface.Syntax))
                .AddMember(DefaultInstanceLazyInitializer(method))
                .AddMember(method.Parameters.HasInputParameters ? SyntaxFactoryHelper.ArgumentsCriteriaProperty(method.ArgumentsCriteria.Syntax) : null)
                .AddMember(CallSetupsFieldDeclaration)
                .AddMember(CurrentlySetupCallFieldDeclaration)
                .AddMember(GetMethodCallSetupDeclarationSyntax)
                .AddMember(DefaultResultGenerator(method))
                .AddMember(Constructor(method))
                .AddMember(SyntaxFactoryHelper.DeclareInitializeOutParametersWithDefaultsMethod(method))
                .AddMember(ReturnsMethodDeclarationSyntax(method))
                .AddMember(ReturnsValueMethodDeclarationSyntax(method))
                .AddMember(ThrowsTExceptionMethodDeclarationSyntax(method))
                .AddMember(ThrowsExceptionInstanceMethodDeclarationSyntax(method))
                .AddMember(ThrowsExceptionWithGeneratorMethodDeclarationSyntax(method))
                .AddMember(CallBeforeReturnMethodDeclarationSyntax(method))
                .AddMember(CallAfterReturnMethodDeclarationSyntax(method))
                .AddMember(NextMethodCallSetupFieldDeclaration)
                .AddMember(BuildGetNextSetupMethod(method))
                .AddMember(InvokeMethodDeclarationSyntax(method))
                .AddMember(NestedMethodInvocationSetupType(method))
                .Build()
#if DEBUG
                .WithLeadingTriviaComment(method.DisplayName)
#endif
            ;
    }
}