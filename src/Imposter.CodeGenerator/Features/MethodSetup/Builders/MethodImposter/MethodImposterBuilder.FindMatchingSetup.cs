using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax BuildFindMatchingSetupMethod(in ImposterTargetMethodMetadata method)
    {
        var findMatchingSetupMethod = new MethodDeclarationBuilder(NullableType(method.InvocationSetup.Syntax), "FindMatchingSetup")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(GetArgumentsParameter(method));

        if (method.Parameters.HasInputParameters)
        {
            return findMatchingSetupMethod
                .WithBody(Block(
                    ForEachStatement(
                        Var,
                        Identifier("setup"),
                        IdentifierName(method.MethodImposter.InvocationSetupsField.Name),
                        Block(
                            IfStatement(
                                IdentifierName("setup")
                                    .Dot(IdentifierName("ArgumentsCriteria"))
                                    .Dot(IdentifierName("Matches"))
                                    .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))),
                                ReturnStatement(IdentifierName("setup"))
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
                    IdentifierName(method.MethodImposter.InvocationSetupsField.Name)
                        .Dot(ConcurrentStackSyntaxHelper.TryPeek)
                        .Call(ArgumentListSyntax(OutVarArgument("setup"))
                        ),
                    ReturnStatement(IdentifierName("setup")),
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