using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Imposter.Builders;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.InterfaceSetup.Builders;
using Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal readonly ref struct ImposterBuilder
{
    private readonly ClassDeclarationBuilder _imposterBuilder;
    private readonly ImposterInstanceBuilder _imposterInstanceBuilder;
    private readonly string _imposterName;
    private readonly TypeMetadata _typeMetadata;
    private readonly BlockBuilder _memberInitializationBuilder;
    private readonly BlockBuilder _clearBodyBuilder;
    private readonly ParameterMetadata _invocationBehaviorParameter;
    private readonly bool _isClassTarget;
    private readonly bool _hasModeDependentMembers;
    private readonly ImposterTargetConstructorMetadata[] _accessibleConstructors;
    private readonly NameSet _memberNameSet;
    private readonly List<InterfaceSetupMemberMetadata> _interfaceSetupMembers = [];
    private readonly MemberTypes _memberTypes = new();
    private readonly List<ExpressionSyntax> _invocationSnapshots = [];

    private ImposterBuilder(
        ClassDeclarationBuilder imposterBuilder,
        ImposterInstanceBuilder imposterInstanceBuilder,
        string imposterName,
        TypeMetadata typeMetadata,
        BlockBuilder memberInitializationBuilder,
        BlockBuilder clearBodyBuilder,
        ParameterMetadata invocationBehaviorParameter,
        bool isClassTarget,
        bool hasModeDependentMembers,
        ImposterTargetConstructorMetadata[] accessibleConstructors,
        NameSet memberNameSet
    )
    {
        _imposterBuilder = imposterBuilder;
        _imposterInstanceBuilder = imposterInstanceBuilder;
        _imposterName = imposterName;
        _typeMetadata = typeMetadata;
        _memberInitializationBuilder = memberInitializationBuilder;
        _clearBodyBuilder = clearBodyBuilder;
        _invocationBehaviorParameter = invocationBehaviorParameter;
        _isClassTarget = isClassTarget;
        _hasModeDependentMembers = hasModeDependentMembers;
        _accessibleConstructors = accessibleConstructors;
        _memberNameSet = memberNameSet;
    }

    // The types a member declares, named after its unique name.
    internal ImposterBuilder AddMemberTypes(
        string uniqueName,
        IEnumerable<MemberDeclarationSyntax?> types
    )
    {
        foreach (var type in types.OfType<MemberDeclarationSyntax>())
        {
            _memberTypes.Add(uniqueName, type);
            _imposterBuilder.AddMember(type);
        }

        return this;
    }

    // The unique names of the members whose types clash with another member of the built imposter.
    internal HashSet<string> UniqueNamesOfClashingTypes(ClassDeclarationSyntax imposter) =>
        _memberTypes.UniqueNamesOfClashingTypes(imposter);

    internal ImposterBuilder AddPropertyImposter(in ImposterPropertyMetadata property)
    {
        new PropertyImposterMembersBuilder(
            _imposterBuilder,
            _memberInitializationBuilder,
            _typeMetadata.ModeFieldAccess,
            _imposterInstanceBuilder
        ).AddProperty(property);

        _clearBodyBuilder.AddStatement(
            InvocationLifecycleBuilder.ClearChild(property.BuilderField.Name)
        );
        _invocationSnapshots.Add(
            IdentifierName(property.BuilderField.Name)
                .Dot(IdentifierName("Snapshot"))
                .Call(Argument(SyntaxFactoryHelper.False))
        );

        return this;
    }

    // Null for a class target's members: only interface targets have setup views.
    internal ImposterBuilder AddInterfaceSetupMember(
        InterfaceSetupMemberModel? member,
        string setupName,
        TypeSyntax returnType
    )
    {
        if (member is not null)
        {
            _interfaceSetupMembers.Add(
                new InterfaceSetupMemberMetadata(member, setupName, returnType)
            );
        }
        return this;
    }

    internal ImposterBuilder AddInterfaceSetupMember(
        InterfaceSetupMemberModel? member,
        in ImposterIndexerMetadata indexer
    )
    {
        if (member is not null)
        {
            _interfaceSetupMembers.Add(new InterfaceSetupMemberMetadata(member, indexer));
        }
        return this;
    }

    internal ImposterBuilder AddInterfaceSetupViews(in ImposterGenerationContext context)
    {
        if (context.Imposter.IsClass)
        {
            return this;
        }

        var members = context
            .Imposter.Methods.Select(method => new InterfaceSetupMemberMetadata(method))
            .Concat(_interfaceSetupMembers)
            .ToArray();
        var setup = new InterfaceSetupMetadata(
            context.Target.InterfaceSetup!,
            context.Target.TypeParameters.Select(parameter => parameter.Name),
            members,
            _imposterBuilder.Members
        );
        foreach (var view in setup.Views)
        {
            _imposterBuilder
                .AddBaseType(
                    SimpleBaseType(
                        QualifiedName(
                            context.Imposter.ImposterTypeSyntax,
                            IdentifierName(view.Name)
                        )
                    )
                )
                .AddMember(InterfaceSetupViewBuilder.BuildInterface(view))
                .AddMembers(InterfaceSetupViewBuilder.BuildImplementations(view))
                .AddMember(InterfaceSetupViewBuilder.BuildSelector(view, setup.SelectorName));
        }

        return this;
    }

    internal ImposterBuilder AddEventImposter(in ImposterEventMetadata @event)
    {
        new EventImposterMembersBuilder(
            _imposterBuilder,
            _memberInitializationBuilder,
            _imposterInstanceBuilder
        ).AddEvent(@event);

        _clearBodyBuilder.AddStatement(
            InvocationLifecycleBuilder.ClearChild(@event.BuilderField.Name)
        );
        _invocationSnapshots.Add(
            IdentifierName(@event.BuilderField.Name)
                .Dot(IdentifierName("Snapshot"))
                .Call(Argument(SyntaxFactoryHelper.False))
        );

        return this;
    }

    internal ImposterBuilder AddIndexerImposter(in ImposterIndexerMetadata indexer)
    {
        new IndexerImposterMembersBuilder(
            _imposterBuilder,
            _memberInitializationBuilder,
            _typeMetadata.ModeFieldAccess,
            _imposterInstanceBuilder
        ).AddIndexer(indexer);

        _clearBodyBuilder.AddStatement(
            InvocationLifecycleBuilder.ClearChild(indexer.BuilderField.Name)
        );
        _invocationSnapshots.Add(
            IdentifierName(indexer.BuilderField.Name)
                .Dot(IdentifierName("Snapshot"))
                .Call(Argument(SyntaxFactoryHelper.False))
        );

        return this;
    }

    internal ClassDeclarationSyntax Build()
    {
        var imposterBuilder = _imposterBuilder;

        if (_isClassTarget)
        {
            imposterBuilder = imposterBuilder.AddMembers(BuildClassConstructors());
        }
        else
        {
            var constructor = new ConstructorBuilder(_imposterName)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(_invocationBehaviorParameter))
                .WithBody(BuildInterfaceConstructorBody())
                .Build();
            imposterBuilder = imposterBuilder.AddMember(constructor);
        }

        return imposterBuilder
            .AddMember(
                InvocationMembersBuilder.Build(
                    _invocationSnapshots,
                    _typeMetadata.InvocationListVariableName
                )
            )
            .AddMember(
                new MethodDeclarationBuilder(
                    WellKnownTypes.Void,
                    InvocationLifecycleBuilder.ClearMethodName
                )
                    .WithExplicitInterfaceSpecifier(WellKnownTypes.Imposter.Abstractions.IImposter)
                    .WithBody(_clearBodyBuilder.Build())
                    .Build()
            )
            .AddMember(
                new MethodDeclarationBuilder(
                    WellKnownTypes.Void,
                    InvocationLifecycleBuilder.ResetMethodName
                )
                    .WithExplicitInterfaceSpecifier(WellKnownTypes.Imposter.Abstractions.IImposter)
                    .WithBody(
                        new BlockBuilder()
                            .AddStatements(_clearBodyBuilder)
                            .AddStatements(_memberInitializationBuilder)
                            .Build()
                    )
                    .Build()
            )
            .AddMember(_imposterInstanceBuilder.Build())
            .Build();
    }

    internal static ImposterBuilder Create(in ImposterGenerationContext imposterGenerationContext)
    {
        var typeParameters = imposterGenerationContext.Imposter.TypeParameters;

        var imposterBuilder = new ClassDeclarationBuilder(
            imposterGenerationContext.Imposter.Name,
            typeParameters.TypeParameterListSyntax
        )
            .AddConstraintClauses(typeParameters.ConstraintClauses)
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IImposter))
            .AddBaseType(
                SimpleBaseType(
                    WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(
                        imposterGenerationContext.Imposter.TargetTypeSyntax
                    )
                )
            )
            .AddMembers(MethodImposterMembersBuilder.BuildMethodFields(imposterGenerationContext))
            .AddMembers(
                MethodImposterMembersBuilder.BuildInvocationHistoryFields(imposterGenerationContext)
            )
            .AddMembers(
                MethodImposterMembersBuilder.BuildMethodBuilders(imposterGenerationContext)
            );

        var memberNameSet = GetImposterNameSet(imposterGenerationContext, imposterBuilder.Members);
        var typeMetadata = new TypeMetadata(memberNameSet);

        var invocationBehaviorParameter = imposterGenerationContext
            .Imposter
            .InvocationBehaviorParameter;
        var isClassTarget = imposterGenerationContext.Imposter.IsClass;
        var hasModeDependentMembers =
            imposterGenerationContext.Imposter.Methods.Count > 0
            || imposterGenerationContext.Imposter.Properties.Count > 0
            || imposterGenerationContext.Imposter.Indexers.Count > 0;
        var accessibleConstructors = imposterGenerationContext.Imposter.AccessibleConstructors;

        var memberInitializationBuilder = CreateMemberInitializationBuilder(
            imposterGenerationContext,
            typeMetadata.ModeFieldAccess
        );

        var clearBodyBuilder = new BlockBuilder().AddStatements(
            imposterGenerationContext.Imposter.Methods.Select(method =>
                IdentifierName(method.InvocationHistory.Collection.AsField.Name)
                    .Dot(IdentifierName(InvocationHistoryCollectionMetadata.ClearMethodName))
                    .Call()
                    .ToStatementSyntax()
            )
        );

        var imposterClassBuilder = imposterBuilder
            .AddMember(
                hasModeDependentMembers
                    ? SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                        WellKnownTypes.Imposter.Abstractions.ImposterMode,
                        typeMetadata.ModeFieldName
                    )
                    : null
            )
            .AddMember(
                ImposterInstanceField(
                    typeMetadata.ImposterTargetInstanceClassName,
                    typeMetadata.ImposterInstanceFieldName
                )
            )
            .AddMember(
                ImposterInstanceMembersBuilder.InstanceMethod(
                    imposterGenerationContext,
                    typeMetadata.ImposterInstanceFieldName
                )
            )
            .AddModifier(
                Token(
                    imposterGenerationContext.Imposter.DeclaredAccessibility == Accessibility.Public
                        ? SyntaxKind.PublicKeyword
                        : SyntaxKind.InternalKeyword
                )
            )
            .AddModifier(Token(SyntaxKind.SealedKeyword));

        var imposterInstanceBuilder = ImposterInstanceBuilder.Create(
            imposterGenerationContext,
            typeMetadata.ImposterTargetInstanceClassName
        );

        var result = new ImposterBuilder(
            imposterClassBuilder,
            imposterInstanceBuilder,
            imposterGenerationContext.Imposter.Name,
            typeMetadata,
            memberInitializationBuilder,
            clearBodyBuilder,
            invocationBehaviorParameter,
            isClassTarget,
            hasModeDependentMembers,
            accessibleConstructors,
            memberNameSet
        );
        foreach (var method in imposterGenerationContext.Imposter.Methods)
        {
            result._invocationSnapshots.Add(
                IdentifierName(method.InvocationHistory.Collection.AsField.Name)
                    .Dot(IdentifierName("Snapshot"))
                    .Call(Argument(SyntaxFactoryHelper.False))
            );
        }
        return result;
    }

    internal NameSet MemberNameSet => _memberNameSet;

    private static NameSet GetImposterNameSet(
        in ImposterGenerationContext imposterGenerationContext,
        IReadOnlyList<MemberDeclarationSyntax> imposterBuilderMembers
    )
    {
        var futureMemberNames = GetMemberNames(imposterGenerationContext);

        // The imposter declares the target's type parameters, which none of its members can be named after.
        var memberNameSeeds = MemberNamesHelper
            .GetNames(imposterBuilderMembers)
            .Concat(futureMemberNames)
            .Concat(imposterGenerationContext.Target.TypeParameters.Select(it => it.Name));

        return new NameSet(memberNameSeeds);
    }

    private static List<string> GetMemberNames(
        in ImposterGenerationContext imposterGenerationContext
    )
    {
        var memberNames = new List<string>();

        memberNames.AddRange(
            imposterGenerationContext.Imposter.Properties.Select(it => it.Member.Name)
        );
        memberNames.AddRange(
            imposterGenerationContext.Imposter.Indexers.Select(_ =>
                ImposterTargetMetadata.IndexerMemberName
            )
        );
        memberNames.AddRange(
            imposterGenerationContext.Imposter.Methods.Select(it => it.Model.Name)
        );
        memberNames.AddRange(
            imposterGenerationContext.Imposter.Events.Select(it => it.Member.Name)
        );

        return memberNames;
    }

    private static BlockBuilder CreateMemberInitializationBuilder(
        in ImposterGenerationContext imposterGenerationContext,
        ExpressionSyntax invocationBehavior
    )
    {
        return new BlockBuilder()
            .AddStatements(
                imposterGenerationContext.Imposter.Methods.Select(method =>
                    ThisExpression()
                        .Dot(IdentifierName(method.InvocationHistory.Collection.AsField.Name))
                        .Assign(method.InvocationHistory.Collection.Syntax.New())
                        .ToStatementSyntax()
                )
            )
            .AddStatements(
                imposterGenerationContext.Imposter.Methods.Select(method =>
                {
                    var constructorArguments = new List<ArgumentSyntax>
                    {
                        Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name)),
                        Argument(invocationBehavior),
                    };

                    return ThisExpression()
                        .Dot(IdentifierName(method.ImposterField.Name))
                        .Assign(
                            method.ImposterField.Type.New(
                                SyntaxFactoryHelper.ArgumentListSyntax(constructorArguments)
                            )
                        )
                        .ToStatementSyntax();
                })
            );
    }

    private BlockBuilder CreateConstructorBodyBuilder() =>
        new BlockBuilder()
            .AddStatement(
                _hasModeDependentMembers
                    ? _typeMetadata
                        .ModeFieldAccess.Assign(IdentifierName(_invocationBehaviorParameter.Name))
                        .ToStatementSyntax()
                    : null
            )
            .AddStatements(_memberInitializationBuilder);

    private BlockSyntax BuildInterfaceConstructorBody() =>
        CreateConstructorBodyBuilder()
            .AddStatement(BuildInterfaceImposterInstanceAssignment())
            .Build();

    private List<ConstructorDeclarationSyntax> BuildClassConstructors()
    {
        var constructors = new List<ConstructorDeclarationSyntax>(_accessibleConstructors.Length);

        foreach (var constructorMetadata in _accessibleConstructors)
        {
            var constructorBuilder = new ConstructorBuilder(_imposterName)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .AddParameters(
                    SyntaxFactoryHelper.ParameterSyntaxes(constructorMetadata.Parameters)
                )
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(_invocationBehaviorParameter));

            var constructorBody = CreateConstructorBodyBuilder()
                .AddStatement(BuildClassImposterInstanceAssignment(constructorMetadata.Parameters))
                .Build();

            constructors.Add(constructorBuilder.WithBody(constructorBody).Build());
        }

        return constructors;
    }

    private ExpressionStatementSyntax BuildInterfaceImposterInstanceAssignment() =>
        ThisExpression()
            .Dot(IdentifierName(_typeMetadata.ImposterInstanceFieldName))
            .Assign(
                IdentifierName(_typeMetadata.ImposterTargetInstanceClassName)
                    .New(ThisExpression().ToSingleArgumentList())
            )
            .ToStatementSyntax();

    private ExpressionStatementSyntax BuildClassImposterInstanceAssignment(
        in ImmutableArray<ParameterModel> parameters
    )
    {
        var arguments = new List<ArgumentSyntax>(parameters.Length + 1)
        {
            Argument(ThisExpression()),
        };

        arguments.AddRange(SyntaxFactoryHelper.ArgumentListSyntax(parameters).Arguments);

        var argumentList = SyntaxFactoryHelper.ArgumentListSyntax(arguments);

        return ThisExpression()
            .Dot(IdentifierName(_typeMetadata.ImposterInstanceFieldName))
            .Assign(IdentifierName(_typeMetadata.ImposterTargetInstanceClassName).New(argumentList))
            .ToStatementSyntax();
    }

    private static FieldDeclarationSyntax ImposterInstanceField(
        string imposterTargetInstanceClassName,
        string imposterInstanceFieldName
    ) =>
        SyntaxFactoryHelper.SingleVariableField(
            IdentifierName(imposterTargetInstanceClassName),
            imposterInstanceFieldName,
            SyntaxKind.PrivateKeyword
        );

    private readonly struct TypeMetadata
    {
        internal readonly string ImposterTargetInstanceClassName;
        internal readonly string ImposterInstanceFieldName;
        internal readonly string ModeFieldName;
        internal readonly MemberAccessExpressionSyntax ModeFieldAccess;
        internal readonly string InvocationListVariableName;

        internal TypeMetadata(NameSet nameSet)
        {
            ImposterTargetInstanceClassName = nameSet.Use("ImposterTargetInstance");
            ImposterInstanceFieldName = nameSet.Use("_imposterInstance");
            ModeFieldName = nameSet.Use("_imposterMode");
            ModeFieldAccess = ThisExpression().Dot(IdentifierName(ModeFieldName));
            InvocationListVariableName = nameSet.Use("invocations");
        }
    }
}
