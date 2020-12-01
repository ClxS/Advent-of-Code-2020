# Advent-of-Code-2020

Here's my Advent of Code solutions for 2020.
Below are the daily benchmarks. These probably will be tiny for the first half the days.

## Day 1
|                    Method |        Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |------------:|----------:|----------:|-------:|------:|------:|----------:|
|                     [Part1](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part1Solver.cs) |    22.14 us |  0.097 us |  0.075 us |      - |     - |     - |      64 B |
|                     [Part2](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2Solver.cs) | 2,345.42 us | 12.574 us | 11.762 us |      - |     - |     - |     221 B |
|              [Part2HashSet](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverHashTable.cs) |   291.18 us |  2.092 us |  1.747 us | 2.9297 |     - |     - |   13472 B |
|           [Part2IntBuckets](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverIntBuckets.cs) |    26.04 us |  0.306 us |  0.256 us | 0.6104 |     - |     - |    2576 B |
| [Part2IntBucketsStackalloc](https://github.com/ClxS/Advent-of-Code-2020/blob/master/Source/Day-01/Solution/Part2SolverIntBucketsStackalloc.cs) |    26.79 us |  0.209 us |  0.185 us | 0.6104 |     - |     - |    2576 B |
