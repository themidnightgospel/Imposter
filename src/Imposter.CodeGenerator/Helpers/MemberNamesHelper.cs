using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Helpers;

internal static class MemberNamesHelper
{
    internal static IEnumerable<string> GetNames(IReadOnlyCollection<MemberDeclarationSyntax> members) =>
        members
            .SelectMany(m => m switch
            {
                MethodDeclarationSyntax method => [method.Identifier.Text],
                PropertyDeclarationSyntax property => [property.Identifier.Text],
                FieldDeclarationSyntax field => field.Declaration.Variables.Select(v => v.Identifier.Text),
                TypeDeclarationSyntax type => [type.Identifier.Text],
                _ => []
            });
}
