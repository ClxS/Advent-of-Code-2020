namespace Day8
{
    using Common;
    using Common.Asm;
    using Serilog;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading;

    public class Part2Solver : ISolver
    {
        private readonly string instructions;

        public Part2Solver(string instructions)
        {
            this.instructions = instructions;
        }

        public string Name => "Day8 Part2";

        public void Solve()
        {            
            AsmInterpretor interpretor = new AsmInterpretor();

            interpretor.Load(this.instructions);
            var instructions = interpretor.Instructions;
            var instructionCount = interpretor.InstructionCount;

            CancellationTokenSource cts = default;
            BitArray visitedInstructions = default;
            interpretor.InstructionMoved += (int instruction) =>
            {
                if (visitedInstructions.Get(instruction))
                {
                    cts.Cancel();
                }

                visitedInstructions.Set(instruction, true);
            };

            for (int i = 0; i < instructionCount; i++)
            {
                if (instructions[i].OpCode != Opcode.Jmp && instructions[i].OpCode != Opcode.Nop)
                {
                    continue;
                }

                var originalInstruction = instructions[i];
                instructions[i] = originalInstruction with { OpCode = instructions[i].OpCode == Opcode.Jmp ? Opcode.Nop : Opcode.Jmp };

                cts = new CancellationTokenSource();
                visitedInstructions = new BitArray(2048);

                interpretor.Execute(0, cts.Token);
                if (!cts.IsCancellationRequested)
                {
                    Log.Information("Loop fix detected. Accumulator is {Accumulator}", interpretor.AccumulatorValue);
                    break;
                }

                instructions[i] = originalInstruction;
            }
        }
    }
}
