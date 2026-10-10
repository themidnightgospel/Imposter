using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;

internal readonly struct SetMethodMetadata
{
    internal readonly string Name = "Set";

    internal readonly TypeSyntax ReturnType;

    internal readonly bool RequiresDirectBaseAssignment;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata BaseImplementationParameter;

    internal SetMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        RequiresDirectBaseAssignment = property.SetterRequiresDirectBaseAssignment;
        ReturnType = RequiresDirectBaseAssignment ? WellKnownTypes.Bool : WellKnownTypes.Void;
        ValueParameter = new ParameterMetadata("value", property.NullableAwareStoredTypeSyntax);
        BaseImplementationParameter = new ParameterMetadata(
            "baseImplementation",
            property.SetterCallbackType.ToNullableType(),
            Null
        );
    }
}
