using System;
using RockPaperScissors.Enums;
using RockPaperScissors.Utilities;

namespace RockPaperScissors.Players
{
    public sealed class ComputerRandomSelectorPlayer : BasePlayer
    {
        /// <summary>
        /// The default player name.
        /// </summary>
        private const string DefaultPlayerName = "Computer random selector player";

        /// <summary>
        /// The randomizer for the <see cref="Option"/> selection.
        /// </summary>
        private readonly Random _rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerRandomSelectorPlayer"/> class.
        /// </summary>
        public ComputerRandomSelectorPlayer()
            : base(DefaultPlayerName)
        {
            _rnd = new Random();
        }

        /// <summary>
        /// Selects an <see cref="Option"/> randomly.
        /// </summary>
        /// <returns>The <see cref="Option"/> selected.</returns>
        public override Option Select()
        {
            Option selectedOption = (Option)_rnd.Next(Helpers.MinValue<Option>(), Helpers.MaxValue<Option>() + 1);

            RegisterSelection(selectedOption);

            return selectedOption;
        }
    }
}
