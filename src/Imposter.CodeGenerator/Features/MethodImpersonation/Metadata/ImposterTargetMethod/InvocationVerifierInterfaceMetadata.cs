using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.ImposterTargetMethod;

internal readonly struct InvocationVerifierInterfaceMetadata
{
    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly NameSyntax Syntax;

    internal InvocationVerifierInterfaceMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}InvocationVerifier";
        Interface = new TypeMetadata(Name);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
        CalledMethod = new CalledMethodMetadata();
    }
}
