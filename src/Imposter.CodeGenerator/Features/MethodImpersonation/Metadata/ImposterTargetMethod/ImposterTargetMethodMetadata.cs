using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct ImposterTargetMethodMetadata
{
    internal readonly IMethodSymbol Symbol;

    internal readonly ImposterTargetMethodParametersMetadata Parameters;

    internal readonly ReservedParameterNames ReservedParameterNames;

    internal readonly MethodInvocationImposterGroupMetadata MethodInvocationImposterGroup;

    internal readonly MethodInvocationImposterMetadata MethodInvocationImposter;

    internal readonly TypeMetadata Delegate;

    internal readonly TypeMetadata CallbackDelegate;

    internal readonly TypeMetadata ExceptionGeneratorDelegate;

    internal readonly ArgumentCriteriaTypeMetadata ArgumentsCriteria;

    internal readonly AsMethodMetadata ArgumentsCriteriaAsMethod;

    internal readonly TypeMetadata Arguments;

    internal readonly InvocationHistoryTypeMetadata InvocationHistory;

    internal readonly InvocationVerifierInterfaceMetadata InvocationVerifierInterface;

    internal readonly MethodImposterMetadata MethodImposter;

    internal readonly ReturnTypeMetadata ReturnType;

    internal readonly NameSet GenericTypeParameterNameSet;

    internal readonly bool HasReturnValue;

    internal readonly bool SupportsBaseImplementation;

    internal readonly string UniqueName;

    internal readonly string DisplayName;

    internal readonly TypeSyntax ReturnTypeSyntax;

    internal readonly SyntaxTokenList ImposterInstanceMethodModifiers;

    internal readonly bool RequiresExplicitInterfaceImplementation;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    internal readonly IReadOnlyList<NameSyntax> GenericTypeArguments;

    internal readonly TypeArgumentListSyntax? GenericTypeArgumentListSyntax;

    internal readonly TypeParameterListSyntax? GenericTypeParameterListSyntax;

    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> GenericTypeConstraintClauses;

    internal readonly IReadOnlyList<NameSyntax> TargetGenericTypeArguments;

    internal readonly TypeParameterListSyntax? TargetGenericTypeParameterListSyntax;

    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> TargetGenericTypeConstraintClauses;

    internal bool IsAsync { get; }

    internal ImposterTargetMethodMetadata(
        IMethodSymbol symbol,
        string uniqueName,
        bool supportsNullableGenericType,
        bool requiresExplicitInterfaceImplementation = false
    )
    {
        Symbol = symbol;
        UniqueName = uniqueName;
        DisplayName = Symbol.ToFullDisplayName();
        var containingNamespace = Symbol.ContainingNamespace.ToDisplayString();
        ReturnTypeSyntax = SyntaxFactoryHelper.TypeSyntax(Symbol.ReturnType);
        ReturnType = new ReturnTypeMetadata(
            Symbol.ReturnType,
            ReturnTypeSyntax,
            supportsNullableGenericType
        );
        HasReturnValue = !Symbol.ReturnsVoid;
        SupportsBaseImplementation =
            Symbol.ContainingType?.TypeKind == TypeKind.Class && !Symbol.IsAbstract;
        IsAsync = symbol.IsMethodAsync();

        Parameters = new ImposterTargetMethodParametersMetadata(Symbol.Parameters);
        ReservedParameterNames = new ReservedParameterNames(
            Symbol.Parameters.Select(p => p.Name).Concat([UniqueName, containingNamespace])
        );
        GenericTypeParameterNameSet = new NameSet(Symbol.TypeParameters.Select(p => p.Name));
        GenericTypeArguments = Symbol
            .TypeParameters.Select(p =>
                SyntaxFactory.IdentifierName(SyntaxFactoryHelper.EscapedIdentifier(p.Name))
            )
            .ToArray();
        GenericTypeArgumentListSyntax = SyntaxFactoryHelper.TypeArgumentListSyntax(
            GenericTypeArguments
        );
        GenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(
            GenericTypeArguments
        );
        GenericTypeConstraintClauses = SyntaxFactoryHelper.TypeParameterConstraintClauses(
            Symbol.TypeParameters
        );

        var targetGenericNameContext = new NameSet(Symbol.TypeParameters.Select(p => p.Name));
        TargetGenericTypeArguments = Symbol
            .TypeParameters.Select(p =>
                SyntaxFactory.IdentifierName(targetGenericNameContext.Use($"{p.Name}Target"))
            )
            .ToArray();
        TargetGenericTypeParameterListSyntax = SyntaxFactoryHelper.TypeParameterListSyntax(
            TargetGenericTypeArguments
        );

        if (GenericTypeConstraintClauses.Count > 0)
        {
            var targetRenamer = new TypeParameterRenamer(
                Symbol.TypeParameters,
                TargetGenericTypeArguments
            );
            TargetGenericTypeConstraintClauses = GenericTypeConstraintClauses
                .Select(c => (TypeParameterConstraintClauseSyntax)targetRenamer.Visit(c))
                .ToArray();
        }
        else
        {
            TargetGenericTypeConstraintClauses = [];
        }

        Delegate = TypeMetadataFactory.Create($"{uniqueName}Delegate", GenericTypeArguments);
        CallbackDelegate = TypeMetadataFactory.Create(
            $"{uniqueName}CallbackDelegate",
            GenericTypeArguments
        );
        ExceptionGeneratorDelegate = TypeMetadataFactory.Create(
            $"{uniqueName}ExceptionGeneratorDelegate",
            GenericTypeArguments
        );

        var argumentsTypeName = $"{uniqueName}Arguments";
        Arguments = new TypeMetadata(
            argumentsTypeName,
            SyntaxFactoryHelper.WithMethodGenericArguments(GenericTypeArguments, argumentsTypeName)
        );
        ArgumentsCriteria = new ArgumentCriteriaTypeMetadata(this);
        ArgumentsCriteriaAsMethod = new AsMethodMetadata(
            Symbol.TypeParameters,
            GenericTypeParameterNameSet
        );
        InvocationHistory = new InvocationHistoryTypeMetadata(this);
        MethodInvocationImposterGroup = new MethodInvocationImposterGroupMetadata(this);
        MethodInvocationImposter = new MethodInvocationImposterMetadata(ReservedParameterNames);
        InvocationVerifierInterface = new InvocationVerifierInterfaceMetadata(this);
        MethodImposter = new MethodImposterMetadata(this);
        RequiresExplicitInterfaceImplementation = requiresExplicitInterfaceImplementation;
        if (requiresExplicitInterfaceImplementation && symbol.ContainingType is not null)
        {
            ExplicitInterfaceSpecifier = SyntaxFactory.ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(symbol.ContainingType)
            );
            ImposterInstanceMethodModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceMethodModifiers = ImposterInstanceModifierBuilder.For(symbol);
        }
    }

    internal readonly struct AsMethodMetadata
    {
        internal readonly NameSyntax[] TargetTypeArguments;
        internal readonly TypeParameterSyntax[] TypeParameters;

        internal AsMethodMetadata(
            IReadOnlyList<ITypeParameterSymbol> typeParameters,
            NameSet nameSet
        )
        {
            var allocatedNames = typeParameters
                .Select(p => nameSet.Use($"{p.Name}Target"))
                .ToArray();

            TargetTypeArguments = allocatedNames
                .Select(name => (NameSyntax)SyntaxFactory.IdentifierName(name))
                .ToArray();

            TypeParameters = allocatedNames.Select(SyntaxFactory.TypeParameter).ToArray();
        }
    }
}
