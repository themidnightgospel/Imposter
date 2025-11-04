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
        return new ClassDeclarationBuilder(InvocationSetupMetadata.MethodInvocationImposterTypeName)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(DefaultInvocationImposterField())
            .AddMember(MethodInvocationImposterStaticConstructor(method))
            .AddMember(ResultGeneratorField(method))
            .AddMember(CallbacksField(method))
            .AddMember(IsEmptyProperty())
            .AddMember(InvokeInvocationMethod(method))
            .AddMember(CallbackMethod(method))
            .AddMember(method.HasReturnValue ? ReturnsDelegateMethod(method) : null)
            .AddMember(method.HasReturnValue ? ReturnsValueMethod(method) : null)
            .AddMember(ThrowsMethod(method))
            .AddMember(InitializeOutParametersMethodBuilder.Build(method))
            .AddMember(DefaultResultGenerator(method))
            .Build();
    }

    private static FieldDeclarationSyntax DefaultInvocationImposterField() =>
        SingleVariableField(
            IdentifierName(InvocationSetupMetadata.MethodInvocationImposterTypeName),
            "Default",
            TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword)));

    private static ConstructorDeclarationSyntax MethodInvocationImposterStaticConstructor(in ImposterTargetMethodMetadata method)
    {
        var body = new BlockBuilder()
            .AddStatement(
                AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("Default"),
                        ObjectCreationExpression(IdentifierName(InvocationSetupMetadata.MethodInvocationImposterTypeName))
                            .WithArgumentList(ArgumentList()))
                    .ToStatementSyntax());

        if (method.HasReturnValue)
        {
            body.AddStatement(
                IdentifierName("Default")
                    .Dot(IdentifierName(method.InvocationSetup.ReturnsMethod.Name))
                    .Call(Argument(IdentifierName(method.InvocationSetup.DefaultResultGeneratorMethod.Name)))
                    .ToStatementSyntax());
        }
        else
        {
            body.AddStatement(
                MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("Default"),
                        IdentifierName("_resultGenerator"))
                    .Assign(IdentifierName(method.InvocationSetup.DefaultResultGeneratorMethod.Name))
                    .ToStatementSyntax());
        }

        return ConstructorDeclaration(Identifier(InvocationSetupMetadata.MethodInvocationImposterTypeName))
            .AddModifiers(Token(SyntaxKind.StaticKeyword))
            .WithBody(body.Build());
    }

    private static FieldDeclarationSyntax ResultGeneratorField(in ImposterTargetMethodMetadata method) =>
        SingleVariableField(
            method.Delegate.Syntax,
            "_resultGenerator",
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

    private static PropertyDeclarationSyntax IsEmptyProperty() =>
        PropertyDeclaration(PredefinedType(Token(SyntaxKind.BoolKeyword)), Identifier("IsEmpty"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .WithExpressionBody(
                ArrowExpressionClause(
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
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))))))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

    private static MethodDeclarationSyntax InvokeInvocationMethod(in ImposterTargetMethodMetadata method)
    {
        var blockBuilder = new BlockBuilder()
            .AddStatement(
                AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("_resultGenerator"),
                        BinaryExpression(
                            SyntaxKind.CoalesceExpression,
                            IdentifierName("_resultGenerator"),
                            IdentifierName(method.InvocationSetup.DefaultResultGeneratorMethod.Name)))
                    .ToStatementSyntax());

        var invokeExpression = IdentifierName("_resultGenerator")
            .Dot(IdentifierName("Invoke"))
            .Call(SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters));

        if (method.Symbol.ReturnsVoid)
        {
            blockBuilder.AddStatement(ExpressionStatement(invokeExpression));
        }
        else
        {
            blockBuilder.AddStatement(
                LocalDeclarationStatement(
                    VariableDeclaration(method.ReturnTypeSyntax)
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("result"))
                                    .WithInitializer(EqualsValueClause(invokeExpression))))));
        }

        blockBuilder.AddStatement(
            ForEachStatement(
                IdentifierName("var"),
                Identifier("callback"),
                IdentifierName("_callbacks"),
                Block(
                    ExpressionStatement(
                        IdentifierName("callback")
                            .Call(SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters))))));

        if (!method.Symbol.ReturnsVoid)
        {
            blockBuilder.AddStatement(ReturnStatement(IdentifierName("result")));
        }

        return MethodDeclaration(method.ReturnTypeSyntax, "Invoke")
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(method.Parameters.ParameterListSyntax)
            .WithBody(blockBuilder.Build());
    }

    private static MethodDeclarationSyntax CallbackMethod(in ImposterTargetMethodMetadata method) =>
        MethodDeclaration(WellKnownTypes.Void, method.InvocationSetup.CallbackMethod.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(Parameter(Identifier(method.InvocationSetup.CallbackMethod.CallbackParameter.Name)).WithType(method.CallbackDelegate.Syntax))
            .WithBody(
                Block(
                    IdentifierName("_callbacks")
                        .Dot(IdentifierName("Enqueue"))
                        .Call(Argument(IdentifierName(method.InvocationSetup.CallbackMethod.CallbackParameter.Name)))
                        .ToStatementSyntax()));

    private static MethodDeclarationSyntax ReturnsDelegateMethod(in ImposterTargetMethodMetadata method) =>
        MethodDeclaration(WellKnownTypes.Void, method.InvocationSetup.ReturnsMethod.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(Parameter(Identifier(method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter.Name)).WithType(method.Delegate.Syntax))
            .WithBody(
                Block(
                    IdentifierName("_resultGenerator")
                        .Assign(IdentifierName(method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter.Name))
                        .ToStatementSyntax()));

    private static MethodDeclarationSyntax ReturnsValueMethod(in ImposterTargetMethodMetadata method)
    {
        var lambdaBody = new BlockBuilder();

        if (method.Parameters.HasOutputParameters)
        {
            lambdaBody.AddStatement(InitializeOutParametersMethodBuilder.Invoke(method));
        }

        lambdaBody.AddStatement(ReturnStatement(IdentifierName(method.InvocationSetup.ReturnsMethod.ValueParameter.Name)));

        return MethodDeclaration(WellKnownTypes.Void, method.InvocationSetup.ReturnsMethod.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(Parameter(Identifier(method.InvocationSetup.ReturnsMethod.ValueParameter.Name)).WithType(method.ReturnTypeSyntax))
            .WithBody(
                Block(
                    IdentifierName("_resultGenerator")
                        .Assign(SyntaxFactoryHelper.Lambda(method.Symbol.Parameters, lambdaBody.Build()))
                        .ToStatementSyntax()));
    }

    private static MethodDeclarationSyntax ThrowsMethod(in ImposterTargetMethodMetadata method)
    {
        var throwsParameter = method.InvocationSetup.ThrowsMethod.ExceptionGeneratorParameter;

        return MethodDeclaration(WellKnownTypes.Void, method.InvocationSetup.ThrowsMethod.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(Parameter(Identifier(throwsParameter.Name)).WithType(throwsParameter.Type))
            .WithBody(
                Block(
                    IdentifierName("_resultGenerator")
                        .Assign(
                            SyntaxFactoryHelper.Lambda(
                                method.Symbol.Parameters,
                                Block(
                                    ThrowStatement(
                                        IdentifierName(throwsParameter.Name)
                                            .Call(SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters))))))
                        .ToStatementSyntax()));
    }
}
