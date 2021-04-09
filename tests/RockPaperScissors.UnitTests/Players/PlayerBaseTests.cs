using RockPaperScissors.Players;
using Xunit;

namespace RockPaperScissors.UnitTests.Players
{
    public abstract class PlayerBaseTests
    {
        protected PlayerBaseTests(IPlayer player)
        {
            Player = player;
        }

        protected IPlayer Player { get; init; }

        [Fact]
        public void Constructor_ShouldKeepBaseClassDefinitions()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(Player.SelectionHistory);
            Assert.Empty(Player.SelectionHistory);
            Assert.Null(Player.LastSelection);
        }
    }
}
