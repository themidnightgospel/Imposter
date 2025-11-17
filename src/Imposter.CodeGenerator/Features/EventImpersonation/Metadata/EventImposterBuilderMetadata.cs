using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventImposterBuilderMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly EventImposterBuilderFieldsMetadata Fields;

    internal readonly EventImposterBuilderMethodsMetadata Methods;

    internal EventImposterBuilderMetadata(in ImposterEventCoreMetadata core)
    {
        Name = $"{core.UniqueName}EventImposterBuilder";
        TypeSyntax = IdentifierName(Name);
        Fields = new EventImposterBuilderFieldsMetadata(core);
        Methods = new EventImposterBuilderMethodsMetadata(core);
    }
}
