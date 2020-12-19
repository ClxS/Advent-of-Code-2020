namespace Day19
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day19 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            var rules = new Dictionary<int, Rule>();
            bool isReadingRules = true;
            var matchCount = 0;
            foreach (var line in text.SplitLinesAsMemory())
            {
                var lineUsed = line;
                if (lineUsed.Length == 0)
                {
                    isReadingRules = false;
                    Utility.ReadRule(rules, "8: 42 | 42 8");
                    Utility.ReadRule(rules, "11: 42 31 | 42 11 31");
                    continue;
                }

                if (isReadingRules)
                {
                    Utility.ReadRule(rules, lineUsed.Span);
                }
                else
                {
                    if (Utility.MatchesRuleRecursive(lineUsed, 0, rules))
                    {
                        matchCount++;
                    }
                }
            }

            return matchCount;
        }
    }
}
