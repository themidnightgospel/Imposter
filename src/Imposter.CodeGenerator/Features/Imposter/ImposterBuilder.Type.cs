using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Imposter.Builders;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.InterfaceSetup.Builders;
using Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
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
    private readonly BlockBuilder _constructorBodyBuilder;
    private readonly string _invocationBehaviorParameterName;
    private readonly bool _isClassTarget;
    private readonly ImposterTargetConstructorMetadata[] _accessibleConstructors;
    private readonly NameSet _memberNameSet;
    private readonly List<InterfaceSetupMemberMetadata> _interfaceSetupMembers = [];

    private ImposterBuilder(
        ClassDeclarationBuilder imposterBuilder,
        ImposterInstanceBuilder imposterInstanceBuilder,
        string imposterName,
        TypeMetadata typeMetadata,
        BlockBuilder constructorBodyBuilder,
        string invocationBehaviorParameterName,
        bool isClassTarget,
        ImposterTargetConstructorMetadata[] accessibleConstructors,
        NameSet memberNameSet
    )
    {
        _imposterBuilder = imposterBuilder;
        _imposterInstanceBuilder = imposterInstanceBuilder;
        _imposterName = imposterName;
        _typeMetadata = typeMetadata;
        _constructorBodyBuilder = constructorBodyBuilder;
        _invocationBehaviorParameterName = invocationBehaviorParameterName;
        _isClassTarget = isClassTarget;
        _accessibleConstructors = accessibleConstructors;
        _memberNameSet = memberNameSet;
    }

    internal ImposterBuilder AddMembers(IEnumerable<MemberDeclarationSyntax>? members)
    {
        _imposterBuilder.AddMembers(members);
        return this;
    }

    internal ImposterBuilder AddMember(MemberDeclarationSyntax? member)
    {
        _imposterBuilder.AddMember(member);
        return this;
    }

    internal ImposterBuilder AddPropertyImposter(in ImposterPropertyMetadata property)
    {
        new PropertyImposterMembersBuilder(
            _imposterBuilder,
            _constructorBodyBuilder,
            _invocationBehaviorParameterName,
            _imposterInstanceBuilder
        ).AddProperty(property);

        return this;
    }

    // Null for a class target's members: only interface targets have setup views.
    internal ImposterBuilder AddInterfaceSetupMember(
        InterfaceSetupMemberModel? member,
        string setupName,
        TypeSyntax returnType,
        bool isSetUpByMethod = false
    )
    {
        if (member is not null)
        {
            _interfaceSetupMembers.Add(
                new InterfaceSetupMemberMetadata(member, setupName, returnType, isSetUpByMethod)
            );
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
            _constructorBodyBuilder,
            _imposterInstanceBuilder
        ).AddEvent(@event);

        return this;
    }

    internal ImposterBuilder AddIndexerImposter(in ImposterIndexerMetadata indexer)
    {
        new IndexerImposterMembersBuilder(
            _imposterBuilder,
            _constructorBodyBuilder,
            _invocationBehaviorParameterName,
            _imposterInstanceBuilder
        ).AddIndexer(indexer);

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
                .AddParameter(CreateInvocationBehaviorParameter(_invocationBehaviorParameterName))
                .WithBody(BuildInterfaceConstructorBody())
                .Build();
            imposterBuilder = imposterBuilder.AddMember(constructor);
        }

        return imposterBuilder.AddMember(_imposterInstanceBuilder.Build()).Build();
    }

    internal static ImposterBuilder Create(in ImposterGenerationContext imposterGenerationContext)
    {
        var typeParameters = imposterGenerationContext.Imposter.TypeParameters;

        var imposterBuilder = new ClassDeclarationBuilder(
            imposterGenerationContext.Imposter.Name,
            typeParameters.TypeParameterListSyntax
        )
            .WithTypeParameterConstraintClauses(typeParameters.ConstraintClauses)
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

        var constructorParameterName = imposterGenerationContext
            .Imposter
            .InvocationBehaviorParameterName;
        var isClassTarget = imposterGenerationContext.Imposter.IsClass;
        var accessibleConstructors = imposterGenerationContext.Imposter.AccessibleConstructors;

        var constructorBodyBuilder = CreateConstructorBodyBuilderWithoutInstanceAssignment(
            imposterGenerationContext,
            constructorParameterName
        );

        var imposterClassBuilder = imposterBuilder
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

        return new ImposterBuilder(
            imposterClassBuilder,
            imposterInstanceBuilder,
            imposterGenerationContext.Imposter.Name,
            typeMetadata,
            constructorBodyBuilder,
            constructorParameterName,
            isClassTarget,
            accessibleConstructors,
            memberNameSet
        );
    }

    internal NameSet MemberNameSet => _memberNameSet;

    private static NameSet GetImposterNameSet(
        in ImposterGenerationContext imposterGenerationContext,
        IReadOnlyList<MemberDeclarationSyntax> imposterBuilderMembers
    )
    {
        var futureMemberNames = GetMemberNames(imposterGenerationContext);

        var memberNameSeeds = MemberNamesHelper
            .GetNames(imposterBuilderMembers)
            .Concat(futureMemberNames);

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

    private static BlockBuilder CreateConstructorBodyBuilderWithoutInstanceAssignment(
        in ImposterGenerationContext imposterGenerationContext,
        string invocationBehaviorParameterName
    )
    {
        return new BlockBuilder().AddStatements(
            imposterGenerationContext.Imposter.Methods.Select(method =>
            {
                var constructorArguments = new List<ArgumentSyntax>
                {
                    Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name)),
                    Argument(IdentifierName(invocationBehaviorParameterName)),
                };

                return ThisExpression()
                    .Dot(
                        method.Model.IsGenericMethod
                            ? IdentifierName(method.MethodImposter.Collection.AsField.Name)
                            : IdentifierName(method.MethodImposter.AsField.Name)
                    )
                    .Assign(
                        (
                            method.Model.IsGenericMethod
                                ? method.MethodImposter.Collection.Syntax
                                : method.MethodImposter.Syntax
                        ).New(ArgumentList(SeparatedList(constructorArguments)))
                    )
                    .ToStatementSyntax();
            })
        );
    }

    private static ParameterSyntax CreateInvocationBehaviorParameter(string parameterName) =>
        SyntaxFactoryHelper
            .ParameterSyntax(WellKnownTypes.Imposter.Abstractions.ImposterMode, parameterName)
            .WithDefault(
                EqualsValueClause(
                    QualifiedName(
                        WellKnownTypes.Imposter.Abstractions.ImposterMode,
                        IdentifierName("Implicit")
                    )
                )
            );

    private BlockSyntax BuildInterfaceConstructorBody() =>
        _constructorBodyBuilder.Build().AddStatements(BuildInterfaceImposterInstanceAssignment());

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
                .AddParameter(CreateInvocationBehaviorParameter(_invocationBehaviorParameterName));

            var constructorBody = _constructorBodyBuilder
                .Build()
                .AddStatements(
                    BuildClassImposterInstanceAssignment(constructorMetadata.Parameters)
                );

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

        if (parameters.Length > 0)
        {
            arguments.AddRange(
                SyntaxFactoryHelper.ArgumentListSyntax(parameters, includeRefKind: true).Arguments
            );
        }

        var argumentList = SyntaxFactoryHelper.ArgumentListSyntax(arguments);

        return ThisExpression()
            .Dot(IdentifierName(_typeMetadata.ImposterInstanceFieldName))
            .Assign(IdentifierName(_typeMetadata.ImposterTargetInstanceClassName).New(argumentList))
            .ToStatementSyntax();
    }

    private static FieldDeclarationSyntax ImposterInstanceField(
        in string imposterTargetInstanceClassName,
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

        internal TypeMetadata(NameSet nameSet)
        {
            ImposterTargetInstanceClassName = nameSet.Use("ImposterTargetInstance");
            ImposterInstanceFieldName = nameSet.Use("_imposterInstance");
        }
    }
}
