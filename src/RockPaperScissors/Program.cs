using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Enums;
using RockPaperScissors.Players;
using RockPaperScissors.Utilities;

namespace RockPaperScissors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                PrintHeader();

                PrintRules();

                Mode mode = ReadMode();

                var players = CreatePlayers(mode);
                var game = Game.Create(players.Player1, players.Player2);
                game.Start();

                Helpers.PrintIndented("Press any key to exit");
                Console.ReadLine();
            }
            catch (Exception exc)
            {
                Helpers.PrintIndentedError("An error occurred during the application execution. Sorry!");
                Helpers.PrintIndentedError(exc.Message);
            }
        }

        /// <summary>
        /// Prints application header.
        /// </summary>
        private static void PrintHeader()
        {
            Console.WriteLine();
            Console.WriteLine("  *******************************************************************************************************************");
            Console.WriteLine("  *                                                                                                                 *");
            Console.WriteLine("  *                                                ROCK PAPER SCISSORS                                              *");
            Console.WriteLine("  *                                                                                                                 *");
            Console.WriteLine("  *******************************************************************************************************************");
            Console.WriteLine("  *  Lucas Rosinelli https://lucasrosinelli.com/                                                                    *");
            Console.WriteLine("  *******************************************************************************************************************");
            Console.WriteLine();
        }

        private static void PrintRules()
        {
            Console.WriteLine();
            Console.WriteLine("   /---------------------------------------------------------------------------\\");
            Console.WriteLine("   | RULES                                                                     |");
            Console.WriteLine("   |---------------------------------------------------------------------------|");
            Console.WriteLine("   | The first player to win 3 rounds wins.                                    |");
            Console.WriteLine("   | During each round, players select one of the three following options:     |");
            Console.WriteLine("   |   - Rock, which beats scissors                                            |");
            Console.WriteLine("   |   - Paper, which beats rock                                               |");
            Console.WriteLine("   |   - Scissors, which beats paper                                           !");
            Console.WriteLine("   | When the same option is selected by both players, the round is restarted. |");
            Console.WriteLine("   \\---------------------------------------------------------------------------/");
            Console.WriteLine();

        }

        /// <summary>
        /// Reads the game <see cref="Mode"/>.
        /// </summary>
        /// <returns>The <see cref="Mode"/>.</returns>
        private static Mode ReadMode()
        {
            while (true)
            {
                Console.WriteLine();
                Helpers.PrintIndentedLine("Select the mode you want to play:");
                IEnumerable<string> modeDescriptions = Enum.GetValues<Mode>().Select(o => o.Describe());
                foreach (var modeDescription in modeDescriptions)
                {
                    Helpers.PrintIndentedLine($">> {modeDescription}");
                }

                Helpers.PrintIndented(string.Empty);
                string? modeFromUser = Console.ReadLine()?.Trim();

                if (Enum.TryParse<Mode>(modeFromUser, true, out var mode) && Enum.IsDefined(mode))
                {
                    return mode;
                }

                Helpers.PrintIndentedLineError($"{modeFromUser} is an invalid mode. Please, try again.");
            }
        }

        /// <summary>
        /// Creates the <see cref="IPlayer"/> 1 and 2 based on <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">The chosen <see cref="Mode"/>.</param>
        /// <returns>The <see cref="IPlayer"/> 1 and 2.</returns>
        private static (IPlayer Player1, IPlayer Player2) CreatePlayers(Mode mode)
        {
            IPlayer player1 = CreatePlayer1();
            IPlayer player2 = CreatePlayer2(mode);

            return (player1, player2);
        }

        /// <summary>
        /// Creates the <see cref="IPlayer"/> 1.
        /// </summary>
        /// <remarks>
        /// Currently, the <see cref="IPlayer"/> 1 is always a <see cref="HumanPlayer"/>.
        /// </remarks>
        /// <returns>The <see cref="IPlayer"/> 1.</returns>
        private static IPlayer CreatePlayer1()
        {
            return CreateHumanPlayer("1");
        }

        /// <summary>
        /// Creates the <see cref="IPlayer"/> 2 based on <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">The chosen <see cref="Mode"/>.</param>
        /// <returns>The <see cref="IPlayer"/> 2.</returns>
        private static IPlayer CreatePlayer2(Mode mode)
        {
            return mode switch
            {
                Mode.TwoHuman => CreateHumanPlayer("2"),
                Mode.AgainstComputer => new ComputerPlayer(),
                Mode.AgainstComputerRandom => new ComputerRandomSelectorPlayer(),
                _ => throw new NotImplementedException($"Unrecognized mode selected: {mode}"),
            };
        }

        /// <summary>
        /// Creates a <see cref="HumanPlayer"/>.
        /// </summary>
        /// <param name="playerIdentifier">The player identifier: 1 or 2.</param>
        /// <returns>The <see cref="HumanPlayer"/>.</returns>
        private static HumanPlayer CreateHumanPlayer(string playerIdentifier)
        {
            Console.WriteLine();
            var message = $"Player {playerIdentifier}, enter your name: ";
            Helpers.PrintIndented(message);
            string? name = Console.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(name))
            {
                Helpers.PrintIndentedLineError($"Your name cannot be blank. Please, try again.");
                Helpers.PrintIndented(message);
                name = Console.ReadLine()?.Trim();
            }

            return new HumanPlayer(name);
        }
    }
}
