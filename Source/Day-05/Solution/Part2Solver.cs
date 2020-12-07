namespace Day5
{
    using Common;
    using Serilog;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly string[] lines;

        public Part2Solver(string[] lines)
        {
            this.lines = lines;
        }

        public string Name => "Day5 Part2";

        public void Solve()
        {
            var seats = new uint[128];
            seats[0] = 0b1111_1111;

            Parallel.ForEach(this.lines, (line) =>
            {
                var lineSpan = line.AsSpan();
                var row = BinarySearch(lineSpan.Slice(0, 7), 128);
                var seat = BinarySearch(lineSpan[7..], 8);

                var initialValue = seats[row];
                var newValue = initialValue | 1u << seat;
                do
                {
                    initialValue = seats[row];
                    newValue = initialValue | 1u << seat;
                }
                while (Interlocked.CompareExchange(ref seats[row], newValue, initialValue) != initialValue);
            });

            for(int row = 7; row < seats.Length; ++row)
            {
                var rowSeats = seats[row];
                if (BitOperations.PopCount(rowSeats) >= 8)
                {
                    continue;
                }

                for (int seat = 0; seat < 8; seat++)
                {
                    if ((rowSeats & (1u << seat)) == 0)
                    {
                        var seatId = (row * 8) + seat;
                        Log.Information("Your SeatId: {SeatId}", seatId);
                        return;
                    }
                }

                Debug.Assert(false, "Shouldn't hit here");
            }
        }

        private int BinarySearch(ReadOnlySpan<char> chars, int length)
        {
            var pivotA = 0;
            var pivotB = length - 1;

            foreach (var @char in chars)
            {
                var distance = pivotB - pivotA;
                if (@char == 'F' || @char == 'L')
                {
                    pivotB -= (int)Math.Ceiling(distance / 2.0);
                }
                else
                {
                    pivotA += (int)Math.Ceiling(distance / 2.0);
                }
            }

            return pivotA;
        }
    }
}
