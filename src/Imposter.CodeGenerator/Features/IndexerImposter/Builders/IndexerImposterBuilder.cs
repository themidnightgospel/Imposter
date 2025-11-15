using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using GetterReturnsMetadata = Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface.ReturnsMethodMetadata;
using GetterThrowsMetadata = Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface.ThrowsMethodMetadata;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerImposterBuilder
{
    private const string DefaultBehaviourParameterName = "defaultBehaviour";
    private const string InvocationBehaviorParameterName = "invocationBehavior";
    private const string PropertyDisplayNameParameterName = "propertyDisplayName";
    private const string BaseImplementationParameterName = "baseImplementation";

    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var classBuilder = new ClassDeclarationBuilder(indexer.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.Builder.DefaultBehaviourField,
                    indexer.DefaultIndexerBehaviour.TypeSyntax.New()
                )
            )
            .AddMember(
                indexer.Core.HasGetter
                    ? SinglePrivateReadonlyVariableField(indexer.Builder.GetterImposterField)
                    : null
            )
            .AddMember(
                indexer.Core.HasSetter
                    ? SinglePrivateReadonlyVariableField(indexer.Builder.SetterImposterField)
                    : null
            )
            .AddMember(DefaultIndexerBehaviourBuilder.Build(indexer))
            .AddMember(BuildConstructor(indexer))
            .AddMember(indexer.Core.HasGetter ? BuildCreateGetterMethod(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildCreateSetterMethod(indexer) : null)
            .AddMember(BuildInvocationBuilder(indexer))
            .AddMember(indexer.Core.HasGetter ? BuildGetForwarder(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildSetForwarder(indexer) : null)
            .AddMember(indexer.Core.HasGetter ? BuildGetterImposter(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildSetterImposter(indexer) : null);

        return classBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterIndexerMetadata indexer)
    {
        var invocationBehaviorParameter = Parameter(Identifier("invocationBehavior"))
            .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode);
        var propertyDisplayNameParameter = Parameter(Identifier("propertyDisplayName"))
            .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)));

        var getterInitialization = indexer.Core.HasGetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.GetterImposterField.Name))
                .Assign(
                    IdentifierName("GetterImposter")
                        .New(
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(indexer.Builder.DefaultBehaviourField.Name)
                                ),
                                Argument(
                                    IdentifierName(invocationBehaviorParameter.Identifier.Text)
                                ),
                                Argument(
                                    IdentifierName(propertyDisplayNameParameter.Identifier.Text)
                                ),
                            ])
                        )
                )
                .ToStatementSyntax()
            : null;

        var setterInitialization = indexer.Core.HasSetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.SetterImposterField.Name))
                .Assign(
                    IdentifierName("SetterImposter")
                        .New(
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(indexer.Builder.DefaultBehaviourField.Name)
                                ),
                                Argument(
                                    IdentifierName(invocationBehaviorParameter.Identifier.Text)
                                ),
                                Argument(
                                    IdentifierName(propertyDisplayNameParameter.Identifier.Text)
                                ),
                            ])
                        )
                )
                .ToStatementSyntax()
            : null;

        var bodyBuilder = new BlockBuilder()
            .AddStatement(getterInitialization)
            .AddStatement(setterInitialization);

        return new ConstructorBuilder(indexer.Builder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(invocationBehaviorParameter)
            .AddParameter(propertyDisplayNameParameter)
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateGetterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var body = Block(
            ReturnStatement(
                QualifiedName(IdentifierName("GetterImposter"), IdentifierName("Builder"))
                    .New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(indexer.Builder.GetterImposterField.Name)),
                            Argument(IdentifierName("criteria")),
                        ])
                    )
            )
        );

        return new MethodDeclarationBuilder(
            indexer.GetterBuilderInterface.TypeSyntax,
            "CreateGetter"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateSetterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var bodyBuilder = new BlockBuilder();

        if (indexer.Core.HasSetter)
        {
            bodyBuilder.AddStatement(
                IdentifierName(indexer.Builder.SetterImposterField.Name)
                    .Dot(IdentifierName("MarkConfigured"))
                    .Call()
                    .ToStatementSyntax()
            );
        }

        bodyBuilder.AddStatement(
            ReturnStatement(
                QualifiedName(IdentifierName("SetterImposter"), IdentifierName("Builder"))
                    .New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(indexer.Builder.SetterImposterField.Name)),
                            Argument(IdentifierName("criteria")),
                        ])
                    )
            )
        );

        return new MethodDeclarationBuilder(
            indexer.SetterBuilderInterface.TypeSyntax,
            "CreateSetter"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static ClassDeclarationSyntax BuildInvocationBuilder(in ImposterIndexerMetadata indexer)
    {
        var invocationBuilder = new ClassDeclarationBuilder("InvocationBuilder")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.BuilderInterface.TypeSyntax))
            .AddMember(SinglePrivateReadonlyVariableField(indexer.Builder.TypeSyntax, "_builder"))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    "_criteria"
                )
            )
            .AddMember(
                new ConstructorBuilder("InvocationBuilder")
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .AddParameter(
                        Parameter(Identifier("builder")).WithType(indexer.Builder.TypeSyntax)
                    )
                    .AddParameter(
                        Parameter(Identifier("criteria"))
                            .WithType(indexer.ArgumentsCriteria.TypeSyntax)
                    )
                    .WithBody(
                        new BlockBuilder()
                            .AddStatement(
                                ThisExpression()
                                    .Dot(IdentifierName("_builder"))
                                    .Assign(IdentifierName("builder"))
                                    .ToStatementSyntax()
                            )
                            .AddStatement(
                                ThisExpression()
                                    .Dot(IdentifierName("_criteria"))
                                    .Assign(IdentifierName("criteria"))
                                    .ToStatementSyntax()
                            )
                            .Build()
                    )
                    .Build()
            );

        if (indexer.Core.HasGetter)
        {
            invocationBuilder = invocationBuilder.AddMember(
                new MethodDeclarationBuilder(
                    indexer.GetterBuilderInterface.TypeSyntax,
                    indexer.BuilderInterface.GetterMethod.Name
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                IdentifierName("_builder")
                                    .Dot(IdentifierName("CreateGetter"))
                                    .Call(Argument(IdentifierName("_criteria")))
                            )
                        )
                    )
                    .Build()
            );
        }

        if (indexer.Core.HasSetter)
        {
            invocationBuilder = invocationBuilder.AddMember(
                new MethodDeclarationBuilder(
                    indexer.SetterBuilderInterface.TypeSyntax,
                    indexer.BuilderInterface.SetterMethod.Name
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                IdentifierName("_builder")
                                    .Dot(IdentifierName("CreateSetter"))
                                    .Call(Argument(IdentifierName("_criteria")))
                            )
                        )
                    )
                    .Build()
            );
        }

        return invocationBuilder.Build();
    }

    private static MethodDeclarationSyntax BuildGetForwarder(in ImposterIndexerMetadata indexer)
    {
        var parameters = indexer
            .Core.Parameters.Select(parameter => parameter.ParameterSyntax)
            .ToList();

        ParameterSyntax? getterBaseImplementationParameter = null;
        if (indexer.Core.GetterSupportsBaseImplementation)
        {
            getterBaseImplementationParameter = Parameter(
                    Identifier(BaseImplementationParameterName)
                )
                .WithType(indexer.Core.AsSystemFuncType.ToNullableType())
                .WithDefault(EqualsValueClause(Null));

            parameters.Add(getterBaseImplementationParameter);
        }

        var invocationArguments = indexer
            .Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))
            .ToList();

        if (
            indexer.Core.GetterSupportsBaseImplementation
            && getterBaseImplementationParameter is not null
        )
        {
            invocationArguments.Add(
                Argument(IdentifierName(getterBaseImplementationParameter.Identifier))
            );
        }

        return new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Get")
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

    private static MethodDeclarationSyntax BuildSetForwarder(in ImposterIndexerMetadata indexer)
    {
        var parameters = indexer
            .Core.Parameters.Select(parameter => parameter.ParameterSyntax)
            .ToList();

        var setterValueParameterName = indexer.SetterImplementation.ValueParameterName;
        parameters.Add(
            Parameter(Identifier(setterValueParameterName)).WithType(indexer.Core.TypeSyntax)
        );

        ParameterSyntax? setterBaseImplementationParameter = null;
        if (indexer.Core.SetterSupportsBaseImplementation)
        {
            setterBaseImplementationParameter = Parameter(
                    Identifier(indexer.SetterImplementation.BaseImplementationParameterName)
                )
                .WithType(indexer.Core.AsSystemActionType.ToNullableType())
                .WithDefault(EqualsValueClause(Null));

            parameters.Add(setterBaseImplementationParameter);
        }

        var invocationArguments = indexer
            .Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))
            .ToList();

        invocationArguments.Add(Argument(IdentifierName(setterValueParameterName)));
        invocationArguments.Add(
            indexer.Core.SetterSupportsBaseImplementation
            && setterBaseImplementationParameter is not null
                ? Argument(IdentifierName(setterBaseImplementationParameter.Identifier))
                : Argument(Null)
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters.ToArray())
            .WithBody(
                Block(
                    IdentifierName(indexer.Builder.SetterImposterField.Name)
                        .Dot(IdentifierName("Set"))
                        .Call(ArgumentListSyntax(invocationArguments))
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static ClassDeclarationSyntax BuildGetterImposter(in ImposterIndexerMetadata indexer)
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
                    WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(
                        indexer.Arguments.TypeSyntax
                    ),
                    indexer.GetterImplementation.InvocationHistoryField.Name,
                    WellKnownTypes
                        .System.Collections.Concurrent.ConcurrentBag(indexer.Arguments.TypeSyntax)
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
                    PredefinedType(Token(SyntaxKind.StringKeyword)),
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
                new PropertyDeclarationBuilder(
                    PredefinedType(Token(SyntaxKind.StringKeyword)),
                    "PropertyDisplayName"
                )
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
                        Parameter(Identifier(builderMetadata.ImposterFieldName))
                            .WithType(indexer.GetterImplementation.TypeSyntax)
                    )
                    .AddParameter(
                        Parameter(Identifier(builderMetadata.CriteriaFieldName))
                            .WithType(indexer.ArgumentsCriteria.TypeSyntax)
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
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(returnsMetadata.InterfaceSyntax)
            )
            .AddParameter(parameter)
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)
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
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(returnsMetadata.InterfaceSyntax)
            )
            .AddParameter(parameter)
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)
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
                        indexer,
                        fromArguments: true
                    )
                )
        );

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(returnsMetadata.InterfaceSyntax)
            )
            .AddParameter(parameter)
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)
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
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(throwsMetadata.InterfaceSyntax)
            )
            .AddParameter(parameter)
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsGenericExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata
    ) =>
        new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(throwsMetadata.InterfaceSyntax)
            )
            .WithTypeParameters(
                TypeParameterList(
                    SingletonSeparatedList(TypeParameter(throwsMetadata.GenericTypeParameterName))
                )
            )
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddReturnValue(
                        indexer.GetterImplementation.Builder,
                        SimpleLambdaExpression(
                            Parameter(
                                Identifier(indexer.GetterImplementation.ArgumentsVariableName)
                            ),
                            ThrowExpression(
                                ObjectCreationExpression(
                                        IdentifierName(throwsMetadata.GenericTypeParameterName)
                                    )
                                    .WithArgumentList(EmptyArgumentListSyntax)
                            )
                        )
                    )
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
                            indexer,
                            fromArguments: true
                        )
                    )
            )
        );

        return new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(throwsMetadata.InterfaceSyntax)
            )
            .AddParameter(parameter)
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)
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
                ExplicitInterfaceSpecifier(
                    indexer.GetterBuilderInterface.CallbackMethod.InterfaceSyntax
                )
            )
            .AddParameter(parameter)
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterAddCallback(
                        indexer.GetterImplementation.Builder,
                        IdentifierName(parameter.Identifier)
                    )
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
                ExplicitInterfaceSpecifier(
                    indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax
                )
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
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(thenMetadata.InterfaceSyntax)
            )
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var metadata = indexer.GetterBuilderInterface.UseBaseImplementationMethod!.Value;
        return new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(metadata.InterfaceSyntax))
            .WithBody(
                BuildFluentBodyReturningThis(
                    InvocationImposterUseBaseImplementation(indexer.GetterImplementation.Builder)
                )
            )
            .Build();
    }

    private static BlockSyntax BuildFluentBodyReturningThis(params StatementSyntax[] statements)
    {
        var fluentStatements = statements.Concat([ReturnStatement(ThisExpression())]).ToArray();
        return Block(fluentStatements);
    }

    private static ExpressionStatementSyntax InvocationImposterAddReturnValue(
        IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata,
        ExpressionSyntax lambda
    ) =>
        IdentifierName(builderMetadata.InvocationImposterPropertyName)
            .Dot(IdentifierName("AddReturnValue"))
            .Call(ArgumentList(SingletonSeparatedList(Argument(lambda))))
            .ToStatementSyntax();

    private static ExpressionStatementSyntax InvocationImposterAddCallback(
        IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata,
        ExpressionSyntax callback
    ) =>
        IdentifierName(builderMetadata.InvocationImposterPropertyName)
            .Dot(IdentifierName("AddCallback"))
            .Call(ArgumentList(SingletonSeparatedList(Argument(callback))))
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
                    invocationMetadata.InvocationCountField.Type,
                    invocationMetadata.InvocationCountField.Name,
                    SyntaxKind.PrivateKeyword
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
            .AddMember(
                new PropertyDeclarationBuilder(WellKnownTypes.Int, "InvocationCount")
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .Build()
                    .WithAccessorList(null)
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            WellKnownTypes
                                .System.Threading.Volatile.Dot(IdentifierName("Read"))
                                .Call(
                                    Argument(
                                        null,
                                        Token(SyntaxKind.RefKeyword),
                                        IdentifierName(invocationMetadata.InvocationCountField.Name)
                                    )
                                )
                        )
                    )
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            )
            .AddMember(BuildGetterInvocationConstructor(indexer))
            .AddMember(BuildGetterInvocationAddReturnValueMethod(indexer))
            .AddMember(BuildGetterInvocationAddCallbackMethod(indexer))
            .AddMember(BuildGetterInvocationInvokeMethod(indexer))
            .AddMember(BuildGetterInvocationResolveNextGeneratorMethod(indexer))
            .AddMember(BuildGetterInvocationUseBaseImplementationMethod(indexer))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildGetterInvocationConstructor(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;

        return new ConstructorBuilder(invocationMetadata.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(
                Parameter(Identifier("parent")).WithType(indexer.GetterImplementation.TypeSyntax)
            )
            .AddParameter(
                Parameter(Identifier(DefaultBehaviourParameterName))
                    .WithType(indexer.DefaultIndexerBehaviour.TypeSyntax)
            )
            .AddParameter(
                Parameter(Identifier(indexer.GetterImplementation.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
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
        var parameter = Parameter(Identifier("generator"))
            .WithType(builderMetadata.ReturnGeneratorType);
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
                        .Dot(IdentifierName("Enqueue"))
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
                        .Dot(IdentifierName("Enqueue"))
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
        var argumentsParameter = Parameter(Identifier(argumentsParameterName))
            .WithType(indexer.Arguments.TypeSyntax);
        var baseImplementationParameter = Parameter(
                Identifier(indexer.GetterImplementation.BaseImplementationParameterName)
            )
            .WithType(indexer.Core.AsSystemFuncType.ToNullableType())
            .WithDefault(EqualsValueClause(Null));

        var incrementInvocation = WellKnownTypes
            .System.Threading.Interlocked.Dot(IdentifierName("Increment"))
            .Call(
                Argument(
                    null,
                    Token(SyntaxKind.RefKeyword),
                    IdentifierName(invocationMetadata.InvocationCountField.Name)
                )
            )
            .ToStatementSyntax();

        var foreachCallbacks = ForEachStatement(
            IdentifierName("var"),
            Identifier("callback"),
            IdentifierName(invocationMetadata.CallbacksField.Name),
            Block(
                IdentifierName("callback")
                    .Call(
                        BuildDelegateInvocationArguments(
                            IdentifierName(argumentsParameterName),
                            indexer,
                            fromArguments: true
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

        return new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParameter)
            .AddParameter(baseImplementationParameter)
            .WithBody(
                Block(incrementInvocation, foreachCallbacks, generatorDeclaration, returnStatement)
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationResolveNextGeneratorMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var argumentsParameterName = indexer.GetterImplementation.ArgumentsVariableName;
        var argumentsParameter = Parameter(Identifier(argumentsParameterName))
            .WithType(indexer.Arguments.TypeSyntax);
        const string ReturnValueVariableName = "returnValue";
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

        var dequeueNextReturnValue = IdentifierName(invocationMetadata.ReturnValuesField.Name)
            .Dot(IdentifierName("TryDequeue"))
            .Call(
                Argument(
                    null,
                    Token(SyntaxKind.OutKeyword),
                    DeclarationExpression(
                        Var,
                        SingleVariableDesignation(Identifier(ReturnValueVariableName))
                    )
                )
            )
            .ToStatementSyntax();

        var declareNextReturnValue = LocalVariableDeclarationSyntax(
            Var,
            NextReturnValueVariableName,
            IdentifierName(ReturnValueVariableName)
                .Coalesce(IdentifierName(invocationMetadata.LastReturnValueField.Name))
        );

        var throwIfMissing = IfStatement(
            IdentifierName(NextReturnValueVariableName).IsNull(),
            Block(BuildMissingImposterThrow(indexer, indexer.GetterImplementation.GetterSuffix))
        );

        var updateLastReturnValue = IdentifierName(invocationMetadata.LastReturnValueField.Name)
            .Assign(IdentifierName(NextReturnValueVariableName))
            .ToStatementSyntax();

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
                Block(
                    defaultBehaviourCheck,
                    dequeueNextReturnValue,
                    declareNextReturnValue,
                    throwIfMissing,
                    updateLastReturnValue,
                    returnNext
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var messageExpression = BinaryExpression(
            SyntaxKind.AddExpression,
            IdentifierName(invocationMetadata.PropertyDisplayNameField.Name),
            LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                Literal(indexer.GetterImplementation.GetterSuffix)
            )
        );

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
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName(
                                indexer.GetterImplementation.BaseImplementationParameterName
                            ),
                            Null
                        ),
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
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(handlerLambda))
                        .ToStatementSyntax(),
                    IdentifierName(invocationMetadata.LastReturnValueField.Name)
                        .Assign(Null)
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static ClassDeclarationSyntax BuildSetterImposter(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;

        return new ClassDeclarationBuilder(setter.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    setter.CallbacksField,
                    setter.CallbacksField.Type.New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    setter.InvocationHistoryField,
                    setter.InvocationHistoryField.Type.New()
                )
            )
            .AddMember(
                indexer.Core.SetterSupportsBaseImplementation
                && setter.BaseImplementationCriteriaField.HasValue
                    ? SinglePrivateReadonlyVariableField(
                        setter.BaseImplementationCriteriaField.Value,
                        setter.BaseImplementationCriteriaField.Value.Type.New()
                    )
                    : null
            )
            .AddMember(SinglePrivateReadonlyVariableField(setter.DefaultBehaviourField))
            .AddMember(SinglePrivateReadonlyVariableField(setter.InvocationBehaviorField))
            .AddMember(SinglePrivateReadonlyVariableField(setter.PropertyDisplayNameField))
            .AddMember(
                SingleVariableField(
                    setter.HasConfiguredSetterField.Type,
                    setter.HasConfiguredSetterField.Name,
                    SyntaxKind.PrivateKeyword
                )
            )
            .AddMember(BuildSetterConstructor(indexer))
            .AddMember(BuildSetterCallbackMethod(indexer))
            .AddMember(BuildSetterCalledMethod(indexer))
            .AddMember(BuildSetterSetMethod(indexer))
            .AddMember(BuildEnsureSetterConfiguredMethod(indexer))
            .AddMember(BuildMarkSetterConfiguredMethod(indexer))
            .AddMember(
                indexer.Core.SetterSupportsBaseImplementation
                    ? BuildSetterUseBaseImplementationMethod(indexer)
                    : null
            )
            .AddMember(BuildSetterBuilder(indexer))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildSetterConstructor(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;

        return new ConstructorBuilder(setter.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(
                Parameter(Identifier(DefaultBehaviourParameterName))
                    .WithType(indexer.DefaultIndexerBehaviour.TypeSyntax)
            )
            .AddParameter(
                Parameter(Identifier(InvocationBehaviorParameterName))
                    .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode)
            )
            .AddParameter(
                Parameter(Identifier(PropertyDisplayNameParameterName))
                    .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
            )
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(setter.DefaultBehaviourField.Name))
                            .Assign(IdentifierName(DefaultBehaviourParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(setter.InvocationBehaviorField.Name))
                            .Assign(IdentifierName(InvocationBehaviorParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(setter.PropertyDisplayNameField.Name))
                            .Assign(IdentifierName(PropertyDisplayNameParameterName))
                            .ToStatementSyntax()
                    )
                    .Build()
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;
        var callbackParameter = ParameterSyntax(
            indexer.SetterBuilderInterface.CallbackMethod.CallbackParameter
        );

        var tupleExpression = TupleExpression(
            SeparatedList<ArgumentSyntax>(
                new SyntaxNodeOrToken[]
                {
                    Argument(IdentifierName(setter.CriteriaParameterName)),
                    Token(SyntaxKind.CommaToken),
                    Argument(IdentifierName(callbackParameter.Identifier)),
                }
            )
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Callback")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(
                Parameter(Identifier(setter.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .AddParameter(callbackParameter)
            .WithBody(
                Block(
                    IdentifierName(setter.CallbacksField.Name)
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(tupleExpression))
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;
        var countParameter = ParameterSyntax(
            indexer.SetterBuilderInterface.CalledMethod.CountParameter
        );

        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            "invocationCount",
            IdentifierName(setter.InvocationHistoryField.Name)
                .Dot(IdentifierName("Count"))
                .Call(
                    Argument(
                        IdentifierName(setter.CriteriaParameterName).Dot(IdentifierName("Matches"))
                    )
                )
        );

        var condition = Not(
            IdentifierName(countParameter.Identifier)
                .Dot(IdentifierName("Matches"))
                .Call(Argument(IdentifierName("invocationCount")))
        );

        var throwStatement = ThrowStatement(
            ObjectCreationExpression(
                    WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                )
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                Argument(IdentifierName(countParameter.Identifier)),
                                Token(SyntaxKind.CommaToken),
                                Argument(IdentifierName("invocationCount")),
                            }
                        )
                    )
                )
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(
                Parameter(Identifier(setter.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .AddParameter(countParameter)
            .WithBody(
                Block(invocationCountDeclaration, IfStatement(condition, Block(throwStatement)))
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "UseBaseImplementation")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(
                Parameter(Identifier(setter.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .WithBody(
                Block(
                    IdentifierName(setter.BaseImplementationCriteriaField!.Value.Name)
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(IdentifierName(setter.CriteriaParameterName)))
                        .ToStatementSyntax(),
                    IdentifierName("MarkConfigured").Call().ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterSetMethod(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;
        var argumentsVariable = IdentifierName(indexer.GetterImplementation.ArgumentsVariableName);
        var parameters = indexer
            .Core.Parameters.Select(parameter => parameter.ParameterSyntax)
            .Concat([
                Parameter(Identifier(setter.ValueParameterName)).WithType(indexer.Core.TypeSyntax),
            ])
            .ToList();

        parameters.Add(
            Parameter(Identifier(setter.BaseImplementationParameterName))
                .WithType(indexer.Core.AsSystemActionType.ToNullableType())
                .WithDefault(EqualsValueClause(Null))
        );

        var callbackMatchedIdentifier = IdentifierName("matchedCallback");

        var foreachStatement = ForEachStatement(
            IdentifierName("var"),
            Identifier("registration"),
            IdentifierName(setter.CallbacksField.Name),
            Block(
                IfStatement(
                    IdentifierName("registration")
                        .Dot(IdentifierName("Criteria"))
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(argumentsVariable)),
                    Block(
                        IdentifierName("registration")
                            .Dot(IdentifierName("Callback"))
                            .Call(
                                BuildDelegateInvocationArgumentsWithValue(
                                    argumentsVariable,
                                    indexer,
                                    fromArguments: true,
                                    IdentifierName(setter.ValueParameterName)
                                )
                            )
                            .ToStatementSyntax(),
                        callbackMatchedIdentifier.Assign(True).ToStatementSyntax()
                    )
                )
            )
        );

        ExpressionSyntax defaultBehaviourCondition = IdentifierName(
                setter.DefaultBehaviourField.Name
            )
            .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName));

        defaultBehaviourCondition = Not(callbackMatchedIdentifier).And(defaultBehaviourCondition);

        ExpressionSyntax? invokedBaseIdentifier = null;
        StatementSyntax? baseCriteriaLoop = null;

        if (
            indexer.Core.SetterSupportsBaseImplementation
            && setter.BaseImplementationCriteriaField.HasValue
        )
        {
            invokedBaseIdentifier = IdentifierName("invokedBaseImplementation");

            baseCriteriaLoop = ForEachStatement(
                IdentifierName("var"),
                Identifier("criteria"),
                IdentifierName(setter.BaseImplementationCriteriaField.Value.Name),
                Block(
                    IfStatement(
                        IdentifierName("criteria")
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(argumentsVariable)),
                        Block(
                            IfStatement(
                                BinaryExpression(
                                    SyntaxKind.EqualsExpression,
                                    IdentifierName(setter.BaseImplementationParameterName),
                                    Null
                                ),
                                Block(BuildMissingImposterThrow(indexer, setter.SetterSuffix))
                            ),
                            IdentifierName(setter.BaseImplementationParameterName)
                                .Call(EmptyArgumentListSyntax)
                                .ToStatementSyntax(),
                            invokedBaseIdentifier.Assign(True).ToStatementSyntax(),
                            BreakStatement()
                        )
                    )
                )
            );

            defaultBehaviourCondition = Not(invokedBaseIdentifier).And(defaultBehaviourCondition);
        }

        var defaultBehaviourBlock = IfStatement(
            defaultBehaviourCondition,
            Block(
                IdentifierName(setter.DefaultBehaviourField.Name)
                    .Dot(IdentifierName("Set"))
                    .Call(
                        ArgumentListSyntax([
                            Argument(argumentsVariable),
                            Argument(IdentifierName(setter.ValueParameterName)),
                            Argument(Null),
                        ])
                    )
                    .ToStatementSyntax()
            )
        );

        var bodyBuilder = new BlockBuilder()
            .AddStatement(IdentifierName("EnsureSetterConfigured").Call().ToStatementSyntax())
            .AddStatement(CreateArgumentsDeclaration(indexer))
            .AddStatement(
                IdentifierName(setter.InvocationHistoryField.Name)
                    .Dot(IdentifierName("Add"))
                    .Call(Argument(argumentsVariable))
                    .ToStatementSyntax()
            )
            .AddStatement(
                LocalVariableDeclarationSyntax(
                    WellKnownTypes.Bool,
                    callbackMatchedIdentifier.Identifier.Text,
                    False
                )
            )
            .AddStatement(foreachStatement);

        if (invokedBaseIdentifier is not null && baseCriteriaLoop is not null)
        {
            bodyBuilder.AddStatement(
                LocalVariableDeclarationSyntax(
                    WellKnownTypes.Bool,
                    ((IdentifierNameSyntax)invokedBaseIdentifier).Identifier.Text,
                    False
                )
            );
            bodyBuilder.AddStatement(baseCriteriaLoop);
        }

        bodyBuilder.AddStatement(defaultBehaviourBlock);

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters.ToArray())
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildEnsureSetterConfiguredMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;

        var explicitCheck = BinaryExpression(
            SyntaxKind.EqualsExpression,
            IdentifierName(setter.InvocationBehaviorField.Name),
            WellKnownTypes.Imposter.Abstractions.ImposterMode.Dot(IdentifierName("Explicit"))
        );

        var configuredCheck = WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Read"))
            .Call(
                Argument(
                    null,
                    Token(SyntaxKind.RefKeyword),
                    IdentifierName(setter.HasConfiguredSetterField.Name)
                )
            );

        var condition = explicitCheck.And(Not(configuredCheck));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "EnsureSetterConfigured")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(
                        condition,
                        Block(BuildMissingImposterThrow(indexer, setter.SetterSuffix))
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildMarkSetterConfiguredMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkConfigured")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    WellKnownTypes
                        .System.Threading.Volatile.Dot(IdentifierName("Write"))
                        .Call(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(
                                            null,
                                            Token(SyntaxKind.RefKeyword),
                                            IdentifierName(setter.HasConfiguredSetterField.Name)
                                        ),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(True),
                                    }
                                )
                            )
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static ClassDeclarationSyntax BuildSetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.SetterImplementation.Builder;

        return new ClassDeclarationBuilder(builderMetadata.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.FluentInterfaceTypeSyntax))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.SetterImplementation.TypeSyntax,
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
                        Parameter(Identifier(builderMetadata.ImposterFieldName))
                            .WithType(indexer.SetterImplementation.TypeSyntax)
                    )
                    .AddParameter(
                        Parameter(Identifier(builderMetadata.CriteriaFieldName))
                            .WithType(indexer.ArgumentsCriteria.TypeSyntax)
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
            .AddMember(BuildSetterBuilderCallbackMethod(indexer))
            .AddMember(BuildSetterBuilderCalledMethod(indexer))
            .AddMember(BuildSetterBuilderThenMethod(indexer))
            .AddMember(
                indexer.SetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildSetterBuilderUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var callbackMetadata = indexer.SetterBuilderInterface.CallbackMethod;
        var parameter = ParameterSyntax(callbackMetadata.CallbackParameter);

        return new MethodDeclarationBuilder(callbackMetadata.ReturnType, callbackMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    indexer.SetterBuilderInterface.CallbackMethod.InterfaceSyntax
                )
            )
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("Callback"))
                        .Call([
                            Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                            Argument(IdentifierName(parameter.Identifier)),
                        ])
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var calledMetadata = indexer.SetterBuilderInterface.CalledMethod;
        var parameter = ParameterSyntax(calledMetadata.CountParameter);

        return new MethodDeclarationBuilder(calledMetadata.ReturnType, calledMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    indexer.SetterBuilderInterface.VerificationInterfaceTypeSyntax
                )
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

    private static MethodDeclarationSyntax BuildSetterBuilderThenMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var thenMetadata = indexer.SetterBuilderInterface.ThenMethod;

        return new MethodDeclarationBuilder(thenMetadata.ReturnType, thenMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(thenMetadata.InterfaceSyntax)
            )
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var metadata = indexer.SetterBuilderInterface.UseBaseImplementationMethod!.Value;

        return new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(metadata.InterfaceSyntax))
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("UseBaseImplementation"))
                        .Call(Argument(IdentifierName(builderMetadata.CriteriaFieldName)))
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build();
    }

    private static ArgumentSyntax BuildArgument(
        IParameterSymbol parameter,
        ExpressionSyntax expression
    )
    {
        var modifier = parameter.RefKind switch
        {
            RefKind.Ref => Token(SyntaxKind.RefKeyword),
            RefKind.Out => Token(SyntaxKind.OutKeyword),
            RefKind.In => Token(SyntaxKind.InKeyword),
            _ => default(SyntaxToken),
        };

        return Argument(null, modifier, expression);
    }

    private static ArgumentListSyntax BuildIndexerArgumentsArgumentList(
        in ImposterIndexerMetadata indexer
    ) =>
        ArgumentList(
            SeparatedList(
                indexer.Core.Parameters.Select(parameter => ArgumentSyntax(parameter.Symbol))
            )
        );

    private static LocalDeclarationStatementSyntax CreateArgumentsDeclaration(
        in ImposterIndexerMetadata indexer
    ) =>
        LocalVariableDeclarationSyntax(
            indexer.Arguments.TypeSyntax,
            indexer.GetterImplementation.ArgumentsVariableName,
            indexer.Arguments.TypeSyntax.New(BuildIndexerArgumentsArgumentList(indexer))
        );

    private static ArgumentListSyntax BuildDelegateInvocationArguments(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        bool fromArguments
    )
    {
        var arguments = indexer.Core.Parameters.Select(parameter =>
        {
            ExpressionSyntax argumentExpression = fromArguments
                ? source.Dot(IdentifierName(parameter.Name))
                : IdentifierName(parameter.Name);

            return BuildArgument(parameter.Symbol, argumentExpression);
        });

        return ArgumentList(SeparatedList(arguments));
    }

    private static ArgumentListSyntax BuildDelegateInvocationArgumentsWithValue(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        bool fromArguments,
        ExpressionSyntax valueExpression
    )
    {
        var arguments = BuildDelegateInvocationArguments(source, indexer, fromArguments)
            .Arguments.Add(Argument(valueExpression));

        return ArgumentList(arguments);
    }

    private static BinaryExpressionSyntax BuildMissingImposterMessage(
        in ImposterIndexerMetadata indexer,
        string suffix
    ) =>
        BinaryExpression(
            SyntaxKind.AddExpression,
            IdentifierName(indexer.GetterImplementation.PropertyDisplayNameField.Name),
            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(suffix))
        );

    private static ThrowStatementSyntax BuildMissingImposterThrow(
        in ImposterIndexerMetadata indexer,
        string suffix
    ) =>
        ThrowStatement(
            ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                .WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(BuildMissingImposterMessage(indexer, suffix))
                        )
                    )
                )
        );

    private static ConstructorDeclarationSyntax BuildGetterConstructor(
        in ImposterIndexerMetadata indexer
    ) =>
        new ConstructorBuilder("GetterImposter")
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(
                Parameter(Identifier(DefaultBehaviourParameterName))
                    .WithType(indexer.DefaultIndexerBehaviour.TypeSyntax)
            )
            .AddParameter(
                Parameter(Identifier(InvocationBehaviorParameterName))
                    .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode)
            )
            .AddParameter(
                Parameter(Identifier(PropertyDisplayNameParameterName))
                    .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
            )
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        ThisExpression()
                            .Dot(
                                IdentifierName(
                                    indexer.GetterImplementation.DefaultBehaviourField.Name
                                )
                            )
                            .Assign(IdentifierName(DefaultBehaviourParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(
                                IdentifierName(
                                    indexer.GetterImplementation.InvocationBehaviorField.Name
                                )
                            )
                            .Assign(IdentifierName(InvocationBehaviorParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(
                                IdentifierName(
                                    indexer.GetterImplementation.PropertyDisplayNameField.Name
                                )
                            )
                            .Assign(IdentifierName(PropertyDisplayNameParameterName))
                            .ToStatementSyntax()
                    )
                    .Build()
            )
            .Build();

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
        var parameters = indexer
            .Core.Parameters.Select(parameter => parameter.ParameterSyntax)
            .ToList();

        parameters.Add(
            Parameter(Identifier(indexer.GetterImplementation.BaseImplementationParameterName))
                .WithType(indexer.Core.AsSystemFuncType.ToNullableType())
                .WithDefault(EqualsValueClause(Null))
        );

        var tryStatements = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(
                IdentifierName("var"),
                indexer.GetterImplementation.SetupVariableName,
                IdentifierName("FindGetterInvocationImposter")
                    .Call(ArgumentList(SingletonSeparatedList(Argument(argumentsIdentifier))))
            ),
            IfStatement(
                IsPatternExpression(
                    IdentifierName(indexer.GetterImplementation.SetupVariableName),
                    ConstantPattern(Null)
                ),
                Block(
                    IdentifierName("EnsureGetterConfigured").Call().ToStatementSyntax(),
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
                                indexer,
                                indexer.GetterImplementation.GetterSuffix
                            )
                        ),
                        ElseClause(
                            Block(ReturnStatement(DefaultExpression(indexer.Core.TypeSyntax)))
                        )
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
                    .Dot(IdentifierName("Add"))
                    .Call(ArgumentList(SingletonSeparatedList(Argument(argumentsIdentifier))))
                    .ToStatementSyntax()
            )
        );

        var body = Block(
            CreateArgumentsDeclaration(indexer),
            TryStatement(Block(tryStatements), default, finallyClause)
        );

        return new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Get")
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

        return new MethodDeclarationBuilder(getterInvocationType, "FindGetterInvocationImposter")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName))
                    .WithType(indexer.Arguments.TypeSyntax)
            )
            .WithBody(
                Block(
                    ForEachStatement(
                        IdentifierName("var"),
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
                Parameter(Identifier(indexer.GetterImplementation.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
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
                            Parameter(Identifier("key"))
                                .WithType(indexer.ArgumentsCriteria.TypeSyntax)
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
                                    .Dot(IdentifierName("Push"))
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
        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            "invocationCount",
            IdentifierName(indexer.GetterImplementation.InvocationHistoryField.Name)
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

        var throwStatement = ThrowStatement(
            ObjectCreationExpression(
                    WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                )
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                Argument(
                                    IdentifierName(indexer.GetterImplementation.CountParameterName)
                                ),
                                Token(SyntaxKind.CommaToken),
                                Argument(IdentifierName("invocationCount")),
                            }
                        )
                    )
                )
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                Parameter(Identifier(indexer.GetterImplementation.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .AddParameter(
                Parameter(Identifier(indexer.GetterImplementation.CountParameterName))
                    .WithType(WellKnownTypes.Imposter.Abstractions.Count)
            )
            .WithBody(
                Block(invocationCountDeclaration, IfStatement(condition, Block(throwStatement)))
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildMarkReturnConfiguredMethod(
        in ImposterIndexerMetadata indexer
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkReturnConfigured")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    WellKnownTypes
                        .System.Threading.Volatile.Dot(IdentifierName("Write"))
                        .Call(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(
                                            null,
                                            Token(SyntaxKind.RefKeyword),
                                            IdentifierName(
                                                indexer
                                                    .GetterImplementation
                                                    .HasConfiguredReturnField
                                                    .Name
                                            )
                                        ),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(True),
                                    }
                                )
                            )
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var explicitCheck = BinaryExpression(
            SyntaxKind.EqualsExpression,
            IdentifierName(indexer.GetterImplementation.InvocationBehaviorField.Name),
            WellKnownTypes.Imposter.Abstractions.ImposterMode.Dot(IdentifierName("Explicit"))
        );

        var hasReturnConfigured = IdentifierName(
            indexer.GetterImplementation.HasConfiguredReturnField.Name
        );
        var missingReturnCheck = Not(
            WellKnownTypes
                .System.Threading.Volatile.Dot(IdentifierName("Read"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(null, Token(SyntaxKind.RefKeyword), hasReturnConfigured)
                        )
                    )
                )
        );

        var condition = explicitCheck.And(missingReturnCheck);

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "EnsureGetterConfigured")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(
                        condition,
                        Block(
                            BuildMissingImposterThrow(
                                indexer,
                                indexer.GetterImplementation.GetterSuffix
                            )
                        )
                    )
                )
            )
            .Build();
    }
}
