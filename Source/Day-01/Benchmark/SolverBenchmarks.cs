namespace Day1_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day1;
    using System.Threading.Tasks;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        private int[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.data = FileUtil.GetIntArray("Inputs/part1.txt");
        }

        [Benchmark]
        public void Part1()
        {
            ProgramShell.RunSilent(new Part1Solver(2020, this.data));
        }

        [Benchmark]
        public void Part2()
        {
            ProgramShell.RunSilent(new Part2Solver(2020, this.data));
        }
    }
}
