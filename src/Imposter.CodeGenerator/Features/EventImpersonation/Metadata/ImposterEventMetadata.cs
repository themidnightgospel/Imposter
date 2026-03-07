using Imposter.CodeGenerator.Helpers;
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

    internal readonly bool RequiresExplicitInterfaceImplementation;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    internal ImposterEventMetadata(
        IEventSymbol eventSymbol,
        string uniqueName,
        bool requiresExplicitInterfaceImplementation = false
    )
    {
        Core = new ImposterEventCoreMetadata(eventSymbol, uniqueName);
        BuilderInterface = new EventImposterBuilderInterfaceMetadata(Core);
        Builder = new EventImposterBuilderMetadata(Core);
        BuilderField = new FieldMetadata($"_{Core.UniqueName}", Builder.TypeSyntax);

        RequiresExplicitInterfaceImplementation = requiresExplicitInterfaceImplementation;
        if (requiresExplicitInterfaceImplementation && eventSymbol.ContainingType is not null)
        {
            ExplicitInterfaceSpecifier = ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(eventSymbol.ContainingType)
            );
            ImposterInstanceModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceModifiers = ImposterInstanceModifierBuilder.For(eventSymbol);
        }
    }
}
