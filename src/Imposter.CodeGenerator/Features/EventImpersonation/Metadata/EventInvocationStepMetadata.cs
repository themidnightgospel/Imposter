using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventInvocationStepMetadata
{
    internal readonly string Name;
    internal readonly NameSyntax Type;
    internal readonly FieldMetadata Parent;
    internal readonly FieldMetadata Criteria;
    internal readonly FieldMetadata Subscription;

    internal EventInvocationStepMetadata(in ImposterEventCoreMetadata core, NameSyntax builderType)
    {
        var names = core.CreateParameterNameSet();
        // A nested helper must not hide a type parameter used by the event's handler type.
        foreach (var token in core.HandlerTypeSyntax.DescendantTokens())
            names.Use(token.ValueText);
        Name = names.Use("InvocationStep");
        Type = IdentifierName(Name);
        Parent = new FieldMetadata(names.Use("_parent"), builderType);
        Criteria = new FieldMetadata(names.Use("_criteria"), core.HandlerArgTypeSyntax);
        Subscription = new FieldMetadata(names.Use("_subscription"), WellKnownTypes.Bool);
    }
}
