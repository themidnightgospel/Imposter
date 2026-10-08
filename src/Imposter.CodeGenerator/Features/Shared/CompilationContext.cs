using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.Shared;

internal record CompilationContext(CSharpCompilation Compilation, bool IsLoggingEnabled)
{
    // Generated imposters use C# 9 features.
    internal const LanguageVersion MinimumLanguageVersion = LanguageVersion.CSharp9;

    internal bool IsLanguageVersionSupported =>
        Compilation.LanguageVersion >= MinimumLanguageVersion;
}
