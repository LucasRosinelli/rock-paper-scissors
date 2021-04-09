using System;
using System.Collections.Generic;
using RockPaperScissors.Enums;

namespace RockPaperScissors.Players
{
    /// <summary>
    /// The player base implementation.
    /// </summary>
    public abstract class BasePlayer : IPlayer
    {
        /// <summary>
        /// The history of <see cref="Option"/> selection.
        /// </summary>
        protected readonly List<Option> _selectionHistory = new();

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public virtual IReadOnlyList<Option> SelectionHistory
        {
            get
            {
                return _selectionHistory;
            }
        }

        /// <inheritdoc/>
        public Option? LastSelection { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlayer"/> class.
        /// </summary>
        /// <param name="name">The player name.</param>
        protected BasePlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name.Trim();
        }

        /// <inheritdoc/>
        public abstract Option Select();

        /// <summary>
        /// Sets the <see cref="Option"/> as <see cref="IPlayer.LastSelection"/> and adds it to the history.
        /// </summary>
        /// <param name="selection">The <see cref="Option"/>.</param>
        protected void RegisterSelection(Option selection)
        {
            LastSelection = selection;
            _selectionHistory.Add(selection);
        }
    }
}
