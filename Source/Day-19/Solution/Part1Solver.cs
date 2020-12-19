namespace Day19
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day19 Part1";

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
                if (line.Length == 0)
                {
                    isReadingRules = false;
                    continue;
                }

                if (isReadingRules)
                {
                    Utility.ReadRule(rules, line.Span);
                }
                else
                {
                    if (Utility.MatchesRuleRecursive(line, 0, rules))
                    {
                        matchCount++;
                    }
                }
            }

            return matchCount;
        }
    }
}
