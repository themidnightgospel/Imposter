using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;

internal readonly struct PropertyGetterImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly ReturnValuesFieldMetadata ReturnValuesField;

    internal readonly CallbacksFieldMetadata CallbacksField;

    internal readonly LastReturnValueFieldMetadata LastReturnValueField;

    internal readonly InvocationCountFieldMetadata InvocationCountField;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly AddReturnValueMethodMetadata AddReturnValueMethod;

    internal readonly GetMethodMetadata GetMethod;

    internal readonly NextReturnValueMethodMetadata NextReturnValueMethod;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly FieldMetadata PropertyDisplayNameField;

    internal readonly FieldMetadata HasConfiguredReturnField;

    internal readonly ParameterMetadata InvocationBehaviorParameter;

    internal readonly ParameterMetadata PropertyDisplayNameParameter;

    internal readonly MethodMetadata EnableBaseImplementationMethod;

    internal readonly MethodMetadata EnsureConfiguredMethod;

    internal PropertyGetterImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata
    )
    {
        Name = "GetterImposterBuilder";
        TypeSyntax = SyntaxFactory.IdentifierName(Name);
        var returnHandlerType = WellKnownTypes.System.Func(
            property.AsSystemFuncType.ToNullableType(),
            property.NullableAwareStoredTypeSyntax
        );
        ReturnValuesField = new ReturnValuesFieldMetadata(returnHandlerType);
        CallbacksField = new CallbacksFieldMetadata();
        LastReturnValueField = new LastReturnValueFieldMetadata(returnHandlerType);
        InvocationCountField = new InvocationCountFieldMetadata();
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        AddReturnValueMethod = new AddReturnValueMethodMetadata(returnHandlerType);
        GetMethod = new GetMethodMetadata(property);
        NextReturnValueMethod = new NextReturnValueMethodMetadata(returnHandlerType);
        InvocationBehaviorField = new FieldMetadata(
            "_invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        PropertyDisplayNameField = new FieldMetadata("_propertyDisplayName", WellKnownTypes.String);
        HasConfiguredReturnField = new FieldMetadata("_hasConfiguredReturn", WellKnownTypes.Bool);
        InvocationBehaviorParameter = new ParameterMetadata(
            "invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        PropertyDisplayNameParameter = new ParameterMetadata(
            "propertyDisplayName",
            WellKnownTypes.String
        );
        EnableBaseImplementationMethod = new MethodMetadata(
            "EnableBaseImplementation",
            WellKnownTypes.Void
        );
        EnsureConfiguredMethod = new MethodMetadata("EnsureGetterConfigured", WellKnownTypes.Void);
    }
}
