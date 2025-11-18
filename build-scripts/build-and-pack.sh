#!/usr/bin/env bash

set -Eeuo pipefail

# Derived from Mapperly's packaging script (https://github.com/riok/mapperly) under the Apache-2.0 license.
# Pack a nupkg for each Roslyn version supported by Imposter and merge them into a single multi-target package.

roslyn_versions=('4.0' '4.4' '4.5' '4.7' '4.11' '4.14' '5.0')

RELEASE_VERSION=${RELEASE_VERSION:-"0.0.1-dev.$(date +%s)"}
RELEASE_NOTES=${RELEASE_NOTES:-''}

# https://stackoverflow.com/a/246128/3302887
script_dir=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)
artifacts_dir="${script_dir}/../artifacts"

echo "building Imposter v${RELEASE_VERSION} for ${IMPOSTER_ENVIRONMENT:-'local'}"
echo "cleaning artifacts dir"
mkdir -p "${artifacts_dir}"
rm -rf "${artifacts_dir:?}"/*

artifacts_dir="$(realpath "$artifacts_dir")"
source_generator_path="$(realpath "${script_dir}/../src/Imposter.CodeGenerator")"

for roslyn_version in "${roslyn_versions[@]}"; do
    echo "building for Roslyn ${roslyn_version}"
    dotnet pack \
        "$source_generator_path" \
        --verbosity quiet \
        -c Release \
        /p:ROSLYN_VERSION="${roslyn_version}" \
        -o "${artifacts_dir}/roslyn-${roslyn_version}" \
        /p:Version="${RELEASE_VERSION}" \
        /p:PackageReleaseNotes=\""${RELEASE_NOTES}"\"
done

echo "merging multi targets to a single nupkg"
zipmerge "${artifacts_dir}/Imposter.${RELEASE_VERSION}.nupkg" "${artifacts_dir}"/*/*.nupkg
echo "built ${artifacts_dir}/Imposter.${RELEASE_VERSION}.nupkg"
