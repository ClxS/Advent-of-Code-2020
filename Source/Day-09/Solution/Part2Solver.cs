namespace Day9
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly string text;
        private readonly long searchValue;

        public Part2Solver(string text, long searchValue)
        {
            this.text = text;
            this.searchValue = searchValue;
        }

        public string Name => "Day9 Part2";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Solve()
        {
            var backlog = new List<long>();

            var reader = new SpanStringReader(this.text);
            while (!reader.IsEndOfFile())
            {
                var num = long.Parse(reader.ReadWord(true));
                backlog.Add(num);
            }

            var backlogCount = backlog.Count;
            for(var i = 0; i < backlogCount; i++)
            {
                if (backlog[i] > this.searchValue)
                {
                    continue;
                }

                var sum = backlog[i];
                var min = backlog[i];
                var max = backlog[i];
                for (var j = i + 1; j < backlogCount; j++)
                {
                    sum += backlog[j];
                    min = Math.Min(backlog[j], min);
                    max = Math.Max(backlog[j], max);
                    if (sum > this.searchValue)
                    {
                        break;
                    }

                    if (sum == this.searchValue)
                    {
                        Log.Information("Encryption weakness: {Value}", min + max);
                        return;
                    }
                }
            }
        }
    }
}
