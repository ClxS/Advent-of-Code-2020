namespace Common
{
    using Serilog;
    using System.Threading.Tasks;

    public static class ProgramShell
    {
        public static Task RunAsync(params ISolver[] solvers)
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .CreateLogger();

            return RunCommonAsync(solvers);
        }

        public static Task RunSilentAsync(params ISolver[] solvers)
        {
            return RunCommonAsync(solvers);
        }

        private static async Task RunCommonAsync(params ISolver[] solvers)
        {
            foreach (var solver in solvers)
            {
                Log.Information("Solver: {Solver}", solver.Name);
                await solver.SolveAsync().ConfigureAwait(false);
            }
        }
    }
}
