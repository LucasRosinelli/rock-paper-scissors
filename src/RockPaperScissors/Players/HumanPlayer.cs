using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockPaperScissors.Enums;
using RockPaperScissors.Utilities;

namespace RockPaperScissors.Players
{
    /// <summary>
    /// The human player.
    /// </summary>
    public sealed class HumanPlayer : BasePlayer
    {
        /// <summary>
        /// The <see cref="IConsoleWrapper"/>.
        /// </summary>
        private readonly IConsoleWrapper _consoleWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanPlayer"/> class.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="name">The player name.</param>
        public HumanPlayer(IConsoleWrapper consoleWrapper, string name)
            : base(name)
        {
            _consoleWrapper = consoleWrapper ?? throw new ArgumentNullException(nameof(consoleWrapper));
        }

        /// <summary>
        /// Selects an <see cref="Option"/>.
        /// </summary>
        /// <remarks>
        /// It does not allow an invalid selection.
        /// </remarks>
        /// <returns>The <see cref="Option"/> selected.</returns>
        public override Option Select()
        {
            while (true)
            {
                _consoleWrapper.PrintIndented("# ");
                _consoleWrapper.PrintIndentedSuccess(Name, 0);
                _consoleWrapper.PrintIndentedLine(", type the number or the complete word", 0);
                _consoleWrapper.PrintIndentedLineWarning("=> Don't worry, your selection will not be displayed =)");
                string selectionFromUser = ReadLineHidden();
                _consoleWrapper.WriteLine();

                if (Enum.TryParse<Option>(selectionFromUser, true, out var option) && Enum.IsDefined(option))
                {
                    RegisterSelection(option);

                    return option;
                }

                _consoleWrapper.PrintIndentedLineError($"{selectionFromUser} is an invalid option. Please, try again.");
            }
        }

        /// <summary>
        /// Reads the <see cref="Option"/> hiding the entered text.
        /// </summary>
        /// <remarks>
        /// It is very useful when using the same device to play against another <see cref="HumanPlayer"/>.
        /// </remarks>
        /// <returns>The entered text.</returns>
        private string ReadLineHidden()
        {
            StringBuilder selectionFromUser = new();
            _consoleWrapper.PrintIndentedLine("# Select your option:");
            IEnumerable<string> optionDescriptions = Helpers.OptionBeatingMapper.Select(o => o.Key.Describe());
            foreach (var optionDescription in optionDescriptions)
            {
                _consoleWrapper.PrintIndentedLine($"# >> {optionDescription}");
            }

            _consoleWrapper.PrintIndented(string.Empty);
            var newLine = '\r';
            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = _consoleWrapper.ReadKey(true);
                char selectionCharacter = consoleKeyInfo.KeyChar;

                if (selectionCharacter == newLine)
                {
                    break;
                }

                selectionFromUser.Append(selectionCharacter);
            }

            return selectionFromUser.ToString().Trim();
        }
    }
}
