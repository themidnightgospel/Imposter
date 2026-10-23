| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 184.04 μs | 12.84 KB |
| NSubstitute | 1 | 3.65 μs | 7.48 KB |
| FakeItEasy | 1 | 3.38 μs | 5.50 KB |
| Rocks | 1 | 87.34 ns | 304 B |
| **Imposter** | **1** | **442.53 ns** | **2.20 KB** |
| Moq | 10 | 1.77 ms | 114.98 KB |
| NSubstitute | 10 | 17.48 μs | 26.20 KB |
| FakeItEasy | 10 | 17.98 μs | 34.88 KB |
| Rocks | 10 | 939.14 ns | 1.84 KB |
| **Imposter** | **10** | **4.22 μs** | **20.48 KB** |
| Moq | 100 | 17.81 ms | 1.38 MB |
| NSubstitute | 100 | 369.55 μs | 217.45 KB |
| FakeItEasy | 100 | 342.93 μs | 715.15 KB |
| Rocks | 100 | 67.88 μs | 17.31 KB |
| **Imposter** | **100** | **58.07 μs** | **203.30 KB** |
| Moq | 1,000 | 256.39 ms | 41.21 MB |
| NSubstitute | 1,000 | 26.29 ms | 2.07 MB |
| FakeItEasy | 1,000 | 27.74 ms | 44.72 MB |
| Rocks | 1,000 | 3.75 ms | 172.00 KB |
| **Imposter** | **1,000** | **2.89 ms** | **1.98 MB** |

Intel Xeon Platinum 8370C CPU 2.80GHz, 4 logical cores, Linux Ubuntu 24.04.5 LTS (Noble Numbat), .NET 10.0.13, BenchmarkDotNet 0.15.6, commit f829eff0, 2026-10-23 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/39991679000)).
