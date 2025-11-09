using System;
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

internal static class EventImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterEventMetadata @event) =>
        new ClassDeclarationBuilder(@event.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.SetupInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(@event.BuilderInterface.VerificationInterfaceTypeSyntax))
            .AddMembers(BuildFields(@event))
            .AddMember(BuildConstructor(@event))
            .AddMember(BuildSubscribeMethod(@event))
            .AddMember(BuildUnsubscribeMethod(@event))
            .AddMember(BuildCallbackMethod(@event))
            .AddMember(BuildRaiseMethod(@event))
            .AddMember(BuildSubscribedVerificationMethod(@event))
            .AddMember(BuildUnsubscribedVerificationMethod(@event))
            .AddMember(BuildOnSubscribeMethod(@event))
            .AddMember(BuildOnUnsubscribeMethod(@event))
            .AddMember(BuildRaisedVerificationMethod(@event))
            .AddMember(BuildHandlerInvokedVerificationMethod(@event))
            .AddMember(@event.Core.IsAsync ? null : BuildRaiseInternalMethod(@event))
            .AddMember(@event.Core.IsAsync ? BuildRaiseCoreAsyncMethod(@event) : null)
            .AddMember(BuildEnumerateHandlersMethod(@event))
            .AddMember(BuildCountMatchesMethod(@event.Builder.Methods))
            .AddMember(BuildEnsureCountMatchesMethod(@event.Builder.Methods))
            .Build();

    private static MemberDeclarationSyntax[] BuildFields(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;

        return
        [
            SinglePrivateReadonlyVariableField(fields.HandlerOrder, fields.HandlerOrder.Type.New()),
            SinglePrivateReadonlyVariableField(fields.HandlerCounts, fields.HandlerCounts.Type.New()),
            SinglePrivateReadonlyVariableField(fields.Callbacks, fields.Callbacks.Type.New()),
            SinglePrivateReadonlyVariableField(fields.History, fields.History.Type.New()),
            SinglePrivateReadonlyVariableField(fields.SubscribeHistory, fields.SubscribeHistory.Type.New()),
            SinglePrivateReadonlyVariableField(fields.UnsubscribeHistory, fields.UnsubscribeHistory.Type.New()),
            SinglePrivateReadonlyVariableField(fields.SubscribeInterceptors, fields.SubscribeInterceptors.Type.New()),
            SinglePrivateReadonlyVariableField(fields.UnsubscribeInterceptors, fields.UnsubscribeInterceptors.Type.New()),
            SinglePrivateReadonlyVariableField(fields.HandlerInvocations, fields.HandlerInvocations.Type.New())
        ];
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterEventMetadata @event) =>
        new ConstructorBuilder(@event.Builder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .WithBody(Block())
            .Build();

    private static MethodDeclarationSyntax BuildSubscribeMethod(in ImposterEventMetadata @event)
    {
        var fields = @event.Builder.Fields;
        var method = @event.Builder.Methods.Subscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        var addOrUpdateExpression =
            FieldIdentifier(fields.HandlerCounts)
                .Dot(IdentifierName("AddOrUpdate"))
                .Call([
                    Argument(handlerIdentifier),
                    Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))),
                    Argument(BuildIncrementLambda())
                ]);

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
                    .AddExpression(
                        FieldIdentifier(fields.HandlerOrder)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(handlerIdentifier)))
                    .AddExpression(addOrUpdateExpression)
                    .AddExpression(
                        FieldIdentifier(fields.SubscribeHistory)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(handlerIdentifier)))
                    .AddStatement(ForEachInterceptor(fields.SubscribeInterceptors, method.HandlerParameter.Name))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildUnsubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Unsubscribe;
        var handlerIdentifier = IdentifierName(method.HandlerParameter.Name);

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), method.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.HandlerParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.HandlerParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.HandlerCounts)
                            .Dot(IdentifierName("AddOrUpdate"))
                            .Call([
                                Argument(handlerIdentifier),
                                Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                                Argument(BuildUnsubscribeLambda())
                            ]))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.UnsubscribeHistory)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(handlerIdentifier)))
                    .AddStatement(ForEachInterceptor(@event.Builder.Fields.UnsubscribeInterceptors, method.HandlerParameter.Name))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildCallbackMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Callback;
        var callbackIdentifier = IdentifierName(method.CallbackParameter.Name);

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.CallbackParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.CallbackParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.Callbacks)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(callbackIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

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

    private static MethodDeclarationBuilder ExplicitInterfaceMethod(
        NameSyntax interfaceType,
        TypeSyntax returnType,
        string name) =>
        new MethodDeclarationBuilder(returnType, name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(interfaceType));

    private static ExpressionStatementSyntax BuildAwaitRaiseAsyncStatement(in ImposterEventMetadata @event) =>
        ExpressionStatement(
            AwaitExpression(
                IdentifierName(@event.Builder.Methods.RaiseCoreAsyncName)
                    .Call(@event.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))
                    .Dot(IdentifierName("ConfigureAwait"))
                    .Call(Argument(LiteralExpression(SyntaxKind.FalseLiteralExpression)))));

    private static InvocationExpressionSyntax ThrowIfNull(string parameterName) =>
        IdentifierName(nameof(ArgumentNullException))
            .Dot(IdentifierName("ThrowIfNull"))
            .Call(Argument(IdentifierName(parameterName)));

    private static ForEachStatementSyntax ForEachInterceptor(in FieldMetadata interceptorsField, string handlerIdentifier) =>
        ForEachStatement(
            Var,
            Identifier("interceptor"),
            FieldIdentifier(interceptorsField),
            Block(
                ExpressionStatement(
                    IdentifierName("interceptor")
                        .Call(Argument(IdentifierName(handlerIdentifier))))));

    private static ParenthesizedLambdaExpressionSyntax BuildUnsubscribeLambda() =>
        ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier("_")),
                        Parameter(Identifier("count"))
                    ])))
            .WithBlock(
                Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.GreaterThanExpression,
                            IdentifierName("count"),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                        Block(
                            ReturnStatement(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    IdentifierName("count"),
                                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))))),
                    ReturnStatement(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)))));

    private static ParenthesizedLambdaExpressionSyntax BuildIncrementLambda() =>
        ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier("_")),
                        Parameter(Identifier("count"))
                    ])))
            .WithExpressionBody(
                BinaryExpression(
                    SyntaxKind.AddExpression,
                    IdentifierName("count"),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))));

    private static MethodDeclarationSyntax BuildSubscribedVerificationMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Subscribed;
        var criteriaName = method.CriteriaParameter.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.SubscribeHistory,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler => IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(handler)),
                    action: "subscribed"))
            .Build();
    }

    private static MethodDeclarationSyntax BuildUnsubscribedVerificationMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.Unsubscribed;
        var criteriaName = method.CriteriaParameter.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.CriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.UnsubscribeHistory,
                    criteriaParameterName: criteriaName,
                    predicateFactory: handler => IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(handler)),
                    action: "unsubscribed"))
            .Build();
    }

    private static MethodDeclarationSyntax BuildOnSubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.OnSubscribe;
        var interceptorIdentifier = IdentifierName(method.InterceptorParameter.Name);

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.InterceptorParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.InterceptorParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.SubscribeInterceptors)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(interceptorIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildOnUnsubscribeMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.OnUnsubscribe;
        var interceptorIdentifier = IdentifierName(method.InterceptorParameter.Name);

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.InterceptorParameter))
            .WithBody(
                new BlockBuilder()
                    .AddExpression(ThrowIfNull(method.InterceptorParameter.Name))
                    .AddExpression(
                        FieldIdentifier(@event.Builder.Fields.UnsubscribeInterceptors)
                            .Dot(IdentifierName("Enqueue"))
                            .Call(Argument(interceptorIdentifier)))
                    .AddStatement(ReturnStatement(ThisExpression()))
                    .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildRaisedVerificationMethod(in ImposterEventMetadata @event)
    {
        var methodBuilder = ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.Builder.Methods.RaisedVerificationName)
            .AddParameters(@event.Core.Parameters.Select(parameter =>
                ParameterSyntax(parameter.ArgTypeSyntax, $"{parameter.Name}Criteria")))
            .AddParameter(CountParameter(@event));

        return methodBuilder.WithBody(BuildRaisedBody(@event)).Build();
    }

    private static BlockSyntax BuildRaisedBody(in ImposterEventMetadata @event)
    {
        var blockBuilder = new BlockBuilder();
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var countIdentifier = IdentifierName(countParameterName);
        var countMatchesIdentifier = IdentifierName(@event.Builder.Methods.CountMatchesName);
        var ensureCountMatchesIdentifier = IdentifierName(@event.Builder.Methods.EnsureCountMatchesName);

        foreach (var parameter in @event.Core.Parameters)
        {
            blockBuilder.AddExpression(ThrowIfNull($"{parameter.Name}Criteria"));
        }

        blockBuilder.AddExpression(ThrowIfNull(countParameterName));

        var predicate = BuildRaisedPredicate(@event);

        blockBuilder.AddStatement(
            LocalDeclarationStatement(
                VariableDeclaration(
                    PredefinedType(Token(SyntaxKind.IntKeyword)),
                    SingletonSeparatedList(
                        VariableDeclarator(
                            Identifier("actual"),
                            null,
                            EqualsValueClause(
                                countMatchesIdentifier
                                    .Call([
                                        Argument(FieldIdentifier(@event.Builder.Fields.History)),
                                        Argument(predicate)
                                    ])))))));

        blockBuilder.AddExpression(
            ensureCountMatchesIdentifier
                .Call([
                    Argument(IdentifierName("actual")),
                    Argument(countIdentifier),
                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("raised")))
                ]));

        blockBuilder.AddStatement(ReturnStatement(ThisExpression()));

        return blockBuilder.Build();
    }

    private static SimpleLambdaExpressionSyntax BuildRaisedPredicate(in ImposterEventMetadata @event)
    {
        var entryIdentifier = IdentifierName("entry");
        ExpressionSyntax? predicateBody = null;

        if (@event.Core.Parameters.Length > 0)
        {
            foreach (var parameter in @event.Core.Parameters)
            {
                var matchCall = IdentifierName($"{parameter.Name}Criteria")
                    .Dot(IdentifierName("Matches"))
                    .Call(Argument(entryIdentifier.Dot(IdentifierName(parameter.Name))));

                predicateBody = predicateBody is null
                    ? matchCall
                    : BinaryExpression(SyntaxKind.LogicalAndExpression, predicateBody, matchCall);
            }
        }
        else
        {
            predicateBody = LiteralExpression(SyntaxKind.TrueLiteralExpression);
        }

        return SimpleLambdaExpression(Parameter(Identifier("entry")), predicateBody!);
    }

    private static MethodDeclarationSyntax BuildHandlerInvokedVerificationMethod(in ImposterEventMetadata @event)
    {
        var method = @event.Builder.Methods.HandlerInvoked;
        var criteriaName = method.HandlerCriteriaParameter.Name;

        return ExplicitInterfaceMethod(
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                @event.BuilderInterface.VerificationInterfaceTypeSyntax,
                method.Name)
            .AddParameter(ParameterSyntax(method.HandlerCriteriaParameter))
            .AddParameter(CountParameter(@event))
            .WithBody(
                BuildHistoryVerificationBody(
                    @event,
                    historyField: @event.Builder.Fields.HandlerInvocations,
                    criteriaParameterName: criteriaName,
                    predicateFactory: entry => IdentifierName(criteriaName)
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(entry.Dot(IdentifierName("Handler")))),
                    action: "invoked"))
            .Build();
    }

    private static BlockSyntax BuildHistoryVerificationBody(
        in ImposterEventMetadata @event,
        in FieldMetadata historyField,
        string criteriaParameterName,
        Func<ExpressionSyntax, ExpressionSyntax> predicateFactory,
        string action)
    {
        var countParameterName = @event.Builder.Methods.CountParameter.Name;
        var countIdentifier = IdentifierName(countParameterName);
        var countMatchesIdentifier = IdentifierName(@event.Builder.Methods.CountMatchesName);
        var ensureCountMatchesIdentifier = IdentifierName(@event.Builder.Methods.EnsureCountMatchesName);

        var blockBuilder = new BlockBuilder()
            .AddExpression(ThrowIfNull(criteriaParameterName))
            .AddExpression(ThrowIfNull(countParameterName));

        blockBuilder.AddStatement(
            LocalDeclarationStatement(
                VariableDeclaration(
                    PredefinedType(Token(SyntaxKind.IntKeyword)),
                    SingletonSeparatedList(
                        VariableDeclarator(
                            Identifier("actual"),
                            null,
                            EqualsValueClause(
                                countMatchesIdentifier
                                    .Call([
                                        Argument(FieldIdentifier(historyField)),
                                        Argument(
                                            SimpleLambdaExpression(
                                                Parameter(Identifier("entry")),
                                                predicateFactory(IdentifierName("entry"))))
                                    ])))))));

        blockBuilder.AddExpression(
            ensureCountMatchesIdentifier
                .Call([
                    Argument(IdentifierName("actual")),
                    Argument(countIdentifier),
                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(action)))
                ]));

        blockBuilder.AddStatement(ReturnStatement(ThisExpression()));

        return blockBuilder.Build();
    }

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

    private static IdentifierNameSyntax FieldIdentifier(in FieldMetadata field) =>
        IdentifierName(field.Name);

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
                        InvocationExpression(
                            IdentifierName("budgets")
                                .Dot(IdentifierName("TryGetValue")))
                            .WithArgumentList(
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

    private static MethodDeclarationSyntax BuildCountMatchesMethod(in EventImposterBuilderMethodsMetadata methods)
    {
        var enumerableType = QualifiedName(
            WellKnownTypes.System.Collections.Generic.Namespace,
            GenericName(
                Identifier("IEnumerable"),
                TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName("T")))));

        var funcType = QualifiedName(
            WellKnownTypes.System.Namespace,
            GenericName(
                Identifier("Func"),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>([
                        IdentifierName("T"),
                        PredefinedType(Token(SyntaxKind.BoolKeyword))
                    ]))));

        return MethodDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)), Identifier(methods.CountMatchesName))
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.StaticKeyword))
            .WithTypeParameterList(TypeParameterList(SingletonSeparatedList(TypeParameter("T"))))
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier("source")).WithType(enumerableType),
                        Parameter(Identifier("predicate")).WithType(funcType)
                    ])))
            .WithBody(
                Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            PredefinedType(Token(SyntaxKind.IntKeyword)),
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("count"))
                                    .WithInitializer(EqualsValueClause(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))))))),
                    ForEachStatement(
                        Var,
                        Identifier("item"),
                        IdentifierName("source"),
                        Block(
                            IfStatement(
                                InvocationExpression(IdentifierName("predicate"))
                                    .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("item"))))),
                                Block(
                                    ExpressionStatement(
                                        PostfixUnaryExpression(
                                            SyntaxKind.PostIncrementExpression,
                                            IdentifierName("count"))))))),
                    ReturnStatement(IdentifierName("count"))));
    }

    private static MethodDeclarationSyntax BuildEnsureCountMatchesMethod(in EventImposterBuilderMethodsMetadata methods) =>
        MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier(methods.EnsureCountMatchesName))
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.StaticKeyword))
            .WithParameterList(
                ParameterList(SeparatedList([
                    Parameter(Identifier("actual")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("expected")).WithType(WellKnownTypes.Imposter.Abstractions.Count),
                    Parameter(Identifier("action")).WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
                ])))
            .WithBody(
                Block(
                    IfStatement(
                        PrefixUnaryExpression(
                            SyntaxKind.LogicalNotExpression,
                            IdentifierName("expected")
                                .Dot(IdentifierName("Matches"))
                                .Call(Argument(IdentifierName("actual")))),
                        Block(
                            ThrowStatement(
                                ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.VerificationFailedException)
                                    .WithArgumentList(
                                        ArgumentList(SeparatedList([
                                            Argument(IdentifierName("expected")),
                                            Argument(IdentifierName("actual"))
                                        ]))))))));

    private static ParameterSyntax CountParameter(in ImposterEventMetadata @event) =>
        ParameterSyntax(@event.Builder.Methods.CountParameter);
}



