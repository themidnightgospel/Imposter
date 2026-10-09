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
        var blockBuilder = new BlockBuilder();
        var fields = @event.Builder.Fields;
        blockBuilder.AddExpression(
            FieldIdentifier(fields.History)
                .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
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
            .AddParameters(@event.Core.RaiseParameterSyntaxes)
            .WithBody(BuildRaiseCoreAsyncBody(@event, taskListType))
            .Build();
    }

    private static BlockSyntax BuildRaiseCoreAsyncBody(
        in ImposterEventMetadata @event,
        TypeSyntax taskListType
    )
    {
        var fields = @event.Builder.Fields;
        var usesValueTask = @event.Core.ReturnsNonGenericValueTask;
        var pendingTasks = IdentifierName(@event.Builder.Methods.RaiseLocalNames.PendingTasks);

        return new BlockBuilder()
            .AddExpression(
                FieldIdentifier(fields.History)
                    .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                    .Call(Argument(BuildHistoryEntryExpression(@event)))
            )
            .AddStatement(
                LocalVariableDeclarationSyntax(
                    taskListType,
                    pendingTasks.Identifier.Text,
                    taskListType.New()
                )
            )
            .AddStatement(ForEachAsyncInvocation(fields.Callbacks, @event, usesValueTask))
            .AddStatement(AwaitPendingTasksStatement(pendingTasks))
            .AddStatement(pendingTasks.Dot(IdentifierName("Clear")).Call().ToStatementSyntax())
            .AddStatement(ForEachAsyncHandlerInvocation(@event, usesValueTask))
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
    )
    {
        var callback = IdentifierName(@event.Builder.Methods.RaiseLocalNames.Callback);

        return ForEachStatement(
            Var,
            callback.Identifier,
            FieldIdentifier(field),
            Block(
                callback
                    .Call(@event.Core.Parameters.Select(parameter => parameter.ForwardingArgument))
                    .ToStatementSyntax()
            )
        );
    }

    private static ForEachStatementSyntax ForEachHandlerInvocation(in ImposterEventMetadata @event)
    {
        var handler = IdentifierName(@event.Builder.Methods.RaiseLocalNames.Handler);

        return ForEachStatement(
            Var,
            handler.Identifier,
            IdentifierName(@event.Builder.Methods.EnumerateHandlers.Name).Call(),
            Block(
                FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
                    .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                    .Call(Argument(BuildHandlerInvocationTuple(handler, @event)))
                    .ToStatementSyntax(),
                handler
                    .Call(@event.Core.Parameters.Select(parameter => parameter.ForwardingArgument))
                    .ToStatementSyntax()
            )
        );
    }

    private static ForEachStatementSyntax ForEachAsyncInvocation(
        in FieldMetadata field,
        in ImposterEventMetadata @event,
        bool usesValueTask
    )
    {
        var callback = IdentifierName(@event.Builder.Methods.RaiseLocalNames.Callback);

        return ForEachStatement(
            Var,
            callback.Identifier,
            FieldIdentifier(field),
            Block(InvokeAndCollectTaskStatements(callback, @event, usesValueTask))
        );
    }

    private static ForEachStatementSyntax ForEachAsyncHandlerInvocation(
        in ImposterEventMetadata @event,
        bool usesValueTask
    )
    {
        var handler = IdentifierName(@event.Builder.Methods.RaiseLocalNames.Handler);
        StatementSyntax[] body =
        [
            FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
                .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                .Call(Argument(BuildHandlerInvocationTuple(handler, @event)))
                .ToStatementSyntax(),
            .. InvokeAndCollectTaskStatements(handler, @event, usesValueTask),
        ];

        return ForEachStatement(
            Var,
            handler.Identifier,
            IdentifierName(@event.Builder.Methods.EnumerateHandlers.Name).Call(),
            Block(body)
        );
    }

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
