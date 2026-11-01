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

    internal readonly PropertyAccessorStateMetadata State;

    internal readonly AddReturnValueMethodMetadata AddReturnValueMethod;

    internal readonly GetMethodMetadata GetMethod;

    internal readonly NextReturnValueMethodMetadata NextReturnValueMethod;

    internal readonly MethodMetadata EnableBaseImplementationMethod;

    internal PropertyGetterImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata
    )
    {
        Name = "GetterImposterBuilder";
        TypeSyntax = SyntaxFactory.IdentifierName(Name);
        ReturnValuesField = new ReturnValuesFieldMetadata(property.ReturnHandlerType);
        CallbacksField = new CallbacksFieldMetadata();
        LastReturnValueField = new LastReturnValueFieldMetadata(property.ReturnHandlerType);
        InvocationCountField = new InvocationCountFieldMetadata();
        State = PropertyAccessorStateMetadata.Getter(defaultPropertyBehaviourMetadata);
        AddReturnValueMethod = new AddReturnValueMethodMetadata(property.ReturnHandlerType);
        GetMethod = new GetMethodMetadata(property);
        NextReturnValueMethod = new NextReturnValueMethodMetadata(property.ReturnHandlerType);
        EnableBaseImplementationMethod = new MethodMetadata(
            "EnableBaseImplementation",
            WellKnownTypes.Void
        );
    }
}
