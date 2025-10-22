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
                    field
                        .Declaration
                        .Variables
                        .Select(it =>
                            Parameter(Identifier(it.Identifier.Text))
                                .WithType(field.Declaration.Type))
                ).ToArray())
            .WithBody(Block(SyntaxFactory.List(
                fields
                    .SelectMany(field =>
                        field.Declaration.Variables
                            .Select(v =>
                                ThisExpression()
                                    .Dot(IdentifierName(v.Identifier.Text))
                                    .Assign(IdentifierName(v.Identifier.Text))
                                    .ToStatementSyntax()
                            ))
            ).ToArray<StatementSyntax>()));
    }
}