using Imposter.CodeGenerator.Features.Shared;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Metadata;

internal readonly struct PropertyImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax Syntax;
    
    internal readonly FieldMetadata DefaultPropertyBehaviourField;
    
    internal readonly FieldMetadata SetterImposterField;
    
    internal readonly FieldMetadata GetterImposterBuilderField;

    internal PropertyImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        in FieldMetadata defaultPropertyBehaviourMetadata,
        in PropertySetterImposterMetadata setterImposter,
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder)
    {
        Name = $"{property.UniqueName}PropertyBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        DefaultPropertyBehaviourField = defaultPropertyBehaviourMetadata;
        SetterImposterField = new FieldMetadata("_setterImposter", setterImposter.TypeSyntax);
        GetterImposterBuilderField = new FieldMetadata("_getterImposterBuilder", getterImposterBuilder.TypeSyntax);
    }
}