using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
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
            MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(DefaultInvocationImposterField())
            .AddMember(MethodInvocationImposterStaticConstructor(method))
            .AddMember(ResultGeneratorField(method))
            .AddMember(CallbacksField(method))
            .AddMember(method.SupportsBaseImplementation ? UseBaseImplementationField() : null)
            .AddMember(IsEmptyProperty(method.SupportsBaseImplementation))
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
            .AddMember(method.SupportsBaseImplementation ? UseBaseImplementationMethod() : null)
            .AddMember(InitializeOutParametersMethodBuilder.Build(method))
            .AddMember(DefaultResultGenerator(method));

        return classBuilder.Build();
    }

    private static FieldDeclarationSyntax DefaultInvocationImposterField() =>
        SingleVariableField(
            IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName),
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
                            MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName
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
                    .Dot(IdentifierName("_resultGenerator"))
                    .Assign(DefaultResultGeneratorDelegate(method))
                    .ToStatementSyntax()
            );
        }

        return new ConstructorBuilder(
            MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName
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
            "_resultGenerator",
            TokenList(Token(SyntaxKind.PrivateKeyword))
        );

    private static FieldDeclarationSyntax UseBaseImplementationField() =>
        SingleVariableField(
            WellKnownTypes.Bool,
            "_useBaseImplementation",
            TokenList(Token(SyntaxKind.PrivateKeyword))
        );

    private static FieldDeclarationSyntax CallbacksField(in ImposterTargetMethodMetadata method)
    {
        var queueType = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            method.CallbackDelegate.Syntax
        );

        return SinglePrivateReadonlyVariableField(
            queueType,
            "_callbacks",
            queueType.New(ArgumentList())
        );
    }

    private static PropertyDeclarationSyntax IsEmptyProperty(bool supportsBaseImplementation)
    {
        ExpressionSyntax condition = BinaryExpression(
                SyntaxKind.EqualsExpression,
                IdentifierName("_resultGenerator"),
                Null
            )
            .And(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    IdentifierName("_callbacks").Dot(IdentifierName("Count")),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
                )
            );

        if (supportsBaseImplementation)
        {
            condition = Not(IdentifierName("_useBaseImplementation")).And(condition);
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
        var arguments = ArgumentListSyntax(method.Symbol.Parameters);
        var resultInvocation = IdentifierName("_resultGenerator")
            .Dot(IdentifierName("Invoke"))
            .Call(arguments);
        var resultVariableIdentifier = IdentifierName(
            method.MethodInvocationImposter.ResultVariableName
        );

        var defaultBlockBuilder = new BlockBuilder().AddStatement(
            IfStatement(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    IdentifierName("_resultGenerator"),
                    Null
                ),
                Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName("invocationBehavior"),
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
                                        Argument(IdentifierName("methodDisplayName"))
                                            .AsSingleArgumentListSyntax()
                                    )
                            )
                        )
                    ),
                    IdentifierName("_resultGenerator")
                        .Assign(DefaultResultGeneratorDelegate(method))
                        .ToStatementSyntax()
                )
            )
        );

        if (method.Symbol.ReturnsVoid)
        {
            defaultBlockBuilder.AddStatement(resultInvocation.ToStatementSyntax());
        }
        else
        {
            defaultBlockBuilder.AddStatement(
                LocalVariableDeclarationSyntax(
                    method.ReturnTypeSyntax,
                    method.MethodInvocationImposter.ResultVariableName,
                    resultInvocation
                )
            );
        }

        var callbackIdentifier = Identifier(
            method.MethodImposter.InvokeMethod.CallbackIterationVariableName
        );
        var callbackInvocation = ForEachStatement(
            IdentifierName("var"),
            callbackIdentifier,
            IdentifierName("_callbacks"),
            Block(
                IdentifierName(method.MethodImposter.InvokeMethod.CallbackIterationVariableName)
                    .Call(arguments)
                    .ToStatementSyntax()
            )
        );

        defaultBlockBuilder.AddStatement(callbackInvocation);

        if (!method.Symbol.ReturnsVoid)
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
                    Argument(IdentifierName("methodDisplayName")).AsSingleArgumentListSyntax()
                );

            var assignBaseImplementation = IfStatement(
                IdentifierName("_useBaseImplementation"),
                Block(
                    IdentifierName("_resultGenerator")
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

        return new MethodDeclarationBuilder(method.ReturnTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameterList)
            .WithBody(body)
            .Build();
    }

    private static ParameterListSyntax BuildInvocationParameterList(
        in ImposterTargetMethodMetadata method
    )
    {
        var parameters = new List<ParameterSyntax>
        {
            ParameterSyntax(
                WellKnownTypes.Imposter.Abstractions.ImposterMode,
                "invocationBehavior"
            ),
            ParameterSyntax(PredefinedType(Token(SyntaxKind.StringKeyword)), "methodDisplayName"),
        };

        parameters.AddRange(
            method.Parameters.ParameterListSyntaxIncludingNullable.Parameters
        );

        if (method.SupportsBaseImplementation)
        {
            parameters.Add(
                Parameter(
                        Identifier(method.MethodImposter.InvokeMethod.BaseInvocationParameterName)
                    )
                    .WithType(method.Delegate.Syntax.ToNullableType())
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
                    IdentifierName("_callbacks")
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
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
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
                Parameter(
                        Identifier(
                            method
                                .MethodInvocationImposterGroup
                                .ReturnsMethod
                                .ResultGeneratorParameter
                                .Name
                        )
                    )
                    .WithType(method.Delegate.Syntax)
            )
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax ReturnsValueMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var lambdaBody = new BlockBuilder();

        if (method.Parameters.HasOutputParameters)
        {
            lambdaBody.AddStatement(InitializeOutParametersMethodBuilder.Invoke(method));
        }

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
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(Lambda(method.Symbol.Parameters, lambdaBody.Build()))
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            method.MethodInvocationImposterGroup.ReturnsMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(
                        Identifier(
                            method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter.Name
                        )
                    )
                    .WithType(method.ReturnTypeSyntax)
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
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(
                    Lambda(
                        method.Symbol.Parameters,
                        Block(
                            ThrowStatement(
                                IdentifierName(throwsParameter.Name)
                                    .Call(ArgumentListSyntax(method.Symbol.Parameters))
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
            .AddParameter(
                Parameter(Identifier(throwsParameter.Name)).WithType(throwsParameter.Type)
            )
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax UseBaseImplementationMethod() =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "UseBaseImplementation")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    IdentifierName("_useBaseImplementation").Assign(True).ToStatementSyntax(),
                    IdentifierName("_resultGenerator").Assign(Null).ToStatementSyntax()
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
        var lambdaBody = new BlockBuilder();

        if (method.Parameters.HasOutputParameters)
        {
            lambdaBody.AddStatement(InitializeOutParametersMethodBuilder.Invoke(method));
        }

        lambdaBody.AddStatement(ReturnStatement(IdentifierName(returnsAsync.ValueParameter.Name)));

        var returnsAsyncBodyBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            returnsAsyncBodyBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        returnsAsyncBodyBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(AsyncLambda(method.Symbol.Parameters, lambdaBody.Build()))
                .ToStatementSyntax()
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, returnsAsync.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier(returnsAsync.ValueParameter.Name))
                    .WithType(returnsAsync.ValueParameter.Type)
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
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(
                    AsyncLambda(
                        method.Symbol.Parameters,
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
                Parameter(Identifier(throwsAsync.ExceptionParameter.Name))
                    .WithType(throwsAsync.ExceptionParameter.Type)
            )
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static ExpressionStatementSyntax DisableBaseImplementationStatement() =>
        IdentifierName("_useBaseImplementation").Assign(False).ToStatementSyntax();
}
