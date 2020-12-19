namespace Day19
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Part2RegexSolver : ISolver
    {
        private const int iterationMax = 5;

        private readonly string text;
        private static readonly ReadOnlyMemory<char>[] rules = new ReadOnlyMemory<char>[256];
        private static readonly string[] regex = new string[256];

        public Part2RegexSolver(string text)
        {
            this.text = text;
        }

        public string Name => "Day19 Part2 Regex Parser";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static int Solve(string text)
        {
            bool isReadingRules = true;
            var matchCount = 0;
            string regex = null;
            foreach (var line in text.SplitLinesAsMemory())
            {
                var lineUsed = line;
                if (lineUsed.Length == 0)
                {
                    isReadingRules = false;
                    SetPart2Rules();
                    regex = $"^{GetRuleRegexRecursive(0)}$";
                    continue;
                }

                if (isReadingRules)
                {
                    var splitIdx = line.Span.IndexOf(":");
                    var ruleIdx = int.Parse(line.Span.Slice(0, splitIdx));
                    rules[ruleIdx] = line[(splitIdx + 2)..];                    
                }
                else
                {
                    if (Regex.IsMatch(new string(line.Span), regex))
                    {
                        matchCount++;
                    }
                }
            }

            return matchCount;
        }

        private static string GetRuleRegexRecursive(int rule)
        {
            if (regex[rule] != null)
            {
                return regex[rule];
            }

            var line = rules[rule];
            if (line.Span[0] == '"')
            {
                return line.Span.Slice(1, 1).ToString();
            }

            var result = "(";
            foreach (var part in line.SplitAsSpansAsMemory(new[] { ' ' }))
            {
                if (int.TryParse(part.Span, out int n))
                {
                    result += GetRuleRegexRecursive(n);
                }
                else if (part.Span[0] == '|')
                {
                    result += '|';
                }
            }
            result += ')';

            regex[rule] = result;
            return result;
        }

        private static void SetPart2Rules()
        {
            regex[8] = $"({GetRuleRegexRecursive(42)}+)";

            var rule42 = GetRuleRegexRecursive(42);
            var rule31 = GetRuleRegexRecursive(31);
            string result = "(";
            for (int i = 1; i < iterationMax; i++)
            {
                if (i > 1)
                {
                    result += '|';
                }

                result += '(';
                for (int j = 0; j < i; j++)
                {
                    result += rule42;
                }
                for (int j = 0; j < i; j++)
                {
                    result += rule31;
                }

                result += ')';
            }

            result += ')';
            regex[11] = result;
            regex[0] = null;
        }
    }
}
