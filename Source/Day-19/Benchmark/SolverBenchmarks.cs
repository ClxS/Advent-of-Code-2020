namespace Day19_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day19;
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
        public void Part1NaiveParser()
        {
            Part1Solver.Solve(this.text);
        }

        [Benchmark]
        public void Part2NaiveParser()
        {
            Part2NaiveParserSolver.Solve(this.text);
        }

        [Benchmark]
        public void Part2RegexParser()
        {
            Part2RegexSolver.Solve(this.text);
        }
    }
}
