namespace Day1_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using System.Threading.Tasks;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        [Benchmark]
        public Task Part1Async()
        {
            return Task.CompletedTask;
        }
    }
}
