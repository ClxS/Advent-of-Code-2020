namespace Common
{
    using System.Threading.Tasks;

    public static class ProgramShell
    {
        public static Task RunAsync(params ISolver[] solvers)
        {
            foreach (var solver in solvers)
            {
                await solver.SolveAsync().ConfigureAwait(false);
            }
        }
    }
}
