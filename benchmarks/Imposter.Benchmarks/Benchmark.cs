using System;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Imposter.Benchmarks.ImposterVsAlternatives;

// The benchmark targets net10.0 because Rocks requires it.
// BenchmarkDotNet creates an isolated build that doesn't inherit MSBuild properties,
// so we must explicitly pass ROSLYN_VERSION=5.0 (the Roslyn version shipping with net10.0)
// for the source generator to compile against the correct Roslyn API surface.
// The Benchmarks workflow checks pull requests with a short run and renders the README's table from the JSON report.
var job =
    Environment.GetEnvironmentVariable("IMPOSTER_BENCHMARK_JOB") == "short"
        ? Job.ShortRun
        : Job.Default;
var config = DefaultConfig
    .Instance.AddJob(job.WithArguments([new MsBuildArgument("/p:ROSLYN_VERSION=5.0")]))
    .AddExporter(JsonExporter.Full)
    .WithOption(ConfigOptions.DisableOptimizationsValidator, true);

// Runs the comparison the README shows unless --filter picks others, e.g. --filter *ComplexMethodMockingBenchmark*.
BenchmarkSwitcher
    .FromAssembly(typeof(SimpleMethodMockingBenchmarks).Assembly)
    .Run(
        args.Contains("--filter") || args.Contains("-f")
            ? args
            : [.. args, "--filter", "*SimpleMethodMockingBenchmarks*"],
        config
    );
