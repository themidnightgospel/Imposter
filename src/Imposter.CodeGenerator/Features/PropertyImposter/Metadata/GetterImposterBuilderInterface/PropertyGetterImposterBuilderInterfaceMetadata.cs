using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertyGetterThenMethodMetadata = Imposter.CodeGenerator.Features.PropertyImposter.Metadata.Common.ThenMethodMetadata;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct PropertyGetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string OutcomeInterfaceName;

    internal readonly NameSyntax OutcomeInterfaceTypeSyntax;

    internal readonly string ContinuationInterfaceName;

    internal readonly NameSyntax ContinuationInterfaceTypeSyntax;

    internal readonly string VerificationInterfaceName;

    internal readonly NameSyntax VerificationInterfaceTypeSyntax;

    internal readonly string FluentInterfaceName;

    internal readonly NameSyntax FluentInterfaceTypeSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly PropertyGetterThenMethodMetadata ThenMethod;

    internal PropertyGetterImposterBuilderInterfaceMetadata(in ImposterPropertyCoreMetadata property)
    {
        Name = $"I{property.UniqueName}PropertyGetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        OutcomeInterfaceName = $"I{property.UniqueName}PropertyGetterOutcomeBuilder";
        OutcomeInterfaceTypeSyntax = SyntaxFactory.ParseName(OutcomeInterfaceName);
        ContinuationInterfaceName = $"I{property.UniqueName}PropertyGetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.ParseName(ContinuationInterfaceName);
        VerificationInterfaceName = $"I{property.UniqueName}PropertyGetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.ParseName(VerificationInterfaceName);
        FluentInterfaceName = $"I{property.UniqueName}PropertyGetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.ParseName(FluentInterfaceName);

        ReturnsMethod = new ReturnsMethodMetadata(in property, ContinuationInterfaceTypeSyntax, OutcomeInterfaceTypeSyntax);
        ThrowsMethod = new ThrowsMethodMetadata(ContinuationInterfaceTypeSyntax, OutcomeInterfaceTypeSyntax);
        CallbackMethod = new CallbackMethodMetadata(ContinuationInterfaceTypeSyntax, ContinuationInterfaceTypeSyntax);
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new PropertyGetterThenMethodMetadata(ContinuationInterfaceTypeSyntax, FluentInterfaceTypeSyntax);
    }
}
