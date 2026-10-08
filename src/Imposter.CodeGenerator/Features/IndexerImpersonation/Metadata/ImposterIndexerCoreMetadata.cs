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

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsSystemActionType;

    internal readonly bool GetterSupportsBaseImplementation;

    internal readonly bool SetterSupportsBaseImplementation;

    internal readonly SyntaxTokenList GetterModifiers;

    internal readonly SyntaxTokenList SetterModifiers;

    internal ImposterIndexerCoreMetadata(
        IPropertySymbol property,
        string uniqueName,
        MemberAccess memberAccess
    )
    {
        var getter = memberAccess.AccessibleOrNull(property.GetMethod);
        var setter = memberAccess.AccessibleOrNull(property.SetMethod);
        UniqueName = uniqueName;
        HasGetter = getter is not null;
        HasSetter = setter is not null;
        GetterModifiers = ImposterInstanceModifierBuilder.ForAccessor(
            getter,
            property,
            memberAccess
        );
        SetterModifiers = ImposterInstanceModifierBuilder.ForAccessor(
            setter,
            property,
            memberAccess
        );
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(NullableAwareTypeSyntax);
        AsSystemActionType = WellKnownTypes.System.Action;
        Parameters = property
            .Parameters.Select(parameter => new IndexerParameterMetadata(
                ParameterModel.From(parameter)
            ))
            .ToArray();
        ParameterSyntaxes = Parameters.Select(parameter => parameter.ParameterSyntax).ToArray();
        ParameterArguments = Parameters
            .Select(parameter => parameter.ForwardingArgument(parameter.Name))
            .ToArray();
        var containingType = property.ContainingType;
        var containingTypeIsClass = containingType?.TypeKind == TypeKind.Class;
        GetterSupportsBaseImplementation = containingTypeIsClass && getter is { IsAbstract: false };
        SetterSupportsBaseImplementation = containingTypeIsClass && setter is { IsAbstract: false };

        var parametersDisplay = string.Join(
            ", ",
            property.Parameters.Select(parameter =>
                parameter.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
            )
        );
        var containingTypeDisplay =
            containingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
            ?? property.ContainingSymbol?.ToDisplayString(
                SymbolDisplayFormat.CSharpErrorMessageFormat
            )
            ?? property.Name;
        DisplayName = $"{containingTypeDisplay}.this[{parametersDisplay}]";
    }

    internal NameSet CreateParameterNameSet() =>
        new(Parameters.Select(parameter => parameter.Name));
}
