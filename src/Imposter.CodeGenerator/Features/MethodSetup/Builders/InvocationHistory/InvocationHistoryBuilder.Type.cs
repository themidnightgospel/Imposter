using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var fields = GetFields(method).ToArray();

        return new ClassDeclarationBuilder(method.InvocationHistory.Name, method.GenericTypeParameterListSyntax)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.InvocationHistory.Interface.Syntax))
            .AddMembers(fields)
            .AddMember(BuildConstructor(method.InvocationHistory.Name, fields))
            .AddMember(BuildMatchesMethod(method))
            .Build();

        static ConstructorDeclarationSyntax BuildConstructor(in string className, IReadOnlyList<FieldDeclarationSyntax> fields) =>
            new ConstructorBuilder(className)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .AddParameters(fields
                    .Select(field =>
                        Parameter(Identifier(field.Declaration.Variables[0].Identifier.Text))
                            .WithType(field.Declaration.Type)
                    ))
                .WithBody(Block(fields
                        .Select(field =>
                            ThisExpression()
                                .Dot(IdentifierName(field.Declaration.Variables[0].Identifier.Text))
                                .Assign(IdentifierName(field.Declaration.Variables[0].Identifier.Text))
                                .ToStatementSyntax()
                        )
                    )
                )
                .Build();
    }
}