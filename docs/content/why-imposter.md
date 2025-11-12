# Why Imposter?

- Source-generated test doubles, zero runtime overhead.
- Strong typing: fluent API matches your types and signatures, including ref/out/in and generics.
- Performance-conscious: lightweight shapes and allocation-aware design.
- Concurrency: stress-tested for thread-safety; sequencing preserves order.
- Coverage: methods, properties, indexers, and events with verification helpers.
- Ergonomics: `Arg<T>` matchers, `Count` verification, callbacks, sequencing.

See benchmarks under `benchmarks/Imposter.Benchmarks` for comparisons with Moq and NSubstitute, and the on-site [Benchmarks](benchmarks.md) page for a summary and run instructions.

Before adopting, review the [Limitations](limitations.md) to understand scope and constraints.
