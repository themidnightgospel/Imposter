# Security Policy

## Supported Versions

Security fixes are released in the latest available Imposter version. Older
versions are not guaranteed to receive backported fixes. Because Imposter
includes a Roslyn source generator that runs during compilation, generator and
dependency updates may be security relevant even when the public API is
unchanged.

| Version | Supported |
| ------- | --------- |
| Latest release | Yes |
| Older releases | No |

## Reporting a Vulnerability

Do not disclose a suspected vulnerability, exploit details, or a proof of
concept in a public issue, discussion, or pull request.

Use GitHub's
[private vulnerability reporting](https://github.com/themidnightgospel/Imposter/security/advisories/new)
to send the maintainers a confidential report. Include, when applicable:

- A description of the vulnerability and its potential impact
- The affected Imposter version
- The .NET SDK, Roslyn version, and target framework
- The operating system and architecture
- Minimal reproduction steps or a proof of concept
- Any known mitigations or suggested fixes
- Whether the report may be disclosed after a fix is available

## Response and Remediation

- A maintainer will acknowledge a private vulnerability report within seven
  calendar days.
- Confirmed vulnerabilities will be assessed and coordinated through a private
  GitHub security advisory until a fix and disclosure are ready.
- Remediation is prioritized according to severity, exploitability, and impact.
  Critical vulnerabilities receive the earliest practical fix and release.
- If a fix requires additional time, the maintainers will share available
  mitigations and expected next steps without disclosing information that would
  put users at additional risk.

Please allow a reasonable amount of time for investigation and remediation
before making the vulnerability public.

## Scope

Security reports may concern the build-time source generator, generated code,
the runtime abstractions included in the NuGet package, the package's dependency
chain, or the build and release process. Reports are assessed based on
reproducibility, impact, and whether the issue is in Imposter, one of its
dependencies, or the consuming project.

If the report is determined not to be a vulnerability, the maintainers may
recommend opening a regular GitHub issue instead.

## Automated Security Checks

The repository uses Dependabot update pull requests for NuGet packages, GitHub
Actions, and documentation dependencies. It also runs CodeQL code scanning and
OpenSSF Scorecard analysis. These checks reduce risk but do not replace manual
review of source-generator behavior, generated code, or dependency updates.

## Using Imposter Safely

Roslyn source generators execute inside the compiler process. Restore Imposter
only from a trusted package source, keep it and the .NET SDK current, and review
package updates before adopting them. Avoid building untrusted projects in an
environment that contains credentials or has access to sensitive resources.
