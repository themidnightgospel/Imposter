using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly ExceptionTypeParameterMetadata ExceptionTypeParameter;

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata ExceptionGeneratorParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceExceptionParameterName;

    internal readonly string InterfaceExceptionGeneratorParameterName;

    public ThrowsMethodMetadata(
        in ReservedParameterNames reservedParameterNames,
        NameSyntax exceptionGeneratorDelegateSyntax,
        NameSyntax interfaceTypeSyntax,
        NameSyntax continuationInterfaceSyntax,
        NameSet genericTypeParameterNameSet
    )
    {
        InterfaceSyntax = interfaceTypeSyntax;
        ReturnType = continuationInterfaceSyntax;
        ExceptionTypeParameter = new ExceptionTypeParameterMetadata(
            genericTypeParameterNameSet.Use(ExceptionTypeParameterMetadata.PreferredName)
        );
        var nameContext = reservedParameterNames.CreateNameSet();
        InterfaceExceptionParameterName = "exception";
        InterfaceExceptionGeneratorParameterName = "exceptionGenerator";
        ExceptionParameter = new ParameterMetadata(
            nameContext.Use(InterfaceExceptionParameterName),
            WellKnownTypes.System.Exception
        );
        ExceptionGeneratorParameter = new ParameterMetadata(
            nameContext.Use(InterfaceExceptionGeneratorParameterName),
            exceptionGeneratorDelegateSyntax
        );
    }
}
