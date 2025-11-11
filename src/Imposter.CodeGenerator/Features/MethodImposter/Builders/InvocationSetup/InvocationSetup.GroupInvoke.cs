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
    internal static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        var invocationImposterType = IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName);
        var invocationImposterIdentifier = IdentifierName("invocationImposter");
        var invocationImposterAssignment = LocalVariableDeclarationSyntax(
            invocationImposterType,
            "invocationImposter",
            IdentifierName("GetInvocationImposter").Call());

        var guardMissingImposter = IfStatement(
            BinaryExpression(
                SyntaxKind.EqualsExpression,
                IdentifierName("invocationImposter"),
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
                    IdentifierName("invocationImposter")
                        .Assign(invocationImposterType.Dot(IdentifierName("Default"))).ToStatementSyntax()));

        var invokeCall = invocationImposterIdentifier
            .Dot(IdentifierName("Invoke"))
            .Call(BuildInvokeArgumentList(method));

        var methodDeclaration = new MethodDeclarationBuilder(method.ReturnTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(BuildParameterList(method))
            .WithBody(
                Block(
                    invocationImposterAssignment,
                    guardMissingImposter,
                    method.Symbol.ReturnsVoid
                        ? invokeCall.ToStatementSyntax()
                        : ReturnStatement(invokeCall)
                )
            )
            .Build();

        return methodDeclaration;

        static ParameterListSyntax BuildParameterList(in ImposterTargetMethodMetadata method)
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
                    ParameterSyntax(method.Delegate.Syntax, method.MethodImposter.InvokeMethod.BaseInvocationParameterName)
                        .WithDefault(EqualsValueClause(LiteralExpression(SyntaxKind.NullLiteralExpression))));
            }

            return ParameterList(SeparatedList(parameters));
        }

        static ArgumentListSyntax BuildInvokeArgumentList(in ImposterTargetMethodMetadata method)
        {
            var arguments = new List<ArgumentSyntax>
            {
                Argument(IdentifierName("invocationBehavior")),
                Argument(IdentifierName("methodDisplayName"))
            };

            arguments.AddRange(ArgumentListSyntax(method.Symbol.Parameters).Arguments);
            if (method.SupportsBaseImplementation)
            {
                arguments.Add(Argument(IdentifierName(method.MethodImposter.InvokeMethod.BaseInvocationParameterName)));
            }

            return ArgumentList(SeparatedList(arguments));
        }
    }
}
