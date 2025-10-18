using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Contexts;

internal readonly record struct InvocationSetupType
{
    internal const string GetOrAddMethodSetupMethodName = "GetOrAddMethodSetup";

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly NameSyntax Syntax;

    internal InvocationSetupType(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationsSetup";
        Interface = TypeMetadataFactory.Create($"I{Name}", method.GenericTypeArguments);
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
    }

    internal readonly struct DefaultInvocationSetupMethod
    {
        public const string Name = "DefaultInvocationSetup";
    }
}