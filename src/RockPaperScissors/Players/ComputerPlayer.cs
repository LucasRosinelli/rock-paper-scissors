using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Enums;
using RockPaperScissors.Utilities;

namespace RockPaperScissors.Players
{
    /// <summary>
    /// The computer player.
    /// </summary>
    public sealed class ComputerPlayer : BasePlayer
    {
        /// <summary>
        /// The default player name.
        /// </summary>
        private const string DefaultPlayerName = "Computer player";

        /// <summary>
        /// The randomizer for the first <see cref="Option"/> selection.
        /// </summary>
        private readonly Random _rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerPlayer"/> class.
        /// </summary>
        public ComputerPlayer()
            : base(DefaultPlayerName)
        {
            _rnd = new();
        }

        /// <summary>
        /// Selects an <see cref="Option"/> that beats its previous selection.
        /// </summary>
        /// <remarks>
        /// In the first selection, a random <see cref="Option"/> is selected.
        /// </remarks>
        /// <returns>The <see cref="Option"/> selected.</returns>
        public override Option Select()
        {
            Option previousSelection = LastSelection ?? (Option)_rnd.Next(Helpers.MinValue<Option>(), Helpers.MaxValue<Option>() + 1);

            HashSet<Option> previousSelectionIsBeaten = Helpers.OptionBeatingMapper.Where(o => o.Value.Contains(previousSelection)).Select(o => o.Key).ToHashSet();
            Option selectedOption = previousSelectionIsBeaten.ElementAt(_rnd.Next(0, previousSelectionIsBeaten.Count));

            RegisterSelection(selectedOption);

            return selectedOption;
        }
    }
}
