namespace Day16_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day16;
    using System.IO;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        private string text;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.text = File.ReadAllText("Inputs/part1.txt");
        }

        [Benchmark]
        public void Part1()
        {
            Part1Solver.Solve(this.text);
        }

        [Benchmark]
        public void Part2()
        {
            Part2Solver.Solve(this.text);
        }
    }
}
