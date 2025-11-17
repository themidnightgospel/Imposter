using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

internal readonly struct DefaultPropertyBehaviourMetadata
{
    internal readonly string Name = "DefaultPropertyBehaviour";

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata IsOnField = new(
        "IsOn",
        WellKnownTypes.Bool,
        TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword)),
        True
    );

    internal readonly FieldMetadata HasValueSetField = new(
        "HasValueSet",
        WellKnownTypes.Bool,
        TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword)),
        False
    );

    internal readonly FieldMetadata BackingField;

    internal DefaultPropertyBehaviourMetadata(in ImposterPropertyCoreMetadata property)
    {
        TypeSyntax = SyntaxFactory.ParseName(Name);
        BackingField = new FieldMetadata("BackingField", property.NullableAwareTypeSyntax);
    }
}
