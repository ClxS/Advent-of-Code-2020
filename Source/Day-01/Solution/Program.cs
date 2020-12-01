namespace Day1
{
    using Common;

    public static class Program
    {
        public static void Main()
        {
            var data = FileUtil.GetIntArray("Inputs/part1.txt");
            ProgramShell
                .Run(
                    new Part1Solver(2020, data),
                    new Part2Solver(2020, data),
                    new Part2SolverHashSet(2020, data),
                    new Part2SolverIntBuckets(2020, data),
                    new Part2SolverIntBucketsStackalloc(2020, data), 
                    new Part2SolverLinq(2020, data)
                    new Part3Solver(2020, data));
        }
    }
}
