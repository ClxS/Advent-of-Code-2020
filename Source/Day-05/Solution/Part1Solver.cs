namespace Day5
{
    using Common;
    using Serilog;
    using System;
    using System.Linq;

    public class Part1Solver : ISolver
    {
        private readonly string[] lines;

        public Part1Solver(string[] lines)
        {
            this.lines = lines;
        }

        public string Name => "Day5 Part1";

        public void Solve()
        {
            var highestSeatId = 0;
            foreach (ReadOnlySpan<char> line in this.lines)
            {
                var row = BinarySearch(line.Slice(0, 7), 128);
                var seat = BinarySearch(line[7..], 8);

                var seatId = (row * 8) + seat;
                if (seatId > highestSeatId)
                {
                    highestSeatId = seatId;
                }
            }

            Log.Information("Highest SeatId: {SeatId}", highestSeatId);
        }

        private int BinarySearch(ReadOnlySpan<char> chars, int length)
        {
            var pivotA = 0;
            var pivotB = length - 1;

            foreach(var @char in chars)
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
