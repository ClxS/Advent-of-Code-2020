namespace Day17
{
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var data = File.ReadAllText("Inputs/part1.txt");
            //new Part1Solver(data).Solve();
            //new Part2Solver(data).Solve();
            new Part2SolverComputeShader(data).Solve();
        }
    }
}
