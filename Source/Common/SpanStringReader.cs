namespace Common
{
    using System;

    public ref struct SpanStringReader
    {
        private ReadOnlySpan<char> data;

        public SpanStringReader(ReadOnlySpan<char> sourceString)
        {
            this.data = sourceString;
        }

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

            data = data[(idx + 1)..];
            return retValue;
        }

        public char ReadChar()
        {
            if (data.Length == 0)
            {
                return default;
            }

            var retValue = data[0];
            data = data[1..];
            return retValue;
        }
    }
}
