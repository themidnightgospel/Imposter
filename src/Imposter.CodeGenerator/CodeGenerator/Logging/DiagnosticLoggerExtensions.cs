using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.CodeGenerator.Logging;

internal static class DiagnosticLoggerExtensions
{
    internal static void LogSupportedCSharpFeatures(this IGeneratorLogger logger, in SupportedCSharpFeatures features)
    {
        logger.Log($"SupportedCSharpFeatures. SupportsNullableGenericType : {features.SupportsNullableGenericType},  SupportsTypeExtensions: {features.SupportsTypeExtensions}");
    }

    internal static void LogCompilation(this IGeneratorLogger logger, in CSharpCompilation compilation)
    {
        logger.Log($"CSharpCompilation. LanguageVersion : {compilation.LanguageVersion}");
    }
}