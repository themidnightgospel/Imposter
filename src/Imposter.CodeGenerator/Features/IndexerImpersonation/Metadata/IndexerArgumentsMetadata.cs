using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerArgumentsMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string OtherVariableName;

    internal readonly string HashVariableName;

    internal IndexerArgumentsMetadata(in ImposterIndexerCoreMetadata core)
    {
        Name = $"{core.UniqueName}IndexerArguments";
        TypeSyntax = IdentifierName(Name);
        // The class keeps each parameter in a field of the same name, which Equals and GetHashCode read by name.
        var names = core.CreateParameterNameSet();
        OtherVariableName = names.Use("other");
        HashVariableName = names.Use("hash");
    }
}
