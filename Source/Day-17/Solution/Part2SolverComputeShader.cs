namespace Day17
{
    using ComputeSharp;
    using Serilog;
    using System;
    using System.Runtime.CompilerServices;

    public class Part2SolverComputeShader
    {
        private readonly string text;
        const int size = 32;
        const int origin = 32 / 2;

        public Part2SolverComputeShader(string text)
        {
            this.text = text;
        }

        public string Name => "Day17 Part2 ComputeShader";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public readonly struct CopyData : IComputeShader
        {
            public readonly ReadWriteBuffer<int> readBuffer;
            public readonly ReadWriteBuffer<int> writeBuffer;

            public CopyData(ReadWriteBuffer<int> readBuffer, ReadWriteBuffer<int> writeBuffer)
            {
                this.readBuffer = readBuffer;
                this.writeBuffer = writeBuffer;
            }

            public void Execute(ThreadIds ids)
            {
                this.writeBuffer[ids.X] = this.readBuffer[ids.X];
            }
        }

        public readonly struct Simulate : IComputeShader
        {
            public readonly ReadWriteBuffer<int> readBuffer;
            public readonly ReadWriteBuffer<int> writeBuffer;
            private readonly int iterationOffset;
            private readonly int wMax;

            public Simulate(
                ReadWriteBuffer<int> readBuffer,
                ReadWriteBuffer<int> writeBuffer,
                int iterationOffset,
                int wMax)
            {
                this.readBuffer = readBuffer;
                this.writeBuffer = writeBuffer;
                this.iterationOffset = iterationOffset;
                this.wMax = wMax;
            }

            public void Execute(ThreadIds ids)
            {
                var x = this.iterationOffset + ids.X;
                var y = this.iterationOffset + ids.Y;
                var z = this.iterationOffset + ids.Z;
                for (var w = this.iterationOffset; w <= wMax; w++)
                {
                    var cell = ((16 + x) * 32 * 32 * 32) + ((16 + y) * 32 * 32) + ((16 + z) * 32) + (16 + w);

                    var neighbours = 0;
                    for (var xn = x - 1; xn <= x + 1; xn++)
                    {
                        for (var yn = y - 1; yn <= y + 1; yn++)
                        {
                            for (var zn = z - 1; zn <= z + 1; zn++)
                            {
                                for (var wn = w - 1; wn <= w + 1; wn++)
                                {
                                    if (x == xn && y == yn && z == zn && w == wn)
                                    {
                                        continue;
                                    }

                                    if (readBuffer[((16 + xn) * 32 * 32 * 32) + ((16 + yn) * 32 * 32) + ((16 + zn) * 32) + (16 + wn)] == 1)
                                    {
                                        neighbours++;
                                    }
                                }
                            }
                        }
                    }

                    if (readBuffer[cell] == 1)
                    {
                        if (neighbours != 2 && neighbours != 3)
                        {
                            writeBuffer[cell] = 0;
                        }
                    }
                    else
                    {
                        if (neighbours == 3)
                        {
                            writeBuffer[cell] = 1;
                        }
                    }
                }
            }
        }

        public static void DryRunShader()
        {
            var tmp = Gpu.Default.AllocateReadWriteBuffer<int>(1);
            Gpu.Default.For(0, new CopyData(tmp, tmp));
            Gpu.Default.For(0, new Simulate(tmp, tmp, default, default));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Solve(string text)
        {
            var data = new int[size * size * size * size];
            var count = 0;
            InitializeDimension(text, data, out var width, out var height, ref count);

            ReadWriteBuffer<int> dimensionA = Gpu.Default.AllocateReadWriteBuffer<int>(size * size * size * size);
            ReadWriteBuffer<int> dimensionB = Gpu.Default.AllocateReadWriteBuffer<int>(size * size * size * size);

            dimensionA.SetData(data);

            var readBuffer = dimensionA;
            var writeBuffer = dimensionB;

            var depth = Math.Max(width, height);
            for (var i = 1; i <= 6; i++)
            {
                Gpu.Default.For(data.Length, new CopyData(readBuffer, writeBuffer));
                Gpu.Default.For(
                    width + 2 + (i * 2),
                    height + 2 + (i * 2),
                    depth + 2 + (i * 2),
                    new Simulate(
                        readBuffer,
                        writeBuffer,
                        -i,
                        depth + 2 + (i * 2)));

                writeBuffer = writeBuffer == dimensionA ? dimensionB : dimensionA;
                readBuffer = readBuffer == dimensionA ? dimensionB : dimensionA;
            }

            count = 0;
            readBuffer.GetData(data);
            for (int j = 0; j < data.Length; j++)
            {
                if (data[j] != 0)
                {
                    count++;
                }
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InitializeDimension(string text, Span<int> readDimension, out int width, out int height, ref int count)
        {
            height = 1;
            width = 0;
            count = 0;
            for (var i = 0; i < text.Length; ++i)
            {
                switch (text[i])
                {
                    case '#':
                        readDimension[ResolveCell(width, height - 1, 0, 0)] = 1;
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

        private static int ResolveCell(int x, int y, int z, int w)
        {
            return ((origin + x) * size * size * size) + ((origin + y) * size * size) + ((origin + z) * size) + (origin + w);
        }
    }
}
