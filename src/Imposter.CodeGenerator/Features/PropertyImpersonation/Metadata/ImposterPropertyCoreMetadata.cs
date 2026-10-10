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

    private readonly SpanModel? _span;

    private readonly TypeModel _type;

    // The type the imposter gets and sets the value as: the property's type, or the array that keeps a span's elements.
    internal readonly TypeSyntax NullableAwareStoredTypeSyntax;

    // The type the setter passes a set value on and keeps it as: the stored type, or the object a dynamic is, so the
    // setter's own calls bind when the imposter compiles (see SyntaxFactoryHelper.KeptTypeSyntaxIncludingNullable).
    internal readonly TypeSyntax NullableAwareKeptTypeSyntax;

    internal readonly string DisplayName;

    internal readonly bool IsPassedThrough;

    // The default behaviour keeps the last value set for the getter to return: not without a getter, or for a value
    // passed through.
    internal readonly bool KeepsValue;

    internal readonly PropertyDelegateMetadata? Delegates;

    // Action<T>, or the generated setter callback delegate for a value passed through.
    internal readonly TypeSyntax SetterCallbackType;

    // Func<T>, or the generated value delegate for a value passed through.
    internal readonly TypeSyntax ValueGeneratorType;

    // The getter's outcomes, Func<Func<T>?, T> or the generated return handler delegate: each gets the base getter, if
    // there is one.
    internal readonly TypeSyntax ReturnHandlerType;

    // The setter's value criteria: none for a value passed through, which it can't match.
    internal readonly TypeSyntax? AsArgType;

    internal readonly ParameterMetadata? SetterCriteriaParameter;

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
        var span = property.Span;
        _span = span;
        _type = property.Type;
        NullableAwareStoredTypeSyntax = span is null
            ? NullableAwareTypeSyntax
            : SyntaxFactoryHelper.SpanElementsArrayType(span);
        NullableAwareKeptTypeSyntax = SyntaxFactoryHelper.KeptTypeSyntaxIncludingNullable(
            span,
            property.Type
        );
        IsPassedThrough = property.IsPassedThrough;
        KeepsValue = HasGetter && !IsPassedThrough;
        if (IsPassedThrough)
        {
            var delegates = new PropertyDelegateMetadata(uniqueName);
            Delegates = delegates;
            SetterCallbackType = delegates.SetterCallbackDelegateType;
            ValueGeneratorType = delegates.ValueDelegateType;
            ReturnHandlerType = delegates.ReturnHandlerDelegateType;
            AsArgType = null;
        }
        else
        {
            Delegates = null;
            SetterCallbackType = WellKnownTypes.System.ActionOfT(NullableAwareStoredTypeSyntax);
            ValueGeneratorType = WellKnownTypes.System.Func(NullableAwareStoredTypeSyntax);
            ReturnHandlerType = WellKnownTypes.System.Func(
                ValueGeneratorType.ToNullableType(),
                NullableAwareStoredTypeSyntax
            );
            AsArgType = span is null
                ? WellKnownTypes.Imposter.Abstractions.Arg(NullableAwareStoredTypeSyntax)
                : SyntaxFactoryHelper.SpanArgType(span);
        }

        SetterCriteriaParameter = AsArgType is null
            ? null
            : new ParameterMetadata("criteria", AsArgType);
        GetterSupportsBaseImplementation =
            property.IsClassMember && property.Getter is { IsAbstract: false };
        SetterSupportsBaseImplementation =
            property.IsClassMember && property.Setter is { IsAbstract: false };
        SetterRequiresDirectBaseAssignment = IsInitOnly && SetterSupportsBaseImplementation;
        SupportsBaseImplementation =
            GetterSupportsBaseImplementation || SetterSupportsBaseImplementation;
        DisplayName = property.DisplayName;
    }

    internal ExpressionSyntax KeptValue(ExpressionSyntax value) =>
        SyntaxFactoryHelper.KeptValue(value, _span, _type);
}
