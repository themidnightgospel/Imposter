using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal class InterfaceDeclarationBuilder(string name, TypeParameterListSyntax? typeParameters = null)
{
    private readonly List<MemberDeclarationSyntax> _members = new();
    private readonly List<AttributeListSyntax> _attributes = new();
    private readonly List<BaseTypeSyntax> _baseTypes = new();
    private readonly List<SyntaxToken> _modifiers = new(1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal InterfaceDeclarationBuilder AddBaseType(BaseTypeSyntax baseType)
    {
        _baseTypes.Add(baseType);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal InterfaceDeclarationBuilder AddModifier(SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal InterfaceDeclarationBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal InterfaceDeclarationBuilder AddMember(MemberDeclarationSyntax? member)
    {
        if (member is not null)
        {
            _members.Add(member);
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal InterfaceDeclarationBuilder AddMembers(IEnumerable<MemberDeclarationSyntax>? members)
    {
        if (members is not null)
        {
            _members.AddRange(members);
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal InterfaceDeclarationBuilder AddMemberIf(bool condition, Func<MemberDeclarationSyntax> memberGenerator)
    {
        return condition ? AddMember(memberGenerator()) : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public InterfaceDeclarationSyntax Build(
        SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses = default)
    {
        return InterfaceDeclaration(
            List(DefaultAttributes.DefaultTypeAttributes.Concat(_attributes)),
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            Identifier(name),
            typeParameters,
            _baseTypes.Count > 0 ? BaseList(SeparatedList(_baseTypes)) : null,
            constraintClauses,
            List(_members));
    }
}