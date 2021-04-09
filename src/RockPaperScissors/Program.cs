using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Enums;
using RockPaperScissors.Players;
using RockPaperScissors.Utilities;

namespace RockPaperScissors
{
    public class Program
    {
        private static IConsoleWrapper _consoleWrapper;

        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _consoleWrapper = serviceProvider.GetRequiredService<IConsoleWrapper>();

            try
            {
                PrintHeader();

                PrintRules();

                Mode mode = ReadMode();

                var players = CreatePlayers(mode);
                var game = Game.Create(_consoleWrapper, players.Player1, players.Player2);
                game.Start();

                _consoleWrapper.PrintIndented("Press any key to exit");
                _consoleWrapper.ReadLine();
            }
            catch (Exception exc)
            {
                _consoleWrapper.PrintIndentedError("An error occurred during the application execution. Sorry!");
                _consoleWrapper.PrintIndentedError(exc.Message);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        }

        /// <summary>
        /// Prints application header.
        /// </summary>
        private static void PrintHeader()
        {
            _consoleWrapper.WriteLine();
            _consoleWrapper.WriteLine("  *******************************************************************************************************************");
            _consoleWrapper.WriteLine("  *                                                                                                                 *");
            _consoleWrapper.WriteLine("  *                                                ROCK PAPER SCISSORS                                              *");
            _consoleWrapper.WriteLine("  *                                                                                                                 *");
            _consoleWrapper.WriteLine("  *******************************************************************************************************************");
            _consoleWrapper.WriteLine("  *  Lucas Rosinelli https://lucasrosinelli.com/                                                                    *");
            _consoleWrapper.WriteLine("  *******************************************************************************************************************");
            _consoleWrapper.WriteLine();
        }

        private static void PrintRules()
        {
            _consoleWrapper.WriteLine();
            _consoleWrapper.WriteLine("   /---------------------------------------------------------------------------\\");
            _consoleWrapper.WriteLine("   | RULES                                                                     |");
            _consoleWrapper.WriteLine("   |---------------------------------------------------------------------------|");
            _consoleWrapper.WriteLine("   | The first player to win 3 rounds wins.                                    |");
            _consoleWrapper.WriteLine("   | During each round, players select one of the three following options:     |");
            _consoleWrapper.WriteLine("   |   - Rock, which beats scissors                                            |");
            _consoleWrapper.WriteLine("   |   - Paper, which beats rock                                               |");
            _consoleWrapper.WriteLine("   |   - Scissors, which beats paper                                           !");
            _consoleWrapper.WriteLine("   | When the same option is selected by both players, the round is restarted. |");
            _consoleWrapper.WriteLine("   \\---------------------------------------------------------------------------/");
            _consoleWrapper.WriteLine();

        }

        /// <summary>
        /// Reads the game <see cref="Mode"/>.
        /// </summary>
        /// <returns>The <see cref="Mode"/>.</returns>
        private static Mode ReadMode()
        {
            while (true)
            {
                _consoleWrapper.WriteLine();
                _consoleWrapper.PrintIndentedLine("Select the mode you want to play:");
                IEnumerable<string> modeDescriptions = Enum.GetValues<Mode>().Select(o => o.Describe());
                foreach (var modeDescription in modeDescriptions)
                {
                    _consoleWrapper.PrintIndentedLine($">> {modeDescription}");
                }

                _consoleWrapper.PrintIndented(string.Empty);
                string? modeFromUser = _consoleWrapper.ReadLine()?.Trim();

                if (Enum.TryParse<Mode>(modeFromUser, true, out var mode) && Enum.IsDefined(mode))
                {
                    return mode;
                }

                _consoleWrapper.PrintIndentedLineError($"{modeFromUser} is an invalid mode. Please, try again.");
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
            _consoleWrapper.WriteLine();
            var message = $"Player {playerIdentifier}, enter your name: ";
            _consoleWrapper.PrintIndented(message);
            string? name = _consoleWrapper.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(name))
            {
                _consoleWrapper.PrintIndentedLineError($"Your name cannot be blank. Please, try again.");
                _consoleWrapper.PrintIndented(message);
                name = _consoleWrapper.ReadLine()?.Trim();
            }

            return new HumanPlayer(_consoleWrapper, name);
        }
    }
}
