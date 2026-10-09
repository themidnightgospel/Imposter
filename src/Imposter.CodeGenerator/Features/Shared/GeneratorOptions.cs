using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.Shared;

/// <summary>
/// The compilation-wide inputs of the generator, kept apart from the compilation so outputs that depend on them are
/// reused while they don't change.
/// </summary>
internal sealed record GeneratorOptions(LanguageVersion LanguageVersion, bool IsLoggingEnabled)
{
    // Generated imposters use C# 9 features.
    internal const LanguageVersion MinimumLanguageVersion = LanguageVersion.CSharp9;

    internal bool IsLanguageVersionSupported => LanguageVersion >= MinimumLanguageVersion;
}
