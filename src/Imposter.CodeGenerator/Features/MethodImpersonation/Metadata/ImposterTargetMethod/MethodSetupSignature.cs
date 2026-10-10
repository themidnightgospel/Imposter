using System.Linq;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

// What tells the setups of two methods with the same name apart: their arity and matcher parameter types. Type
// parameters compare by position, and reference nullability doesn't count, since overloads can't differ in it.
internal static class MethodSetupSignature
{
    internal static string Of(MethodModel method)
    {
        var positionalTypeParameters = new TypeParameterRenamer(
            method.TypeParameters,
            method
                .TypeParameters.Select((_, index) => (NameSyntax)IdentifierName("T" + index))
                .ToArray()
        );
        var parameterTypes = method
            .Parameters.Where(parameter => !parameter.IsPassedThrough)
            .Select(parameter =>
                positionalTypeParameters
                    .Visit(SyntaxFactoryHelper.ArgType(WithoutNullableAnnotations(parameter)))
                    .ToString()
            );

        return method.TypeParameters.Count + "(" + string.Join(",", parameterTypes) + ")";
    }

    private static ParameterModel WithoutNullableAnnotations(ParameterModel parameter) =>
        parameter with
        {
            Type = WithoutNullableAnnotations(parameter.Type),
            Span = parameter.Span is { } span
                ? span with
                {
                    ElementType = WithoutNullableAnnotations(span.ElementType),
                }
                : null,
        };

    private static TypeModel WithoutNullableAnnotations(TypeModel type) =>
        type with
        {
            FullyQualifiedNameIncludingNullable = type.FullyQualifiedName,
        };
}
