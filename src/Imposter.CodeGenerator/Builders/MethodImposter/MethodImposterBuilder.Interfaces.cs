using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    private static InterfaceDeclarationSyntax BuildNonGenericMethodImposterInterface(ImposterTargetMethodMetadata method)
    {
        return InterfaceDeclaration(method.MethodImposter.Interface.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddMembers(MethodDeclaration(
                    NullableType(method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments),
                    "As"
                )
                .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(method.TargetGenericTypeArguments))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
    }

    private static InterfaceDeclarationSyntax BuildGenericMethodImposterInterface(ImposterTargetMethodMetadata method)
    {
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
            )
            .AddParameterListParameters(
                Parameter(Identifier("arguments"))
                    .WithType(method.ArgumentsType.Syntax)
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        return SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, genericInterfaceType)
            .AddBaseType(SimpleBaseType(method.MethodImposter.Interface.Syntax))
            .AddMember(invokeMethod)
            .AddMember(hasMatchingSetupMethod)
            .Build(modifiers: TokenList(Token(SyntaxKind.InternalKeyword)));
    }
}