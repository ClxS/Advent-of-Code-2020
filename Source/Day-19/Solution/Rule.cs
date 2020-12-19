namespace Day19
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Rule
    {
        public List<RuleBundle> Bundles { get; } = new List<RuleBundle>();

        public string[] Possibilities { get; private set; }

        public bool IsEvaluated => this.Possibilities != null;

        public bool Evaluate(Dictionary<int, Rule> rules)
        {
            if (!this.Bundles.All(b => b.Symbols.OfType<ReferenceSymbol>().All(r => rules[r.Number].IsEvaluated)))
            {
                return false;
            }

            var bundles = new List<StringBuilder>();
            foreach (var bundle in this.Bundles)
            {
                var builders = new List<StringBuilder>();
                builders.Add(new StringBuilder());

                foreach (var symbol in bundle.Symbols)
                {
                    switch (symbol)
                    {
                        case CharacterSymbol characterSymbol:
                            foreach (var builder in builders)
                            {
                                builder.Append(characterSymbol.Character);
                            }
                            break;
                        case ReferenceSymbol referenceSymbol:
                            var otherRule = rules[referenceSymbol.Number];
                            if (otherRule.Possibilities.Length > 1)
                            {
                                var copies = builders.Select(b => b.ToString()).ToArray();
                                foreach (var builder in builders)
                                {
                                    builder.Append(otherRule.Possibilities[0]);
                                }

                                for (var i = 1; i < otherRule.Possibilities.Length; i++)
                                {
                                    foreach (var copy in copies)
                                    {
                                        builders.Add(new StringBuilder(copy + otherRule.Possibilities[i]));
                                    }
                                }
                            }
                            else
                            {
                                foreach (var builder in builders)
                                {
                                    builder.Append(otherRule.Possibilities[0]);
                                }
                            }

                            break;
                    }
                }

                bundles.AddRange(builders);
            }

            this.Possibilities = bundles.Select(b => b.ToString()).ToArray();

            return true;
        }

        internal IEnumerable<ReadOnlyMemory<char>> Matches(ReadOnlyMemory<char> line, int ownId, Dictionary<int, Rule> rules)
        {
            var listA = new List<ReadOnlyMemory<char>>();
            var listB = new List<ReadOnlyMemory<char>>();

            foreach(var branch in this.Bundles)
            {
                listA.Clear();
                listB.Clear();
                listA.Add(line);

                var readList = listA;
                var writeList = listB;

                var match = true;

                foreach (var symbol in branch.Symbols)
                {
                    writeList.Clear();
                    foreach (var split in readList)
                    {
                        if (split.Length == 0)
                        {
                            match = false;
                            break;
                        }

                        switch (symbol)
                        {
                            case CharacterSymbol character:
                                if (split.Span[0] == character.Character)
                                {
                                    writeList.Add(split[1..]);
                                }
                                break;
                            case ReferenceSymbol reference:
                                var branchSplits = rules[reference.Number].Matches(split, reference.Number, rules).ToArray();
                                if (branchSplits.Length > 0)
                                {
                                    writeList.AddRange(branchSplits);
                                }
                                break;
                        }
                    }

                    readList = readList == listA ? listB : listA;
                    writeList = writeList == listA ? listB : listA;

                    if (!match)
                    {
                        break;
                    }
                }

                if (readList.Count > 0)
                {
                    foreach(var split in readList)
                    {
                        yield return split;
                    }
                }
            }
        }
    }

    public interface ISymbol
    {
    }

    public record RuleBundle(List<ISymbol> Symbols);
    public record ReferenceSymbol(int Number) : ISymbol;
    public record CharacterSymbol(char Character) : ISymbol;
}
