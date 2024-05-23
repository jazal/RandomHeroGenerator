using RandomHeroGenerator.Host.Models;

namespace RandomHeroGenerator.Host.Services
{
    public static class HeroHelper
    {
        public static void GetTheWinner(Hero attacker, Hero defender)
        {
            Random Random = new();

            bool? attackSuccess;

            switch (attacker.Type)
            {
                case "Archer":
                    attackSuccess = defender.Type switch
                    {
                        "Horseman" => Random.NextDouble() < 0.4,
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

            if (attackSuccess == true)
            {
                //defender.Health = Math.Max(defender.Health / 2, defender.InitialHealth / 4);
                //if (defender.Health <= defender.InitialHealth / 4) defender.Health = 0;
                defender.Health = 0;
            }
            else if (attackSuccess == false)
            {
                //attacker.Health = Math.Max(attacker.Health / 2, attacker.InitialHealth / 4);
                //if (attacker.Health <= attacker.InitialHealth / 4) attacker.Health = 0;
                attacker.Health = 0;
            }
        }
    }
}
