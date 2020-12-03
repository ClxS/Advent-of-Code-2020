namespace Day2_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day3;
    using System.IO;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        private string[] lines;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.lines = File.ReadAllLines("Inputs/part1.txt");
        }

        [Benchmark]
        public void Part1()
        {
            ProgramShell.RunSilent(new Part1Solver(this.lines, (3, 1)));
        }

        [Benchmark]
        public void Part2()
        {
            ProgramShell.RunSilent(new Part2Solver(this.lines, (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)));
        }
    }
}
