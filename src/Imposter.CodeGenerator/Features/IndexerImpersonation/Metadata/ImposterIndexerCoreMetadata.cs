using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly ref struct ImposterIndexerCoreMetadata
{
    internal const string BaseImplementationParameterName = "baseImplementation";

    internal readonly string UniqueName;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly string DisplayName;

    internal readonly IndexerParameterMetadata[] Parameters;

    internal readonly ParameterSyntax[] ParameterSyntaxes;

    internal readonly ArgumentSyntax[] ParameterArguments;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    internal readonly bool HasSpanValue;

    // The type the imposter gets and sets the value as: the indexer's type, or the array that keeps a span's elements.
    internal readonly TypeSyntax NullableAwareStoredTypeSyntax;

    internal readonly IndexerDelegateMetadata Delegates;

    // A value passed through isn't kept: the default behaviour, the setter's history and Returns leave it out.
    internal readonly bool IsPassedThrough;

    // The default behaviour keeps the values set, and also falls back on the base getter, so a getter has it for a
    // value passed through too.
    internal readonly bool HasDefaultBehaviour;

    // Func<T>, or the generated base getter delegate for a value passed through.
    internal readonly TypeSyntax ValueGeneratorType;

    // Action, or the generated base setter delegate, which takes a value passed through instead of capturing it.
    internal readonly TypeSyntax BaseSetterType;

    internal readonly bool GetterSupportsBaseImplementation;

    internal readonly bool SetterSupportsBaseImplementation;

    internal readonly SyntaxTokenList GetterModifiers;

    internal readonly SyntaxTokenList SetterModifiers;

    internal ImposterIndexerCoreMetadata(PropertyModel indexer, string uniqueName)
    {
        UniqueName = uniqueName;
        HasGetter = indexer.Getter is not null;
        HasSetter = indexer.Setter is not null;
        GetterModifiers = ImposterInstanceModifierBuilder.ForAccessor(indexer.Getter, indexer);
        SetterModifiers = ImposterInstanceModifierBuilder.ForAccessor(indexer.Setter, indexer);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(indexer.Type);
        HasSpanValue = indexer.Span is not null;
        NullableAwareStoredTypeSyntax = indexer.Span is { } span
            ? SyntaxFactoryHelper.SpanElementsArrayType(span)
            : NullableAwareTypeSyntax;
        Delegates = new IndexerDelegateMetadata(uniqueName);
        IsPassedThrough = indexer.IsPassedThrough;
        HasDefaultBehaviour = !IsPassedThrough || HasGetter;
        ValueGeneratorType = IsPassedThrough
            ? Delegates.BaseGetterDelegateType
            : WellKnownTypes.System.Func(NullableAwareStoredTypeSyntax);
        BaseSetterType = IsPassedThrough
            ? Delegates.BaseSetterDelegateType
            : WellKnownTypes.System.Action;
        var fieldNames = new NameSet(
            indexer.Parameters.Select(parameter =>
                SyntaxFactoryHelper.EscapeKeyword(parameter.Name)
            )
        );
        Parameters = indexer
            .Parameters.Select(parameter => new IndexerParameterMetadata(parameter, fieldNames))
            .ToArray();
        ParameterSyntaxes = Parameters.Select(parameter => parameter.ParameterSyntax).ToArray();
        ParameterArguments = Parameters
            .Select(parameter => parameter.ForwardingArgument(parameter.Name))
            .ToArray();
        GetterSupportsBaseImplementation =
            indexer.IsClassMember && indexer.Getter is { IsAbstract: false };
        SetterSupportsBaseImplementation =
            indexer.IsClassMember && indexer.Setter is { IsAbstract: false };
        DisplayName = indexer.DisplayName;
    }

    // The indexer's value as the imposter keeps it: a copy of a span's elements, or the value itself.
    internal ExpressionSyntax StoredValue(ExpressionSyntax value) =>
        HasSpanValue ? SyntaxFactoryHelper.SpanElementsCopy(value) : value;

    internal NameSet CreateParameterNameSet() =>
        new(Parameters.Select(parameter => parameter.Name));

    // An accessor's optional base implementation, which the imposter calls when it's set up to use it.
    internal ParameterMetadata GetterBaseImplementationParameter(string name) =>
        new(name, ValueGeneratorType.ToNullableType(), SyntaxFactoryHelper.Null);

    internal ParameterMetadata SetterBaseImplementationParameter(string name) =>
        new(name, BaseSetterType.ToNullableType(), SyntaxFactoryHelper.Null);
}
