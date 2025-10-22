using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static MethodDeclarationSyntax ThrowsTExceptionMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                method.InvocationSetup.Interface.Syntax,
                Identifier(method.InvocationSetup.ThrowsMethod.Name)
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
            .WithTypeParameterList(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
            .WithBody(Block(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                        Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatement(InvokeInitializeOutParametersWithDefaultValues(method))
                                .AddStatement(ThrowStatement(ObjectCreationExpression(IdentifierName("TException"), ArgumentList(), null)))
                                .Build()
                        )
                    )),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MethodDeclarationSyntax ThrowsExceptionInstanceMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                method.InvocationSetup.Interface.Syntax,
                Identifier(method.InvocationSetup.ThrowsMethod.Name)
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
            .WithParameterList(ParameterSyntax(method.InvocationSetup.ThrowsMethod.ExceptionParameter).ToSingleParameterListSyntax())
            .WithBody(Block(ExpressionStatement(
                    GetMethodCallSetupAccessExpressionSyntax("ResultGenerator")
                        .Assign(Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatement(InvokeInitializeOutParametersWithDefaultValues(method))
                                .AddStatement(ThrowStatement(IdentifierName(method.InvocationSetup.ThrowsMethod.ExceptionParameter.Name)))
                                .Build())
                        )
                ),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MethodDeclarationSyntax ThrowsExceptionWithGeneratorMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                method.InvocationSetup.Interface.Syntax,
                Identifier(method.InvocationSetup.ThrowsMethod.Name)
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
            .AddParameterListParameters(
                ParameterSyntax(method.InvocationSetup.ThrowsMethod.ExceptionGeneratorParameter)
            )
            .WithBody(
                Block(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                            Lambda(
                                method.Symbol.Parameters,
                                Block(
                                    ThrowStatement(
                                        InvocationExpression(
                                            IdentifierName(method.InvocationSetup.ThrowsMethod.ExceptionGeneratorParameter.Name),
                                            ArgumenstListSyntax(method.Symbol.Parameters)
                                        )
                                    ))
                            )
                        )),
                    ReturnStatement(ThisExpression())
                ));
    }
}