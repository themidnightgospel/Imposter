using Imposter.CodeGenerator.SyntaxHelpers;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

// What the getter and the setter each keep the same way: the default behaviour they share, the mode, the display name
// and whether a setup configured the accessor.
internal readonly struct PropertyAccessorStateMetadata
{
    internal static readonly FieldMetadata InvocationBehaviorField = new(
        "_invocationBehavior",
        WellKnownTypes.Imposter.Abstractions.ImposterMode
    );

    internal static readonly ParameterMetadata InvocationBehaviorParameter = new(
        "invocationBehavior",
        WellKnownTypes.Imposter.Abstractions.ImposterMode
    );

    internal static readonly FieldMetadata PropertyDisplayNameField = new(
        "_propertyDisplayName",
        WellKnownTypes.String
    );

    internal static readonly ParameterMetadata PropertyDisplayNameParameter = new(
        "propertyDisplayName",
        WellKnownTypes.String
    );

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly FieldMetadata HasConfiguredField;

    internal readonly string EnsureConfiguredMethodName;

    // Follows the display name in the message of a missing imposter.
    internal readonly string MissingImposterSuffix;

    private PropertyAccessorStateMetadata(
        in FieldMetadata defaultPropertyBehaviourField,
        string hasConfiguredFieldName,
        string ensureConfiguredMethodName,
        string missingImposterSuffix
    )
    {
        DefaultPropertyBehaviourField = defaultPropertyBehaviourField;
        HasConfiguredField = new FieldMetadata(hasConfiguredFieldName, WellKnownTypes.Bool);
        EnsureConfiguredMethodName = ensureConfiguredMethodName;
        MissingImposterSuffix = missingImposterSuffix;
    }

    internal static PropertyAccessorStateMetadata Getter(
        in FieldMetadata defaultPropertyBehaviourField
    ) =>
        new(
            defaultPropertyBehaviourField,
            "_hasConfiguredReturn",
            "EnsureGetterConfigured",
            " (getter)"
        );

    internal static PropertyAccessorStateMetadata Setter(
        in FieldMetadata defaultPropertyBehaviourField
    ) =>
        new(
            defaultPropertyBehaviourField,
            "_hasConfiguredSetter",
            "EnsureSetterConfigured",
            " (setter)"
        );
}
