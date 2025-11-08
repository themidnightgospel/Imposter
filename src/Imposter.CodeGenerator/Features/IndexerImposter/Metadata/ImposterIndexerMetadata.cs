using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly ref struct ImposterIndexerMetadata
{
    internal readonly ImposterIndexerCoreMetadata Core;

    internal readonly IndexerArgumentsMetadata Arguments;

    internal readonly IndexerArgumentsCriteriaMetadata ArgumentsCriteria;

    internal readonly DefaultIndexerBehaviourMetadata DefaultIndexerBehaviour;

    internal readonly FieldMetadata DefaultIndexerBehaviourField;

    internal readonly IndexerDelegateMetadata Delegates;

    internal readonly IndexerGetterImposterMetadata GetterImplementation;

    internal readonly IndexerSetterImposterMetadata SetterImplementation;

    internal readonly IndexerGetterImposterBuilderInterfaceMetadata GetterBuilderInterface;

    internal readonly IndexerSetterImposterBuilderInterfaceMetadata SetterBuilderInterface;

    internal readonly IndexerImposterBuilderInterfaceMetadata BuilderInterface;

    internal readonly IndexerImposterBuilderMetadata Builder;

    internal readonly FieldMetadata BuilderField;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    internal ImposterIndexerMetadata(IPropertySymbol propertySymbol, string uniqueName)
    {
        Core = new ImposterIndexerCoreMetadata(propertySymbol, uniqueName);
        Arguments = new IndexerArgumentsMetadata(Core);
        ArgumentsCriteria = new IndexerArgumentsCriteriaMetadata(Core);
        DefaultIndexerBehaviour = new DefaultIndexerBehaviourMetadata(Core, Arguments);
        DefaultIndexerBehaviourField = new FieldMetadata($"_{Core.UniqueName}DefaultIndexerBehaviour", DefaultIndexerBehaviour.TypeSyntax);
        Delegates = new IndexerDelegateMetadata(Core);
        GetterImplementation = new IndexerGetterImposterMetadata(this);
        SetterImplementation = new IndexerSetterImposterMetadata(this);
        GetterBuilderInterface = new IndexerGetterImposterBuilderInterfaceMetadata(Core, Delegates);
        SetterBuilderInterface = new IndexerSetterImposterBuilderInterfaceMetadata(Core, Delegates);
        BuilderInterface = new IndexerImposterBuilderInterfaceMetadata(Core, SetterBuilderInterface, GetterBuilderInterface);
        Builder = new IndexerImposterBuilderMetadata(Core, DefaultIndexerBehaviourField);
        BuilderField = new FieldMetadata($"_{Core.UniqueName}Indexer", Builder.TypeSyntax);
        ImposterInstanceModifiers = BuildImposterInstanceModifiers(propertySymbol);
    }

    private static SyntaxTokenList BuildImposterInstanceModifiers(IPropertySymbol propertySymbol)
    {
        if (propertySymbol.ContainingType?.TypeKind == TypeKind.Class)
        {
            return propertySymbol.DeclaredAccessibility switch
            {
                Accessibility.Public => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword)),
                Accessibility.Internal => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InternalKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword)),
                Accessibility.Protected => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword)),
                Accessibility.ProtectedOrInternal => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.InternalKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword)),
                Accessibility.ProtectedAndInternal => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword)),
                _ => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
            };
        }

        return SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
    }
}