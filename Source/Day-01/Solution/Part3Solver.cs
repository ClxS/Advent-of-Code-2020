namespace Day1
{
    using Common;
    using Serilog;
    using System.Collections.Generic;

    public class Part3Solver : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part3Solver(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public string Name => "Day1 Part3 (Self Challenge)";

        public void Solve()
        {
            var length = this.inputs.Length;
            var kAndlPairs = new List<int>(GetTriangleNumber(this.inputs.Length));

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    kAndlPairs.Add(this.inputs[i] + this.inputs[j]);
                }
            }

            var store = new IntBucketSet(kAndlPairs);
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

        private int GetTriangleNumber(int length)
        {
            int result = 0;
            for (int i = length; i > 0; i--)
            {
                result += i;
            }

            return result;
        }
    }
}
