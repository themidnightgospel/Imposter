using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static PropertyDeclarationSyntax ParameterAsArgProperty(IParameterSymbol parameter)
    {
        return PropertyDeclaration(ArgType(parameter), Identifier(parameter.Name))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
    }

    internal static FieldDeclarationSyntax ParameterAsReadonlyField(IParameterSymbol parameter) =>
        SingleVariableField(
            TypeSyntax(parameter.Type),
            parameter.Name,
            TokenList(Token(SyntaxKind.PublicKeyword)
            )
        );
}