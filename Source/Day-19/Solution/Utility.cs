using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public static class Utility
    {
        public static bool MatchesRule(ReadOnlySpan<char> line, Rule rule)
        {
            foreach (var possibility in rule.Possibilities)
            {
                if (line.SequenceEqual(possibility))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool MatchesRuleRecursive(ReadOnlyMemory<char> line, int ruleIdx, Dictionary<int, Rule> rules)
        {
            return rules[ruleIdx].Matches(line, 0, rules).Any(c => c.Length == 0);
        }

        public static void EvaluateRules(Dictionary<int, Rule> rules)
        {
            var evaluationList = rules.Values.ToList();
            while (evaluationList.Count > 0)
            {
                for (var i = evaluationList.Count - 1; i >= 0; i--)
                {
                    if (evaluationList[i].Evaluate(rules))
                    {
                        evaluationList.RemoveAt(i);
                    }
                }
            }
        }

        public static void ReadRule(Dictionary<int, Rule> rules, ReadOnlySpan<char> line)
        {
            var reader = new SpanStringReader(line);
            var rule = new Rule();
            var ruleIdx = reader.ReadInt();
            rules[ruleIdx] = rule;
            reader.ReadChar();

            var currentBundle = new RuleBundle(new List<ISymbol>());
            while (!reader.IsEndOfFile())
            {
                var symbol = reader.ReadSymbol(true);
                if (symbol[0] == '|')
                {
                    rule.Bundles.Add(currentBundle);
                    currentBundle = new RuleBundle(new List<ISymbol>());
                }
                else if (symbol[0] == '"')
                {
                    currentBundle.Symbols.Add(new CharacterSymbol(symbol[1]));
                }
                else
                {
                    currentBundle.Symbols.Add(new ReferenceSymbol(int.Parse(symbol)));
                }
            }

            rule.Bundles.Add(currentBundle);
        }
    }
}
