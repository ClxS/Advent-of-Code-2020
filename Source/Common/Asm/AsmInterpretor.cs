namespace Common.Asm
{
    using System;
    using System.Threading;

    public class AsmInterpretor
    {
        public delegate void InstructionMovedEventHandler(int instruction);

        private readonly int stackSize;

        public event InstructionMovedEventHandler InstructionMoved;

        public AsmInterpretor(int stackSize = 2048)
        {
            this.stackSize = stackSize;
            this.Instructions = new AsmInstruction[this.stackSize];
        }

        public AsmInstruction[] Instructions { get; private set; }

        public int InstructionCount { get; private set; }

        public int AccumulatorValue { get; private set; }

        public void Load(string instructionData)
        {
            int opIdx = 0;
            foreach (var line in instructionData.SplitLines())
            {
                ReadOnlySpan<char> op = default;
                int accumulator = default;
                foreach (var word in line.SplitAsSpans(" "))
                {
                    if (op == default)
                    {
                        op = word;
                    }
                    else
                    {
                        accumulator = int.Parse(word[1..]);
                        if (word[0] == '-')
                        {
                            accumulator = -accumulator;
                        }
                    }
                }

                this.Instructions[opIdx++] = new AsmInstruction(GetOpCode(op), accumulator);
            }

            this.InstructionCount = opIdx;
        }

        public void Load(AsmInstruction[] instructions, int instructionCount)
        {
            this.Instructions = instructions;
            this.InstructionCount = instructionCount;
        }

        public void Execute(int instructionPointer = 0, CancellationToken cancellationToken = default)
        {
            this.AccumulatorValue = 0;
            while (instructionPointer < InstructionCount && !cancellationToken.IsCancellationRequested)
            {
                AsmInstruction instruction = Instructions[instructionPointer];
                switch (instruction.OpCode)
                {
                    case Opcode.Nop:
                        instructionPointer++;
                        OnInstructionMoved(instructionPointer);
                        break;
                    case Opcode.Acc:
                        AccumulatorValue += instruction.Operand;
                        instructionPointer++;
                        OnInstructionMoved(instructionPointer);
                        break;
                    case Opcode.Jmp:
                        instructionPointer += instruction.Operand;
                        OnInstructionMoved(instructionPointer);
                        break;
                }
            }
        }

        protected void OnInstructionMoved(int pointer)
        {
            InstructionMoved?.Invoke(pointer);
        }

        private Opcode GetOpCode(ReadOnlySpan<char> op)
        {
            if (op.Equals("nop", StringComparison.Ordinal))
            {
                return Opcode.Nop;
            }

            if (op.Equals("acc", StringComparison.Ordinal))
            {
                return Opcode.Acc;
            }

            if (op.Equals("jmp", StringComparison.Ordinal))
            {
                return Opcode.Jmp;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
