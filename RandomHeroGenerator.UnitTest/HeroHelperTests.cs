using RandomHeroGenerator.Host.Models;
using RandomHeroGenerator.Host.Services;

namespace RandomHeroGenerator.UnitTest
{
    [TestClass]
    public class HeroHelperTests
    {
        [TestMethod]
        public void GetTheWinner_ArcherVsHorseman_HorsemanDies()
        {
            // Arrange
            var archer = new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 };
            var horseman = new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 };

            // Act
            HeroHelper.GetTheWinner(archer, horseman);

            // Assert
            Assert.IsTrue(horseman.Health == 0 || horseman.Health == 150); // Horseman has a 40% chance to die
        }

        [TestMethod]
        public void GetTheWinner_ArcherVsSwordsman_SwordsmanDies()
        {
            // Arrange
            var archer = new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 };
            var swordsman = new Hero { Id = 2, Type = "Swordsman", Health = 120, InitialHealth = 120 };

            // Act
            HeroHelper.GetTheWinner(archer, swordsman);

            // Assert
            Assert.AreEqual(0, swordsman.Health); // Swordsman always dies
        }

        [TestMethod]
        public void GetTheWinner_SwordsmanVsHorseman_NoEffect()
        {
            // Arrange
            var swordsman = new Hero { Id = 1, Type = "Swordsman", Health = 120, InitialHealth = 120 };
            var horseman = new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 };

            // Act
            HeroHelper.GetTheWinner(swordsman, horseman);

            // Assert
            Assert.AreEqual(120, swordsman.Health); // Swordsman health unchanged
            Assert.AreEqual(150, horseman.Health); // Horseman health unchanged
        }

        [TestMethod]
        public void GetTheWinner_SwordsmanVsArcher_SwordsmanDies()
        {
            // Arrange
            var swordsman = new Hero { Id = 1, Type = "Swordsman", Health = 120, InitialHealth = 120 };
            var archer = new Hero { Id = 2, Type = "Archer", Health = 100, InitialHealth = 100 };

            // Act
            HeroHelper.GetTheWinner(swordsman, archer);

            // Assert
            Assert.AreEqual(0, swordsman.Health); // Swordsman dies
        }

        [TestMethod]
        public void GetTheWinner_HorsemanVsHorseman_DefenderDies()
        {
            // Arrange
            var horseman1 = new Hero { Id = 1, Type = "Horseman", Health = 150, InitialHealth = 150 };
            var horseman2 = new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 };

            // Act
            HeroHelper.GetTheWinner(horseman1, horseman2);

            // Assert
            Assert.AreEqual(0, horseman2.Health); // Defender horseman dies
        }
    }
}
