namespace Common
{
    using System;

    public static class StringExtensions
    {
        public static SplitEnumerator SplitLines(this string str)
        {
            return str.SplitAsSpans(new[] { '\r', '\n' });
        }
        
        public static SplitEnumerator SplitAsSpans(this string str, ReadOnlySpan<char> separators)
        {
            return new SplitEnumerator(str.AsSpan(), separators);
        }

        public static SplitEnumerator SplitAsSpans(this ReadOnlySpan<char> str, ReadOnlySpan<char> separators)
        {
            return new SplitEnumerator(str, separators);
        }

        public ref struct SplitEnumerator
        {
            private SplitEntry current;
            private ReadOnlySpan<char> chars;
            private readonly ReadOnlySpan<char> separators;

            public SplitEnumerator(ReadOnlySpan<char> str, ReadOnlySpan<char> separators)
            {
                this.chars = str;
                this.separators = separators;
                this.current = default;
            }

            // Needed to be compatible with the foreach operator
            public SplitEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                var span = chars;
                if (span.Length == 0) // Reach the end of the string
                    return false;

                var index = span.IndexOfAny(separators);
                if (index == -1) // The string is composed of only one line
                {
                    chars = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                    current = new SplitEntry(span, ReadOnlySpan<char>.Empty);
                    return true;
                }

                if (index < span.Length - 1)
                {
                    // Try to consume the '\n' associated to the '\r'
                    var next = span[index + 1];
                    if (separators.Contains(next))
                    {
                        current = new SplitEntry(span.Slice(0, index), span.Slice(index, 2));
                        chars = span[(index + 2)..];
                        return true;
                    }
                }

                current = new SplitEntry(span.Slice(0, index), span.Slice(index, 1));
                chars = span[(index + 1)..];
                return true;
            }

            public ReadOnlySpan<char> Current => this.current.Line;
        }

        public readonly ref struct SplitEntry
        {
            public SplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator)
            {
                Line = line;
                Separator = separator;
            }

            public ReadOnlySpan<char> Line { get; }

            public ReadOnlySpan<char> Separator { get; }

            // https://docs.microsoft.com/en-us/dotnet/csharp/deconstruct#deconstructing-user-defined-types
            public void Deconstruct(out ReadOnlySpan<char> line, out ReadOnlySpan<char> separator)
            {
                line = Line;
                separator = Separator;
            }

            // This method allow to implicitly cast the type into a ReadOnlySpan<char>, so you can write the following code
            // foreach (ReadOnlySpan<char> entry in str.SplitLines())
            public static implicit operator ReadOnlySpan<char>(SplitEntry entry) => entry.Line;
        }
    }
}
