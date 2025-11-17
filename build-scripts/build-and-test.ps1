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
