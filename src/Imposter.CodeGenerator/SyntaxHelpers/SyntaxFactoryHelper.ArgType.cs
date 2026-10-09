using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    // A span parameter is matched with SpanArg<T>, which has a type of its own, so a method overloaded on T[] and on
    // Span<T> keeps distinct setups.
    internal static TypeSyntax ArgType(ParameterModel parameter)
    {
        if (parameter.SpanElementType is { } elementType)
        {
            return WellKnownTypes.Imposter.Abstractions.SpanArg(
                TypeSyntaxIncludingNullable(elementType)
            );
        }

        var parameterType = TypeSyntaxIncludingNullable(parameter.Type);

        return parameter.RefKind == RefKind.Out
            ? WellKnownTypes.Imposter.Abstractions.OutArg(parameterType)
            : WellKnownTypes.Imposter.Abstractions.Arg(parameterType);
    }

    // A span argument is kept as an array of its elements.
    internal static TypeSyntax StoredTypeSyntaxIncludingNullable(ParameterModel parameter) =>
        parameter.SpanElementType is { } elementType
            ? ArrayType(
                TypeSyntaxIncludingNullable(elementType),
                SingletonList(ArrayRankSpecifier())
            )
            : TypeSyntaxIncludingNullable(parameter.Type);

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

    internal static ParameterListSyntax ArgParameters(IEnumerable<ParameterModel> parameters) =>
        ParameterList(SeparatedList(parameters.Select(ArgParameter)));

    internal static ParameterListSyntax ArgParameters(
        IEnumerable<MethodParameterMetadata> parameters
    ) => ParameterList(SeparatedList(parameters.Select(parameter => ArgParameter(parameter))));

    internal static ParameterSyntax ArgParameter(ParameterModel parameter) =>
        ParameterSyntax(ArgType(parameter), EscapeKeyword(parameter.Name));

    internal static ParameterSyntax ArgParameter(in MethodParameterMetadata parameter) =>
        ParameterSyntax(parameter.ArgTypeSyntax, parameter.Name);

    internal static ObjectCreationExpressionSyntax NewArgumentsCriteria(
        in ImposterTargetMethodMetadata method
    ) =>
        method.ArgumentsCriteria.Syntax.New(
            ArgumentListSyntax(
                method.Parameters.AllParameterMetadata.Select(parameter =>
                    Argument(IdentifierName(parameter.Name))
                )
            )
        );
}
