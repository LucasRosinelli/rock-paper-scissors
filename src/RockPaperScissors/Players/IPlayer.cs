using System.Collections.Generic;
using RockPaperScissors.Enums;

namespace RockPaperScissors.Players
{
    /// <summary>
    /// The player contract.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The player name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The history of <see cref="Option"/> selection.
        /// </summary>
        IReadOnlyList<Option> SelectionHistory { get; }

        /// <summary>
        /// The last selection.
        /// </summary>
        Option? LastSelection { get; }

        /// <summary>
        /// Selects an <see cref="Option"/>.
        /// </summary>
        /// <returns>The <see cref="Option"/> selected.</returns>
        Option Select();
    }
}
