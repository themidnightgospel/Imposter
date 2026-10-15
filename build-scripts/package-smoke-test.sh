#!/usr/bin/env bash

set -Eeuo pipefail

# Packs the generator, checks that release notes convert to plain text and survive unchanged, builds a small consumer project with
# IMPOSTER_LOG=true, and checks that the generator log reaches the build output.
# This exercises what a NuGet consumer gets, which the in-repo tests (project references) do not.

script_dir=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)
cd "${script_dir}/.."

work_dir="artifacts/package-smoke-test"
version="0.0.0-smoke.$(date +%s)"

rm -rf "${work_dir}"
mkdir -p "${work_dir}/feed" "${work_dir}/consumer"

# The release body is Markdown with the constructs the release notes use. The conversion must drop the markup
# and keep shell and MSBuild expressions literal, including percent-encoded special characters.
release_notes_file="$(realpath "${work_dir}")/release notes.txt"
expected_notes_file="${work_dir}/expected notes.txt"
# shellcheck disable=SC2016 # The notes deliberately contain literal MSBuild expressions.
printf '%s' '## Fixes

- **Configure `Arg<string>.Is("health").Callback(_ => { })`** ([#1](https://example.invalid/1)).
  Keep $(Version), @(Compile), 100%, %3B, %22 and %24 unchanged.

| Call | Before |
| --- | ---: |
| method | 144 B |

Preserve XML characters <tag> & text and Unicode: café.' | python3 build-scripts/release-notes-to-text.py > "${release_notes_file}"
# shellcheck disable=SC2016
printf '%s' 'Fixes

- Configure Arg<string>.Is("health").Callback(_ => { }) (#1).
  Keep $(Version), @(Compile), 100%, %3B, %22 and %24 unchanged.

| Call | Before |
| --- | ---: |
| method | 144 B |

Preserve XML characters <tag> & text and Unicode: café.' > "${expected_notes_file}"
diff "${expected_notes_file}" "${release_notes_file}" || {
    echo "release-notes-to-text.py changed the notes beyond dropping the markup"
    exit 1
}
echo "Release notes converted to plain text"

# The released package holds one analyzer build per Roslyn version; the compiler loads the newest one
# it supports. A single build is enough here, so pack the one a .NET 10 SDK compiler loads.
echo "packing Imposter ${version}"
dotnet pack src/Imposter.CodeGenerator -c Release --verbosity quiet \
    -p:ROSLYN_VERSION=5.0 -p:Version="${version}" -o "${work_dir}/feed" \
    -p:PackageReleaseNotesFile="${release_notes_file}"

python3 - "${work_dir}/feed/Imposter.${version}.nupkg" "${release_notes_file}" <<'PY'
import pathlib
import sys
import xml.etree.ElementTree as ET
import zipfile

expected = pathlib.Path(sys.argv[2]).read_text(encoding="utf-8")
with zipfile.ZipFile(sys.argv[1]) as package:
    nuspec = ET.fromstring(package.read("Imposter.nuspec"))
actual = nuspec.findtext("{*}metadata/{*}releaseNotes")
if actual != expected:
    sys.exit(f"Package release notes changed: expected {expected!r}, got {actual!r}")
print("Package release notes are unchanged")
PY

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
