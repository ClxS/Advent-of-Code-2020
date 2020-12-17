namespace Day17
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var data = File.ReadAllText("Inputs/part1.txt");
            new Part1Solver(data).Solve();
            new Part2Solver(data).Solve();
            new Part2SolverComputeShader(data).Solve();
        }

        private static void TimeComputeSolver()
        {
            var data = File.ReadAllText("Inputs/part1.txt");
            for (int i = 0; i < 10; i++)
            {
                Part2SolverComputeShader.DryRunShader();
            }

            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 1000; i++)
            {
                Part2SolverComputeShader.Solve(data);
            }

            watch.Stop();
            var time = watch.ElapsedMilliseconds / 1000.0;
            Console.WriteLine(time);
        }
    }
}
