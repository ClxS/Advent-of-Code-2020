namespace Common
{
    using Serilog;
    using System.Threading.Tasks;

    public static class ProgramShell
    {
        public static async Task RunAsync(params ISolver[] solvers)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            foreach (var solver in solvers)
            {
                await solver.SolveAsync().ConfigureAwait(false);
            }
        }
    }
}
