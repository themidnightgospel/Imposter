using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared;

internal static class UsingStatements
{
    private static readonly List<UsingDirectiveSyntax> DefaultUsings =
    [
        UsingDirective(WellKnownTypes.System.Namespace),
        UsingDirective(WellKnownTypes.System.Linq.Namespace),
        UsingDirective(WellKnownTypes.System.Collections.Generic.Namespace),
        UsingDirective(WellKnownTypes.System.Threading.Tasks.Namespace),
        UsingDirective(WellKnownTypes.System.Diagnostics.Namespace),
        UsingDirective(WellKnownTypes.System.Runtime.CompilerServices.Namespace),
        UsingDirective(WellKnownTypes.Imposter.Abstractions.Namespace),
        UsingDirective(WellKnownTypes.System.Collections.Concurrent.Namespace)
    ];

    internal static List<UsingDirectiveSyntax> Build(INamespaceSymbol imposterTargetNamespace)
    {
        if (!imposterTargetNamespace.ContainingNamespace.IsGlobalNamespace)
        {
            return DefaultUsings
                .Concat([
                    UsingDirective(
                        ParseName(imposterTargetNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)))
                ])
                .ToList();
        }

        return DefaultUsings;
    }
}
