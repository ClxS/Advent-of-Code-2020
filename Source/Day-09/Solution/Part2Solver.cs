namespace Template
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

        public string Name => "Template Part2";

        public void Solve()
        {
        }
    }
}
