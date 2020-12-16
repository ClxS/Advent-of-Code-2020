namespace Day16
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day16 Part1";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            var reader = new SpanStringReader(text);
            //var fields = new Dictionary<string, (int groupStart, int groupEnd)[]>();
            var fields = new List<(int groupStart, int groupEnd)>();

            while (true)
            {
                var line = reader.ReadLine();
                if (line.Length == 0)
                {
                    break;
                }

                var lineReader = new SpanStringReader(line);
                var field = lineReader.ReadUntil(':', true);
                lineReader.ReadChar();
                var group1Start = lineReader.ReadInt();
                lineReader.ReadChar();
                var group1End = lineReader.ReadInt();
                lineReader.Skip(3);
                var group2Start = lineReader.ReadInt();
                lineReader.ReadChar();
                var group2End = lineReader.ReadInt();
                fields.Add((group1Start, group1End));
                fields.Add((group2Start, group2End));
            }

            // Skip own ticket
            while (true)
            {
                var line = reader.ReadLine();
                if (line.Length == 0)
                {
                    break;
                }
            }

            var errorRate = 0;
            reader.ReadLine();
            while (!reader.IsEndOfFile())
            {
                for (var i = 0; i < fields.Count; ++i)
                {
                    reader.ReadUntilDigit(false);
                    var value = reader.ReadInt(true);
                    if (!IsValueValid(fields, value))
                    {
                        errorRate += value;
                    }
                }
            }

            return errorRate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValueValid(List<(int groupStart, int groupEnd)> fields, int value)
        {
            foreach (var field in fields)
            {
                if (value >= field.groupStart && value <= field.groupEnd)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
