namespace Day7_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day7;
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
        public void Part1And2()
        {
            ProgramShell.RunSilent(new Day7Solver(this.text));
        }

        //[Benchmark]
        //public void Part2()
        //{
        //    //ProgramShell.RunSilent(new Part2Solver(this.lines));
        //}
    }
}
