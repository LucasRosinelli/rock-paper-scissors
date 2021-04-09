using System;

namespace RockPaperScissors.Utilities
{
    public interface IConsoleWrapper
    {
        ConsoleColor ForegroundColor { get; set; }

        ConsoleKeyInfo ReadKey(bool intercept);

        string? ReadLine();

        void Write(string value);

        void WriteLine();

        void WriteLine(string value);
    }
}
