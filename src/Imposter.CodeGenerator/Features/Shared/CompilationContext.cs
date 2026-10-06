using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.Shared;

internal record CompilationContext(CSharpCompilation Compilation, bool IsLoggingEnabled) { }
