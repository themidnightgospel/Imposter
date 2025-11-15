using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal readonly struct ClassDeclarationBuilder(
    string name,
    TypeParameterListSyntax? typeParameters = default
)
{
    private readonly List<MemberDeclarationSyntax> _members = [];
    private readonly List<AttributeListSyntax> _attribute = [];
    private readonly List<BaseTypeSyntax> _baseTypes = [];
    private readonly List<SyntaxToken> _modifiers = new(1);

    internal IReadOnlyList<MemberDeclarationSyntax> Members => _members;

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

    internal ClassDeclarationBuilder AddMember(MemberDeclarationSyntax? member)
    {
        if (member is not null)
        {
            _members.Add(member);
        }
        return this;
    }

    internal ClassDeclarationBuilder AddMembers(IEnumerable<MemberDeclarationSyntax>? members)
    {
        if (members is not null)
        {
            _members.AddRange(members);
        }
        return this;
    }

    internal ClassDeclarationBuilder AddMemberIf(
        bool condition,
        Func<MemberDeclarationSyntax> memberGenerator
    )
    {
        return condition ? AddMember(memberGenerator()) : this;
    }

    internal ClassDeclarationBuilder AddPublicModifier() =>
        AddModifier(Token(SyntaxKind.PublicKeyword));

    internal ClassDeclarationBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public ClassDeclarationSyntax Build()
    {
        return ClassDeclaration(
            List(DefaultAttributes.DefaultTypeAttributes.Concat(_attribute)),
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            Identifier(name),
            typeParameters,
            _baseTypes.Count > 0 ? BaseList(SeparatedList(_baseTypes)) : null,
            default,
            new SyntaxList<MemberDeclarationSyntax>(_members)
        );
    }
}
