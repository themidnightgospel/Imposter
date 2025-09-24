using System.Collections.Generic;
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

    internal static PropertyDeclarationSyntax ArgumentsCriteriaProperty(string argArgumentTypeName) =>
        PropertyDeclaration(IdentifierName(argArgumentTypeName), Identifier("ArgumentsCriteria"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

    internal static ArgumentSyntax ArgAnyArgument(IParameterSymbol parameter) =>
        Argument(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                ArgType(parameter),
                IdentifierName("Any")
            ));

    internal static ArgumentListSyntax ArgAnyArgumentList(IEnumerable<IParameterSymbol> parameter) => ArgumentList(SeparatedList(parameter.Select(ArgAnyArgument)));
    
    internal static ParameterListSyntax ArgParameters(IEnumerable<IParameterSymbol> parameters) =>
        ParameterList(SeparatedList(parameters.Select(ArgParameter)));

    internal static ParameterSyntax ArgParameter(IParameterSymbol parameter) =>
        Parameter(Identifier(parameter.Name)).WithType(ArgType(parameter));

    internal static ObjectCreationExpressionSyntax ArgumentCriteriaCreationExpression(ImposterTargetMethod method)
        => ObjectCreationExpression(IdentifierName(method.ArgumentsCriteriaClassName))
            .WithArgumentList(
                ArgumentList(
                    SeparatedList(
                        method.Symbol.Parameters.Select(p => Argument(IdentifierName(p.Name)))
                    )
                )
            );
}