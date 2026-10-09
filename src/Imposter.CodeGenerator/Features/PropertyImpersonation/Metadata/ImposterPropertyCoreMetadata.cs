using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
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

    internal ImposterPropertyCoreMetadata(PropertyModel property, string uniqueName)
    {
        UniqueName = uniqueName;
        HasGetter = property.Getter is not null;
        HasSetter = property.Setter is not null;
        IsInitOnly = property.Setter?.IsInitOnly == true;
        GetterModifiers = ImposterInstanceModifierBuilder.ForAccessor(property.Getter, property);
        SetterModifiers = ImposterInstanceModifierBuilder.ForAccessor(property.Setter, property);
        Name = property.Name;
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(property.Type);
        AsSystemFuncType = WellKnownTypes.System.FuncOfT(NullableAwareTypeSyntax);
        AsSystemActionType = WellKnownTypes.System.ActionOfT(NullableAwareTypeSyntax);
        AsArgType = WellKnownTypes.Imposter.Abstractions.Arg(NullableAwareTypeSyntax);
        GetterSupportsBaseImplementation =
            property.IsClassMember && property.Getter is { IsAbstract: false };
        SetterSupportsBaseImplementation =
            property.IsClassMember && property.Setter is { IsAbstract: false };
        SetterRequiresDirectBaseAssignment = IsInitOnly && SetterSupportsBaseImplementation;
        SupportsBaseImplementation =
            GetterSupportsBaseImplementation || SetterSupportsBaseImplementation;
        DisplayName = property.DisplayName;
    }
}
