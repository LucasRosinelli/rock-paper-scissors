using System;
using Moq;
using RockPaperScissors.Enums;
using RockPaperScissors.Players;
using RockPaperScissors.Utilities;
using Xunit;

namespace RockPaperScissors.UnitTests.Players
{
    public class HumanPlayerTests : PlayerBaseTests
    {
        private readonly Mock<IConsoleWrapper> _mockConsoleWrapper;

        public HumanPlayerTests()
            : base(new HumanPlayer(new Mock<IConsoleWrapper>().Object, "fake"))
        {
            _mockConsoleWrapper = new Mock<IConsoleWrapper>();
        }

        [Fact]
        public void Constructor_WithNullConsoleWrapper_ThrowsArgumentNullException()
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new HumanPlayer(null!, "fake"));
            Assert.Contains("Parameter 'consoleWrapper'", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Constructor_WithNullOrEmptyOrWhiteSpaceName_ThrowsArgumentNullException(string name)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new HumanPlayer(_mockConsoleWrapper.Object, name));
            Assert.Contains("Parameter 'name'", exception.Message);
        }

        [Theory]
        [InlineData("name 1")]
        [InlineData("a fake name")]
        [InlineData("     many spaces before and after    ")]
        public void Constructor_ShouldInstantiateWithTrimmedName(string name)
        {
            // Arrange

            // Act
            var player = new HumanPlayer(_mockConsoleWrapper.Object, name);

            // Assert
            Assert.NotNull(player.Name);
            Assert.Equal(name.Trim(), player.Name);
        }

        [Theory]
        [InlineData(Option.Rock)]
        [InlineData(Option.Paper)]
        [InlineData(Option.Scissors)]
        [InlineData(Option.Flamethrower)]
        public void Select_WithValidInput_ShouldSelectTheOptionRequested_SetItInLastSelection_And_AddItToSelectionHistory(Option selection)
        {
            // Arrange
            char selectionChar = Convert.ToChar((int)selection + 48);
            _mockConsoleWrapper
                .SetupSequence(x => x.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo(selectionChar, (ConsoleKey)selectionChar, false, false, false))
                .Returns(new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false));
            var player = new HumanPlayer(_mockConsoleWrapper.Object, "fake");

            // Act
            Option selectedOption = player.Select();

            // Assert
            _mockConsoleWrapper.Verify(x => x.ReadKey(It.IsAny<bool>()), Times.Exactly(2));
            Assert.NotNull(player.LastSelection);
            Assert.Equal(selection, selectedOption);
            Assert.Equal(selectedOption, player.LastSelection);
            Assert.Single(player.SelectionHistory, selectedOption);
        }

        [Theory]
        [InlineData((Option)0, Option.Rock)]
        [InlineData((Option)5, Option.Paper)]
        [InlineData((Option)8, Option.Scissors)]
        [InlineData((Option)9, Option.Flamethrower)]
        public void Select_WithInvalidInput_ShouldAskForNewTyping_SetItInLastSelection_And_AddItToSelectionHistory(Option selection, Option expectedSelectionSecondAttempt)
        {
            // Arrange
            char selectionChar = Convert.ToChar((int)selection + 48);
            char correctSelection = Convert.ToChar((int)expectedSelectionSecondAttempt + 48);
            _mockConsoleWrapper
                .SetupSequence(x => x.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo(selectionChar, (ConsoleKey)selectionChar, false, false, false))
                .Returns(new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false))
                .Returns(new ConsoleKeyInfo(correctSelection, (ConsoleKey)correctSelection, false, false, false))
                .Returns(new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false));
            var player = new HumanPlayer(_mockConsoleWrapper.Object, "fake");

            // Act
            Option selectedOption = player.Select();

            // Assert
            _mockConsoleWrapper.Verify(x => x.ReadKey(It.IsAny<bool>()), Times.Exactly(4));
            Assert.NotNull(player.LastSelection);
            Assert.Equal(expectedSelectionSecondAttempt, selectedOption);
            Assert.Equal(selectedOption, player.LastSelection);
            Assert.Single(player.SelectionHistory, selectedOption);
        }
    }
}
