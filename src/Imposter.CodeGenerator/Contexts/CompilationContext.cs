using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Contexts;

public sealed record CompilationContext(CSharpCompilation Compilation, UniqueName GeneratedCsFileUniqueName)
{
}