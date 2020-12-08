namespace Common.Asm
{
    public record AsmInstruction
    {
        public Opcode OpCode;

        public int Operand;

        public AsmInstruction(Opcode opcode, int operand) => (OpCode, Operand) = (opcode, operand);
    }
}
