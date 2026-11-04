| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 84.32 μs | 12.80 KB |
| NSubstitute | 1 | 3.12 μs | 7.48 KB |
| FakeItEasy | 1 | 2.68 μs | 5.49 KB |
| Rocks | 1 | 73.18 ns | 304 B |
| **Imposter** | **1** | **148.16 ns** | **624 B** |
| Moq | 10 | 813.70 μs | 114.58 KB |
| NSubstitute | 10 | 15.98 μs | 26.20 KB |
| FakeItEasy | 10 | 15.03 μs | 34.89 KB |
| Rocks | 10 | 854.30 ns | 1.84 KB |
| **Imposter** | **10** | **1.35 μs** | **4.76 KB** |
| Moq | 100 | 8.64 ms | 1.38 MB |
| NSubstitute | 100 | 394.31 μs | 228.32 KB |
| FakeItEasy | 100 | 265.74 μs | 715.13 KB |
| Rocks | 100 | 40.91 μs | 17.31 KB |
| **Imposter** | **100** | **26.50 μs** | **46.24 KB** |
| Moq | 1,000 | 124.64 ms | 41.17 MB |
| NSubstitute | 1,000 | 20.26 ms | 2.07 MB |
| FakeItEasy | 1,000 | 16.54 ms | 44.72 MB |
| Rocks | 1,000 | 3.88 ms | 172.00 KB |
| **Imposter** | **1,000** | **2.34 ms** | **461.09 KB** |

INTEL XEON PLATINUM 8573C, 4 logical cores, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon), .NET 10.0.14, BenchmarkDotNet 0.15.6, commit 063f59a1, 2026-11-04 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/41810425921)).
