using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders;

internal static class UsingStatements
{
    private static readonly List<UsingDirectiveSyntax> DefaultUsings =
    [
        UsingDirective(ParseName("System")),
        UsingDirective(ParseName("System.Linq")),
        UsingDirective(ParseName("System.Collections.Generic")),
        UsingDirective(ParseName("System.Threading.Tasks")),
        UsingDirective(ParseName("System.Diagnostics")),
        UsingDirective(ParseName("System.Runtime.CompilerServices")),
        UsingDirective(ParseName("Imposter.Abstractions")),
        UsingDirective(ParseName("System.Collections.Concurrent"))
    ];
    
    internal static IEnumerable<UsingDirectiveSyntax> Build(INamespaceSymbol imposterTargetNamespace)
    {
        if (!imposterTargetNamespace.ContainingNamespace.IsGlobalNamespace)
        {
            return DefaultUsings
                .Concat([
                    UsingDirective(
                        ParseName(imposterTargetNamespace.ToDisplayString()))
                ]);
        }

        return DefaultUsings;
    }
}