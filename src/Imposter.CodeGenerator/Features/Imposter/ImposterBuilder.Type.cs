using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Imposter.Builders;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.Helpers;
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
    private readonly ConstructorBuilder _constructorBuilder;
    private readonly BlockBuilder _constructorBodyBuilder;
    private readonly string _invocationBehaviorParameterName;
    private readonly bool _isClassTarget;
    private readonly ImposterTargetConstructorMetadata[] _accessibleConstructors;
    private readonly NameSet _memberNameSet;

    private ImposterBuilder(
        ClassDeclarationBuilder imposterBuilder,
        ImposterInstanceBuilder imposterInstanceBuilder,
        string imposterName,
        TypeMetadata typeMetadata,
        ConstructorBuilder constructorBuilder,
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
        _constructorBuilder = constructorBuilder;
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
            var constructor = _constructorBuilder.WithBody(BuildInterfaceConstructorBody()).Build();
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
        var invocationBehaviorField = SyntaxFactoryHelper.SingleVariableField(
            WellKnownTypes.Imposter.Abstractions.ImposterMode,
            typeMetadata.InvocationBehaviorFieldName,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))
        );

        var constructorParameterName = "invocationBehavior";
        var isClassTarget = imposterGenerationContext.Imposter.IsClass;
        var accessibleConstructors = imposterGenerationContext.Imposter.AccessibleConstructors;

        ConstructorBuilder constructorBuilder;
        BlockBuilder constructorBodyBuilder;

        if (isClassTarget)
        {
            constructorBodyBuilder = CreateConstructorBodyBuilderWithoutInstanceAssignment(
                imposterGenerationContext,
                constructorParameterName
            );
            constructorBuilder = default;
        }
        else
        {
            (constructorBuilder, constructorBodyBuilder) = CreateConstructorBuilder(
                imposterGenerationContext,
                constructorParameterName
            );
        }

        var imposterClassBuilder = imposterBuilder
            .AddMember(invocationBehaviorField)
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
            .AddModifier(Token(SyntaxKind.PublicKeyword))
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
            constructorBuilder,
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
            imposterGenerationContext.Imposter.PropertySymbols.Select(it => it.Name)
        );
        memberNames.AddRange(
            imposterGenerationContext.Imposter.IndexerSymbols.Select(it =>
                it.IsIndexer ? "Indexer" : it.Name
            )
        );
        memberNames.AddRange(
            imposterGenerationContext.Imposter.Methods.Select(it => it.Symbol.Name)
        );
        memberNames.AddRange(imposterGenerationContext.Imposter.EventSymbols.Select(it => it.Name));

        return memberNames;
    }

    private static (
        ConstructorBuilder constructorBuilder,
        BlockBuilder bodyBuilder
    ) CreateConstructorBuilder(
        in ImposterGenerationContext imposterGenerationContext,
        string invocationBehaviorParameterName
    )
    {
        var blockBuilder = CreateConstructorBodyBuilderWithoutInstanceAssignment(
            imposterGenerationContext,
            invocationBehaviorParameterName
        );

        var constructorBuilder = new ConstructorBuilder(imposterGenerationContext.Imposter.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddParameter(CreateInvocationBehaviorParameter(invocationBehaviorParameterName));

        return (constructorBuilder, blockBuilder);
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
                        method.Symbol.IsGenericMethod
                            ? IdentifierName(method.MethodImposter.Collection.AsField.Name)
                            : IdentifierName(method.MethodImposter.AsField.Name)
                    )
                    .Assign(
                        (
                            method.Symbol.IsGenericMethod
                                ? method.MethodImposter.Collection.Syntax
                                : method.MethodImposter.Syntax
                        ).New(ArgumentList(SeparatedList(constructorArguments)))
                    )
                    .ToStatementSyntax();
            })
        );
    }

    private static ParameterSyntax CreateInvocationBehaviorParameter(string parameterName) =>
        Parameter(Identifier(parameterName))
            .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode)
            .WithDefault(
                EqualsValueClause(
                    QualifiedName(
                        WellKnownTypes.Imposter.Abstractions.ImposterMode,
                        IdentifierName("Implicit")
                    )
                )
            );

    private BlockSyntax BuildInterfaceConstructorBody() =>
        _constructorBodyBuilder
            .Build()
            .AddStatements(
                BuildInterfaceImposterInstanceAssignment(),
                BuildInvocationBehaviorAssignment()
            );

    private List<ConstructorDeclarationSyntax> BuildClassConstructors()
    {
        var constructors = new List<ConstructorDeclarationSyntax>(_accessibleConstructors.Length);

        foreach (var constructorMetadata in _accessibleConstructors)
        {
            var constructorBuilder = new ConstructorBuilder(_imposterName)
                .WithModifiers(GetConstructorModifiers(constructorMetadata.Accessibility))
                .AddParameters(
                    SyntaxFactoryHelper.ParameterSyntaxes(constructorMetadata.Parameters)
                )
                .AddParameter(CreateInvocationBehaviorParameter(_invocationBehaviorParameterName));

            var constructorBody = _constructorBodyBuilder
                .Build()
                .AddStatements(
                    BuildClassImposterInstanceAssignment(constructorMetadata.Parameters),
                    BuildInvocationBehaviorAssignment()
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
        in ImmutableArray<IParameterSymbol> parameters
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

    private ExpressionStatementSyntax BuildInvocationBehaviorAssignment() =>
        ThisExpression()
            .Dot(IdentifierName(_typeMetadata.InvocationBehaviorFieldName))
            .Assign(IdentifierName(_invocationBehaviorParameterName))
            .ToStatementSyntax();

    private static SyntaxTokenList GetConstructorModifiers(Accessibility accessibility) =>
        TokenList(Token(SyntaxKind.PublicKeyword));

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
        internal readonly string InvocationBehaviorFieldName;

        internal TypeMetadata(NameSet nameSet)
        {
            ImposterTargetInstanceClassName = nameSet.Use("ImposterTargetInstance");
            ImposterInstanceFieldName = nameSet.Use("_imposterInstance");
            InvocationBehaviorFieldName = nameSet.Use("_invocationBehavior");
        }
    }
}
