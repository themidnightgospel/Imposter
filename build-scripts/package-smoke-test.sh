#!/usr/bin/env bash

set -Eeuo pipefail

# Packs the generator, checks that release notes convert to plain text and survive unchanged, builds a small consumer project with
# IMPOSTER_LOG=true, checks the generator log, and executes property setups from the packed runtime.
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
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <!-- CI installs the .NET 10 SDK; prefer .NET 8 when present, otherwise use an installed newer runtime. -->
    <RollForward>Major</RollForward>
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
[assembly: GenerateImposter(typeof(Consumer.BaseClock))]
[assembly: GenerateImposter(typeof(Consumer.IValue<>))]

namespace Consumer;

public interface IClock
{
    int Now();
    int Offset { get; set; }
}

public interface IValue<T>
{
    T Value { get; set; }
}

public class BaseClock
{
    public virtual int Offset { get; set; }
}
EOF

# Execute code against the package reference, not the repository's project references. A successful build alone
# cannot catch a missing runtime assembly or a forwarding method that throws when it is first JIT-compiled.
cat > "${work_dir}/consumer/Program.cs" <<'EOF'
using System;
using System.Collections.Generic;
using Consumer;
using Imposter.Abstractions;

var imposter = new IClockImposter();
var clock = imposter.Instance();
clock.Offset = 7;
Require(clock.Offset == 7, "auto-property value");
var calls = 0;
imposter.Offset.Getter().Returns(10).Then().Returns(20).Callback(() => calls++);
Require(clock.Offset == 10 && clock.Offset == 20 && clock.Offset == 20, "outcome sequence");
Require(calls == 3, "getter callbacks");
imposter.Offset.Getter().Called(Count.Exactly(4));
imposter.Offset.Setter(Arg<int>.Is(7)).Called(Count.Once());

var strict = new IClockImposter(ImposterMode.Explicit);
try
{
    _ = strict.Instance().Offset;
    throw new Exception("An unconfigured strict getter was accepted.");
}
catch (MissingImposterException) { }
strict.Offset.Getter().Returns(42);
Require(strict.Instance().Offset == 42, "strict getter");

var baseImposter = new BaseClockImposter();
baseImposter.Offset.UseBaseImplementation();
baseImposter.Instance().Offset = 23;
Require(baseImposter.Instance().Offset == 23, "base accessor dispatch");

ExerciseValue<string>(null);
ExerciseValue("reference");
ExerciseValue<decimal?>(null);
ExerciseValue(1.234m);
ExerciseValue(Guid.NewGuid());
ExerciseValue<Func<int>>(() => 42);

Console.WriteLine($"Packed property runtime passed on .NET {Environment.Version}.");

static void ExerciseValue<T>(T value)
{
    var imposter = new IValueImposter<T>();
    var instance = imposter.Instance();
    instance.Value = value;
    Require(EqualityComparer<T>.Default.Equals(instance.Value, value), "generic auto-property");
    imposter.Value.Getter().Returns(() => value).Then().Throws<InvalidOperationException>().Then().Returns(value);
    Require(EqualityComparer<T>.Default.Equals(instance.Value, value), "generic factory");
    try
    {
        _ = instance.Value;
        throw new Exception("A configured exception was not thrown.");
    }
    catch (InvalidOperationException) { }
    Require(EqualityComparer<T>.Default.Equals(instance.Value, value), "generic outcome after exception");
    imposter.Value.Getter().Called(Count.Exactly(4));
    imposter.Value.Setter(Arg<T>.Is(value)).Called(Count.Once());
}

static void Require(bool condition, string scenario)
{
    if (!condition)
        throw new Exception("Packed property runtime failed: " + scenario);
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

dotnet run --project "${work_dir}/consumer/Consumer.csproj" -c Release --no-build --no-restore
