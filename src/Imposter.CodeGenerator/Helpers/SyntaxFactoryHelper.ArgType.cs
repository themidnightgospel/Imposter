using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Helpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax ArgType(IParameterSymbol parameter)
    {
        return GenericName(Identifier(parameter.RefKind == RefKind.Out ? "OutArg" : "Arg"))
            .WithTypeArgumentList(
                TypeArgumentList(SingletonSeparatedList(TypeSyntax(parameter.Type)))
            );
    }

    internal static PropertyDeclarationSyntax ArgArgumentsProperty(string argArgumentTypeName) =>
        PropertyDeclaration(IdentifierName(argArgumentTypeName), Identifier("ArgArguments"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

    internal static ParameterSyntax ArgParameter(IParameterSymbol parameter) =>
        Parameter(Identifier(parameter.Name)).WithType(ArgType(parameter));

    internal static ObjectCreationExpressionSyntax CreateArgArgumentsInstance(ImposterTargetMethod method)
        => ObjectCreationExpression(IdentifierName(method.ArgArgumentsClassName))
            .WithArgumentList(
                ArgumentList(
                    SeparatedList(
                        method.Symbol.Parameters.Select(p => Argument(IdentifierName(p.Name)))
                    )
                )
            );
}