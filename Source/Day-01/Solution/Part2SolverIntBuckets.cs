namespace Day1
{
    using Common;
    using Serilog;

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
            var length = this.inputs.Length;
            var store = new IntBucketSet(this.inputs);

            for (int i = 0; i < length; i++)
            {
                var iValue = this.inputs[i];
                for (int j = i + 1; j < length; j++)
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
