using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Helpers.SyntaxBuilders;

internal class ClassDeclarationBuilder(string name)
{
    private readonly List<MemberDeclarationSyntax> _members = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddMember(MemberDeclarationSyntax member)
    {
        _members.Add(member);
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddMembers(IEnumerable<MemberDeclarationSyntax> members)
    {
        _members.AddRange(members);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddMemberIf(bool condition, Func<MemberDeclarationSyntax> memberGenerator)
    {
        return condition ? AddMember(memberGenerator()) : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ClassDeclarationSyntax Build(
        SyntaxList<AttributeListSyntax> attributeLists = default,
        SyntaxTokenList modifiers = default,
        TypeParameterListSyntax? typeParameterList = default,
        BaseListSyntax? baseList = default,
        SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses = default,
        SyntaxList<MemberDeclarationSyntax> members = default)
    {
        
        return ClassDeclaration(
            attributeLists,
            modifiers,
            Identifier(name),
            typeParameterList,
            baseList,
            constraintClauses,
            new SyntaxList<MemberDeclarationSyntax>(_members));
    }
}