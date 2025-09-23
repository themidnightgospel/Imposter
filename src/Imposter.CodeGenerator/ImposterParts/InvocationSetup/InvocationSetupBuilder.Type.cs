using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
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

    internal static ClassDeclarationSyntax GetInvocationSetupBuilder(ImposterTargetMethod method)
    {
        return new ClassDeclarationBuilder(method.InvocationsSetupBuilder)
            .AddMember(DefaultInstanceLazyInitializer(method))
            .AddMember(DefaultResultGenerator(method))
            .AddMember(Constructor(method))
            .AddMember(SyntaxFactoryHelper.ArgArgumentsProperty(method.ArgArgumentsClassName))
            .AddMember(CallSetupsFieldDeclaration)
            .AddMember(CurrentlySetupCallFieldDeclaration)
            .AddMember(GetMethodCallSetupDeclarationSyntax)
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
            .AddMember(InvokeMethodDeclarationSyntax(method.Symbol.Parameters, method.Symbol.ReturnType))
            .AddMember(NestedMethodInvocationSetupType(method))
            .Build();
    }
}