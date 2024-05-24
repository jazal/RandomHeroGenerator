using Microsoft.AspNetCore.Mvc;
using RandomHeroGenerator.Host.Controllers;
using RandomHeroGenerator.Host.Models;

namespace RandomHeroGenerator.UnitTest
{
    [TestClass]
    public class HeroesControllerTests
    {
        private HeroesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new HeroesController();
        }

        [TestMethod]
        public void GenerateHeroes_ReturnsArenaId()
        {
            // Arrange
            int numberOfFighters = 5;

            // Act
            var result = _controller.GenerateHeroes(numberOfFighters);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(int));
            Assert.IsTrue((int)okResult.Value > 0);
        }

        [TestMethod]
        public void Battle_ReturnsNotFoundForInvalidArenaId()
        {
            // Arrange
            int invalidArenaId = 999;

            // Act
            var result = _controller.Battle(invalidArenaId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void Battle_ReturnsHistoryForValidArenaId()
        {
            // Arrange
            var generateResult = _controller.GenerateHeroes(5);
            var okGenerateResult = generateResult.Result as OkObjectResult;
            var arenaId = (int)okGenerateResult.Value;

            // Act
            var battleResult = _controller.Battle(arenaId);

            // Assert
            Assert.IsInstanceOfType(battleResult.Result, typeof(OkObjectResult));
            var okBattleResult = battleResult.Result as OkObjectResult;
            Assert.IsNotNull(okBattleResult);
            var arenaResult = okBattleResult.Value as ArenaResult;
            Assert.IsNotNull(arenaResult);
            Assert.IsTrue(arenaResult.History.Count > 0);
        }
    }
}