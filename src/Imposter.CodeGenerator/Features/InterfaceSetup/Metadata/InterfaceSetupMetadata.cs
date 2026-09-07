using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;

internal readonly struct InterfaceSetupMetadata
{
    internal readonly string SelectorName;
    internal readonly IReadOnlyList<InterfaceSetupViewMetadata> Views;

    internal InterfaceSetupMetadata(
        INamedTypeSymbol targetSymbol,
        IReadOnlyList<InterfaceSetupMemberMetadata> members,
        IReadOnlyList<MemberDeclarationSyntax> existingMembers
    )
    {
        var names = new NameSet(
            MemberNamesHelper
                .GetNames(existingMembers)
                .Concat(targetSymbol.TypeParameters.Select(parameter => parameter.Name))
                .Concat(members.Select(member => member.Symbol.Name))
                .Concat(
                    members.SelectMany(member =>
                        member.Symbol is IMethodSymbol method
                            ? method.TypeParameters.Select(parameter => parameter.Name)
                            : Enumerable.Empty<string>()
                    )
                )
        );
        SelectorName = names.Use("For");
        var interfaces = targetSymbol.AllInterfaces.Prepend(targetSymbol).ToArray();
        var viewNames = new Dictionary<INamedTypeSymbol, string>(SymbolEqualityComparer.Default);
        foreach (var interfaceSymbol in interfaces)
        {
            viewNames.Add(interfaceSymbol, names.Use(interfaceSymbol.Name + "Setup"));
        }

        Views = interfaces
            .Select(interfaceSymbol => new InterfaceSetupViewMetadata(
                interfaceSymbol,
                members,
                viewNames
            ))
            .ToArray();
    }
}
