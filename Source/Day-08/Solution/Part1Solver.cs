namespace Day8
{
    using Common;
    using Common.Asm;
    using Serilog;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading;

    public class Part1Solver : ISolver
    {
        private readonly string instructions;

        public Part1Solver(string instructions)
        {
            this.instructions = instructions;
        }

        public string Name => "Day8 Part1";

        public void Solve()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            BitArray visitedInstructions = new BitArray(2048);

            AsmInterpretor interpretor = new AsmInterpretor();
            interpretor.Load(this.instructions);
            interpretor.InstructionMoved += (int instruction) =>
            {
                if (visitedInstructions.Get(instruction))
                {
                    Log.Information("Loop detected. Accumulator is {Accumulator}", interpretor.AccumulatorValue);
                    cts.Cancel();
                }

                visitedInstructions.Set(instruction, true);
            };

            interpretor.Execute(0, cts.Token);
        }
    }
}
