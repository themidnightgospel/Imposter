using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct MethodImposterBuilderMetadata
{
    internal readonly string Name = "Builder";

    internal readonly TypeSyntax Syntax;

    // The method imposter, or a generic method's collection that the builder adds one to. The builder registers its
    // invocation imposter group with the method imposter in its constructor and doesn't keep a reference to it.
    internal readonly ParameterMetadata ImposterParameter;

    internal readonly FieldMetadata ArgumentsCriteriaField;

    internal readonly FieldMetadata InvocationImposterGroupField;

    internal readonly FieldMetadata CurrentInvocationImposterField;

    // methodImposterCollectionSyntax is null for a non-generic method, which has no collection.
    internal MethodImposterBuilderMetadata(
        NameSyntax methodImposterSyntax,
        NameSyntax? methodImposterCollectionSyntax,
        NameSyntax argumentCriteriaSyntax,
        NameSyntax invocationImposterGroupType,
        NameSyntax methodInvocationImposterType
    )
    {
        Syntax = SyntaxFactory.QualifiedName(
            methodImposterSyntax,
            SyntaxFactory.IdentifierName("Builder")
        );
        ImposterParameter = methodImposterCollectionSyntax is not null
            ? new ParameterMetadata("imposterCollection", methodImposterCollectionSyntax)
            : new ParameterMetadata("imposter", methodImposterSyntax);
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
