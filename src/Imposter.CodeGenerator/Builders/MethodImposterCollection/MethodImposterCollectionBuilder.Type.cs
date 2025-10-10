using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposterCollection;

internal static class MethodImposterCollectionBuilder
{
    internal static ClassDeclarationSyntax Build(ImposterTargetMethodMetadata method)
    {
        var classDeclaration = ClassDeclaration(method.MethodImposter.Collection.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword));

        if (!method.Symbol.IsGenericMethod)
        {
            return classDeclaration;
        }

        return classDeclaration
            .AddMembers(
                BuildImpostersField(method),
                BuildAddNewMethod(method),
                BuildFilterMethod(method)
            );
    }

    private static MemberDeclarationSyntax BuildImpostersField(ImposterTargetMethodMetadata method)
    {
        var concurrentStackType = GenericName(Identifier("ConcurrentStack"))
            .AddTypeArgumentListArguments(method.MethodImposter.Interface.Syntax);

        return FieldDeclaration(
                VariableDeclaration(concurrentStackType)
                    .AddVariables(
                        VariableDeclarator("_imposters")
                            .WithInitializer(
                                EqualsValueClause(
                                    ImplicitObjectCreationExpression()
                                )
                            )
                    )
            )
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));
    }

    private static MemberDeclarationSyntax BuildAddNewMethod(ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters
            .Select(p => TypeParameter(p.Name))
            .ToArray();
        var typeArguments = method.Symbol.TypeParameters
            .Select(p => (TypeSyntax)IdentifierName(p.Name))
            .ToArray();

        return MethodDeclaration(method.MethodImposter.Syntax, "AddNew")
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddTypeParameterListParameters(typeParameters)
            .WithBody(
                Block(
                    LocalDeclarationStatement(
                        VariableDeclaration(IdentifierName("var"))
                            .AddVariables(
                                VariableDeclarator("imposter")
                                    .WithInitializer(
                                        EqualsValueClause(
                                            ObjectCreationExpression(method.MethodImposter.Syntax)
                                                .WithArgumentList(ArgumentList())
                                        )
                                    )
                            )
                    ),
                    ExpressionStatement(
                        InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("_imposters"), IdentifierName("Push"))
                            )
                            .AddArgumentListArguments(
                                Argument(IdentifierName("imposter"))
                            )
                    ),
                    ReturnStatement(IdentifierName("imposter"))
                )
            );
    }

    private static MemberDeclarationSyntax BuildFilterMethod(ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters
            .Select(p => TypeParameter(p.Name))
            .ToArray();
        var typeArguments = method.Symbol.TypeParameters
            .Select(p => (TypeSyntax)IdentifierName(p.Name))
            .ToArray();

        var returnType = GenericName(Identifier("IEnumerable"))
            .AddTypeArgumentListArguments(method.MethodImposter.GenericInterface.Syntax);

        var selectAs = SimpleLambdaExpression(
            Parameter(Identifier("it")),
            InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("it"),
                        GenericName("As").AddTypeArgumentListArguments(typeArguments))
                )
        );

        var whereNotNull = SimpleLambdaExpression(
            Parameter(Identifier("it")),
            IsPatternExpression(
                IdentifierName("it"),
                UnaryPattern(
                    Token(SyntaxKind.NotKeyword),
                    ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression))
                )
            )
        );

        var selectIt = SimpleLambdaExpression(
            Parameter(Identifier("it")),
            PostfixUnaryExpression(
                SyntaxKind.SuppressNullableWarningExpression,
                IdentifierName("it")
            )
        );

        var body = ReturnStatement(
            InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    InvocationExpression(
                        MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("_imposters"), IdentifierName("Select"))
                            ).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(selectAs)))),
                            IdentifierName("Where"))
                    ).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(whereNotNull)))),
                    IdentifierName("Select"))
            ).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(selectIt))))
        );

        return MethodDeclaration(returnType, "Filter")
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddTypeParameterListParameters(typeParameters)
            .WithBody(Block(body));
    }
}