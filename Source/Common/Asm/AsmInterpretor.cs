namespace Common.Asm
{
    using System;
    using System.IO;
    using System.Threading;

    public class AsmInterpretor
    {
        private bool stop = false;
        private int instructionCount;
        private int accumulatorValue;
        private AsmInstruction[] instructions;

        public delegate void InstructionMovedEventHandler(AsmInterpretor interpretor, int instruction);

        private readonly int stackSize;

        public event InstructionMovedEventHandler InstructionMoved;

        public AsmInterpretor(int stackSize = 2048)
        {
            this.stackSize = stackSize;
            this.Instructions = new AsmInstruction[this.stackSize];
        }

        public AsmInstruction[] Instructions { get => instructions; private set => instructions = value; }

        public int InstructionCount { get => instructionCount; private set => instructionCount = value; }

        public int AccumulatorValue { get => accumulatorValue; private set => accumulatorValue = value; }

        public void Load(string instructionData)
        {
            int opIdx = 0;

            var reader = new SpanStringReader(instructionData);
            ReadOnlySpan<char> value;
            while ((value = reader.ReadWord()) != default)
            {
                var factor = reader.ReadChar();
                var operand = reader.ReadWord();

                var operandValue = int.Parse(operand);
                if (factor == '-')
                {
                    operandValue = -operandValue;
                }

                this.Instructions[opIdx++] = new AsmInstruction(GetOpCode(value), operandValue);
            }

            this.InstructionCount = opIdx;
        }

        public void Load(AsmInstruction[] instructions, int instructionCount)
        {
            this.InstructionCount = instructionCount;
            this.instructions = new AsmInstruction[instructionCount];
            Array.Copy(instructions, this.instructions, instructionCount);
        }

        public void Stop()
        {
            this.stop = true;
        }

        public bool Execute(int instructionPointer = 0)
        {
            this.accumulatorValue = 0;
            this.stop = false;
            while (instructionPointer < instructionCount && !this.stop)
            {
                AsmInstruction instruction = instructions[instructionPointer];
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

            return !this.stop;
        }

        protected void OnInstructionMoved(int pointer)
        {
            InstructionMoved?.Invoke(this, pointer);
        }

        private Opcode GetOpCode(ReadOnlySpan<char> op)
        {
            if (op.SequenceEqual("nop"))
            {
                return Opcode.Nop;
            }

            if (op.SequenceEqual("acc"))
            {
                return Opcode.Acc;
            }

            if (op.SequenceEqual("jmp"))
            {
                return Opcode.Jmp;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
