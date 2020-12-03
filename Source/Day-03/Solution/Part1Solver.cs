namespace Day3
{
    using Common;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part1Solver : ISolver
    {
        private readonly string[] map;
        private readonly (int X, int Y) movement;

        public Part1Solver(string[] map, (int X, int Y) movement)
        {
            this.map = map;
            this.movement = movement;
        }

        public string Name => "Day3 Part1";

        public void Solve()
        {
            var count = 0;
            var xVelocity = this.movement.X;
            Parallel.For(0, map.Length, i =>
            {
                var line = map[i];
                var x = (xVelocity * i) % line.Length;
                if (line[x] == '#')
                {
                    Interlocked.Increment(ref count);
                }
            });

            Log.Information("You hit {Count} trees.", count);
        }
    }
}
