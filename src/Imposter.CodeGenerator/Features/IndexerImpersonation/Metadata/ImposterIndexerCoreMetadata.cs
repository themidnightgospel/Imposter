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

    // The keys a setup matches, which the arguments class keeps: all but the custom ref structs.
    internal readonly IndexerParameterMetadata[] MatchedParameters;

    // The custom ref struct keys, which only pass through: the delegates and the base accessors take them beside the
    // arguments class.
    internal readonly IndexerParameterMetadata[] PassedThroughParameters;

    // Their names, as the accessors' own parameters.
    internal readonly string[] PassedThroughKeyNames;

    internal readonly ParameterSyntax[] ParameterSyntaxes;

    internal readonly ArgumentSyntax[] ParameterArguments;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    internal readonly bool HasSpanValue;

    private readonly TypeModel? _dynamicValueType;

    // The type the imposter gets and sets the value as: the indexer's type, or the array that keeps a span's elements.
    internal readonly TypeSyntax NullableAwareStoredTypeSyntax;

    internal readonly IndexerDelegateMetadata Delegates;

    // A value passed through isn't kept: the default behaviour, the setter's history and Returns leave it out.
    internal readonly bool IsValuePassedThrough;

    // The default behaviour keeps the values set, and also falls back on the base getter, so a getter has it for a
    // value passed through too.
    internal readonly bool HasDefaultBehaviour;

    // Func<T>, Action and the getter's outcome types can't take a ref struct value or key, so with either the indexer
    // generates delegates of its own (see IndexerDelegateMetadata).
    internal readonly bool HasGeneratedValueDelegates;

    // Func<T>, or the generated base getter delegate.
    internal readonly TypeSyntax BaseGetterType;

    // Action, or the generated base setter delegate, which takes the ref struct keys and the value instead of
    // capturing them.
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
        _dynamicValueType = indexer.Type.IsDynamic ? indexer.Type : null;
        NullableAwareStoredTypeSyntax = indexer.Span is { } span
            ? SyntaxFactoryHelper.SpanElementsArrayType(span)
            : NullableAwareTypeSyntax;
        var fieldNames = new NameSet(
            indexer.Parameters.Select(parameter =>
                SyntaxFactoryHelper.EscapeKeyword(parameter.Name)
            )
        );
        Parameters = indexer
            .Parameters.Select(parameter => new IndexerParameterMetadata(parameter, fieldNames))
            .ToArray();
        MatchedParameters = Parameters.Where(parameter => !parameter.IsPassedThrough).ToArray();
        PassedThroughParameters = Parameters
            .Where(parameter => parameter.IsPassedThrough)
            .ToArray();
        PassedThroughKeyNames = PassedThroughParameters
            .Select(parameter => parameter.Name)
            .ToArray();
        Delegates = new IndexerDelegateMetadata(uniqueName);
        IsValuePassedThrough = indexer.IsPassedThrough;
        HasDefaultBehaviour = !IsValuePassedThrough || HasGetter;
        HasGeneratedValueDelegates = IsValuePassedThrough || PassedThroughParameters.Length > 0;
        BaseGetterType = HasGeneratedValueDelegates
            ? Delegates.BaseGetterDelegateType
            : WellKnownTypes.System.Func(NullableAwareStoredTypeSyntax);
        BaseSetterType = HasGeneratedValueDelegates
            ? Delegates.BaseSetterDelegateType
            : WellKnownTypes.System.Action;
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

    // The indexer's value as the imposter keeps it: a copy of a span's elements, the object a dynamic value is (see
    // SyntaxFactoryHelper.AsObject), or the value itself.
    internal ExpressionSyntax StoredValue(ExpressionSyntax value) =>
        HasSpanValue ? SyntaxFactoryHelper.SpanElementsCopy(value)
        : _dynamicValueType is { } dynamicValueType
            ? SyntaxFactoryHelper.AsObject(value, dynamicValueType)
        : value;

    internal NameSet CreateParameterNameSet() =>
        new(Parameters.Select(parameter => parameter.Name));

    // An accessor's optional base implementation, which the imposter calls when it's set up to use it.
    internal ParameterMetadata GetterBaseImplementationParameter(string name) =>
        new(name, BaseGetterType.ToNullableType(), SyntaxFactoryHelper.Null);

    internal ParameterMetadata SetterBaseImplementationParameter(string name) =>
        new(name, BaseSetterType.ToNullableType(), SyntaxFactoryHelper.Null);
}
