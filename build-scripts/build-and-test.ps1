Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Write-Host "Running Imposter test task suite..." -ForegroundColor Cyan

function Invoke-Task {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Name,
        [Parameter(Mandatory = $true)]
        [string]$Command,
        [Parameter(Mandatory = $true)]
        [string[]]$Args
    )

    Write-Host "==> $Name" -ForegroundColor Green
    & $Command @Args
    if ($LASTEXITCODE -ne 0) {
        throw "Task '$Name' failed with exit code $LASTEXITCODE."
    }
}

function Get-StagedFiles {
    $staged = @()
    try {
        $staged = & git diff --cached --name-only --diff-filter=ACMRTUXB
    }
    catch {
        Write-Warning "Unable to determine staged files via git. CSharpier step will be skipped."
        return @()
    }

    if ($LASTEXITCODE -ne 0) {
        Write-Warning "git diff --cached returned exit code $LASTEXITCODE. CSharpier step will be skipped."
        return @()
    }

    return $staged | Where-Object { -not [string]::IsNullOrWhiteSpace($_) }
}

function Get-CSharpierTargets {
    $includeExtensions = @(".cs", ".props", ".csproj")
    $stagedFiles = Get-StagedFiles
    if (-not $stagedFiles) {
        return @()
    }

    $targets = foreach ($file in $stagedFiles) {
        $extension = [System.IO.Path]::GetExtension($file)
        if ($includeExtensions -notcontains $extension) {
            continue
        }

        if ($file.EndsWith(".g.cs")) {
            continue
        }

        if ($file -like "*GeneratedFiles/*" -or $file -like "*GeneratedFiles\*") {
            continue
        }

        $file
    }

    return $targets | Sort-Object -Unique
}

$csharpierTargets = Get-CSharpierTargets
if ($csharpierTargets.Count -gt 0) {
    $csharpierArgs = @("csharpier", "format") + $csharpierTargets
    Invoke-Task -Name "CSharpier" -Command "dotnet" -Args $csharpierArgs
}
else {
    Write-Host "Skipping CSharpier run because no staged files match include/exclude rules." -ForegroundColor Yellow
}

Invoke-Task -Name "Imposter.Abstractions.Tests" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Abstractions.Tests/Imposter.Abstractions.Tests.csproj",
    "-c", "Release"
)

Invoke-Task -Name "Imposter.CodeGenerator.Tests" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.CodeGenerator.Tests/Imposter.CodeGenerator.Tests.csproj",
    "-c", "Release"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 4.0" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=4.0"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 4.4" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=4.4"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 4.5" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=4.5"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 4.7" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=4.7"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 4.11" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=4.11"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 4.14" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=4.14"
)

Invoke-Task -Name "Imposter.Tests / Roslyn 5.0" -Command "dotnet" -Args @(
    "test",
    "tests/Imposter.Tests/Imposter.Tests.csproj",
    "-c", "Release",
    "/p:ROSLYN_VERSION=5.0"
)

Write-Host "All tasks completed successfully." -ForegroundColor Cyan
