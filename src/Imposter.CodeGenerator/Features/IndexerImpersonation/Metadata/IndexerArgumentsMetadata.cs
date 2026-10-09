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
        // The class keeps each parameter in a field named after it, which can't share the class's name and which
        // Equals and GetHashCode read by name.
        var names = core.CreateParameterNameSet();
        Name = names.Use($"{core.UniqueName}IndexerArguments");
        TypeSyntax = IdentifierName(Name);
        OtherVariableName = names.Use("other");
        HashVariableName = names.Use("hash");
    }
}
