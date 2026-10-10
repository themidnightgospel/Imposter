using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

// A span key is kept as the array of its elements, so the imposter's members take it as that array, by value.
internal readonly struct IndexerParameterMetadata
{
    internal readonly ParameterModel Model;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    // The type the arguments class keeps this parameter as: TypeSyntax, or the object a dynamic is.
    internal readonly TypeSyntax KeptTypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal readonly ParameterSyntax ParameterSyntax;

    // The field that keeps this parameter in the arguments and criteria classes: the parameter name, unless one of
    // those classes declares a member of that name.
    internal readonly string FieldName;

    // A custom ref struct key isn't kept or matched: it goes to the delegates and the base accessors as it is.
    internal bool IsPassedThrough => Model.IsPassedThrough;

    internal IndexerParameterMetadata(ParameterModel model, NameSet fieldNames)
    {
        Model = model;
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.StoredTypeSyntaxIncludingNullable(model);
        KeptTypeSyntax = SyntaxFactoryHelper.KeptTypeSyntaxIncludingNullable(
            model.Span,
            model.Type
        );
        ArgTypeSyntax = SyntaxFactoryHelper.ArgType(model);
        ParameterSyntax = model.Span is null
            ? SyntaxFactoryHelper.ParameterSyntaxIncludingNullable(model)
            : SyntaxFactoryHelper.ParameterSyntax(TypeSyntax, Name);
        FieldName = Name is "Equals" or "GetHashCode" or "Matches" ? fieldNames.Use(Name) : Name;
    }

    // Compares two arguments, so arguments class equality matches Arg<T>.Is, and SpanArg<T>.Is for a span key's
    // elements.
    internal ExpressionSyntax EqualityComparer =>
        (
            Model.Span is { } span
                ? WellKnownTypes.Imposter.Abstractions.SpanElementsComparer(
                    SyntaxFactoryHelper.TypeSyntaxIncludingNullable(span.ElementType)
                )
                : WellKnownTypes.System.Collections.Generic.EqualityComparer(KeptTypeSyntax)
        ).Dot(IdentifierName("Default"));

    // The indexer's argument as the imposter's members take it: a copy of a span's elements, or the argument itself.
    internal ArgumentSyntax ImposterArgument =>
        Model.Span is null
            ? ForwardingArgument(Name)
            : Argument(SyntaxFactoryHelper.SpanElementsCopy(IdentifierName(Name)));

    // Passes this parameter to the arguments class's constructor, which declares it the same way.
    internal ArgumentSyntax ConstructorArgument =>
        Model.Span is null
            ? SyntaxFactoryHelper.ArgumentSyntax(Model)
            : Argument(IdentifierName(Name));

    // Passes this parameter, or a copy of it, to a member that declares the same parameter. A span key's array
    // converts to the span by itself, and a dynamic key passes as an object (see SyntaxFactoryHelper.AsObject).
    internal ArgumentSyntax ForwardingArgument(string variableName) =>
        Model.Span is not null ? Argument(IdentifierName(variableName))
        : SyntaxFactoryHelper.PassesAsObject(Model)
            ? Argument(SyntaxFactoryHelper.AsObject(IdentifierName(variableName), Model.Type))
        : SyntaxFactoryHelper.ForwardingArgument(variableName, Model.RefKind);
}
