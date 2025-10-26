using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter;

internal static class PropertyImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetPropertyMetadata property)
    {
        return new ClassDeclarationBuilder(property.PropertyImposter.Name)
            .AddBaseType(SimpleBaseType(property.PropertyImposterInterface.Syntax))
            .AddMember(BuildGetterReturnValuesField(property))
            .AddMember(BuildGetterCallbacksField(property))
            .AddMember(BuildLastGetterReturnValueField(property))
            .AddMember(BuildGetterInvocationCountField(property))
            .AddMember(BuildSetterCallbacksField(property))
            .AddMember(BuildSetterInvocationHistoryField(property))
            .AddMember(BuildUseAutoPropertyBehaviourField(property))
            .AddMember(BuildBackingFieldField(property))
            .AddMember(BuildAddGetterReturnValueMethod(property))
            .AddMembers(BuildReturnsMethod(property))
            .AddMembers(BuildThrowsMethod(property))
            .AddMember(BuildSetterCallbackMethod(property))
            .AddMember(BuildGetterCallbackMethod(property))
            .AddMember(BuildGetterCalledMethod(property))
            .AddMember(BuildSetterCalledMethod(property))
            .AddMember(BuildGetMethod(property))
            .AddMember(BuildSetMethod(property))
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildSetMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetMethod.ReturnType, property.SetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(property.SetMethod.ValueParameter))
            .WithBody(Block(
                    TrackSetterInvocation(property),
                    InvokeCallbacks(property),
                    SetBackingField(property)
                )
            )
            .Build();

        static StatementSyntax SetBackingField(in ImposterTargetPropertyMetadata property)
            => IfStatement(
                IdentifierName(property.UseAutoPropertyBehaviourField.Name),
                IdentifierName(property.BackingFieldField.Name)
                    .Assign(IdentifierName(property.SetMethod.ValueParameter.Name))
                    .ToStatementSyntax()
            );

        static StatementSyntax InvokeCallbacks(in ImposterTargetPropertyMetadata property)
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
                IdentifierName(property.SetterCallbacksField.Name),
                Block(
                    IfStatement(
                        IdentifierName("criteria")
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(IdentifierName(property.SetMethod.ValueParameter.Name))),
                        IdentifierName("setterCallback")
                            .Call(Argument(IdentifierName(property.SetMethod.ValueParameter.Name)))
                            .ToStatementSyntax()
                    )
                ));
        }

        static StatementSyntax TrackSetterInvocation(in ImposterTargetPropertyMetadata property) =>
            IdentifierName(property.SetterInvocationHistoryField.Name)
                .Dot(IdentifierName("Add"))
                .Call(Argument(IdentifierName(property.SetMethod.ValueParameter.Name)))
                .ToStatementSyntax();
    }

    internal static MethodDeclarationSyntax? BuildGetMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.GetMethod.ReturnType, property.GetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(
                    TrackGetterInvocation(property),
                    InvokeGetterCallbacks(property),
                    IfAutoPropertyBehaviourReturnBackingField(property),
                    DequeNextGetterReturnValue(property),
                    ReturnStatement(IdentifierName(property.LastGetterReturnValueField.Name).Call())
                )
            )
            .Build();

        static StatementSyntax DequeNextGetterReturnValue(in ImposterTargetPropertyMetadata property)
        {
            return IfStatement(
                IdentifierName(property.GetterReturnValuesField.Name)
                    .Dot(IdentifierName("TryDequeue"))
                    .Call(Argument(
                        null,
                        Token(SyntaxKind.OutKeyword),
                        DeclarationExpression(
                            IdentifierName("var"),
                            SingleVariableDesignation(Identifier("returnValue"))
                        ))),
                IdentifierName(property.LastGetterReturnValueField.Name).Assign(IdentifierName("returnValue")).ToStatementSyntax()
            );
        }


        static StatementSyntax IfAutoPropertyBehaviourReturnBackingField(in ImposterTargetPropertyMetadata property) =>
            IfStatement(
                IdentifierName(property.UseAutoPropertyBehaviourField.Name),
                ReturnStatement(IdentifierName(property.BackingFieldField.Name))
            );

        static StatementSyntax InvokeGetterCallbacks(in ImposterTargetPropertyMetadata property) =>
            ForEachStatement(
                type: Var,
                identifier: Identifier("getterCallback"),
                expression: IdentifierName(property.GetterCallbacksField.Name),
                statement: Block(
                    IdentifierName("getterCallback")
                        .Call()
                        .ToStatementSyntax()
                )
            );

        static StatementSyntax TrackGetterInvocation(in ImposterTargetPropertyMetadata property) =>
            WellKnownTypes.System.Threading.Interlocked
                .Dot(IdentifierName("Increment"))
                .Call(Argument(
                        null,
                        Token(SyntaxKind.RefKeyword),
                        IdentifierName(property.GetterInvocationCountField.Name)
                    )
                )
                .ToStatementSyntax();
    }


    internal static MethodDeclarationSyntax? BuildSetterCalledMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetterCalledMethod.ReturnType, property.SetterCalledMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
            .AddParameter(ParameterSyntax(property.SetterCalledMethod.CriteriaParameter))
            .AddParameter(ParameterSyntax(property.SetterCalledMethod.CountParameter))
            .WithBody(Block(
                LocalVariableDeclarationSyntax(Var, property.SetterCalledMethod.SetterInvocationCountVariableName,
                    IdentifierName(property.SetterInvocationHistoryField.Name)
                        .Dot(IdentifierName("Count"))
                        .Call(
                            Argument(
                                IdentifierName(property.SetterCalledMethod.CriteriaParameter.Name)
                                    .Dot(IdentifierName("Matches")))
                        )
                ),
                IfStatement(
                    Not(
                        IdentifierName(property.SetterCalledMethod.CountParameter.Name)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(IdentifierName(property.SetterCalledMethod.SetterInvocationCountVariableName)))
                    ),
                    ThrowStatement(WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                        .New(ArgumentList(SeparatedList(
                                [
                                    Argument(IdentifierName(property.SetterCalledMethod.CountParameter.Name)),
                                    Argument(IdentifierName(property.SetterCalledMethod.SetterInvocationCountVariableName))
                                ]
                            )
                        ))
                    )
                )
            ))
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildGetterCalledMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.GetterCalledMethod.ReturnType, property.GetterCalledMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
            .AddParameter(ParameterSyntax(property.GetterCalledMethod.CountParameter))
            .WithBody(Block(
                IfStatement(
                    Not(
                        IdentifierName(property.GetterCalledMethod.CountParameter.Name)
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(IdentifierName(property.GetterInvocationCountField.Name)))
                    ),
                    ThrowStatement(WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                        .New(ArgumentList(SeparatedList(
                                [
                                    Argument(IdentifierName(property.GetterCalledMethod.CountParameter.Name)),
                                    Argument(IdentifierName(property.GetterInvocationCountField.Name))
                                ]
                            )
                        ))
                    )
                )
            ))
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildGetterCallbackMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.GetterCallbackMethod.ReturnType, property.GetterCallbackMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
            .AddParameter(ParameterSyntax(property.GetterCallbackMethod.CallbackParameter))
            .WithBody(Block(
                IdentifierName(property.GetterCallbacksField.Name)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(IdentifierName(property.GetterCallbackMethod.CallbackParameter.Name)))
                    .ToStatementSyntax(),
                ReturnStatement(ThisExpression())
            ))
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildSetterCallbackMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetterCallbackMethod.ReturnType, property.SetterCallbackMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
            .AddParameter(ParameterSyntax(property.SetterCallbackMethod.CriteriaParameter))
            .AddParameter(ParameterSyntax(property.SetterCallbackMethod.CallbackParameter))
            .WithBody(Block(
                    IdentifierName(property.SetterCallbacksField.Name)
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(
                                property
                                    .SetterCallbacksField
                                    .TupleTypeSyntax
                                    .New(ArgumentList(
                                            SeparatedList(
                                                [
                                                    Argument(IdentifierName(property.SetterCallbackMethod.CriteriaParameter.Name)),
                                                    Argument(IdentifierName(property.SetterCallbackMethod.CallbackParameter.Name)),
                                                ]
                                            )
                                        )
                                    )
                            )
                        )
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax[]? BuildThrowsMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return
        [
            new MethodDeclarationBuilder(property.ThrowsMethod.ReturnType, property.ThrowsMethod.Name)
                .AddParameter(ParameterSyntax(property.ThrowsMethod.ExceptionParameter))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
                .WithBody(Block(
                        IdentifierName(property.AddGetterReturnValueMethod.Name)
                            .Call(
                                Argument(
                                    EmptyParametersGoesTo(
                                        ThrowExpression(IdentifierName(property.ThrowsMethod.ExceptionParameter.Name))
                                    )
                                )
                            )
                            .ToStatementSyntax(),
                        ReturnStatement(ThisExpression())
                    )
                )
                .Build(),

            new MethodDeclarationBuilder(property.ThrowsMethod.ReturnType, property.ThrowsMethod.Name)
                .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
                .WithBody(Block(
                        IdentifierName(property.AddGetterReturnValueMethod.Name)
                            .Call(
                                Argument(
                                    EmptyParametersGoesTo(
                                        ThrowExpression(IdentifierName("TException").New())
                                    )
                                )
                            )
                            .ToStatementSyntax(),
                        ReturnStatement(ThisExpression())
                    )
                )
                .Build(),
        ];
    }

    private static MethodDeclarationSyntax[]? BuildReturnsMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return
        [
            new MethodDeclarationBuilder(property.ReturnsMethod.ReturnType, property.ReturnsMethod.Name)
                .AddParameter(ParameterSyntax(property.ReturnsMethod.ValueParameter))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
                .WithBody(Block(
                        IdentifierName(property.AddGetterReturnValueMethod.Name)
                            .Call(
                                Argument(
                                    EmptyParametersGoesTo(IdentifierName(property.ReturnsMethod.ValueParameter.Name)))
                            )
                            .ToStatementSyntax(),
                        ReturnStatement(ThisExpression())
                    )
                )
                .Build(),

            new MethodDeclarationBuilder(property.ReturnsMethod.ReturnType, property.ReturnsMethod.Name)
                .AddParameter(ParameterSyntax(property.ReturnsMethod.ValueGeneratorParameter))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.PropertyImposterInterface.Syntax))
                .WithBody(Block(
                        IdentifierName(property.AddGetterReturnValueMethod.Name)
                            .Call(Argument(IdentifierName(property.ReturnsMethod.ValueGeneratorParameter.Name)))
                            .ToStatementSyntax(),
                        ReturnStatement(ThisExpression())
                    )
                )
                .Build(),
        ];
    }

    private static MethodDeclarationSyntax BuildAddGetterReturnValueMethod(in ImposterTargetPropertyMetadata property)
    {
        return new MethodDeclarationBuilder(property.AddGetterReturnValueMethod.ReturnType, property.AddGetterReturnValueMethod.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(ParameterSyntax(property.AddGetterReturnValueMethod.ValueGeneratorParameter))
            .WithBody(Block(
                IdentifierName(property.UseAutoPropertyBehaviourField.Name).Assign(False).ToStatementSyntax(),
                IdentifierName(property.GetterReturnValuesField.Name)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(IdentifierName(property.AddGetterReturnValueMethod.ValueGeneratorParameter.Name)))
                    .ToStatementSyntax()
            ))
            .Build();
    }

    private static FieldDeclarationSyntax BuildBackingFieldField(in ImposterTargetPropertyMetadata property) =>
        SingleVariableField(
            property.BackingFieldField.TypeSyntax,
            property.BackingFieldField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword)),
            Default
        );

    private static FieldDeclarationSyntax BuildUseAutoPropertyBehaviourField(in ImposterTargetPropertyMetadata property) =>
        SingleVariableField(
            property.UseAutoPropertyBehaviourField.TypeSyntax,
            property.UseAutoPropertyBehaviourField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword)),
            True
        );

    private static FieldDeclarationSyntax BuildSetterInvocationHistoryField(in ImposterTargetPropertyMetadata property) =>
        SinglePrivateReadonlyVariableField(
            property.SetterInvocationHistoryField.TypeSyntax,
            property.SetterInvocationHistoryField.Name,
            property.SetterInvocationHistoryField.TypeSyntax.New()
        );

    private static FieldDeclarationSyntax BuildSetterCallbacksField(in ImposterTargetPropertyMetadata property) =>
        SinglePrivateReadonlyVariableField(
            property.SetterCallbacksField.TypeSyntax,
            property.SetterCallbacksField.Name,
            property.SetterCallbacksField.TypeSyntax.New()
        );

    private static FieldDeclarationSyntax BuildGetterInvocationCountField(in ImposterTargetPropertyMetadata property) =>
        SingleVariableField(
            property.GetterInvocationCountField.TypeSyntax,
            property.GetterInvocationCountField.Name,
            SyntaxKind.PrivateKeyword);

    private static FieldDeclarationSyntax BuildLastGetterReturnValueField(in ImposterTargetPropertyMetadata property) =>
        SingleVariableField(
            property.LastGetterReturnValueField.TypeSyntax,
            property.LastGetterReturnValueField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword)),
            EmptyParametersGoesTo(Default)
        );

    private static FieldDeclarationSyntax BuildGetterReturnValuesField(in ImposterTargetPropertyMetadata property) =>
        SinglePrivateReadonlyVariableField(
            property.GetterReturnValuesField.TypeSyntax,
            property.GetterReturnValuesField.Name,
            property.GetterReturnValuesField.TypeSyntax.New());

    private static FieldDeclarationSyntax BuildGetterCallbacksField(in ImposterTargetPropertyMetadata property) =>
        SinglePrivateReadonlyVariableField(
            property.GetterCallbacksField.TypeSyntax,
            property.GetterCallbacksField.Name,
            property.GetterCallbacksField.TypeSyntax.New());
}