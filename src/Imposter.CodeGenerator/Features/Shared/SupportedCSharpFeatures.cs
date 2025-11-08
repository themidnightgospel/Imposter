using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct SupportedCSharpFeatures
{
    internal bool SupportsTypeExtensions { get; }

    internal SupportedCSharpFeatures(CSharpCompilation compilation)
    {
        SupportsTypeExtensions = SupportsTypeExtensionsCore(compilation.LanguageVersion);
    }

    private static bool SupportsTypeExtensionsCore(LanguageVersion languageVersion)
    {
        var effectiveVersion = languageVersion.MapSpecifiedToEffectiveVersion();
        return (int)effectiveVersion >= 1400;
    }
}
