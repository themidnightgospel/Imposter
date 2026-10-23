using System;
using BenchmarkDotNet.Attributes;
using Imposter.Abstractions;

namespace Imposter.Benchmarks;

// Run with IMPOSTER_BENCHMARK_JOB=short and --filter '*ArgMatcherBenchmarks*'.
// Keep predicates and candidate values outside the measured construction calls.
[MemoryDiagnoser]
public class ArgMatcherBenchmarks
{
    private readonly Func<int, bool> _predicate = value => value > 0;
    private readonly Arg<int> _number = Arg<int>.Is(5);
    private readonly Arg<string> _text = Arg<string>.Is("expected");
    private readonly Arg<string> _custom = Arg<string>.Is(
        "expected",
        StringComparer.OrdinalIgnoreCase
    );
    private readonly Arg<int> _negatedPredicate = Arg<int>.IsNot(value => value > 0);
    private int _candidate = 5;
    private string _textCandidate = "expected";

    [Benchmark]
    public Arg<int> IsValue() => Arg<int>.Is(_candidate);

    [Benchmark]
    public Arg<int> IsNotValue() => Arg<int>.IsNot(_candidate);

    [Benchmark]
    public Arg<int> ImplicitConversion() => _candidate;

    [Benchmark]
    public Arg<string> IsReferenceValue() => Arg<string>.Is(_textCandidate);

    [Benchmark]
    public Arg<int> IsPredicate() => Arg<int>.Is(_predicate);

    [Benchmark]
    public Arg<int> IsNotPredicate() => Arg<int>.IsNot(_predicate);

    [Benchmark]
    public bool MatchValue() => _number.Matches(_candidate);

    [Benchmark]
    public bool MatchReference() => _text.Matches(_textCandidate);

    [Benchmark]
    public bool MatchCustomComparer() => _custom.Matches(_textCandidate);

    [Benchmark]
    public bool MatchNegatedPredicate() => _negatedPredicate.Matches(_candidate);
}
