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
    // Each span kind has a matcher type of its own, so a method overloaded on T[], Span<T> and ReadOnlySpan<T> keeps
    // distinct setups. An out span, like any out argument, has a wildcard matcher of its own.
    internal static TypeSyntax ArgType(ParameterModel parameter)
    {
        if (parameter.Span is { } span)
        {
            var elementType = TypeSyntaxIncludingNullable(span.ElementType);
            return (span.IsReadOnly, parameter.RefKind == RefKind.Out) switch
            {
                (true, true) => WellKnownTypes.Imposter.Abstractions.OutReadOnlySpanArg(
                    elementType
                ),
                (true, false) => WellKnownTypes.Imposter.Abstractions.ReadOnlySpanArg(elementType),
                (false, true) => WellKnownTypes.Imposter.Abstractions.OutSpanArg(elementType),
                (false, false) => WellKnownTypes.Imposter.Abstractions.SpanArg(elementType),
            };
        }

        var parameterType = TypeSyntaxIncludingNullable(parameter.Type);

        return parameter.RefKind == RefKind.Out
            ? WellKnownTypes.Imposter.Abstractions.OutArg(parameterType)
            : WellKnownTypes.Imposter.Abstractions.Arg(parameterType);
    }

    // A span argument is kept as an array of its elements.
    internal static TypeSyntax StoredTypeSyntaxIncludingNullable(ParameterModel parameter) =>
        parameter.Span is { } span
            ? SpanElementsArrayType(span)
            : TypeSyntaxIncludingNullable(parameter.Type);

    internal static ArrayTypeSyntax SpanElementsArrayType(SpanModel span) =>
        ArrayType(
            TypeSyntaxIncludingNullable(span.ElementType),
            SingletonList(ArrayRankSpecifier())
        );

    internal static ExpressionSyntax SpanElementsCopy(ExpressionSyntax span) =>
        span.Dot(IdentifierName("ToArray")).Call();

    internal static PropertyDeclarationSyntax ArgumentsCriteriaProperty(
        TypeSyntax argArgumentTypeSyntax
    ) =>
        PropertyDeclaration(argArgumentTypeSyntax, Identifier("ArgumentsCriteria"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            );

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
