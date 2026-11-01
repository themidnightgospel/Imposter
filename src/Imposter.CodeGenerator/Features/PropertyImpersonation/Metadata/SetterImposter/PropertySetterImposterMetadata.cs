using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter.Builder;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;

internal readonly struct PropertySetterImposterMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly CallbacksFieldMetadata CallbacksField;

    // The values set, which Called matches against its criteria. A value passed through can't be kept, so the setter
    // only counts the sets in InvocationCountField instead.
    internal readonly FieldMetadata? InvocationHistoryField;

    internal readonly FieldMetadata InvocationCountField;

    internal readonly PropertyAccessorStateMetadata State;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly SetMethodMetadata SetMethod;

    internal readonly PropertySetterImposterBuilderMetadata Builder;

    internal readonly FieldMetadata UseBaseImplementationField;

    internal readonly MethodMetadata UseBaseImplementationMethod;

    public PropertySetterImposterMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata
    )
    {
        Name = "SetterImposter";
        TypeSyntax = SyntaxFactory.IdentifierName(Name);
        CallbacksField = new CallbacksFieldMetadata(property);
        InvocationHistoryField = property.IsPassedThrough
            ? null
            : new FieldMetadata(
                "_invocationHistory",
                WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                    property.NullableAwareKeptTypeSyntax
                )
            );
        InvocationCountField = new FieldMetadata("_invocationCount", WellKnownTypes.Int);
        State = PropertyAccessorStateMetadata.Setter(defaultPropertyBehaviourMetadata);
        CallbackMethod = new CallbackMethodMetadata(property);
        CalledMethod = new CalledMethodMetadata(property);
        SetMethod = new SetMethodMetadata(property);
        Builder = new PropertySetterImposterBuilderMetadata(property, TypeSyntax);
        UseBaseImplementationField = new FieldMetadata(
            "_useBaseImplementation",
            WellKnownTypes.Bool
        );
        UseBaseImplementationMethod = new MethodMetadata(
            "UseBaseImplementation",
            WellKnownTypes.Void
        );
    }
}
