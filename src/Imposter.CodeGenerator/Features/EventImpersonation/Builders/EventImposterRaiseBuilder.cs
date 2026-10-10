using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.EventImpersonation.Builders.EventImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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
            SingleVariableField(fields.HandlerInvocations),
        ];
    }

    internal static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event)
    {
        var methodBuilder = new MethodDeclarationBuilder(
            @event.BuilderInterface.RaiseMethod.ReturnType,
            @event.BuilderInterface.RaiseMethod.Name
        )
            .WithExplicitInterfaceSpecifier(@event.BuilderInterface.SetupInterfaceTypeSyntax)
            .AddParameters(@event.Core.RaiseParameterSyntaxes);

        if (@event.Core.IsAsync)
        {
            return methodBuilder
                .AddModifier(Token(SyntaxKind.AsyncKeyword))
                .WithBody(
                    new BlockBuilder()
                        .AddStatement(BuildAwaitRaiseAsyncStatement(@event))
                        .AddStatement(ReturnThis)
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
                                    parameter.ForwardingArgument
                                )
                            )
                    )
                    .AddStatement(ReturnThis)
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
            .Call(Argument(False))
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
        var localNames = @event.Builder.Methods.RaiseLocalNames;
        var callback = IdentifierName(localNames.Callback);
        var handler = IdentifierName(localNames.Handler);

        return new BlockBuilder()
            .AddStatements(AssignOutParametersDefault(@event))
            .AddExpression(EnqueueHistoryEntry(@event))
            .AddStatement(ForEachCallback(@event, InvokeStatement(callback, @event)))
            .AddStatement(
                ForEachActiveHandler(
                    @event,
                    EnqueueHandlerInvocation(@event, handler),
                    InvokeStatement(handler, @event)
                )
            )
            .Build();
    }

    // An out argument starts as the default: the history records it before the callbacks and handlers assign it, and
    // it stays the default when none does.
    private static IEnumerable<StatementSyntax> AssignOutParametersDefault(
        in ImposterEventMetadata @event
    ) =>
        @event
            .Core.Parameters.Where(parameter => parameter.IsOut)
            .Select(parameter =>
                IdentifierName(parameter.Name).Assign(DefaultNonNullable).ToStatementSyntax()
            );

    internal static MethodDeclarationSyntax BuildRaiseCoreAsyncMethod(
        in ImposterEventMetadata @event
    )
    {
        var taskType = WellKnownTypes.System.Threading.Tasks.Task;
        var taskListType = WellKnownTypes.System.Collections.Generic.List(taskType);

        return new MethodDeclarationBuilder(
            @event.Builder.Methods.RaiseCoreAsync.ReturnType,
            @event.Builder.Methods.RaiseCoreAsync.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.AsyncKeyword))
            .AddParameters(@event.Core.RaiseParameterSyntaxes)
            .WithBody(BuildRaiseCoreAsyncBody(@event, taskListType))
            .Build();
    }

    private static BlockSyntax BuildRaiseCoreAsyncBody(
        in ImposterEventMetadata @event,
        TypeSyntax taskListType
    )
    {
        var usesValueTask = @event.Core.ReturnsNonGenericValueTask;
        var localNames = @event.Builder.Methods.RaiseLocalNames;
        var pendingTasks = IdentifierName(localNames.PendingTasks);
        var callback = IdentifierName(localNames.Callback);
        var handler = IdentifierName(localNames.Handler);

        return new BlockBuilder()
            .AddExpression(EnqueueHistoryEntry(@event))
            .AddStatement(
                LocalVariableDeclarationSyntax(
                    taskListType,
                    pendingTasks.Identifier.Text,
                    taskListType.New()
                )
            )
            .AddStatement(
                ForEachCallback(
                    @event,
                    InvokeAndCollectTaskStatements(callback, @event, usesValueTask)
                )
            )
            .AddStatement(AwaitPendingTasksStatement(pendingTasks))
            .AddStatement(pendingTasks.Dot(IdentifierName("Clear")).Call().ToStatementSyntax())
            .AddStatement(
                ForEachActiveHandler(
                    @event,
                    [
                        EnqueueHandlerInvocation(@event, handler),
                        .. InvokeAndCollectTaskStatements(handler, @event, usesValueTask),
                    ]
                )
            )
            .AddStatement(AwaitPendingTasksStatement(pendingTasks))
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
        var enumerableType = WellKnownTypes.System.Collections.Generic.IEnumerable(
            @event.Core.HandlerTypeSyntax
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
        var handlers = IdentifierName("handlers");

        return Block(
            LocalVariableDeclarationSyntax(
                Var,
                handlers.Identifier.Text,
                FieldIdentifier(@event.Builder.Fields.ActiveHandlers)
            ),
            IfStatement(
                handlers.IsNotNull(),
                Block(
                    ForEachStatement(
                        Var,
                        Identifier("handler"),
                        handlers.Dot(IdentifierName("GetInvocationList")).Call(),
                        Block(
                            YieldStatement(
                                SyntaxKind.YieldReturnStatement,
                                CastExpression(
                                    @event.Core.HandlerTypeSyntax,
                                    IdentifierName("handler")
                                )
                            )
                        )
                    )
                )
            )
        );
    }

    private static InvocationExpressionSyntax EnqueueHistoryEntry(
        in ImposterEventMetadata @event
    ) =>
        FieldIdentifier(@event.Builder.Fields.History)
            .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
            .Call(Argument(@event.Builder.Fields.HistoryEntry.Entry));

    private static ExpressionStatementSyntax EnqueueHandlerInvocation(
        in ImposterEventMetadata @event,
        IdentifierNameSyntax handler
    ) =>
        FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
            .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
            .Call(Argument(@event.Builder.Fields.HandlerInvocationEntry.Entry(handler)))
            .ToStatementSyntax();

    private static ExpressionStatementSyntax InvokeStatement(
        IdentifierNameSyntax invoked,
        in ImposterEventMetadata @event
    ) =>
        invoked
            .Call(@event.Core.Parameters.Select(parameter => parameter.ForwardingArgument))
            .ToStatementSyntax();

    // Runs the body for each callback, which it reads as RaiseLocalNames.Callback.
    private static ForEachStatementSyntax ForEachCallback(
        in ImposterEventMetadata @event,
        params StatementSyntax[] body
    ) =>
        ForEachStatement(
            Var,
            Identifier(@event.Builder.Methods.RaiseLocalNames.Callback),
            FieldIdentifier(@event.Builder.Fields.Callbacks),
            Block(body)
        );

    // Runs the body for each subscribed handler, which it reads as RaiseLocalNames.Handler.
    private static ForEachStatementSyntax ForEachActiveHandler(
        in ImposterEventMetadata @event,
        params StatementSyntax[] body
    ) =>
        ForEachStatement(
            Var,
            Identifier(@event.Builder.Methods.RaiseLocalNames.Handler),
            IdentifierName(@event.Builder.Methods.EnumerateHandlers.Name).Call(),
            Block(body)
        );

    private static StatementSyntax[] InvokeAndCollectTaskStatements(
        IdentifierNameSyntax invoked,
        in ImposterEventMetadata @event,
        bool usesValueTask
    )
    {
        var localNames = @event.Builder.Methods.RaiseLocalNames;
        var task = IdentifierName(localNames.Task);

        return
        [
            LocalVariableDeclarationSyntax(
                Var,
                task.Identifier.Text,
                invoked.Call(
                    @event.Core.Parameters.Select(parameter => parameter.ForwardingArgument)
                )
            ),
            IfStatement(
                task.IsNotDefault(),
                Block(
                    IdentifierName(localNames.PendingTasks)
                        .Dot(IdentifierName("Add"))
                        .Call(Argument(ToTaskExpression(task, usesValueTask)))
                        .ToStatementSyntax()
                )
            ),
        ];
    }

    private static IfStatementSyntax AwaitPendingTasksStatement(
        IdentifierNameSyntax pendingTasks
    ) =>
        IfStatement(
            BinaryExpression(
                SyntaxKind.GreaterThanExpression,
                pendingTasks.Dot(IdentifierName("Count")),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
            ),
            Block(
                WellKnownTypes
                    .System.Threading.Tasks.Task.Dot(IdentifierName("WhenAll"))
                    .Call(Argument(pendingTasks))
                    .Dot(IdentifierName("ConfigureAwait"))
                    .Call(Argument(False))
                    .Await()
                    .ToStatementSyntax()
            )
        );
}
