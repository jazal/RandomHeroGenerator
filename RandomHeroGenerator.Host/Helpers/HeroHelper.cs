using RandomHeroGenerator.Host.Models;

namespace RandomHeroGenerator.Host.Helpers
{
    public static class HeroHelper
    {
        public static void SimulateAttack(Hero attacker, Hero defender)
        {
            Random Random = new();

            bool? attackSuccess;

            switch (attacker.Type)
            {
                case "Archer":
                    attackSuccess = defender.Type switch
                    {
                        "Horseman" => Random.NextDouble() < 0.4,    // 40% chance the horseman dies, 60% chance it&#39;s blocked
                        "Swordsman" => true,
                        "Archer" => true,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case "Swordsman":
                    attackSuccess = defender.Type switch
                    {
                        "Horseman" => null,
                        "Swordsman" => true,
                        "Archer" => false,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case "Horseman":
                    attackSuccess = defender.Type switch
                    {
                        "Horseman" => true,
                        "Swordsman" => false,
                        "Archer" => false,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // The health of participating heroes is halved during the battle.
            // If it becomes less than a quarter of the initial health, they die.
            attacker.Health = attacker.Health / 2;
            defender.Health = defender.Health / 2;

            if (defender.Health <= defender.InitialHealth / 4) defender.Health = 0;
            if (attacker.Health <= attacker.InitialHealth / 4) attacker.Health = 0;

            if (attackSuccess == true)
            {
                // var dQuarterHealth = defender.InitialHealth / 4;
                // defender.Health = Math.Max(defender.Health / 2, dQuarterHealth);
                // if (defender.Health <= dQuarterHealth) defender.Health = 0;
                defender.Health = 0;
            }
            else if (attackSuccess == false)
            {
                // var aQuarterHealth = attacker.InitialHealth / 4;
                // attacker.Health = Math.Max(attacker.Health / 2, aQuarterHealth);
                // if (attacker.Health <= aQuarterHealth) attacker.Health = 0;
                attacker.Health = 0;
            }
        }

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
