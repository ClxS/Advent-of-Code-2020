namespace Day15
{
    using Common;
    using Serilog;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day15 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Solve(string text)
        {
            var speakTable = new Dictionary<int, (int NextToLast, int Last)>();

            var reader = new SpanStringReader(text);

            var idx = 1;
            int value = 0;
            while (!reader.IsEndOfFile())
            {
                value = reader.ReadInt();
                reader.ReadChar();

                value = Step(value, idx, speakTable);
                idx++;
            }

            for (; idx <= 30000000; ++idx)
            {
                value = Step(value, idx, speakTable);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Step(int value, int idx, Dictionary<int, (int NextToLast, int Last)> lastTable)
        {
            if (lastTable.TryGetValue(value, out var last))
            {
                value = last.Last - last.NextToLast;

                if (lastTable.TryGetValue(value, out last))
                {
                    last = (last.Last, idx);
                    lastTable[value] = last;
                }
                else
                {
                    lastTable[value] = (idx, idx);
                }
            }
            else
            {
                lastTable[value] = (idx, idx);
            }

            return value;
        }
    }
}
