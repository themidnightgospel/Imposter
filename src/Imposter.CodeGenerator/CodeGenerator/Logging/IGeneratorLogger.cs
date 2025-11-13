namespace Imposter.CodeGenerator.CodeGenerator.Logging;

internal interface IGeneratorLogger
{
    void Log(string message);
    void Log(string label, string value);
}

