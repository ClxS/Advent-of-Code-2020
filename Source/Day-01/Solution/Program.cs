namespace Day1
{
    using Common;

    public static class Program
    {
        public static void Main()
        {
            ProgramShell
                .Run(
                    new Part1Solver(2020, FileUtil.GetIntArray("Inputs/part1.txt")),
                    new Part2Solver(2020, FileUtil.GetIntArray("Inputs/part1.txt")));
        }
    }
}
