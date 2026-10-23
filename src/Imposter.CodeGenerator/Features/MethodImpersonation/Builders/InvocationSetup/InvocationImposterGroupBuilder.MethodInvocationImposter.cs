using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static partial class InvocationImposterGroupBuilder
{
    private static ClassDeclarationSyntax MethodInvocationImposterType(
        in ImposterTargetMethodMetadata method
    )
    {
        var classBuilder = new ClassDeclarationBuilder(
            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(DefaultInvocationImposterField(method))
            .AddMember(MethodInvocationImposterStaticConstructor(method))
            .AddMember(ResultGeneratorField(method))
            .AddMember(CallbacksField(method))
            .AddMember(
                method.SupportsBaseImplementation ? UseBaseImplementationField(method) : null
            )
            .AddMember(
                SingleVariableField(
                    WellKnownTypes.Bool,
                    method.MethodInvocationImposter.IsConfiguredFieldName,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword))
                )
            )
            .AddMember(InvokeInvocationMethod(method))
            .AddMember(CallbackMethod(method))
            .AddMember(method.HasReturnValue ? ReturnsDelegateMethod(method) : null)
            .AddMember(method.KeepsResult ? ReturnsValueMethod(method) : null)
            .AddMember(
                method.MethodInvocationImposterGroup.ReturnsAsyncMethod.HasValue
                    ? ReturnsAsyncMethod(method)
                    : null
            )
            .AddMember(ThrowsMethod(method))
            .AddMember(
                method.MethodInvocationImposterGroup.ThrowsAsyncMethod.HasValue
                    ? ThrowsAsyncMethod(method)
                    : null
            )
            .AddMember(
                method.SupportsBaseImplementation ? UseBaseImplementationMethod(method) : null
            )
            .AddMember(!method.HasReturnValue ? UseDefaultResultGeneratorMethod(method) : null)
            .AddMember(InitializeOutParametersMethodBuilder.Build(method))
            .AddMember(DefaultResultGenerator(method));

        return classBuilder.Build();
    }

    private static FieldDeclarationSyntax DefaultInvocationImposterField(
        in ImposterTargetMethodMetadata method
    ) =>
        SingleVariableField(
            IdentifierName(method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName),
            "Default",
            TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword))
        );

    private static ConstructorDeclarationSyntax MethodInvocationImposterStaticConstructor(
        in ImposterTargetMethodMetadata method
    )
    {
        var body = new BlockBuilder().AddStatement(
            IdentifierName("Default")
                .Assign(
                    IdentifierName(
                            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
                        )
                        .New(ArgumentList())
                )
                .ToStatementSyntax()
        );

        if (method.HasReturnValue)
        {
            body.AddStatement(
                IdentifierName("Default")
                    .Dot(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.Name))
                    .Call(Argument(DefaultResultGeneratorDelegate(method)))
                    .ToStatementSyntax()
            );
        }
        else
        {
            body.AddStatement(
                IdentifierName("Default")
                    .Dot(ResultGeneratorIdentifier(method))
                    .Assign(DefaultResultGeneratorDelegate(method))
                    .ToStatementSyntax()
            );
        }

        return new ConstructorBuilder(
            method.MethodInvocationImposterGroup.MethodInvocationImposterTypeName
        )
            .WithModifiers(TokenList(Token(SyntaxKind.StaticKeyword)))
            .WithBody(body.Build())
            .Build();
    }

    private static FieldDeclarationSyntax ResultGeneratorField(
        in ImposterTargetMethodMetadata method
    ) =>
        SingleVariableField(
            method.Delegate.Syntax.ToNullableType(),
            method.MethodInvocationImposter.ResultGeneratorFieldName,
            TokenList(Token(SyntaxKind.PrivateKeyword))
        );

    private static FieldDeclarationSyntax UseBaseImplementationField(
        in ImposterTargetMethodMetadata method
    ) =>
        SingleVariableField(
            WellKnownTypes.Bool,
            method.MethodInvocationImposter.UseBaseImplementationFieldName,
            TokenList(Token(SyntaxKind.PrivateKeyword))
        );

    private static FieldDeclarationSyntax CallbacksField(in ImposterTargetMethodMetadata method)
    {
        var queueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            method.CallbackDelegate.Syntax
        );

        return SinglePrivateReadonlyVariableField(
            queueType,
            method.MethodInvocationImposter.CallbacksFieldName,
            queueType.New(ArgumentList())
        );
    }

    // Produces the result and runs the callbacks of one invocation.
    private static MethodDeclarationSyntax InvokeInvocationMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var arguments = ArgumentListSyntax(method.Parameters.AllParameters);
        var body = new BlockBuilder()
            .AddStatement(
                method.SupportsBaseImplementation ? UseBaseImplementationAsResult(method) : null
            )
            .AddStatement(EnsureResultGenerator(method))
            .AddStatement(GenerateResult(method, arguments))
            .AddStatement(InvokeCallbacks(method, arguments))
            .AddStatement(
                method.HasReturnValue
                    ? ReturnStatement(
                        IdentifierName(method.MethodInvocationImposter.ResultVariableName)
                    )
                    : null
            )
            .Build();

        return new MethodDeclarationBuilder(
            method.NullableAwareReturnTypeSyntax,
            MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(InvokeSignatureBuilder.InvocationImposterParameters(method))
            .WithBody(body)
            .Build();
    }

    // Set up to use the base implementation, the invocation calls the base invocation, or throws if none was passed.
    private static IfStatementSyntax UseBaseImplementationAsResult(
        in ImposterTargetMethodMetadata method
    ) =>
        IfStatement(
            UseBaseImplementationIdentifier(method),
            Block(
                ResultGeneratorIdentifier(method)
                    .Assign(
                        IdentifierName(
                                method.MethodImposter.InvokeMethod.BaseInvocationParameter.Name
                            )
                            .Coalesce(
                                ThrowExpression(
                                    MissingImposterException(
                                        IdentifierName(
                                            method
                                                .MethodImposter
                                                .InvokeMethod
                                                .MethodDisplayNameParameterName
                                        )
                                    )
                                )
                            )
                    )
                    .ToStatementSyntax()
            )
        );

    // Without a result set up, an Explicit-mode imposter throws and an Implicit one returns the default result.
    private static IfStatementSyntax EnsureResultGenerator(
        in ImposterTargetMethodMetadata method
    ) =>
        IfStatement(
            ResultGeneratorIdentifier(method).IsNull(),
            Block(
                ThrowIfExplicit(
                    IdentifierName(
                        method.MethodImposter.InvokeMethod.InvocationBehaviorParameterName
                    ),
                    IdentifierName(
                        method.MethodImposter.InvokeMethod.MethodDisplayNameParameterName
                    )
                ),
                ResultGeneratorIdentifier(method)
                    .Assign(DefaultResultGeneratorDelegate(method))
                    .ToStatementSyntax()
            )
        );

    private static StatementSyntax GenerateResult(
        in ImposterTargetMethodMetadata method,
        ArgumentListSyntax arguments
    )
    {
        var result = ResultGeneratorIdentifier(method)
            .Dot(IdentifierName("Invoke"))
            .Call(arguments);

        return method.HasReturnValue
            ? LocalVariableDeclarationSyntax(
                method.NullableAwareReturnTypeSyntax,
                method.MethodInvocationImposter.ResultVariableName,
                result
            )
            : result.ToStatementSyntax();
    }

    private static IfStatementSyntax InvokeCallbacks(
        in ImposterTargetMethodMetadata method,
        ArgumentListSyntax arguments
    )
    {
        var callback = IdentifierName(
            method.MethodImposter.InvokeMethod.CallbackIterationVariableName
        );
        var callbackCall = callback.Call(arguments);

        return method.ReturnType.IsAwaitable
            ? InvokeCallbacksAwaitedByResult(method, callback, callbackCall)
            : ConcurrentQueueSyntaxHelper.UnlessEmpty(
                ForEachStatement(
                    Var,
                    callback.Identifier,
                    CallbacksIdentifier(method),
                    Block(callbackCall.ToStatementSyntax())
                )
            );
    }

    // An async method's callbacks still start with the call, because they can take ref, out and span parameters a
    // continuation couldn't carry. The returned task then waits for those that haven't finished, so their failures
    // reach the caller.
    private static IfStatementSyntax InvokeCallbacksAwaitedByResult(
        in ImposterTargetMethodMetadata method,
        IdentifierNameSyntax callback,
        InvocationExpressionSyntax callbackCall
    )
    {
        var callbackTasks = IdentifierName(
            method.MethodImposter.InvokeMethod.CallbackTasksVariableName
        );
        var result = IdentifierName(method.MethodInvocationImposter.ResultVariableName);
        var track = WellKnownTypes
            .Imposter.Abstractions.AsyncCallbacks.Dot(IdentifierName("Track"))
            .Call(
                ArgumentListSyntax([
                    RefArgument(callbackTasks.Identifier.Text),
                    Argument(callbackCall),
                ])
            );

        return IfStatement(
            Not(CallbacksIdentifier(method).Dot(ConcurrentQueueSyntaxHelper.IsEmpty)),
            Block(
                LocalVariableDeclarationSyntax(
                    WellKnownTypes
                        .System.Collections.Generic.List(WellKnownTypes.System.Threading.Tasks.Task)
                        .ToNullableType(),
                    callbackTasks.Identifier.Text,
                    Null
                ),
                ForEachStatement(
                    Var,
                    callback.Identifier,
                    CallbacksIdentifier(method),
                    Block(track.ToStatementSyntax())
                ),
                IfStatement(
                    callbackTasks.IsNotNull(),
                    result
                        .Assign(ResultAfterCallbacks(method, result, callbackTasks))
                        .ToStatementSyntax()
                )
            )
        );
    }

    // AsyncCallbacks waits on tasks, so a ValueTask result is waited on as a task and wrapped back.
    private static ExpressionSyntax ResultAfterCallbacks(
        in ImposterTargetMethodMetadata method,
        IdentifierNameSyntax result,
        IdentifierNameSyntax callbackTasks
    )
    {
        var completeAfter = WellKnownTypes.Imposter.Abstractions.AsyncCallbacks.Dot(
            IdentifierName("CompleteAfter")
        );
        if (!method.ReturnType.IsValueTask)
        {
            return completeAfter.Call(
                ArgumentListSyntax([Argument(result), Argument(callbackTasks)])
            );
        }

        var resultTask = result.Dot(IdentifierName("AsTask")).Call();
        return method.NullableAwareReturnTypeSyntax.New(
            Argument(
                    completeAfter.Call(
                        ArgumentListSyntax([Argument(resultTask), Argument(callbackTasks)])
                    )
                )
                .ToSingleArgumentList()
        );
    }

    private static MethodDeclarationSyntax CallbackMethod(in ImposterTargetMethodMetadata method)
    {
        var callback = method.MethodInvocationImposterGroup.CallbackMethod;

        return new MethodDeclarationBuilder(WellKnownTypes.Void, callback.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(callback.CallbackParameter))
            .WithBody(
                Block(
                    CallbacksIdentifier(method)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(callback.CallbackParameter.Name.ToArgument())
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax ReturnsDelegateMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var returns = method.MethodInvocationImposterGroup.ReturnsMethod;

        return ResultGeneratorSetter(
            method,
            returns.Name,
            returns.ResultGeneratorParameter,
            IdentifierName(returns.ResultGeneratorParameter.Name)
        );
    }

    private static MethodDeclarationSyntax ReturnsValueMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var returns = method.MethodInvocationImposterGroup.ReturnsMethod;
        var returnValue = new BlockBuilder()
            .AddStatement(InitializeOutParametersMethodBuilder.Invoke(method))
            .AddStatement(ReturnStatement(IdentifierName(returns.ValueParameter.Name)))
            .Build();

        return ResultGeneratorSetter(
            method,
            returns.Name,
            returns.ValueParameter,
            Lambda(method.Parameters.ParameterListSyntaxIncludingNullable, returnValue)
        );
    }

    private static MethodDeclarationSyntax ThrowsMethod(in ImposterTargetMethodMetadata method)
    {
        var throws = method.MethodInvocationImposterGroup.ThrowsMethod;
        var throwGeneratedException = Block(
            ThrowStatement(
                IdentifierName(throws.ExceptionGeneratorParameter.Name)
                    .Call(ArgumentListSyntax(method.Parameters.AllParameters))
            )
        );

        return ResultGeneratorSetter(
            method,
            throws.Name,
            throws.ExceptionGeneratorParameter,
            Lambda(method.Parameters.ParameterListSyntaxIncludingNullable, throwGeneratedException)
        );
    }

    private static MethodDeclarationSyntax UseBaseImplementationMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "UseBaseImplementation")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    UseBaseImplementationIdentifier(method).Assign(True).ToStatementSyntax(),
                    ResultGeneratorIdentifier(method).Assign(Null).ToStatementSyntax()
                )
            )
            .Build();

    private static MethodDeclarationSyntax UseDefaultResultGeneratorMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "UseDefaultResultGenerator")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    ResultGeneratorIdentifier(method)
                        .Assign(DefaultResultGeneratorDelegate(method))
                        .ToStatementSyntax()
                )
            )
            .Build();

    private static IdentifierNameSyntax DefaultResultGeneratorDelegate(
        in ImposterTargetMethodMetadata method
    )
    {
        return IdentifierName(
            method.MethodInvocationImposterGroup.DefaultResultGeneratorMethod.Name
        );
    }

    private static MethodDeclarationSyntax ReturnsAsyncMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var returns = method.MethodInvocationImposterGroup.ReturnsAsyncMethod!.Value;
        var value = method.ReturnType.AwaitableResultValue(
            IdentifierName(returns.ValueParameter.Name)
        );
        ExpressionSyntax awaitable = method.ReturnType.IsValueTask
            ? method.NullableAwareReturnTypeSyntax.New(Argument(value).ToSingleArgumentList())
            : WellKnownTypes
                .System.Threading.Tasks.Task.Dot(
                    GenericName(
                        Identifier("FromResult"),
                        TypeArgumentList(
                            SingletonSeparatedList(method.ReturnType.GenericAwaitableResultType!)
                        )
                    )
                )
                .Call(Argument(value));

        return CachedAsyncResultSetter(method, returns.Name, returns.ValueParameter, awaitable);
    }

    private static MethodDeclarationSyntax ThrowsAsyncMethod(in ImposterTargetMethodMetadata method)
    {
        var throws = method.MethodInvocationImposterGroup.ThrowsAsyncMethod!.Value;
        var functionName = method.MethodInvocationImposter.AsyncResultFunctionName;

        // Run the async factory once at setup. Unlike Task.FromException, this also preserves
        // cancellation and the original exception instance for OperationCanceledException.
        return CachedAsyncResultSetter(
            method,
            throws.Name,
            throws.ExceptionParameter,
            IdentifierName(functionName).Call(),
            LocalFunctionStatement(method.NullableAwareReturnTypeSyntax, Identifier(functionName))
                .AddModifiers(Token(SyntaxKind.AsyncKeyword))
                .WithBody(Block(ThrowStatement(IdentifierName(throws.ExceptionParameter.Name))))
        );
    }

    private static MethodDeclarationSyntax CachedAsyncResultSetter(
        in ImposterTargetMethodMetadata method,
        string name,
        in ParameterMetadata parameter,
        ExpressionSyntax awaitable,
        LocalFunctionStatementSyntax? factory = null
    )
    {
        var cachedResult = IdentifierName(
            method.MethodInvocationImposter.CachedAsyncResultVariableName
        );
        return ResultGeneratorSetter(
            method,
            name,
            parameter,
            Lambda(
                method.Parameters.ParameterListSyntaxIncludingNullable,
                new BlockBuilder()
                    .AddStatement(InitializeOutParametersMethodBuilder.Invoke(method))
                    .AddStatement(ReturnStatement(cachedResult))
                    .Build()
            ),
            LocalVariableDeclarationSyntax(
                method.NullableAwareReturnTypeSyntax,
                cachedResult.Identifier.Text,
                awaitable
            ),
            factory
        );
    }

    // An outcome setter: it turns the base implementation off, when the method has one, and makes resultGenerator
    // produce the outcome of the calls this invocation imposter handles.
    private static MethodDeclarationSyntax ResultGeneratorSetter(
        in ImposterTargetMethodMetadata method,
        string name,
        in ParameterMetadata parameter,
        ExpressionSyntax resultGenerator,
        StatementSyntax? initializeResult = null,
        LocalFunctionStatementSyntax? factory = null
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(parameter))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(initializeResult)
                    .AddStatement(
                        method.SupportsBaseImplementation
                            ? DisableBaseImplementationStatement(method)
                            : null
                    )
                    .AddStatement(
                        ResultGeneratorIdentifier(method)
                            .Assign(resultGenerator)
                            .ToStatementSyntax()
                    )
                    .AddStatement(factory)
                    .Build()
            )
            .Build();

    private static ExpressionStatementSyntax DisableBaseImplementationStatement(
        in ImposterTargetMethodMetadata method
    ) => UseBaseImplementationIdentifier(method).Assign(False).ToStatementSyntax();

    private static IdentifierNameSyntax ResultGeneratorIdentifier(
        in ImposterTargetMethodMetadata method
    ) => IdentifierName(method.MethodInvocationImposter.ResultGeneratorFieldName);

    private static IdentifierNameSyntax CallbacksIdentifier(
        in ImposterTargetMethodMetadata method
    ) => IdentifierName(method.MethodInvocationImposter.CallbacksFieldName);

    private static IdentifierNameSyntax UseBaseImplementationIdentifier(
        in ImposterTargetMethodMetadata method
    ) => IdentifierName(method.MethodInvocationImposter.UseBaseImplementationFieldName);
}
