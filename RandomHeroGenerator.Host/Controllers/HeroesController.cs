using Microsoft.AspNetCore.Mvc;
using RandomHeroGenerator.Host.Models;
using RandomHeroGenerator.Host.Services;

namespace RandomHeroGenerator.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private static readonly Dictionary<int, Arena> Arenas = new();
        private static int _arenaCounter = 0;
        private static int _heroCounter = 0;
        private static readonly Random Random = new();

        [HttpPost("generate")]
        public ActionResult<int> GenerateHeroes([FromQuery] int numberOfFighters)
        {
            var arenaId = ++_arenaCounter;
            var heroes = new List<Hero>();

            for (var i = 0; i < numberOfFighters; i++)
            {
                heroes.Add(GenerateRandomHero());
            }

            Arenas[arenaId] = new Arena
            {
                Heroes = heroes,
                History = new List<BattleRound>()
            };

            return Ok(arenaId);
        }

        [HttpPost("battle")]
        public ActionResult<ArenaResult> Battle([FromQuery] int arenaId)
        {
            if (!Arenas.ContainsKey(arenaId))
            {
                return NotFound("Arena not found.");
            }

            var arena = Arenas[arenaId];
            var history = new List<BattleRound>();

            while (arena.Heroes.Count > 1)
            {
                var round = new BattleRound();
                var (attacker, defender) = SelectRandomFighters(arena.Heroes);
                round.Attacker = attacker.Id;
                round.Defender = defender.Id;

                round.AttackerType = attacker.Type;
                round.DefenderType = defender.Type;

                var initialHealth = (attacker.Health, defender.Health);
                HeroHelper.SimulateAttack(attacker, defender);

                round.AttackerHealthChange = initialHealth.Item1 - attacker.Health;
                round.DefenderHealthChange = initialHealth.Item2 - defender.Health;

                if (defender.Health <= 0) arena.Heroes.Remove(defender);
                if (attacker.Health <= 0) arena.Heroes.Remove(attacker);

                foreach (var hero in arena.Heroes)
                {
                    if (hero.Id != attacker.Id && hero.Id != defender.Id)
                    {
                        hero.Health = Math.Min(hero.Health + 10, hero.InitialHealth);
                    }
                }

                history.Add(round);
            }

            arena.History = history; // Update arena history
            Arenas[arenaId] = arena; // Save the updated arena back to the dictionary

            return Ok(new ArenaResult
            {
                History = history,
                LastHeroStanding = arena.Heroes.Count == 1 ? arena.Heroes[0].Id : (int?)null
            });
        }

        private static Hero GenerateRandomHero()
        {
            var types = new[] { "Archer", "Horseman", "Swordsman" };
            var type = types[Random.Next(types.Length)];
            var id = ++_heroCounter;
            return type switch
            {
                "Archer" => new Hero { Id = id, Type = type, Health = 100, InitialHealth = 100 },
                "Horseman" => new Hero { Id = id, Type = type, Health = 150, InitialHealth = 150 },
                "Swordsman" => new Hero { Id = id, Type = type, Health = 120, InitialHealth = 120 },
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static (Hero, Hero) SelectRandomFighters(List<Hero> heroes)
        {
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