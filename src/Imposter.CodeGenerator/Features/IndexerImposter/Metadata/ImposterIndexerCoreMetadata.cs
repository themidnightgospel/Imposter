using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly ref struct ImposterIndexerCoreMetadata
{
    internal readonly string UniqueName;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly string DisplayName;

    internal readonly IndexerParameterMetadata[] Parameters;

    internal readonly NameSet ParameterNameSet;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsSystemActionType;

    internal readonly bool GetterSupportsBaseImplementation;

    internal readonly bool SetterSupportsBaseImplementation;

    internal ImposterIndexerCoreMetadata(IPropertySymbol property, string uniqueName)
    {
        UniqueName = uniqueName;
        HasGetter = property.GetMethod is not null;
        HasSetter = property.SetMethod is not null;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(TypeSyntax);
        AsSystemActionType = WellKnownTypes.System.Action;
        Parameters = property.Parameters.Select(parameter => new IndexerParameterMetadata(parameter)).ToArray();
        ParameterNameSet = new NameSet(Parameters.Select(parameter => parameter.Name));
        var containingType = property.ContainingType;
        var containingTypeIsClass = containingType?.TypeKind == TypeKind.Class;
        GetterSupportsBaseImplementation = containingTypeIsClass && property.GetMethod is { IsAbstract: false };
        SetterSupportsBaseImplementation = containingTypeIsClass && property.SetMethod is { IsAbstract: false };

        var parametersDisplay = string.Join(", ", Parameters.Select(p => p.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)));
        var containingTypeDisplay = containingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
            ?? property.ContainingSymbol?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
            ?? property.Name;
        DisplayName = $"{containingTypeDisplay}.this[{parametersDisplay}]";
    }
}
