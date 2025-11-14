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

    internal readonly string CallbackInterfaceName;

    internal readonly NameSyntax CallbackInterfaceTypeSyntax;

    internal readonly string ContinuationInterfaceName;

    internal readonly NameSyntax ContinuationInterfaceTypeSyntax;

    internal readonly string VerificationInterfaceName;

    internal readonly NameSyntax VerificationInterfaceTypeSyntax;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly PropertySetterThenMethodMetadata ThenMethod;

    internal readonly string? UseBaseImplementationEntryInterfaceName;

    internal readonly NameSyntax? UseBaseImplementationEntryInterfaceTypeSyntax;

    internal readonly SetterUseBaseImplementationMethodMetadata? UseBaseImplementationEntryMethod;

    internal readonly PropertySetterThenMethodMetadata? InitialThenMethod;

    internal PropertySetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertySetterBuilder";
        Syntax = SyntaxFactory.ParseName(Name);
        FluentInterfaceName = $"I{property.UniqueName}PropertySetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.ParseName(FluentInterfaceName);
        CallbackInterfaceName = $"I{property.UniqueName}PropertySetterCallbackBuilder";
        CallbackInterfaceTypeSyntax = SyntaxFactory.ParseName(CallbackInterfaceName);
        ContinuationInterfaceName = $"I{property.UniqueName}PropertySetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.ParseName(ContinuationInterfaceName);
        VerificationInterfaceName = $"I{property.UniqueName}PropertySetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.ParseName(VerificationInterfaceName);
        CalledMethod = new CalledMethodMetadata();
        CallbackMethod = new CallbackMethodMetadata(property, ContinuationInterfaceTypeSyntax, CallbackInterfaceTypeSyntax);
        if (property.SetterSupportsBaseImplementation)
        {
            UseBaseImplementationEntryInterfaceName = $"I{property.UniqueName}PropertySetterUseBaseImplementationBuilder";
            UseBaseImplementationEntryInterfaceTypeSyntax = SyntaxFactory.ParseName(UseBaseImplementationEntryInterfaceName);
            UseBaseImplementationEntryMethod = new SetterUseBaseImplementationMethodMetadata(
                UseBaseImplementationEntryInterfaceTypeSyntax,
                FluentInterfaceTypeSyntax);
            ThenMethod = new PropertySetterThenMethodMetadata(ContinuationInterfaceTypeSyntax, UseBaseImplementationEntryInterfaceTypeSyntax);
            // Setter builders already inherit the fluent chain via the use-base interface, so emitting
            // another Then() on the builder would only hide the inherited member.
            InitialThenMethod = null;
        }
        else
        {
            UseBaseImplementationEntryInterfaceName = null;
            UseBaseImplementationEntryInterfaceTypeSyntax = null;
            UseBaseImplementationEntryMethod = null;
            ThenMethod = new PropertySetterThenMethodMetadata(ContinuationInterfaceTypeSyntax, FluentInterfaceTypeSyntax);
            InitialThenMethod = null;
        }
    }
}
