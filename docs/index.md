# Imposter Documentation

Welcome to the official documentation for **Imposter**, the Roslyn incremental source generator that creates lightweight imposters (mocks/stubs) for interfaces annotated with `GenerateImposterAttribute`. Use this site to learn how the generator works, how to consume the abstractions package, and how to extend the project for your own needs.

## Project at a Glance

- **Core packages**
  - `Imposter.Abstractions`: runtime helpers and source-generator attributes consumed by client applications.
  - `Imposter.CodeGenerator`: the incremental generator that emits imposter implementations during compilation.
- **Supporting areas**
  - `tests/`: feature and regression coverage for both abstractions and generator behaviors.
  - `benchmarks/Imposter.Benchmarks`: BenchmarkDotNet suite comparing Imposter to other mocking frameworks.
  - `playground/`: scratchpad for rapid prototyping and scenario exploration.

Refer to the in-repo architecture notes (`Imposter_codex/ARCHITECTURE.md`) for a deeper structural overview.

## Getting Started

1. **Install the tooling**
   ```bash
   dotnet tool restore
   ```
   or install the packages directly:
   ```bash
   dotnet add package Imposter.Abstractions
   ```

2. **Mark target interfaces**
   ```csharp
   [GenerateImposter]
   public interface IWeatherService { ... }
   ```

3. **Build**
   ```bash
   dotnet build Imposter.sln
   ```

4. **Use the generated imposter** – the generator emits an implementation during compilation that can be configured in tests or runtime scenarios.

### Helpful Links

- [Usage guide](../Imposter_codex/USAGE.md)
- [Local development workflow](../Imposter_codex/LOCAL_DEVELOPMENT.md)
- [Coding standards](../Imposter_codex/CODING_STANDARDS.md)

## Documentation Workflow

This site is powered by **MkDocs**, the **Material for MkDocs** theme, and **mike** for version management.

### Local Preview

```bash
pip install -r docs/requirements.txt
mkdocs serve
```

Open `http://127.0.0.1:8000/` to preview live edits.

### Versioned Releases with mike

```bash
mike deploy 1.0 latest
mike set-default latest
```

Each `deploy` call builds the docs for the current checkout and pushes static files to the `gh-pages` branch under the given label.

### Publishing to GitHub Pages

After running mike commands locally (or inside CI), push the generated `gh-pages` branch:

```bash
git push origin gh-pages
```

Then configure GitHub Pages to serve from the `gh-pages` branch (root). A GitHub Actions workflow can automate these steps; add it once CI/CD requirements are defined.

---

Future sections will cover API references, generator internals, and feature cookbooks. Contributions are welcome—open a PR with updates under `docs/` and link to relevant feature files in `Imposter_codex/`.
