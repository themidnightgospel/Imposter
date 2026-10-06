using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct MethodImposterBuilderMetadata
{
    internal readonly string Name = "Builder";

    internal readonly TypeSyntax Syntax;

    // The builder registers its invocation imposter group with the method imposter in its constructor
    // and does not keep a reference to it afterwards.
    internal readonly ParameterMetadata ImposterCollectionParameter;

    internal readonly ParameterMetadata MethodImposterParameter;

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
        ImposterCollectionParameter = new ParameterMetadata(
            "imposterCollection",
            methodImposterCollectionSyntax
        );
        MethodImposterParameter = new ParameterMetadata("imposter", methodImposterSyntax);
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
