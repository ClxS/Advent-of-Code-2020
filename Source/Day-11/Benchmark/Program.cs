namespace Day11_Benchmark
{
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<SolverBenchmarks>();
        }
    }
}
