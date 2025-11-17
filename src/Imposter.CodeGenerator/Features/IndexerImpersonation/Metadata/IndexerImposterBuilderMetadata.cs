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
        GetterImposterField = new FieldMetadata(
            "_getterImposter",
            IdentifierName("GetterImposter")
        );
        SetterImposterField = new FieldMetadata(
            "_setterImposter",
            IdentifierName("SetterImposter")
        );
        InvocationBuilderTypeSyntax = QualifiedName(
            TypeSyntax,
            IdentifierName("InvocationBuilder")
        );
    }
}
