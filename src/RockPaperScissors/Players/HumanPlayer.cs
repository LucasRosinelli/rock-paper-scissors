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
        /// Initializes a new instance of the <see cref="HumanPlayer"/> class.
        /// </summary>
        /// <param name="name">The player name.</param>
        public HumanPlayer(string name)
            : base(name)
        {
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
                Helpers.PrintIndented("# ");
                Helpers.PrintIndentedSuccess(Name, 0);
                Helpers.PrintIndentedLine(", type the number or the complete word", 0);
                Helpers.PrintIndentedLineWarning("=> Don't worry, your selection will not be displayed =)");
                string selectionFromUser = ReadLineHidden();
                Console.WriteLine();

                if (Enum.TryParse<Option>(selectionFromUser, true, out var option) && Enum.IsDefined(option))
                {
                    RegisterSelection(option);

                    return option;
                }

                Helpers.PrintIndentedLineError($"{selectionFromUser} is an invalid option. Please, try again.");
            }
        }

        /// <summary>
        /// Reads the <see cref="Option"/> hiding the entered text.
        /// </summary>
        /// <remarks>
        /// It is very useful when using the same device to play agains another <see cref="HumanPlayer"/>.
        /// </remarks>
        /// <returns>The entered text.</returns>
        private string ReadLineHidden()
        {
            StringBuilder selectionFromUser = new();
            Helpers.PrintIndentedLine("# Select your option:");
            IEnumerable<string> optionDescriptions = Helpers.OptionBeatingMapper.Select(o => o.Key.Describe());
            foreach (var optionDescription in optionDescriptions)
            {
                Helpers.PrintIndentedLine($"# >> {optionDescription}");
            }

            Helpers.PrintIndented(string.Empty);
            var newLine = '\r';
            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
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
