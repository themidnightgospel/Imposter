using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Getter;

internal static class GetterImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterPropertyMetadata property) =>
        new ClassDeclarationBuilder(property.GetterImposterBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.TypeSyntax))
            .AddMember(BuildGetterReturnValuesField(property.GetterImposterBuilder))
            .AddMember(BuildGetterCallbacksField(property.GetterImposterBuilder))
            .AddMember(BuildLastGetterReturnValueField(property.GetterImposterBuilder))
            .AddMember(BuildGetterInvocationCountField(property.GetterImposterBuilder))
            .AddMember(SinglePrivateReadonlyVariableField(property.GetterImposterBuilder.DefaultPropertyBehaviourField))
            .AddMember(BuildConstructor(property))
            .AddMember(BuildAddGetterReturnValueMethod(property.GetterImposterBuilder, property.DefaultPropertyBehaviour))
            .AddMembers(BuildReturnsMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMembers(BuildThrowsMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(BuildGetterCallbackMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(BuildGetterCalledMethod(property.GetterImposterBuilder, property.GetterImposterBuilderInterface))
            .AddMember(BuildGetMethod(property.GetterImposterBuilder, property.DefaultPropertyBehaviour))
            .Build();

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterPropertyMetadata property) =>
        new ConstructorWithFieldInitializationBuilder(property.GetterImposterBuilder.Name)
            .WithModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameter(property.GetterImposterBuilder.DefaultPropertyBehaviourField)
            .Build();

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
            TokenList(Token(SyntaxKind.PrivateKeyword)),
            EmptyParametersGoesTo(Default)
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
                    .ToStatementSyntax()
            ))
            .Build();

    private static MethodDeclarationSyntax[] BuildReturnsMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in PropertyGetterImposterBuilderInterfaceMetadata builderInterface) =>
    [
        new MethodDeclarationBuilder(builderInterface.ReturnsMethod.ReturnType, builderInterface.ReturnsMethod.Name)
            .AddParameter(ParameterSyntax(builderInterface.ReturnsMethod.ValueParameter))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.TypeSyntax))
            .WithBody(Block(
                    IdentifierName(builder.AddReturnValueMethod.Name)
                        .Call(
                            Argument(
                                EmptyParametersGoesTo(IdentifierName(builderInterface.ReturnsMethod.ValueParameter.Name)))
                        )
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build(),

        new MethodDeclarationBuilder(builderInterface.ReturnsMethod.ReturnType, builderInterface.ReturnsMethod.Name)
            .AddParameter(ParameterSyntax(builderInterface.ReturnsMethod.ValueGeneratorParameter))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.TypeSyntax))
            .WithBody(Block(
                    IdentifierName(builder.AddReturnValueMethod.Name)
                        .Call(Argument(IdentifierName(builderInterface.ReturnsMethod.ValueGeneratorParameter.Name)))
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
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.TypeSyntax))
            .WithBody(Block(
                    IdentifierName(builder.AddReturnValueMethod.Name)
                        .Call(
                            Argument(
                                EmptyParametersGoesTo(
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
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.TypeSyntax))
            .WithBody(Block(
                    IdentifierName(builder.AddReturnValueMethod.Name)
                        .Call(
                            Argument(
                                EmptyParametersGoesTo(
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
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.TypeSyntax))
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
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(builderInterface.TypeSyntax))
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
    
    internal static MethodDeclarationSyntax BuildGetMethod(
        in PropertyGetterImposterBuilderMetadata builder,
        in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour)
    {
        return new MethodDeclarationBuilder(builder.GetMethod.ReturnType, builder.GetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(
                    TrackGetterInvocation(builder),
                    InvokeGetterCallbacks(builder),
                    IfAutoPropertyBehaviourReturnBackingField(builder, defaultPropertyBehaviour),
                    DequeNextGetterReturnValue(builder),
                    ReturnStatement(IdentifierName(builder.LastReturnValueField.Name).Call())
                )
            )
            .Build();

        static StatementSyntax DequeNextGetterReturnValue(in PropertyGetterImposterBuilderMetadata builder) =>
            IfStatement(
                IdentifierName(builder.ReturnValuesField.Name)
                    .Dot(ConcurrentQueueSyntaxHelper.TryDequeue)
                    .Call(Argument(
                        null,
                        Token(SyntaxKind.OutKeyword),
                        DeclarationExpression(
                            Var,
                            SingleVariableDesignation(Identifier("returnValue"))
                        ))),
                IdentifierName(builder.LastReturnValueField.Name).Assign(IdentifierName("returnValue")).ToStatementSyntax()
            );


        static StatementSyntax IfAutoPropertyBehaviourReturnBackingField(
            in PropertyGetterImposterBuilderMetadata builder,
            in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour) =>
            IfStatement(
                IdentifierName(builder.DefaultPropertyBehaviourField.Name).Dot(IdentifierName(defaultPropertyBehaviour.IsOnField.Name)),
                ReturnStatement(IdentifierName(builder.DefaultPropertyBehaviourField.Name).Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name)))
            );

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
}