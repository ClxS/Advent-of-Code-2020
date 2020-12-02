namespace Day1
{
    using Common;
    using Serilog;
    using System;

    public class Part2SolverIntBuckets : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part2SolverIntBuckets(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public string Name => "Day1 Part2 Int Buckets";

        public void Solve()
        {
            Array.Sort(this.inputs);

            var length = this.inputs.Length;
            var store = new IntBucketSet(this.inputs);

            for (int i = length - 1; i >= 0; i--)
            {
                var iValue = this.inputs[i];
                for (int j = length - 1; j >= i + 1; j--)
                {
                    var jValue = this.inputs[j];
                    var requiredValue = this.target - (iValue + jValue);
                    if (!store.Contains(requiredValue))
                    {
                        continue;
                    }

                    Log.Information("Match: {A} * {B} * {C} = {D}", iValue, jValue, requiredValue, iValue * jValue * requiredValue);
                    return;
                }
            }
        }
    }
}
