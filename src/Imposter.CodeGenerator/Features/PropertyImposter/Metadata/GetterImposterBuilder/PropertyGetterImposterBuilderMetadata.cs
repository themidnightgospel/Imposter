using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

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

    internal PropertyGetterImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata
    )
    {
        Name = "GetterImposterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        var returnHandlerType = WellKnownTypes.System.Func(
            property.AsSystemFuncType.ToNullableType(),
            property.TypeSyntax
        );
        ReturnValuesField = new ReturnValuesFieldMetadata(returnHandlerType);
        CallbacksField = new CallbacksFieldMetadata();
        LastReturnValueField = new LastReturnValueFieldMetadata(returnHandlerType);
        InvocationCountField = new InvocationCountFieldMetadata();
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        AddReturnValueMethod = new AddReturnValueMethodMetadata(returnHandlerType);
        GetMethod = new GetMethodMetadata(property);
    }
}
