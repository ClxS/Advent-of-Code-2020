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
                if (idx + 1 >= data.Length || data[idx + 1] == '\n')
                {
                    break;
                }

                idx++;
            }

            var skip = 1;
            if (data[idx] == '\r')
            {
                skip++;
            }

            if (idx > 0 && data[idx - 1] == '\r')
            {
                idx--;
            }

            if (idx + 1 == data.Length)
            {
                idx++;
            }

            var retValue = this.data.Slice(0, idx);
            data = data[(Math.Min(idx + skip, data.Length))..];
            return retValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadUntil(char v, bool skipFound)
        {
            if (data.Length == 0)
            {
                return default;
            }

            var idx = 0;
            while (idx < data.Length)
            {
                if (data[idx] == v)
                {
                    break;
                }

                idx++;
            }

            var retValue = this.data.Slice(0, idx);

            if (skipFound && idx < data.Length - 1)
            {
                idx++;
            }

            data = data[idx..];
            return retValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadUntilDigit(bool skipFound)
        {
            if (data.Length == 0)
            {
                return default;
            }

            var idx = 0;
            while (idx < data.Length)
            {
                if (data[idx] >= '0' && data[idx] <= '9')
                {
                    break;
                }

                idx++;
            }

            var retValue = this.data.Slice(0, idx);

            if (skipFound && idx < data.Length - 1)
            {
                idx++;
            }

            data = data[idx..];
            return retValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Skip(int count)
        {
            data = data[count..];
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
                if (!((@char >= 'a' && @char <= 'z') || (@char >= 'A' && @char <= 'Z') || (@char >= '0' && @char <= '9')))
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
        public ReadOnlySpan<char> PeekWord()
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

            return this.data.Slice(0, idx + 1);
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
            return NumberParser.ParseInt(retValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong ReadUlongBase2(bool skipToNextChar = true)
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
                if (!(@char >= '0' && @char <= '1'))
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
            return NumberParser.ParseUlongBase2(retValue);
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
