using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct MethodImposterBuilderMetadata
{
    internal readonly string Name = "Builder";

    internal readonly TypeSyntax Syntax;

    internal readonly FieldMetadata ImposterCollectionField;

    internal readonly FieldMetadata MethodImposterField;

    internal readonly FieldMetadata ArgumentsCriteriaField;

    internal readonly FieldMetadata InvocationImposterGroupField;

    internal readonly FieldMetadata CurrentInvocationImposterField;

    internal MethodImposterBuilderMetadata(
        NameSyntax methodImposterSyntax,
        NameSyntax methodImposterCollectionSyntax,
        NameSyntax argumentCriteriaSyntax,
        NameSyntax invocationImposterGroupType,
        NameSyntax methodInvocationImposterType
    )
    {
        Syntax = SyntaxFactory.QualifiedName(
            methodImposterSyntax,
            SyntaxFactory.IdentifierName("Builder")
        );
        ImposterCollectionField = new FieldMetadata(
            "_imposterCollection",
            methodImposterCollectionSyntax
        );
        MethodImposterField = new FieldMetadata("_imposter", methodImposterSyntax);
        ArgumentsCriteriaField = new FieldMetadata("_argumentsCriteria", argumentCriteriaSyntax);
        InvocationImposterGroupField = new FieldMetadata(
            "_invocationImposterGroup",
            invocationImposterGroupType
        );
        CurrentInvocationImposterField = new FieldMetadata(
            "_currentInvocationImposter",
            methodInvocationImposterType
        );
    }
}
