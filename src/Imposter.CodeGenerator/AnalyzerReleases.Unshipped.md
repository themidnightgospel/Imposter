; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
IMP006 | Imposter | Warning  | Imposter.CodeGenerator

### Removed Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
IMP001 | Imposter | Error    | Imposter.CodeGenerator, unreachable: the generator only runs for C# compilations
