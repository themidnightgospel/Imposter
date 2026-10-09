using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly struct MethodParameterMetadata
{
    internal readonly ParameterModel Model;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    // The type the arguments, the criteria and the invocation history keep this parameter as: the parameter's own
    // type, or the array a span argument is copied into.
    internal readonly TypeSyntax NullableAwareStoredTypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal MethodParameterMetadata(ParameterModel model)
    {
        Model = model;
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(model.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(model.Type);
        NullableAwareStoredTypeSyntax = SyntaxFactoryHelper.StoredTypeSyntaxIncludingNullable(
            model
        );
        ArgTypeSyntax = SyntaxFactoryHelper.ArgType(model);
    }

    internal bool IsSpan => Model.Span is not null;

    // The parameter's value as the stored type: a copy of a span's elements, or the parameter itself.
    internal ExpressionSyntax StoredValue =>
        IsSpan ? SyntaxFactoryHelper.SpanElementsCopy(IdentifierName(Name)) : IdentifierName(Name);
}
