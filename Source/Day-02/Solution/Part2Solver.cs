namespace Day2
{
    using Common;
    using Serilog;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly Password[] passwords;

        public Part2Solver(Password[] passwords)
        {
            this.passwords = passwords;
        }

        public string Name => "Day2 Part2";

        public void Solve()
        {
            int count = 0;
            Parallel.ForEach(passwords, password => ProcessPassword(password, ref count));

            Log.Information("{Count} passwords were valid", count);
        }

        private void ProcessPassword(Password password, ref int count)
        {
            var target = password.Character;
            var pw = password.PasswordString;
            var pwLength = pw.Length;

            if (password.MaximumRepetition > pwLength || password.MinimumRepetition > pwLength)
            {
                return;
            }

            if ((pw[password.MinimumRepetition - 1] == target) ^ (pw[password.MaximumRepetition - 1] == target))
            {
                Interlocked.Increment(ref count);
            }
        }
    }
}
