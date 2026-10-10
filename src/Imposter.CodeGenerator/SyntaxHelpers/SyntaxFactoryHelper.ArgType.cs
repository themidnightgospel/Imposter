using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
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
            return parameter.RefKind == RefKind.Out ? OutSpanArgType(span) : SpanArgType(span);
        }

        var parameterType = TypeSyntaxIncludingNullable(parameter.Type);

        return parameter.RefKind == RefKind.Out
            ? WellKnownTypes.Imposter.Abstractions.OutArg(parameterType)
            : WellKnownTypes.Imposter.Abstractions.Arg(parameterType);
    }

    internal static NameSyntax SpanArgType(SpanModel span)
    {
        var elementType = TypeSyntaxIncludingNullable(span.ElementType);

        return span.IsReadOnly
            ? WellKnownTypes.Imposter.Abstractions.ReadOnlySpanArg(elementType)
            : WellKnownTypes.Imposter.Abstractions.SpanArg(elementType);
    }

    private static NameSyntax OutSpanArgType(SpanModel span)
    {
        var elementType = TypeSyntaxIncludingNullable(span.ElementType);

        return span.IsReadOnly
            ? WellKnownTypes.Imposter.Abstractions.OutReadOnlySpanArg(elementType)
            : WellKnownTypes.Imposter.Abstractions.OutSpanArg(elementType);
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

    // A setup's matchers, which leave out the arguments the imposter only passes through.
    internal static ParameterListSyntax ArgParameters(IEnumerable<ParameterModel> parameters) =>
        ParameterList(
            SeparatedList(parameters.Where(it => !it.IsPassedThrough).Select(ArgParameter))
        );

    private static ParameterSyntax ArgParameter(ParameterModel parameter) =>
        ParameterSyntax(ArgType(parameter), EscapeKeyword(parameter.Name));
}
