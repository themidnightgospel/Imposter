using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;

internal readonly struct InterfaceSetupViewMetadata
{
    internal const string SelectorParameterName = "interfaceType";

    internal readonly string Name;
    internal readonly NameSyntax Syntax;
    internal readonly TypeSyntax SelectorType;
    internal readonly IReadOnlyList<InterfaceSetupMemberMetadata> Members;
    internal readonly IReadOnlyList<BaseTypeSyntax> BaseTypes;

    internal InterfaceSetupViewMetadata(
        INamedTypeSymbol interfaceSymbol,
        IReadOnlyList<InterfaceSetupMemberMetadata> members,
        IReadOnlyDictionary<INamedTypeSymbol, string> viewNames
    )
    {
        Name = viewNames[interfaceSymbol];
        Syntax = IdentifierName(Name);
        SelectorType = SyntaxFactoryHelper.TypeSyntax(interfaceSymbol).ToNullableType();
        var declaredMembers = members
            .Where(member =>
                SymbolEqualityComparer.Default.Equals(member.Symbol.ContainingType, interfaceSymbol)
            )
            .ToList();
        // Identical interface events already share a builder in the generator. Expose that
        // same builder through every declaration's view without changing event semantics.
        foreach (var eventSymbol in interfaceSymbol.GetMembers().OfType<IEventSymbol>())
        {
            if (
                declaredMembers.Any(member =>
                    SymbolEqualityComparer.Default.Equals(member.Symbol, eventSymbol)
                )
            )
            {
                continue;
            }

            foreach (
                var member in members
                    .Where(member =>
                        member.Symbol is IEventSymbol existingEvent
                        && existingEvent.Name == eventSymbol.Name
                        && SymbolEqualityComparer.Default.Equals(
                            existingEvent.Type,
                            eventSymbol.Type
                        )
                    )
                    .Take(1)
            )
            {
                declaredMembers.Add(
                    new InterfaceSetupMemberMetadata(
                        eventSymbol,
                        member.SetupName,
                        member.ReturnType
                    )
                );
            }
        }
        Members = declaredMembers;
        BaseTypes = interfaceSymbol
            .Interfaces.Select(parent =>
                (BaseTypeSyntax)SimpleBaseType(IdentifierName(viewNames[parent]))
            )
            .ToArray();
    }
}
