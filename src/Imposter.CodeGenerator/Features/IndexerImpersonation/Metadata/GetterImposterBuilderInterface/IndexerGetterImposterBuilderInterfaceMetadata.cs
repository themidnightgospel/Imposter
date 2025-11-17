using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;

internal readonly struct IndexerGetterImposterBuilderInterfaceMetadata
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

    internal readonly ThenMethodMetadata ThenMethod;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal IndexerGetterImposterBuilderInterfaceMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerDelegateMetadata delegatesMetadata
    )
    {
        Name = $"I{core.UniqueName}IndexerGetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);

        OutcomeInterfaceName = $"I{core.UniqueName}IndexerGetterOutcomeBuilder";
        OutcomeInterfaceTypeSyntax = SyntaxFactory.ParseName(OutcomeInterfaceName);

        ContinuationInterfaceName = $"I{core.UniqueName}IndexerGetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.ParseName(ContinuationInterfaceName);

        CallbackInterfaceName = $"I{core.UniqueName}IndexerGetterCallbackBuilder";
        CallbackInterfaceTypeSyntax = SyntaxFactory.ParseName(CallbackInterfaceName);

        VerificationInterfaceName = $"I{core.UniqueName}IndexerGetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.ParseName(VerificationInterfaceName);

        FluentInterfaceName = $"I{core.UniqueName}IndexerGetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.ParseName(FluentInterfaceName);

        ReturnsMethod = new ReturnsMethodMetadata(
            core,
            delegatesMetadata,
            ContinuationInterfaceTypeSyntax,
            OutcomeInterfaceTypeSyntax
        );
        ThrowsMethod = new ThrowsMethodMetadata(
            delegatesMetadata,
            ContinuationInterfaceTypeSyntax,
            OutcomeInterfaceTypeSyntax
        );
        CallbackMethod = new CallbackMethodMetadata(
            delegatesMetadata,
            ContinuationInterfaceTypeSyntax,
            CallbackInterfaceTypeSyntax
        );
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new ThenMethodMetadata(
            ContinuationInterfaceTypeSyntax,
            FluentInterfaceTypeSyntax
        );
        UseBaseImplementationMethod = core.GetterSupportsBaseImplementation
            ? new UseBaseImplementationMethodMetadata(
                OutcomeInterfaceTypeSyntax,
                ContinuationInterfaceTypeSyntax
            )
            : null;
    }
}
