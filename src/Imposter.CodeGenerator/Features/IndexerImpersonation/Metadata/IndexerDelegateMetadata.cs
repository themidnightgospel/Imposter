using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerDelegateMetadata
{
    internal readonly string ValueDelegateName;

    internal readonly NameSyntax ValueDelegateType;

    internal readonly string GetterCallbackDelegateName;

    internal readonly NameSyntax GetterCallbackDelegateType;

    internal readonly string SetterCallbackDelegateName;

    internal readonly NameSyntax SetterCallbackDelegateType;

    internal readonly string ExceptionDelegateName;

    internal readonly NameSyntax ExceptionDelegateType;

    internal IndexerDelegateMetadata(in ImposterIndexerCoreMetadata core)
    {
        ValueDelegateName = $"{core.UniqueName}IndexerDelegate";
        ValueDelegateType = IdentifierName(ValueDelegateName);
        GetterCallbackDelegateName = $"{core.UniqueName}IndexerGetterCallback";
        GetterCallbackDelegateType = IdentifierName(GetterCallbackDelegateName);
        SetterCallbackDelegateName = $"{core.UniqueName}IndexerSetterCallback";
        SetterCallbackDelegateType = IdentifierName(SetterCallbackDelegateName);
        ExceptionDelegateName = $"{core.UniqueName}IndexerExceptionGenerator";
        ExceptionDelegateType = IdentifierName(ExceptionDelegateName);
    }
}
