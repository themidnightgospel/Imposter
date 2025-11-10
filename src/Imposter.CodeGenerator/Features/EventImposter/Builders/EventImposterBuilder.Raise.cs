using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Builders;

internal static partial class EventImposterBuilder
{
    private static MethodDeclarationSyntax BuildRaiseMethod(in ImposterEventMetadata @event)
    {
        var methodBuilder = ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.RaiseMethodReturnType,
                @event.BuilderInterface.RaiseMethodName)
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax));

        if (@event.Core.IsAsync)
        {
            return methodBuilder
                .AddModifier(Token(SyntaxKind.AsyncKeyword))
                .WithBody(
                    new BlockBuilder()
                        .AddStatement(BuildAwaitRaiseAsyncStatement(@event))
                        .AddStatement(ReturnStatement(ThisExpression()))
                        .Build())
                .Build();
        }

        return methodBuilder
            .WithBody(
                new BlockBuilder()
                    .AddExpression(
                        IdentifierName(@event.Builder.Methods.RaiseInternalName)
                            .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static ExpressionStatementSyntax BuildAwaitRaiseAsyncStatement(in ImposterEventMetadata @event) =>
        ExpressionStatement(
            AwaitExpression(
                IdentifierName(@event.Builder.Methods.RaiseCoreAsyncName)
                    .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))
                    .Dot(IdentifierName("ConfigureAwait"))
                    .Call(Argument(LiteralExpression(SyntaxKind.FalseLiteralExpression)))));

    private static MethodDeclarationSyntax BuildRaiseInternalMethod(in ImposterEventMetadata @event) =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), @event.Builder.Methods.RaiseInternalName)
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
                .Call(Argument(BuildHistoryEntryExpression(@event))));

        blockBuilder.AddStatement(ForEachInvocation(fields.Callbacks, @event));
        blockBuilder.AddStatement(ForEachHandlerInvocation(@event));

        return blockBuilder.Build();
    }

    private static MethodDeclarationSyntax BuildRaiseCoreAsyncMethod(in ImposterEventMetadata @event)
    {
        var taskType = WellKnownTypes.System.Threading.Tasks.Task;
        var taskListType = QualifiedName(
            WellKnownTypes.System.Collections.Generic.Namespace,
            GenericName(Identifier("List"),
                TypeArgumentList(SingletonSeparatedList(taskType))));

        return new MethodDeclarationBuilder(taskType, @event.Builder.Methods.RaiseCoreAsyncName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.AsyncKeyword))
            .AddParameters(@event.Core.Parameters.Select(parameter => parameter.ParameterSyntax))
            .WithBody(
                BuildRaiseCoreAsyncBody(@event, taskListType, taskType))
            .Build();
    }

    private static BlockSyntax BuildRaiseCoreAsyncBody(
        in ImposterEventMetadata @event,
        TypeSyntax taskListType,
        TypeSyntax taskType)
    {
        var blockBuilder = new BlockBuilder();
        var fields = @event.Builder.Fields;
        var usesValueTask = @event.Core.DelegateReturnTypeSymbol.IsWellKnownType(
            WellKnownTypes.System.Threading.Tasks.ValueTask,
            WellKnownAssemblyNames.SystemAssemblies);
        blockBuilder.AddExpression(
            FieldIdentifier(fields.History)
                .Dot(IdentifierName("Enqueue"))
                .Call(Argument(BuildHistoryEntryExpression(@event))));

        blockBuilder.AddStatement(
            LocalDeclarationStatement(
                VariableDeclaration(
                    taskListType,
                    SingletonSeparatedList(
                        VariableDeclarator(
                            Identifier("pendingTasks"),
                            null,
                            EqualsValueClause(taskListType.New()))))));

        blockBuilder.AddStatement(ForEachAsyncInvocation(fields.Callbacks, @event, taskType, usesValueTask));
        blockBuilder.AddStatement(ForEachAsyncHandlerInvocation(@event, taskType, usesValueTask));

        blockBuilder.AddStatement(
            IfStatement(
                BinaryExpression(
                    SyntaxKind.GreaterThanExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("pendingTasks"),
                        IdentifierName("Count")),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                Block(
                    ExpressionStatement(
                        AwaitExpression(
                            WellKnownTypes.System.Threading.Tasks.Task
                                .Dot(IdentifierName("WhenAll"))
                                .Call(Argument(IdentifierName("pendingTasks")))
                                .Dot(IdentifierName("ConfigureAwait"))
                                .Call(Argument(LiteralExpression(SyntaxKind.FalseLiteralExpression))))))));

        return blockBuilder.Build();
    }

    private static ExpressionSyntax ToTaskExpression(ExpressionSyntax taskExpression, bool usesValueTask) =>
        usesValueTask
            ? taskExpression.Dot(IdentifierName("AsTask")).Call()
            : taskExpression;

    private static MethodDeclarationSyntax BuildEnumerateHandlersMethod(in ImposterEventMetadata @event)
    {
        var enumerableType = QualifiedName(
            WellKnownTypes.System.Collections.Generic.Namespace,
            GenericName(
                Identifier("IEnumerable"),
                TypeArgumentList(SingletonSeparatedList(@event.Core.HandlerTypeSyntax))));

        return new MethodDeclarationBuilder(enumerableType, @event.Builder.Methods.EnumerateHandlersName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(BuildEnumerateHandlersBody(@event))
            .Build();
    }

    private static BlockSyntax BuildEnumerateHandlersBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var dictionaryType = WellKnownTypes.System.Collections.Generic.Dictionary(
            @event.Core.HandlerTypeSyntax,
            PredefinedType(Token(SyntaxKind.IntKeyword)));
        var handlerCounts = FieldIdentifier(@event.Builder.Fields.HandlerCounts);
        var handlerOrder = FieldIdentifier(@event.Builder.Fields.HandlerOrder);

        blockBuilder.AddStatement(
            LocalDeclarationStatement(
                VariableDeclaration(
                    dictionaryType,
                    SingletonSeparatedList(
                        VariableDeclarator(
                            Identifier("budgets"),
                            null,
                            EqualsValueClause(
                                dictionaryType.New(
                                    ArgumentList(SingletonSeparatedList(
                                        Argument(handlerCounts))))))))));

        blockBuilder.AddStatement(
            ForEachStatement(
                Var,
                Identifier("handler"),
                handlerOrder,
                Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            PredefinedType(Token(SyntaxKind.IntKeyword)),
                            SingletonSeparatedList(
                                VariableDeclarator(
                                    Identifier("remaining"))))),
                    IfStatement(
                        IdentifierName("budgets")
                            .Dot(IdentifierName("TryGetValue"))
                            .Call(
                                ArgumentList(SeparatedList([
                                    Argument(IdentifierName("handler")),
                                    Argument(null, Token(SyntaxKind.OutKeyword), IdentifierName("remaining"))
                                ]))),
                        Block(
                            IfStatement(
                                BinaryExpression(
                                    SyntaxKind.GreaterThanExpression,
                                    IdentifierName("remaining"),
                                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                                Block(
                                    ExpressionStatement(
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            ElementAccessExpression(IdentifierName("budgets"))
                                                .WithArgumentList(BracketedArgumentList(SingletonSeparatedList(Argument(IdentifierName("handler"))))),
                                            BinaryExpression(
                                                SyntaxKind.SubtractExpression,
                                                IdentifierName("remaining"),
                                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))))),
                                    YieldStatement(SyntaxKind.YieldReturnStatement, IdentifierName("handler")))))))));

        return blockBuilder.Build();
    }

    private static ExpressionSyntax BuildHistoryEntryExpression(in ImposterEventMetadata @event)
    {
        if (@event.Core.Parameters.Length == 0)
        {
            return LiteralExpression(SyntaxKind.TrueLiteralExpression);
        }

        return TupleExpression(
            SeparatedList(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))));
    }

    private static TupleExpressionSyntax BuildHandlerInvocationTuple(ExpressionSyntax handlerExpression, in ImposterEventMetadata @event)
    {
        var arguments = new List<ArgumentSyntax> { Argument(handlerExpression) };
        arguments.AddRange(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))));
        return TupleExpression(SeparatedList(arguments));
    }

    private static ForEachStatementSyntax ForEachInvocation(in FieldMetadata field, in ImposterEventMetadata @event) =>
        ForEachStatement(
            Var,
            Identifier("callback"),
            FieldIdentifier(field),
            Block(
                ExpressionStatement(
                    IdentifierName("callback")
                        .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))))));

    private static ForEachStatementSyntax ForEachHandlerInvocation(in ImposterEventMetadata @event) =>
        ForEachStatement(
            Var,
            Identifier("handler"),
            IdentifierName("EnumerateActiveHandlers").Call(),
            Block(
                ExpressionStatement(
                    FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(BuildHandlerInvocationTuple(IdentifierName("handler"), @event)))),
                ExpressionStatement(
                    IdentifierName("handler")
                        .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name)))))));

    private static ForEachStatementSyntax ForEachAsyncInvocation(in FieldMetadata field, in ImposterEventMetadata @event, TypeSyntax taskType, bool usesValueTask)
    {
        var asyncResultType = usesValueTask
            ? WellKnownTypes.System.Threading.Tasks.ValueTask
            : taskType;

        return
        ForEachStatement(
            Var,
            Identifier("callback"),
            FieldIdentifier(field),
            Block(
                LocalDeclarationStatement(
                    VariableDeclaration(
                        asyncResultType,
                        SingletonSeparatedList(
                            VariableDeclarator(
                                Identifier("task"),
                                null,
                                EqualsValueClause(
                                    IdentifierName("callback")
                                        .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))))))),
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.NotEqualsExpression,
                        IdentifierName("task"),
                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                    Block(
                        ExpressionStatement(
                            IdentifierName("pendingTasks")
                                .Dot(IdentifierName("Add"))
                                .Call(Argument(ToTaskExpression(IdentifierName("task"), usesValueTask))))))));
    }

    private static ForEachStatementSyntax ForEachAsyncHandlerInvocation(in ImposterEventMetadata @event, TypeSyntax taskType, bool usesValueTask)
    {
        var asyncResultType = usesValueTask
            ? WellKnownTypes.System.Threading.Tasks.ValueTask
            : taskType;

        return
        ForEachStatement(
            Var,
            Identifier("handler"),
            IdentifierName("EnumerateActiveHandlers").Call(),
            Block(
                ExpressionStatement(
                    FieldIdentifier(@event.Builder.Fields.HandlerInvocations)
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(BuildHandlerInvocationTuple(IdentifierName("handler"), @event)))),
                LocalDeclarationStatement(
                    VariableDeclaration(
                        asyncResultType,
                        SingletonSeparatedList(
                            VariableDeclarator(
                                Identifier("task"),
                                null,
                                EqualsValueClause(
                                    IdentifierName("handler")
                                        .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))))))),
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.NotEqualsExpression,
                        IdentifierName("task"),
                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                    Block(
                        ExpressionStatement(
                            IdentifierName("pendingTasks")
                                .Dot(IdentifierName("Add"))
                                .Call(Argument(ToTaskExpression(IdentifierName("task"), usesValueTask))))))));
    }
}
