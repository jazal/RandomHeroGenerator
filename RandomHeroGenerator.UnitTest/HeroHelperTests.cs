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
            Assert.AreEqual(100, archer.Health); 
            Assert.AreEqual(0, swordsman.Health); // Swordsman always dies
        }

        [TestMethod]
        public void GetTheWinner_ArcherVsArcher_DefendingArcherDies()
        {
            // Arrange
            var archerAttack = new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 };
            var archerDefend = new Hero { Id = 2, Type = "Archer", Health = 100, InitialHealth = 100 };

            // Act
            HeroHelper.GetTheWinner(archerAttack, archerDefend);

            // Assert
            Assert.AreEqual(100, archerAttack.Health);
            Assert.AreEqual(0, archerDefend.Health);
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
        public void GetTheWinner_SwordsmanVsSwordsman_DefendingSwordsmanDies()
        {
            // Arrange
            var swordsmanA = new Hero { Id = 1, Type = "Swordsman", Health = 120, InitialHealth = 120 };
            var swordsmanD = new Hero { Id = 2, Type = "Swordsman", Health = 120, InitialHealth = 120 };

            // Act
            HeroHelper.GetTheWinner(swordsmanA, swordsmanD);

            // Assert
            Assert.AreEqual(120, swordsmanA.Health); // Attacking Swordsman dies
            Assert.AreEqual(0, swordsmanD.Health); // Defending Swordsman dies
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
            Assert.AreEqual(100, archer.Health); // Archer wins
        }

        [TestMethod]
        public void GetTheWinner_HorsemanVsHorseman_DefenderDies()
        {
            // Arrange
            var horsemanA = new Hero { Id = 1, Type = "Horseman", Health = 150, InitialHealth = 150 };
            var horsemanD = new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 };

            // Act
            HeroHelper.GetTheWinner(horsemanA, horsemanD);

            // Assert
            Assert.AreEqual(150, horsemanA.Health); // Attacking horseman wins
            Assert.AreEqual(0, horsemanD.Health); // Defender horseman dies
        }

        [TestMethod]
        public void GetTheWinner_HorsemanVsSwordsman_HorsemanDies()
        {
            // Arrange
            var horseman = new Hero { Id = 1, Type = "Horseman", Health = 150, InitialHealth = 150 };
            var swordsman = new Hero { Id = 1, Type = "Swordsman", Health = 120, InitialHealth = 120 };

            // Act
            HeroHelper.GetTheWinner(horseman, swordsman);

            // Assert
            Assert.AreEqual(0, horseman.Health); // Attacking horseman dies
            Assert.AreEqual(120, swordsman.Health); // Defender swordsman wins
        }

        [TestMethod]
        public void GetTheWinner_HorsemanVsArcher_ArcherDies()
        {
            // Arrange
            var horseman = new Hero { Id = 1, Type = "Horseman", Health = 150, InitialHealth = 150 };
            var archer = new Hero { Id = 2, Type = "Archer", Health = 100, InitialHealth = 100 };

            // Act
            HeroHelper.GetTheWinner(horseman, archer);

            // Assert
            Assert.AreEqual(0, horseman.Health); // Attacking horseman dies
            Assert.AreEqual(100, archer.Health); // Defender archer wins
        }
    }
}
