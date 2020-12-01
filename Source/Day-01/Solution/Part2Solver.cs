namespace Day1
{
    using Common;
    using Serilog;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part2Solver(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public string Name => "Day1 Part2";

        public void Solve()
        {
            var length = this.inputs.Length;
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    for (int k = j + 1; k < length; k++)
                    {
                        if (this.inputs[i] + this.inputs[j] + this.inputs[k] == this.target)
                        {
                            Log.Information("Match: {A} * {B} * {C} = {D}", this.inputs[i], this.inputs[j], this.inputs[k], this.inputs[i] * this.inputs[j] * this.inputs[k]);
                            return;
                        }
                    }
                }
            }
        }
    }
}
