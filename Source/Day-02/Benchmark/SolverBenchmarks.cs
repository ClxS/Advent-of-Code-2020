namespace Day2_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day2;
    using System.IO;
    using System.Threading.Tasks;

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
        public void ComposePasswords()
        {
            Password.GetPasswords(lines);
        }

        [Benchmark]
        public void Part1()
        {
            ProgramShell.RunSilent(new Part1Solver(Password.GetPasswords(lines)));
        }

        [Benchmark]
        public void Part2()
        {
            ProgramShell.RunSilent(new Part2Solver(Password.GetPasswords(lines)));
        }
    }
}
