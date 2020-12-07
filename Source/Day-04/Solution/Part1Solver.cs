namespace Day4
{
    using Common;
    using Serilog;
    using System;
    using System.Linq;

    public class Part1Solver : ISolver
    {
        private readonly string text;

        public Part1Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day4 Part1";

        private enum State
        {
            ReadingFieldName,
            ReadingValue,
            Seeking,
        }

        public void Solve()
        {
            var fields = 0;
            var valid = 0;
            var invalid = 0;

            var currentState = State.Seeking;
            var idx = 0;

            ReadOnlySpan<char> field;
            ReadOnlySpan<char> textStr = text;

            while (idx < text.Length)
            {
                switch (currentState)
                {
                    case State.ReadingFieldName:
                        var startIdx = idx;
                        while(idx < text.Length)
                        {
                            var @char = text[idx];
                            var isLetter = @char >= 'a' && @char <= 'z';

                            if (isLetter)
                            {
                                idx++;
                            }
                            else
                            {
                                currentState = State.Seeking;
                                break;
                            }
                        }

                        field = textStr.Slice(startIdx, idx - startIdx);

                        if (!field.Equals("cid", StringComparison.OrdinalIgnoreCase))
                        {
                            fields++;
                        }

                        break;
                    case State.ReadingValue:
                        while (idx < text.Length)
                        {
                            var @char = text[idx];
                            if (@char != ' ' && @char != '\n')
                            {
                                idx++;
                            }
                            else
                            {
                                currentState = State.Seeking;
                                break;
                            }
                        }

                        break;
                    case State.Seeking:
                        {
                            var wasLastNewline = false;
                            while (idx < text.Length)
                            {
                                var @char = text[idx];
                                var isLetter = @char >= 'a' && @char <= 'z';

                                if (isLetter)
                                {
                                    currentState = State.ReadingFieldName;
                                    break;
                                }
                                else if (@char == ':')
                                {
                                    currentState = State.ReadingValue;
                                    idx++;
                                    break;
                                }
                                else if (@char == '\n')
                                {
                                    if (wasLastNewline)
                                    {
                                        if (fields >= 7)
                                        {
                                            valid++;
                                        }
                                        else
                                        {
                                            invalid++;
                                        }

                                        fields = 0;
                                    }

                                    wasLastNewline = true;
                                    idx++;
                                }
                                else if (@char == '\r')
                                {
                                    idx++;
                                }
                                else
                                {
                                    wasLastNewline = false;
                                    idx++;
                                }
                            }

                            break;
                        }
                }
            }

            if (fields >= 7)
            {
                valid++;
            }
            else
            {
                invalid++;
            }

            Log.Information("There were {Valid} valid and {Invalid} invalid", valid, invalid);
        }
    }
}
