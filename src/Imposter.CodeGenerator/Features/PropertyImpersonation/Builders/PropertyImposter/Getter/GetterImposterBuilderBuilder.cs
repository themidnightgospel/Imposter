using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.ThenMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;

internal static partial class GetterImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterPropertyMetadata property)
    {
        if (property.Core.UsesSharedRuntime)
        {
            return BuildShared(property);
        }

        var builder = new ClassDeclarationBuilder(property.GetterImposterBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(
                SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource)
            )
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.TypeSyntax))
            .AddBaseType(
                SimpleBaseType(property.GetterImposterBuilderInterface.FluentInterfaceTypeSyntax)
            );

        if (property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod is { } entry)
        {
            builder = builder.AddBaseType(SimpleBaseType(entry.InterfaceSyntax));
        }

        builder = builder
            .AddMember(
                SingleVariableField(
                    WellKnownTypes.Imposter.Abstractions.IImposter.ToNullableType(),
                    InvocationStepNames.OwnerPropertyName,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                )
            )
            .AddMember(
                LazyCollectionSyntaxHelper.Field(property.GetterImposterBuilder.ReturnValuesField)
            )
            .AddMember(
                LazyCollectionSyntaxHelper.Field(property.GetterImposterBuilder.CallbacksField)
            )
            .AddMember(BuildLastGetterReturnValueField(property))
            .AddMember(
                SingleVariableField(
                    property.GetterImposterBuilder.VerificationField.Type,
                    property.GetterImposterBuilder.VerificationField.Name,
                    TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
                    property.GetterImposterBuilder.VerificationField.Type.New()
                )
            )
            .AddMembers(
                PropertyAccessorStateBuilder.Fields(property, property.GetterImposterBuilder.State)
            )
            .AddMember(
                PropertyAccessorStateBuilder.Constructor(
                    property.GetterImposterBuilder.Name,
                    property,
                    property.GetterImposterBuilder.State
                )
            )
            .AddMember(BuildAddGetterReturnValueMethod(property))
            .AddMember(
                InvocationLifecycleBuilder.Clear([
                    IdentifierName(property.GetterImposterBuilder.VerificationField.Name)
                        .Dot(IdentifierName("Clear"))
                        .Call()
                        .ToStatementSyntax(),
                ])
            )
            .AddMembers(BuildReturnsMethods(property))
            .AddMembers(
                BuildThrowsMethods(
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
            .AddMembers(BuildThenMethods(property))
            .AddMembers(BuildCounterVerificationMembers(property))
            .AddMember(
                BuildGetterCalledMethod(
                    property.GetterImposterBuilder,
                    property.GetterImposterBuilderInterface,
                    true
                )
            )
            .AddMember(
                property.Core.GetterSupportsBaseImplementation
                    ? BuildUseBaseImplementationMethod(property)
                    : null
            )
            .AddMembers(BuildUseBaseImplementationMethods(property))
            .AddMember(BuildGetMethod(property))
            .AddMember(BuildNextReturnValueMethod(property.GetterImposterBuilder))
            .AddMember(
                PropertyAccessorStateBuilder.EnsureConfiguredMethod(
                    property.GetterImposterBuilder.State
                )
            );

        return builder.Build();
    }

    // A value passed through can't be kept like the default behaviour keeps one, so until a setup says otherwise the
    // getter reads the base getter, if there is one, every time.
    private static FieldDeclarationSyntax BuildLastGetterReturnValueField(
        in ImposterPropertyMetadata property
    )
    {
        var getterImposterBuilder = property.GetterImposterBuilder;

        return SingleVariableField(
            getterImposterBuilder.LastReturnValueField.Type,
            getterImposterBuilder.LastReturnValueField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.VolatileKeyword)),
            property.Core.IsPassedThrough && property.Core.GetterSupportsBaseImplementation
                ? BaseImplementationOrDefault(
                    getterImposterBuilder.GetMethod.BaseImplementationParameter.Name
                )
                : DiscardParameterGoesTo(DefaultNonNullable)
        );
    }

    private static SimpleLambdaExpressionSyntax BaseImplementationOrDefault(
        string baseImplementationName
    )
    {
        var baseImplementation = IdentifierName(baseImplementationName);

        return Identifier(baseImplementationName)
            .Lambda(
                ConditionalExpression(
                    baseImplementation.IsNotNull(),
                    baseImplementation.Call(),
                    DefaultNonNullable
                )
            );
    }

    private static MethodDeclarationSyntax BuildAddGetterReturnValueMethod(
        in ImposterPropertyMetadata property
    )
    {
        var getterImposterBuilder = property.GetterImposterBuilder;
        var turnDefaultBehaviourOff = property.Core.KeepsValue
            ? IdentifierName(getterImposterBuilder.State.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(DefaultPropertyBehaviourMetadata.TurnOffMethodName))
                .Call()
            : null;

        return new MethodDeclarationBuilder(
            getterImposterBuilder.AddReturnValueMethod.ReturnType,
            getterImposterBuilder.AddReturnValueMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(ParameterSyntax(getterImposterBuilder.AddReturnValueParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(turnDefaultBehaviourOff)
                    .AddExpression(
                        IdentifierName(getterImposterBuilder.VerificationField.Name)
                            .Dot(IdentifierName("Configure"))
                            .Call()
                    )
                    .AddExpression(
                        LazyCollectionSyntaxHelper
                            .EnsureInitialized(getterImposterBuilder.ReturnValuesField.Name)
                            .Dot(IdentifierName("Add"))
                            .Call(
                                Argument(
                                    IdentifierName(
                                        getterImposterBuilder.AddReturnValueParameter.Name
                                    )
                                )
                            )
                    )
                    .AddExpression(
                        IdentifierName(getterImposterBuilder.State.HasConfiguredField.Name)
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
            .WithBody(AddReturnValueAndReturnThis(property.GetterImposterBuilder, returnedValue))
            .Build();
    }

    private static MethodDeclarationSyntax[] BuildThrowsMethods(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface
    )
    {
        var throws = builderInterface.ThrowsMethod;

        return
        [
            new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
                .AddParameter(ParameterSyntax(throws.ExceptionParameter))
                .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
                .WithBody(
                    AddReturnValueAndReturnThis(
                        builder,
                        ThrowExpression(IdentifierName(throws.ExceptionParameter.Name))
                    )
                )
                .Build(),
            new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
                .WithTypeParameters(throws.ExceptionTypeParameter.TypeParameterList)
                .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
                .WithBody(
                    AddReturnValueAndReturnThis(
                        builder,
                        ThrowExpression(IdentifierName(throws.ExceptionTypeParameter.Name).New())
                    )
                )
                .Build(),
        ];
    }

    // The outcome ignores the base getter that Get passes every outcome.
    private static BlockSyntax AddReturnValueAndReturnThis(
        in PropertyGetterImposterBuilderMetadata builder,
        ExpressionSyntax outcome
    ) =>
        Block(
            IdentifierName(builder.AddReturnValueMethod.Name)
                .Call(Argument(IgnoreBaseImplementationLambda(outcome)))
                .ToStatementSyntax(),
            ReturnThis
        );

    private static MethodDeclarationSyntax BuildGetterCallbackMethod(
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
                    LazyCollectionSyntaxHelper
                        .EnsureInitialized(builder.CallbacksField.Name)
                        .Dot(IdentifierName("Add"))
                        .Call(
                            Argument(
                                IdentifierName(
                                    builderInterface.CallbackMethod.CallbackParameter.Name
                                )
                            )
                        )
                        .ToStatementSyntax(),
                    IdentifierName(builder.VerificationField.Name)
                        .Dot(IdentifierName("Configure"))
                        .Call()
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterCalledMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface,
        bool withReason = false
    )
    {
        var count = IdentifierName(builderInterface.CalledMethod.CountParameter.Name);

        var method = new MethodDeclarationBuilder(
            builderInterface.CalledMethod.ReturnType,
            builderInterface.CalledMethod.Name
        )
            .WithExplicitInterfaceSpecifier(builderInterface.VerificationInterfaceTypeSyntax)
            .AddParameter(ParameterSyntax(builderInterface.CalledMethod.CountParameter));
        if (withReason)
            method.AddParameter(ParameterSyntax(builderInterface.CalledMethod.BecauseParameter));
        return method
            .WithBody(
                Block(
                    IdentifierName(builder.VerificationField.Name)
                        .Dot(IdentifierName("Verify"))
                        .Call([
                            Argument(count),
                            Argument(
                                IdentifierName(
                                        PropertyAccessorStateMetadata.PropertyDisplayNameField.Name
                                    )
                                    .Add(" (getter)".StringLiteral())
                            ),
                            Argument(
                                withReason
                                    ? IdentifierName(
                                        builderInterface.CalledMethod.BecauseParameter.Name
                                    )
                                    : Null
                            ),
                            Argument(IdentifierName(InvocationStepNames.OwnerPropertyName)),
                        ])
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static List<MethodDeclarationSyntax> BuildThenMethods(
        in ImposterPropertyMetadata property
    )
    {
        List<MethodDeclarationSyntax> methods =
        [
            ThenReturningThis(property.GetterImposterBuilderInterface.ThenMethod),
        ];
        if (property.GetterImposterBuilderInterface.InitialThenMethod is { } initialThen)
        {
            methods.Add(ThenReturningThis(initialThen));
        }

        return methods;
    }

    private static List<MethodDeclarationSyntax> BuildUseBaseImplementationMethods(
        in ImposterPropertyMetadata property
    )
    {
        var builderInterface = property.GetterImposterBuilderInterface;
        List<MethodDeclarationSyntax> methods = [];
        if (builderInterface.UseBaseImplementationEntryMethod is { } entry)
        {
            methods.Add(
                BuildUseBaseImplementationInterfaceMethod(property.GetterImposterBuilder, entry)
            );
        }

        if (builderInterface.UseBaseImplementationMethod is { } useBaseImplementation)
        {
            methods.Add(
                BuildUseBaseImplementationInterfaceMethod(
                    property.GetterImposterBuilder,
                    useBaseImplementation
                )
            );
        }

        return methods;
    }

    private static MethodDeclarationSyntax BuildUseBaseImplementationInterfaceMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        UseBaseImplementationMethodMetadata methodMetadata
    ) =>
        new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithExplicitInterfaceSpecifier(methodMetadata.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(builder.UseBaseImplementationMethod.Name)
                        .Call()
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildUseBaseImplementationMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.GetterImposterBuilder.UseBaseImplementationMethod.ReturnType,
            property.GetterImposterBuilder.UseBaseImplementationMethod.Name
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
                            PropertyAccessorStateBuilder.ThrowMissingImposter(
                                property.GetterImposterBuilder.State
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

    private static MethodDeclarationSyntax BuildGetMethod(in ImposterPropertyMetadata property)
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
                    .AddStatement(TrackGetterInvocation(builder))
                    .AddExpression(IdentifierName(builder.State.EnsureConfiguredMethodName).Call())
                    .AddExpression(
                        IdentifierName(builder.VerificationField.Name)
                            .Dot(IdentifierName("Use"))
                            .Call()
                    )
                    .AddStatement(InvokeGetterCallbacks(builder))
                    .AddStatement(
                        property.Core.KeepsValue
                            ? IfAutoPropertyBehaviourReturnValue(
                                builder,
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

        static StatementSyntax IfAutoPropertyBehaviourReturnValue(
            in PropertyGetterImposterBuilderMetadata builder,
            ExpressionSyntax baseImplementationIdentifier
        )
        {
            var defaultPropertyBehaviour = IdentifierName(
                builder.State.DefaultPropertyBehaviourField.Name
            );

            return IfStatement(
                defaultPropertyBehaviour.Dot(
                    IdentifierName(DefaultPropertyBehaviourMetadata.IsOnPropertyName)
                ),
                Block(
                    ReturnStatement(
                        defaultPropertyBehaviour
                            .Dot(IdentifierName(DefaultPropertyBehaviourMetadata.ReadMethodName))
                            .Call(Argument(baseImplementationIdentifier))
                    )
                )
            );
        }

        static StatementSyntax InvokeGetterCallbacks(
            in PropertyGetterImposterBuilderMetadata builder
        ) =>
            LazyCollectionSyntaxHelper.UnlessNull(
                ForEachStatement(
                    type: Var,
                    identifier: Identifier("getterCallback"),
                    expression: IdentifierName(builder.CallbacksField.Name),
                    statement: Block(IdentifierName("getterCallback").Call().ToStatementSyntax())
                )
            );

        static StatementSyntax TrackGetterInvocation(
            in PropertyGetterImposterBuilderMetadata builder
        ) =>
            IdentifierName(builder.VerificationField.Name)
                .Dot(IdentifierName("Record"))
                .Call()
                .ToStatementSyntax();
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
                    builder.ReturnValuesField.Name,
                    IdentifierName(builder.LastReturnValueField.Name)
                )
            )
            .Build();
}
