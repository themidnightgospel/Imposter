using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    internal static FieldDeclarationSyntax DefaultInstanceLazyInitializer(ImposterTargetMethodMetadata method)
    {
        return FieldDeclaration(
                VariableDeclaration(method.InvocationSetupType.Syntax)
                    .AddVariables(
                        VariableDeclarator(Identifier("DefaultInvocationSetup"))
                            .WithInitializer(
                                EqualsValueClause(
                                    ObjectCreationExpression(method.InvocationSetupType.Syntax)
                                        .WithArgumentList(
                                            method.ParametersExceptOut.Count > 0
                                                ? ArgumentList(
                                                    SingletonSeparatedList(
                                                        Argument(
                                                            ObjectCreationExpression(method.ArgumentsCriteriaType.Syntax)
                                                                .WithArgumentList(SyntaxFactoryHelper.ArgAnyArgumentList(method.Symbol.Parameters))
                                                        )
                                                    )
                                                )
                                                : ArgumentList()
                                        )
                                )
                            )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.InternalKeyword),
                Token(SyntaxKind.StaticKeyword)
            );
    }

    internal static MethodDeclarationSyntax DefaultResultGenerator(ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                Identifier("DefaultResultGenerator")
            )
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.InternalKeyword),
                    Token(SyntaxKind.StaticKeyword)
                )
            )
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters))
            .WithBody(
                new BlockBuilder()
                    .AddStatementsIf(method.HasOutParameters, () => SyntaxFactoryHelper.InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters))
                    .AddStatementsIf(method.HasReturnValue, () => SyntaxFactoryHelper.ReturnDefault(method.Symbol.ReturnType))
                    .Build()
            );
    }
}