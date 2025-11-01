using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

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
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.CallbacksField.Type, PropertySetterImposterMetadata.CallbacksFieldMetadata.Name, property.SetterImposter.CallbacksField.Type.New()))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.InvocationHistoryField, property.SetterImposter.InvocationHistoryField.Type.New()))
            .AddMember(SinglePrivateReadonlyVariableField(property.SetterImposter.DefaultPropertyBehaviourField))
            .AddMember(BuildConstructor(property))
            .AddMember(BuildSetterCallbackMethod(property.SetterImposter))
            .AddMember(BuildSetterCalledMethod(property.SetterImposter))
            .AddMember(BuildSetMethod(property.SetterImposter, property.DefaultPropertyBehaviour))
            .AddMember(SetterImposterBuilderBuilder.Build(property))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterPropertyMetadata property) =>
        new ConstructorWithFieldInitializationBuilder(property.SetterImposter.Name)
            .WithModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameter(property.SetterImposter.DefaultPropertyBehaviourField)
            .Build();


    internal static MethodDeclarationSyntax BuildSetMethod(in PropertySetterImposterMetadata setterImposter, in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour)
    {
        return new MethodDeclarationBuilder(setterImposter.SetMethod.ReturnType, setterImposter.SetMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(setterImposter.SetMethod.ValueParameter))
            .WithBody(Block(
                    TrackSetterInvocation(setterImposter),
                    InvokeCallbacks(setterImposter),
                    SetBackingField(setterImposter, defaultPropertyBehaviour)
                )
            )
            .Build();

        static StatementSyntax SetBackingField(
            in PropertySetterImposterMetadata setterImposter,
            in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour)
            => IfStatement(
                IdentifierName(setterImposter.DefaultPropertyBehaviourField.Name)
                    .Dot(IdentifierName(defaultPropertyBehaviour.IsOnField.Name))
                ,
                IdentifierName(setterImposter.DefaultPropertyBehaviourField.Name)
                    .Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name))
                    .Assign(IdentifierName(setterImposter.SetMethod.ValueParameter.Name))
                    .ToStatementSyntax()
            );

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
                IdentifierName(PropertySetterImposterMetadata.CallbacksFieldMetadata.Name),
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
                    IdentifierName(PropertySetterImposterMetadata.CallbacksFieldMetadata.Name)
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
}