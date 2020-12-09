namespace Template
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

        public string Name => "Template Part1";

        public void Solve()
        {
        }
    }
}
