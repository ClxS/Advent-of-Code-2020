namespace Template
{
    using Common;
    using Serilog;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Template Part1";

        public void Solve()
        {
            Log.Information("Differences: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            var buckets = new StackIntBucketSet(stackalloc int[2048]);
            var reader = new SpanStringReader(text);
            while(!reader.IsEndOfFile())
            {
                buckets.Add(int.Parse(reader.ReadWord(true)));
            }

            var differentials = new StackIntBucketSet(stackalloc int[4]);
            var lastValue = buckets.HighestValue + 3;
            var i = 0;
            for(i = lastValue; i >= 0; i--)
            {
                if (buckets.Contains(i))
                {
                    differentials.Add(lastValue - i);
                    lastValue = i;
                }
            }

            differentials.Add(lastValue);
            return differentials[1] * differentials[3];
        }
    }
}
