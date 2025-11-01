using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly string GenericTypeParameterName = "TException";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal ThrowsMethodMetadata(TypeSyntax builderInterfaceTypeSyntax)
    {
        ReturnType = builderInterfaceTypeSyntax;
        ExceptionParameter = new ParameterMetadata("exception", WellKnownTypes.System.Exception);
    }
}