using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventParameterMetadata
{
    internal readonly string Name;

    // The type the raise and handler-invocation histories keep this parameter as: the array of a span's elements, or
    // the object a dynamic is.
    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    // This parameter's value as the histories keep it: a copy of a span's elements, since the span can't be kept, or
    // the object a dynamic is.
    internal readonly ExpressionSyntax KeptValue;

    // This parameter by value, as the object a dynamic is (see SyntaxFactoryHelper.RuntimeValue).
    internal readonly ExpressionSyntax RuntimeValue;

    internal readonly ParameterSyntax ParameterSyntax;

    // Passes this parameter on to a member that declares it the same way: a handler, a callback or the sync raise. A
    // dynamic passes as the object it is.
    internal readonly ArgumentSyntax ForwardingArgument;

    internal readonly bool IsOut;

    // A ref struct the raise passes on to the callbacks and handlers, which the histories and Raised leave out.
    internal readonly bool IsPassedThrough;

    // This parameter's element in the raise and handler-invocation history tuples: the parameter name, unless C#
    // reserves it for tuple elements.
    internal readonly string TupleElementName;

    internal EventParameterMetadata(ParameterModel model, NameSet tupleElementNames)
    {
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.KeptTypeSyntaxIncludingNullable(model.Span, model.Type);
        ArgTypeSyntax = model.Span is { } span
            ? SyntaxFactoryHelper.SpanArgType(span)
            : WellKnownTypes.Imposter.Abstractions.Arg(
                SyntaxFactoryHelper.TypeSyntaxIncludingNullable(model.Type)
            );
        KeptValue = SyntaxFactoryHelper.KeptValue(IdentifierName(Name), model.Span, model.Type);
        RuntimeValue = SyntaxFactoryHelper.RuntimeValue(IdentifierName(Name), model.Type);
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntaxIncludingNullable(model);
        ForwardingArgument = SyntaxFactoryHelper.PassesAsObject(model)
            ? Argument(SyntaxFactoryHelper.AsObject(IdentifierName(Name), model.Type))
            : SyntaxFactoryHelper.ForwardingArgument(Name, model.RefKind);
        IsOut = model.RefKind == RefKind.Out;
        IsPassedThrough = model.IsPassedThrough;
        TupleElementName = TupleElementNames.IsReserved(Name) ? tupleElementNames.Use(Name) : Name;
    }
}
