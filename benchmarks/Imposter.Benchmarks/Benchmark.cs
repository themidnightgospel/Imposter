using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Imposter.Benchmarks.ImposterVsMoqVsNSubstitute;

// The benchmark targets net10.0 because Rocks requires it.
// BenchmarkDotNet creates an isolated build that doesn't inherit MSBuild properties,
// so we must explicitly pass ROSLYN_VERSION=5.0 (the Roslyn version shipping with net10.0)
// for the source generator to compile against the correct Roslyn API surface.
var config = DefaultConfig
    .Instance.AddJob(
        Job.Default.WithArguments(
            new[] { new MsBuildArgument("/p:ROSLYN_VERSION=5.0") }
        )
    )
    .WithOption(ConfigOptions.DisableOptimizationsValidator, true);

BenchmarkRunner.Run<SimpleMethodMockingBenchmarks>(config);
// BenchmarkRunner.Run<ComplexMethodMockingBenchmark>(config);
//BenchmarkRunner.Run<ReadonlyStructBenchmarks>(config);
