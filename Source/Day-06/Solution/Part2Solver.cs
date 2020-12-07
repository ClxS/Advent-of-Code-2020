namespace Day6
{
    using Common;
    using Serilog;
    using System;
    using System.Diagnostics;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly string[] lines;

        public Part2Solver(string[] lines)
        {
            this.lines = lines;
        }

        public string Name => "Day6 Part2";

        public void Solve()
        {
            var sum = 0;
            var currentCount = 0xFFFFFFFFu;
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    sum += BitOperations.PopCount(currentCount);
                    currentCount = 0xFFFFFFFFu;
                }
                else
                {
                    var userCount = 0u;
                    foreach (var @char in line)
                    {
                        userCount |= 1u << (@char - 'a');
                    }

                    currentCount &= userCount;
                }
            }

            sum += BitOperations.PopCount(currentCount);

            Log.Information("Survey Sum: {Sum}", sum);
        }
    }
}
