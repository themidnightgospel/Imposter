using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal const string MethodInvocationSetupTypeName = "MethodInvocationSetup";

    internal static ConstructorDeclarationSyntax Constructor(ImposterTargetMethod method) =>
        ConstructorDeclaration(
                Identifier(method.InvocationsSetupBuilder))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                ParameterList(
                    SeparatedList(method
                        .Symbol
                        .Parameters
                        .Select(parameter => Parameter(Identifier(parameter.Name))
                            .WithType(SyntaxFactoryHelper.ArgType(parameter))))))
            .WithBody(Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("ArgArguments"),
                        SyntaxFactoryHelper.CreateArgArgumentsInstance(method)))));

    internal static void AddInvocationSetupBuilder(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var invocationsSetupBuilder = new ClassDeclarationBuilder(method.InvocationsSetupBuilder)
            .AddMember(Constructor(method))
            .AddMember(SyntaxFactoryHelper.ArgArgumentsProperty(method.ArgArgumentsClassName))
            .AddMember(CallSetupsFieldDeclaration)
            .AddMember(CurrentlySetupCallFieldDeclaration)
            .AddMember(GetMethodCallSetupDeclarationSyntax)
            .AddMemberIf(method.HasOutParameters, () => InitializeOutParametersWithDefaultsMethod(method))
            .AddMemberIf(method.HasReturnValue, () => ReturnsMethodDeclarationSyntax(method))
            .AddMemberIf(method.HasReturnValue, () => ReturnsValueMethodDeclarationSyntax(method))
            .AddMember(ThrowsTExceptionMethodDeclarationSyntax(method))
            .AddMember(ThrowsExceptionInstanceMethodDeclarationSyntax(method))
            .AddMember(ThrowsExceptionWithGeneratorMethodDeclarationSyntax(method))
            .AddMember(CallBeforeReturnMethodDeclarationSyntax(method))
            .AddMember(CallAfterReturnMethodDeclarationSyntax(method))
            .AddMember(NextMethodCallSetupFieldDeclaration)
            .AddMember(GetMethodDeclarationSyntax)
            .AddMember(InvokeMethodDeclarationSyntax(method.Symbol.Parameters, method.Symbol.ReturnType))
            .AddMember(NestedMethodInvocationSetupType(method));

        imposterGenerationContext.SourceBuilder.AppendLine(invocationsSetupBuilder.Build(
            modifiers: TokenList(Token(SyntaxKind.PublicKeyword))
        ).NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }
}