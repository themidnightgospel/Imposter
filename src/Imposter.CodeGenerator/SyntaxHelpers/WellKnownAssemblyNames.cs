namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class WellKnownAssemblyNames
{
    internal const string SystemRuntime = "System.Runtime";
    internal const string SystemPrivateCoreLib = "System.Private.CoreLib";
    internal const string Mscorlib = "mscorlib";

    internal static readonly string[] SystemAssemblies =
    [
        SystemRuntime,
        SystemPrivateCoreLib,
        Mscorlib
    ];
}
