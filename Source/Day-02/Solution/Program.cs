namespace Day2
{
    using Common;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var data = File.ReadAllLines("Inputs/part1.txt");
            var passwords = Password.GetPasswords(data);
            ProgramShell
                .Run(
                    new Part1Solver(passwords),
                    new Part2Solver(passwords));
        }     
    }
}
