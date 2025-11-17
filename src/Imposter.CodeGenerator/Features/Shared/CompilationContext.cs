using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.Shared;

internal record CompilationContext(
    CSharpCompilation Compilation,
    NameSet NameSet,
    bool IsLoggingEnabled
) { }
