using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.ImposterBuilderInterface;

internal readonly struct PropertyImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly GetterMethodMetadata GetterMethod;

    internal readonly SetterMethodMetadata SetterMethod;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal PropertyImposterBuilderInterfaceMetadata(
        in ImposterPropertyCoreMetadata property,
        in PropertySetterImposterBuilderInterfaceMetadata setterInterfaceMetadata,
        in PropertyGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata)
    {
        Name = $"I{property.UniqueName}PropertyBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        SetterMethod = new SetterMethodMetadata(property, setterInterfaceMetadata);
        GetterMethod = new GetterMethodMetadata(getterInterfaceMetadata);
        UseBaseImplementationMethod = property.SupportsBaseImplementation
            ? new UseBaseImplementationMethodMetadata(Syntax)
            : null;
    }
}
