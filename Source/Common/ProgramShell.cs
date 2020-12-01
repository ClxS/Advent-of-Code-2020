namespace Common
{
    using Serilog;
    using System.Threading.Tasks;

    public static class ProgramShell
    {
        public static void Run(params ISolver[] solvers)
        {
            ConfigureLogger();
            RunCommon(solvers);
        }

        public static void RunSilent(params ISolver[] solvers)
        {
            RunCommon(solvers);
        }

        public static Task RunAsync(params IAsyncSolver[] solvers)
        {
            ConfigureLogger();
            return RunCommonAsync(solvers);
        }

        public static Task RunSilentAsync(params IAsyncSolver[] solvers)
        {
            return RunCommonAsync(solvers);
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .CreateLogger();
        }

        private static void RunCommon(params ISolver[] solvers)
        {
            foreach (var solver in solvers)
            {
                Log.Information("Solver: {Solver}", solver.Name);
                solver.Solve();
            }
        }

        private static async Task RunCommonAsync(params IAsyncSolver[] solvers)
        {
            foreach (var solver in solvers)
            {
                Log.Information("Solver: {Solver}", solver.Name);
                await solver.SolveAsync().ConfigureAwait(false);
            }
        }
    }
}
