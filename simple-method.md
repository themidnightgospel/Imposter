| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 52.33 μs | 12.84 KB |
| NSubstitute | 1 | 2.01 μs | 7.48 KB |
| FakeItEasy | 1 | 1.90 μs | 5.49 KB |
| Rocks | 1 | 46.60 ns | 304 B |
| **Imposter** | **1** | **95.09 ns** | **624 B** |
| Moq | 10 | 490.60 μs | 115.04 KB |
| NSubstitute | 10 | 9.11 μs | 26.20 KB |
| FakeItEasy | 10 | 9.29 μs | 34.87 KB |
| Rocks | 10 | 553.48 ns | 1.84 KB |
| **Imposter** | **10** | **839.29 ns** | **4.76 KB** |
| Moq | 100 | 5.11 ms | 1.38 MB |
| NSubstitute | 100 | 197.56 μs | 217.46 KB |
| FakeItEasy | 100 | 171.93 μs | 715.14 KB |
| Rocks | 100 | 27.02 μs | 17.31 KB |
| **Imposter** | **100** | **16.25 μs** | **46.24 KB** |
| Moq | 1,000 | 79.22 ms | 41.23 MB |
| NSubstitute | 1,000 | 13.01 ms | 2.07 MB |
| FakeItEasy | 1,000 | 14.00 ms | 44.72 MB |
| Rocks | 1,000 | 2.40 ms | 172.00 KB |
| **Imposter** | **1,000** | **1.90 ms** | **461.09 KB** |

AMD EPYC 9V45, 4 logical cores, Linux Ubuntu 24.04.5 LTS (Noble Numbat), .NET 10.0.14, BenchmarkDotNet 0.15.6, commit 214652c7, 2026-10-29 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/40966758628)).
