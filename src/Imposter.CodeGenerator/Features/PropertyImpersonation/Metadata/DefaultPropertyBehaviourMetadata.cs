using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

// The auto-property behaviour both property paths share: an Imposter.Abstractions PropertyValue<T>.
internal readonly struct DefaultPropertyBehaviourMetadata
{
    internal const string IsOnPropertyName = "IsOn";

    internal const string TurnOffMethodName = "TurnOff";

    internal const string ReadMethodName = "Read";

    internal const string WriteMethodName = "Write";

    internal readonly NameSyntax TypeSyntax;

    internal DefaultPropertyBehaviourMetadata(in ImposterPropertyCoreMetadata property)
    {
        TypeSyntax = WellKnownTypes.Imposter.Abstractions.PropertyValue(
            property.NullableAwareValueTypeSyntax
        );
    }
}
