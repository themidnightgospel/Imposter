#!/usr/bin/env bash

set -Eeuo pipefail

# Fails when a verification build left the generated-file snapshots different from the committed ones,
# which means a generator change was committed without its snapshots. Run it after the verification builds.
# git diff compares content after line-ending normalization, so snapshots rewritten with CRLF do not count.

script_dir=$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" &>/dev/null && pwd)
cd "${script_dir}/.."

snapshots=(
  tests/Imposter.Tests/GeneratedFiles
  benchmarks/Imposter.Benchmarks/GeneratedFiles
  playground/Imposter.Playground/GeneratedFiles
)

untracked=$(git ls-files --others --exclude-standard -- "${snapshots[@]}")

if git diff --quiet -- "${snapshots[@]}" && [[ -z "${untracked}" ]]; then
  echo "generated-file snapshots are up to date"
  exit 0
fi

echo "::error::The generated-file snapshots are out of date. Build with -p:VERIFICATION_BUILD=true (the pre-commit hook does) and commit the changes under GeneratedFiles."
git diff --stat -- "${snapshots[@]}"
if [[ -n "${untracked}" ]]; then
  echo "new files:"
  echo "${untracked}"
fi
exit 1
