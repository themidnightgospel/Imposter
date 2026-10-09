using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;

internal readonly struct InterfaceSetupMetadata
{
    internal readonly string SelectorName;
    internal readonly IReadOnlyList<InterfaceSetupViewMetadata> Views;

    internal InterfaceSetupMetadata(
        InterfaceSetupTargetModel target,
        IReadOnlyList<InterfaceSetupMemberMetadata> members,
        IReadOnlyList<MemberDeclarationSyntax> existingMembers
    )
    {
        var names = new NameSet(
            MemberNamesHelper
                .GetNames(existingMembers)
                .Concat(target.TypeParameterNames)
                .Concat(members.Select(member => member.Model.Name))
                .Concat(
                    members.SelectMany(member =>
                        member.Model.TypeParameters.Select(parameter => parameter.Name)
                    )
                )
        );
        SelectorName = names.Use("For");
        // Keyed by the type's name without nullable annotations, which never tell two interfaces apart.
        var viewNames = new Dictionary<string, string>();
        foreach (var @interface in target.Interfaces)
        {
            viewNames.Add(@interface.Type.FullyQualifiedName, names.Use(@interface.Name + "Setup"));
        }

        Views = target
            .Interfaces.Select(@interface => new InterfaceSetupViewMetadata(
                @interface,
                members,
                viewNames
            ))
            .ToArray();
    }
}
