using RockPaperScissors.Enums;

namespace RockPaperScissors.Utilities
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Describes the <see cref="Mode"/>.
        /// </summary>
        /// <param name="mode">The <see cref="Mode"/> to describe.</param>
        /// <returns>The <see cref="Mode"/> description.</returns>
        public static string Describe(this Mode mode)
        {
            return mode switch
            {
                Mode.TwoHuman => $"[{(int)mode}] Two human players",
                Mode.AgainstComputer => $"[{(int)mode}] Against a computer player",
                _ => "Unrecognized mode",
            };
        }

        /// <summary>
        /// Describes the <see cref="Option"/>.
        /// </summary>
        /// <param name="option">The <see cref="Option"/> to describe.</param>
        /// <returns>The <see cref="Option"/> description.</returns>
        public static string Describe(this Option option)
        {
            return option switch
            {
                _ => $"[{(int)option}] {option}",
            };
        }
    }
}
