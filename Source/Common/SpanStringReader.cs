namespace Common
{
    using System;
    using System.Runtime.CompilerServices;

    public ref struct SpanStringReader
    {
        private ReadOnlySpan<char> data;

        public SpanStringReader(ReadOnlySpan<char> sourceString)
        {
            this.data = sourceString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadLine()
        {
            if (data.Length == 0)
            {
                return default;
            }

            var idx = 0;
            while(idx < data.Length)
            {
                if (data[idx + 1] == '\n')
                {
                    break;
                }

                idx++;
            }

            if (data[idx - 1] == '\r')
            {
                idx--;
            }

            var retValue = this.data.Slice(0, idx);
            data = data[idx..];
            return retValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEndOfFile()
        {
            return this.data.Length == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadWord(bool skipToNextChar = true)
        {
            var dataLength = data.Length;
            if (dataLength == 0)
            {
                return default;
            }

            var idx = 0;
            while (idx < data.Length && idx + 1 < dataLength)
            {
                var @char = data[idx + 1];
                if (!((@char >= 'a' && @char <= 'z') || (@char >= '0' && @char <= '9')))
                {
                    break;
                }

                idx++;
            }

            var retValue = this.data.Slice(0, idx + 1);

            if (skipToNextChar)
            {
                ProceedToNextChar(ref idx);
            }

            data = data[(idx + 1)..];
            return retValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char ReadChar(bool skipToNextChar = true)
        {
            if (data.Length == 0)
            {
                return default;
            }

            var idx = 0;
            var retValue = data[0];

            if (skipToNextChar)
            {
                ProceedToNextChar(ref idx);
            }

            data = data[(idx + 1)..];
            return retValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReadInt(bool skipToNextChar = true)
        {
            if (data.Length == 0)
            {
                return default;
            }

            var dataLength = data.Length;
            var idx = 0;
            while (idx < data.Length && idx + 1 < dataLength)
            {
                var @char = data[idx + 1];
                if (!(@char >= '0' && @char <= '9'))
                {
                    break;
                }

                idx++;
            }

            var retValue = this.data.Slice(0, idx + 1);

            if (skipToNextChar)
            {
                ProceedToNextChar(ref idx);
            }

            data = data[(idx + 1)..];
            return ParseInt(retValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int ParseInt(ReadOnlySpan<char> input)
        {
            int val = 0;
            for (var i = 0; i < input.Length; ++i)
            {
                val = (val * 10) + (input[i] - '0');
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProceedToNextChar(ref int idx)
        {
            var dataLength = data.Length;
            while (idx < data.Length && idx + 1 < dataLength)
            {
                var @char = data[idx + 1];
                if (!(@char == '\r' || @char == '\n' || @char == ' '))
                {
                    break;
                }

                idx++;
            }
        }
    }
}
