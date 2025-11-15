using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal readonly struct CompilationUnitSyntaxBuilder
{
    private readonly List<MemberDeclarationSyntax> _members = [];

    public CompilationUnitSyntaxBuilder() { }

    internal CompilationUnitSyntaxBuilder AddMember(MemberDeclarationSyntax member)
    {
        _members.Add(member);
        return this;
    }

    internal CompilationUnitSyntax Build(IEnumerable<UsingDirectiveSyntax> usings)
    {
        return SyntaxFactory.CompilationUnit(
            externs: SyntaxFactory.List<ExternAliasDirectiveSyntax>(),
            usings: SyntaxFactory.List(usings ?? []),
            attributeLists: SyntaxFactory.List<AttributeListSyntax>(),
            members: SyntaxFactory.List(_members)
        );
    }
}
