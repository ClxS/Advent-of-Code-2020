namespace Day14
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day14 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static ulong Solve(string text)
        {
            var memory = new Dictionary<ulong, ulong>();

            var reader = new SpanStringReader(text);
            while (!reader.IsEndOfFile())
            {
                var mask = CommonUtil.GetMask(ref reader);
                while (!reader.IsEndOfFile() && !reader.PeekWord().SequenceEqual("mask"))
                {
                    var (address, value) = CommonUtil.GetMemoryLine(ref reader);
                    foreach(var newAddress in ApplyPermutingMask((ulong)address, mask.ToArray()))
                    {
                        memory[newAddress] = value;
                    }
                }
            }

            var sum = 0UL;
            foreach(var value in memory.Values)
            {
                sum += value;
            }

            return sum;
        }

        private static IEnumerable<ulong> ApplyPermutingMask(ulong value, char[] mask)
        {
            for (var i = mask.Length - 1; i >= 0; i--)
            {
                switch (mask[i])
                {
                    case '0':
                        mask[i] = '.';
                        break;
                }
            }

            var permutations = new List<char[]>()
            {
                mask.ToArray()
            };

            for (var i = mask.Length - 1; i >= 0; i--)
            {
                switch (mask[i])
                {
                    case 'X':
                        var existingCopies = permutations.ToArray();
                        permutations.Clear();

                        foreach (var copy in existingCopies)
                        {
                            copy[i] = '0';
                            permutations.Add(copy.ToArray());
                            copy[i] = '1';
                            permutations.Add(copy.ToArray());
                        }
                        break;
                }
            }

            foreach (var permutation in permutations)
            {
                yield return ApplyMask(value, permutation);
            }
        }

        private static ulong ApplyMask(ulong value, ReadOnlySpan<char> mask)
        {
            for (var i = mask.Length - 1; i >= 0; i--)
            {
                var offset = (mask.Length - 1) - i;
                char maskValue = mask[i];
                switch (maskValue)
                {
                    case '.': break;
                    case '0':
                        value &= ~(1UL << offset);
                        break;
                    case '1':
                        value |= (1UL << offset);
                        break;
                }
            }

            return value;
        }
    }
}
