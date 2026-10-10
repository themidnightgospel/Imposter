using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.VerificationFailedBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.InterlockedSyntaxHelper;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;

internal static class GetterImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterPropertyMetadata property)
    {
        var builder = new ClassDeclarationBuilder(property.GetterImposterBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.TypeSyntax))
            .AddBaseType(
                SimpleBaseType(property.GetterImposterBuilderInterface.FluentInterfaceTypeSyntax)
            );

        if (property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod is not null)
        {
            builder = builder.AddBaseType(
                SimpleBaseType(
                    property
                        .GetterImposterBuilderInterface
                        .UseBaseImplementationEntryMethod
                        .Value
                        .InterfaceSyntax
                )
            );
        }

        builder = builder
            .AddMember(BuildGetterReturnValuesField(property.GetterImposterBuilder))
            .AddMember(BuildGetterCallbacksField(property.GetterImposterBuilder))
            .AddMember(BuildLastGetterReturnValueField(property.GetterImposterBuilder))
            .AddMember(BuildGetterInvocationCountField(property.GetterImposterBuilder))
            .AddMember(
                property.Core.KeepsValue
                    ? SinglePrivateReadonlyVariableField(
                        property.GetterImposterBuilder.DefaultPropertyBehaviourField
                    )
                    : null
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    property.GetterImposterBuilder.InvocationBehaviorField
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    property.GetterImposterBuilder.PropertyDisplayNameField
                )
            )
            .AddMember(
                SingleVariableField(
                    property.GetterImposterBuilder.HasConfiguredReturnField,
                    SyntaxKind.PrivateKeyword
                )
            )
            .AddMember(BuildConstructor(property))
            .AddMember(BuildAddGetterReturnValueMethod(property))
            .AddMembers(BuildReturnsMethods(property))
            .AddMembers(
                BuildThrowsMethod(
                    property.GetterImposterBuilder,
                    property.GetterImposterBuilderInterface
                )
            )
            .AddMember(
                BuildGetterCallbackMethod(
                    property.GetterImposterBuilder,
                    property.GetterImposterBuilderInterface
                )
            )
            .AddMember(
                BuildGetterCalledMethod(
                    property.GetterImposterBuilder,
                    property.GetterImposterBuilderInterface
                )
            )
            .AddMember(BuildThenMethod(property.GetterImposterBuilderInterface))
            .AddMember(
                property.GetterImposterBuilderInterface.InitialThenMethod is not null
                    ? BuildInitialThenMethod(property)
                    : null
            )
            .AddMember(
                property.Core.GetterSupportsBaseImplementation
                    ? BuildEnableBaseImplementationMethod(property)
                    : null
            )
            .AddMember(
                property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod is not null
                    ? BuildUseBaseImplementationInterfaceMethod(
                        property.GetterImposterBuilder,
                        property
                            .GetterImposterBuilderInterface
                            .UseBaseImplementationEntryMethod
                            .Value
                    )
                    : null
            )
            .AddMember(
                property.GetterImposterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildUseBaseImplementationInterfaceMethod(
                        property.GetterImposterBuilder,
                        property.GetterImposterBuilderInterface.UseBaseImplementationMethod.Value
                    )
                    : null
            )
            .AddMember(BuildGetMethod(property))
            .AddMember(BuildNextReturnValueMethod(property.GetterImposterBuilder))
            .AddMember(BuildEnsureGetterConfiguredMethod(property.GetterImposterBuilder));

        return builder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property
    )
    {
        var builder = property.GetterImposterBuilder;
        var constructor = new ConstructorWithFieldInitializationBuilder(builder.Name).WithModifiers(
            Token(SyntaxKind.InternalKeyword)
        );

        if (property.Core.KeepsValue)
        {
            constructor.AddParameter(builder.DefaultPropertyBehaviourField);
        }

        return constructor
            .AddParameter(builder.InvocationBehaviorParameter, builder.InvocationBehaviorField.Name)
            .AddParameter(
                builder.PropertyDisplayNameParameter,
                builder.PropertyDisplayNameField.Name
            )
            .Build();
    }

    private static FieldDeclarationSyntax BuildGetterReturnValuesField(
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder
    ) =>
        SinglePrivateReadonlyVariableField(
            getterImposterBuilder.ReturnValuesField.TypeSyntax,
            getterImposterBuilder.ReturnValuesField.Name,
            getterImposterBuilder.ReturnValuesField.TypeSyntax.New()
        );

    private static FieldDeclarationSyntax BuildGetterCallbacksField(
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder
    ) =>
        SinglePrivateReadonlyVariableField(
            getterImposterBuilder.CallbacksField.TypeSyntax,
            getterImposterBuilder.CallbacksField.Name,
            getterImposterBuilder.CallbacksField.TypeSyntax.New()
        );

    private static FieldDeclarationSyntax BuildLastGetterReturnValueField(
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder
    ) =>
        SingleVariableField(
            getterImposterBuilder.LastReturnValueField.TypeSyntax,
            getterImposterBuilder.LastReturnValueField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.VolatileKeyword)),
            DiscardParameterGoesTo(DefaultNonNullable)
        );

    private static FieldDeclarationSyntax BuildGetterInvocationCountField(
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder
    ) =>
        SingleVariableField(
            getterImposterBuilder.InvocationCountField.TypeSyntax,
            getterImposterBuilder.InvocationCountField.Name,
            SyntaxKind.PrivateKeyword
        );

    private static MethodDeclarationSyntax BuildAddGetterReturnValueMethod(
        in ImposterPropertyMetadata property
    )
    {
        var getterImposterBuilder = property.GetterImposterBuilder;
        var turnDefaultBehaviourOff = property.Core.KeepsValue
            ? IdentifierName(getterImposterBuilder.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(property.DefaultPropertyBehaviour.IsOnField.Name))
                .Assign(False)
            : null;

        return new MethodDeclarationBuilder(
            getterImposterBuilder.AddReturnValueMethod.ReturnType,
            getterImposterBuilder.AddReturnValueMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(getterImposterBuilder.AddReturnValueMethod.ValueGeneratorParameter)
            )
            .WithBody(
                new BlockBuilder()
                    .AddExpression(turnDefaultBehaviourOff)
                    .AddExpression(
                        IdentifierName(getterImposterBuilder.ReturnValuesField.Name)
                            .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                            .Call(
                                Argument(
                                    IdentifierName(
                                        getterImposterBuilder
                                            .AddReturnValueMethod
                                            .ValueGeneratorParameter
                                            .Name
                                    )
                                )
                            )
                    )
                    .AddExpression(
                        IdentifierName(getterImposterBuilder.HasConfiguredReturnField.Name)
                            .Assign(True)
                    )
                    .Build()
            )
            .Build();
    }

    private static MethodDeclarationSyntax[] BuildReturnsMethods(
        in ImposterPropertyMetadata property
    )
    {
        var returns = property.GetterImposterBuilderInterface.ReturnsMethod;
        var returnsGeneratedValue = BuildReturnsMethod(
            property,
            returns.ValueGeneratorParameter,
            IdentifierName(returns.ValueGeneratorParameter.Name).Call()
        );

        return returns.ValueParameter is { } value
            ?
            [
                BuildReturnsMethod(property, value, IdentifierName(value.Name)),
                returnsGeneratedValue,
            ]
            : [returnsGeneratedValue];
    }

    private static MethodDeclarationSyntax BuildReturnsMethod(
        in ImposterPropertyMetadata property,
        in ParameterMetadata parameter,
        ExpressionSyntax returnedValue
    )
    {
        var returns = property.GetterImposterBuilderInterface.ReturnsMethod;

        return new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .AddParameter(ParameterSyntax(parameter))
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(property.GetterImposterBuilder.AddReturnValueMethod.Name)
                        .Call(Argument(IgnoreBaseImplementationLambda(returnedValue)))
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax[] BuildThrowsMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface
    ) =>
        [
            new MethodDeclarationBuilder(
                builderInterface.ThrowsMethod.ReturnType,
                builderInterface.ThrowsMethod.Name
            )
                .AddParameter(ParameterSyntax(builderInterface.ThrowsMethod.ExceptionParameter))
                .WithExplicitInterfaceSpecifier(builderInterface.ThrowsMethod.InterfaceSyntax)
                .WithBody(
                    Block(
                        IdentifierName(builder.AddReturnValueMethod.Name)
                            .Call(
                                Argument(
                                    IgnoreBaseImplementationLambda(
                                        ThrowExpression(
                                            IdentifierName(
                                                builderInterface
                                                    .ThrowsMethod
                                                    .ExceptionParameter
                                                    .Name
                                            )
                                        )
                                    )
                                )
                            )
                            .ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build(),
            new MethodDeclarationBuilder(
                builderInterface.ThrowsMethod.ReturnType,
                builderInterface.ThrowsMethod.Name
            )
                .WithTypeParameters(
                    builderInterface.ThrowsMethod.ExceptionTypeParameter.TypeParameterList
                )
                .WithExplicitInterfaceSpecifier(builderInterface.ThrowsMethod.InterfaceSyntax)
                .WithBody(
                    Block(
                        IdentifierName(builder.AddReturnValueMethod.Name)
                            .Call(
                                Argument(
                                    IgnoreBaseImplementationLambda(
                                        ThrowExpression(
                                            IdentifierName(
                                                    builderInterface
                                                        .ThrowsMethod
                                                        .ExceptionTypeParameter
                                                        .Name
                                                )
                                                .New()
                                        )
                                    )
                                )
                            )
                            .ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build(),
        ];

    internal static MethodDeclarationSyntax BuildGetterCallbackMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface
    ) =>
        new MethodDeclarationBuilder(
            builderInterface.CallbackMethod.ReturnType,
            builderInterface.CallbackMethod.Name
        )
            .WithExplicitInterfaceSpecifier(builderInterface.CallbackMethod.InterfaceSyntax)
            .AddParameter(ParameterSyntax(builderInterface.CallbackMethod.CallbackParameter))
            .WithBody(
                Block(
                    IdentifierName(builder.CallbacksField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(
                            Argument(
                                IdentifierName(
                                    builderInterface.CallbackMethod.CallbackParameter.Name
                                )
                            )
                        )
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();

    internal static MethodDeclarationSyntax BuildGetterCalledMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface
    )
    {
        var count = IdentifierName(builderInterface.CalledMethod.CountParameter.Name);
        var invocationCount = IdentifierName(builder.InvocationCountField.Name);

        return new MethodDeclarationBuilder(
            builderInterface.CalledMethod.ReturnType,
            builderInterface.CalledMethod.Name
        )
            .WithExplicitInterfaceSpecifier(builderInterface.VerificationInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(builderInterface.CalledMethod.CountParameter))
            .WithBody(
                Block(
                    IfStatement(
                        CountDoesNotMatch(count, invocationCount),
                        ThrowVerificationFailed(count, invocationCount)
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildThenMethod(
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface
    ) =>
        new MethodDeclarationBuilder(
            builderInterface.ThenMethod.ReturnType,
            builderInterface.ThenMethod.Name
        )
            .WithExplicitInterfaceSpecifier(builderInterface.ThenMethod.InterfaceSyntax)
            .WithBody(Block(ReturnThis))
            .Build();

    private static MethodDeclarationSyntax BuildInitialThenMethod(
        in ImposterPropertyMetadata property
    )
    {
        var method = property.GetterImposterBuilderInterface.InitialThenMethod!.Value;

        return new MethodDeclarationBuilder(method.ReturnType, method.Name)
            .WithExplicitInterfaceSpecifier(property.GetterImposterBuilderInterface.TypeSyntax)
            .WithBody(Block(ReturnThis))
            .Build();
    }

    private static MethodDeclarationSyntax BuildUseBaseImplementationInterfaceMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        UseBaseImplementationMethodMetadata methodMetadata
    ) =>
        new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithExplicitInterfaceSpecifier(methodMetadata.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(builder.EnableBaseImplementationMethod.Name)
                        .Call()
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildEnableBaseImplementationMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.GetterImposterBuilder.EnableBaseImplementationMethod.ReturnType,
            property.GetterImposterBuilder.EnableBaseImplementationMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    IdentifierName(property.GetterImposterBuilder.AddReturnValueMethod.Name)
                        .Call(Argument(BuildBaseImplementationReturnHandler(property)))
                        .ToStatementSyntax()
                )
            )
            .Build();

    private static ParenthesizedLambdaExpressionSyntax BuildBaseImplementationReturnHandler(
        in ImposterPropertyMetadata property
    )
    {
        var baseImplementation = property
            .GetterImposterBuilder
            .GetMethod
            .BaseImplementationParameter;
        var baseImplementationParameter = ParameterSyntax(
            baseImplementation.Type,
            baseImplementation.Name
        );
        var baseImplementationIdentifier = IdentifierName(baseImplementation.Name);

        return ParenthesizedLambdaExpression()
            .WithParameterList(ParameterList(SingletonSeparatedList(baseImplementationParameter)))
            .WithBlock(
                Block(
                    IfStatement(
                        baseImplementationIdentifier.IsNotNull(),
                        ReturnStatement(baseImplementationIdentifier.Call()),
                        ElseClause(
                            ThrowMissingImposter(
                                property.GetterImposterBuilder.PropertyDisplayNameField.Name,
                                " (getter)"
                            )
                        )
                    )
                )
            );
    }

    private static ParenthesizedLambdaExpressionSyntax IgnoreBaseImplementationLambda(
        ExpressionSyntax body
    ) =>
        ParenthesizedLambdaExpression(body)
            .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("_")))));

    internal static MethodDeclarationSyntax BuildGetMethod(in ImposterPropertyMetadata property)
    {
        var builder = property.GetterImposterBuilder;
        var baseImplementationIdentifier = IdentifierName(
            builder.GetMethod.BaseImplementationParameter.Name
        );
        const string NextReturnValueVariableName = "nextReturnValue";

        return new MethodDeclarationBuilder(builder.GetMethod.ReturnType, builder.GetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(builder.GetMethod.BaseImplementationParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(IdentifierName(builder.EnsureConfiguredMethod.Name).Call())
                    .AddStatement(TrackGetterInvocation(builder))
                    .AddStatement(InvokeGetterCallbacks(builder))
                    .AddStatement(
                        property.Core.KeepsValue
                            ? IfAutoPropertyBehaviourReturnBackingField(
                                builder,
                                property.DefaultPropertyBehaviour,
                                baseImplementationIdentifier
                            )
                            : null
                    )
                    .AddStatement(
                        DeclareNextGetterReturnValue(builder, NextReturnValueVariableName)
                    )
                    .AddStatement(
                        ReturnNextGetterReturnValue(
                            NextReturnValueVariableName,
                            baseImplementationIdentifier
                        )
                    )
                    .Build()
            )
            .Build();

        static LocalDeclarationStatementSyntax DeclareNextGetterReturnValue(
            in PropertyGetterImposterBuilderMetadata builder,
            string nextVariableName
        ) =>
            LocalVariableDeclarationSyntax(
                Var,
                nextVariableName,
                IdentifierName(builder.NextReturnValueMethod.Name).Call()
            );

        static StatementSyntax ReturnNextGetterReturnValue(
            string nextVariableName,
            ExpressionSyntax baseImplementationIdentifier
        ) =>
            ReturnStatement(
                IdentifierName(nextVariableName).Call(Argument(baseImplementationIdentifier))
            );

        static StatementSyntax IfAutoPropertyBehaviourReturnBackingField(
            in PropertyGetterImposterBuilderMetadata builder,
            in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour,
            ExpressionSyntax baseImplementationIdentifier
        )
        {
            var defaultBehaviourCheck = IdentifierName(builder.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(defaultPropertyBehaviour.IsOnField.Name));
            var baseImplementationIsNotNull = baseImplementationIdentifier.IsNotNull();
            var returnBackingField = ReturnStatement(
                IdentifierName(builder.DefaultPropertyBehaviourField.Name)
                    .Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name))
            );

            // If default behaviour is on, optionally seed the backing field from base on first read
            var hasValueSetCheck = IdentifierName(builder.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(defaultPropertyBehaviour.HasValueSetField.Name));
            var seedFromBaseCondition = BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    IdentifierName(builder.InvocationCountField.Name),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))
                )
                .And(baseImplementationIsNotNull)
                .And(Not(hasValueSetCheck));

            var seedFromBase = IdentifierName(builder.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name))
                .Assign(baseImplementationIdentifier.Call())
                .ToStatementSyntax();

            return IfStatement(
                defaultBehaviourCheck,
                Block(IfStatement(seedFromBaseCondition, Block(seedFromBase)), returnBackingField)
            );
        }

        static StatementSyntax InvokeGetterCallbacks(
            in PropertyGetterImposterBuilderMetadata builder
        ) =>
            ForEachStatement(
                type: Var,
                identifier: Identifier("getterCallback"),
                expression: IdentifierName(builder.CallbacksField.Name),
                statement: Block(IdentifierName("getterCallback").Call().ToStatementSyntax())
            );

        static StatementSyntax TrackGetterInvocation(
            in PropertyGetterImposterBuilderMetadata builder
        ) => InterlockedIncrement(builder.InvocationCountField.Name).ToStatementSyntax();
    }

    private static MethodDeclarationSyntax BuildNextReturnValueMethod(
        in PropertyGetterImposterBuilderMetadata builder
    ) =>
        new MethodDeclarationBuilder(
            builder.NextReturnValueMethod.ReturnType,
            builder.NextReturnValueMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                NextOutcomeSyntaxHelper.TakeNextOutcomeBody(
                    IdentifierName(builder.ReturnValuesField.Name),
                    IdentifierName(builder.LastReturnValueField.Name),
                    IdentifierName("returnValue")
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod(
        in PropertyGetterImposterBuilderMetadata builder
    ) =>
        EnsureConfiguredMethod(
            builder.EnsureConfiguredMethod.Name,
            builder.InvocationBehaviorField.Name,
            IdentifierName(builder.HasConfiguredReturnField.Name),
            ThrowMissingImposter(builder.PropertyDisplayNameField.Name, " (getter)")
        );
}
