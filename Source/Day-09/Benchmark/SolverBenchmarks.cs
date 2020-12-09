namespace Day9_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day9;
    using System.IO;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        private string text;
        private long part1Value;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.text = File.ReadAllText("Inputs/part1.txt");
            part1Value = Part1Solver.Solve(this.text);
        }

        [Benchmark]
        public void Part1()
        {
            Part1Solver.Solve(this.text);
        }

        [Benchmark]
        public void Part2()
        {
            Part2Solver.Solve(this.text, this.part1Value);
        }
    }
}
