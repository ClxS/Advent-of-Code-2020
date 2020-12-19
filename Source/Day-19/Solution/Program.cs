namespace Day19
{
    using Common;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var data = File.ReadAllText("Inputs/part1.txt");
            ProgramShell
                .Run(
                    new Part1Solver(data),
                    new Part2NaiveParserSolver(data),
                    new Part2RegexSolver(data));
        }
    }
}
