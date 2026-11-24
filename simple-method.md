| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 50.63 μs | 12.84 KB |
| NSubstitute | 1 | 1.99 μs | 7.48 KB |
| FakeItEasy | 1 | 1.92 μs | 5.49 KB |
| Rocks | 1 | 45.57 ns | 304 B |
| **Imposter** | **1** | **103.84 ns** | **656 B** |
| Moq | 10 | 477.29 μs | 115.04 KB |
| NSubstitute | 10 | 9.68 μs | 26.20 KB |
| FakeItEasy | 10 | 9.31 μs | 34.87 KB |
| Rocks | 10 | 544.40 ns | 1.84 KB |
| **Imposter** | **10** | **900.07 ns** | **4.93 KB** |
| Moq | 100 | 5.19 ms | 1.38 MB |
| NSubstitute | 100 | 198.48 μs | 217.46 KB |
| FakeItEasy | 100 | 182.46 μs | 715.14 KB |
| Rocks | 100 | 26.36 μs | 17.31 KB |
| **Imposter** | **100** | **16.49 μs** | **47.82 KB** |
| Moq | 1,000 | 77.65 ms | 41.23 MB |
| NSubstitute | 1,000 | 13.52 ms | 2.07 MB |
| FakeItEasy | 1,000 | 14.78 ms | 44.72 MB |
| Rocks | 1,000 | 2.20 ms | 172.00 KB |
| **Imposter** | **1,000** | **1.81 ms** | **476.73 KB** |

AMD EPYC 9V45, 4 logical cores, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon), .NET 10.0.15, BenchmarkDotNet 0.15.6, commit 5c746e43, 2026-11-24 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/45362657419)).
