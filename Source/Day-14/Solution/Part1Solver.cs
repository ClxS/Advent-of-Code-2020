namespace Day14
{
    using Common;
    using global::Common;
    using Serilog;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day14 Part1";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static ulong Solve(string text)
        {
            var memory = new ulong[64 * 1024];

            var reader = new SpanStringReader(text);
            while (!reader.IsEndOfFile())
            {
                var mask = CommonUtil.GetMask(ref reader);
                while (!reader.IsEndOfFile() && !reader.PeekWord().SequenceEqual("mask"))
                {
                    var (address, value) = CommonUtil.GetMemoryLine(ref reader);
                    value = ApplyMask(value, mask);
                    memory[address] = value;
                }
            }

            var sum = 0UL;
            for(var i = 0; i < memory.Length; ++i)
            {
                sum += memory[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong ApplyMask(ulong value, ReadOnlySpan<char> mask)
        {
            for(var i = mask.Length - 1; i >= 0; i--)
            {
                var offset = (mask.Length - 1) - i;
                char maskValue = mask[i];
                switch (maskValue)
                {
                    case 'X': break;
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
