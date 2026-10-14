| Library | N | Mean time | Allocated |
|---------|--:|----------:|----------:|
| Moq | 1 | 184.59 μs | 12.84 KB |
| NSubstitute | 1 | 3.65 μs | 7.48 KB |
| FakeItEasy | 1 | 3.46 μs | 5.50 KB |
| Rocks | 1 | 83.03 ns | 304 B |
| **Imposter** | **1** | **413.27 ns** | **2.28 KB** |
| Moq | 10 | 1.74 ms | 114.98 KB |
| NSubstitute | 10 | 17.17 μs | 26.20 KB |
| FakeItEasy | 10 | 18.65 μs | 34.88 KB |
| Rocks | 10 | 923.98 ns | 1.84 KB |
| **Imposter** | **10** | **4.17 μs** | **21.27 KB** |
| Moq | 100 | 17.94 ms | 1.38 MB |
| NSubstitute | 100 | 452.18 μs | 228.33 KB |
| FakeItEasy | 100 | 346.63 μs | 715.15 KB |
| Rocks | 100 | 42.56 μs | 17.31 KB |
| **Imposter** | **100** | **55.69 μs** | **211.11 KB** |
| Moq | 1,000 | 262.12 ms | 41.21 MB |
| NSubstitute | 1,000 | 36.56 ms | 2.07 MB |
| FakeItEasy | 1,000 | 27.51 ms | 44.72 MB |
| Rocks | 1,000 | 3.66 ms | 172.00 KB |
| **Imposter** | **1,000** | **3.25 ms** | **2.06 MB** |

Intel Xeon Platinum 8370C CPU 2.80GHz, 4 logical cores, Linux Ubuntu 24.04.5 LTS (Noble Numbat), .NET 10.0.13, BenchmarkDotNet 0.15.6, commit 09bf8020, 2026-10-14 ([workflow run](https://github.com/themidnightgospel/Imposter/actions/runs/38567640047)).
