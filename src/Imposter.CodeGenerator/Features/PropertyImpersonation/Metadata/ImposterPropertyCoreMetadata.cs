using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

internal readonly ref struct ImposterPropertyCoreMetadata
{
    internal readonly string Name;

    internal readonly bool HasGetter;

    internal readonly bool HasSetter;

    internal readonly bool IsInitOnly;

    internal readonly bool SetterRequiresDirectBaseAssignment;

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

    internal readonly SyntaxTokenList GetterModifiers;

    internal readonly SyntaxTokenList SetterModifiers;

    internal ImposterPropertyCoreMetadata(
        IPropertySymbol property,
        string uniqueName,
        MemberAccess memberAccess
    )
    {
        var getter = memberAccess.AccessibleOrNull(property.GetMethod);
        var setter = memberAccess.AccessibleOrNull(property.SetMethod);
        UniqueName = uniqueName;
        HasGetter = getter != null;
        HasSetter = setter != null;
        IsInitOnly = setter?.IsInitOnly == true;
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
        Name = property.Name;
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(property.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(NullableAwareTypeSyntax);
        AsSystemActionType = WellKnownTypes.System.ActionOfT(NullableAwareTypeSyntax);
        AsArgType = WellKnownTypes.Imposter.Abstractions.Arg(NullableAwareTypeSyntax);
        var containingType = property.ContainingType;
        var containingTypeIsClass = containingType?.TypeKind == TypeKind.Class;
        GetterSupportsBaseImplementation = containingTypeIsClass && getter is { IsAbstract: false };
        SetterSupportsBaseImplementation = containingTypeIsClass && setter is { IsAbstract: false };
        SetterRequiresDirectBaseAssignment = IsInitOnly && SetterSupportsBaseImplementation;
        SupportsBaseImplementation =
            GetterSupportsBaseImplementation || SetterSupportsBaseImplementation;
        DisplayName =
            $"{containingType?.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat) ?? property.Name}.{Name}";
    }
}
