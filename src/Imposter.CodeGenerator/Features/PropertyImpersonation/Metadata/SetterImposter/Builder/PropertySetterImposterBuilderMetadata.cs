using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter.Builder;

internal readonly struct PropertySetterImposterBuilderMetadata
{
    internal readonly string Name = "Builder";

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata SetterImposterField;

    internal readonly FieldMetadata CriteriaField;

    public PropertySetterImposterBuilderMetadata(
        in ImposterPropertyCoreMetadata property,
        NameSyntax setterImposterTypeSyntax
    )
    {
        TypeSyntax = SyntaxFactory.QualifiedName(
            setterImposterTypeSyntax,
            SyntaxFactory.IdentifierName(Name)
        );
        SetterImposterField = new FieldMetadata("_setterImposter", setterImposterTypeSyntax);
        CriteriaField = new FieldMetadata("_criteria", property.AsArgType);
    }
}
