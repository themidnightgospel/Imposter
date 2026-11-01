using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct InvocationImposterGroupsFieldMetadata
{
    internal readonly string Name;

    internal InvocationImposterGroupsFieldMetadata(NameSet memberNames)
    {
        Name = memberNames.Use("_invocationImposters");
    }
}
