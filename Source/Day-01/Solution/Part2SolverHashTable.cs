namespace Day1
{
    using Common;
    using Serilog;
    using System.Collections.Generic;

    public class Part2SolverHashSet : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part2SolverHashSet(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public string Name => "Day1 Part2 HashTable";

        public void Solve()
        {
            var store = new HashSet<int>();
            var length = this.inputs.Length;
            for (int i = 0; i < length; i++)
            {
                store.Add(this.inputs[i]);
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    var requiredValue = this.target - (this.inputs[i] + this.inputs[j]);
                    if (!store.Contains(requiredValue))
                    {
                        continue;
                    }

                    Log.Information("Match: {A} * {B} * {C} = {D}", this.inputs[i], this.inputs[j], requiredValue, this.inputs[i] * this.inputs[j] * requiredValue);
                }
            }
        }
    }
}
