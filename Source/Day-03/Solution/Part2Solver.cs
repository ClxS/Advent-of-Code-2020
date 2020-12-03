namespace Day3
{
    using Common;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly string[] map;
        private readonly (int X, int Y)[] movements;

        public Part2Solver(string[] map, params (int X, int Y)[] movements)
        {
            this.map = map;
            this.movements = movements;
        }

        public string Name => "Day3 Part2";

        public void Solve()
        {
            var totalMultiplier = 1L;
            foreach (var (X, Y) in this.movements)
            {
                var count = 0;
                var xVelocity = X;
                var YVelocity = Y;
                var cellsToMoveDown = map.Length / YVelocity;
                for (int i = 0; i < cellsToMoveDown; i++)
                {
                    var line = map[i * YVelocity];
                    var x = (xVelocity * i) % line.Length;
                    if (line[x] == '#')
                    {
                        Interlocked.Increment(ref count);
                    }
                };

                totalMultiplier *= count;
            }

            Log.Information("You a multiple of {Count} trees.", totalMultiplier);
        }
    }
}
