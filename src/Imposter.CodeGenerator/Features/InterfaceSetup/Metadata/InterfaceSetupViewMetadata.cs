using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
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
        InterfaceSetupInterfaceModel @interface,
        IReadOnlyList<InterfaceSetupMemberMetadata> members,
        IReadOnlyDictionary<string, string> viewNames
    )
    {
        Name = viewNames[@interface.Type.FullyQualifiedName];
        Syntax = IdentifierName(Name);
        SelectorType = SyntaxFactoryHelper.TypeSyntax(@interface.Type).ToNullableType();
        var declaredMembers = members
            .Where(member =>
                member.Model.ContainingInterface.FullyQualifiedName
                == @interface.Type.FullyQualifiedName
            )
            .ToList();
        // Identical interface events already share a builder in the generator. Expose that
        // same builder through every declaration's view without changing event semantics.
        foreach (var @event in @interface.Events)
        {
            if (declaredMembers.Any(member => member.Model == @event))
            {
                continue;
            }

            foreach (
                var member in members
                    .Where(member =>
                        member.Model.Kind == InterfaceSetupMemberKind.Event
                        && member.Model.Name == @event.Name
                        && member.Model.EventType?.FullyQualifiedName
                            == @event.EventType?.FullyQualifiedName
                    )
                    .Take(1)
            )
            {
                declaredMembers.Add(
                    new InterfaceSetupMemberMetadata(@event, member.SetupName, member.ReturnType)
                );
            }
        }
        Members = declaredMembers;
        BaseTypes = @interface
            .Parents.Select(parent =>
                (BaseTypeSyntax)SimpleBaseType(IdentifierName(viewNames[parent.FullyQualifiedName]))
            )
            .ToArray();
    }
}
