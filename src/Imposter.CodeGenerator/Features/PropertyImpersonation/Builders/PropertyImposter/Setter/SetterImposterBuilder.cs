using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.VerificationFailedBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.InterlockedSyntaxHelper;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;

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
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    property.SetterImposter.CallbacksField.Type,
                    CallbacksFieldMetadata.Name,
                    property.SetterImposter.CallbacksField.Type.New()
                )
            )
            .AddMember(BuildInvocationsField(property.SetterImposter))
            .AddMember(
                property.Core.KeepsValue
                    ? SinglePrivateReadonlyVariableField(
                        property.SetterImposter.DefaultPropertyBehaviourField
                    )
                    : null
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(property.SetterImposter.InvocationBehaviorField)
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(property.SetterImposter.PropertyDisplayNameField)
            )
            .AddMember(
                SingleVariableField(
                    property.SetterImposter.HasConfiguredSetterField,
                    SyntaxKind.PrivateKeyword
                )
            )
            .AddMember(
                property.Core.SetterSupportsBaseImplementation
                    ? SingleVariableField(
                        property.SetterImposter.UseBaseImplementationField,
                        SyntaxKind.PrivateKeyword
                    )
                    : null
            )
            .AddMember(BuildConstructor(property))
            .AddMember(BuildSetterCallbackMethod(property.SetterImposter))
            .AddMember(BuildSetterCalledMethod(property.SetterImposter))
            .AddMember(
                property.Core.SetterSupportsBaseImplementation
                    ? BuildUseBaseImplementationMethod(property.SetterImposter)
                    : null
            )
            .AddMember(BuildSetMethod(property))
            .AddMember(BuildEnsureSetterConfiguredMethod(property.SetterImposter))
            .AddMember(BuildMarkConfiguredMethod(property.SetterImposter))
            .AddMember(
                property.SetterImposter.InvocationHistoryField is null
                    ? null
                    : FormatValueMethodBuilder.Build()
            )
            .AddMember(SetterImposterBuilderBuilder.Build(property))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property
    )
    {
        var setterImposter = property.SetterImposter;
        var constructor = new ConstructorWithFieldInitializationBuilder(
            setterImposter.Name
        ).WithModifiers(Token(SyntaxKind.InternalKeyword));

        if (property.Core.KeepsValue)
        {
            constructor.AddParameter(setterImposter.DefaultPropertyBehaviourField);
        }

        return constructor
            .AddParameter(
                setterImposter.InvocationBehaviorParameter,
                setterImposter.InvocationBehaviorField.Name
            )
            .AddParameter(
                setterImposter.PropertyDisplayNameParameter,
                setterImposter.PropertyDisplayNameField.Name
            )
            .Build();
    }

    private static FieldDeclarationSyntax BuildInvocationsField(
        in PropertySetterImposterMetadata setterImposter
    ) =>
        setterImposter.InvocationHistoryField is { } invocationHistory
            ? SinglePrivateReadonlyVariableField(invocationHistory, invocationHistory.Type.New())
            : SingleVariableField(setterImposter.InvocationCountField, SyntaxKind.PrivateKeyword);

    private static MethodDeclarationSyntax BuildUseBaseImplementationMethod(
        in PropertySetterImposterMetadata setterImposter
    ) =>
        new MethodDeclarationBuilder(
            setterImposter.UseBaseImplementationMethod.ReturnType,
            setterImposter.UseBaseImplementationMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    IdentifierName(setterImposter.HasConfiguredSetterField.Name)
                        .Assign(True)
                        .ToStatementSyntax(),
                    IdentifierName(setterImposter.UseBaseImplementationField.Name)
                        .Assign(True)
                        .ToStatementSyntax()
                )
            )
            .Build();

    internal static MethodDeclarationSyntax BuildSetMethod(in ImposterPropertyMetadata property)
    {
        var setterImposter = property.SetterImposter;
        var baseImplementationIdentifier = IdentifierName(
            setterImposter.SetMethod.BaseImplementationParameter.Name
        );
        var bodyStatements = new List<StatementSyntax>
        {
            IdentifierName(setterImposter.EnsureConfiguredMethod.Name).Call().ToStatementSyntax(),
            TrackSetterInvocation(setterImposter),
            InvokeCallbacks(setterImposter),
        };

        bodyStatements.AddRange(
            SetBackingField(
                setterImposter,
                property.DefaultPropertyBehaviour,
                baseImplementationIdentifier,
                property.Core.SetterSupportsBaseImplementation,
                property.Core.KeepsValue
            )
        );

        if (setterImposter.SetMethod.RequiresDirectBaseAssignment)
        {
            bodyStatements.Add(ReturnStatement(False));
        }

        var baseImplementationParameter = setterImposter.SetMethod.BaseImplementationParameter;

        return new MethodDeclarationBuilder(
            setterImposter.SetMethod.ReturnType,
            setterImposter.SetMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(setterImposter.SetMethod.ValueParameter))
            .AddParameterIf(
                !setterImposter.SetMethod.RequiresDirectBaseAssignment,
                () => ParameterSyntax(baseImplementationParameter)
            )
            .WithBody(Block(bodyStatements.ToArray()))
            .Build();

        static IEnumerable<StatementSyntax> SetBackingField(
            in PropertySetterImposterMetadata setterImposter,
            in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour,
            ExpressionSyntax baseImplementationIdentifier,
            bool setterSupportsBaseImplementation,
            bool keepsValue
        )
        {
            var defaultBehaviourCheck = IdentifierName(
                    setterImposter.DefaultPropertyBehaviourField.Name
                )
                .Dot(IdentifierName(defaultPropertyBehaviour.IsOnField.Name));

            StatementSyntax[] assignBackingFieldStatements =
            [
                IdentifierName(setterImposter.DefaultPropertyBehaviourField.Name)
                    .Dot(IdentifierName(defaultPropertyBehaviour.BackingField.Name))
                    .Assign(IdentifierName(setterImposter.SetMethod.ValueParameter.Name))
                    .ToStatementSyntax(),
                IdentifierName(setterImposter.DefaultPropertyBehaviourField.Name)
                    .Dot(IdentifierName(defaultPropertyBehaviour.HasValueSetField.Name))
                    .Assign(True)
                    .ToStatementSyntax(),
            ];

            var defaultBehaviourPath = IfStatement(
                defaultBehaviourCheck,
                Block(assignBackingFieldStatements)
            );

            var statements = new List<StatementSyntax>();

            if (setterImposter.SetMethod.RequiresDirectBaseAssignment)
            {
                // An init-only base assignment must execute in the generated init accessor,
                // after configuration checks, invocation tracking and callbacks have completed.
                statements.Add(
                    IfStatement(
                        IdentifierName(setterImposter.UseBaseImplementationField.Name),
                        Block(ReturnStatement(True))
                    )
                );
            }
            else if (setterSupportsBaseImplementation)
            {
                var baseImplementationCall = baseImplementationIdentifier.Call(
                    Argument(IdentifierName(setterImposter.SetMethod.ValueParameter.Name))
                );
                var baseImplementationPath = CallBaseImplementationIfUsed(
                    IdentifierName(setterImposter.UseBaseImplementationField.Name),
                    baseImplementationIdentifier,
                    Block(baseImplementationCall.ToStatementSyntax(), ReturnStatement()),
                    ThrowMissingImposter(setterImposter.PropertyDisplayNameField.Name, " (setter)")
                );

                statements.Add(baseImplementationPath);
            }

            if (keepsValue)
            {
                statements.Add(defaultBehaviourPath);
            }

            return statements;
        }

        // A callback with criteria runs only for a value they match.
        static StatementSyntax InvokeCallbacks(in PropertySetterImposterMetadata setterImposter)
        {
            var value = IdentifierName(setterImposter.SetMethod.ValueParameter.Name);
            var invokeCallback = IdentifierName("setterCallback")
                .Call(Argument(value))
                .ToStatementSyntax();

            if (setterImposter.CallbacksField.TupleTypeSyntax is null)
            {
                return ForEachStatement(
                    type: Var,
                    identifier: Identifier("setterCallback"),
                    expression: IdentifierName(CallbacksFieldMetadata.Name),
                    statement: Block(invokeCallback)
                );
            }

            return ForEachVariableStatement(
                variable: DeclarationExpression(
                    Var,
                    ParenthesizedVariableDesignation(
                        Token(SyntaxKind.OpenParenToken),
                        SeparatedList<VariableDesignationSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                SingleVariableDesignation(Identifier("criteria")),
                                Token(SyntaxKind.CommaToken),
                                SingleVariableDesignation(Identifier("setterCallback")),
                            }
                        ),
                        Token(SyntaxKind.CloseParenToken)
                    )
                ),
                IdentifierName(CallbacksFieldMetadata.Name),
                Block(
                    IfStatement(
                        IdentifierName("criteria")
                            .Dot(IdentifierName("Matches"))
                            .Call(Argument(value)),
                        invokeCallback
                    )
                )
            );
        }

        // A value passed through can't be kept, so the setter only counts it.
        static StatementSyntax TrackSetterInvocation(
            in PropertySetterImposterMetadata setterImposter
        ) =>
            (
                setterImposter.InvocationHistoryField is { } invocationHistory
                    ? IdentifierName(invocationHistory.Name)
                        .Dot(ConcurrentStackSyntaxHelper.Push)
                        .Call(
                            Argument(IdentifierName(setterImposter.SetMethod.ValueParameter.Name))
                        )
                    : InterlockedIncrement(setterImposter.InvocationCountField.Name)
            ).ToStatementSyntax();
    }

    internal static MethodDeclarationSyntax BuildSetterCalledMethod(
        in PropertySetterImposterMetadata setterImposter
    )
    {
        var called = setterImposter.CalledMethod;
        var count = IdentifierName(called.CountParameter.Name);
        var method = new MethodDeclarationBuilder(called.ReturnType, called.Name);

        if (
            called.CriteriaParameter is not { } criteria
            || setterImposter.InvocationHistoryField is not { } invocationHistoryField
        )
        {
            // A value passed through isn't kept, so Called counts every set.
            var setCount = IdentifierName(setterImposter.InvocationCountField.Name);

            return method
                .AddParameter(ParameterSyntax(called.CountParameter))
                .WithBody(
                    Block(
                        IfStatement(
                            CountDoesNotMatch(count, setCount),
                            ThrowVerificationFailed(count, setCount)
                        )
                    )
                )
                .Build();
        }

        var invocationHistory = IdentifierName(invocationHistoryField.Name);
        var invocationCount = IdentifierName(called.InvocationCountVariableName);
        var value = IdentifierName("value");
        var valueDescription = "set "
            .StringLiteral()
            .Add(IdentifierName(setterImposter.PropertyDisplayNameField.Name))
            .Add(" = ".StringLiteral())
            .Add(Invocation(value));

        return method
            .AddParameter(ParameterSyntax(criteria))
            .AddParameter(ParameterSyntax(called.CountParameter))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        invocationCount.Identifier.Text,
                        invocationHistory
                            .Dot(IdentifierName("Count"))
                            .Call(
                                Argument(
                                    IdentifierName(criteria.Name).Dot(IdentifierName("Matches"))
                                )
                            )
                    ),
                    ThrowIfCountDoesNotMatch(
                        count,
                        invocationCount,
                        new PerformedInvocations(invocationHistory, value, valueDescription)
                    )
                )
            )
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildSetterCallbackMethod(
        in PropertySetterImposterMetadata setterImposter
    )
    {
        var callbackMethod = setterImposter.CallbackMethod;
        var callback = IdentifierName(callbackMethod.CallbackParameter.Name);
        // A callback is queued with its criteria, unless the value is passed through and can't be matched.
        ExpressionSyntax queuedCallback =
            callbackMethod.CriteriaParameter is { } criteria
            && setterImposter.CallbacksField.TupleTypeSyntax is { } tupleType
                ? tupleType.New(
                    ArgumentListSyntax([
                        Argument(IdentifierName(criteria.Name)),
                        Argument(callback),
                    ])
                )
                : callback;

        return new MethodDeclarationBuilder(callbackMethod.ReturnType, callbackMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                callbackMethod.CriteriaParameter is null
                    ? null
                    : ParameterSyntax(callbackMethod.CriteriaParameter.Value)
            )
            .AddParameter(ParameterSyntax(callbackMethod.CallbackParameter))
            .WithBody(
                Block(
                    IdentifierName(CallbacksFieldMetadata.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(queuedCallback))
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildEnsureSetterConfiguredMethod(
        in PropertySetterImposterMetadata setterImposter
    ) =>
        EnsureConfiguredMethod(
            setterImposter.EnsureConfiguredMethod.Name,
            setterImposter.InvocationBehaviorField.Name,
            IdentifierName(setterImposter.HasConfiguredSetterField.Name),
            ThrowMissingImposter(setterImposter.PropertyDisplayNameField.Name, " (setter)")
        );

    private static MethodDeclarationSyntax BuildMarkConfiguredMethod(
        in PropertySetterImposterMetadata setterImposter
    ) =>
        new MethodDeclarationBuilder(
            setterImposter.MarkConfiguredMethod.ReturnType,
            setterImposter.MarkConfiguredMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    IdentifierName(setterImposter.HasConfiguredSetterField.Name)
                        .Assign(True)
                        .ToStatementSyntax()
                )
            )
            .Build();
}
