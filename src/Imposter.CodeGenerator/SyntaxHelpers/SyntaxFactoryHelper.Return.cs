using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static readonly LiteralExpressionSyntax Default = LiteralExpression(SyntaxKind.DefaultLiteralExpression);

    // TODO delete
    internal static ExpressionSyntax Default2(ITypeSymbol type)
    {
        if (type.SpecialType == SpecialType.System_Void)
        {
            throw new ArgumentException("Type must not be void", nameof(type));
            ;
        }

        if (type is INamedTypeSymbol namedTypeSymbol)
        {
            if (namedTypeSymbol.Name == "Task" && namedTypeSymbol.ContainingNamespace.ToDisplayString() == "System.Threading.Tasks")
            {
                if (namedTypeSymbol.IsGenericType)
                {
                    var genericType = namedTypeSymbol.TypeArguments[0];
                    return InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("Task"),
                            GenericName(
                                Identifier("FromResult"),
                                TypeArgumentList(SingletonSeparatedList(ParseTypeName(genericType.ToDisplayString())))
                            )
                        )
                    ).WithArgumentList(
                        ArgumentListSyntax(
                            SingletonSeparatedList(
                                Argument(DefaultExpression(ParseTypeName(genericType.ToDisplayString())))
                            )
                        )
                    );
                }

                return MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("Task"),
                    IdentifierName("CompletedTask")
                );
            }

            if (namedTypeSymbol.Name == "ValueTask" && namedTypeSymbol.ContainingNamespace.ToDisplayString() == "System.Threading.Tasks")
            {
                if (namedTypeSymbol.IsGenericType)
                {
                    var genericType = namedTypeSymbol.TypeArguments[0];
                    return
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("Task"),
                                GenericName(
                                    Identifier("FromResult"),
                                    TypeArgumentList(SingletonSeparatedList(ParseTypeName(genericType.ToDisplayString())))
                                )
                            )
                        ).WithArgumentList(
                            ArgumentListSyntax(
                                SingletonSeparatedList(
                                    Argument(DefaultExpression(ParseTypeName(genericType.ToDisplayString())))
                                )
                            )
                        );
                }

                return
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("Task"),
                        IdentifierName("CompletedTask")
                    );
            }
        }

        return DefaultExpression(
            ParseTypeName(type.ToDisplayString())
        );
    }

    public static ReturnStatementSyntax ReturnDefault(ITypeSymbol returnTypeSymbol)
    {
        return returnTypeSymbol.SpecialType == SpecialType.System_Void ? ReturnStatement() : ReturnStatement(Default);
    }
}