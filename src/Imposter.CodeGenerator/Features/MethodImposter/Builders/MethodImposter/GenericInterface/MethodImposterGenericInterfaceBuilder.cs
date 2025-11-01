using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.GenericInterface;

internal static class MethodImposterGenericInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }

        var genericInterfaceType = method.MethodImposter.Interface;
        var invokeMethod = MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                "Invoke"
            )
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        var hasMatchingSetupMethod = MethodDeclaration(
            PredefinedType(Token(SyntaxKind.BoolKeyword)),
            "HasMatchingSetup"
        );

        if (method.Parameters.HasInputParameters)
        {
            hasMatchingSetupMethod = hasMatchingSetupMethod
                .AddParameterListParameters(
                    Parameter(Identifier("arguments")).WithType(method.Arguments.Syntax)
                );
        }

        hasMatchingSetupMethod = hasMatchingSetupMethod
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        return InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, genericInterfaceType.Name)
            .AddBaseType(SimpleBaseType(method.MethodImposter.Interface.Syntax))
            .AddMember(invokeMethod)
            .AddMember(hasMatchingSetupMethod)
            .Build(modifiers: TokenList(Token(SyntaxKind.InternalKeyword)));
    }
}