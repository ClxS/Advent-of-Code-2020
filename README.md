# Advent of Code 2020

Here's my Advent of Code solutions for 2020.
Below are the daily benchmarks. These probably will be tiny for the first half the days.

## Machine Info

All benchmarks were taken on the following environment.
````
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
AMD Ryzen 5 2600, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
````
## [Day 16](https://adventofcode.com/2020/day/17)
|             Method |        Mean |     Error |    StdDev |      Median |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|------------------- |------------:|----------:|----------:|------------:|---------:|---------:|---------:|----------:|
|              Part1 |    638.3 us |   8.21 us |   7.68 us |    641.6 us |        - |        - |        - |       9 B |
|              Part2 | 35,296.1 us | 357.39 us | 316.82 us | 35,218.4 us | 142.8571 | 142.8571 | 142.8571 | 2097195 B |
| Part2ComputeShader | 12,297.1 us | 341.12 us | 995.06 us | 11,703.9 us | 187.5000 | 187.5000 | 187.5000 | 4205692 B |

## [Day 16](https://adventofcode.com/2020/day/16)
| Method |       Mean |    Error |   StdDev |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |-----------:|---------:|---------:|---------:|------:|------:|----------:|
|  Part1 |   124.9 us |  1.30 us |  1.02 us |   0.2441 |     - |     - |   1.12 KB |
|  Part2 | 3,391.9 us | 36.56 us | 32.41 us | 417.9688 |     - |     - | 1717.3 KB |

## [Day 15](https://adventofcode.com/2020/day/15)
| Method |            Mean |         Error |        StdDev |     Gen 0 |     Gen 1 |     Gen 2 |    Allocated |
|------- |----------------:|--------------:|--------------:|----------:|----------:|----------:|-------------:|
|  Part1 |        57.95 us |      1.122 us |      1.536 us |    5.3101 |    0.0610 |         - |     21.79 KB |
|  Part2 | 2,471,931.63 us | 35,300.070 us | 33,019.707 us | 4000.0000 | 4000.0000 | 4000.0000 | 316836.99 KB |

## [Day 14](https://adventofcode.com/2020/day/14)
| Method |        Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |    Gen 2 |   Allocated |
|------- |------------:|----------:|----------:|----------:|----------:|---------:|------------:|
|  Part1 |    351.6 us |   6.94 us |   9.96 us |  166.5039 |  166.5039 | 166.5039 |   512.02 KB |
|  Part2 | 35,166.9 us | 536.55 us | 475.64 us | 5357.1429 | 1285.7143 | 785.7143 | 27005.59 KB |

## [Day 13](https://adventofcode.com/2020/day/13)
| Method |        Mean |     Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |------------:|----------:|---------:|------:|------:|------:|----------:|
|  Part1 |    757.7 ns |  13.50 ns | 12.63 ns |     - |     - |     - |         - |
|  Part2 | 12,929.2 ns | 101.77 ns | 90.22 ns |     - |     - |     - |         - |

## [Day 12](https://adventofcode.com/2020/day/12)
| Method |     Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|---------:|---------:|------:|------:|------:|----------:|
|  Part1 | 16.43 us | 0.248 us | 0.232 us |     - |     - |     - |         - |
|  Part2 | 16.08 us | 0.183 us | 0.163 us |     - |     - |     - |         - |

## [Day 11](https://adventofcode.com/2020/day/11)
| Method |     Mean |    Error |   StdDev |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|---------:|---------:|----------:|------:|------:|----------:|
|  Part1 | 82.15 ms | 0.860 ms | 0.718 ms | 7571.4286 |     - |     - |  30.23 MB |
|  Part2 | 76.33 ms | 0.690 ms | 0.576 ms | 5428.5714 |     - |     - |  22.16 MB |

## [Day 10](https://adventofcode.com/2020/day/10)
| Method |     Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|----------:|----------:|------:|------:|------:|----------:|
|  Part1 | 4.404 us | 0.0442 us | 0.0392 us |     - |     - |     - |         - |
|  Part2 | 3.777 us | 0.0288 us | 0.0269 us |     - |     - |     - |         - |

## [Day 9](https://adventofcode.com/2020/day/9)
| Method |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
|  Part1 | 108.8 us | 0.85 us | 0.75 us |      - |     - |     - |     344 B |
|  Part2 | 272.6 us | 2.19 us | 1.94 us | 3.9063 |     - |     - |   16664 B |

## [Day 8](https://adventofcode.com/2020/day/8)
|        Method |        Mean |     Error |    StdDev |     Gen 0 |    Gen 1 | Gen 2 |  Allocated |
|-------------- |------------:|----------:|----------:|----------:|---------:|------:|-----------:|
|         Part1 |    57.51 us |  0.481 us |  0.426 us |    7.5684 |        - |     - |   31.17 KB |
|         Part2 |   395.43 us |  2.081 us |  1.844 us |   17.0898 |        - |     - |    71.2 KB |

## [Day 7](https://adventofcode.com/2020/day/7)
|    Method |     Mean |    Error |   StdDev |    Gen 0 |   Gen 1 | Gen 2 | Allocated |
|---------- |---------:|---------:|---------:|---------:|--------:|------:|----------:|
| Part1And2 | 894.6 us | 17.49 us | 16.36 us | 100.5859 | 33.2031 |     - | 545.66 KB |

## [Day 6](https://adventofcode.com/2020/day/6)
| Method |     Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|---------:|---------:|------:|------:|------:|----------:|
|  Part1 | 21.20 us | 0.121 us | 0.094 us |     - |     - |     - |      56 B |
|  Part2 | 20.64 us | 0.319 us | 0.282 us |     - |     - |     - |      56 B |

## [Day 5](https://adventofcode.com/2020/day/5)
|        Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|         Part1 | 44.92 us | 0.264 us | 0.220 us |      - |     - |     - |      56 B |
| Part1Parallel | 22.14 us | 0.192 us | 0.170 us | 0.7935 |     - |     - |    3344 B |
|         Part2 | 27.77 us | 0.257 us | 0.477 us | 1.0986 |     - |     - |    4565 B |

## [Day 4](https://adventofcode.com/2020/day/4)
| Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|  Part1 |  75.48 us | 0.264 us | 0.206 us |      - |     - |     - |      56 B |
|  Part2 | 186.14 us | 0.365 us | 0.323 us | 2.9297 |     - |     - |   12568 B |

## [Day 3](https://adventofcode.com/2020/day/3)
| Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
|  [Part1](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-03/Solution/Part1Solver.cs) | 1.381 us | 0.0192 us | 0.0179 us | 0.0134 |     - |     - |      64 B |
|  [Part2](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-03/Solution/Part2Solver.cs) | 7.926 us | 0.0892 us | 0.0835 us | 0.0305 |     - |     - |     128 B |

## [Day 2](https://adventofcode.com/2020/day/2)
|           Method |     Mean |    Error |   StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|--------:|-------:|------:|----------:|
| [ComposePasswords](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-02/Solution/Password.cs) | 43.17 us | 0.850 us | 1.219 us | 25.0244 | 2.9907 |     - |  98.95 KB |
|            [Part1](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-02/Solution/Part1Solver.cs) | 78.90 us | 1.465 us | 1.223 us | 25.7568 | 0.6104 |     - |  102.8 KB |
|            [Part2](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-02/Solution/Part2Solver.cs) | 77.70 us | 1.409 us | 1.832 us | 25.7568 | 0.2441 |     - | 102.98 KB |

## [Day 1](https://adventofcode.com/2020/day/1)

For an additional challenege, I added "Part 3", which looks for a combination of 4 numbers which match the condition.

|                    Method |       Mean |     Error |    StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------------- |-----------:|----------:|----------:|--------:|-------:|------:|----------:|
|                     [Part1](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part1Solver.cs) |   2.69 us | 0.0104 us | 0.0097 us |  0.0153 |      - |     - |      64 B |
|                     [Part2](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2Solver.cs) | 748.55 us | 4.1333 us | 3.4515 us |       - |      - |     - |     216 B |
|              [Part2HashSet](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverHashTable.cs) | 103.59 us | 0.5929 us | 0.5546 us |  3.0518 |      - |     - |   13176 B |
|           [Part2IntBuckets](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverIntBuckets.cs) |  5.26 us | 0.1001 us | 0.1229 us |  0.5341 |      - |     - |    2272 B |
| [Part2IntBucketsStackalloc](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverIntBucketsStackalloc.cs)  |  14.91 us | 0.2608 us | 0.2439 us |  0.5341 |      - |     - |    2272 B |
| [Part2Linq](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverLinq.cs) | 721.30 us | 9.24 us | 7.7100 us | 115.2344 |     - |     - | 471.02 KB |
|                     [Part3](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part3Solver.cs) | 280.10 us | 3.1263 us | 2.6106 us | 19.5313 | 4.3945 |     - |   84568 B |

