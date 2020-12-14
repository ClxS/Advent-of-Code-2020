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
    }
}
