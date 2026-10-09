using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly ref struct ImposterIndexerCoreMetadata
{
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

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsSystemActionType;

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
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(NullableAwareStoredTypeSyntax);
        AsSystemActionType = WellKnownTypes.System.Action;
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
}
