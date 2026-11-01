using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Imposter;

// The types the imposter declares for each member, by the unique name of the member they're named after, such as
// ICurrentPropertyBuilder or GetDelegate. C# doesn't let a nested type share its name with a member that isn't a type,
// with another type of the same arity, or with a type parameter of the imposter, which a target member or type
// parameter named like one of them, or two members deriving the same name, would make it do.
internal sealed class MemberTypes
{
    private readonly List<(string UniqueName, MemberDeclarationSyntax Type)> _types = [];

    // Renaming a member must rename its types too, or the generator's clash-resolution loop cannot make progress.
    internal void Add(string uniqueName, MemberDeclarationSyntax type)
    {
        foreach (var declaration in Declarations(type))
        {
            if (declaration.Name.IndexOf(uniqueName, StringComparison.Ordinal) < 0)
            {
                throw new InvalidOperationException(
                    $"Generated type '{declaration.Name}' must contain its member's unique name '{uniqueName}' so type-clash resolution can make progress."
                );
            }
        }

        _types.Add((uniqueName, type));
    }

    // The unique names of the members whose types clash with another member of the imposter, or with a type parameter
    // they declare.
    internal HashSet<string> UniqueNamesOfClashingTypes(ClassDeclarationSyntax imposter)
    {
        var declarations = imposter
            .Members.SelectMany(Declarations)
            .Concat(TypeParameters(imposter))
            .ToLookup(it => it.Name);

        return
        [
            .. _types
                .GroupBy(it => it.UniqueName, it => it.Type)
                .Where(member =>
                    DeclaresATypeParameterNamedLikeItsTypes([.. member])
                    || member.Any(type =>
                        Declarations(type).Any(declaration => Clashes(declaration, declarations))
                    )
                )
                .Select(member => member.Key),
        ];
    }

    // A generic method's types declare its type parameters, so a method type parameter named like one of its types
    // would either be declared by that type (CS0694) or hide it from the others that refer to it.
    private static bool DeclaresATypeParameterNamedLikeItsTypes(
        IReadOnlyCollection<MemberDeclarationSyntax> types
    )
    {
        var typeNames = new HashSet<string>(
            types
                .SelectMany(Declarations)
                .Where(it => it.TypeArity is not null)
                .Select(it => it.Name)
        );

        return types.SelectMany(TypeParameterNames).Any(typeNames.Contains);
    }

    private static IEnumerable<string> TypeParameterNames(MemberDeclarationSyntax type) =>
        (
            type switch
            {
                TypeDeclarationSyntax declaration => declaration.TypeParameterList,
                DelegateDeclarationSyntax @delegate => @delegate.TypeParameterList,
                _ => null,
            }
        )?.Parameters.Select(it => it.Identifier.Text) ?? [];

    // The type itself is among the declarations, so another one sharing its name means a clash.
    private static bool Clashes(Declaration type, ILookup<string, Declaration> declarations) =>
        declarations[type.Name].Count(it => it.TypeArity is null || it.TypeArity == type.TypeArity)
        > 1;

    // The names a member takes in the imposter, with the arity of a type. An explicit interface implementation, a
    // constructor and an indexer take none.
    private static IEnumerable<Declaration> Declarations(MemberDeclarationSyntax member) =>
        member switch
        {
            TypeDeclarationSyntax type =>
            [
                new(type.Identifier.Text, type.TypeParameterList?.Parameters.Count ?? 0),
            ],
            DelegateDeclarationSyntax @delegate =>
            [
                new(@delegate.Identifier.Text, @delegate.TypeParameterList?.Parameters.Count ?? 0),
            ],
            MethodDeclarationSyntax { ExplicitInterfaceSpecifier: null } method =>
            [
                new(method.Identifier.Text, null),
            ],
            PropertyDeclarationSyntax { ExplicitInterfaceSpecifier: null } property =>
            [
                new(property.Identifier.Text, null),
            ],
            EventDeclarationSyntax { ExplicitInterfaceSpecifier: null } @event =>
            [
                new(@event.Identifier.Text, null),
            ],
            BaseFieldDeclarationSyntax field => field.Declaration.Variables.Select(
                it => new Declaration(it.Identifier.Text, null)
            ),
            _ => [],
        };

    // The imposter's type parameters, the target's, which a nested type of any arity can't be named after.
    private static IEnumerable<Declaration> TypeParameters(ClassDeclarationSyntax imposter) =>
        imposter.TypeParameterList?.Parameters.Select(it => new Declaration(
            it.Identifier.Text,
            null
        )) ?? [];

    // TypeArity is null for a member that isn't a type, or for a type parameter.
    private readonly record struct Declaration(string Name, int? TypeArity);
}
