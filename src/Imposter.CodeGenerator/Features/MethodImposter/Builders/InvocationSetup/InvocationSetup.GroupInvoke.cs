using System.Collections.Generic;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        var invocationImposterType = IdentifierName(MethodInvocationImposterGroupMetadata.MethodInvocationImposterTypeName);
        var invocationImposterIdentifier = IdentifierName("invocationImposter");
        var invocationImposterAssignment = LocalDeclarationStatement(
            VariableDeclaration(invocationImposterType)
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("invocationImposter"))
                            .WithInitializer(
                                EqualsValueClause(
                                    InvocationExpression(IdentifierName("GetInvocationImposter")))))));

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
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("invocationImposter"),
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            invocationImposterType,
                            IdentifierName("Default"))))));

        var invokeCall = InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                invocationImposterIdentifier,
                IdentifierName("Invoke")),
            BuildInvokeArgumentList(method));

        var methodDeclaration = new MethodDeclarationBuilder(method.ReturnTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(BuildParameterList(method))
            .WithBody(
                Block(
                    invocationImposterAssignment,
                    guardMissingImposter,
                    method.Symbol.ReturnsVoid
                        ? ExpressionStatement(invokeCall)
                        : ReturnStatement(invokeCall)
                )
            )
            .Build();

        return methodDeclaration;

        static ParameterListSyntax BuildParameterList(in ImposterTargetMethodMetadata method)
        {
            var parameters = new List<ParameterSyntax>
            {
                Parameter(Identifier("invocationBehavior"))
                    .WithType(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior),
                Parameter(Identifier("methodDisplayName"))
                    .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
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

        static ArgumentListSyntax BuildInvokeArgumentList(in ImposterTargetMethodMetadata method)
        {
            var arguments = new List<ArgumentSyntax>
            {
                Argument(IdentifierName("invocationBehavior")),
                Argument(IdentifierName("methodDisplayName"))
            };

            arguments.AddRange(SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters).Arguments);
            if (method.SupportsBaseImplementation)
            {
                arguments.Add(Argument(IdentifierName(method.MethodImposter.InvokeMethod.BaseInvocationParameterName)));
            }

            return ArgumentList(SeparatedList(arguments));
        }
    }
}
