using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Enums;
using RockPaperScissors.Players;
using RockPaperScissors.Utilities;

namespace RockPaperScissors
{
    /// <summary>
    /// The Rock, Paper, Scissors game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The number of rounds wins to win the game.
        /// </summary>
        private const int RoundsWinsLimit = 3;

        /// <summary>
        /// The <see cref="IConsoleWrapper"/>.
        /// </summary>
        private readonly IConsoleWrapper _consoleWrapper;

        /// <summary>
        /// The round winner. The list index indicates the round.
        /// </summary>
        private readonly List<IPlayer> _roundWinner;

        /// <summary>
        /// The <see cref="IPlayer"/> 1 with their wins.
        /// </summary>
        private (IPlayer Player, int Wins) _player1;

        /// <summary>
        /// The <see cref="IPlayer"/> 2 with their wins.
        /// </summary>
        private (IPlayer Player, int Wins) _player2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="player1">The <see cref="IPlayer"/> 1.</param>
        /// <param name="player2">The <see cref="IPlayer"/> 2.</param>
        private Game(IConsoleWrapper consoleWrapper, IPlayer player1, IPlayer player2)
        {
            _roundWinner = new List<IPlayer>();
            _consoleWrapper = consoleWrapper;
            _player1 = new (player1, 0);
            _player2 = new (player2, 0);
        }

        /// <summary>
        /// Creates a new <see cref="Game"/>.
        /// </summary>
        /// <param name="consoleWrapper">The <see cref="IConsoleWrapper"/>.</param>
        /// <param name="player1">The <see cref="IPlayer"/> 1.</param>
        /// <param name="player2">The <see cref="IPlayer"/> 2.</param>
        /// <returns>A new <see cref="Game"/>.</returns>
        public static Game Create(IConsoleWrapper consoleWrapper, IPlayer player1, IPlayer player2)
        {
            if (consoleWrapper is null)
            {
                throw new ArgumentNullException(nameof(consoleWrapper));
            }

            if (player1 is null)
            {
                throw new ArgumentNullException(nameof(player1));
            }

            if (player2 is null)
            {
                throw new ArgumentNullException(nameof(player1));
            }

            var game = new Game(consoleWrapper, player1, player2);

            return game;
        }

        /// <summary>
        /// Starts the <see cref="Game"/>.
        /// </summary>
        public void Start()
        {
            do
            {
                PlayRound();
            }
            while (_player1.Wins < RoundsWinsLimit && _player2.Wins < RoundsWinsLimit);

            PrintGameWinner();
        }

        /// <summary>
        /// Plays a round.
        /// </summary>
        private void PlayRound()
        {
            _consoleWrapper.WriteLine();
            _consoleWrapper.PrintIndentedLine("##################################################");
            _consoleWrapper.PrintIndentedLine($"# Round {_roundWinner.Count + 1}");

            _player1.Player.Select();
            _player2.Player.Select();

            PrintSelection();

            IPlayer? roundWinner = EvaluateRoundWinner();
            PrintRoundWinner(roundWinner);
            if (roundWinner is not null)
            {
                _roundWinner.Add(roundWinner);

                PrintScore();
            }

            _consoleWrapper.PrintIndentedLine("##################################################");
            _consoleWrapper.WriteLine();
        }

        /// <summary>
        /// Prints the players selection.
        /// </summary>
        private void PrintSelection()
        {
            PrintPlayerSelection(_player1.Player);
            PrintPlayerSelection(_player2.Player);
        }

        /// <summary>
        /// Prints the player selection.
        /// </summary>
        /// <param name="player">The <see cref="IPlayer"/>.</param>
        private void PrintPlayerSelection(IPlayer player)
        {
            _consoleWrapper.PrintIndentedLine($"# Player \"{player.Name}\" selected {player.LastSelection}");
        }

        /// <summary>
        /// Evaluates the round winner.
        /// </summary>
        /// <returns>The round winner. In case of tie (same option selected by both players), returns <see langword="null"/>.</returns>
        private IPlayer? EvaluateRoundWinner()
        {
            Option p1Selection = _player1.Player.LastSelection!.Value;
            Option p2Selection = _player2.Player.LastSelection!.Value;

            if (p1Selection == p2Selection)
            {
                return null;
            }

            if (Helpers.OptionBeatingMapper[p1Selection].Any(x => x == p2Selection))
            {
                _player1.Wins++;
                return _player1.Player;
            }

            _player2.Wins++;
            return _player2.Player;
        }

        /// <summary>
        /// Prints the round winner.
        /// </summary>
        /// <param name="winner">The <see cref="IPlayer"/>.</param>
        private void PrintRoundWinner(IPlayer? winner)
        {
            if (winner is null)
            {
                _consoleWrapper.PrintIndentedLineWarning("# Tied round. Restarting...");
                return;
            }

            _consoleWrapper.PrintIndentedLineSuccess($"# {winner.Name} won this round.");
        }

        /// <summary>
        /// Prints the score.
        /// </summary>
        private void PrintScore()
        {
            _consoleWrapper.WriteLine();
            _consoleWrapper.PrintIndentedLine("===========================================================================================");
            _consoleWrapper.PrintIndentedLine("= SCORE                                                                                   =");
            _consoleWrapper.PrintIndentedLine("===========================================================================================");
            _consoleWrapper.PrintIndented($"= Player ");
            _consoleWrapper.PrintIndentedSuccess(_player1.Player.Name, 0);
            _consoleWrapper.Write(": ");
            _consoleWrapper.PrintIndentedLineSuccess(_player1.Wins.ToString(), 0);
            _consoleWrapper.PrintIndentedLine("=");
            _consoleWrapper.PrintIndented($"= Player ");
            _consoleWrapper.PrintIndentedSuccess(_player2.Player.Name, 0);
            _consoleWrapper.Write(": ");
            _consoleWrapper.PrintIndentedLineSuccess(_player2.Wins.ToString(), 0);
            _consoleWrapper.PrintIndentedLine("===========================================================================================");
            _consoleWrapper.WriteLine();
        }

        /// <summary>
        /// Prints the game winner.
        /// </summary>
        private void PrintGameWinner()
        {
            IPlayer winner = _player1.Wins == RoundsWinsLimit ? _player1.Player : _player2.Player;

            _consoleWrapper.WriteLine();
            _consoleWrapper.PrintIndentedLine("*******************************************************************************************");
            _consoleWrapper.PrintIndentedLine("* WE HAVE A GAME WINNER!!!                                                                *");
            _consoleWrapper.PrintIndentedLine("*******************************************************************************************");
            _consoleWrapper.PrintIndentedLine("*                                                                                         *");
            _consoleWrapper.PrintIndented($"* ");
            _consoleWrapper.PrintIndentedLineSuccess($"Player {winner.Name} is the game WINNER! Congratulations!", 0);
            _consoleWrapper.PrintIndentedLine("*                                                                                         *");
            _consoleWrapper.PrintIndentedLine("*******************************************************************************************");
            _consoleWrapper.WriteLine();
        }
    }
}
