using RockPaperScissors.Players;
using Xunit;

namespace RockPaperScissors.UnitTests.Players
{
    public abstract class PlayerBaseTests
    {
        protected IPlayer Player { get; init; }

        protected PlayerBaseTests(IPlayer player)
        {
            Player = player;
        }

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
