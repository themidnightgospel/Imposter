using System.Collections.Generic;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static ClassDeclarationSyntax MethodInvocationImposterType(in ImposterTargetMethodMetadata method)
    {
        var classBuilder = new ClassDeclarationBuilder(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName)
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
            .AddMember(method.MethodInvocationImposterGroup.ReturnsAsyncMethod.HasValue ? ReturnsAsyncMethod(method) : null)
            .AddMember(ThrowsMethod(method))
            .AddMember(method.MethodInvocationImposterGroup.ThrowsAsyncMethod.HasValue ? ThrowsAsyncMethod(method) : null)
            .AddMember(method.SupportsBaseImplementation ? UseBaseImplementationMethod() : null)
            .AddMember(InitializeOutParametersMethodBuilder.Build(method))
            .AddMember(DefaultResultGenerator(method));

        return classBuilder.Build();
    }

    private static FieldDeclarationSyntax DefaultInvocationImposterField() =>
        SingleVariableField(
            IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName),
            "Default",
            TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword)));

    private static ConstructorDeclarationSyntax MethodInvocationImposterStaticConstructor(in ImposterTargetMethodMetadata method)
    {
        var body = new BlockBuilder()
            .AddStatement(
                AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("Default"),
                        ObjectCreationExpression(IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName))
                            .WithArgumentList(ArgumentList()))
                    .ToStatementSyntax());

        if (method.HasReturnValue)
        {
            body.AddStatement(
                IdentifierName("Default")
                    .Dot(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.Name))
                    .Call(Argument(IdentifierName(method.MethodInvocationImposterGroup.DefaultResultGeneratorMethod.Name)))
                    .ToStatementSyntax());
        }
        else
        {
            body.AddStatement(
                MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("Default"),
                        IdentifierName("_resultGenerator"))
                    .Assign(IdentifierName(method.MethodInvocationImposterGroup.DefaultResultGeneratorMethod.Name))
                    .ToStatementSyntax());
        }

        return new ConstructorBuilder(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName)
            .WithModifiers(TokenList(Token(SyntaxKind.StaticKeyword)))
            .WithBody(body.Build())
            .Build();
    }

    private static FieldDeclarationSyntax ResultGeneratorField(in ImposterTargetMethodMetadata method) =>
        SingleVariableField(
            method.Delegate.Syntax,
            "_resultGenerator",
            TokenList(Token(SyntaxKind.PrivateKeyword)));

    private static FieldDeclarationSyntax UseBaseImplementationField() =>
        SingleVariableField(
            PredefinedType(Token(SyntaxKind.BoolKeyword)),
            "_useBaseImplementation",
            TokenList(Token(SyntaxKind.PrivateKeyword)));

    private static FieldDeclarationSyntax CallbacksField(in ImposterTargetMethodMetadata method) =>
        FieldDeclaration(
                VariableDeclaration(WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(method.CallbackDelegate.Syntax))
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier("_callbacks"))
                                .WithInitializer(
                                    EqualsValueClause(
                                        ObjectCreationExpression(WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(method.CallbackDelegate.Syntax))
                                            .WithArgumentList(ArgumentList()))))))
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

    private static PropertyDeclarationSyntax IsEmptyProperty(bool supportsBaseImplementation)
    {
        ExpressionSyntax condition =
            BinaryExpression(
                SyntaxKind.LogicalAndExpression,
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    IdentifierName("_resultGenerator"),
                    LiteralExpression(SyntaxKind.NullLiteralExpression)),
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("_callbacks"),
                        IdentifierName("Count")),
                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))));

        if (supportsBaseImplementation)
        {
            condition = BinaryExpression(
                SyntaxKind.LogicalAndExpression,
                PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, IdentifierName("_useBaseImplementation")),
                condition);
        }

        return new PropertyDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "IsEmpty")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(ArrowExpressionClause(condition))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    private static MethodDeclarationSyntax InvokeInvocationMethod(in ImposterTargetMethodMetadata method)
    {
        var parameterList = BuildInvocationParameterList(method);
        var arguments = ArgumentListSyntax(method.Symbol.Parameters);
        var resultInvocation = IdentifierName("_resultGenerator")
            .Dot(IdentifierName("Invoke"))
            .Call(arguments);

        var defaultBlockBuilder = new BlockBuilder()
            .AddStatement(
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName("_resultGenerator"),
                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                    Block(
                        IfStatement(
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("invocationBehavior"),
                                QualifiedName(
                                    WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior,
                                    IdentifierName("Explicit"))),
                            Block(
                                ThrowStatement(
                                    ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                                        .WithArgumentList(
                                            Argument(IdentifierName("methodDisplayName"))
                                                .AsSingleArgumentListSyntax())))),
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("_resultGenerator"),
                                IdentifierName(method.MethodInvocationImposterGroup.DefaultResultGeneratorMethod.Name))))));

        if (method.Symbol.ReturnsVoid)
        {
            defaultBlockBuilder.AddStatement(ExpressionStatement(resultInvocation));
        }
        else
        {
            defaultBlockBuilder.AddStatement(
                LocalDeclarationStatement(
                    VariableDeclaration(method.ReturnTypeSyntax)
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("result"))
                                    .WithInitializer(EqualsValueClause(resultInvocation))))));
        }

        var callbackInvocation = ForEachStatement(
            IdentifierName("var"),
            Identifier("callback"),
            IdentifierName("_callbacks"),
            Block(
                ExpressionStatement(
                    IdentifierName("callback")
                        .Call(arguments))));

        defaultBlockBuilder.AddStatement(callbackInvocation);

        if (!method.Symbol.ReturnsVoid)
        {
            defaultBlockBuilder.AddStatement(ReturnStatement(IdentifierName("result")));
        }

        var defaultBlock = defaultBlockBuilder.Build();
        BlockSyntax body;

        if (method.SupportsBaseImplementation)
        {
            var missingImposterException = ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                .WithArgumentList(
                    Argument(IdentifierName("methodDisplayName"))
                        .AsSingleArgumentListSyntax());

            var assignBaseImplementation = IfStatement(
                IdentifierName("_useBaseImplementation"),
                Block(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("_resultGenerator"),
                            BinaryExpression(
                                SyntaxKind.CoalesceExpression,
                                IdentifierName(method.MethodImposter.InvokeMethod.BaseInvocationParameterName),
                                ThrowExpression(missingImposterException))))));

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
    
    private static ParameterListSyntax BuildInvocationParameterList(in ImposterTargetMethodMetadata method)
    {
        var parameters = new List<ParameterSyntax>
        {
            ParameterSyntax(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior, "invocationBehavior"),
            ParameterSyntax(PredefinedType(Token(SyntaxKind.StringKeyword)), "methodDisplayName")
        };

        parameters.AddRange(method.Parameters.ParameterListSyntax.Parameters);
        if (method.SupportsBaseImplementation)
        {
            parameters.Add(
                Parameter(Identifier(method.MethodImposter.InvokeMethod.BaseInvocationParameterName))
                    .WithType(method.Delegate.Syntax)
                    .WithDefault(EqualsValueClause(LiteralExpression(SyntaxKind.NullLiteralExpression))));
        }

        return ParameterList(SeparatedList(parameters));
    }

    private static MethodDeclarationSyntax CallbackMethod(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, method.MethodInvocationImposterGroup.CallbackMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.CallbackDelegate.Syntax, method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter.Name))
            .WithBody(
                Block(
                    IdentifierName("_callbacks")
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(IdentifierName(method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter.Name)))
                        .ToStatementSyntax()))
            .Build();

    private static MethodDeclarationSyntax ReturnsDelegateMethod(in ImposterTargetMethodMetadata method)
    {
        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter.Name))
                .ToStatementSyntax());

        return new MethodDeclarationBuilder(WellKnownTypes.Void, method.MethodInvocationImposterGroup.ReturnsMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(Parameter(Identifier(method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter.Name)).WithType(method.Delegate.Syntax))
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax ReturnsValueMethod(in ImposterTargetMethodMetadata method)
    {
        var lambdaBody = new BlockBuilder();

        if (method.Parameters.HasOutputParameters)
        {
            lambdaBody.AddStatement(InitializeOutParametersMethodBuilder.Invoke(method));
        }

        lambdaBody.AddStatement(ReturnStatement(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter.Name)));

        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(Lambda(method.Symbol.Parameters, lambdaBody.Build()))
                .ToStatementSyntax());

        return new MethodDeclarationBuilder(WellKnownTypes.Void, method.MethodInvocationImposterGroup.ReturnsMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(Parameter(Identifier(method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter.Name)).WithType(method.ReturnTypeSyntax))
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax ThrowsMethod(in ImposterTargetMethodMetadata method)
    {
        var throwsParameter = method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionGeneratorParameter;

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
                                    .Call(ArgumentListSyntax(method.Symbol.Parameters))))))
                .ToStatementSyntax());

        return new MethodDeclarationBuilder(WellKnownTypes.Void, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(Parameter(Identifier(throwsParameter.Name)).WithType(throwsParameter.Type))
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax UseBaseImplementationMethod() =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "UseBaseImplementation")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("_useBaseImplementation"),
                            LiteralExpression(SyntaxKind.TrueLiteralExpression))
                        .ToStatementSyntax(),
                    AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("_resultGenerator"),
                            LiteralExpression(SyntaxKind.NullLiteralExpression))
                        .ToStatementSyntax()))
            .Build();

    private static MethodDeclarationSyntax ReturnsAsyncMethod(in ImposterTargetMethodMetadata method)
    {
        var returnsAsync = method.MethodInvocationImposterGroup.ReturnsAsyncMethod!.Value;
        var lambdaBody = new BlockBuilder();

        if (method.Parameters.HasOutputParameters)
        {
            lambdaBody.AddStatement(InitializeOutParametersMethodBuilder.Invoke(method));
        }

        lambdaBody.AddStatement(
            ReturnStatement(
                BuildAsyncReturnExpression(method, IdentifierName(returnsAsync.ValueParameter.Name))));

        var blockBuilder = new BlockBuilder();

        if (method.SupportsBaseImplementation)
        {
            blockBuilder.AddStatement(DisableBaseImplementationStatement());
        }

        blockBuilder.AddStatement(
            IdentifierName("_resultGenerator")
                .Assign(Lambda(method.Symbol.Parameters, lambdaBody.Build()))
                .ToStatementSyntax());

        return new MethodDeclarationBuilder(WellKnownTypes.Void, returnsAsync.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier(returnsAsync.ValueParameter.Name)).WithType(returnsAsync.ValueParameter.Type))
            .WithBody(blockBuilder.Build())
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
                    Lambda(
                        method.Symbol.Parameters,
                        Block(
                            ReturnStatement(
                                BuildFaultedAsyncReturnExpression(
                                    method,
                                    IdentifierName(throwsAsync.ExceptionParameter.Name))))))
                .ToStatementSyntax());

        return new MethodDeclarationBuilder(WellKnownTypes.Void, throwsAsync.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier(throwsAsync.ExceptionParameter.Name)).WithType(throwsAsync.ExceptionParameter.Type))
            .WithBody(blockBuilder.Build())
            .Build();
    }

    private static ExpressionStatementSyntax DisableBaseImplementationStatement() =>
        AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName("_useBaseImplementation"),
                LiteralExpression(SyntaxKind.FalseLiteralExpression))
            .ToStatementSyntax();

    private static ExpressionSyntax BuildAsyncReturnExpression(in ImposterTargetMethodMetadata method, ExpressionSyntax valueExpression)
    {
        if (method.ReturnType.IsGenericTask)
        {
            return WellKnownTypes.System.Threading.Tasks.Task
                .Dot(IdentifierName("FromResult"))
                .Call(Argument(valueExpression).AsSingleArgumentListSyntax());
        }

        return ObjectCreationExpression(
                WellKnownTypes.System.Threading.Tasks.ValueTaskOfT(method.ReturnType.AsyncValueTypeSyntax!))
            .WithArgumentList(Argument(valueExpression).AsSingleArgumentListSyntax());
    }

    private static ExpressionSyntax BuildFaultedAsyncReturnExpression(in ImposterTargetMethodMetadata method, ExpressionSyntax exceptionExpression)
    {
        var faultedTaskExpression = BuildTaskFromExceptionExpression(method, exceptionExpression);

        if (method.ReturnType.IsValueTask)
        {
            var valueTaskType = method.ReturnType.IsGenericValueTask
                ? WellKnownTypes.System.Threading.Tasks.ValueTaskOfT(method.ReturnType.AsyncValueTypeSyntax!)
                : WellKnownTypes.System.Threading.Tasks.ValueTask;

            return ObjectCreationExpression(valueTaskType)
                .WithArgumentList(Argument(faultedTaskExpression).AsSingleArgumentListSyntax());
        }

        return faultedTaskExpression;
    }

    private static InvocationExpressionSyntax BuildTaskFromExceptionExpression(in ImposterTargetMethodMetadata method, ExpressionSyntax exceptionExpression)
    {
        SimpleNameSyntax fromExceptionIdentifier = method.ReturnType.IsGenericTask || method.ReturnType.IsGenericValueTask
            ? GenericName(Identifier("FromException"))
                .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList(method.ReturnType.AsyncValueTypeSyntax!)))
            : IdentifierName("FromException");

        return WellKnownTypes.System.Threading.Tasks.Task
            .Dot(fromExceptionIdentifier)
            .Call(Argument(exceptionExpression).AsSingleArgumentListSyntax());
    }
}
