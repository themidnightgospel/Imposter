using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata;

internal readonly struct PropertyImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal PropertyImposterBuilderInterfaceMetadata(
        in ImposterPropertyCoreMetadata property,
        in PropertySetterImposterBuilderInterfaceMetadata setterInterfaceMetadata,
        in PropertyGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata)
    {
        Name = $"I{property.UniqueName}PropertyBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        SetterMethod = new SetterMethodMetadata(property, setterInterfaceMetadata);
        GetterMethod = new GetterMethodMetadata(getterInterfaceMetadata);
    }

    internal readonly GetterMethodMetadata GetterMethod;

    internal readonly SetterMethodMetadata SetterMethod;

    internal readonly struct SetterMethodMetadata
    {
        internal readonly string Name = "Setter";

        internal readonly TypeSyntax ReturnType;

        internal readonly ParameterMetadata CriteriaParameter;

        internal SetterMethodMetadata(in ImposterPropertyCoreMetadata property, in PropertySetterImposterBuilderInterfaceMetadata setterInterfaceMetadata)
        {
            ReturnType = setterInterfaceMetadata.Syntax;
            CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
        }
    }

    internal readonly struct GetterMethodMetadata
    {
        internal readonly string Name = "Getter";

        internal readonly TypeSyntax ReturnType;

        internal GetterMethodMetadata(in PropertyGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata)
        {
            ReturnType = getterInterfaceMetadata.TypeSyntax;
        }
    }
}