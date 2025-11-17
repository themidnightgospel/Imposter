using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata;

internal readonly ref struct ImposterPropertyCoreMetadata
{
    internal readonly string Name;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly string UniqueName;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    internal readonly string DisplayName;

    internal readonly TypeSyntax AsSystemActionType;

    internal readonly TypeSyntax AsSystemFuncType;

    internal readonly TypeSyntax AsArgType;

    internal readonly bool GetterSupportsBaseImplementation;

    internal readonly bool SetterSupportsBaseImplementation;

    internal readonly bool SupportsBaseImplementation;

    internal ImposterPropertyCoreMetadata(IPropertySymbol property, string uniqueName)
    {
        UniqueName = uniqueName;
        HasGetter = property.GetMethod != null;
        HasSetter = property.SetMethod != null;
        Name = property.Name;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(NullableAwareTypeSyntax);
        AsSystemActionType = WellKnownTypes.System.ActionOfT(NullableAwareTypeSyntax);
        AsArgType = WellKnownTypes.Imposter.Abstractions.Arg(NullableAwareTypeSyntax);
        var containingType = property.ContainingType;
        var containingTypeIsClass = containingType?.TypeKind == TypeKind.Class;
        GetterSupportsBaseImplementation =
            containingTypeIsClass && property.GetMethod is { IsAbstract: false };
        SetterSupportsBaseImplementation =
            containingTypeIsClass && property.SetMethod is { IsAbstract: false };
        SupportsBaseImplementation =
            GetterSupportsBaseImplementation || SetterSupportsBaseImplementation;
        DisplayName =
            $"{containingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ?? property.Name}.{Name}";
    }
}
