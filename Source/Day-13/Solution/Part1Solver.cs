namespace Day13
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

        public string Name => "Day13 Part1";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            var reader = new SpanStringReader(text);
            Span<int> schedules = stackalloc int[2048];

            var scheduleCount = 0;
            var departure = reader.ReadInt(true);
            while(!reader.IsEndOfFile())
            {
                var value = reader.ReadUntil(',', true);
                if (value[0] == 'x')
                {
                    continue;
                }

                schedules[scheduleCount++] = NumberParser.ParseInt(value);
            }

            var closestScheduleValue = double.MaxValue;
            var closestSchedule = -1;
            for(var i = 0; i < scheduleCount; ++i)
            {
                var nextDepartureTime = Math.Ceiling(departure / (double)schedules[i]) * schedules[i];
                var minutesToWait = nextDepartureTime - departure;

                if (minutesToWait < closestScheduleValue)
                {
                    closestScheduleValue = minutesToWait;
                    closestSchedule = i;
                }
            }

            return schedules[closestSchedule] * (int)closestScheduleValue;
        }
    }
}
