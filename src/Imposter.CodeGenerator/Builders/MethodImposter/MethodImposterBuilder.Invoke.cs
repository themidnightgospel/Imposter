using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax InvokeMethod(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(method.ReturnTypeSyntax, MethodImposterMetadata.InvokeMethodMetadata.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithParameterList(method.Parameters.ParameterListSyntax)
            .WithBody(new BlockBuilder()
                .AddStatement(DeclareAndInitializeArgumentsVariable(method))
                .AddStatement(DeclareMatchingSetupVariable(method))
                .AddStatement(TryStatement(
                        new BlockBuilder()
                            .AddStatement(InvokeMatchingSetup(method))
                            .AddStatement(AddToInvocationHistoryCollection(method, threwException: false))
                            .AddStatement(ReturnResultStatement(method))
                            .Build(),
                        SingletonList(
                            CatchClause(
                                CatchDeclaration(
                                    WellKnownTypes.System.Exception,
                                    Identifier(method.MethodImposter.InvokeMethod.ExceptionVariableName)),
                                null,
                                new BlockBuilder()
                                    .AddStatement(AddToInvocationHistoryCollection(method, threwException: true))
                                    .AddStatement(ThrowStatement())
                                    .Build()
                            )
                        ),
                        null
                    )
                )
                .Build())
            .Build();

    private static StatementSyntax? ReturnResultStatement(in ImposterTargetMethodMetadata method)
    {
        return method.HasReturnValue ? ReturnStatement(IdentifierName(method.MethodImposter.InvokeMethod.ResultVariableName)) : null;
    }

    private static StatementSyntax? DeclareAndInitializeArgumentsVariable(in ImposterTargetMethodMetadata method) =>
        method.Parameters.HasInputParameters
            ? LocalVariableDeclarationSyntax(
                typeSyntax: Var,
                name: method.MethodImposter.InvokeMethod.ArgumentsVariableName,
                initializer: method.Arguments.Syntax.New(method.Parameters.ArgumentListSyntaxWithoutRef))
            : null;

    private static StatementSyntax AddToInvocationHistoryCollection(in ImposterTargetMethodMetadata method, bool threwException)
    {
        return IdentifierName(method.InvocationHistory.Collection.AsField.Name)
            .Dot(IdentifierName("Add"))
            .Call(
                method
                    .InvocationHistory
                    .Syntax
                    .New(ArgumentListSyntax(GetArguments(method, threwException)))
                    .AsSingleArgumentList()
            )
            .AsStatement();

        static IReadOnlyList<ArgumentSyntax> GetArguments(in ImposterTargetMethodMetadata method, bool threwException)
        {
            List<ArgumentSyntax> arguments = [];

            if (method.Parameters.HasInputParameters)
            {
                arguments.Add(method.MethodImposter.InvokeMethod.ArgumentsVariableName.AsArgument());
            }

            if (method.HasReturnValue)
            {
                arguments
                    .Add(threwException ? Argument(Null) : method.MethodImposter.InvokeMethod.ResultVariableName.AsArgument());
            }

            arguments.Add(threwException
                ? method.MethodImposter.InvokeMethod.ExceptionVariableName.AsArgument()
                : Argument(Default));

            return arguments;
        }
    }


    private static StatementSyntax InvokeMatchingSetup(in ImposterTargetMethodMetadata method)
    {
        var invokeExpression = IdentifierName(method.MethodImposter.InvokeMethod.MatchingSetupVariableName)
            .Dot(IdentifierName("Invoke"))
            .Call(ArgumenstListSyntax(method.Symbol.Parameters));

        if (method.Symbol.ReturnsVoid)
        {
            return ExpressionStatement(invokeExpression);
        }

        return LocalDeclarationStatement(VariableDeclarationSyntax(Var, method.MethodImposter.InvokeMethod.ResultVariableName, initializer: invokeExpression));
    }

    private static StatementSyntax DeclareMatchingSetupVariable(in ImposterTargetMethodMetadata method) =>
        LocalVariableDeclarationSyntax(
            Var,
            method.MethodImposter.InvokeMethod.MatchingSetupVariableName,
            IdentifierName(MethodImposterMetadata.FindMatchingSetupMethodMetadata.Name)
                .Call(method.Parameters.HasInputParameters
                    ? Argument(IdentifierName(method.MethodImposter.InvokeMethod.ArgumentsVariableName)).AsSingleArgumentList()
                    : ArgumentList())
                .QuestionMarkQuestionMark(method.InvocationSetup.Syntax.Dot(IdentifierName("DefaultInvocationSetup")))
        );
}