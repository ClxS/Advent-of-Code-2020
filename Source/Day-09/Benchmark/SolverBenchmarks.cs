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
            part1Value = new Part1Solver(this.text).GetValue();
        }

        [Benchmark]
        public void Part1()
        {
            ProgramShell.RunSilent(new Part1Solver(this.text));
        }

        [Benchmark]
        public void Part2()
        {
            ProgramShell.RunSilent(new Part2Solver(this.text, this.part1Value));
        }
    }
}
