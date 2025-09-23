using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static FieldDeclarationSyntax DefaultInstanceLazyInitializer(ImposterTargetMethod method)
    {
        var fieldtype = GenericName(
            Identifier("Lazy"),
            TypeArgumentList(
                SingletonSeparatedList<TypeSyntax>(IdentifierName(method.InvocationsSetupBuilder))
            )
        );

        return FieldDeclaration(
                VariableDeclaration(fieldtype)
                    .AddVariables(
                        VariableDeclarator(Identifier("DefaultInvocationSetup"))
                            .WithInitializer(
                                EqualsValueClause(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            ObjectCreationExpression(fieldtype)
                                                .WithArgumentList(SyntaxFactoryHelper.ArgAnyArgumentList(method.Symbol.Parameters)),
                                            IdentifierName("Returns")
                                        ),
                                        ArgumentList(
                                            SingletonSeparatedList(
                                                Argument(IdentifierName("DefaultResultGenerator"))))
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