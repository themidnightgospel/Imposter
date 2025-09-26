using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal class ClassDeclarationBuilder(string name)
{
    private readonly List<MemberDeclarationSyntax> _members = new();
    private readonly List<AttributeListSyntax> _attribute = new();
    private readonly List<BaseTypeSyntax> _baseTypes = new();

    internal IReadOnlyList<MemberDeclarationSyntax> Members => _members;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddBaseType(BaseTypeSyntax baseType)
    {
        _baseTypes.Add(baseType);
        return this;
    }

    internal ClassDeclarationBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attribute.Add(attribute);
        return this;
    }

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
        SyntaxTokenList modifiers = default,
        TypeParameterListSyntax? typeParameterList = default,
        SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses = default)
    {
        return ClassDeclaration(
            List(Defaults.DefaultTypeAttributes.Concat(_attribute)),
            modifiers,
            Identifier(name),
            typeParameterList,
            _baseTypes.Count > 0 ? BaseList(SeparatedList(_baseTypes)) : null,
            constraintClauses,
            new SyntaxList<MemberDeclarationSyntax>(_members));
    }
}