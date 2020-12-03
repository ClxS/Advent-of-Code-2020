namespace Day2
{
    using Common;
    using Day3;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var data = File.ReadAllLines("Inputs/part1.txt");
            ProgramShell
                .Run(
                    new Part1Solver(data, (3, 1)),
                    new Part2Solver(data, (1,1), (3, 1), (5, 1), (7, 1), (1, 2)));
        }
    }
}
