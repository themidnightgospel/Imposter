using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

public sealed record CompilationContext(CSharpCompilation Compilation, UniqueName GeneratedCsFileUniqueName)
{
}