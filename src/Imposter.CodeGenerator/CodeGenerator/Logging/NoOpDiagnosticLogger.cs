namespace Imposter.CodeGenerator.CodeGenerator.Logging;

internal readonly struct NoOpDiagnosticLogger : IGeneratorLogger
{
    public void Log(string message)
    {
    }

    public void Log(string label, string value)
    {
    }
}