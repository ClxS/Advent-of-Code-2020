namespace Day8
{
    using Common;
    using Common.Asm;
    using Serilog;
    using System.Collections;

    public class Part1Solver : ISolver
    {
        private readonly string instructions;
        private readonly BitArray visitedInstructions = new BitArray(2048);

        public Part1Solver(string instructions)
        {
            this.instructions = instructions;
        }

        public string Name => "Day8 Part1";

        public void Solve()
        {
            AsmInterpretor interpretor = new AsmInterpretor();
            interpretor.Load(this.instructions);
            interpretor.InstructionMoved += Interpretor_InstructionMoved;
            interpretor.Execute(0);
        }

        private void Interpretor_InstructionMoved(AsmInterpretor interpretor, int instruction)
        {
            if (visitedInstructions.Get(instruction))
            {
                Log.Information("Loop detected. Accumulator is {Accumulator}", interpretor.AccumulatorValue);
                interpretor.Stop();
            }

            visitedInstructions.Set(instruction, true);
        }
    }
}
