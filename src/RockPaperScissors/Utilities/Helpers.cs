using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Enums;

namespace RockPaperScissors.Utilities
{
    /// <summary>
    /// Helpers.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// The mapper of options beat. The <see cref="KeyValuePair{TKey, TValue}.Key"/> <see cref="Option"/> beats any <see cref="Option"/> in <see cref="KeyValuePair{TKey, TValue}.Value"/>.
        /// </summary>
        internal static readonly IReadOnlyDictionary<Option, HashSet<Option>> OptionBeatingMapper = InitOptionBeatingMapper();

        /// <summary>
        /// Gets the maximum value of the enumeration.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/>.</typeparam>
        /// <returns>The maximum value of the enumeration.</returns>
        public static int MaxValue<T>()
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }

        /// <summary>
        /// Gets the minimum value of the enumeration.
        /// </summary>
        /// <typeparam name="T">The <see cref="Enum"/>.</typeparam>
        /// <returns>The minimum value of the enumeration.</returns>
        public static int MinValue<T>()
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }

        /// <summary>
        /// Initializes the mapper of options beat. The <see cref="KeyValuePair{TKey, TValue}.Key"/> <see cref="Option"/> beats any <see cref="Option"/> in <see cref="KeyValuePair{TKey, TValue}.Value"/>.
        /// </summary>
        /// <returns>The <see cref="IReadOnlyDictionary{TKey, TValue}"/> with the <see cref="Option"/> and a set of <see cref="Option"/> to beat.</returns>
        private static IReadOnlyDictionary<Option, HashSet<Option>> InitOptionBeatingMapper()
        {
            var optionBeatingMapper = new Dictionary<Option, HashSet<Option>>
            {
                { Option.Rock, new HashSet<Option>() { Option.Scissors, Option.Flamethrower, } },
                { Option.Paper, new HashSet<Option>() { Option.Rock, } },
                { Option.Scissors, new HashSet<Option>() { Option.Paper, Option.Flamethrower, } },
                { Option.Flamethrower, new HashSet<Option>() { Option.Paper, } },
            };

            return optionBeatingMapper;
        }
    }
}
