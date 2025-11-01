using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

internal readonly struct AddReturnValueMethodMetadata
{
    internal readonly string Name = "AddReturnValue";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueGeneratorParameter;

    internal AddReturnValueMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = WellKnownTypes.Void;
        ValueGeneratorParameter = new ParameterMetadata("valueGenerator",
            WellKnownTypes.System.FuncOfT(property.TypeSyntax));
    }
}