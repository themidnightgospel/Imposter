using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

public static partial class InvocationHistoryBuilder
{
    private static MemberDeclarationSyntax BuildInvocationHistoryCollectionClass(ImposterTargetMethodMetadata method)
    {
        var collectionClassName = $"{method.InvocationHistory.Name}Collection";

        var criteriaGenericName = method.ArgumentsCriteriaType.Syntax as GenericNameSyntax;
        var typeArguments = criteriaGenericName?.TypeArgumentList.Arguments ?? SeparatedList<TypeSyntax>();

        var countMethodTypeParameters = typeArguments
            .OfType<IdentifierNameSyntax>()
            .Select(ins => TypeParameter(ins.Identifier))
            .ToList();

        var countMethod = MethodDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)), "Count")
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .WithParameterList(
                method.HasInputParameters
                    ? ParameterList(SingletonSeparatedList(
                            Parameter(Identifier("argumentsCriteria"))
                                .WithType(method.ArgumentsCriteriaType.Syntax)
                        )
                    )
                    : ParameterList()
            )
            .WithBody(Block(
                ReturnStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("_invocationHistory"),
                            IdentifierName("Count")
                        )
                    ).WithArgumentList(ArgumentList(SingletonSeparatedList(
                        Argument(
                            SimpleLambdaExpression(
                                Parameter(Identifier("it")),
                                InvocationExpression(
                                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("it"), IdentifierName("Matches")),
                                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName("argumentsCriteria"))))
                                )
                            )
                        )
                    )))
                )
            ));

        if (countMethodTypeParameters.Any())
        {
            countMethod = countMethod.WithTypeParameterList(TypeParameterList(SeparatedList(countMethodTypeParameters)));
        }

        var concurrentStackType = GenericName("ConcurrentStack")
            .WithTypeArgumentList(
                TypeArgumentList(
                    SingletonSeparatedList<TypeSyntax>(method.InvocationHistory.Interface.Syntax)));

        return ClassDeclaration(collectionClassName)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddMembers(
                FieldDeclaration(
                        VariableDeclaration(concurrentStackType)
                            .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator(Identifier("_invocationHistory"))
                                        .WithInitializer(
                                            EqualsValueClause(
                                                ObjectCreationExpression(concurrentStackType)
                                                    .WithArgumentList(ArgumentList())
                                            )
                                        )
                                )
                            )
                    )
                    .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))),
                MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), "Add")
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .WithParameterList(ParameterList(SingletonSeparatedList(
                        Parameter(Identifier("invocationHistory")).WithType(method.InvocationHistory.Interface.Syntax)
                    )))
                    .WithBody(Block(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_invocationHistory"),
                                    IdentifierName("Push")
                                )
                            ).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("invocationHistory")))))
                        )
                    )),
                countMethod
            );
    }
}