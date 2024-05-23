using RandomHeroGenerator.Host.Models;

namespace RandomHeroGenerator.Host.Services
{
    public static class Fighters
    {
        public static (Hero, Hero) SelectRandomFighters(List<Hero> heroes)
        {
            if (heroes.Count == 1) throw new Exception("There should be atleast 2 or more fighters.");

            Random Random = new();

            var attacker = heroes[Random.Next(heroes.Count)];
            Hero defender;

            do
            {
                defender = heroes[Random.Next(heroes.Count)];
            } while (defender.Id == attacker.Id);   // Same hero cannot battle together

            return (attacker, defender);
        }
    }
}
