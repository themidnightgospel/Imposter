using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Helpers;

public class UniqueName(IEnumerable<string> alreadyUsedNames)
{
    private readonly HashSet<string> _alreadyUsedNames = [..alreadyUsedNames];

    internal virtual string New(string name)
    {
        var baseName = name;
        var uniquePostfix = 0;
        while (_alreadyUsedNames.Contains(name))
        {
            name = $"{baseName}{uniquePostfix++}";
        }

        _alreadyUsedNames.Add(name);
        return name;
    }
}

public class ClassMemberUniqueName(IReadOnlyCollection<MemberDeclarationSyntax> members)
    : UniqueName(members
        .SelectMany(m => m switch
        {
            MethodDeclarationSyntax method => [method.Identifier.Text],
            PropertyDeclarationSyntax property => [property.Identifier.Text],
            FieldDeclarationSyntax field => field.Declaration.Variables.Select(v => v.Identifier.Text),
            TypeDeclarationSyntax type => [type.Identifier.Text],
            _ => []
        }));