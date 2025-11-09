using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertySetterThenMethodMetadata = Imposter.CodeGenerator.Features.PropertyImposter.Metadata.Common.ThenMethodMetadata;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct PropertySetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly PropertySetterThenMethodMetadata ThenMethod;

    internal PropertySetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertySetterBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        CalledMethod = new CalledMethodMetadata();
        CallbackMethod = new CallbackMethodMetadata(property, Syntax);
        ThenMethod = new PropertySetterThenMethodMetadata(Syntax, Syntax);
    }
}
