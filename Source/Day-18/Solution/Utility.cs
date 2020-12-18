namespace Day18
{
    using Common;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public static class Utility
    {
        private static char[] operatorPresidence = new char[] { '+', '*' };

        private static int FindOperatorPosition(int startIndx, List<char> line, bool forwards)
        {
            var insertIdx = startIndx;
            var scopeDepth = 0;
            bool foundInsertee = false;
            var movement = forwards ? 1 : -1;
            var openBrace = forwards ? '(' : ')';
            var closeBrace = forwards ? ')' : '(';

            while (insertIdx >= 0 && insertIdx < line.Count)
            {
                if (line[insertIdx] == openBrace)
                {
                    scopeDepth++;
                }
                else if (scopeDepth > 0 && line[insertIdx] == closeBrace)
                {
                    scopeDepth--;
                    if (scopeDepth == 0)
                    {
                        insertIdx += movement;
                        break;
                    }
                }
                else if (scopeDepth == 0)
                {
                    if (line[insertIdx] >= '0' && line[insertIdx] <= '9')
                    {
                        foundInsertee = true;
                    }
                    else if (foundInsertee)
                    {
                        break;
                    }
                }

                insertIdx += movement;
            }

            return insertIdx + (!forwards ? 1 : 0);
        }

        internal static void ConvertLineToReversePolish(List<char> line, bool respectOrderOfOperations)
        {
            if (respectOrderOfOperations)
            {
                InsertOrderOfOperationsBraces(line);
            }

            var length = line.Count;
            for (var i = length - 1; i >= 0; i--)
            {
                switch (line[i])
                {
                    case '+':
                    case '*':
                        var @char = line[i];
                        line.RemoveAt(i);

                        line.Insert(FindOperatorPosition(i, line, true), @char);
                        break;
                }
            }
        }


        private static void InsertOrderOfOperationsBraces(List<char> line)
        {
            foreach (var @char in operatorPresidence)
            {
                var i = 0;
                while (i < line.Count)
                {
                    if (line[i] != @char)
                    {
                        i++;
                        continue;
                    }

                    var leftPivot = FindOperatorPosition(i, line, false);
                    var rightPivot = FindOperatorPosition(i, line, true);

                    line.Insert(rightPivot, ')');
                    line.Insert(leftPivot, '(');
                    i += 2;
                }
            }
        }

        internal static ulong ExecuteExpression(List<char> line)
        {
            var numbers = new Stack<ulong>();
            var parseIdx = 0;
            var lineSpan = CollectionsMarshal.AsSpan(line);
            while (parseIdx < line.Count)
            {
                if (line[parseIdx] >= '0' && line[parseIdx] <= '9')
                {
                    var parseEnd = parseIdx + 1;
                    while (parseEnd < line.Count)
                    {
                        if (line[parseEnd] >= '0' && line[parseEnd] <= '9')
                        {
                            parseEnd++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    numbers.Push(NumberParser.ParseUlong(lineSpan.Slice(parseIdx, parseEnd - parseIdx)));
                    parseIdx = parseEnd;
                }
                else if (line[parseIdx] == '+')
                {
                    numbers.Push(numbers.Pop() + numbers.Pop());
                    parseIdx++;
                }
                else if (line[parseIdx] == '*')
                {
                    numbers.Push(numbers.Pop() * numbers.Pop());
                    parseIdx++;
                }
                else
                {
                    parseIdx++;
                }
            }

            return numbers.Pop();
        }
    }
}
