namespace Imposter.CodeGenerator.CodeGenerator.Logging;

internal static class DiagnosticLoggerExtensions
{
    internal static void LogSupportedCSharpFeatures(this IGeneratorLogger logger, in SupportedCSharpFeatures features)
    {
        logger.Log($"SupportedCSharpFeatures {features}");
    }
}