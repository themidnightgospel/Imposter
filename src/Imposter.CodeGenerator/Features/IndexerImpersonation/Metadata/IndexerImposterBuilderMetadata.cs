using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata DefaultBehaviourField;

    internal readonly FieldMetadata GetterImposterField;

    internal readonly FieldMetadata SetterImposterField;

    internal readonly NameSyntax InvocationBuilderTypeSyntax;

    internal IndexerImposterBuilderMetadata(
        in ImposterIndexerCoreMetadata core,
        in FieldMetadata defaultBehaviourField
    )
    {
        Name = $"{core.UniqueName}IndexerBuilder";
        TypeSyntax = IdentifierName(Name);
        DefaultBehaviourField = defaultBehaviourField;
        // The builder's getter and setter take the indexer's parameters next to these fields.
        var fieldNames = core.CreateParameterNameSet();
        GetterImposterField = new FieldMetadata(
            fieldNames.Use("_getterImposter"),
            IdentifierName("GetterImposter")
        );
        SetterImposterField = new FieldMetadata(
            fieldNames.Use("_setterImposter"),
            IdentifierName("SetterImposter")
        );
        InvocationBuilderTypeSyntax = QualifiedName(
            TypeSyntax,
            IdentifierName("InvocationBuilder")
        );
    }
}
