namespace Day12
{
    using Common;
    using Serilog;
    using System;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day12 Part2";

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
            (int X, int Y) waypoint = (10, 1);
            while (!reader.IsEndOfFile())
            {
                var instruction = reader.ReadChar();
                var magnitude = reader.ReadInt(true);
                switch (instruction)
                {
                    case 'N':
                        waypoint = (waypoint.X, waypoint.Y + magnitude);
                        break;
                    case 'S':
                        waypoint = (waypoint.X, waypoint.Y - magnitude);
                        break;
                    case 'E':
                        waypoint = (waypoint.X + magnitude, waypoint.Y);
                        break;
                    case 'W':
                        waypoint = (waypoint.X - magnitude, waypoint.Y);
                        break;
                    case 'L':
                        {
                            var count = magnitude / 90;
                            for (var i = 0; i < count; ++i)
                            {
                                waypoint = ((waypoint.X * cos90) - (waypoint.Y * sin90), (waypoint.X * sin90) + (waypoint.Y * cos90));
                            }
                        }
                        break;
                    case 'R':
                        {
                            var count = magnitude / 90;
                            for (var i = 0; i < count; ++i)
                            {
                                waypoint = ((waypoint.X * -cos90) - (waypoint.Y * -sin90), (waypoint.X * -sin90) + (waypoint.Y * -cos90));
                            }
                        }
                        break;
                    case 'F':
                        pos = (pos.X + (waypoint.X * magnitude), pos.Y + (waypoint.Y * magnitude));
                        break;
                }

            }

            return Math.Abs(pos.X) + Math.Abs(pos.Y);
        }
    }
}
