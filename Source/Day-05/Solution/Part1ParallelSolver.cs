namespace Day5
{
    using Common;
    using Serilog;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part1ParallelSolver : ISolver
    {
        private readonly string[] lines;

        public Part1ParallelSolver(string[] lines)
        {
            this.lines = lines;
        }

        public string Name => "Day5 Part1";

        public void Solve()
        {
            var highestSeatId = 0;
            Parallel.ForEach(this.lines, (line) =>
            {
                var lineSpan = line.AsSpan();
                var row = BinarySearch(lineSpan.Slice(0, 7), 128);
                var seat = BinarySearch(lineSpan[7..], 8);

                var seatId = (row * 8) + seat;

                var initialValue = highestSeatId;
                do
                {
                    initialValue = highestSeatId;
                    if (seatId <= highestSeatId)
                    {
                        break;
                    }
                }
                while (Interlocked.CompareExchange(ref highestSeatId, seatId, initialValue) != initialValue);
                
            });

            Log.Information("Highest SeatId: {SeatId}", highestSeatId);
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
