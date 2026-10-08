#!/usr/bin/env bash

set -Eeuo pipefail

# Packs the generator, builds a small consumer project from that local package with
# IMPOSTER_LOG=true, and checks that the generator log reaches the build output.
# This exercises what a NuGet consumer gets, which the in-repo tests (project references) do not.

script_dir=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)
cd "${script_dir}/.."

work_dir="artifacts/package-smoke-test"
version="0.0.0-smoke.$(date +%s)"

rm -rf "${work_dir}"
mkdir -p "${work_dir}/feed" "${work_dir}/consumer"

# The released package holds one analyzer build per Roslyn version; the compiler loads the newest one
# it supports. A single build is enough here, so pack the one a .NET 10 SDK compiler loads.
echo "packing Imposter ${version}"
dotnet pack src/Imposter.CodeGenerator -c Release --verbosity quiet \
    -p:ROSLYN_VERSION=5.0 -p:Version="${version}" -o "${work_dir}/feed"

cat > "${work_dir}/consumer/Consumer.csproj" <<EOF
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <RestoreSources>../feed;https://api.nuget.org/v3/index.json</RestoreSources>
    <RestorePackagesPath>../packages</RestorePackagesPath>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Imposter" Version="${version}" PrivateAssets="all" />
  </ItemGroup>
</Project>
EOF

cat > "${work_dir}/consumer/Clock.cs" <<'EOF'
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Consumer.IClock))]

namespace Consumer;

public interface IClock
{
    int Now();
}
EOF

echo "building the consumer with IMPOSTER_LOG=true"
build_log="${work_dir}/consumer-build.log"
dotnet build "${work_dir}/consumer/Consumer.csproj" -c Release -v:d -p:IMPOSTER_LOG=true > "${build_log}" 2>&1 || {
    tail -n 40 "${build_log}"
    echo "consumer build failed"
    exit 1
}

if ! grep -q "IMPLOG001" "${build_log}"; then
    echo "the generator log (IMPLOG001) did not reach the consumer build output"
    exit 1
fi

echo "IMPOSTER_LOG reaches the generator from the package:"
grep -o "IMPLOG001: .*" "${build_log}" | sort -u
