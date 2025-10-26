using System.Collections.Generic;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ConstructorDeclarationSyntax BuildConstructorAndInitializeMembers(
        string className,
        IEnumerable<FieldDeclarationSyntax> fields)
    {
        var constructorBuilder = new ConstructorBuilder(className)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));

        var constructorBody = new BlockBuilder();

        foreach (var field in fields)
        {
            var fieldType = field.Declaration.Type;

            foreach (var fieldVariable in field.Declaration.Variables)
            {
                var fieldName = fieldVariable.Identifier.Text;

                constructorBuilder.AddParameter(ParameterSyntax(fieldType, fieldName));
                constructorBody.AddStatement(
                    ThisExpression()
                        .Dot(IdentifierName(fieldName))
                        .Assign(IdentifierName(fieldName))
                        .ToStatementSyntax()
                );
            }
        }

        constructorBuilder.WithBody(constructorBody.Build());

        return constructorBuilder.Build();
    }
}