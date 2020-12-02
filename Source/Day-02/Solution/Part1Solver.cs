namespace Day2
{
    using Common;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part1Solver : ISolver
    {
        private readonly Password[] passwords;

        public Part1Solver(Password[] passwords)
        {
            this.passwords = passwords;
        }

        public string Name => "Day2 Part1";

        public void Solve()
        {
            int count = 0;
            Parallel.ForEach(passwords, password => ProcessPassword(password, ref count));

            Log.Information("{Count} passwords were valid", count);
        }

        private void ProcessPassword(Password password, ref int count)
        {
            var pw = password.PasswordString;
            var pwLength = pw.Length;
            Span<byte> charBucket = stackalloc byte[26];
            for (int i = 0; i < pwLength; i++)
            {
                charBucket[pw[i] - 'a']++;
            }

            var targetCharCount = charBucket[password.Character - 'a'];
            if (targetCharCount >= password.MinimumRepetition && targetCharCount <= password.MaximumRepetition)
            {
                Interlocked.Increment(ref count);
            }
        }
    }
}
