using RandomHeroGenerator.Host.Models;
using RandomHeroGenerator.Host.Helpers;

namespace RandomHeroGenerator.UnitTest
{
    [TestClass]
    public class SelectRandomFightersTests
    {
        [TestMethod]
        public void SelectRandomFighters_ReturnsTwoDifferentHeroes()
        {
            // Arrange
            var heroes = new List<Hero>
            {
                new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 },
                new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 },
                new Hero { Id = 3, Type = "Swordsman", Health = 120, InitialHealth = 120 }
            };

            // Act
            var result = HeroHelper.SelectRandomFighters(heroes);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Item1.Id, result.Item2.Id, "The attacker and defender should be different heroes.");
        }

        [TestMethod]
        public void SelectRandomFighters_AllowsOnlyUniquePairs()
        {
            // Arrange
            var heroes = new List<Hero>
            {
                new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 },
                new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 }
            };

            // Act
            var result = HeroHelper.SelectRandomFighters(heroes);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Item1.Id, result.Item2.Id, "The attacker and defender should be different heroes.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SelectRandomFighters_ThrowsExceptionForSingleHero()
        {
            // Arrange
            var heroes = new List<Hero>
            {
                new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 }
            };

            // Act
            HeroHelper.SelectRandomFighters(heroes);
        }

        [TestMethod]
        public void SelectRandomFighters_SelectsHeroesFromList()
        {
            // Arrange
            var heroes = new List<Hero>
            {
                new Hero { Id = 1, Type = "Archer", Health = 100, InitialHealth = 100 },
                new Hero { Id = 2, Type = "Horseman", Health = 150, InitialHealth = 150 },
                new Hero { Id = 3, Type = "Swordsman", Health = 120, InitialHealth = 120 }
            };

            // Act
            var result = HeroHelper.SelectRandomFighters(heroes);

            // Assert
            Assert.IsTrue(heroes.Contains(result.Item1), "The attacker should be one of the heroes from the list.");
            Assert.IsTrue(heroes.Contains(result.Item2), "The defender should be one of the heroes from the list.");
        }
    }
}