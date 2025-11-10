using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct IndexerSetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string CallbackInterfaceName;

    internal readonly NameSyntax CallbackInterfaceTypeSyntax;

    internal readonly string ContinuationInterfaceName;

    internal readonly NameSyntax ContinuationInterfaceTypeSyntax;

    internal readonly string VerificationInterfaceName;

    internal readonly NameSyntax VerificationInterfaceTypeSyntax;

    internal readonly string FluentInterfaceName;

    internal readonly NameSyntax FluentInterfaceTypeSyntax;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly ThenMethodMetadata ThenMethod;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal IndexerSetterImposterBuilderInterfaceMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerDelegateMetadata delegatesMetadata)
    {
        Name = $"I{core.UniqueName}IndexerSetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        CallbackInterfaceName = $"I{core.UniqueName}IndexerSetterCallbackBuilder";
        CallbackInterfaceTypeSyntax = SyntaxFactory.ParseName(CallbackInterfaceName);

        ContinuationInterfaceName = $"I{core.UniqueName}IndexerSetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.ParseName(ContinuationInterfaceName);

        VerificationInterfaceName = $"I{core.UniqueName}IndexerSetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.ParseName(VerificationInterfaceName);

        FluentInterfaceName = $"I{core.UniqueName}IndexerSetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.ParseName(FluentInterfaceName);

        CallbackMethod = new CallbackMethodMetadata(delegatesMetadata, ContinuationInterfaceTypeSyntax, CallbackInterfaceTypeSyntax);
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new ThenMethodMetadata(ContinuationInterfaceTypeSyntax, FluentInterfaceTypeSyntax);
        UseBaseImplementationMethod = core.SetterSupportsBaseImplementation
            ? new UseBaseImplementationMethodMetadata(TypeSyntax, FluentInterfaceTypeSyntax)
            : null;
    }
}
