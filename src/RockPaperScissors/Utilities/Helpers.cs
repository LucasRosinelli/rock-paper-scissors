using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Enums;

namespace RockPaperScissors.Utilities
{
    /// <summary>
    /// Helpers.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// The mapper of options beat. The <see cref="KeyValuePair{TKey, TValue}.Key"/> <see cref="Option"/> beats any <see cref="Option"/> in <see cref="KeyValuePair{TKey, TValue}.Value"/>.
        /// </summary>
        internal static readonly IReadOnlyDictionary<Option, HashSet<Option>> OptionBeatingMapper = InitOptionBeatingMapper();

        /// <summary>
        /// Default foreground color of the console.
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
        /// Gets the maximum value of the enumeration.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/>.</typeparam>
        /// <returns>The maximum value of the enumeration.</returns>
        public static int MaxValue<T>()
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }

        /// <summary>
        /// Gets the minimum value of the enumeration.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/>.</typeparam>
        /// <returns>The minimum value of the enumeration.</returns>
        public static int MinValue<T>()
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }

        ///// <summary>
        ///// Prints a line in the console with indentation.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndented(string content, int indent = 5)
        //{
        //    string indentation = string.Empty.PadLeft(indent);
        //    Console.Write($"{indentation}{content}");
        //}

        ///// <summary>
        ///// Prints a colored line in the console with indentation.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="color">The <see cref="ConsoleColor"/>.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedColored(string content, ConsoleColor color, int indent = 7)
        //{
        //    Console.ForegroundColor = color;
        //    PrintIndented(content, indent);
        //    Console.ForegroundColor = DefaultForegroundColor;
        //}

        ///// <summary>
        ///// Prints a success line in the console with indentation.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedSuccess(string content, int indent = 7)
        //{
        //    PrintIndentedColored(content, SuccessForegroundColor, indent);
        //}

        ///// <summary>
        ///// Prints a warning line in the console with indentation.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedWarning(string content, int indent = 7)
        //{
        //    PrintIndentedColored(content, WarningForegroundColor, indent);
        //}

        ///// <summary>
        ///// Prints an error line in the console with indentation.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedError(string content, int indent = 7)
        //{
        //    PrintIndentedColored(content, ErrorForegroundColor, indent);
        //}

        ///// <summary>
        ///// Prints a line in the console with indentation followed by the current line terminator.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedLine(string content, int indent = 5)
        //{
        //    string indentation = string.Empty.PadLeft(indent);
        //    Console.WriteLine($"{indentation}{content}");
        //}

        ///// <summary>
        ///// Prints a colored line in the console with indentation followed by the current line terminator.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="color">The <see cref="ConsoleColor"/>.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedLineColored(string content, ConsoleColor color, int indent = 7)
        //{
        //    Console.ForegroundColor = color;
        //    PrintIndentedLine(content, indent);
        //    Console.ForegroundColor = DefaultForegroundColor;
        //}

        ///// <summary>
        ///// Prints a success line in the console with indentation followed by the current line terminator.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedLineSuccess(string content, int indent = 7)
        //{
        //    PrintIndentedLineColored(content, SuccessForegroundColor, indent);
        //}

        ///// <summary>
        ///// Prints a warning line in the console with indentation followed by the current line terminator.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedLineWarning(string content, int indent = 7)
        //{
        //    PrintIndentedLineColored(content, WarningForegroundColor, indent);
        //}

        ///// <summary>
        ///// Prints an error line in the console with indentation followed by the current line terminator.
        ///// </summary>
        ///// <param name="content">The content to indent.</param>
        ///// <param name="indent">The indentation (white spaces).</param>
        //internal static void PrintIndentedLineError(string content, int indent = 7)
        //{
        //    PrintIndentedLineColored(content, ErrorForegroundColor, indent);
        //}

        /// <summary>
        /// Initializes the mapper of options beat. The <see cref="KeyValuePair{TKey, TValue}.Key"/> <see cref="Option"/> beats any <see cref="Option"/> in <see cref="KeyValuePair{TKey, TValue}.Value"/>.
        /// </summary>
        /// <returns>The <see cref="IReadOnlyDictionary{TKey, TValue}"/> with the <see cref="Option"/> and a set of <see cref="Option"/> to beat.</returns>
        private static IReadOnlyDictionary<Option, HashSet<Option>> InitOptionBeatingMapper()
        {
            var optionBeatingMapper = new Dictionary<Option, HashSet<Option>>
            {
                { Option.Rock, new() { Option.Scissors, Option.Flamethrower, } },
                { Option.Paper, new() { Option.Rock, } },
                { Option.Scissors, new() { Option.Paper, Option.Flamethrower, } },
                { Option.Flamethrower, new() { Option.Paper, } },
            };

            return optionBeatingMapper;
        }
    }
}
