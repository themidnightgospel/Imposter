using BenchmarkDotNet.Running;
using Imposter.Benchmarks.ImposterVsMoqVsNSubstitute;

BenchmarkRunner.Run<SimpleMethodMockingBenchmarks>();
//BenchmarkRunner.Run<ReadonlyStructBenchmarks>();
