using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;

internal readonly struct AddReturnValueMethodMetadata
{
    internal readonly string Name = "AddReturnValue";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueGeneratorParameter;

    internal AddReturnValueMethodMetadata(TypeSyntax handlerType)
    {
        ReturnType = WellKnownTypes.Void;
        ValueGeneratorParameter = new ParameterMetadata("valueGenerator", handlerType);
    }
}
