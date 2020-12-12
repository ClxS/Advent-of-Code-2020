namespace Day12
{
    using Common;
    using Serilog;
    using System;
    using System.Linq;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day12 Part1";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            var reader = new SpanStringReader(text);

            const int cos90 = 0;
            const int sin90 = 1;

            (int X, int Y) pos = (0, 0);
            (int X, int Y) vector = (1, 0);
            while(!reader.IsEndOfFile())
            {
                var instruction = reader.ReadChar();
                var magnitude = reader.ReadInt(true);
                switch (instruction)
                {
                    case 'N':
                        pos = (pos.X, pos.Y + magnitude);
                        break;
                    case 'S':
                        pos = (pos.X, pos.Y - magnitude);
                        break;
                    case 'E':
                        pos = (pos.X + magnitude, pos.Y);
                        break;
                    case 'W':
                        pos = (pos.X - magnitude, pos.Y);
                        break;
                    case 'L':
                        {
                            var count = magnitude / 90;
                            for (var i = 0; i < count; ++i)
                            {
                                vector = ((vector.X * cos90) - (vector.Y * sin90), (vector.X * sin90) + (vector.Y * cos90));
                            }
                        }
                        break;
                    case 'R':
                        {
                            var count = magnitude / 90;
                            for (var i = 0; i < count; ++i)
                            {
                                vector = ((vector.X * -cos90) - (vector.Y * -sin90), (vector.X * -sin90) + (vector.Y * -cos90));
                            }
                        }
                        break;
                    case 'F':
                        pos = (pos.X + (vector.X * magnitude), pos.Y + (vector.Y * magnitude));
                        break;
                }

            }

            return Math.Abs(pos.X) + Math.Abs(pos.Y);
        }
    }
}
