namespace Day9
{
    using Common;
    using Serilog;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Part1Solver : ISolver
    {
        const int preambleSize = 25;

        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day9 Part1";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Solve()
        {
            Log.Information("Incorrect element value: {Value}", Solve(this.text));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Solve(string text)
        {
            var backlog = new StackCircularBuffer<long>(stackalloc long[preambleSize]);

            var reader = new SpanStringReader(text);
            for (int i = 0; i < preambleSize; i++)
            {
                backlog.PushBack(long.Parse(reader.ReadWord(true)));
            }

            while (!reader.IsEndOfFile())
            {
                var num = long.Parse(reader.ReadWord(true));
                bool found = false;
                for (int i = 0; i < preambleSize; i++)
                {
                    for (int j = 0; j < preambleSize; j++)
                    {
                        if (i != j && backlog[i] + backlog[j] == num)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                if (found)
                {
                    backlog.PushBack(num);
                }
                else
                {
                    return num;
                }
            }

            return -1;
        }
    }
}
