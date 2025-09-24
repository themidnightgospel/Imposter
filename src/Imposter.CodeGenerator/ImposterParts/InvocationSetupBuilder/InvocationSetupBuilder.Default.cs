using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetupBuilder;

internal static partial class InvocationSetupBuilder
{
    internal static FieldDeclarationSyntax DefaultInstanceLazyInitializer(ImposterTargetMethod method)
    {
        var fieldtype = IdentifierName(method.InvocationsSetupBuilder);

        return FieldDeclaration(
                VariableDeclaration(fieldtype)
                    .AddVariables(
                        VariableDeclarator(Identifier("DefaultInvocationSetup"))
                            .WithInitializer(
                                EqualsValueClause(
                                    ObjectCreationExpression(fieldtype)
                                        .WithArgumentList(
                                            method.ParametersExceptOut.Count > 0
                                                ? ArgumentList(
                                                    SingletonSeparatedList(
                                                        Argument(
                                                            ObjectCreationExpression(IdentifierName(method.ArgumentsCriteriaClassName))
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

    internal static MethodDeclarationSyntax DefaultResultGenerator(ImposterTargetMethod method)
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