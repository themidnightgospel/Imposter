using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Arguments;

public static class ArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax? Build(ImposterTargetMethodMetadata method)
    {
        var parametersExceptOut = method.ParametersExceptOut;

        if (parametersExceptOut.Count <= 0)
        {
            return null;
        }

        var argumentsCriteriaClass = ClassDeclaration(method.ArgumentsCriteriaType.Name)
            .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(method.Symbol))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithMembers(List<MemberDeclarationSyntax>(parametersExceptOut.Select(SyntaxFactoryHelper.ParameterAsArgProperty)))
            .AddMembers(
                ConstructorDeclaration(method.ArgumentsCriteriaType.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList(
                                parametersExceptOut.Select(SyntaxFactoryHelper.ArgParameter)
                            )
                        )
                    )
                    .WithBody(Block(parametersExceptOut
                        .Select(parameter =>
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        ThisExpression(),
                                        IdentifierName(parameter.Name)
                                    ),
                                    IdentifierName(parameter.Name)
                                )
                            )
                        ))))
            .AddMembers(MatchesMethod(method));

        if (method.Symbol.IsGenericMethod)
        {
            argumentsCriteriaClass = argumentsCriteriaClass.AddMembers(BuildArgumentsCriteriaAsMethod(method));
        }

        return argumentsCriteriaClass
            .WithLeadingTriviaComment(method.DisplayName)
            .WithTrailingTrivia(CarriageReturnLineFeed);
    }

    private static MemberDeclarationSyntax BuildArgumentsCriteriaAsMethod(ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters;
        var asMethodTypeParams = typeParameters.Select(p => TypeParameter(p.Name + "Target")).ToArray();
        var targetTypeArgs = typeParameters.Select(p => IdentifierName(p.Name + "Target")).ToArray();
        var returnType = GenericName(method.ArgumentsCriteriaType.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(targetTypeArgs)));

        var constructorArgs = method.ParametersExceptOut.Select(p =>
        {
            var renamer = new TypeParameterRenamer(typeParameters, "Target");
            var targetType = renamer.Visit(SyntaxFactoryHelper.TypeSyntax(p.Type));
            var sourceType = SyntaxFactoryHelper.TypeSyntax(p.Type);
            var argType = GenericName("Arg").WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList(targetType)));

            var lambdaVarIdentifier = IdentifierName("it");
            var tryCastVarIdentifier = Identifier(p.Name + "Target");

            return Argument(
                InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, argType, IdentifierName("Is")),
                    ArgumentList(SingletonSeparatedList(Argument(
                        SimpleLambdaExpression(
                            Parameter(lambdaVarIdentifier.Identifier),
                            BinaryExpression(
                                SyntaxKind.LogicalAndExpression,
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("TypeCaster"),
                                        GenericName("TryCast").WithTypeArgumentList(
                                            TypeArgumentList(SeparatedList([targetType, sourceType])))),
                                    ArgumentList(SeparatedList([
                                        Argument(lambdaVarIdentifier),
                                        Argument(DeclarationExpression(sourceType, SingleVariableDesignation(tryCastVarIdentifier))).WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword))
                                    ]))),
                                InvocationExpression(
                                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName(p.Name), IdentifierName("Matches")),
                                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName(tryCastVarIdentifier))))))))))
                )
            );
        });

        return MethodDeclaration(returnType, "As")
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithTypeParameterList(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(
                ReturnStatement(
                    ObjectCreationExpression(returnType)
                        .WithArgumentList(ArgumentList(SeparatedList(constructorArgs)))
                )
            ));
    }

    private static MethodDeclarationSyntax MatchesMethod(ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.BoolKeyword)),
                    Identifier("Matches")
                )
                .WithModifiers(
                    TokenList(Token(SyntaxKind.PublicKeyword))
                )
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(Identifier("arguments"))
                                .WithType(method.ArgumentsType.Syntax)
                        )
                    )
                )
                .WithBody(
                    Block(
                        ReturnStatement(
                            method.Symbol.Parameters.Length switch
                            {
                                0 => LiteralExpression(SyntaxKind.TrueLiteralExpression),
                                1 => InvokeMatches(method.Symbol.Parameters[0]),
                                _ => method.Symbol.Parameters
                                    .Skip(1)
                                    .Select(InvokeMatches)
                                    .Aggregate(
                                        (ExpressionSyntax)InvokeMatches(method.Symbol.Parameters[0]),
                                        (left, right) => BinaryExpression(
                                            SyntaxKind.LogicalAndExpression,
                                            left,
                                            right
                                        ))
                            }
                        )
                    )
                )
            ;

        InvocationExpressionSyntax InvokeMatches(IParameterSymbol p)
        {
            return InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(p.Name),
                    IdentifierName("Matches")
                ),
                ArgumentList(SingletonSeparatedList(
                    Argument(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("arguments"),
                            IdentifierName(p.Name)
                        )
                    )
                ))
            );
        }
    }
}