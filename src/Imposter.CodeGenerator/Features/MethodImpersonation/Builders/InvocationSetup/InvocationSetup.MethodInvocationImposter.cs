using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
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
            .AddMember(IsEmptyProperty(method))
            .AddMember(InvokeInvocationMethod(method))
            .AddMember(CallbackMethod(method))
            .AddMember(method.HasReturnValue ? ReturnsDelegateMethod(method) : null)
            .AddMember(method.HasReturnValue ? ReturnsValueMethod(method) : null)
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

    private static PropertyDeclarationSyntax IsEmptyProperty(in ImposterTargetMethodMetadata method)
    {
        ExpressionSyntax condition = ResultGeneratorIdentifier(method)
            .IsNull()
            .And(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    CallbacksIdentifier(method).Dot(IdentifierName("Count")),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
                )
            );

        if (method.SupportsBaseImplementation)
        {
            condition = Not(UseBaseImplementationIdentifier(method)).And(condition);
        }

        return new PropertyDeclarationBuilder(WellKnownTypes.Bool, "IsEmpty")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(ArrowExpressionClause(condition))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    private static MethodDeclarationSyntax InvokeInvocationMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var parameterList = BuildInvocationParameterList(method);
        var arguments = ArgumentListSyntax(method.Parameters.AllParameters);
        var resultInvocation = ResultGeneratorIdentifier(method)
            .Dot(IdentifierName("Invoke"))
            .Call(arguments);
        var resultVariableIdentifier = IdentifierName(
            method.MethodInvocationImposter.ResultVariableName
        );

        var defaultBlockBuilder = new BlockBuilder().AddStatement(
            IfStatement(
                ResultGeneratorIdentifier(method).IsNull(),
                Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName(
                                method.MethodImposter.InvokeMethod.InvocationBehaviorParameterName
                            ),
                            QualifiedName(
                                WellKnownTypes.Imposter.Abstractions.ImposterMode,
                                IdentifierName("Explicit")
                            )
                        ),
                        Block(
                            ThrowStatement(
                                ObjectCreationExpression(
                                        WellKnownTypes
                                            .Imposter
                                            .Abstractions
                                            .MissingImposterException
                                    )
                                    .WithArgumentList(
                                        Argument(
                                                IdentifierName(
                                                    method
                                                        .MethodImposter
                                                        .InvokeMethod
                                                        .MethodDisplayNameParameterName
                                                )
                                            )
                                            .AsSingleArgumentListSyntax()
                                    )
                            )
                        )
                    ),
                    ResultGeneratorIdentifier(method)
                        .Assign(DefaultResultGeneratorDelegate(method))
                        .ToStatementSyntax()
                )
            )
        );

        if (method.Model.ReturnType.IsVoid)
        {
            defaultBlockBuilder.AddStatement(resultInvocation.ToStatementSyntax());
        }
        else
        {
            defaultBlockBuilder.AddStatement(
                LocalVariableDeclarationSyntax(
                    method.NullableAwareReturnTypeSyntax,
                    method.MethodInvocationImposter.ResultVariableName,
                    resultInvocation
                )
            );
        }

        var callbackIdentifier = Identifier(
            method.MethodImposter.InvokeMethod.CallbackIterationVariableName
        );
        var callbackInvocation = ForEachStatement(
            Var,
            callbackIdentifier,
            CallbacksIdentifier(method),
            Block(
                IdentifierName(method.MethodImposter.InvokeMethod.CallbackIterationVariableName)
                    .Call(arguments)
                    .ToStatementSyntax()
            )
        );

        defaultBlockBuilder.AddStatement(callbackInvocation);

        if (!method.Model.ReturnType.IsVoid)
        {
            defaultBlockBuilder.AddStatement(ReturnStatement(resultVariableIdentifier));
        }

        var defaultBlock = defaultBlockBuilder.Build();
        BlockSyntax body;

        if (method.SupportsBaseImplementation)
        {
            var missingImposterException = ObjectCreationExpression(
                    WellKnownTypes.Imposter.Abstractions.MissingImposterException
                )
                .WithArgumentList(
                    Argument(
                            IdentifierName(
                                method.MethodImposter.InvokeMethod.MethodDisplayNameParameterName
                            )
                        )
                        .AsSingleArgumentListSyntax()
                );

            var assignBaseImplementation = IfStatement(
                UseBaseImplementationIdentifier(method),
                Block(
                    ResultGeneratorIdentifier(method)
                        .Assign(
                            IdentifierName(
                                    method.MethodImposter.InvokeMethod.BaseInvocationParameterName
                                )
                                .Coalesce(ThrowExpression(missingImposterException))
                        )
                        .ToStatementSyntax()
                )
            );

            body = new BlockBuilder()
                .AddStatement(assignBaseImplementation)
                .AddStatements(defaultBlock.Statements)
                .Build();
        }
        else
        {
            body = defaultBlock;
        }

        return new MethodDeclarationBuilder(method.NullableAwareReturnTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameterList)
            .WithBody(body)
            .Build();
    }

    private static ParameterListSyntax BuildInvocationParameterList(
        in ImposterTargetMethodMetadata method
    )
    {
        List<ParameterSyntax> parameters =
        [
            ParameterSyntax(
                WellKnownTypes.Imposter.Abstractions.ImposterMode,
                method.MethodImposter.InvokeMethod.InvocationBehaviorParameterName
            ),
            ParameterSyntax(
                WellKnownTypes.String,
                method.MethodImposter.InvokeMethod.MethodDisplayNameParameterName
            ),
            .. method.Parameters.ParameterListSyntaxIncludingNullable.Parameters,
        ];

        if (method.SupportsBaseImplementation)
        {
            parameters.Add(
                ParameterSyntax(
                        method.Delegate.Syntax.ToNullableType(),
                        method.MethodImposter.InvokeMethod.BaseInvocationParameterName
                    )
                    .WithDefault(EqualsValueClause(Null))
            );
        }

        return ParameterList(SeparatedList(parameters));
    }

    private static MethodDeclarationSyntax CallbackMethod(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            method.MethodInvocationImposterGroup.CallbackMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    method.CallbackDelegate.Syntax,
                    method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter.Name
                )
            )
            .WithBody(
                Block(
                    CallbacksIdentifier(method)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(
                            Argument(
                                IdentifierName(
                                    method
                                        .MethodInvocationImposterGroup
                                        .CallbackMethod
                                        .CallbackParameter
                                        .Name
                                )
                            )
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();

    private static MethodDeclarationSyntax ReturnsDelegateMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement(method));
        }

        blockBuilder.AddStatement(
            ResultGeneratorIdentifier(method)
                .Assign(
                    IdentifierName(
                        method
                            .MethodInvocationImposterGroup
                            .ReturnsMethod
                            .ResultGeneratorParameter
                            .Name
                    )
                )
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            method.MethodInvocationImposterGroup.ReturnsMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    method.Delegate.Syntax,
                    method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter.Name
                )
            )
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax ReturnsValueMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var lambdaBody = new BlockBuilder().AddStatement(
            InitializeOutParametersMethodBuilder.Invoke(method)
        );

        lambdaBody.AddStatement(
            ReturnStatement(
                IdentifierName(
                    method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter.Name
                )
            )
        );

        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement(method));
        }

        blockBuilder.AddStatement(
            ResultGeneratorIdentifier(method)
                .Assign(
                    Lambda(
                        method.Parameters.ParameterListSyntaxIncludingNullable,
                        lambdaBody.Build()
                    )
                )
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            method.MethodInvocationImposterGroup.ReturnsMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter)
            )
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax ThrowsMethod(in ImposterTargetMethodMetadata method)
    {
        var throwsParameter = method
            .MethodInvocationImposterGroup
            .ThrowsMethod
            .ExceptionGeneratorParameter;

        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement(method));
        }

        blockBuilder.AddStatement(
            ResultGeneratorIdentifier(method)
                .Assign(
                    Lambda(
                        method.Parameters.ParameterListSyntaxIncludingNullable,
                        Block(
                            ThrowStatement(
                                IdentifierName(throwsParameter.Name)
                                    .Call(ArgumentListSyntax(method.Parameters.AllParameters))
                            )
                        )
                    )
                )
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            method.MethodInvocationImposterGroup.ThrowsMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(throwsParameter.Type, throwsParameter.Name))
            .WithBody(blockBuilder.Build())
            .Build();
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
        var returnsAsync = method.MethodInvocationImposterGroup.ReturnsAsyncMethod!.Value;
        var lambdaBody = new BlockBuilder().AddStatement(
            InitializeOutParametersMethodBuilder.Invoke(method)
        );

        lambdaBody.AddStatement(ReturnStatement(IdentifierName(returnsAsync.ValueParameter.Name)));

        var returnsAsyncBodyBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            returnsAsyncBodyBuilder.AddStatement(DisableBaseImplementationStatement(method));
        }

        returnsAsyncBodyBuilder.AddStatement(
            ResultGeneratorIdentifier(method)
                .Assign(AsyncResultGenerator(method, lambdaBody.Build()))
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, returnsAsync.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(returnsAsync.ValueParameter.Type, returnsAsync.ValueParameter.Name)
            )
            .WithBody(returnsAsyncBodyBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax ThrowsAsyncMethod(in ImposterTargetMethodMetadata method)
    {
        var throwsAsync = method.MethodInvocationImposterGroup.ThrowsAsyncMethod!.Value;

        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement(method));
        }

        blockBuilder.AddStatement(
            ResultGeneratorIdentifier(method)
                .Assign(
                    AsyncResultGenerator(
                        method,
                        Block(
                            ThrowExpression(IdentifierName(throwsAsync.ExceptionParameter.Name))
                                .ToStatementSyntax()
                        )
                    )
                )
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, throwsAsync.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(
                    throwsAsync.ExceptionParameter.Type,
                    throwsAsync.ExceptionParameter.Name
                )
            )
            .WithBody(blockBuilder.Build())
            .Build();
    }

    // A span parameter can't be declared on an async lambda (CS4012), so a method with span parameters gets a plain
    // lambda that returns the result of an async local function.
    private static ParenthesizedLambdaExpressionSyntax AsyncResultGenerator(
        in ImposterTargetMethodMetadata method,
        BlockSyntax asyncBody
    ) =>
        method.Parameters.HasSpanParameters
            ? Lambda(
                method.Parameters.ParameterListSyntaxIncludingNullable,
                AsyncResultFunctionCall(method, asyncBody)
            )
            : AsyncLambda(method.Parameters.ParameterListSyntaxIncludingNullable, asyncBody);

    // Returns the result of a parameterless async local function that runs asyncBody.
    private static BlockSyntax AsyncResultFunctionCall(
        in ImposterTargetMethodMetadata method,
        BlockSyntax asyncBody
    )
    {
        var functionName = method.MethodInvocationImposter.AsyncResultFunctionName;

        return Block(
            ReturnStatement(IdentifierName(functionName).Call()),
            LocalFunctionStatement(method.NullableAwareReturnTypeSyntax, Identifier(functionName))
                .AddModifiers(Token(SyntaxKind.AsyncKeyword))
                .WithBody(asyncBody)
        );
    }

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
