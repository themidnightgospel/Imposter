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

    internal readonly TypeMetadata Arguments;

    // A generic method's arguments class converts itself with this method. Like the criteria, it keeps a field per
    // parameter, so it takes the criteria's As name.
    internal readonly string ArgumentsAsMethodName;

    internal readonly InvocationHistoryTypeMetadata InvocationHistory;

    internal readonly InvocationVerifierInterfaceMetadata InvocationVerifierInterface;

    internal readonly MethodImposterMetadata MethodImposter;

    // The imposter's field for this method: its method imposter, or for a generic method the collection that keeps a
    // method imposter per type argument.
    internal readonly FieldMetadata ImposterField;

    internal readonly ReturnTypeMetadata ReturnType;

    internal readonly NameSet GenericTypeParameterNameSet;

    // Names for the members the imposters of this method declare, kept apart from the method's parameter and type
    // parameter names, which their setup and invoke members declare next to them.
    internal readonly NameSet MemberNames;

    internal readonly bool HasReturnValue;

    // Returns(value) and the invocation history keep the result, which a ref struct result can't be.
    internal readonly bool KeepsResult;

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

    // The imposter method that sets this method up: named after it, or by its unique name when another member would
    // take the same setup signature.
    internal readonly string SetupName;

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
        IEnumerable<string> targetTypeParameterNames
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
        ReturnType = new ReturnTypeMetadata(Model.ReturnType, NullableAwareReturnTypeSyntax);
        HasReturnValue = !Model.ReturnType.IsVoid;
        KeepsResult = HasReturnValue && !Model.ReturnType.IsPassedThrough;
        SupportsBaseImplementation = Model.IsClassMember && !Model.IsAbstract;
        IsAsync = Model.IsAsync;

        Parameters = new ImposterTargetMethodParametersMetadata(Model.Parameters);
        ReservedParameterNames = new ReservedParameterNames(
            Model.Parameters.Select(p => p.Name).Concat([UniqueName, Model.ContainingNamespace])
        );
        // The method's generic members, such as Throws<TException>(), are declared inside the imposter and, for a
        // generic method, inside types that take the method's type parameters, so their names avoid both.
        GenericTypeParameterNameSet = new NameSet(
            Model.TypeParameters.Select(p => p.Name).Concat(targetTypeParameterNames)
        );
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
        GenericTypeArgumentListSyntax =
            GenericTypeArguments.Count == 0
                ? null
                : SyntaxFactoryHelper.TypeArguments(GenericTypeArguments);
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

        // The arguments class keeps each parameter in a field named after it, which can't share the class's name.
        var argumentsTypeName = MemberNames.Use($"{uniqueName}Arguments");
        Arguments = new TypeMetadata(
            argumentsTypeName,
            SyntaxFactoryHelper.WithMethodGenericArguments(GenericTypeArguments, argumentsTypeName)
        );
        ArgumentsCriteria = new ArgumentCriteriaTypeMetadata(this);
        ArgumentsAsMethodName = ArgumentsCriteria.AsMethod.Name;
        InvocationHistory = new InvocationHistoryTypeMetadata(this);
        MethodInvocationImposterGroup = new MethodInvocationImposterGroupMetadata(this);
        MethodInvocationImposter = new MethodInvocationImposterMetadata(
            ReservedParameterNames,
            MemberNames
        );
        InvocationVerifierInterface = new InvocationVerifierInterfaceMetadata(this);
        MethodImposter = new MethodImposterMetadata(this);
        ImposterField = Model.IsGenericMethod
            ? new FieldMetadata(
                MethodImposter.Collection.AsField.Name,
                MethodImposter.Collection.Syntax
            )
            : new FieldMetadata(MethodImposter.AsField.Name, MethodImposter.Syntax);
        RequiresExplicitInterfaceImplementation = method.RequiresExplicitInterfaceImplementation;
        SetupName = NeedsNumberedSetup(method) ? UniqueName : Model.Name;
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

    // A member name followed by this method's type arguments, when it has any.
    internal SimpleNameSyntax WithGenericArguments(string identifier) =>
        GenericTypeArgumentListSyntax is not null
            ? SyntaxFactory.GenericName(
                SyntaxFactory.Identifier(identifier),
                GenericTypeArgumentListSyntax
            )
            : SyntaxFactory.IdentifierName(identifier);

    // A method another member's setup would collide with is set up by its unique name.
    internal static bool NeedsNumberedSetup(TargetMemberModel<MethodModel> method) =>
        method.RequiresExplicitInterfaceImplementation || method.Member.HasOverloadWithTheSameSetup;

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
}
