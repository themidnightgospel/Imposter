using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.ImposterParts;

internal class DelegateTypeGenerator
{
    internal static void AddMethodDelegate(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var delegateDeclaration = CreateDelegateDeclaration(method, method.DelegateName, SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType));

        imposterGenerationContext.SourceBuilder.AppendLine(delegateDeclaration.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

    internal static void AddCallbackDelegate(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var delegateDeclaration = CreateDelegateDeclaration(
            method,
            method.CallbackDelegateName,
            SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)));
        imposterGenerationContext.SourceBuilder.AppendLine(delegateDeclaration.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

    internal static void AddExceptionGeneratorDelegate(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var delegateDeclaration = CreateDelegateDeclaration(
            method,
            method.ExceptionGeneratorDelegateName,
            WellKnownTypes.System.Exception);

        imposterGenerationContext.SourceBuilder.AppendLine(delegateDeclaration.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

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