using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax ArgType(IParameterSymbol parameter)
    {
        var parameterType = TypeSyntaxIncludingNullable(parameter.Type);

        return parameter.RefKind == RefKind.Out
            ? WellKnownTypes.Imposter.Abstractions.OutArg(parameterType)
            : WellKnownTypes.Imposter.Abstractions.Arg(parameterType);
    }

    internal static PropertyDeclarationSyntax ArgumentsCriteriaProperty(
        TypeSyntax argArgumentTypeSyntax
    ) =>
        PropertyDeclaration(argArgumentTypeSyntax, Identifier("ArgumentsCriteria"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            );

    internal static InvocationExpressionSyntax OutArgAny(TypeSyntax type) =>
        WellKnownTypes.Imposter.Abstractions.OutArg(type).Dot(IdentifierName("Any")).Call();

    internal static InvocationExpressionSyntax ArgAny(TypeSyntax type) =>
        WellKnownTypes.Imposter.Abstractions.Arg(type).Dot(IdentifierName("Any")).Call();

    internal static ArgumentListSyntax ArgAnyArgumentList(
        IEnumerable<MethodParameterMetadata> parameters
    ) =>
        ArgumentListSyntax(
            SeparatedList(
                parameters.Select(parameter =>
                    Argument(parameter.ArgTypeSyntax.Dot(IdentifierName("Any")).Call())
                )
            )
        );

    internal static ParameterListSyntax ArgParameters(IEnumerable<IParameterSymbol> parameters) =>
        ParameterList(SeparatedList(parameters.Select(ArgParameter)));

    internal static ParameterListSyntax ArgParameters(
        IEnumerable<MethodParameterMetadata> parameters
    ) => ParameterList(SeparatedList(parameters.Select(parameter => ArgParameter(parameter))));

    internal static ParameterSyntax ArgParameter(IParameterSymbol parameter) =>
        ParameterSyntax(ArgType(parameter), EscapeKeyword(parameter.Name));

    internal static ParameterSyntax ArgParameter(in MethodParameterMetadata parameter) =>
        ParameterSyntax(parameter.ArgTypeSyntax, parameter.Name);

    internal static ObjectCreationExpressionSyntax NewArgumentsCriteria(
        in ImposterTargetMethodMetadata method
    ) =>
        method.ArgumentsCriteria.Syntax.New(
            ArgumentListSyntax(
                method.Symbol.Parameters.Select(p =>
                    Argument(IdentifierName(EscapeKeyword(p.Name)))
                )
            )
        );
}
