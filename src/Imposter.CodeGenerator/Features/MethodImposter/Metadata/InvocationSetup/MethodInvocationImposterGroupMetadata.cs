using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly record struct MethodInvocationImposterGroupMetadata
{
    internal const string MethodInvocationImposterTypeName = "MethodInvocationImposter";

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly TypeMetadata ContinuationInterface;

    internal readonly TypeMetadata CallbackInterface;

    internal readonly NameSyntax Syntax;

    internal readonly NameSyntax MethodInvocationImposterSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ReturnsAsyncMethodMetadata? ReturnsAsyncMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly ThrowsAsyncMethodMetadata? ThrowsAsyncMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly ThenMethodMetadata ThenMethod;

    internal readonly UseBaseImplementationMethodMetadata? UseBaseImplementationMethod;

    internal readonly DefaultInvocationSetupFieldMetadata DefaultInvocationSetupField;

    internal readonly DefaultResultGeneratorMethodMetadata DefaultResultGeneratorMethod;

    internal MethodInvocationImposterGroupMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationImposterGroup";
        Interface = TypeMetadataFactory.Create($"I{Name}", method.GenericTypeArguments);
        ContinuationInterface = TypeMetadataFactory.Create($"I{Name}Continuation", method.GenericTypeArguments);
        CallbackInterface = TypeMetadataFactory.Create($"I{Name}Callback", method.GenericTypeArguments);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        MethodInvocationImposterSyntax = SyntaxFactory.QualifiedName(Syntax, SyntaxFactory.IdentifierName(MethodInvocationImposterTypeName));
        ReturnsMethod = new ReturnsMethodMetadata(
            method,
            method.ReturnTypeSyntax,
            Interface.Syntax,
            ContinuationInterface.Syntax,
            method.Delegate.Syntax);
        ReturnsAsyncMethod = method.ReturnType.SupportsAsyncValueResult
            ? new ReturnsAsyncMethodMetadata(method, Interface.Syntax, ContinuationInterface.Syntax, method.ReturnType.AsyncValueTypeSyntax!)
            : null;
        ThrowsMethod = new ThrowsMethodMetadata(
            method,
            method.ExceptionGeneratorDelegate.Syntax,
            Interface.Syntax,
            ContinuationInterface.Syntax,
            method.GenericTypeParameterNameSet);
        
        ThrowsAsyncMethod = method.IsAsync
            ? new ThrowsAsyncMethodMetadata(method, Interface.Syntax, ContinuationInterface.Syntax)
            : null;
        CallbackMethod = new CallbackMethodMetadata(
            method,
            ContinuationInterface.Syntax,
            CallbackInterface.Syntax,
            method.CallbackDelegate.Syntax);
        ThenMethod = new ThenMethodMetadata(Interface.Syntax, ContinuationInterface.Syntax);
        UseBaseImplementationMethod = method.SupportsBaseImplementation
            ? new UseBaseImplementationMethodMetadata(Interface.Syntax, ContinuationInterface.Syntax)
            : null;
        DefaultInvocationSetupField = new DefaultInvocationSetupFieldMetadata();
        DefaultResultGeneratorMethod = new DefaultResultGeneratorMethodMetadata(method.ReturnTypeSyntax);
    }
}
