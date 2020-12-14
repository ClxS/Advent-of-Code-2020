namespace Day14
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class CommonUtil
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<char> GetMask(ref SpanStringReader reader)
        {
            reader.ReadUntil('=', false);
            reader.ReadChar(true);
            return reader.ReadWord(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int Address, ulong Value) GetMemoryLine(ref SpanStringReader reader)
        {
            reader.ReadUntil('[', true);
            var address = reader.ReadInt();
            reader.ReadUntil('=', false);
            reader.ReadChar(true);
            var value = (ulong)reader.ReadInt(true);
            return (address, value);
        }

        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            if (!list.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var startingElementIndex = 0;
                foreach (var startingElement in list)
                {
                    var remainingItems = list.Where((e, i) => i != startingElementIndex);

                    foreach (var permutationOfRemainder in remainingItems.Permute())
                    {
                        yield return startingElement.Concat(permutationOfRemainder);
                    }

                    startingElementIndex++;
                }
            }
        }

        private static IEnumerable<T> Concat<T>(this T firstElement, IEnumerable<T> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }
    }
}
