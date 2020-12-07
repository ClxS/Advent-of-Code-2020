namespace Day6
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Part1Solver : ISolver
    {
        private readonly string[] lines;

        public Part1Solver(string[] lines)
        {
            this.lines = lines;
        }

        public string Name => "Day6 Part1";

        public void Solve()
        {
            var sum = 0;
            var currentCount = 0u;
            foreach(var line in lines)
            {
                if (line.Length == 0)
                {
                    sum += BitOperations.PopCount(currentCount);
                    currentCount = 0;
                }
                else
                {
                    foreach(var @char in line)
                    {
                        currentCount |= 1u << (@char - 'a');
                    }
                }
            }

            sum += BitOperations.PopCount(currentCount);

            Log.Information("Survey Sum: {Sum}", sum);
        }
    }
}
