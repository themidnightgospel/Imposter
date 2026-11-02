using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;

internal readonly struct PropertyGetterImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly FieldMetadata ReturnValuesField;

    internal readonly FieldMetadata CallbacksField;

    internal readonly FieldMetadata LastReturnValueField;

    internal readonly FieldMetadata InvocationCountField;

    internal readonly PropertyAccessorStateMetadata State;

    internal readonly MethodMetadata AddReturnValueMethod;

    internal readonly ParameterMetadata AddReturnValueParameter;

    internal readonly GetMethodMetadata GetMethod;

    internal readonly NextReturnValueMethodMetadata NextReturnValueMethod;

    internal readonly MethodMetadata UseBaseImplementationMethod;

    internal PropertyGetterImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata
    )
    {
        Name = "GetterImposterBuilder";
        TypeSyntax = SyntaxFactory.IdentifierName(Name);
        ReturnValuesField = new FieldMetadata(
            "_returnValues",
            WellKnownTypes.Imposter.Abstractions.ReturnValueSequence(property.ReturnHandlerType)
        );
        CallbacksField = new FieldMetadata(
            "_callbacks",
            WellKnownTypes.Imposter.Abstractions.CallbackCollection(WellKnownTypes.System.Action)
        );
        LastReturnValueField = new FieldMetadata("_lastReturnValue", property.ReturnHandlerType);
        InvocationCountField = new FieldMetadata("_invocationCount", WellKnownTypes.Int);
        State = PropertyAccessorStateMetadata.Getter(defaultPropertyBehaviourMetadata);
        AddReturnValueMethod = new MethodMetadata("AddReturnValue", WellKnownTypes.Void);
        AddReturnValueParameter = new ParameterMetadata(
            "valueGenerator",
            property.ReturnHandlerType
        );
        GetMethod = new GetMethodMetadata(property);
        NextReturnValueMethod = new NextReturnValueMethodMetadata(property.ReturnHandlerType);
        UseBaseImplementationMethod = new MethodMetadata(
            "UseBaseImplementation",
            WellKnownTypes.Void
        );
    }
}
