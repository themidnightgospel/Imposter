using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal readonly struct ClassDeclarationBuilder(string name, TypeParameterListSyntax? typeParameters = default)
{
    private readonly List<MemberDeclarationSyntax> _members = new();
    private readonly List<AttributeListSyntax> _attribute = new();
    private readonly List<BaseTypeSyntax> _baseTypes = new();
    private readonly List<SyntaxToken> _modifiers = new(1);

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
    internal ClassDeclarationBuilder AddMember(MemberDeclarationSyntax? member)
    {
        if (member is not null)
        {
            _members.Add(member);
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddMembers(IEnumerable<MemberDeclarationSyntax>? members)
    {
        if (members is not null)
        {
            _members.AddRange(members);
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddMemberIf(bool condition, Func<MemberDeclarationSyntax> memberGenerator)
    {
        return condition ? AddMember(memberGenerator()) : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddPublicModifier() => AddModifier(Token(SyntaxKind.PublicKeyword));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ClassDeclarationBuilder AddModifier(SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ClassDeclarationSyntax Build(
        SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses = default)
    {
        return ClassDeclaration(
            List(DefaultAttributes.DefaultTypeAttributes.Concat(_attribute)),
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            Identifier(name),
            typeParameters,
            _baseTypes.Count > 0 ? BaseList(SeparatedList(_baseTypes)) : null,
            constraintClauses,
            new SyntaxList<MemberDeclarationSyntax>(_members));
    }
}