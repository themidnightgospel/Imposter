using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct DefaultIndexerBehaviourMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string IsOnPropertyName;

    internal readonly FieldMetadata IsOnBackingField;

    internal readonly FieldMetadata BackingField;

    internal DefaultIndexerBehaviourMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerArgumentsMetadata arguments
    )
    {
        Name = $"Default{core.UniqueName}IndexerBehaviour";
        TypeSyntax = IdentifierName(Name);
        IsOnPropertyName = "IsOn";
        IsOnBackingField = new FieldMetadata("_isOn", WellKnownTypes.Bool);
        BackingField = new FieldMetadata(
            "BackingField",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(
                arguments.TypeSyntax,
                core.TypeSyntax
            )
        );
    }
}
