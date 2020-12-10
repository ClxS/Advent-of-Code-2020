namespace Template
{
    using Common;
    using Serilog;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Template Part2";

        public void Solve()
        {
            Log.Information("Combinations: {Value}", Solve(this.text));
        }

        public static long Solve(string text)
        {
            var buckets = new StackIntBucketSet(stackalloc int[2048]);
            var reader = new SpanStringReader(text);
            while (!reader.IsEndOfFile())
            {
                buckets.Add(int.Parse(reader.ReadWord(true)));
            }

            var target = buckets.HighestValue;
            buckets.Add(0);
            buckets.Add(target + 3);

            var routesAtNode = new StackLongBucketSet(stackalloc long[target + 5]);
            routesAtNode.Add(0);

            var combinations = 1l;
            for (var i = 0; i <= target; i++)
            {
                if (!buckets.Contains(i))
                {
                    continue;
                }

                var branches = 0;
                var routesAtCurrentNode = routesAtNode[i];
                if (buckets.Contains(i + 1))
                {
                    routesAtNode.AddMultiple(i + 1, routesAtCurrentNode);
                    branches++;
                }

                if (buckets.Contains(i + 2))
                {
                    routesAtNode.AddMultiple(i + 2, routesAtCurrentNode);
                    branches++;
                }

                if (buckets.Contains(i + 3))
                {
                    routesAtNode.AddMultiple(i + 3, routesAtCurrentNode);
                    branches++;
                }

                combinations += routesAtCurrentNode * (branches - 1);
            }

            return combinations;
        }
    }
}
