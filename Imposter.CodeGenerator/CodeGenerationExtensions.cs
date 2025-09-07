using System.Runtime.CompilerServices;

namespace Imposter.CodeGenerator;

internal static class CodeGenerationExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string AppendCommandIfNotEmpty(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return value + ",";
    }
}