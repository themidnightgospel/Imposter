using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

internal readonly struct ImposterTargetMethodMetadata
{
    // How the setup views declare this method; null for a class target, which has no setup views.
    internal readonly InterfaceSetupMemberModel? InterfaceSetupMember;

    internal readonly MethodModel Model;

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

    // Names for the members the imposters of this method declare, kept apart from the method's parameter and type
    // parameter names, which their setup and invoke members declare next to them.
    internal readonly NameSet MemberNames;

    internal readonly bool HasReturnValue;

    internal readonly bool SupportsBaseImplementation;

    internal readonly string UniqueName;

    internal readonly string DisplayName;

    // Without nullable reference annotations, which `typeof` doesn't allow. Declarations use
    // NullableAwareReturnTypeSyntax, so they match the target's annotations.
    internal readonly TypeSyntax ReturnTypeSyntax;

    internal readonly TypeSyntax NullableAwareReturnTypeSyntax;

    internal readonly SyntaxTokenList ImposterInstanceMethodModifiers;

    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> ImposterInstanceMethodConstraintClauses;

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
        TargetMemberModel<MethodModel> method,
        string uniqueName,
        bool supportsNullableGenericType
    )
    {
        Model = method.Member;
        InterfaceSetupMember = method.Setup;
        UniqueName = uniqueName;
        DisplayName = Model.DisplayName;
        ReturnTypeSyntax = SyntaxFactoryHelper.TypeSyntax(Model.ReturnType.Type);
        NullableAwareReturnTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(
            Model.ReturnType.Type
        );
        ReturnType = new ReturnTypeMetadata(
            Model.ReturnType,
            NullableAwareReturnTypeSyntax,
            supportsNullableGenericType
        );
        HasReturnValue = !Model.ReturnType.IsVoid;
        SupportsBaseImplementation = Model.IsClassMember && !Model.IsAbstract;
        IsAsync = Model.IsAsync;

        Parameters = new ImposterTargetMethodParametersMetadata(Model.Parameters);
        ReservedParameterNames = new ReservedParameterNames(
            Model.Parameters.Select(p => p.Name).Concat([UniqueName, Model.ContainingNamespace])
        );
        GenericTypeParameterNameSet = new NameSet(Model.TypeParameters.Select(p => p.Name));
        MemberNames = new NameSet(
            Model
                .Parameters.Select(parameter => parameter.Name)
                .Concat(Model.TypeParameters.Select(typeParameter => typeParameter.Name))
        );
        GenericTypeArguments = Model
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
            Model.TypeParameters
        );

        var targetGenericNameContext = new NameSet(Model.TypeParameters.Select(p => p.Name));
        TargetGenericTypeArguments = Model
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
                Model.TypeParameters,
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
            Model.TypeParameters,
            GenericTypeParameterNameSet
        );
        InvocationHistory = new InvocationHistoryTypeMetadata(this);
        MethodInvocationImposterGroup = new MethodInvocationImposterGroupMetadata(this);
        MethodInvocationImposter = new MethodInvocationImposterMetadata(
            ReservedParameterNames,
            MemberNames
        );
        InvocationVerifierInterface = new InvocationVerifierInterfaceMetadata(this);
        MethodImposter = new MethodImposterMetadata(this);
        RequiresExplicitInterfaceImplementation = method.RequiresExplicitInterfaceImplementation;
        ImposterInstanceMethodConstraintClauses =
            Model.IsClassMember || RequiresExplicitInterfaceImplementation
                ? SyntaxFactoryHelper.RestatableConstraintClauses(
                    Model.TypeParameters,
                    TypeParametersUsedAsNullable(Parameters, Model.ReturnType)
                )
                : GenericTypeConstraintClauses;
        if (RequiresExplicitInterfaceImplementation)
        {
            ExplicitInterfaceSpecifier = SyntaxFactory.ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(Model.ContainingType)
            );
            ImposterInstanceMethodModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceMethodModifiers = ImposterInstanceModifierBuilder.For(Model);
        }
    }

    // The names of the type parameters written as T? in the parameter or return types.
    private static HashSet<string> TypeParametersUsedAsNullable(
        in ImposterTargetMethodParametersMetadata parameters,
        ReturnTypeModel returnType
    ) =>
        new(
            parameters
                .AllParameterMetadata.Select(it => it.NullableAwareTypeSyntax)
                .Append(SyntaxFactoryHelper.TypeSyntaxIncludingNullable(returnType.Type))
                .SelectMany(type => type.DescendantNodesAndSelf().OfType<NullableTypeSyntax>())
                .Select(nullable => nullable.ElementType)
                .OfType<IdentifierNameSyntax>()
                .Select(name => name.Identifier.ValueText)
        );

    internal readonly struct AsMethodMetadata
    {
        internal readonly NameSyntax[] TargetTypeArguments;
        internal readonly TypeParameterSyntax[] TypeParameters;

        internal AsMethodMetadata(IReadOnlyList<TypeParameterModel> typeParameters, NameSet nameSet)
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
