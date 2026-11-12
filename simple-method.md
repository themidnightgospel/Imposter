| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 233.69 μs | 12.82 KB |
| NSubstitute | 1 | 3.68 μs | 7.48 KB |
| FakeItEasy | 1 | 3.64 μs | 5.49 KB |
| Rocks | 1 | 74.47 ns | 304 B |
| **Imposter** | **1** | **158.95 ns** | **656 B** |
| Moq | 10 | 2.26 ms | 114.85 KB |
| NSubstitute | 10 | 18.57 μs | 26.20 KB |
| FakeItEasy | 10 | 17.51 μs | 34.87 KB |
| Rocks | 10 | 897.60 ns | 1.84 KB |
| **Imposter** | **10** | **1.44 μs** | **4.93 KB** |
| Moq | 100 | 23.01 ms | 1.38 MB |
| NSubstitute | 100 | 375.21 μs | 217.46 KB |
| FakeItEasy | 100 | 343.20 μs | 715.14 KB |
| Rocks | 100 | 46.05 μs | 17.31 KB |
| **Imposter** | **100** | **31.07 μs** | **47.82 KB** |
| Moq | 1,000 | 279.54 ms | 41.22 MB |
| NSubstitute | 1,000 | 32.24 ms | 2.07 MB |
| FakeItEasy | 1,000 | 27.22 ms | 44.72 MB |
| Rocks | 1,000 | 3.98 ms | 172.00 KB |
| **Imposter** | **1,000** | **3.01 ms** | **476.73 KB** |

AMD EPYC 7763, 4 logical cores, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon), .NET 10.0.15, BenchmarkDotNet 0.15.6, commit b6afbd3f, 2026-11-12 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/43314452188)).
