using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal record CompilationContext(CSharpCompilation Compilation, SymbolNameNamespace SymbolNameNamespace)
{
}