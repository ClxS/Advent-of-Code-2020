namespace Day17_Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using Day17;
    using System.IO;

    [MemoryDiagnoser]
    public class SolverBenchmarks
    {
        private string text;

        [GlobalSetup]
        public void GlobalSetup()
        {
            this.text = File.ReadAllText("Inputs/part1.txt");
            Part2SolverComputeShader.DryRunShader();
        }

        [Benchmark]
        public void Part1()
        {
            Part1Solver.Solve(this.text);
        }

        [Benchmark]
        public void Part2()
        {
            Part2Solver.Solve(this.text);
        }

        [Benchmark]
        public void Part2ComputeShader()
        {
            Part2SolverComputeShader.Solve(this.text);
        }
    }
}
