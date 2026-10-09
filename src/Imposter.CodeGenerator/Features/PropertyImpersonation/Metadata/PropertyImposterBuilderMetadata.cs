using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

internal readonly struct PropertyImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax Syntax;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly FieldMetadata SetterImposterField;

    internal readonly FieldMetadata GetterImposterBuilderField;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly ParameterMetadata InvocationBehaviorParameter;

    internal PropertyImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata,
        in PropertySetterImposterMetadata setterImposter,
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder
    )
    {
        Name = $"{property.UniqueName}PropertyBuilder";
        Syntax = SyntaxFactory.IdentifierName(Name);
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        SetterImposterField = new FieldMetadata("_setterImposter", setterImposter.TypeSyntax);
        GetterImposterBuilderField = new FieldMetadata(
            "_getterImposterBuilder",
            getterImposterBuilder.TypeSyntax
        );
        InvocationBehaviorField = new FieldMetadata(
            "_invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        InvocationBehaviorParameter = new ParameterMetadata(
            "invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
    }
}
