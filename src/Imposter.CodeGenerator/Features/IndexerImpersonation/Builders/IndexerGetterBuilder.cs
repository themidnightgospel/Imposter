using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using GetterReturnsMetadata = Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface.ReturnsMethodMetadata;
using GetterThrowsMetadata = Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface.ThrowsMethodMetadata;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerGetterBuilder
{
    internal static MethodDeclarationSyntax BuildGetForwarder(in ImposterIndexerMetadata indexer)
    {
        var parameters = new List<ParameterSyntax>(indexer.Core.ParameterSyntaxes);

        ParameterSyntax? getterBaseImplementationParameter = null;
        if (indexer.Core.GetterSupportsBaseImplementation)
        {
            getterBaseImplementationParameter = ParameterSyntax(
                    indexer.Core.AsSystemFuncType.ToNullableType(),
                    indexer.GetterImplementation.BaseImplementationParameterName
                )
                .WithDefault(EqualsValueClause(Null));

            parameters.Add(getterBaseImplementationParameter);
        }

        var invocationArguments = new List<ArgumentSyntax>(indexer.Core.ParameterArguments);

        if (
            indexer.Core.GetterSupportsBaseImplementation
            && getterBaseImplementationParameter is not null
        )
        {
            invocationArguments.Add(
                Argument(IdentifierName(getterBaseImplementationParameter.Identifier))
            );
        }

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters.ToArray())
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(indexer.Builder.GetterImposterField.Name)
                            .Dot(IdentifierName("Get"))
                            .Call(ArgumentListSyntax(invocationArguments))
                    )
                )
            )
            .Build();
    }

    internal static ClassDeclarationSyntax BuildGetterImposter(in ImposterIndexerMetadata indexer)
    {
        var getterInvocationType = IdentifierName(indexer.GetterImplementation.Invocation.Name);
        var getterImposterBuilder = new ClassDeclarationBuilder("GetterImposter")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.DefaultIndexerBehaviour.TypeSyntax,
                    indexer.GetterImplementation.DefaultBehaviourField.Name
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                        getterInvocationType
                    ),
                    indexer.GetterImplementation.SetupsField.Name,
                    WellKnownTypes
                        .System.Collections.Concurrent.ConcurrentStack(getterInvocationType)
                        .New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(
                        indexer.ArgumentsCriteria.TypeSyntax,
                        getterInvocationType
                    ),
                    indexer.GetterImplementation.SetupLookupField.Name,
                    WellKnownTypes
                        .System.Collections.Concurrent.ConcurrentDictionary(
                            indexer.ArgumentsCriteria.TypeSyntax,
                            getterInvocationType
                        )
                        .New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                        indexer.Arguments.TypeSyntax
                    ),
                    indexer.GetterImplementation.InvocationHistoryField.Name,
                    WellKnownTypes
                        .System.Collections.Concurrent.ConcurrentStack(indexer.Arguments.TypeSyntax)
                        .New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    WellKnownTypes.Imposter.Abstractions.ImposterMode,
                    indexer.GetterImplementation.InvocationBehaviorField.Name
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    WellKnownTypes.String,
                    indexer.GetterImplementation.PropertyDisplayNameField.Name
                )
            )
            .AddMember(
                SingleVariableField(
                    WellKnownTypes.Bool,
                    indexer.GetterImplementation.HasConfiguredReturnField.Name,
                    SyntaxKind.PrivateKeyword
                )
            )
            .AddMember(BuildGetterConstructor(indexer))
            .AddMember(BuildGetterGetMethod(indexer))
            .AddMember(BuildFindGetterInvocationImposterMethod(indexer))
            .AddMember(BuildGetOrCreateMethod(indexer))
            .AddMember(BuildGetterCalledMethod(indexer))
            .AddMember(
                new PropertyDeclarationBuilder(WellKnownTypes.String, "PropertyDisplayName")
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .Build()
                    .WithAccessorList(null)
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            IdentifierName(
                                indexer.GetterImplementation.PropertyDisplayNameField.Name
                            )
                        )
                    )
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            )
            .AddMember(BuildMarkReturnConfiguredMethod(indexer))
            .AddMember(BuildEnsureGetterConfiguredMethod(indexer))
            .AddMember(BuildGetterBuilder(indexer))
            .AddMember(BuildGetterInvocationImposter(indexer));

        return getterImposterBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildGetterConstructor(
        in ImposterIndexerMetadata indexer
    ) =>
        BuildImposterConstructor(
            "GetterImposter",
            indexer.DefaultIndexerBehaviour.TypeSyntax,
            indexer.GetterImplementation.DefaultBehaviourField.Name,
            indexer.GetterImplementation.InvocationBehaviorField.Name,
            indexer.GetterImplementation.PropertyDisplayNameField.Name
        );

    private static ClassDeclarationSyntax BuildGetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.GetterImplementation.Builder;
        var returnsMetadata = indexer.GetterBuilderInterface.ReturnsMethod;
        var throwsMetadata = indexer.GetterBuilderInterface.ThrowsMethod;

        return new ClassDeclarationBuilder(builderMetadata.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.FluentInterfaceTypeSyntax))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.GetterImplementation.TypeSyntax,
                    builderMetadata.ImposterFieldName
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    builderMetadata.CriteriaFieldName
                )
            )
            .AddMember(
                new ConstructorBuilder(builderMetadata.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .AddParameter(
                        ParameterSyntax(
                            indexer.GetterImplementation.TypeSyntax,
                            builderMetadata.ImposterFieldName
                        )
                    )
                    .AddParameter(
                        ParameterSyntax(
                            indexer.ArgumentsCriteria.TypeSyntax,
                            builderMetadata.CriteriaFieldName
                        )
                    )
                    .WithBody(
                        new BlockBuilder()
                            .AddStatement(
                                ThisExpression()
                                    .Dot(IdentifierName(builderMetadata.ImposterFieldName))
                                    .Assign(IdentifierName(builderMetadata.ImposterFieldName))
                                    .ToStatementSyntax()
                            )
                            .AddStatement(
                                ThisExpression()
                                    .Dot(IdentifierName(builderMetadata.CriteriaFieldName))
                                    .Assign(IdentifierName(builderMetadata.CriteriaFieldName))
                                    .ToStatementSyntax()
                            )
                            .Build()
                    )
                    .Build()
            )
            .AddMember(BuildGetterBuilderInvocationProperty(indexer))
            .AddMember(BuildGetterBuilderReturnsValueMethod(indexer, returnsMetadata))
            .AddMember(BuildGetterBuilderReturnsFuncMethod(indexer, returnsMetadata))
            .AddMember(BuildGetterBuilderReturnsDelegateMethod(indexer, returnsMetadata))
            .AddMember(BuildGetterBuilderThrowsExceptionMethod(indexer, throwsMetadata))
            .AddMember(BuildGetterBuilderThrowsGenericExceptionMethod(indexer, throwsMetadata))
            .AddMember(BuildGetterBuilderThrowsDelegateMethod(indexer, throwsMetadata))
            .AddMember(BuildGetterBuilderCallbackMethod(indexer))
            .AddMember(BuildGetterBuilderCalledMethod(indexer))
            .AddMember(BuildGetterBuilderThenMethod(indexer))
            .AddMember(
                indexer.GetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildGetterBuilderUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static PropertyDeclarationSyntax BuildGetterBuilderInvocationProperty(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.GetterImplementation.Builder;
        return new PropertyDeclarationBuilder(
            indexer.GetterImplementation.Invocation.TypeSyntax,
            builderMetadata.InvocationImposterPropertyName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(
                ArrowExpressionClause(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("GetOrCreate"))
                        .Call(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(IdentifierName(builderMetadata.CriteriaFieldName))
                                )
                            )
                        )
                )
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsValueMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returnsMetadata
    )
    {
        var parameter = ParameterSyntax(returnsMetadata.ValueParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            IdentifierName(parameter.Identifier)
        );

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .WithExplicitInterfaceSpecifier(returnsMetadata.InterfaceSyntax)
            .AddParameter(parameter)
            .WithBody(
                Block(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsFuncMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returnsMetadata
    )
    {
        var parameter = ParameterSyntax(returnsMetadata.FuncParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            IdentifierName(parameter.Identifier).Call()
        );

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .WithExplicitInterfaceSpecifier(returnsMetadata.InterfaceSyntax)
            .AddParameter(parameter)
            .WithBody(
                Block(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returnsMetadata
    )
    {
        var parameter = ParameterSyntax(returnsMetadata.DelegateParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            IdentifierName(parameter.Identifier)
                .Call(
                    BuildDelegateInvocationArguments(
                        IdentifierName(indexer.GetterImplementation.ArgumentsVariableName),
                        indexer
                    )
                )
        );

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .WithExplicitInterfaceSpecifier(returnsMetadata.InterfaceSyntax)
            .AddParameter(parameter)
            .WithBody(
                Block(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata
    )
    {
        var parameter = ParameterSyntax(throwsMetadata.ExceptionParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            ThrowExpression(IdentifierName(parameter.Identifier))
        );

        return new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .WithExplicitInterfaceSpecifier(throwsMetadata.InterfaceSyntax)
            .AddParameter(parameter)
            .WithBody(
                Block(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsGenericExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata
    ) =>
        new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .WithExplicitInterfaceSpecifier(throwsMetadata.InterfaceSyntax)
            .WithTypeParameters(
                TypeParameterList(
                    SingletonSeparatedList(TypeParameter(throwsMetadata.GenericTypeParameterName))
                )
            )
            .WithBody(
                Block(
                    InvocationImposterAddReturnValue(
                        indexer.GetterImplementation.Builder,
                        SimpleLambdaExpression(
                            Parameter(
                                Identifier(indexer.GetterImplementation.ArgumentsVariableName)
                            ),
                            ThrowExpression(
                                IdentifierName(throwsMetadata.GenericTypeParameterName).New()
                            )
                        )
                    ),
                    ReturnThis
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata
    )
    {
        var parameter = ParameterSyntax(throwsMetadata.DelegateParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            ThrowExpression(
                IdentifierName(parameter.Identifier)
                    .Call(
                        BuildDelegateInvocationArguments(
                            IdentifierName(indexer.GetterImplementation.ArgumentsVariableName),
                            indexer
                        )
                    )
            )
        );

        return new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .WithExplicitInterfaceSpecifier(throwsMetadata.InterfaceSyntax)
            .AddParameter(parameter)
            .WithBody(
                Block(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var parameter = ParameterSyntax(
            indexer.GetterBuilderInterface.CallbackMethod.CallbackParameter
        );
        return new MethodDeclarationBuilder(
            indexer.GetterBuilderInterface.CallbackMethod.ReturnType,
            indexer.GetterBuilderInterface.CallbackMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                indexer.GetterBuilderInterface.CallbackMethod.InterfaceSyntax
            )
            .AddParameter(parameter)
            .WithBody(
                Block(
                    InvocationImposterAddCallback(
                        indexer.GetterImplementation.Builder,
                        IdentifierName(parameter.Identifier)
                    ),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.GetterImplementation.Builder;
        var parameter = ParameterSyntax(indexer.GetterBuilderInterface.CalledMethod.CountParameter);

        return new MethodDeclarationBuilder(
            indexer.GetterBuilderInterface.CalledMethod.ReturnType,
            indexer.GetterBuilderInterface.CalledMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax
            )
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("Called"))
                        .Call([
                            Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                            Argument(IdentifierName(parameter.Identifier)),
                        ])
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThenMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var thenMetadata = indexer.GetterBuilderInterface.ThenMethod;
        return new MethodDeclarationBuilder(thenMetadata.ReturnType, thenMetadata.Name)
            .WithExplicitInterfaceSpecifier(thenMetadata.InterfaceSyntax)
            .WithBody(Block(ReturnThis))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var metadata = indexer.GetterBuilderInterface.UseBaseImplementationMethod!.Value;
        return new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .WithExplicitInterfaceSpecifier(metadata.InterfaceSyntax)
            .WithBody(
                Block(
                    InvocationImposterUseBaseImplementation(indexer.GetterImplementation.Builder),
                    ReturnThis
                )
            )
            .Build();
    }

    private static ExpressionStatementSyntax InvocationImposterAddReturnValue(
        IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata,
        ExpressionSyntax lambda
    ) =>
        IdentifierName(builderMetadata.InvocationImposterPropertyName)
            .Dot(IdentifierName("AddReturnValue"))
            .Call(Argument(lambda))
            .ToStatementSyntax();

    private static ExpressionStatementSyntax InvocationImposterAddCallback(
        IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata,
        ExpressionSyntax callback
    ) =>
        IdentifierName(builderMetadata.InvocationImposterPropertyName)
            .Dot(IdentifierName("AddCallback"))
            .Call(Argument(callback))
            .ToStatementSyntax();

    private static ExpressionStatementSyntax InvocationImposterUseBaseImplementation(
        IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata
    ) =>
        IdentifierName(builderMetadata.InvocationImposterPropertyName)
            .Dot(IdentifierName("UseBaseImplementation"))
            .Call()
            .ToStatementSyntax();

    private static ClassDeclarationSyntax BuildGetterInvocationImposter(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;

        return new ClassDeclarationBuilder(invocationMetadata.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(SinglePrivateReadonlyVariableField(invocationMetadata.ParentField))
            .AddMember(SinglePrivateReadonlyVariableField(invocationMetadata.DefaultBehaviourField))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    invocationMetadata.ReturnValuesField,
                    invocationMetadata.ReturnValuesField.Type.New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    invocationMetadata.CallbacksField,
                    invocationMetadata.CallbacksField.Type.New()
                )
            )
            .AddMember(
                SingleVariableField(
                    invocationMetadata.LastReturnValueField.Type,
                    invocationMetadata.LastReturnValueField.Name,
                    TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.VolatileKeyword))
                )
            )
            .AddMember(
                SingleVariableField(
                    invocationMetadata.PropertyDisplayNameField.Type,
                    invocationMetadata.PropertyDisplayNameField.Name,
                    SyntaxKind.PrivateKeyword
                )
            )
            .AddMember(
                new PropertyDeclarationBuilder(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    invocationMetadata.CriteriaField.Name
                )
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .Build()
                    .WithAccessorList(
                        AccessorList(
                            List([
                                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                    .AddModifiers(Token(SyntaxKind.PrivateKeyword))
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            ])
                        )
                    )
            )
            .AddMember(BuildGetterInvocationConstructor(indexer))
            .AddMember(BuildGetterInvocationAddReturnValueMethod(indexer))
            .AddMember(BuildGetterInvocationAddCallbackMethod(indexer))
            .AddMember(BuildGetterInvocationInvokeMethod(indexer))
            .AddMember(BuildGetterInvocationResolveNextGeneratorMethod(indexer))
            .AddMember(BuildGetterInvocationNextReturnValueMethod(invocationMetadata))
            .AddMember(
                indexer.GetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildGetterInvocationUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildGetterInvocationConstructor(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;

        return new ConstructorBuilder(invocationMetadata.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(ParameterSyntax(indexer.GetterImplementation.TypeSyntax, "parent"))
            .AddParameter(
                ParameterSyntax(
                    indexer.DefaultIndexerBehaviour.TypeSyntax,
                    DefaultBehaviourParameterName
                )
            )
            .AddParameter(
                ParameterSyntax(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    indexer.GetterImplementation.CriteriaParameterName
                )
            )
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocationMetadata.ParentField.Name))
                            .Assign(IdentifierName("parent"))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocationMetadata.DefaultBehaviourField.Name))
                            .Assign(IdentifierName(DefaultBehaviourParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocationMetadata.PropertyDisplayNameField.Name))
                            .Assign(
                                IdentifierName("parent").Dot(IdentifierName("PropertyDisplayName"))
                            )
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocationMetadata.CriteriaField.Name))
                            .Assign(
                                IdentifierName(indexer.GetterImplementation.CriteriaParameterName)
                            )
                            .ToStatementSyntax()
                    )
                    .Build()
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationAddReturnValueMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var builderMetadata = indexer.GetterImplementation.Builder;
        var parameter = ParameterSyntax(builderMetadata.ReturnGeneratorType, "generator");
        var handlerLambda = ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList<ParameterSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Parameter(
                                Identifier(indexer.GetterImplementation.ArgumentsVariableName)
                            ),
                            Token(SyntaxKind.CommaToken),
                            Parameter(
                                Identifier(
                                    indexer.GetterImplementation.BaseImplementationParameterName
                                )
                            ),
                        }
                    )
                )
            )
            .WithExpressionBody(
                IdentifierName(parameter.Identifier)
                    .Call(
                        ArgumentListSyntax([
                            Argument(
                                IdentifierName(indexer.GetterImplementation.ArgumentsVariableName)
                            ),
                        ])
                    )
            );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "AddReturnValue")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                        .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName))
                        .Assign(False)
                        .ToStatementSyntax(),
                    IdentifierName(invocationMetadata.ReturnValuesField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(handlerLambda))
                        .ToStatementSyntax(),
                    IdentifierName(invocationMetadata.ParentField.Name)
                        .Dot(IdentifierName("MarkReturnConfigured"))
                        .Call()
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationAddCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var parameter = ParameterSyntax(
            indexer.GetterBuilderInterface.CallbackMethod.CallbackParameter
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "AddCallback")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(invocationMetadata.CallbacksField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(IdentifierName(parameter.Identifier)))
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationInvokeMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var argumentsParameterName = indexer.GetterImplementation.ArgumentsVariableName;
        var argumentsParameter = ParameterSyntax(
            indexer.Arguments.TypeSyntax,
            argumentsParameterName
        );
        var baseImplementationParameter = ParameterSyntax(
                indexer.Core.AsSystemFuncType.ToNullableType(),
                indexer.GetterImplementation.BaseImplementationParameterName
            )
            .WithDefault(EqualsValueClause(Null));

        var foreachCallbacks = ForEachStatement(
            Var,
            Identifier("callback"),
            IdentifierName(invocationMetadata.CallbacksField.Name),
            Block(
                IdentifierName("callback")
                    .Call(
                        BuildDelegateInvocationArguments(
                            IdentifierName(argumentsParameterName),
                            indexer
                        )
                    )
                    .ToStatementSyntax()
            )
        );

        var generatorDeclaration = LocalVariableDeclarationSyntax(
            indexer.GetterImplementation.ReturnHandlerType,
            "generator",
            IdentifierName("ResolveNextGenerator")
                .Call(ArgumentListSyntax([Argument(IdentifierName(argumentsParameterName))]))
        );

        var returnStatement = ReturnStatement(
            IdentifierName("generator")
                .Call(
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                Argument(IdentifierName(argumentsParameterName)),
                                Token(SyntaxKind.CommaToken),
                                Argument(
                                    IdentifierName(
                                        indexer.GetterImplementation.BaseImplementationParameterName
                                    )
                                ),
                            }
                        )
                    )
                )
        );

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParameter)
            .AddParameter(baseImplementationParameter)
            .WithBody(Block(foreachCallbacks, generatorDeclaration, returnStatement))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationResolveNextGeneratorMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var argumentsParameterName = indexer.GetterImplementation.ArgumentsVariableName;
        var argumentsParameter = ParameterSyntax(
            indexer.Arguments.TypeSyntax,
            argumentsParameterName
        );
        const string NextReturnValueVariableName = "nextReturnValue";

        var defaultBehaviourHandler = ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList<ParameterSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Parameter(Identifier(argumentsParameterName)),
                            Token(SyntaxKind.CommaToken),
                            Parameter(
                                Identifier(
                                    indexer.GetterImplementation.BaseImplementationParameterName
                                )
                            ),
                        }
                    )
                )
            )
            .WithExpressionBody(
                IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                    .Dot(IdentifierName("Get"))
                    .Call(
                        ArgumentListSyntax([
                            Argument(IdentifierName(argumentsParameterName)),
                            Argument(
                                IdentifierName(
                                    indexer.GetterImplementation.BaseImplementationParameterName
                                )
                            ),
                        ])
                    )
            );

        var defaultBehaviourCheck = IfStatement(
            IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)),
            Block(ReturnStatement(defaultBehaviourHandler))
        );

        var declareNextReturnValue = LocalVariableDeclarationSyntax(
            Var,
            NextReturnValueVariableName,
            IdentifierName(invocationMetadata.NextReturnValueMethod.Name).Call()
        );

        var throwIfMissing = IfStatement(
            IdentifierName(NextReturnValueVariableName).IsNull(),
            Block(
                BuildMissingImposterThrow(
                    invocationMetadata.PropertyDisplayNameField.Name,
                    indexer.GetterImplementation.GetterSuffix
                )
            )
        );

        var returnNext = ReturnStatement(
            PostfixUnaryExpression(
                SyntaxKind.SuppressNullableWarningExpression,
                IdentifierName(NextReturnValueVariableName)
            )
        );

        return new MethodDeclarationBuilder(
            indexer.GetterImplementation.ReturnHandlerType,
            "ResolveNextGenerator"
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(argumentsParameter)
            .WithBody(
                Block(defaultBehaviourCheck, declareNextReturnValue, throwIfMissing, returnNext)
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationNextReturnValueMethod(
        in IndexerGetterImposterMetadata.GetterInvocationMetadata invocationMetadata
    ) =>
        new MethodDeclarationBuilder(
            invocationMetadata.NextReturnValueMethod.ReturnType,
            invocationMetadata.NextReturnValueMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                NextOutcomeSyntaxHelper.TakeNextOutcomeBody(
                    IdentifierName(invocationMetadata.ReturnValuesField.Name),
                    IdentifierName(invocationMetadata.LastReturnValueField.Name),
                    IdentifierName("returnValue")
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterInvocationUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var messageExpression = IdentifierName(invocationMetadata.PropertyDisplayNameField.Name)
            .Add(indexer.GetterImplementation.GetterSuffix.StringLiteral());

        var handlerLambda = ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList<ParameterSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Parameter(
                                Identifier(indexer.GetterImplementation.ArgumentsVariableName)
                            ),
                            Token(SyntaxKind.CommaToken),
                            Parameter(
                                Identifier(
                                    indexer.GetterImplementation.BaseImplementationParameterName
                                )
                            ),
                        }
                    )
                )
            )
            .WithBlock(
                Block(
                    IfStatement(
                        IdentifierName(indexer.GetterImplementation.BaseImplementationParameterName)
                            .IsNull(),
                        Block(
                            ThrowStatement(
                                ObjectCreationExpression(
                                        WellKnownTypes
                                            .Imposter
                                            .Abstractions
                                            .MissingImposterException
                                    )
                                    .WithArgumentList(
                                        ArgumentList(
                                            SingletonSeparatedList(Argument(messageExpression))
                                        )
                                    )
                            )
                        )
                    ),
                    ReturnStatement(
                        IdentifierName(indexer.GetterImplementation.BaseImplementationParameterName)
                            .Call(EmptyArgumentListSyntax)
                    )
                )
            );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "UseBaseImplementation")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    IdentifierName(invocationMetadata.ParentField.Name)
                        .Dot(IdentifierName("MarkReturnConfigured"))
                        .Call()
                        .ToStatementSyntax(),
                    IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                        .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName))
                        .Assign(False)
                        .ToStatementSyntax(),
                    IdentifierName(invocationMetadata.ReturnValuesField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(handlerLambda))
                        .ToStatementSyntax(),
                    IdentifierName(invocationMetadata.LastReturnValueField.Name)
                        .Assign(Null)
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterGetMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsIdentifier = IdentifierName(
            indexer.GetterImplementation.ArgumentsVariableName
        );
        var baseImplementationIdentifier = IdentifierName(
            indexer.GetterImplementation.BaseImplementationParameterName
        );
        var invocationBehaviorIdentifier = IdentifierName(
            indexer.GetterImplementation.InvocationBehaviorField.Name
        );
        var explicitInvocationBehavior = WellKnownTypes.Imposter.Abstractions.ImposterMode.Dot(
            IdentifierName("Explicit")
        );
        var parameters = new List<ParameterSyntax>(indexer.Core.ParameterSyntaxes)
        {
            ParameterSyntax(
                    indexer.Core.AsSystemFuncType.ToNullableType(),
                    indexer.GetterImplementation.BaseImplementationParameterName
                )
                .WithDefault(EqualsValueClause(Null)),
        };

        var tryStatements = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(
                Var,
                indexer.GetterImplementation.SetupVariableName,
                IdentifierName(indexer.GetterImplementation.FindGetterInvocationImposterMethodName)
                    .Call(Argument(argumentsIdentifier))
            ),
            IfStatement(
                IsPatternExpression(
                    IdentifierName(indexer.GetterImplementation.SetupVariableName),
                    ConstantPattern(Null)
                ),
                Block(
                    IdentifierName(indexer.GetterImplementation.EnsureGetterConfiguredMethodName)
                        .Call()
                        .ToStatementSyntax(),
                    IfStatement(
                        IdentifierName(indexer.GetterImplementation.DefaultBehaviourField.Name)
                            .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)),
                        ReturnStatement(
                            IdentifierName(indexer.GetterImplementation.DefaultBehaviourField.Name)
                                .Dot(IdentifierName("Get"))
                                .Call(
                                    ArgumentListSyntax([
                                        Argument(argumentsIdentifier),
                                        Argument(baseImplementationIdentifier),
                                    ])
                                )
                        )
                    ),
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            invocationBehaviorIdentifier,
                            explicitInvocationBehavior
                        ),
                        Block(
                            BuildMissingImposterThrow(
                                indexer.GetterImplementation.PropertyDisplayNameField.Name,
                                indexer.GetterImplementation.GetterSuffix
                            )
                        ),
                        ElseClause(Block(ReturnDefaultNonNullable))
                    )
                )
            ),
            ReturnStatement(
                IdentifierName(indexer.GetterImplementation.SetupVariableName)
                    .Dot(IdentifierName("Invoke"))
                    .Call(
                        ArgumentListSyntax([
                            Argument(argumentsIdentifier),
                            Argument(baseImplementationIdentifier),
                        ])
                    )
            ),
        };

        var finallyClause = FinallyClause(
            Block(
                IdentifierName(indexer.GetterImplementation.InvocationHistoryField.Name)
                    .Dot(ConcurrentStackSyntaxHelper.Push)
                    .Call(Argument(argumentsIdentifier))
                    .ToStatementSyntax()
            )
        );

        var body = Block(
            CreateArgumentsDeclaration(indexer, indexer.GetterImplementation.ArgumentsVariableName),
            TryStatement(Block(tryStatements), default, finallyClause)
        );

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters.ToArray())
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildFindGetterInvocationImposterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getterInvocationType = NullableType(
            IdentifierName(indexer.GetterImplementation.Invocation.Name)
        );

        return new MethodDeclarationBuilder(
            getterInvocationType,
            indexer.GetterImplementation.FindGetterInvocationImposterMethodName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(
                    indexer.Arguments.TypeSyntax,
                    indexer.GetterImplementation.ArgumentsVariableName
                )
            )
            .WithBody(
                Block(
                    ForEachStatement(
                        Var,
                        Identifier(indexer.GetterImplementation.SetupVariableName),
                        IdentifierName(indexer.GetterImplementation.SetupsField.Name),
                        Block(
                            IfStatement(
                                IdentifierName(indexer.GetterImplementation.SetupVariableName)
                                    .Dot(IdentifierName("Criteria"))
                                    .Dot(IdentifierName("Matches"))
                                    .Call(
                                        ArgumentList(
                                            SingletonSeparatedList(
                                                Argument(
                                                    IdentifierName(
                                                        indexer
                                                            .GetterImplementation
                                                            .ArgumentsVariableName
                                                    )
                                                )
                                            )
                                        )
                                    ),
                                Block(
                                    ReturnStatement(
                                        IdentifierName(
                                            indexer.GetterImplementation.SetupVariableName
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    ReturnStatement(Null)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetOrCreateMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getterInvocationType = IdentifierName(indexer.GetterImplementation.Invocation.Name);

        return new MethodDeclarationBuilder(getterInvocationType, "GetOrCreate")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    indexer.GetterImplementation.CriteriaParameterName
                )
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(indexer.GetterImplementation.SetupLookupField.Name)
                            .Dot(IdentifierName("GetOrAdd"))
                            .Call(
                                ArgumentList(
                                    SeparatedList<ArgumentSyntax>(
                                        new SyntaxNodeOrToken[]
                                        {
                                            Argument(
                                                IdentifierName(
                                                    indexer
                                                        .GetterImplementation
                                                        .CriteriaParameterName
                                                )
                                            ),
                                            Token(SyntaxKind.CommaToken),
                                            Argument(IdentifierName("CreateSetup")),
                                        }
                                    )
                                )
                            )
                    ),
                    LocalFunctionStatement(getterInvocationType, Identifier("CreateSetup"))
                        .AddParameterListParameters(
                            ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, "key")
                        )
                        .WithBody(
                            Block(
                                LocalVariableDeclarationSyntax(
                                    getterInvocationType,
                                    indexer.GetterImplementation.SetupVariableName,
                                    getterInvocationType.New(
                                        ArgumentListSyntax([
                                            Argument(ThisExpression()),
                                            Argument(
                                                IdentifierName(
                                                    indexer
                                                        .GetterImplementation
                                                        .DefaultBehaviourField
                                                        .Name
                                                )
                                            ),
                                            Argument(IdentifierName("key")),
                                        ])
                                    )
                                ),
                                IdentifierName(indexer.GetterImplementation.SetupsField.Name)
                                    .Dot(ConcurrentStackSyntaxHelper.Push)
                                    .Call(
                                        Argument(
                                            IdentifierName(
                                                indexer.GetterImplementation.SetupVariableName
                                            )
                                        )
                                    )
                                    .ToStatementSyntax(),
                                ReturnStatement(
                                    IdentifierName(indexer.GetterImplementation.SetupVariableName)
                                )
                            )
                        )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationHistoryIdentifier = IdentifierName(
            indexer.GetterImplementation.InvocationHistoryField.Name
        );

        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            "invocationCount",
            invocationHistoryIdentifier
                .Dot(IdentifierName("Count"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                IdentifierName(indexer.GetterImplementation.CriteriaParameterName)
                                    .Dot(IdentifierName("Matches"))
                            )
                        )
                    )
                )
        );

        var condition = Not(
            IdentifierName(indexer.GetterImplementation.CountParameterName)
                .Dot(IdentifierName("Matches"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(Argument(IdentifierName("invocationCount")))
                    )
                )
        );

        var entryIdentifier = IdentifierName("entry");
        var descriptionExpression = "get "
            .StringLiteral()
            .Add(IdentifierName("_propertyDisplayName"))
            .Add(BuildIndices(indexer, entryIdentifier));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    indexer.GetterImplementation.CriteriaParameterName
                )
            )
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.Imposter.Abstractions.Count,
                    indexer.GetterImplementation.CountParameterName
                )
            )
            .WithBody(
                Block(
                    invocationCountDeclaration,
                    BuildCalledVerificationBlock(
                        condition,
                        invocationHistoryIdentifier,
                        IdentifierName(indexer.GetterImplementation.CountParameterName),
                        descriptionExpression
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildMarkReturnConfiguredMethod(
        in ImposterIndexerMetadata indexer
    ) =>
        BuildMarkConfiguredMethod(
            "MarkReturnConfigured",
            indexer.GetterImplementation.HasConfiguredReturnField.Name
        );

    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod(
        in ImposterIndexerMetadata indexer
    ) =>
        BuildEnsureConfiguredMethod(
            indexer.GetterImplementation.EnsureGetterConfiguredMethodName,
            indexer.GetterImplementation.InvocationBehaviorField.Name,
            indexer.GetterImplementation.HasConfiguredReturnField.Name,
            indexer.GetterImplementation.PropertyDisplayNameField.Name,
            indexer.GetterImplementation.GetterSuffix
        );
}
