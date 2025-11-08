using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

internal readonly struct EventImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string RaiseMethodName;

    internal readonly TypeSyntax RaiseMethodReturnType;

    internal EventImposterBuilderInterfaceMetadata(in ImposterEventCoreMetadata core)
    {
        Name = $"I{core.UniqueName}EventImposterBuilder";
        TypeSyntax = IdentifierName(Name);
        RaiseMethodName = core.IsAsync ? "RaiseAsync" : "Raise";
        RaiseMethodReturnType = core.IsAsync
            ? WellKnownTypes.System.Threading.Tasks.TaskOfT(TypeSyntax)
            : TypeSyntax;
    }
}
