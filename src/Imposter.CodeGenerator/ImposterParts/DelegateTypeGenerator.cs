using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.ImposterParts;

internal static class MethodDelegateTypeBuilder
{
    internal static IEnumerable<DelegateDeclarationSyntax> BuildDelegateTypeDeclarations(ImposterTargetMethod method)
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
            SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)));

    private static DelegateDeclarationSyntax GetExceptionGeneratorDelegateDeclaration(ImposterTargetMethod method) =>
        CreateDelegateDeclaration(
            method,
            method.ExceptionGeneratorDelegateName,
            WellKnownTypes.System.Exception);

    private static DelegateDeclarationSyntax CreateDelegateDeclaration(ImposterTargetMethod method, string delegateName, TypeSyntax returnType) =>
        SyntaxFactory.DelegateDeclaration(
                attributeLists: SyntaxFactory.List<AttributeListSyntax>(),
                modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                returnType: returnType,
                identifier: SyntaxFactory.Identifier(delegateName),
                typeParameterList: null,
                parameterList: SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters),
                constraintClauses: SyntaxFactory.List<TypeParameterConstraintClauseSyntax>())
            .WithLeadingTrivia(
                SyntaxFactory.Comment(method.Comment),
                SyntaxFactory.CarriageReturnLineFeed
            );
}