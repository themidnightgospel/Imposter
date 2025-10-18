using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.NonGenericInterface_;

internal static class MethodImposterNonGenericInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (method.Symbol.IsGenericMethod)
        {
            return null;
        }
        
        return InterfaceDeclaration(method.MethodImposter.Interface.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddMembers(MethodDeclaration(
                    NullableType(method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments),
                    "As"
                )
                .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterListSyntax(method.TargetGenericTypeArguments))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
    }
}