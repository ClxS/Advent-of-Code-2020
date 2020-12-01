namespace Day1
{
    using Common;
    using Serilog;
    using System.Collections.Generic;
    using System.Linq;

    public class Part2SolverLinq : ISolver
    {
        private readonly int target;
        private readonly int[] inputs;

        public Part2SolverLinq(int target, params int[] inputs)
        {
            this.target = target;
            this.inputs = inputs;
        }

        public string Name => "Day1 Part2 LinqHorror";

        public void Solve()
        {
            var store = this.inputs.ToHashSet();

            var match =
                from idx1 in Enumerable.Range(0, this.inputs.Length)
                from idx2 in Enumerable.Range(idx1, this.inputs.Length - idx1)
                where store.Contains(this.target - (this.inputs[idx1] + this.inputs[idx2]))
                select (this.inputs[idx1] * this.inputs[idx2] * (this.target - (this.inputs[idx1] + this.inputs[idx2])));

            if (match.Any())
            {
                Log.Information("{D}", match.First());
            }
        }
    }
}
