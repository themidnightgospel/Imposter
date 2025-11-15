using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.Delegates;

internal static class MethodDelegateTypeBuilder
{
    internal static DelegateDeclarationSyntax[] Build(in ImposterTargetMethodMetadata method) =>
        [
            GetMethodDelegateDeclaration(method),
            GetCallbackDelegateDeclaration(method),
            GetExceptionGeneratorDelegateDeclaration(method),
        ];

    private static DelegateDeclarationSyntax GetMethodDelegateDeclaration(
        in ImposterTargetMethodMetadata method
    ) =>
        CreateDelegateDeclaration(
            method,
            method.Delegate.Name,
            SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType)
        );

    private static DelegateDeclarationSyntax GetCallbackDelegateDeclaration(
        in ImposterTargetMethodMetadata method
    ) =>
        CreateDelegateDeclaration(
            method,
            method.CallbackDelegate.Name,
            method.IsAsync ? WellKnownTypes.System.Threading.Tasks.Task : WellKnownTypes.Void
        );

    private static DelegateDeclarationSyntax GetExceptionGeneratorDelegateDeclaration(
        in ImposterTargetMethodMetadata method
    ) =>
        CreateDelegateDeclaration(
            method,
            method.ExceptionGeneratorDelegate.Name,
            WellKnownTypes.System.Exception
        );

    private static DelegateDeclarationSyntax CreateDelegateDeclaration(
        in ImposterTargetMethodMetadata method,
        in string delegateName,
        in TypeSyntax returnType
    ) =>
        DelegateDeclaration(
                attributeLists: List<AttributeListSyntax>(),
                modifiers: TokenList(Token(SyntaxKind.PublicKeyword)),
                returnType: returnType,
                identifier: Identifier(delegateName),
                typeParameterList: method.GenericTypeArguments.Count > 0
                    ? SyntaxFactoryHelper.TypeParameterListSyntax(method.GenericTypeArguments)
                    : default,
                parameterList: SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters),
                constraintClauses: List<TypeParameterConstraintClauseSyntax>()
            )
            .WithLeadingTriviaComment(method.DisplayName);
}
