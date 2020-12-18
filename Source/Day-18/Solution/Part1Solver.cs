namespace Day18
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day18 Part1";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static ulong Solve(string text)
        {
            var reader = new SpanStringReader(text);
            var totalValue = 0UL;
            while(!reader.IsEndOfFile())
            {
                var line = new List<char>(reader.ReadLine().ToArray());
                Utility.ConvertLineToReversePolish(line, false);
                totalValue += Utility.ExecuteExpression(line);
            }

            return totalValue;
        }
    }
}
