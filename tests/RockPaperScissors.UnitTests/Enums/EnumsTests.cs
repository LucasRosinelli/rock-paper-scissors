using System;
using System.Collections.Generic;
using System.Linq;
using RockPaperScissors.Enums;
using Xunit;

namespace RockPaperScissors.UnitTests.Enums
{
    public class EnumsTests
    {
        [Fact]
        public void ModeEnumeration_ShouldContainOnlySequentialNumberedElementsStartingByOne()
        {
            // Arrange

            // Act

            // Assert
            AssertSequentialElements<Mode>();
        }

        [Fact]
        public void ModeEnumeration_ShouldContainPredefinedElements()
        {
            // Arrange
            List<int> elements = Enum.GetValues(typeof(Mode)).Cast<int>().ToList();

            // Act

            // Assert
            Assert.Collection(elements,
                e => Assert.Equal((int)Mode.TwoHuman, e),
                e => Assert.Equal((int)Mode.AgainstComputer, e),
                e => Assert.Equal((int)Mode.AgainstComputerRandom, e));
        }

        [Fact]
        public void OptionEnumeration_ShouldContainOnlySequentialNumberedElementsStartingByOne()
        {
            // Arrange

            // Act

            // Assert
            AssertSequentialElements<Option>();
        }

        [Fact]
        public void OptionEnumeration_ShouldContainPredefinedElements()
        {
            // Arrange
            List<int> elements = Enum.GetValues(typeof(Option)).Cast<int>().ToList();

            // Act

            // Assert
            Assert.Collection(elements,
                e => Assert.Equal((int)Option.Rock, e),
                e => Assert.Equal((int)Option.Paper, e),
                e => Assert.Equal((int)Option.Scissors, e),
                e => Assert.Equal((int)Option.Flamethrower, e));
        }

        private void AssertSequentialElements<T>()
            where T : Enum
        {
            // Arrange

            // Act
            List<int> orderedElements = Enum.GetValues(typeof(T)).Cast<int>().OrderBy(e => e).ToList();

            // Assert
            Assert.Equal(1, orderedElements.Min());
            Assert.Equal(orderedElements.Count, orderedElements.Max());
            Assert.Equal(orderedElements.Distinct().Count(), orderedElements.Count);
        }
    }
}
