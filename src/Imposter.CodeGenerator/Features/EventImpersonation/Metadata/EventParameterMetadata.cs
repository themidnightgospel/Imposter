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

    // The type the raise and handler-invocation histories keep this parameter as: the array of a span's elements.
    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    // This parameter's value as the histories keep it: a copy of a span's elements, since the span can't be kept.
    internal readonly ExpressionSyntax StoredValue;

    internal readonly ParameterSyntax ParameterSyntax;

    // Passes this parameter on to a member that declares it the same way: a handler, a callback or the sync raise.
    internal readonly ArgumentSyntax ForwardingArgument;

    internal readonly bool IsOut;

    // This parameter's element in the raise and handler-invocation history tuples: the parameter name, unless C#
    // reserves it for tuple elements.
    internal readonly string TupleElementName;

    internal EventParameterMetadata(ParameterModel model, NameSet tupleElementNames)
    {
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.StoredTypeSyntaxIncludingNullable(model);
        if (model.Span is { } span)
        {
            ArgTypeSyntax = SyntaxFactoryHelper.SpanArgType(span);
            StoredValue = SyntaxFactoryHelper.SpanElementsCopy(IdentifierName(Name));
        }
        else
        {
            ArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
            StoredValue = IdentifierName(Name);
        }
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntaxIncludingNullable(model);
        ForwardingArgument = SyntaxFactoryHelper.ForwardingArgument(Name, model.RefKind);
        IsOut = model.RefKind == RefKind.Out;
        TupleElementName = TupleElementNames.IsReserved(Name) ? tupleElementNames.Use(Name) : Name;
    }
}
