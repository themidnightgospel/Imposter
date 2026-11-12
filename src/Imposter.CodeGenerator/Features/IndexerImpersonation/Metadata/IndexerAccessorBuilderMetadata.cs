namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerAccessorBuilderMetadata(string imposterFieldName)
{
    internal readonly string Name = "Builder";

    internal readonly string ImposterFieldName = imposterFieldName;

    internal readonly string CriteriaFieldName = "_criteria";
}
