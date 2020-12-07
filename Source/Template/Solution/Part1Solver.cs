namespace Template
{
    using Common;
    using Serilog;
    using System;
    using System.Linq;

    public class Part1Solver : ISolver
    {
        private readonly string[] lines;

        public Part1Solver(string[] lines)
        {
            this.lines = lines;
        }

        public string Name => "Template Part1";

        public void Solve()
        {
        }
    }
}
