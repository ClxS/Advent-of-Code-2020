namespace Day1
{
    using Common;
    using Serilog;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task Main()
        {
            Log.Information("Part 1");
            await ProgramShell
                .RunAsync(new Part1Solver(2020, FileUtil.GetIntArray("Inputs/part1.txt")))
                .ConfigureAwait(false);

            Log.Information("Part 2");
            await ProgramShell
                .RunAsync(new Part2Solver(2020, FileUtil.GetIntArray("Inputs/part1.txt")))
                .ConfigureAwait(false);
        }
    }
}
