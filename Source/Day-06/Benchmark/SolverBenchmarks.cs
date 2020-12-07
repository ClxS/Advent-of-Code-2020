namespace Day6_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day6;
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
            ProgramShell.RunSilent(new Part1Solver(this.lines));
        }

        [Benchmark]
        public void Part2()
        {
            ProgramShell.RunSilent(new Part2Solver(this.lines));
        }
    }
}
