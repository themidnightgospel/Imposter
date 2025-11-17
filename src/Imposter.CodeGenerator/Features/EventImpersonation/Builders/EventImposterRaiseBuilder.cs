using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.Features.EventImpersonation.Builders.EventImposterBuilderCommon;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterRaiseBuilder
{
    internal static MemberDeclarationSyntax[] BuildFields(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;

        return
        [
            SingleVariableField(fields.Callbacks),
            SingleVariableField(fields.History),
            SingleVariableField(fields.HandlerInvocations)
        ];
    }

    internal static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event)
    {
        var methodBuilder = ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.RaiseMethod.ReturnType,
                @event.BuilderInterface.RaiseMethod.Name
            )
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax));

        if (@event.Core.IsAsync)
        {
            return methodBuilder
                .AddModifier(Token(SyntaxKind.AsyncKeyword))
                .WithBody(
                    new BlockBuilder()
                        .AddStatement(BuildAwaitRaiseAsyncStatement(@event))
                        .AddStatement(ReturnStatement(ThisExpression()))
                        .Build()
                )
                .Build();
        }

        return methodBuilder
            .WithBody(
                new BlockBuilder()
                    .AddExpression(
                        IdentifierName(@event.Builder.Methods.RaiseInternal.Name)
                            .Call(
                                @event.Core.Parameters.Select(parameter =>
                                    Argument(IdentifierName(parameter.Name))
                                )
                            )
                    )
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build()
            )
            .Build();
    }

    private static ExpressionStatementSyntax BuildAwaitRaiseAsyncStatement(
        in ImposterEventMetadata @event
    ) =>
        IdentifierName(@event.Builder.Methods.RaiseCoreAsync.Name)
            .Call(
                @event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))
            )
            .Dot(IdentifierName("ConfigureAwait"))
            .Call(Argument(LiteralExpression(SyntaxKind.FalseLiteralExpression)))
            .Await()
            .ToStatementSyntax();

    internal static MethodDeclarationSyntax BuildRaiseInternalMethod(
        in ImposterEventMetadata @event
    ) =>
        new MethodDeclarationBuilder(
                @event.Builder.Methods.RaiseInternal.ReturnType,
                @event.Builder.Methods.RaiseInternal.Name
            )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax))
            .WithBody(BuildRaiseInternalBody(@event))
            .Build();

    private static BlockSyntax BuildRaiseInternalBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var fields = @event.Builder.Fields;
        blockBuilder.AddExpression(
            FieldIdentifier(fields.History)
                .Dot(IdentifierName("Enqueue"))
                .Call(Argument(BuildHistoryEntryExpression(@event)))
        );

        blockBuilder.AddStatement(ForEachInvocation(fields.Callbacks, @event));
        blockBuilder.AddStatement(ForEachHandlerInvocation(@event));

        return blockBuilder.Build();
    }

    internal static MethodDeclarationSyntax BuildRaiseCoreAsyncMethod(
        in ImposterEventMetadata @event
    )
    {
        var taskType = WellKnownTypes.System.Threading.Tasks.Task;
        var taskListType = QualifiedName(
            WellKnownTypes.System.Collections.Generic.Namespace,
            GenericName(Identifier("List"), TypeArgumentList(SingletonSeparatedList(taskType)))
        );

        return new MethodDeclarationBuilder(
                @event.Builder.Methods.RaiseCoreAsync.ReturnType,
                @event.Builder.Methods.RaiseCoreAsync.Name
            )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.AsyncKeyword))
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax))
            .WithBody(BuildRaiseCoreAsyncBody(@event, taskListType, taskType))
            .Build();
    }

    private static BlockSyntax BuildRaiseCoreAsyncBody(
        in ImposterEventMetadata @event,
        TypeSyntax taskListType,
        TypeSyntax taskType
    )
    {
        var fields = @event.Builder.Fields;
        var usesValueTask = @event.Core.DelegateReturnTypeSymbol.IsWellKnownType(
            WellKnownTypes.System.Threading.Tasks.ValueTask,
            WellKnownAssemblyNames.SystemAssemblies
        );

        return new BlockBuilder()
            .AddExpression(
                FieldIdentifier(fields.History)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(BuildHistoryEntryExpression(@event)))
            )
            .AddStatement(
                LocalVariableDeclarationSyntax(taskListType, "pendingTasks", taskListType.New())
            )
            .AddStatement(ForEachAsyncHandlerInvocation(@event, taskType, usesValueTask))
            .AddStatement(AwaitPendingTasksStatement())
            .AddStatement(
                IdentifierName("pendingTasks")
                    .Dot(IdentifierName("Clear"))
                    .Call()
                    .ToStatementSyntax()
            )
            .AddStatement(ForEachAsyncInvocation(fields.Callbacks, @event, taskType, usesValueTask))
            .AddStatement(AwaitPendingTasksStatement())
            .Build();
    }

    private static ExpressionSyntax ToTaskExpression(
        ExpressionSyntax taskExpression,
        bool usesValueTask
    ) => usesValueTask ? taskExpression.Dot(IdentifierName("AsTask")).Call() : taskExpression;

    internal static MethodDeclarationSyntax BuildEnumerateHandlersMethod(
        in ImposterEventMetadata @event
    )
    {
        var enumerableType = QualifiedName(
            WellKnownTypes.System.Collections.Generic.Namespace,
            GenericName(
                Identifier("IEnumerable"),
                TypeArgumentList(SingletonSeparatedList(@event.Core.HandlerTypeSyntax))
            )
        );

        return new MethodDeclarationBuilder(
                enumerableType,
                @event.Builder.Methods.EnumerateHandlers.Name
            )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(BuildEnumerateHandlersBody(@event))
            .Build();
    }

    private static BlockSyntax BuildEnumerateHandlersBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var dictionaryType = WellKnownTypes.System.Collections.Generic.Dictionary(
            @event.Core.HandlerTypeSyntax,
            WellKnownTypes.Int
        );
        var handlerCounts = FieldIdentifier(@event.Builder.Fields.HandlerCounts);
        var handlerOrder = FieldIdentifier(@event.Builder.Fields.HandlerOrder);

        blockBuilder.AddStatement(
            LocalVariableDeclarationSyntax(
                dictionaryType,
                "budgets",
                dictionaryType.New(ArgumentList(SingletonSeparatedList(Argument(handlerCounts))))
            )
        );

        blockBuilder.AddStatement(
            ForEachStatement(
                Var,
                Identifier("handler"),
                handlerOrder,
                Block(
                    LocalVariableDeclarationSyntax(WellKnownTypes.Int, "remaining"),
                    IfStatement(
                        IdentifierName("budgets")
                            .Dot(IdentifierName("TryGetValue"))
                            .Call(
                                ArgumentList(
                                    SeparatedList([
                                        Argument(IdentifierName("handler")),
                                        Argument(
                                            null,
                                            Token(SyntaxKind.OutKeyword),
                                            IdentifierName("remaining")
                                        ),
                                    ])
                                )
                            ),
                        Block(
                            IfStatement(
                                BinaryExpression(
                                    SyntaxKind.GreaterThanExpression,
                                    IdentifierName("remaining"),
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(0)
                                    )
                                ),
                                Block(
                                    ElementAccessExpression(IdentifierName("budgets"))
                                        .WithArgumentList(
                                            BracketedArgumentList(
                                                SingletonSeparatedList(
                                                    Argument(IdentifierName("handler"))
                                                )
                                            )
                                        )
                                        .Assign(
                                            BinaryExpression(
                                                SyntaxKind.SubtractExpression,
                                                IdentifierName("remaining"),
                                                LiteralExpression(
                                                    SyntaxKind.NumericLiteralExpression,
                                                    Literal(1)
                                                )
                                            )
                                        )
                                        .ToStatementSyntax(),
                                    YieldStatement(
                                        SyntaxKind.YieldReturnStatement,
                                        IdentifierName("handler")
                                    )
                                )
                            )
                        )
                    )
                )
            )
        );

        return blockBuilder.Build();
    }

    private static ExpressionSyntax BuildHistoryEntryExpression(in ImposterEventMetadata @event)
    {
        if (@event.Core.Parameters.Length == 0)
        {
            return True;
        }

        if (@event.Core.Parameters.Length == 1)
        {
            return IdentifierName(@event.Core.Parameters[0].Name);
        }

        return TupleExpression(
            SeparatedList(
                @event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))
            )
        );
    }

    private static ExpressionSyntax BuildHandlerInvocationTuple(
        ExpressionSyntax handlerExpression,
        in ImposterEventMetadata @event
    )
    {
        if (@event.Core.Parameters.Length == 0)
        {
            return handlerExpression;
        }

        var arguments = new List<ArgumentSyntax> { Argument(handlerExpression) };
        arguments.AddRange(
            @event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))
        );
        return TupleExpression(SeparatedList(arguments));
    }

    private static ForEachStatementSyntax ForEachInvocation(
        in FieldMetadata field,
        in ImposterEventMetadata @event
    ) =>
        ForEachStatement(
            Var,
            Identifier("callback"),
            FieldIdentifier(field),
            Block(
                IdentifierName("callback")
                    .Call(
                        @event.Core.Parameters.Select(parameter =>
                            Argument(IdentifierName(parameter.Name))
                        )
                    )
                    .ToStatementSyntax()
            )
        );

    private static ForEachStatementSyntax ForEachHandlerInvocation(
        in ImposterEventMetadata @event
    ) =>
        ForEachStatement(
            Var,
            Identifier("handler"),
            IdentifierName(@event.Builder.Methods.EnumerateHandlers.Name).Call(),
            Block(
                FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(BuildHandlerInvocationTuple(IdentifierName("handler"), @event)))
                    .ToStatementSyntax(),
                IdentifierName("handler")
                    .Call(
                        @event.Core.Parameters.Select(parameter =>
                            Argument(IdentifierName(parameter.Name))
                        )
                    )
                    .ToStatementSyntax()
            )
        );

    private static ForEachStatementSyntax ForEachAsyncInvocation(
        in FieldMetadata field,
        in ImposterEventMetadata @event,
        TypeSyntax taskType,
        bool usesValueTask
    )
    {
        return ForEachStatement(
            Var,
            Identifier("callback"),
            FieldIdentifier(field),
            Block(
                LocalVariableDeclarationSyntax(
                    Var,
                    "task",
                    IdentifierName("callback")
                        .Call(
                            @event.Core.Parameters.Select(parameter =>
                                Argument(IdentifierName(parameter.Name))
                            )
                        )
                ),
                IfStatement(
                    IdentifierName("task").IsNotDefault(),
                    Block(
                        IdentifierName("pendingTasks")
                            .Dot(IdentifierName("Add"))
                            .Call(Argument(ToTaskExpression(IdentifierName("task"), usesValueTask)))
                            .ToStatementSyntax()
                    )
                )
            )
        );
    }

    private static ForEachStatementSyntax ForEachAsyncHandlerInvocation(
        in ImposterEventMetadata @event,
        TypeSyntax taskType,
        bool usesValueTask
    )
    {
        return ForEachStatement(
            Var,
            Identifier("handler"),
            IdentifierName(@event.Builder.Methods.EnumerateHandlers.Name).Call(),
            Block(
                FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(BuildHandlerInvocationTuple(IdentifierName("handler"), @event)))
                    .ToStatementSyntax(),
                LocalVariableDeclarationSyntax(
                    Var,
                    "task",
                    IdentifierName("handler")
                        .Call(
                            @event.Core.Parameters.Select(parameter =>
                                Argument(IdentifierName(parameter.Name))
                            )
                        )
                ),
                IfStatement(
                    IdentifierName("task").IsNotDefault(),
                    Block(
                        IdentifierName("pendingTasks")
                            .Dot(IdentifierName("Add"))
                            .Call(Argument(ToTaskExpression(IdentifierName("task"), usesValueTask)))
                            .ToStatementSyntax()
                    )
                )
            )
        );
    }

    private static IfStatementSyntax AwaitPendingTasksStatement() =>
        IfStatement(
            BinaryExpression(
                SyntaxKind.GreaterThanExpression,
                IdentifierName("pendingTasks").Dot(IdentifierName("Count")),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
            ),
            Block(
                WellKnownTypes
                    .System.Threading.Tasks.Task.Dot(IdentifierName("WhenAll"))
                    .Call(Argument(IdentifierName("pendingTasks")))
                    .Dot(IdentifierName("ConfigureAwait"))
                    .Call(Argument(False))
                    .Await()
                    .ToStatementSyntax()
            )
        );
}
