using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax BuildFindMatchingInvocationImposterGroupMethod(in ImposterTargetMethodMetadata method)
    {
        var setupIdentifier = Identifier(method.MethodImposter.FindMatchingInvocationImposterGroupMethod.SetupVariableName);
        var setupIdentifierName = IdentifierName(setupIdentifier);

        var findMatchingSetupMethod = new MethodDeclarationBuilder(NullableType(method.InvocationSetup.Syntax), method.MethodImposter.FindMatchingInvocationImposterGroupMethod.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(GetArgumentsParameter(method));

        if (method.Parameters.HasInputParameters)
        {
            return findMatchingSetupMethod
                .WithBody(Block(
                    ForEachStatement(
                        Var,
                        setupIdentifier,
                        IdentifierName(method.MethodImposter.InvocationImpostersField.Name),
                        Block(
                            IfStatement(
                                setupIdentifierName
                                    .Dot(IdentifierName("ArgumentsCriteria"))
                                    .Dot(IdentifierName("Matches"))
                                    .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))),
                                ReturnStatement(setupIdentifierName)
                            )
                        )
                    ),
                    ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression))
                ))
                .Build();
        }

        return findMatchingSetupMethod
            .WithBody(Block(
                IfStatement(
                    IdentifierName(method.MethodImposter.InvocationImpostersField.Name)
                        .Dot(ConcurrentStackSyntaxHelper.TryPeek)
                        .Call(ArgumentListSyntax(OutVarArgument(method.MethodImposter.FindMatchingInvocationImposterGroupMethod.SetupVariableName))
                        ),
                    ReturnStatement(setupIdentifierName),
                    ElseClause(ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression)))
                )
            ))
            .Build();

        static ParameterSyntax? GetArgumentsParameter(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasInputParameters
                ? ParameterSyntax(method.Arguments.Syntax, "arguments")
                : null;
    }
}
