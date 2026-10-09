using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly ref struct ImposterEventMetadata
{
    internal readonly ImposterEventCoreMetadata Core;

    internal readonly EventImposterBuilderInterfaceMetadata BuilderInterface;

    internal readonly EventImposterBuilderMetadata Builder;

    internal readonly FieldMetadata BuilderField;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    // The imposter property that sets this event up: named after it, or by its unique name when it's implemented
    // explicitly.
    internal readonly string SetupName;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    internal ImposterEventMetadata(
        EventModel @event,
        string uniqueName,
        bool requiresExplicitInterfaceImplementation
    )
    {
        Core = new ImposterEventCoreMetadata(@event, uniqueName);
        BuilderInterface = new EventImposterBuilderInterfaceMetadata(Core);
        Builder = new EventImposterBuilderMetadata(Core);
        BuilderField = new FieldMetadata($"_{Core.UniqueName}", Builder.TypeSyntax);

        SetupName = requiresExplicitInterfaceImplementation ? Core.UniqueName : Core.Name;
        if (requiresExplicitInterfaceImplementation)
        {
            ExplicitInterfaceSpecifier = ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(@event.ContainingType)
            );
            ImposterInstanceModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceModifiers = ImposterInstanceModifierBuilder.For(@event);
        }
    }
}
