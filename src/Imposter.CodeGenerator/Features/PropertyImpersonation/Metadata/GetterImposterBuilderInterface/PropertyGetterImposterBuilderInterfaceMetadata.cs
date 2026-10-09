using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

    internal readonly ThenMethodMetadata ThenMethod;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal readonly string? UseBaseImplementationEntryInterfaceName;

    internal readonly NameSyntax? UseBaseImplementationEntryInterfaceTypeSyntax;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationEntryMethod;

    internal readonly ThenMethodMetadata? InitialThenMethod;

    internal PropertyGetterImposterBuilderInterfaceMetadata(
        in ImposterPropertyCoreMetadata property
    )
    {
        Name = $"I{property.UniqueName}PropertyGetterBuilder";
        TypeSyntax = SyntaxFactory.IdentifierName(Name);
        OutcomeInterfaceName = $"I{property.UniqueName}PropertyGetterOutcomeBuilder";
        OutcomeInterfaceTypeSyntax = SyntaxFactory.IdentifierName(OutcomeInterfaceName);
        ContinuationInterfaceName = $"I{property.UniqueName}PropertyGetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.IdentifierName(ContinuationInterfaceName);
        CallbackInterfaceName = $"I{property.UniqueName}PropertyGetterCallbackBuilder";
        CallbackInterfaceTypeSyntax = SyntaxFactory.IdentifierName(CallbackInterfaceName);
        VerificationInterfaceName = $"I{property.UniqueName}PropertyGetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.IdentifierName(VerificationInterfaceName);
        FluentInterfaceName = $"I{property.UniqueName}PropertyGetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.IdentifierName(FluentInterfaceName);

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
            CallbackInterfaceTypeSyntax,
            WellKnownTypes.System.Action
        );
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new ThenMethodMetadata(
            FluentInterfaceTypeSyntax,
            ContinuationInterfaceTypeSyntax
        );
        if (property.GetterSupportsBaseImplementation)
        {
            UseBaseImplementationMethod = new UseBaseImplementationMethodMetadata(
                FluentInterfaceTypeSyntax,
                FluentInterfaceTypeSyntax
            );

            UseBaseImplementationEntryInterfaceName =
                $"I{property.UniqueName}PropertyGetterUseBaseImplementationBuilder";
            UseBaseImplementationEntryInterfaceTypeSyntax = SyntaxFactory.IdentifierName(
                UseBaseImplementationEntryInterfaceName
            );
            UseBaseImplementationEntryMethod = new UseBaseImplementationMethodMetadata(
                FluentInterfaceTypeSyntax,
                UseBaseImplementationEntryInterfaceTypeSyntax
            );
            InitialThenMethod = new ThenMethodMetadata(
                UseBaseImplementationEntryInterfaceTypeSyntax,
                TypeSyntax
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
