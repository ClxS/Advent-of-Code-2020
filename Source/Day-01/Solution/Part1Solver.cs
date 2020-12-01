namespace Day1
{
    using Common;
    using Serilog;
    using System.Threading.Tasks;

    public class Part1Solver : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part1Solver(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public Task SolveAsync()
        {
            for (int i = 0; i < this.inputs.Length; i++)
            {
                for (int j = i + 1; j < this.inputs.Length; j++)
                {
                    if (this.inputs[i] + this.inputs[j] == this.target)
                    {
                        Log.Information("Match: {D} * {E} = {F}", this.inputs[i], this.inputs[j], this.inputs[i] * this.inputs[j]);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
