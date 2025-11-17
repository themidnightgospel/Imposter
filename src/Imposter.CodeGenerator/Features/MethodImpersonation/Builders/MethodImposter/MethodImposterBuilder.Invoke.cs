using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax InvokeMethod(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(
            method.ReturnTypeSyntax,
            MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(BuildMethodParameters(method))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(DeclareAndInitializeArgumentsVariable(method))
                    .AddStatement(DeclareMatchingInvocationImposterGroupVariable(method))
                    .AddStatement(EnsureMatchingInvocationImposterGroup(method))
                    .AddStatement(
                        TryStatement(
                            new BlockBuilder()
                                .AddStatement(InvokeMatchingSetup(method))
                                .AddStatement(
                                    AddToInvocationHistoryCollection(method, threwException: false)
                                )
                                .AddStatement(ReturnResultStatement(method))
                                .Build(),
                            SingletonList(
                                CatchClause(
                                    CatchDeclaration(
                                        WellKnownTypes.System.Exception,
                                        Identifier(
                                            method.MethodImposter.InvokeMethod.ExceptionVariableName
                                        )
                                    ),
                                    null,
                                    new BlockBuilder()
                                        .AddStatement(
                                            AddToInvocationHistoryCollection(
                                                method,
                                                threwException: true
                                            )
                                        )
                                        .AddStatement(ThrowStatement())
                                        .Build()
                                )
                            ),
                            default!
                        )
                    )
                    .Build()
            )
            .Build();

    private static ReturnStatementSyntax? ReturnResultStatement(
        in ImposterTargetMethodMetadata method
    )
    {
        return method.HasReturnValue
            ? ReturnStatement(IdentifierName(method.MethodImposter.InvokeMethod.ResultVariableName))
            : null;
    }

    private static LocalDeclarationStatementSyntax? DeclareAndInitializeArgumentsVariable(
        in ImposterTargetMethodMetadata method
    ) =>
        method.Parameters.HasInputParameters
            ? LocalVariableDeclarationSyntax(
                typeSyntax: Var,
                name: method.MethodImposter.InvokeMethod.ArgumentsVariableName,
                initializer: method.Arguments.Syntax.New(
                    method.Parameters.InputParametersAsArgumentListSyntaxWithoutRef
                )
            )
            : null;

    private static ExpressionStatementSyntax AddToInvocationHistoryCollection(
        in ImposterTargetMethodMetadata method,
        bool threwException
    )
    {
        return IdentifierName(method.InvocationHistory.Collection.AsField.Name)
            .Dot(IdentifierName("Add"))
            .Call(
                method
                    .InvocationHistory.Syntax.New(
                        ArgumentListSyntax(GetArguments(method, threwException))
                    )
                    .ToSingleArgumentList()
            )
            .ToStatementSyntax();

        static IReadOnlyList<ArgumentSyntax> GetArguments(
            in ImposterTargetMethodMetadata method,
            bool threwException
        )
        {
            List<ArgumentSyntax> arguments = [];

            if (method.Parameters.HasInputParameters)
            {
                arguments.Add(
                    method.MethodImposter.InvokeMethod.ArgumentsVariableName.ToArgument()
                );
            }

            if (method.HasReturnValue)
            {
                arguments.Add(
                    threwException
                        ? Argument(DefaultNonNullable)
                        : method.MethodImposter.InvokeMethod.ResultVariableName.ToArgument()
                );
            }

            arguments.Add(
                threwException
                    ? method.MethodImposter.InvokeMethod.ExceptionVariableName.ToArgument()
                    : Argument(Default)
            );

            return arguments;
        }
    }

    private static ParameterListSyntax BuildMethodParameters(in ImposterTargetMethodMetadata method)
    {
        var parameterList = method.Parameters.ParameterListSyntax;

        if (method.SupportsBaseImplementation)
        {
            parameterList = parameterList.AddParameters(
                Parameter(
                        Identifier(method.MethodImposter.InvokeMethod.BaseInvocationParameterName)
                    )
                    .WithType(method.Delegate.Syntax.ToNullableType())
                    .WithDefault(EqualsValueClause(Null))
            );
        }

        return parameterList;
    }

    private static StatementSyntax InvokeMatchingSetup(in ImposterTargetMethodMetadata method)
    {
        var invokeArguments = new List<ArgumentSyntax>
        {
            Argument(IdentifierName("_invocationBehavior")),
            Argument(method.DisplayName.StringLiteral()),
        };
        invokeArguments.AddRange(ArgumentListSyntax(method.Symbol.Parameters).Arguments);

        if (method.SupportsBaseImplementation)
        {
            invokeArguments.Add(
                Argument(
                    IdentifierName(method.MethodImposter.InvokeMethod.BaseInvocationParameterName)
                )
            );
        }

        var invokeExpression = IdentifierName(
                method.MethodImposter.InvokeMethod.MatchingInvocationImposterGroupVariableName
            )
            .Dot(IdentifierName("Invoke"))
            .Call(ArgumentList(SeparatedList(invokeArguments)));

        if (method.Symbol.ReturnsVoid)
        {
            return invokeExpression.ToStatementSyntax();
        }

        return LocalVariableDeclarationSyntax(
            Var,
            method.MethodImposter.InvokeMethod.ResultVariableName,
            invokeExpression
        );
    }

    private static LocalDeclarationStatementSyntax DeclareMatchingInvocationImposterGroupVariable(
        in ImposterTargetMethodMetadata method
    ) =>
        LocalVariableDeclarationSyntax(
            Var,
            method.MethodImposter.InvokeMethod.MatchingInvocationImposterGroupVariableName,
            IdentifierName(method.MethodImposter.FindMatchingInvocationImposterGroupMethod.Name)
                .Call(
                    method.Parameters.HasInputParameters
                        ? Argument(
                                IdentifierName(
                                    method.MethodImposter.InvokeMethod.ArgumentsVariableName
                                )
                            )
                            .ToSingleArgumentList()
                        : ArgumentList()
                )
        );

    private static IfStatementSyntax EnsureMatchingInvocationImposterGroup(
        in ImposterTargetMethodMetadata method
    )
    {
        var matchingIdentifier = IdentifierName(
            method.MethodImposter.InvokeMethod.MatchingInvocationImposterGroupVariableName
        );
        var defaultGroup = method.MethodInvocationImposterGroup.Syntax.Dot(
            IdentifierName(method.MethodInvocationImposterGroup.DefaultInvocationSetupField.Name)
        );

        return IfStatement(
            BinaryExpression(SyntaxKind.EqualsExpression, matchingIdentifier, Default),
            Block(
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName("_invocationBehavior"),
                        QualifiedName(
                            WellKnownTypes.Imposter.Abstractions.ImposterMode,
                            IdentifierName("Explicit")
                        )
                    ),
                    Block(
                        ThrowStatement(
                            ObjectCreationExpression(
                                    WellKnownTypes.Imposter.Abstractions.MissingImposterException
                                )
                                .WithArgumentList(
                                    Argument(method.DisplayName.StringLiteral())
                                        .AsSingleArgumentListSyntax()
                                )
                        )
                    )
                ),
                matchingIdentifier.Assign(defaultGroup).ToStatementSyntax()
            )
        );
    }
}
