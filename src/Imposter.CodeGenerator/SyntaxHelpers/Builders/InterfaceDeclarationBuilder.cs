using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal class InterfaceDeclarationBuilder(string name, TypeParameterListSyntax? typeParameters = null)
{
    private readonly List<MemberDeclarationSyntax> _members = [];
    private readonly List<AttributeListSyntax> _attributes = [];
    private readonly List<BaseTypeSyntax> _baseTypes = [];
    private readonly List<SyntaxToken> _modifiers = new(1);

    internal InterfaceDeclarationBuilder AddBaseType(BaseTypeSyntax baseType)
    {
        _baseTypes.Add(baseType);
        return this;
    }

    internal InterfaceDeclarationBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    internal InterfaceDeclarationBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    internal InterfaceDeclarationBuilder AddMember(MemberDeclarationSyntax? member)
    {
        if (member is not null)
        {
            _members.Add(member);
        }

        return this;
    }

    internal InterfaceDeclarationBuilder AddMembers(IEnumerable<MemberDeclarationSyntax>? members)
    {
        if (members is not null)
        {
            _members.AddRange(members);
        }

        return this;
    }

    internal InterfaceDeclarationBuilder AddMemberIf(bool condition, Func<MemberDeclarationSyntax> memberGenerator)
    {
        return condition ? AddMember(memberGenerator()) : this;
    }

    public InterfaceDeclarationSyntax Build()
    {
        return InterfaceDeclaration(
            List(DefaultAttributes.DefaultTypeAttributes.Concat(_attributes)),
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            Identifier(name),
            typeParameters,
            _baseTypes.Count > 0 ? BaseList(SeparatedList(_baseTypes)) : null,
            default,
            List(_members));
    }
}
