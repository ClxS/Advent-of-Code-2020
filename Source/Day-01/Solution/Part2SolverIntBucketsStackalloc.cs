namespace Day1
{
    using Common;
    using Serilog;
    using System;

    public class Part2SolverIntBucketsStackalloc : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part2SolverIntBucketsStackalloc(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public string Name => "Day1 Part2 Int Buckets Stackalloc";

        public void Solve()
        {
            var length = this.inputs.Length;
            var store = new IntBucketSet(this.inputs);
            Span<byte> bits = stackalloc byte[2020];

            for (int i = 0; i < length; i++)
            {
                bits[this.inputs[i]] = 1;
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    var requiredValue = this.target - (this.inputs[i] + this.inputs[j]);
                    if (requiredValue < 0 || requiredValue >= 2020)
                    {
                        continue;
                    }

                    if (bits[requiredValue] == 0)
                    {
                        continue;
                    }

                    Log.Information("Match: {A} * {B} * {C} = {D}", this.inputs[i], this.inputs[j], requiredValue, this.inputs[i] * this.inputs[j] * requiredValue);
                }
            }
        }
    }
}
