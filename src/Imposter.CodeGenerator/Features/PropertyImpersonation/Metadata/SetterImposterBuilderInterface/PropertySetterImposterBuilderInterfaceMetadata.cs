using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposterBuilderInterface;

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

    internal readonly ThenMethodMetadata ThenMethod;

    internal readonly string? UseBaseImplementationEntryInterfaceName;

    internal readonly NameSyntax? UseBaseImplementationEntryInterfaceTypeSyntax;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationEntryMethod;

    internal PropertySetterImposterBuilderInterfaceMetadata(
        in ImposterPropertyCoreMetadata property
    )
    {
        Name = $"I{property.UniqueName}PropertySetterBuilder";
        Syntax = SyntaxFactory.IdentifierName(Name);
        FluentInterfaceName = $"I{property.UniqueName}PropertySetterFluentBuilder";
        FluentInterfaceTypeSyntax = SyntaxFactory.IdentifierName(FluentInterfaceName);
        CallbackInterfaceName = $"I{property.UniqueName}PropertySetterCallbackBuilder";
        CallbackInterfaceTypeSyntax = SyntaxFactory.IdentifierName(CallbackInterfaceName);
        ContinuationInterfaceName = $"I{property.UniqueName}PropertySetterContinuationBuilder";
        ContinuationInterfaceTypeSyntax = SyntaxFactory.IdentifierName(ContinuationInterfaceName);
        VerificationInterfaceName = $"I{property.UniqueName}PropertySetterVerifier";
        VerificationInterfaceTypeSyntax = SyntaxFactory.IdentifierName(VerificationInterfaceName);
        CalledMethod = new CalledMethodMetadata();
        CallbackMethod = new CallbackMethodMetadata(
            ContinuationInterfaceTypeSyntax,
            CallbackInterfaceTypeSyntax,
            property.SetterCallbackType
        );
        if (property.SetterSupportsBaseImplementation)
        {
            UseBaseImplementationEntryInterfaceName =
                $"I{property.UniqueName}PropertySetterUseBaseImplementationBuilder";
            UseBaseImplementationEntryInterfaceTypeSyntax = SyntaxFactory.IdentifierName(
                UseBaseImplementationEntryInterfaceName
            );
            UseBaseImplementationEntryMethod = new UseBaseImplementationMethodMetadata(
                FluentInterfaceTypeSyntax,
                UseBaseImplementationEntryInterfaceTypeSyntax
            );
            ThenMethod = new ThenMethodMetadata(
                UseBaseImplementationEntryInterfaceTypeSyntax,
                ContinuationInterfaceTypeSyntax
            );
        }
        else
        {
            UseBaseImplementationEntryInterfaceName = null;
            UseBaseImplementationEntryInterfaceTypeSyntax = null;
            UseBaseImplementationEntryMethod = null;
            ThenMethod = new ThenMethodMetadata(
                FluentInterfaceTypeSyntax,
                ContinuationInterfaceTypeSyntax
            );
        }
    }
}
