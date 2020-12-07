namespace Day7
{
    using Common;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Day7Solver : ISolver
    {
        private static char[] workSplitters = new[] { ' ', ',' }; 
        private readonly string text;

        public Day7Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day7";

        private enum State
        {
            ComposingBagName,
            CheckingContains,
            AddBags,
        }

        public void Solve()
        {
            var bags = new Dictionary<string, Bag>(2048);
            Bag GetOrAddBag(string name)
            {
                if (!bags.TryGetValue(name, out var bag))
                {
                    bag = new Bag()
                    {
                        Name = name
                    };

                    bags[name] = bag;
                }

                return bag;
            }

            foreach(var line in this.text.SplitLines())
            {
                var state = State.ComposingBagName;

                bool isWritingInnterBag = false;
                int bagCount = 0;
                ReadOnlySpan<char> bagStart = default;
                ReadOnlySpan<char> bagEnd = default;
                Bag lineBag = default;
                foreach (var word in line.SplitAsSpans(workSplitters.AsSpan()))
                {
                    bool repeat;
                    do
                    {
                        repeat = false;
                        switch (state)
                        {
                            case State.ComposingBagName:
                                if (word.Equals("contain", StringComparison.OrdinalIgnoreCase))
                                {
                                    state = State.CheckingContains;
                                }
                                else
                                {
                                    if (bagStart == default)
                                    {
                                        bagStart = word;
                                    }
                                    else
                                    {
                                        bagEnd = word;
                                    }
                                }
                                break;
                            case State.CheckingContains:
                                if (!word.Equals("no", StringComparison.OrdinalIgnoreCase))
                                {
                                    lineBag = GetOrAddBag(ComposeFullName(bagStart, bagEnd, line));
                                    state = State.AddBags;
                                    repeat = true;
                                }
                                else
                                {
                                    break;
                                }
                                break;
                            case State.AddBags:
                                {
                                    if (int.TryParse(word, out var count))
                                    {
                                        if (isWritingInnterBag)
                                        {
                                            var innerBag = GetOrAddBag(ComposeFullName(bagStart, bagEnd, line));
                                            lineBag.AddContainableBag(bagCount, innerBag);
                                        }

                                        bagStart = default;
                                        bagEnd = default;
                                        isWritingInnterBag = true;
                                        bagCount = count;
                                    }
                                    else if (bagStart == default)
                                    {
                                        bagStart = word;
                                    }
                                    else
                                    {
                                        bagEnd = word;
                                    }
                                }

                                break;
                        }
                    } while (repeat);
                }

                if (isWritingInnterBag)
                {
                    var innerBag = GetOrAddBag(ComposeFullName(bagStart, bagEnd, line));
                    lineBag.AddContainableBag(bagCount, innerBag);
                }
            }

            Log.Information("{Number} bags can contain shiny gold bags", bags["shiny gold"].GetAllStorableIn());
            Log.Information("Shiny gold bags contain {Number} bags", bags["shiny gold"].GetContainable());
        }

        private string ComposeFullName(ReadOnlySpan<char> bagStart, ReadOnlySpan<char> bagEnd, ReadOnlySpan<char> line)
        {
            var startIdx = ElementOffsetOf(bagStart, line);
            var endIdx = ElementOffsetOf(bagEnd, line) - 1;
            return new string(line[startIdx..endIdx]);
        }

        int ElementOffsetOf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> array) => (int)Unsafe.ByteOffset(ref MemoryMarshal.GetReference(array), ref MemoryMarshal.GetReference(span)) / Unsafe.SizeOf<T>();
    }
}
