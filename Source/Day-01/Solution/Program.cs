namespace Day1
{
    using Common;
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task MainAsync()
        {
            var solvers = new ISolver[]
            {
                new Part1Solver(),
            };

            foreach (var solver in solvers)
            {
                await solver.SolveAsync().ConfigureAwait(false);
            }
        }
    }
}
