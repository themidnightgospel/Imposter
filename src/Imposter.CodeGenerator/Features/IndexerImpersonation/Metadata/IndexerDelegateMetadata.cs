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

    // An indexer of another ref struct type also gets these in place of Func<T>, Action, Func<Arguments, T> and
    // Func<Arguments, Func<T>?, T>, whose type arguments can't be a ref struct. Its base setter takes the value, which
    // a lambda can't capture.
    internal readonly string BaseGetterDelegateName;

    internal readonly NameSyntax BaseGetterDelegateType;

    internal readonly string BaseSetterDelegateName;

    internal readonly NameSyntax BaseSetterDelegateType;

    internal readonly string ReturnGeneratorDelegateName;

    internal readonly NameSyntax ReturnGeneratorDelegateType;

    internal readonly string ReturnHandlerDelegateName;

    internal readonly NameSyntax ReturnHandlerDelegateType;

    internal IndexerDelegateMetadata(string uniqueName)
    {
        ValueDelegateName = $"{uniqueName}IndexerDelegate";
        ValueDelegateType = IdentifierName(ValueDelegateName);
        GetterCallbackDelegateName = $"{uniqueName}IndexerGetterCallback";
        GetterCallbackDelegateType = IdentifierName(GetterCallbackDelegateName);
        SetterCallbackDelegateName = $"{uniqueName}IndexerSetterCallback";
        SetterCallbackDelegateType = IdentifierName(SetterCallbackDelegateName);
        ExceptionDelegateName = $"{uniqueName}IndexerExceptionGenerator";
        ExceptionDelegateType = IdentifierName(ExceptionDelegateName);
        BaseGetterDelegateName = $"{uniqueName}IndexerBaseGetter";
        BaseGetterDelegateType = IdentifierName(BaseGetterDelegateName);
        BaseSetterDelegateName = $"{uniqueName}IndexerBaseSetter";
        BaseSetterDelegateType = IdentifierName(BaseSetterDelegateName);
        ReturnGeneratorDelegateName = $"{uniqueName}IndexerReturnGenerator";
        ReturnGeneratorDelegateType = IdentifierName(ReturnGeneratorDelegateName);
        ReturnHandlerDelegateName = $"{uniqueName}IndexerReturnHandler";
        ReturnHandlerDelegateType = IdentifierName(ReturnHandlerDelegateName);
    }
}
