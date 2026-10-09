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

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

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
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(indexer.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(indexer.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(TypeSyntax);
        AsSystemActionType = WellKnownTypes.System.Action;
        Parameters = indexer
            .Parameters.Select(parameter => new IndexerParameterMetadata(parameter))
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

    internal NameSet CreateParameterNameSet() =>
        new(Parameters.Select(parameter => parameter.Name));
}
