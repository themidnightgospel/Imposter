using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

// The delegates a property of another ref struct type gets in place of Func<T>, Action<T> and Func<Func<T>?, T>,
// whose type arguments can't be a ref struct.
internal readonly struct PropertyDelegateMetadata
{
    internal readonly string ValueDelegateName;

    internal readonly NameSyntax ValueDelegateType;

    internal readonly string SetterCallbackDelegateName;

    internal readonly NameSyntax SetterCallbackDelegateType;

    internal readonly string ReturnHandlerDelegateName;

    internal readonly NameSyntax ReturnHandlerDelegateType;

    internal PropertyDelegateMetadata(string uniqueName)
    {
        ValueDelegateName = $"{uniqueName}PropertyDelegate";
        ValueDelegateType = IdentifierName(ValueDelegateName);
        SetterCallbackDelegateName = $"{uniqueName}PropertySetterCallback";
        SetterCallbackDelegateType = IdentifierName(SetterCallbackDelegateName);
        ReturnHandlerDelegateName = $"{uniqueName}PropertyReturnHandler";
        ReturnHandlerDelegateType = IdentifierName(ReturnHandlerDelegateName);
    }
}
