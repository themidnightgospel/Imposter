using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Delegates;

internal static class MethodDelegateTypeBuilder
{
    internal static IEnumerable<DelegateDeclarationSyntax> Build(ImposterTargetMethod method)
    {
        yield return GetMethodDelegateDeclaration(method);
        yield return GetCallbackDelegateDeclaration(method);
        yield return GetExceptionGeneratorDelegateDeclaration(method);
    }

    private static DelegateDeclarationSyntax GetMethodDelegateDeclaration(ImposterTargetMethod method) => CreateDelegateDeclaration(method, method.DelegateName, SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType));

    private static DelegateDeclarationSyntax GetCallbackDelegateDeclaration(ImposterTargetMethod method) =>
        CreateDelegateDeclaration(
            method,
            method.CallbackDelegateName,
            PredefinedType(Token(SyntaxKind.VoidKeyword)));

    private static DelegateDeclarationSyntax GetExceptionGeneratorDelegateDeclaration(ImposterTargetMethod method) =>
        CreateDelegateDeclaration(
            method,
            method.ExceptionGeneratorDelegateName,
            WellKnownTypes.System.Exception);

    private static DelegateDeclarationSyntax CreateDelegateDeclaration(ImposterTargetMethod method, string delegateName, TypeSyntax returnType) =>
        DelegateDeclaration(
                attributeLists: List<AttributeListSyntax>(),
                modifiers: TokenList(Token(SyntaxKind.PublicKeyword)),
                returnType: returnType,
                identifier: Identifier(delegateName),
                typeParameterList: null,
                parameterList: SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters),
                constraintClauses: List<TypeParameterConstraintClauseSyntax>())
            .WithLeadingTriviaComment(method.DisplayName);
}