using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Delegates;

internal static class MethodDelegateTypeBuilder
{
    internal static DelegateDeclarationSyntax[] Build(in ImposterTargetMethodMetadata method) =>
    [
        GetMethodDelegateDeclaration(method),
        GetCallbackDelegateDeclaration(method),
        GetExceptionGeneratorDelegateDeclaration(method)
    ];

    private static DelegateDeclarationSyntax GetMethodDelegateDeclaration(ImposterTargetMethodMetadata method)
        => CreateDelegateDeclaration(method, method.Delegate.Name, SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType));

    private static DelegateDeclarationSyntax GetCallbackDelegateDeclaration(ImposterTargetMethodMetadata method) =>
        CreateDelegateDeclaration(
            method,
            method.CallbackDelegate.Name,
            PredefinedType(Token(SyntaxKind.VoidKeyword)));

    private static DelegateDeclarationSyntax GetExceptionGeneratorDelegateDeclaration(ImposterTargetMethodMetadata method) =>
        CreateDelegateDeclaration(
            method,
            method.ExceptionGeneratorDelegate.Name,
            WellKnownTypes.System.Exception);

    private static DelegateDeclarationSyntax CreateDelegateDeclaration(ImposterTargetMethodMetadata method, string delegateName, TypeSyntax returnType) =>
        DelegateDeclaration(
                attributeLists: List<AttributeListSyntax>(),
                modifiers: TokenList(Token(SyntaxKind.PublicKeyword)),
                returnType: returnType,
                identifier: Identifier(delegateName),
                typeParameterList: SyntaxFactoryHelper.TypeParameterList(method.Symbol),
                parameterList: SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters),
                constraintClauses: List<TypeParameterConstraintClauseSyntax>())
            .WithLeadingTriviaComment(method.DisplayName);
}