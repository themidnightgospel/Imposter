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

    // Return the getter's or the setter's builder for some criteria.
    internal readonly MethodMetadata CreateGetterMethod;

    internal readonly MethodMetadata CreateSetterMethod;

    internal readonly ParameterMetadata CriteriaParameter;

    internal readonly InvocationBuilderMetadata InvocationBuilder;

    internal IndexerImposterBuilderMetadata(
        in ImposterIndexerMetadata indexer,
        in FieldMetadata defaultBehaviourField
    )
    {
        Name = $"{indexer.Core.UniqueName}IndexerBuilder";
        TypeSyntax = IdentifierName(Name);
        DefaultBehaviourField = defaultBehaviourField;
        // The builder's getter and setter take the indexer's parameters next to these fields.
        var fieldNames = indexer.Core.CreateParameterNameSet();
        GetterImposterField = new FieldMetadata(
            fieldNames.Use("_getterImposter"),
            indexer.GetterImplementation.TypeSyntax
        );
        SetterImposterField = new FieldMetadata(
            fieldNames.Use("_setterImposter"),
            indexer.SetterImplementation.TypeSyntax
        );
        CreateGetterMethod = new MethodMetadata(
            "CreateGetter",
            indexer.GetterBuilderInterface.TypeSyntax
        );
        CreateSetterMethod = new MethodMetadata(
            "CreateSetter",
            indexer.SetterBuilderInterface.TypeSyntax
        );
        CriteriaParameter = new ParameterMetadata("criteria", indexer.ArgumentsCriteria.TypeSyntax);
        InvocationBuilder = new InvocationBuilderMetadata(
            TypeSyntax,
            indexer.ArgumentsCriteria.TypeSyntax
        );
    }

    // What the setup indexer returns: it keeps the imposter builder and the criteria, and passes the criteria to
    // CreateGetter or CreateSetter.
    internal readonly struct InvocationBuilderMetadata
    {
        internal readonly string Name = "InvocationBuilder";

        internal readonly NameSyntax TypeSyntax;

        internal readonly FieldMetadata BuilderField;

        internal readonly FieldMetadata CriteriaField;

        internal InvocationBuilderMetadata(NameSyntax builderType, TypeSyntax criteriaType)
        {
            TypeSyntax = QualifiedName(builderType, IdentifierName(Name));
            BuilderField = new FieldMetadata("_builder", builderType);
            CriteriaField = new FieldMetadata("_criteria", criteriaType);
        }
    }
}
