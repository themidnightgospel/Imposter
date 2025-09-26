using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ConstructorDeclarationSyntax DeclareConstructorAndInitializeMembers(
        string className,
        IEnumerable<FieldDeclarationSyntax> fields)
    {
        // TODO optimize
        return ConstructorDeclaration(className)
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(fields
                .SelectMany(field =>
                    field.Declaration.Variables.Select(v =>
                        Parameter(
                                Identifier(v.Identifier.Text))
                            .WithType(field.Declaration.Type))
                ).ToArray())
            .WithBody(Block(SyntaxFactory.List(
                fields
                    .SelectMany(field =>
                        field.Declaration.Variables.Select(v =>
                            ExpressionStatement(AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    ThisExpression(),
                                    IdentifierName(v.Identifier.Text)
                                ),
                                IdentifierName(v.Identifier.Text)
                            )))
                    ).ToArray<StatementSyntax>())));
    }
}