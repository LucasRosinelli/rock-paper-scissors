using System;

namespace RockPaperScissors.Utilities
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }

            set
            {
                Console.ForegroundColor = value;
            }
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public string? ReadLine()
        {
            return Console.ReadLine()?.Trim();
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
