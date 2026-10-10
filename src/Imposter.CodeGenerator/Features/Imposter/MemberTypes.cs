using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Imposter;

// The types the imposter declares for each member, by the unique name of the member they're named after, such as
// ICurrentPropertyBuilder or GetDelegate. C# doesn't let a nested type share its name with a member that isn't a type,
// or with another type of the same arity, which a target member named like one of them, or two members deriving the
// same name, would make it do.
internal sealed class MemberTypes
{
    private readonly List<(string UniqueName, MemberDeclarationSyntax Type)> _types = [];

    internal void Add(string uniqueName, MemberDeclarationSyntax type) =>
        _types.Add((uniqueName, type));

    // The unique names of the members whose types clash with another member of the imposter.
    internal HashSet<string> UniqueNamesOfClashingTypes(ClassDeclarationSyntax imposter)
    {
        var declarations = imposter.Members.SelectMany(Declarations).ToLookup(it => it.Name);

        return
        [
            .. _types
                .Where(it => Declarations(it.Type).Any(type => Clashes(type, declarations)))
                .Select(it => it.UniqueName),
        ];
    }

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

    // TypeArity is null for a member that isn't a type.
    private readonly record struct Declaration(string Name, int? TypeArity);
}
