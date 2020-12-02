using System;
using System.Threading.Tasks;

namespace Day2
{
    public record Password
    {
        public Password(int minimumRepetition, int maximumRepetition, char character, string passwordString)
             => (MinimumRepetition, MaximumRepetition, Character, PasswordString) = (minimumRepetition, maximumRepetition, character, passwordString);

        public static Password[] GetPasswords(string[] lines)
        {
            var passwords = new Password[lines.Length];

            Parallel.For(0, lines.Length, i =>
            {
                var line = lines[i].AsSpan();

                int min = 0;
                int max = 0;
                char letter = 'a';
                string password = string.Empty;

                State state = State.FormingMin;
                State nextState = State.Seeking;
                while (true)
                {
                    switch (state)
                    {
                        case State.FormingMin:
                            {
                                int charIdx = 0;
                                for (; charIdx < line.Length; charIdx++)
                                {
                                    if (!char.IsDigit(line[charIdx]))
                                    {
                                        break;
                                    }
                                }

                                min = int.Parse(line.Slice(0, charIdx));
                                line = line[charIdx..];
                                nextState = State.FormingMax;
                                state = State.Seeking;
                            }
                            break;
                        case State.FormingMax:
                            {
                                int charIdx = 0;
                                for (; charIdx < line.Length; charIdx++)
                                {
                                    if (!char.IsDigit(line[charIdx]))
                                    {
                                        break;
                                    }
                                }

                                max = int.Parse(line.Slice(0, charIdx));
                                line = line[charIdx..];
                                nextState = State.FormingChar;
                                state = State.Seeking;
                            }
                            break;
                        case State.FormingChar:
                            letter = line[0];
                            line = line[1..];
                            nextState = State.FormingPassword;
                            state = State.Seeking;
                            break;
                        case State.FormingPassword:
                            password = line.ToString();
                            nextState = State.Done;
                            break;
                        case State.Seeking:
                            {
                                int charIdx = 0;
                                for (; charIdx < line.Length; charIdx++)
                                {
                                    if (char.IsLetterOrDigit(line[charIdx]))
                                    {
                                        break;
                                    }
                                }

                                line = line[charIdx..];
                                state = nextState;
                            }
                            break;
                        default:
                            break;
                    }

                    if (nextState == State.Done)
                    {
                        break;
                    }
                }

                passwords[i] = new Password(min, max, letter, password);
            });

            return passwords;
        }

        public int MinimumRepetition { get; }

        public int MaximumRepetition { get; }

        public char Character { get; }

        public string PasswordString { get; }

        private enum State
        {
            FormingMin,
            FormingMax,
            FormingChar,
            FormingPassword,
            Seeking,
            Done
        }
    }
}
