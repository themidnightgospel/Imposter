using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertyGetterThenMethodMetadata = Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.Common.ThenMethodMetadata;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;

internal readonly struct PropertyGetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string OutcomeInterfaceName;

    internal readonly NameSyntax OutcomeInterfaceTypeSyntax;

    internal readonly string ContinuationInterfaceName;

    internal readonly NameSyntax ContinuationInterfaceTypeSyntax;

    internal readonly string CallbackInterfaceName;

    internal readonly NameSyntax CallbackInterfaceTypeSyntax;

    internal readonly string VerificationInterfaceName;

    internal readonly NameSyntax VerificationInterfaceTypeSyntax;

    internal readonly string FluentInterfaceName;

    internal readonly NameSyntax FluentInterfaceTypeSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly PropertyGetterThenMethodMetadata ThenMethod;

    internal readonly GetterUseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal readonly string? UseBaseImplementationEntryInterfaceName;

    internal readonly NameSyntax? UseBaseImplementationEntryInterfaceTypeSyntax;

    internal readonly GetterUseBaseImplementationMethodMetadata? UseBaseImplementationEntryMethod;

    internal readonly PropertyGetterThenMethodMetadata? InitialThenMethod;

    internal PropertyGetterImposterBuilderInterfaceMetadata(
        in ImposterPropertyCoreMetadata property
    )
    {
        Name = $"I{property.UniqueName}PropertyGetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        OutcomeInterfaceName = $"I{property.UniqueName}PropertyGetterOutcomeBuilder";
        OutcomeInterfaceTypeSyntax = SyntaxFactory.ParseName(OutcomeInterfaceName);
        ContinuationInterfaceName = $"I{property.UniqueName}PropertyGetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.ParseName(ContinuationInterfaceName);
        CallbackInterfaceName = $"I{property.UniqueName}PropertyGetterCallbackBuilder";
        CallbackInterfaceTypeSyntax = SyntaxFactory.ParseName(CallbackInterfaceName);
        VerificationInterfaceName = $"I{property.UniqueName}PropertyGetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.ParseName(VerificationInterfaceName);
        FluentInterfaceName = $"I{property.UniqueName}PropertyGetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.ParseName(FluentInterfaceName);

        ReturnsMethod = new ReturnsMethodMetadata(
            in property,
            ContinuationInterfaceTypeSyntax,
            OutcomeInterfaceTypeSyntax
        );
        ThrowsMethod = new ThrowsMethodMetadata(
            ContinuationInterfaceTypeSyntax,
            OutcomeInterfaceTypeSyntax
        );
        CallbackMethod = new CallbackMethodMetadata(
            ContinuationInterfaceTypeSyntax,
            CallbackInterfaceTypeSyntax
        );
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new PropertyGetterThenMethodMetadata(
            ContinuationInterfaceTypeSyntax,
            FluentInterfaceTypeSyntax
        );
        if (property.GetterSupportsBaseImplementation)
        {
            UseBaseImplementationMethod = new GetterUseBaseImplementationMethodMetadata(
                FluentInterfaceTypeSyntax,
                FluentInterfaceTypeSyntax
            );

            UseBaseImplementationEntryInterfaceName =
                $"I{property.UniqueName}PropertyGetterUseBaseImplementationBuilder";
            UseBaseImplementationEntryInterfaceTypeSyntax = SyntaxFactory.ParseName(
                UseBaseImplementationEntryInterfaceName
            );
            UseBaseImplementationEntryMethod = new GetterUseBaseImplementationMethodMetadata(
                UseBaseImplementationEntryInterfaceTypeSyntax,
                FluentInterfaceTypeSyntax
            );
            InitialThenMethod = new PropertyGetterThenMethodMetadata(
                TypeSyntax,
                UseBaseImplementationEntryInterfaceTypeSyntax
            );
        }
        else
        {
            UseBaseImplementationMethod = null;
            UseBaseImplementationEntryInterfaceName = null;
            UseBaseImplementationEntryInterfaceTypeSyntax = null;
            UseBaseImplementationEntryMethod = null;
            InitialThenMethod = null;
        }
    }
}
