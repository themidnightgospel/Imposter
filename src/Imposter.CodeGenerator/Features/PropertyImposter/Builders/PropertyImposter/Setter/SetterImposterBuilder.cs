using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Setter;

internal static class SetterImposterBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasSetter)
        {
            return null;
        }

        return new ClassDeclarationBuilder(property.SetterImposter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.CallbacksField.Type, CallbacksFieldMetadata.Name, property.SetterImposter.CallbacksField.Type.New()))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.InvocationHistoryField, property.SetterImposter.InvocationHistoryField.Type.New()))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.DefaultPropertyBehaviourField))
            .AddMember(SinglePrivateReadonlyVariableField(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior, "_invocationBehavior"))
            .AddMember(SinglePrivateReadonlyVariableField(PredefinedType(Token(SyntaxKind.StringKeyword)), "_propertyDisplayName"))
            .AddMember(SingleVariableField(PredefinedType(Token(SyntaxKind.BoolKeyword)), "_hasConfiguredSetter", SyntaxKind.PrivateKeyword))
            .AddMember(BuildConstructor(property))
            .AddMember(BuildSetterCallbackMethod(property.SetterImposter))
            .AddMember(BuildSetterCalledMethod(property.SetterImposter))
            .AddMember(BuildSetMethod(property.SetterImposter, property.DefaultPropertyBehaviour))
            .AddMember(BuildEnsureSetterConfiguredMethod())
            .AddMember(BuildMarkConfiguredMethod())
            .AddMember(SetterImposterBuilderBuilder.Build(property))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterPropertyMetadata property)
    {
        var constructor = new ConstructorBuilder(property.SetterImposter.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(ParameterSyntax(property.SetterImposter.DefaultPropertyBehaviourField.Type, property.SetterImposter.DefaultPropertyBehaviourField.Name))
            .AddParameter(Parameter(Identifier("invocationBehavior")).WithType(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior))
            .AddParameter(Parameter(Identifier("propertyDisplayName")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))));

        var body = new BlockBuilder()
            .AddStatement(
                ThisExpression()
                    .Dot(IdentifierName(property.SetterImposter.DefaultPropertyBehaviourField.Name))
                    .Assign(IdentifierName(property.SetterImposter.DefaultPropertyBehaviourField.Name))
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


    internal static MethodDeclarationSyntax BuildSetMethod(in PropertySetterImposterMetadata setterImposter, in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour)
    {
        var baseImplementationIdentifier = IdentifierName(setterImposter.SetMethod.BaseImplementationParameter.Name);

        return new MethodDeclarationBuilder(setterImposter.SetMethod.ReturnType, setterImposter.SetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(setterImposter.SetMethod.ValueParameter))
            .AddParameter(ParameterSyntax(setterImposter.SetMethod.BaseImplementationParameter))
            .WithBody(Block(
                    IdentifierName("EnsureSetterConfigured").Call().ToStatementSyntax(),
                    TrackSetterInvocation(setterImposter),
                    InvokeCallbacks(setterImposter),
                    SetBackingField(setterImposter, defaultPropertyBehaviour, baseImplementationIdentifier)
                )
            )
            .Build();

        static StatementSyntax SetBackingField(
            in PropertySetterImposterMetadata setterImposter,
            in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour,
            ExpressionSyntax baseImplementationIdentifier)
        {
            var defaultBehaviourCheck = IdentifierName(setterImposter.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(defaultPropertyBehaviour.IsOnField.Name));

            var baseProvidedCheck = BinaryExpression(
                SyntaxKind.NotEqualsExpression,
                baseImplementationIdentifier,
                LiteralExpression(SyntaxKind.NullLiteralExpression));

            var assignBackingField = IdentifierName(setterImposter.DefaultPropertyBehaviourField.Name)
                .Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name))
                .Assign(IdentifierName(setterImposter.SetMethod.ValueParameter.Name))
                .ToStatementSyntax();

            return IfStatement(
                defaultBehaviourCheck,
                Block(
                    IfStatement(
                        baseProvidedCheck,
                        Block(
                            baseImplementationIdentifier.Call().ToStatementSyntax(),
                            ReturnStatement()
                        )
                    ),
                    assignBackingField
                )
            );
        }

        static StatementSyntax InvokeCallbacks(in PropertySetterImposterMetadata setterImposter)
        {
            return ForEachVariableStatement(
                variable: DeclarationExpression(
                    Var,
                    ParenthesizedVariableDesignation(
                        Token(SyntaxKind.OpenParenToken),
                        SeparatedList<VariableDesignationSyntax>(new SyntaxNodeOrToken[]
                        {
                            SingleVariableDesignation(Identifier("criteria")),
                            Token(SyntaxKind.CommaToken),
                            SingleVariableDesignation(Identifier("setterCallback"))
                        }),
                        Token(SyntaxKind.CloseParenToken)
                    )
                ),
                IdentifierName(CallbacksFieldMetadata.Name),
                Block(
                    IfStatement(
                        IdentifierName("criteria")
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(IdentifierName(setterImposter.SetMethod.ValueParameter.Name))),
                        IdentifierName("setterCallback")
                            .Call(Argument(IdentifierName(setterImposter.SetMethod.ValueParameter.Name)))
                            .ToStatementSyntax()
                    )
                ));
        }

        static StatementSyntax TrackSetterInvocation(in PropertySetterImposterMetadata setterImposter) =>
            IdentifierName(setterImposter.InvocationHistoryField.Name)
                .Dot(IdentifierName("Add"))
                .Call(Argument(IdentifierName(setterImposter.SetMethod.ValueParameter.Name)))
                .ToStatementSyntax();
    }

    internal static MethodDeclarationSyntax BuildSetterCalledMethod(in PropertySetterImposterMetadata setterImposter) =>
        new MethodDeclarationBuilder(setterImposter.CalledMethod.ReturnType, setterImposter.CalledMethod.Name)
            .AddParameter(ParameterSyntax(setterImposter.CalledMethod.CriteriaParameter))
            .AddParameter(ParameterSyntax(setterImposter.CalledMethod.CountParameter))
            .WithBody(Block(
                LocalVariableDeclarationSyntax(Var, setterImposter.CalledMethod.InvocationCountVariableName,
                    IdentifierName(setterImposter.InvocationHistoryField.Name)
                        .Dot(IdentifierName("Count"))
                        .Call(
                            Argument(
                                IdentifierName(setterImposter.CalledMethod.CriteriaParameter.Name)
                                    .Dot(IdentifierName("Matches")))
                        )
                ),
                IfStatement(
                    Not(
                        IdentifierName(setterImposter.CalledMethod.CountParameter.Name)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(IdentifierName(setterImposter.CalledMethod.InvocationCountVariableName)))
                    ),
                    ThrowStatement(WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                        .New(ArgumentList(SeparatedList(
                                [
                                    Argument(IdentifierName(setterImposter.CalledMethod.CountParameter.Name)),
                                    Argument(IdentifierName(setterImposter.CalledMethod.InvocationCountVariableName))
                                ]
                            )
                        ))
                    )
                )
            ))
            .Build();

    internal static MethodDeclarationSyntax? BuildSetterCallbackMethod(in PropertySetterImposterMetadata setterImposter) =>
        new MethodDeclarationBuilder(setterImposter.CallbackMethod.ReturnType, setterImposter.CallbackMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(setterImposter.CallbackMethod.CriteriaParameter))
            .AddParameter(ParameterSyntax(setterImposter.CallbackMethod.CallbackParameter))
            .WithBody(Block(
                    IdentifierName(CallbacksFieldMetadata.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(
                                setterImposter
                                    .CallbacksField
                                    .TupleTypeSyntax
                                    .New(ArgumentList(
                                            SeparatedList(
                                                [
                                                    Argument(IdentifierName(setterImposter.CallbackMethod.CriteriaParameter.Name)),
                                                    Argument(IdentifierName(setterImposter.CallbackMethod.CallbackParameter.Name)),
                                                ]
                                            )
                                        )
                                    )
                            )
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildEnsureSetterConfiguredMethod()
    {
        var condition = BinaryExpression(
            SyntaxKind.LogicalAndExpression,
            BinaryExpression(
                SyntaxKind.EqualsExpression,
                IdentifierName("_invocationBehavior"),
                QualifiedName(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior, IdentifierName("Explicit"))),
            PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, IdentifierName("_hasConfiguredSetter")));

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), "EnsureSetterConfigured")
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
                                            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(" (setter)"))))
                                    .AsSingleArgumentListSyntax()
                            )
                    )
                )
            ))
            .Build();
    }

    private static MethodDeclarationSyntax BuildMarkConfiguredMethod() =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), "MarkConfigured")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(
                IdentifierName("_hasConfiguredSetter")
                    .Assign(LiteralExpression(SyntaxKind.TrueLiteralExpression))
                    .ToStatementSyntax()
            ))
            .Build();
}
