using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter.Builder;

internal readonly struct PropertySetterImposterBuilderMetadata
{
    internal readonly string Name = "Builder";

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata SetterImposterField;

    internal readonly FieldMetadata? CriteriaField;
    internal readonly FieldMetadata SetupField;
    internal readonly FieldMetadata RuntimeCriteriaField;

    public PropertySetterImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        NameSyntax setterImposterTypeSyntax
    )
    {
        TypeSyntax = SyntaxFactory.QualifiedName(
            setterImposterTypeSyntax,
            SyntaxFactory.IdentifierName(Name)
        );
        SetterImposterField = new FieldMetadata("_setterImposter", setterImposterTypeSyntax);
        RuntimeCriteriaField = new FieldMetadata(
            "_runtimeCriteria",
            WellKnownTypes.Imposter.Abstractions.Arg(property.NullableAwareValueTypeSyntax)
        );
        SetupField = new FieldMetadata(
            "_setup",
            property.IsPassedThrough
                ? WellKnownTypes.Imposter.Abstractions.SetterSetup
                : WellKnownTypes.Imposter.Abstractions.PropertySetterSetup(
                    property.NullableAwareValueTypeSyntax
                )
        );
        CriteriaField = property.AsArgType is { } argType
            ? property.UsesSharedRuntime
                ? RuntimeCriteriaField
                : new FieldMetadata("_criteria", argType)
            : null;
    }
}
