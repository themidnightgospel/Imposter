using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerSetterBuilder
{
    internal static MethodDeclarationSyntax BuildSetForwarder(in ImposterIndexerMetadata indexer)
    {
        var parameters = new List<ParameterSyntax>(indexer.Core.ParameterSyntaxes);

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

        var invocationArguments = new List<ArgumentSyntax>(indexer.Core.ParameterArguments);

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

    internal static ClassDeclarationSyntax BuildSetterImposter(in ImposterIndexerMetadata indexer)
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

        return BuildImposterConstructor(
            setter.Name,
            indexer.DefaultIndexerBehaviour.TypeSyntax,
            setter.DefaultBehaviourField.Name,
            setter.InvocationBehaviorField.Name,
            setter.PropertyDisplayNameField.Name
        );
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
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
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
        var invocationHistoryIdentifier = IdentifierName(setter.InvocationHistoryField.Name);

        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            "invocationCount",
            invocationHistoryIdentifier
                .Dot(IdentifierName("Count"))
                .Call(
                    Argument(
                        SimpleLambdaExpression(
                            Parameter(Identifier("entry")),
                            IdentifierName(setter.CriteriaParameterName)
                                .Dot(IdentifierName("Matches"))
                                .Call(
                                    Argument(
                                        IdentifierName("entry").Dot(IdentifierName("Arguments"))
                                    )
                                )
                        )
                    )
                )
        );

        var condition = Not(
            IdentifierName(countParameter.Identifier)
                .Dot(IdentifierName("Matches"))
                .Call(Argument(IdentifierName("invocationCount")))
        );

        var entryIdentifier = IdentifierName("entry");
        var argumentsIdentifier = entryIdentifier.Dot(IdentifierName("Arguments"));
        var prefix = AddStrings("set ".StringLiteral(), IdentifierName("_propertyDisplayName"));
        var indices = BuildIndices(indexer, argumentsIdentifier);
        var withIndices = AddStrings(prefix, indices);
        var assignment = AddStrings(withIndices, " = ".StringLiteral());
        var descriptionExpression = AddStrings(
            assignment,
            IdentifierName("FormatValue")
                .Call(Argument(entryIdentifier.Dot(IdentifierName("Value"))))
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(
                Parameter(Identifier(setter.CriteriaParameterName))
                    .WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .AddParameter(countParameter)
            .WithBody(
                Block(
                    invocationCountDeclaration,
                    BuildCalledVerificationBlock(
                        condition,
                        invocationHistoryIdentifier,
                        IdentifierName(countParameter.Identifier),
                        descriptionExpression
                    )
                )
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
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
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
        var parameters = new List<ParameterSyntax>(indexer.Core.ParameterSyntaxes)
        {
            Parameter(Identifier(setter.ValueParameterName)).WithType(indexer.Core.TypeSyntax),
            Parameter(Identifier(setter.BaseImplementationParameterName))
                .WithType(indexer.Core.AsSystemActionType.ToNullableType())
                .WithDefault(EqualsValueClause(Null)),
        };

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
                                Block(
                                    BuildMissingImposterThrow(
                                        setter.PropertyDisplayNameField.Name,
                                        setter.SetterSuffix
                                    )
                                )
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
                    .Dot(IdentifierName("Push"))
                    .Call(
                        Argument(
                            TupleExpression(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(argumentsVariable),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(IdentifierName(setter.ValueParameterName)),
                                    }
                                )
                            )
                        )
                    )
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

        return BuildEnsureConfiguredMethod(
            "EnsureSetterConfigured",
            setter.InvocationBehaviorField.Name,
            setter.HasConfiguredSetterField.Name,
            setter.PropertyDisplayNameField.Name,
            setter.SetterSuffix
        );
    }

    private static MethodDeclarationSyntax BuildMarkSetterConfiguredMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var setter = indexer.SetterImplementation;

        return BuildMarkConfiguredMethod(
            "MarkConfigured",
            SyntaxKind.InternalKeyword,
            setter.HasConfiguredSetterField.Name
        );
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
}
