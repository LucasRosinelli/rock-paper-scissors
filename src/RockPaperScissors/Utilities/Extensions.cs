using System;
using RockPaperScissors.Enums;

namespace RockPaperScissors.Utilities
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Default foreground color of the consoleWrapper.
        /// </summary>
        private static readonly ConsoleColor DefaultForegroundColor = Console.ForegroundColor;

        /// <summary>
        /// Success foreground color.
        /// </summary>
        private static readonly ConsoleColor SuccessForegroundColor = ConsoleColor.Green;

        /// <summary>
        /// Warning foreground color.
        /// </summary>
        private static readonly ConsoleColor WarningForegroundColor = ConsoleColor.Yellow;

        /// <summary>
        /// Error foreground color.
        /// </summary>
        private static readonly ConsoleColor ErrorForegroundColor = ConsoleColor.Red;

        /// <summary>
        /// Prints a line in the console with indentation.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndented(this IConsoleWrapper consoleWrapper, string content, int indent = 5)
        {
            string indentation = string.Empty.PadLeft(indent);
            consoleWrapper.Write($"{indentation}{content}");
        }

        /// <summary>
        /// Prints a colored line in the console with indentation.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="color">The <see cref="consoleColor"/>.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedColored(this IConsoleWrapper consoleWrapper, string content, ConsoleColor color, int indent = 7)
        {
            consoleWrapper.ForegroundColor = color;
            consoleWrapper.PrintIndented(content, indent);
            consoleWrapper.ForegroundColor = DefaultForegroundColor;
        }

        /// <summary>
        /// Prints a success line in the console with indentation.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedSuccess(this IConsoleWrapper consoleWrapper, string content, int indent = 7)
        {
            consoleWrapper.PrintIndentedColored(content, SuccessForegroundColor, indent);
        }

        /// <summary>
        /// Prints a warning line in the console with indentation.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedWarning(this IConsoleWrapper consoleWrapper, string content, int indent = 7)
        {
            consoleWrapper.PrintIndentedColored(content, WarningForegroundColor, indent);
        }

        /// <summary>
        /// Prints an error line in the console with indentation.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedError(this IConsoleWrapper consoleWrapper, string content, int indent = 7)
        {
            consoleWrapper.PrintIndentedColored(content, ErrorForegroundColor, indent);
        }

        /// <summary>
        /// Prints a line in the console with indentation followed by the current line terminator.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedLine(this IConsoleWrapper consoleWrapper, string content, int indent = 5)
        {
            string indentation = string.Empty.PadLeft(indent);
            consoleWrapper.WriteLine($"{indentation}{content}");
        }

        /// <summary>
        /// Prints a colored line in the console with indentation followed by the current line terminator.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="color">The <see cref="ConsoleColor"/>.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedLineColored(this IConsoleWrapper consoleWrapper, string content, ConsoleColor color, int indent = 7)
        {
            consoleWrapper.ForegroundColor = color;
            consoleWrapper.PrintIndentedLine(content, indent);
            consoleWrapper.ForegroundColor = DefaultForegroundColor;
        }

        /// <summary>
        /// Prints a success line in the console with indentation followed by the current line terminator.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedLineSuccess(this IConsoleWrapper consoleWrapper, string content, int indent = 7)
        {
            consoleWrapper.PrintIndentedLineColored(content, SuccessForegroundColor, indent);
        }

        /// <summary>
        /// Prints a warning line in the console with indentation followed by the current line terminator.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedLineWarning(this IConsoleWrapper consoleWrapper, string content, int indent = 7)
        {
            consoleWrapper.PrintIndentedLineColored(content, WarningForegroundColor, indent);
        }

        /// <summary>
        /// Prints an error line in the console with indentation followed by the current line terminator.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="content">The content to indent.</param>
        /// <param name="indent">The indentation (white spaces).</param>
        internal static void PrintIndentedLineError(this IConsoleWrapper consoleWrapper, string content, int indent = 7)
        {
            consoleWrapper.PrintIndentedLineColored(content, ErrorForegroundColor, indent);
        }

        /// <summary>
        /// Describes the <see cref="Mode"/>.
        /// </summary>
        /// <param name="mode">The <see cref="Mode"/> to describe.</param>
        /// <returns>The <see cref="Mode"/> description.</returns>
        public static string Describe(this Mode mode)
        {
            return mode switch
            {
                Mode.TwoHuman => $"[{(int)mode}] Two human players",
                Mode.AgainstComputer => $"[{(int)mode}] Against a computer player",
                Mode.AgainstComputerRandom => $"[{(int)mode}] Against a computer random selector player",
                _ => "Unrecognized mode",
            };
        }

        /// <summary>
        /// Describes the <see cref="Option"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option"/> to describe.</param>
        /// <returns>The <see cref="Option"/> description.</returns>
        public static string Describe(this Option option)
        {
            return option switch
            {
                _ => $"[{(int)option}] {option}",
            };
        }
    }
}
