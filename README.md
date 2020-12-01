# Advent-of-Code-2020

Here's my Advent of Code solutions for 2020.
Below are the daily benchmarks. These probably will be tiny for the first half the days.

## Day 1
|          Method |        Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |------------:|----------:|----------:|-------:|------:|------:|----------:|
|           Part1 |    22.26 us |  0.230 us |  0.215 us |      - |     - |     - |      64 B |
|           Part2 | 2,407.20 us | 39.212 us | 36.679 us |      - |     - |     - |     221 B |
|    Part2HashSet |   280.90 us |  0.758 us |  0.592 us | 2.9297 |     - |     - |   13473 B |
| Part2IntBuckets |    25.48 us |  0.388 us |  0.363 us | 0.6104 |     - |     - |    2576 B |
