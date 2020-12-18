namespace Day13
{
    using Common;
    using Serilog;
    using System;
    using System.Runtime.CompilerServices;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day13 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static ulong Solve(string text)
        {
            var reader = new SpanStringReader(text);
            Span<(ulong Value, ulong Modulo)> schedules = stackalloc (ulong Value, ulong Modulo)[2048];

            var scheduleCount = 0;
            var _ = reader.ReadInt(true);
            var cell = -1;
            while (!reader.IsEndOfFile())
            {
                cell++;
                var value = reader.ReadUntil(',', true);
                if (value[0] == 'x')
                {
                    continue;
                }

                var intValue = NumberParser.ParseInt(value);
                schedules[scheduleCount++] = ((ulong)intValue, (ulong)(intValue - cell));
            }

            Span<ulong> x = stackalloc ulong[scheduleCount];

            var nFactor = 1UL;
            for (var i = 0; i < scheduleCount; i++)
            {
                nFactor *= schedules[i].Value;
            }

            for (var i = 0; i < scheduleCount; i++)
            {
                var n = nFactor / schedules[i].Value;
                var xi = FindX(n, schedules[i].Value);

                x[i] = schedules[i].Modulo * n * xi;
            }

            var xTotal = 0UL;
            for (var i = 0; i < scheduleCount; i++)
            {
                xTotal += x[i];
            }

            var timestamp = xTotal % nFactor;
            return xTotal % nFactor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong FindX(ulong n, ulong factor)
        {
            var value = 1UL;
            while(true)
            {
                if ((value * n) % factor == 1)
                {
                    return value;
                }

                value++;
            }
        }
    }
}
