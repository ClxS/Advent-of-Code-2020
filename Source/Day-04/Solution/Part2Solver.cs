namespace Day4
{
    using Common;
    using Serilog;
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    public class Part2Solver : ISolver
    {
        private readonly string text;

        public Part2Solver(string text)
        {
            this.text = text;
        }

        public string Name => "Day4 Part2";

        private enum State
        {
            ReadingFieldName,
            ReadingValue,
            Seeking,
            SeekingToNextEntry,
        }

        public void Solve()
        {
            var fields = 0;
            var valid = 0;
            var invalid = 0;

            var currentState = State.Seeking;
            var idx = 0;
            bool currentInvalid = false;

            ReadOnlySpan<char> field = text;
            ReadOnlySpan<char> fieldValue;
            ReadOnlySpan<char> textStr = text;

            while (idx < text.Length)
            {
                switch (currentState)
                {
                    case State.ReadingFieldName:
                    {
                        var startIdx = idx;
                        while (idx < text.Length)
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
                    }
                    case State.ReadingValue:
                    {
                        var startIdx = idx;
                        while (idx < text.Length)
                        {
                            var @char = text[idx];
                            if (@char != ' ' && @char != '\n' && @char != '\r')
                            {
                                idx++;
                            }
                            else
                            {
                                currentState = State.Seeking;
                                break;
                            }
                        }

                        fieldValue = textStr.Slice(startIdx, idx - startIdx);
                        if (!Validate(field, fieldValue))
                        {
                            Log.Verbose("Discarded entry because {Field} equals {Value}", new string(field.ToArray()), new string(fieldValue.ToArray()));
                            currentState = State.SeekingToNextEntry;
                            currentInvalid = true;
                        }

                        break;
                    }
                    case State.SeekingToNextEntry:
                    {
                        var wasLastNewline = false;
                        while (idx < text.Length && currentState == State.SeekingToNextEntry)
                        {
                            var @char = text[idx];
                            if (@char == '\n')
                            {
                                if (wasLastNewline)
                                {
                                    if (fields >= 7 && !currentInvalid)
                                    {
                                        valid++;
                                    }
                                    else
                                    {
                                        invalid++;
                                    }

                                    currentInvalid = false;

                                    fields = 0;
                                    currentState = State.Seeking;
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

        private bool Validate(ReadOnlySpan<char> field, ReadOnlySpan<char> fieldValue)
        {
            if (field.Equals("byr", StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(fieldValue, out var birthYear))
                {
                    return false;
                }

                return birthYear >= 1920 && birthYear <= 2002;
            }
            else if (field.Equals("iyr", StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(fieldValue, out var issueYear))
                {
                    return false;
                }

                return issueYear >= 2010 && issueYear <= 2020;
            }
            else if (field.Equals("eyr", StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(fieldValue, out var expirationyear))
                {
                    return false;
                }

                return expirationyear >= 2020 && expirationyear <= 2030;
            }
            else if (field.Equals("hgt", StringComparison.OrdinalIgnoreCase))
            {
                if (fieldValue[^2] == 'c' && fieldValue[^1] == 'm')
                {
                    if (!int.TryParse(fieldValue.Slice(0, fieldValue.Length - 2), out var height))
                    {
                        return false;
                    }

                    return height >= 150 && height <= 193;
                }
                else if(fieldValue[^2] == 'i' && fieldValue[^1] == 'n')
                {
                    if (!int.TryParse(fieldValue.Slice(0, fieldValue.Length - 2), out var height))
                    {
                        return false;
                    }

                    return height >= 59 && height <= 76;
                }
                else
                {
                    return false;
                }
            }
            else if (field.Equals("hcl", StringComparison.OrdinalIgnoreCase))
            {
                if (fieldValue[0] != '#' || fieldValue.Length != 7)
                {
                    return false;
                }

                return int.TryParse(fieldValue[1..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _);
            }
            else if (field.Equals("ecl", StringComparison.OrdinalIgnoreCase))
            {
                return fieldValue.Equals("amb", StringComparison.OrdinalIgnoreCase) ||
                    fieldValue.Equals("blu", StringComparison.OrdinalIgnoreCase) ||
                    fieldValue.Equals("brn", StringComparison.OrdinalIgnoreCase) ||
                    fieldValue.Equals("gry", StringComparison.OrdinalIgnoreCase) ||
                    fieldValue.Equals("grn", StringComparison.OrdinalIgnoreCase) ||
                    fieldValue.Equals("hzl", StringComparison.OrdinalIgnoreCase) ||
                    fieldValue.Equals("oth", StringComparison.OrdinalIgnoreCase);
            }
            else if (field.Equals("pid", StringComparison.OrdinalIgnoreCase))
            {
                if (!int.TryParse(fieldValue, out _))
                {
                    return false;
                }

                return fieldValue.Length == 9;
            }
            else if (field.Equals("cid", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
