namespace Day1
{
    using Common;
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task MainAsync()
        {
            await ProgramShell
                .RunAsync(new Part1Solver())
                .ConfigureAwait(false);
        }
    }
}
