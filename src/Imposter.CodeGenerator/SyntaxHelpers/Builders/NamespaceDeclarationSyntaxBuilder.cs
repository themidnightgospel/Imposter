using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal readonly struct NamespaceDeclarationSyntaxBuilder(string @namespace)
{
    private readonly List<MemberDeclarationSyntax> _members = [];

    internal NamespaceDeclarationSyntaxBuilder AddMember(MemberDeclarationSyntax member)
    {
        _members.Add(member);
        return this;
    }

    internal NamespaceDeclarationSyntax Build() =>
        NamespaceDeclaration(
            IdentifierName(@namespace),
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List<UsingDirectiveSyntax>(),
            members: List(_members)
        );
}
