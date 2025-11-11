using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly string GenericTypeParameterName;

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata ExceptionGeneratorParameter;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly string InterfaceExceptionParameterName;

    internal readonly string InterfaceExceptionGeneratorParameterName;

    internal readonly TypeParameterListSyntax TypeParameterList;

    internal readonly TypeParameterConstraintClauseSyntax TypeParameterConstraintClause;

    public ThrowsMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider,
        NameSyntax exceptionGeneratorDelegateSyntax,
        NameSyntax interfaceTypeSyntax,
        NameSyntax continuationInterfaceSyntax,
        NameSet genericTypeParameterNameSet)
    {
        InterfaceSyntax = interfaceTypeSyntax;
        ReturnType = continuationInterfaceSyntax;
        GenericTypeParameterName = genericTypeParameterNameSet.Use("TException");
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        InterfaceExceptionParameterName = "exception";
        InterfaceExceptionGeneratorParameterName = "exceptionGenerator";
        ExceptionParameter = new ParameterMetadata(nameContext.Use(InterfaceExceptionParameterName), WellKnownTypes.System.Exception);
        ExceptionGeneratorParameter = new ParameterMetadata(nameContext.Use(InterfaceExceptionGeneratorParameterName), exceptionGeneratorDelegateSyntax);

        TypeParameterList = TypeParameterList(
            SingletonSeparatedList(
                TypeParameter(GenericTypeParameterName)));

        TypeParameterConstraintClause = TypeParameterConstraintClause(GenericTypeParameterName)
            .AddConstraints(
                TypeConstraint(WellKnownTypes.System.Exception),
                ConstructorConstraint());
    }
}
