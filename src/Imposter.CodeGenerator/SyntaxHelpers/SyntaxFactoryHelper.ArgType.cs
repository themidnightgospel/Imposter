using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax ArgType(IParameterSymbol parameter)
    {
        return parameter.RefKind == RefKind.Out
            ? WellKnownTypes.Imposter.Abstractions.OutArg(TypeSyntax(parameter.Type))
            : WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax(parameter.Type));
    }

    internal static PropertyDeclarationSyntax ArgumentsCriteriaProperty(TypeSyntax argArgumentTypeSyntax) =>
        PropertyDeclaration(argArgumentTypeSyntax, Identifier("ArgumentsCriteria"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

    internal static ArgumentSyntax ArgAnyArgument(IParameterSymbol parameter) =>
        Argument(ArgType(parameter).Dot(IdentifierName("Any")).Call());

    internal static InvocationExpressionSyntax OutArgAny(SyntaxNode type) =>
        WellKnownTypes.Imposter.Abstractions.OutArg(type).Dot(IdentifierName("Any")).Call();

    internal static InvocationExpressionSyntax ArgAny(TypeSyntax type) =>
        WellKnownTypes.Imposter.Abstractions.Arg(type).Dot(IdentifierName("Any")).Call();

    internal static ArgumentListSyntax ArgAnyArgumentList(IEnumerable<IParameterSymbol> parameter) => ArgumentListSyntax(SeparatedList(parameter.Select(ArgAnyArgument)));

    internal static ParameterListSyntax ArgParameters(IEnumerable<IParameterSymbol> parameters) =>
        ParameterList(SeparatedList(parameters.Select(ArgParameter)));

    internal static ParameterSyntax ArgParameter(IParameterSymbol parameter) =>
        Parameter(Identifier(parameter.Name)).WithType(ArgType(parameter));

    internal static ObjectCreationExpressionSyntax ArgumentCriteriaCreationExpression(ImposterTargetMethodMetadata method)
        => ObjectCreationExpression(method.ArgumentsCriteria.Syntax)
            .WithArgumentList(
                ArgumentListSyntax(
                    SeparatedList(
                        method.Symbol.Parameters.Select(p => Argument(IdentifierName(p.Name)))
                    )
                )
            );
}