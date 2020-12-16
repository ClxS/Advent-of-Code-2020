namespace Day16
{
    using Common;
    using Serilog;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day16 Part2";

        public void Solve()
        {
            Log.Information("Value: {Value}", Solve(this.text));
        }

        public static ulong Solve(string text)
        {
            var reader = new SpanStringReader(text);
            var fields = ReadFields(ref reader);           

            var fieldOrder = fields.Keys.ToArray();

            reader.ReadLine();
            var yourTicket = new string(reader.ReadLine()).Split(',').Select(int.Parse).ToArray();

            var potentialFieldSlots = new BitArray[fieldOrder.Length];
            for(var i = 0; i < fieldOrder.Length; i++)
            {
                potentialFieldSlots[i] = new BitArray(fieldOrder.Length, true);
            }

            reader.ReadLine();
            reader.ReadLine();
            Span<int> numbers = stackalloc int[fieldOrder.Length];
            var validCount = 0;
            var invalid = 0;
            while (!reader.IsEndOfFile())
            {
                bool valid = true;

                for (var i = 0; i < fields.Count; ++i)
                {
                    reader.ReadUntilDigit(false);
                    numbers[i] = reader.ReadInt(true);

                    if (valid && !IsValueValid(fields, numbers[i]))
                    {
                        valid = false;
                    }
                }

                if (!valid)
                {
                    invalid++;
                    continue;
                }

                validCount++;
                ValidateFields(fieldOrder, fields, potentialFieldSlots, numbers);
            }

            potentialFieldSlots = ReconcileSlots(potentialFieldSlots);

            var total = 1ul;
            for (int i = 0; i < fieldOrder.Length; ++i)
            {
                if (!fieldOrder[i].StartsWith("departure"))
                {
                    continue;
                }

                for (int j = 0; j < fieldOrder.Length; ++j)
                {
                    if (potentialFieldSlots[j].Get(i))
                    {
                        total *= (ulong)yourTicket[j];
                    }
                }
            }

            return total;
        }

        private static BitArray[] ReconcileSlots(BitArray[] potentialFieldSlots)
        {
            int foundCount = 0;
            BitArray[] final = new BitArray[potentialFieldSlots.Length];
            for (int i = 0; i < potentialFieldSlots.Length; ++i)
            {
                final[i] = new BitArray(potentialFieldSlots.Length, false);
            }

            while(true)
            {
                int? idx = null;
                for (int i = 0; i < potentialFieldSlots.Length; ++i)
                {
                    if (CountPositions(potentialFieldSlots[i]) == 1)
                    {
                        foundCount++;
                        idx = GetPosition(potentialFieldSlots[i]);
                        final[i].Set(idx.Value, true);
                        break;
                    }
                }

                if (foundCount == potentialFieldSlots.Length)
                {
                    break;
                }

                for (int i = 0; i < potentialFieldSlots.Length; ++i)
                {
                    foreach(var pos in GetPositions(potentialFieldSlots[i]))
                    {
                        if (pos == idx)
                        {
                            potentialFieldSlots[i].Set(pos, false);
                            break;
                        }
                    }
                }
            }

            return final;
        }

        private static Dictionary<string, (int GroupStart, int GroupEnd)[]> ReadFields(ref SpanStringReader reader)
        {
            var fields = new Dictionary<string, (int GroupStart, int GroupEnd)[]>();
            while (true)
            {
                var line = reader.ReadLine();
                if (line.Length == 0)
                {
                    break;
                }

                var lineReader = new SpanStringReader(line);
                var field = lineReader.ReadUntil(':', true);
                lineReader.ReadChar();
                var group1Start = lineReader.ReadInt();
                lineReader.ReadChar();
                var group1End = lineReader.ReadInt();
                lineReader.Skip(3);
                var group2Start = lineReader.ReadInt();
                lineReader.ReadChar();
                var group2End = lineReader.ReadInt();
                fields[new string(field)] = new[]
                {
                    (group1Start, group1End),
                    (group2Start, group2End)
                };
            }

            return fields;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetPosition(int numIdx, BitArray[] potentialFieldSlots)
        {
            for (int i = 0; i < potentialFieldSlots.Length; ++i)
            {
                var slot = GetPosition(potentialFieldSlots[i]);
                if (slot != -1)
                {
                    return slot;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetPosition(BitArray arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr.Get(i))
                {
                    return i;
                }
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<int> GetPositions(BitArray arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr.Get(i))
                {
                    yield return i;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CountPositions(BitArray arr)
        {
            int count = 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr.Get(i))
                {
                    count++;
                }
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateFields(string[] fieldOrder, Dictionary<string, (int GroupStart, int GroupEnd)[]> fields, BitArray[] potentialFieldSlots, Span<int> numbers)
        {
            for (int i = 0; i < fieldOrder.Length; ++i)
            {
                foreach(var field in GetInvalidFields(fieldOrder, fields, numbers[i]))
                {
                    potentialFieldSlots[i].Set(field, false);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<int> GetInvalidFields(string[] fieldOrder, Dictionary<string, (int GroupStart, int GroupEnd)[]> fields, int value)
        {
            for (int i = 0; i < fieldOrder.Length; ++i)
            {
                var field = fields[fieldOrder[i]];
                bool valid = false;
                foreach (var (GroupStart, GroupEnd) in field)
                {
                    if (value >= GroupStart && value <= GroupEnd)
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                {
                    yield return i;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValueValid(Dictionary<string, (int GroupStart, int GroupEnd)[]> fields, int value)
        {
            foreach (var field in fields.ToArray())
            {
                bool valid = false;
                foreach (var (GroupStart, GroupEnd) in field.Value)
                {
                    if (value >= GroupStart && value <= GroupEnd)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
