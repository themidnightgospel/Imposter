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
    // type, the array a span argument is copied into, or the object a dynamic is.
    internal readonly TypeSyntax NullableAwareKeptTypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    // The matcher the imposter's own code builds for the parameter, of the type it keeps the argument as.
    internal readonly TypeSyntax KeptArgTypeSyntax;

    internal MethodParameterMetadata(ParameterModel model)
    {
        Model = model;
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(model.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(model.Type);
        NullableAwareKeptTypeSyntax = SyntaxFactoryHelper.KeptTypeSyntaxIncludingNullable(
            model.Span,
            model.Type
        );
        ArgTypeSyntax = SyntaxFactoryHelper.ArgType(model);
        KeptArgTypeSyntax = SyntaxFactoryHelper.KeptArgType(model);
    }

    internal bool IsSpan => Model.Span is not null;

    internal ExpressionSyntax KeptValue =>
        SyntaxFactoryHelper.KeptValue(IdentifierName(Name), Model.Span, Model.Type);
}
