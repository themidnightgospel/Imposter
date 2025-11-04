using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CallbackParameter;

    public CallbackMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax interfaceSyntax,
        TypeSyntax callbackTypeSyntax)
    {
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ReturnType = interfaceSyntax;
        CallbackParameter = new ParameterMetadata(nameContext.Use("callback"), callbackTypeSyntax);
    }
}
