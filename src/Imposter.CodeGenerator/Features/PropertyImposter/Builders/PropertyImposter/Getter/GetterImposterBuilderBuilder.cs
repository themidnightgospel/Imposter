using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Getter;

internal static class GetterImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterPropertyMetadata property)
    {
        var builder = new ClassDeclarationBuilder(property.GetterImposterBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.FluentInterfaceTypeSyntax));

        if (property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod is not null)
        {
            builder = builder.AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod.Value.InterfaceSyntax));
        }

        builder = builder
            .AddMember(BuildGetterReturnValuesField(property.GetterImposterBuilder))
            .AddMember(BuildGetterCallbacksField(property.GetterImposterBuilder))
            .AddMember(BuildLastGetterReturnValueField(property.GetterImposterBuilder))
            .AddMember(BuildGetterInvocationCountField(property.GetterImposterBuilder))
            .AddMember(SinglePrivateReadonlyVariableField(property.GetterImposterBuilder.DefaultPropertyBehaviourField))
            .AddMember(SinglePrivateReadonlyVariableField(WellKnownTypes.Imposter.Abstractions.ImposterMode, "_invocationBehavior"))
            .AddMember(SinglePrivateReadonlyVariableField(PredefinedType(Token(SyntaxKind.StringKeyword)), "_propertyDisplayName"))
            .AddMember(SingleVariableField(WellKnownTypes.Bool, "_hasConfiguredReturn", SyntaxKind.PrivateKeyword))
            .AddMember(BuildConstructor(property))
            .AddMember(BuildAddGetterReturnValueMethod(property.GetterImposterBuilder, property.DefaultPropertyBehaviour))
            .AddMembers(BuildReturnsMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMembers(BuildThrowsMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(BuildGetterCallbackMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(BuildGetterCalledMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(BuildThenMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(property.GetterImposterBuilderInterface.InitialThenMethod is not null
                ? BuildInitialThenMethod(property)
                : null)
            .AddMember(property.Core.GetterSupportsBaseImplementation ? BuildEnableBaseImplementationMethod(property) : null)
            .AddMember(property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod is not null
                ? BuildUseBaseImplementationInterfaceMethod(property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod.Value)
                : null)
            .AddMember(property.GetterImposterBuilderInterface.UseBaseImplementationMethod is not null
                ? BuildUseBaseImplementationInterfaceMethod(property.GetterImposterBuilderInterface.UseBaseImplementationMethod.Value)
                : null)
            .AddMember(BuildGetMethod(property.GetterImposterBuilder, property.DefaultPropertyBehaviour))
            .AddMember(BuildEnsureGetterConfiguredMethod());

        return builder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterPropertyMetadata property)
    {
        var constructor = new ConstructorBuilder(property.GetterImposterBuilder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(ParameterSyntax(property.GetterImposterBuilder.DefaultPropertyBehaviourField.Type, property.GetterImposterBuilder.DefaultPropertyBehaviourField.Name))
            .AddParameter(ParameterSyntax(WellKnownTypes.Imposter.Abstractions.ImposterMode, "invocationBehavior"))
            .AddParameter(ParameterSyntax(PredefinedType(Token(SyntaxKind.StringKeyword)), "propertyDisplayName"));

        var body = new BlockBuilder()
            .AddStatement(
            ThisExpression()
                .Dot(IdentifierName(property.GetterImposterBuilder.DefaultPropertyBehaviourField.Name))
                .Assign(IdentifierName(property.GetterImposterBuilder.DefaultPropertyBehaviourField.Name))
                .ToStatementSyntax())
            .AddStatement(
            ThisExpression()
                .Dot(IdentifierName("_invocationBehavior"))
                .Assign(IdentifierName("invocationBehavior"))
                .ToStatementSyntax())
            .AddStatement(
            ThisExpression()
                .Dot(IdentifierName("_propertyDisplayName"))
                .Assign(IdentifierName("propertyDisplayName"))
                .ToStatementSyntax());

        return constructor.WithBody(body.Build()).Build();
    }

    private static FieldDeclarationSyntax BuildGetterReturnValuesField(in PropertyGetterImposterBuilderMetadata getterImposterBuilder) =>
        SinglePrivateReadonlyVariableField(
            getterImposterBuilder.ReturnValuesField.TypeSyntax,
            getterImposterBuilder.ReturnValuesField.Name,
            getterImposterBuilder.ReturnValuesField.TypeSyntax.New());

    private static FieldDeclarationSyntax BuildGetterCallbacksField(in PropertyGetterImposterBuilderMetadata getterImposterBuilder) =>
        SinglePrivateReadonlyVariableField(
            getterImposterBuilder.CallbacksField.TypeSyntax,
            getterImposterBuilder.CallbacksField.Name,
            getterImposterBuilder.CallbacksField.TypeSyntax.New());

    private static FieldDeclarationSyntax BuildLastGetterReturnValueField(in PropertyGetterImposterBuilderMetadata getterImposterBuilder) =>
        SingleVariableField(
            getterImposterBuilder.LastReturnValueField.TypeSyntax,
            getterImposterBuilder.LastReturnValueField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.VolatileKeyword)),
            DiscardParameterGoesTo(Default)
        );

    private static FieldDeclarationSyntax BuildGetterInvocationCountField(in PropertyGetterImposterBuilderMetadata getterImposterBuilder) =>
        SingleVariableField(
            getterImposterBuilder.InvocationCountField.TypeSyntax,
            getterImposterBuilder.InvocationCountField.Name,
            SyntaxKind.PrivateKeyword);

    private static MethodDeclarationSyntax BuildAddGetterReturnValueMethod(
        in PropertyGetterImposterBuilderMetadata getterImposterBuilder,
        in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour) =>
        new MethodDeclarationBuilder(getterImposterBuilder.AddReturnValueMethod.ReturnType, getterImposterBuilder.AddReturnValueMethod.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(ParameterSyntax(getterImposterBuilder.AddReturnValueMethod.ValueGeneratorParameter))
            .WithBody(Block(
            IdentifierName(getterImposterBuilder.DefaultPropertyBehaviourField.Name)
            .Dot(IdentifierName(defaultPropertyBehaviour.IsOnField.Name))
            .Assign(False)
            .ToStatementSyntax(),
            IdentifierName(getterImposterBuilder.ReturnValuesField.Name)
            .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
            .Call(Argument(IdentifierName(getterImposterBuilder.AddReturnValueMethod.ValueGeneratorParameter.Name)))
            .ToStatementSyntax(),
            IdentifierName("_hasConfiguredReturn")
                .Assign(True)
                .ToStatementSyntax()
            ))
            .Build();

    private static MethodDeclarationSyntax[] BuildReturnsMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface) =>
    [
        new MethodDeclarationBuilder(builderInterface.ReturnsMethod.ReturnType, builderInterface.ReturnsMethod.Name)
            .AddParameter(ParameterSyntax(builderInterface.ReturnsMethod.ValueParameter))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.ReturnsMethod.InterfaceSyntax))
            .WithBody(Block(
                IdentifierName(builder.AddReturnValueMethod.Name)
                    .Call(
                        Argument(
                            IgnoreBaseImplementationLambda(
                                IdentifierName(builderInterface.ReturnsMethod.ValueParameter.Name)))
                    )
                    .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build(),

        new MethodDeclarationBuilder(builderInterface.ReturnsMethod.ReturnType, builderInterface.ReturnsMethod.Name)
            .AddParameter(ParameterSyntax(builderInterface.ReturnsMethod.ValueGeneratorParameter))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.ReturnsMethod.InterfaceSyntax))
            .WithBody(Block(
                IdentifierName(builder.AddReturnValueMethod.Name)
                    .Call(
                        Argument(
                            IgnoreBaseImplementationLambda(
                                IdentifierName(builderInterface.ReturnsMethod.ValueGeneratorParameter.Name).Call()
                            )
                        )
                    )
                    .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build(),
    ];

    private static MethodDeclarationSyntax[] BuildThrowsMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface) =>
    [
        new MethodDeclarationBuilder(builderInterface.ThrowsMethod.ReturnType, builderInterface.ThrowsMethod.Name)
            .AddParameter(ParameterSyntax(builderInterface.ThrowsMethod.ExceptionParameter))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.ThrowsMethod.InterfaceSyntax))
            .WithBody(Block(
                IdentifierName(builder.AddReturnValueMethod.Name)
                    .Call(
                        Argument(
                            IgnoreBaseImplementationLambda(
                                ThrowExpression(IdentifierName(builderInterface.ThrowsMethod.ExceptionParameter.Name))
                            )
                        )
                    )
                    .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build(),

        new MethodDeclarationBuilder(builderInterface.ThrowsMethod.ReturnType, builderInterface.ThrowsMethod.Name)
            .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter(builderInterface.ThrowsMethod.GenericTypeParameterName))))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.ThrowsMethod.InterfaceSyntax))
            .WithBody(Block(
                IdentifierName(builder.AddReturnValueMethod.Name)
                    .Call(
                        Argument(
                            IgnoreBaseImplementationLambda(
                                ThrowExpression(IdentifierName(builderInterface.ThrowsMethod.GenericTypeParameterName).New())
                            )
                        )
                    )
                    .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build()
    ];

    internal static MethodDeclarationSyntax BuildGetterCallbackMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface) =>
        new MethodDeclarationBuilder(builderInterface.CallbackMethod.ReturnType, builderInterface.CallbackMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.CallbackMethod.InterfaceSyntax))
            .AddParameter(ParameterSyntax(builderInterface.CallbackMethod.CallbackParameter))
            .WithBody(Block(
            IdentifierName(builder.CallbacksField.Name)
                .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                .Call(Argument(IdentifierName(builderInterface.CallbackMethod.CallbackParameter.Name)))
                .ToStatementSyntax(),
                ReturnStatement(ThisExpression())
            ))
            .Build();

    internal static MethodDeclarationSyntax BuildGetterCalledMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface) =>
        new MethodDeclarationBuilder(builderInterface.CalledMethod.ReturnType, builderInterface.CalledMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.VerificationInterfaceTypeSyntax))
            .AddParameter(ParameterSyntax(builderInterface.CalledMethod.CountParameter))
            .WithBody(Block(
                IfStatement(
                    Not(
                        IdentifierName(builderInterface.CalledMethod.CountParameter.Name)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(IdentifierName(builder.InvocationCountField.Name)))
                    ),
                    ThrowStatement(WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                        .New(ArgumentList(SeparatedList(
                                [
                                    Argument(IdentifierName(builderInterface.CalledMethod.CountParameter.Name)),
                                    Argument(IdentifierName(builder.InvocationCountField.Name))
                                ]
                            )
                        ))
                    )
                )
            ))
            .Build();

    private static MethodDeclarationSyntax BuildThenMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface) =>
        new MethodDeclarationBuilder(builderInterface.ThenMethod.ReturnType, builderInterface.ThenMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.ThenMethod.InterfaceSyntax))
            .WithBody(Block(
                ReturnStatement(ThisExpression())
            ))
            .Build();

    private static MethodDeclarationSyntax BuildInitialThenMethod(in ImposterPropertyMetadata property)
    {
        var method = property.GetterImposterBuilderInterface.InitialThenMethod!.Value;

        return new MethodDeclarationBuilder(method.ReturnType, method.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.GetterImposterBuilderInterface.TypeSyntax))
            .WithBody(Block(
                ReturnStatement(ThisExpression())
            ))
            .Build();
    }

    private static MethodDeclarationSyntax BuildUseBaseImplementationInterfaceMethod(GetterUseBaseImplementationMethodMetadata methodMetadata) =>
        new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(methodMetadata.InterfaceSyntax))
            .WithBody(Block(
                IdentifierName("EnableBaseImplementation").Call().ToStatementSyntax(),
                ReturnStatement(ThisExpression())
            ))
            .Build();

    private static MethodDeclarationSyntax BuildEnableBaseImplementationMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "EnableBaseImplementation")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(
                IdentifierName(property.GetterImposterBuilder.AddReturnValueMethod.Name)
                    .Call(Argument(BuildBaseImplementationReturnHandler(property)))
                    .ToStatementSyntax()
            ))
            .Build();

    private static ParenthesizedLambdaExpressionSyntax BuildBaseImplementationReturnHandler(in ImposterPropertyMetadata property)
    {
        const string BaseImplementationParameterName = "baseImplementation";

        var baseImplementationParameter = Parameter(Identifier(BaseImplementationParameterName))
            .WithType(property.Core.AsSystemFuncType.ToNullableType());
        var baseImplementationIdentifier = IdentifierName(BaseImplementationParameterName);
        var messageExpression = BinaryExpression(
            SyntaxKind.AddExpression,
            IdentifierName("_propertyDisplayName"),
            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(" (getter)")));

        return ParenthesizedLambdaExpression()
            .WithParameterList(ParameterList(SingletonSeparatedList(baseImplementationParameter)))
            .WithBlock(
                Block(
                    IfStatement(
                        baseImplementationIdentifier.IsNotNull(),
                        ReturnStatement(baseImplementationIdentifier.Call()),
                        ElseClause(
                            ThrowStatement(
                                WellKnownTypes.Imposter.Abstractions.MissingImposterException
                                    .New(
                                        Argument(messageExpression)
                                            .AsSingleArgumentListSyntax()
                                    )
                            )
                        )
                    )
                )
            );
    }

    private static ParenthesizedLambdaExpressionSyntax IgnoreBaseImplementationLambda(ExpressionSyntax body) =>
        ParenthesizedLambdaExpression(body)
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(
                        Parameter(Identifier("_"))
                    )
                )
            );

    internal static MethodDeclarationSyntax BuildGetMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour)
    {
        var baseImplementationIdentifier = IdentifierName(builder.GetMethod.BaseImplementationParameter.Name);
        const string ReturnValueVariableName = "returnValue";
        const string NextReturnValueVariableName = "nextReturnValue";

        return new MethodDeclarationBuilder(builder.GetMethod.ReturnType, builder.GetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(builder.GetMethod.BaseImplementationParameter))
            .WithBody(Block(
                    IdentifierName("EnsureGetterConfigured").Call().ToStatementSyntax(),
                    TrackGetterInvocation(builder),
                    InvokeGetterCallbacks(builder),
                    IfAutoPropertyBehaviourReturnBackingField(builder, defaultPropertyBehaviour, baseImplementationIdentifier),
                    DequeNextGetterReturnValue(builder, ReturnValueVariableName),
                    DeclareNextGetterReturnValue(builder, ReturnValueVariableName, NextReturnValueVariableName),
                    UpdateLastGetterReturnValue(builder, NextReturnValueVariableName),
                    ReturnNextGetterReturnValue(NextReturnValueVariableName, baseImplementationIdentifier)
                )
            )
            .Build();

        static StatementSyntax DequeNextGetterReturnValue(
            in PropertyGetterImposterBuilderMetadata builder,
            string variableName) =>
            IdentifierName(builder.ReturnValuesField.Name)
                .Dot(ConcurrentQueueSyntaxHelper.TryDequeue)
                .Call(
                    Argument(
                        null,
                        Token(SyntaxKind.OutKeyword),
                        DeclarationExpression(
                            Var,
                            SingleVariableDesignation(Identifier(variableName))))
                )
                .ToStatementSyntax();

        static LocalDeclarationStatementSyntax DeclareNextGetterReturnValue(
            in PropertyGetterImposterBuilderMetadata builder,
            string returnValueVariableName,
            string nextVariableName) =>
            LocalVariableDeclarationSyntax(
                Var,
                nextVariableName,
                IdentifierName(returnValueVariableName)
                    .Coalesce(IdentifierName(builder.LastReturnValueField.Name))
            );

        static StatementSyntax UpdateLastGetterReturnValue(
            in PropertyGetterImposterBuilderMetadata builder,
            string nextVariableName) =>
            IfStatement(
                IdentifierName(nextVariableName).IsNotNull(),
                IdentifierName(builder.LastReturnValueField.Name)
                    .Assign(IdentifierName(nextVariableName))
                    .ToStatementSyntax()
            );

        static StatementSyntax ReturnNextGetterReturnValue(string nextVariableName, ExpressionSyntax baseImplementationIdentifier) =>
            ReturnStatement(
                IdentifierName(nextVariableName)
                    .Call(Argument(baseImplementationIdentifier))
            );


        static StatementSyntax IfAutoPropertyBehaviourReturnBackingField(
            in PropertyGetterImposterBuilderMetadata builder,
            in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour,
            ExpressionSyntax baseImplementationIdentifier)
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
            var seedFromBaseCondition =
                BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName(builder.InvocationCountField.Name),
                        LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))
                    .And(baseImplementationIsNotNull)
                    .And(Not(hasValueSetCheck));

            var seedFromBase =
                IdentifierName(builder.DefaultPropertyBehaviourField.Name)
                    .Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name))
                    .Assign(baseImplementationIdentifier.Call())
                    .ToStatementSyntax();

            return IfStatement(
                defaultBehaviourCheck,
                Block(
                    IfStatement(seedFromBaseCondition, Block(seedFromBase)),
                    returnBackingField
                )
            );
        }

        static StatementSyntax InvokeGetterCallbacks(in PropertyGetterImposterBuilderMetadata builder) =>
            ForEachStatement(
                type: Var,
                identifier: Identifier("getterCallback"),
                expression: IdentifierName(builder.CallbacksField.Name),
                statement: Block(
                IdentifierName("getterCallback")
                    .Call()
                    .ToStatementSyntax()
                )
            );

        static StatementSyntax TrackGetterInvocation(in PropertyGetterImposterBuilderMetadata builder) =>
        WellKnownTypes.System.Threading.Interlocked
            .Dot(IdentifierName("Increment"))
            .Call(Argument(
                    null,
                    Token(SyntaxKind.RefKeyword),
                    IdentifierName(builder.InvocationCountField.Name)
                )
            )
            .ToStatementSyntax();
    }

    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod()
    {
        var condition =
            BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    IdentifierName("_invocationBehavior"),
                    QualifiedName(WellKnownTypes.Imposter.Abstractions.ImposterMode, IdentifierName("Explicit")))
                .And(Not(IdentifierName("_hasConfiguredReturn")));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "EnsureGetterConfigured")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(Block(
                IfStatement(
                    condition,
                    ThrowStatement(
                        ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                            .WithArgumentList(
                                Argument(
                                        BinaryExpression(
                                            SyntaxKind.AddExpression,
                                            IdentifierName("_propertyDisplayName"),
                                            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(" (getter)"))))
                                    .AsSingleArgumentListSyntax()
                            )
                    )
                )
            ))
            .Build();
    }
}
