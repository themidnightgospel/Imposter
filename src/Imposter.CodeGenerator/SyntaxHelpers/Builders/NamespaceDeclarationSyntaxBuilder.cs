using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal readonly struct NamespaceDeclarationSyntaxBuilder(string @namespace)
{
    private readonly List<MemberDeclarationSyntax> _members = [];

    internal NamespaceDeclarationSyntaxBuilder AddMemberIfNotNull(MemberDeclarationSyntax? member)
    {
        if (member is not null)
        {
            AddMember(member);
        }

        return this;
    }

    internal NamespaceDeclarationSyntaxBuilder AddMember(MemberDeclarationSyntax member)
    {
        _members.Add(member);
        return this;
    }

    internal NamespaceDeclarationSyntaxBuilder AddMembers(IEnumerable<MemberDeclarationSyntax> members)
    {
        _members.AddRange(members);
        return this;
    }

    internal NamespaceDeclarationSyntax Build(SyntaxTrivia leadingTrivia = default) =>
        NamespaceDeclaration(
            IdentifierName(@namespace),
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List<UsingDirectiveSyntax>(),
            members: List(_members));
}