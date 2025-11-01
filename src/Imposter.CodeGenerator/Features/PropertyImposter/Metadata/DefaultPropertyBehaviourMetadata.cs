using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata;

internal readonly struct DefaultPropertyBehaviourMetadata
{
    internal readonly string Name = "DefaultPropertyBehaviour";

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata IsOnField = new("IsOn", WellKnownTypes.Bool);

    internal readonly FieldMetadata BackingField;

    internal DefaultPropertyBehaviourMetadata(in ImposterPropertyCoreMetadata property)
    {
        TypeSyntax = SyntaxFactory.ParseName(Name);
        BackingField = new FieldMetadata("BackingField", property.TypeSyntax);
    }
}