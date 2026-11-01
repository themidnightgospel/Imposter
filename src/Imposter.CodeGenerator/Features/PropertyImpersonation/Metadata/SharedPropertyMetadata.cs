using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

internal readonly struct SharedPropertyMetadata
{
    internal readonly NameSyntax GetterType;
    internal readonly NameSyntax SetterType;
    internal const string SetForInit = "SetForInit";

    internal SharedPropertyMetadata(in ImposterPropertyCoreMetadata property)
    {
        GetterType = WellKnownTypes.Imposter.Abstractions.PropertyGetter(
            property.NullableAwareValueTypeSyntax
        );
        SetterType = WellKnownTypes.Imposter.Abstractions.PropertySetter(
            property.NullableAwareValueTypeSyntax
        );
    }
}
