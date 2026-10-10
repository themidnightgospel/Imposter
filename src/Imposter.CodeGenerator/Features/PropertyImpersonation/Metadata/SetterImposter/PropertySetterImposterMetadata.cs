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

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly SetMethodMetadata SetMethod;

    internal readonly PropertySetterImposterBuilderMetadata Builder;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly FieldMetadata PropertyDisplayNameField;

    internal readonly FieldMetadata HasConfiguredSetterField;

    internal readonly FieldMetadata UseBaseImplementationField;

    internal readonly ParameterMetadata InvocationBehaviorParameter;

    internal readonly ParameterMetadata PropertyDisplayNameParameter;

    internal readonly MethodMetadata UseBaseImplementationMethod;

    internal readonly MethodMetadata EnsureConfiguredMethod;

    internal readonly MethodMetadata MarkConfiguredMethod;

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
                    property.NullableAwareStoredTypeSyntax
                )
            );
        InvocationCountField = new FieldMetadata("_invocationCount", WellKnownTypes.Int);
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        CallbackMethod = new CallbackMethodMetadata(property);
        CalledMethod = new CalledMethodMetadata(property);
        SetMethod = new SetMethodMetadata(property);
        Builder = new PropertySetterImposterBuilderMetadata(property, TypeSyntax);
        InvocationBehaviorField = new FieldMetadata(
            "_invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        PropertyDisplayNameField = new FieldMetadata("_propertyDisplayName", WellKnownTypes.String);
        HasConfiguredSetterField = new FieldMetadata("_hasConfiguredSetter", WellKnownTypes.Bool);
        UseBaseImplementationField = new FieldMetadata(
            "_useBaseImplementation",
            WellKnownTypes.Bool
        );
        InvocationBehaviorParameter = new ParameterMetadata(
            "invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        PropertyDisplayNameParameter = new ParameterMetadata(
            "propertyDisplayName",
            WellKnownTypes.String
        );
        UseBaseImplementationMethod = new MethodMetadata(
            "UseBaseImplementation",
            WellKnownTypes.Void
        );
        EnsureConfiguredMethod = new MethodMetadata("EnsureSetterConfigured", WellKnownTypes.Void);
        MarkConfiguredMethod = new MethodMetadata("MarkConfigured", WellKnownTypes.Void);
    }
}
