| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 235.34 μs | 12.82 KB |
| NSubstitute | 1 | 3.94 μs | 7.48 KB |
| FakeItEasy | 1 | 3.42 μs | 5.49 KB |
| Rocks | 1 | 75.44 ns | 304 B |
| **Imposter** | **1** | **149.77 ns** | **624 B** |
| Moq | 10 | 2.27 ms | 114.85 KB |
| NSubstitute | 10 | 17.99 μs | 26.20 KB |
| FakeItEasy | 10 | 18.06 μs | 34.87 KB |
| Rocks | 10 | 896.95 ns | 1.84 KB |
| **Imposter** | **10** | **1.39 μs** | **4.76 KB** |
| Moq | 100 | 22.99 ms | 1.38 MB |
| NSubstitute | 100 | 391.87 μs | 217.46 KB |
| FakeItEasy | 100 | 327.98 μs | 715.14 KB |
| Rocks | 100 | 46.11 μs | 17.31 KB |
| **Imposter** | **100** | **29.83 μs** | **46.24 KB** |
| Moq | 1,000 | 283.14 ms | 41.22 MB |
| NSubstitute | 1,000 | 29.91 ms | 2.07 MB |
| FakeItEasy | 1,000 | 27.75 ms | 44.72 MB |
| Rocks | 1,000 | 4.13 ms | 172.00 KB |
| **Imposter** | **1,000** | **2.94 ms** | **461.09 KB** |

AMD EPYC 7763, 4 logical cores, Linux Ubuntu 26.04.1 LTS (Resolute Raccoon), .NET 10.0.14, BenchmarkDotNet 0.15.6, commit ec03ac98, 2026-11-01 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/41318741211)).
