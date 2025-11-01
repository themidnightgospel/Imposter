using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;

internal readonly struct CalledMethodMetadata
{
    internal readonly string Name = "Called";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CriteriaParameter;

    internal readonly ParameterMetadata CountParameter;

    internal readonly string InvocationCountVariableName = "invocationCount";

    internal CalledMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = WellKnownTypes.Void;
        CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
        CountParameter = new ParameterMetadata("count", WellKnownTypes.Imposter.Abstractions.Count);
    }
}