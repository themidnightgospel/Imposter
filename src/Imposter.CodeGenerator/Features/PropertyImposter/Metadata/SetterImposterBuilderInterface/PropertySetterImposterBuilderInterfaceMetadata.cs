using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertySetterThenMethodMetadata = Imposter.CodeGenerator.Features.PropertyImposter.Metadata.Common.ThenMethodMetadata;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct PropertySetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly string FluentInterfaceName;

    internal readonly NameSyntax FluentInterfaceTypeSyntax;

    internal readonly string VerificationInterfaceName;

    internal readonly NameSyntax VerificationInterfaceTypeSyntax;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly PropertySetterThenMethodMetadata ThenMethod;

    internal PropertySetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertySetterBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        FluentInterfaceName = $"I{property.UniqueName}PropertySetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.ParseName(FluentInterfaceName);
        VerificationInterfaceName = $"I{property.UniqueName}PropertySetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.ParseName(VerificationInterfaceName);
        CalledMethod = new CalledMethodMetadata();
        CallbackMethod = new CallbackMethodMetadata(property, FluentInterfaceTypeSyntax, FluentInterfaceTypeSyntax);
        ThenMethod = new PropertySetterThenMethodMetadata(FluentInterfaceTypeSyntax, FluentInterfaceTypeSyntax);
    }
}
