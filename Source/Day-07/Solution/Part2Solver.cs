namespace Day7
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
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day7 Part2";

        public void Solve()
        {
        }
    }
}
