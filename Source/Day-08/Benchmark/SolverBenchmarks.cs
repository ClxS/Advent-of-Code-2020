namespace Day8_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day8;
    using System.IO;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        private string instructions;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.instructions = File.ReadAllText("Inputs/part1.txt");
        }

        [Benchmark]
        public void Part1()
        {
            ProgramShell.RunSilent(new Part1Solver(this.instructions));
        }

        [Benchmark]
        public void Part2()
        {
            ProgramShell.RunSilent(new Part2Solver(this.instructions));
        }
    }
}
