namespace Day13
{
    using Common;
    using Serilog;
    using System;

    public class Part2SolverBruteForce : ISolver
    {
        private readonly string text;

        public Part2SolverBruteForce(string text)
        {
            this.text = text;
        }

        public string Name => "Day13 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            var reader = new SpanStringReader(text);
            Span<(int Value, int Cell)> schedules = stackalloc (int Value, int Cell)[2048];

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

                schedules[scheduleCount++] = (NumberParser.ParseInt(value), cell);
            }

            var sortedSchedules = schedules.Slice(0, scheduleCount);
            sortedSchedules.Sort();
            sortedSchedules.Reverse();
            var highest = schedules[0].Value;

            var timestamp = 0;
            while (true)
            {
                bool found = true;
                for (int i = 0; i < scheduleCount; ++i)
                {
                    if (timestamp % sortedSchedules[i].Value != (sortedSchedules[i].Value - sortedSchedules[i].Cell) % sortedSchedules[i].Value)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }

                timestamp++;
            }

            return timestamp;
        }
    }
}
