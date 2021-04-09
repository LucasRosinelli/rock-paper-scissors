using System.Collections.Generic;
using RockPaperScissors.Enums;
using RockPaperScissors.Players;
using Xunit;

namespace RockPaperScissors.UnitTests.Players
{
    public class ComputerPlayerTests : PlayerBaseTests
    {
        private static readonly Dictionary<Option, HashSet<Option>> ExpectedSelection = new Dictionary<Option, HashSet<Option>>()
        {
            { Option.Rock, new HashSet<Option>() { Option.Paper, } },
            { Option.Paper, new HashSet<Option>() { Option.Scissors, Option.Flamethrower, } },
            { Option.Scissors, new HashSet<Option>() { Option.Rock, } },
            { Option.Flamethrower, new HashSet<Option>() { Option.Rock, Option.Scissors, } },
        };

        public ComputerPlayerTests()
            : base(new ComputerPlayer())
        {
        }

        [Fact]
        public void Constructor_ShouldInstantiateWithDefaultName()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(Player.Name);
            Assert.Equal("Computer player", Player.Name);
        }

        [Fact]
        public void Select_WithNoPreviousSelection_ShouldSelectAnOptionRandomly_SetItInLastSelection_And_AddItToSelectionHistory()
        {
            // Arrange

            // Act
            Option selectedOption = Player.Select();

            // Assert
            Assert.NotNull(Player.LastSelection);
            Assert.Equal(selectedOption, Player.LastSelection);
            Assert.Single(Player.SelectionHistory, selectedOption);
        }

        [Theory]
        [InlineData(Option.Rock)]
        [InlineData(Option.Paper)]
        [InlineData(Option.Scissors)]
        [InlineData(Option.Flamethrower)]
        public void Select_ShouldReturnOneOptionThatBeatsTheLastSelection(Option lastSelection)
        {
            // Arrange
            while (Player.LastSelection != lastSelection)
            {
                Player.Select();
            }

            // Act
            Option selectedOption = Player.Select();

            // Assert
            Assert.Contains(ExpectedSelection[lastSelection], o => o == selectedOption);
        }
    }
}
