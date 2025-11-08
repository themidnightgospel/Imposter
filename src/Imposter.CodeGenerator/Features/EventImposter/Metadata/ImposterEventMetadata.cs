using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

internal readonly ref struct ImposterEventMetadata
{
    internal readonly ImposterEventCoreMetadata Core;

    internal readonly EventImposterBuilderInterfaceMetadata BuilderInterface;

    internal readonly EventImposterBuilderMetadata Builder;

    internal readonly FieldMetadata BuilderField;

    internal ImposterEventMetadata(IEventSymbol eventSymbol, string uniqueName)
    {
        Core = new ImposterEventCoreMetadata(eventSymbol, uniqueName);
        BuilderInterface = new EventImposterBuilderInterfaceMetadata(Core);
        Builder = new EventImposterBuilderMetadata(Core);
        BuilderField = new FieldMetadata($"_{Core.UniqueName}", Builder.TypeSyntax);
    }
}
