using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter.Builder;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;

internal readonly struct PropertySetterImposterMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly CallbacksFieldMetadata CallbacksField;

    internal readonly FieldMetadata InvocationHistoryField;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly SetMethodMetadata SetMethod;

    internal readonly PropertySetterImposterBuilderMetadata Builder;

    public PropertySetterImposterMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata
    )
    {
        Name = "SetterImposter";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        CallbacksField = new CallbacksFieldMetadata(property);
        InvocationHistoryField = new FieldMetadata(
            "_invocationHistory",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(
                property.NullableAwareTypeSyntax
            )
        );
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        CallbackMethod = new CallbackMethodMetadata(property);
        CalledMethod = new CalledMethodMetadata(property);
        SetMethod = new SetMethodMetadata(property);
        Builder = new PropertySetterImposterBuilderMetadata(property, TypeSyntax);
    }
}
