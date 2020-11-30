namespace Day1
{
    using BenchmarkDotNet.Running;
    using Day1_Benchmark;

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<SolverBenchmarks>();
        }
    }
}
