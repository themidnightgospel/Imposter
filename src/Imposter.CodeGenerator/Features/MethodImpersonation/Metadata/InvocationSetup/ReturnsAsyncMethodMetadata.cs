using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct ReturnsAsyncMethodMetadata
{
    internal readonly string Name = "ReturnsAsync";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceValueParameterName;

    internal ReturnsAsyncMethodMetadata(
        in ReservedParameterNames reservedParameterNames,
        NameSyntax interfaceSyntax,
        NameSyntax continuationInterfaceSyntax,
        TypeSyntax valueParameterType
    )
    {
        InterfaceSyntax = interfaceSyntax;
        ReturnType = continuationInterfaceSyntax;
        InterfaceValueParameterName = "value";
        var nameContext = reservedParameterNames.CreateNameSet();
        ValueParameter = new ParameterMetadata(
            nameContext.Use(InterfaceValueParameterName),
            valueParameterType
        );
    }
}
