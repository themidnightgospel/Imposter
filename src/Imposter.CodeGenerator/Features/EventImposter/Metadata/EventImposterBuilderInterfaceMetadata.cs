using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

internal readonly struct EventImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string SetupInterfaceName;

    internal readonly NameSyntax SetupInterfaceTypeSyntax;

    internal readonly string VerificationInterfaceName;

    internal readonly NameSyntax VerificationInterfaceTypeSyntax;

    internal readonly string RaiseMethodName;

    internal readonly TypeSyntax RaiseMethodReturnType;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal EventImposterBuilderInterfaceMetadata(in ImposterEventCoreMetadata core)
    {
        Name = $"I{core.UniqueName}EventImposterBuilder";
        TypeSyntax = IdentifierName(Name);
        SetupInterfaceName = $"I{core.UniqueName}EventImposterSetupBuilder";
        SetupInterfaceTypeSyntax = IdentifierName(SetupInterfaceName);
        VerificationInterfaceName = $"I{core.UniqueName}EventImposterVerificationBuilder";
        VerificationInterfaceTypeSyntax = IdentifierName(VerificationInterfaceName);
        RaiseMethodName = core.IsAsync ? "RaiseAsync" : "Raise";
        RaiseMethodReturnType = core.IsAsync
            ? WellKnownTypes.System.Threading.Tasks.TaskOfT(SetupInterfaceTypeSyntax)
            : SetupInterfaceTypeSyntax;
        UseBaseImplementationMethod = core.SupportsBaseImplementation
            ? new UseBaseImplementationMethodMetadata(TypeSyntax)
            : null;
    }
}
