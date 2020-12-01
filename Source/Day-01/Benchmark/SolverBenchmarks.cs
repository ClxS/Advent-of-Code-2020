namespace Day1_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Common;
    using Day1;
    using System.Threading.Tasks;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        [Benchmark]
        public async Task Part1Async()
        {
            await ProgramShell
                .RunSilentAsync(new Part1Solver(2020, FileUtil.GetIntArray("Inputs/part1.txt")))
                .ConfigureAwait(false);
        }
        [Benchmark]
        public async Task Part2Async()
        {
            await ProgramShell
                .RunSilentAsync(new Part2Solver(2020, FileUtil.GetIntArray("Inputs/part1.txt")))
                .ConfigureAwait(false);
        }
    }
}
