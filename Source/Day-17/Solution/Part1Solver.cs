﻿namespace Day17
{
    using Serilog;
    using System;
    using System.Runtime.CompilerServices;

    public class Part1Solver
    {
        private readonly string text;
        private const int size = 32;
        private const int origin = size / 2;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day17 Part1";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Solve(string text)
        {
            Span<byte> dimensionA = stackalloc byte[size * size * size];
            Span<byte> dimensionB = stackalloc byte[size * size * size];

            Span<byte> writeDimension = dimensionA;
            Span<byte> readDimension = dimensionB;

            int count = 0;
            InitializeDimension(text, readDimension, out var width, out var height, ref count);

            var depth = Math.Max(width, height);
            for(var i = 1; i <= 6; i++)
            {
                readDimension.CopyTo(writeDimension);
                Simulate(readDimension, writeDimension, (-i, width + i), (-i, height + i), (-i, depth + i), ref count);
                writeDimension = writeDimension == dimensionA ? dimensionB : dimensionA;
                readDimension = readDimension == dimensionA ? dimensionB : dimensionA;
            }

            return count;
        }

        private static void Simulate(
            Span<byte> readDimension,
            Span<byte> writeDimension,
            (int XMin, int XMax) xBounds,
            (int YMin, int YMax) yBounds,
            (int ZMin, int ZMax) zBounds,
            ref int count)
        {
            for (var x = xBounds.XMin; x <= xBounds.XMax; x++)
            {
                for (var y = yBounds.YMin; y <= yBounds.YMax; y++)
                {
                    for (var z = zBounds.ZMin; z <= zBounds.ZMax; z++)
                    {
                        var cell = ResolveCell(x, y, z);
                        var neighbours = CountActiveNeighbours(readDimension, x, y, z);
                        if (readDimension[cell] == 1)
                        {
                            if (neighbours != 2 && neighbours != 3)
                            {
                                writeDimension[cell] = 0;
                                count--;
                            }
                        }
                        else
                        {
                            if (neighbours == 3)
                            {
                                writeDimension[cell] = 1;
                                count++;
                            }
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CountActiveNeighbours(Span<byte> readDimension, int xIn, int yIn, int zIn)
        {
            var sum = 0;
            for (var x = xIn - 1; x <= xIn + 1; x++)
            {
                for (var y = yIn - 1; y <= yIn + 1; y++)
                {
                    for (var z = zIn - 1; z <= zIn + 1; z++)
                    {
                        if (x == xIn && y == yIn && z == zIn)
                        {
                            continue;
                        }

                        if (readDimension[ResolveCell(x, y, z)] == 1)
                        {
                            sum++;
                        }
                    }
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InitializeDimension(string text, Span<byte> readDimension, out int width, out int height, ref int count)
        {
            height = 1;
            width = 0;
            count = 0;
            for(var i = 0; i < text.Length; ++i)
            {
                switch (text[i])
                {
                    case '#':
                        readDimension[ResolveCell(width, height - 1, 0)] = 1;
                        count++;
                        width++;
                        break;
                    case '.':
                        width++;
                        break;
                    case '\n':
                        height++;
                        width = 0;
                        break;
                }

            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ResolveCell(int x, int y, int z)
        {
            return ((origin + x) * (size * size)) + (origin + y) * size + (origin + z);
        }
    }
}
