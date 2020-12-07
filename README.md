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
## [Day 7](https://adventofcode.com/2020/day/7)
|    Method |     Mean |     Error |    StdDev |    Gen 0 |   Gen 1 | Gen 2 | Allocated |
|---------- |---------:|----------:|----------:|---------:|--------:|------:|----------:|
| Part1And2 | 1.200 ms | 0.0114 ms | 0.0095 ms | 123.0469 | 48.8281 |     - | 661.97 KB |

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

