namespace Day9
{
    using Common;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var data = File.ReadAllText("Inputs/part1.txt");
            var value = new Part1Solver(data).GetValue();
            ProgramShell
                .Run(
                    new Part1Solver(data),
                    new Part2Solver(data, value));
        }
    }
}
