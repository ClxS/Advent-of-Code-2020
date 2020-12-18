namespace Day18
{
    using Common;
    using Serilog;
    using System.Collections.Generic;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day18 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static ulong Solve(string text)
        {
            var reader = new SpanStringReader(text);
            var totalValue = 0UL;
            while (!reader.IsEndOfFile())
            {
                var line = new List<char>(reader.ReadLine().ToArray());
                Utility.ConvertLineToReversePolish(line, true);
                totalValue += Utility.ExecuteExpression(line);
            }

            return totalValue;
        }
    }
}
