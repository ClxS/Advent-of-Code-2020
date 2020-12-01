# Advent-of-Code-2020

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
## Day 1

For an additional challenege, I added "Part 3", which looks for a combination of 4 numbers which match the condition.

|                    Method |       Mean |     Error |    StdDev |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|-------------------------- |-----------:|----------:|----------:|--------:|-------:|------:|----------:|
|                     [Part1](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part1Solver.cs) |   2.690 us | 0.0104 us | 0.0097 us |  0.0153 |      - |     - |      64 B |
|                     [Part2](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2Solver.cs) | 748.552 us | 4.1333 us | 3.4515 us |       - |      - |     - |     216 B |
|              [Part2HashSet](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverHashTable.cs) | 103.591 us | 0.5929 us | 0.5546 us |  3.0518 |      - |     - |   13176 B |
|           [Part2IntBuckets](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverIntBuckets.cs) |  12.347 us | 0.1006 us | 0.0941 us |  0.5341 |      - |     - |    2272 B |
| [Part2IntBucketsStackalloc](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverIntBucketsStackalloc.cs)  |  14.915 us | 0.2608 us | 0.2439 us |  0.5341 |      - |     - |    2272 B |
|                     [Part3](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part3Solver.cs) | 280.107 us | 3.1263 us | 2.6106 us | 19.5313 | 4.3945 |     - |   84568 B |

