using Microsoft.AspNetCore.Mvc;
using RandomHeroGenerator.Host.Models;
using RandomHeroGenerator.Host.Helpers;

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
        private static List<ArenaResult> ArenaResults = new();

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
                return NotFound("Arena not found!");
            }

            var result = ArenaResults.FirstOrDefault(ar => ar.ArenaId == arenaId);
            if (result is not null) return Ok(result);

            var arena = Arenas[arenaId];
            var history = new List<BattleRound>();
            var eliminatedHeroes = new List<Hero>();

            while (arena.Heroes.Count > 1)
            {
                var round = new BattleRound();
                var (attacker, defender) = HeroHelper.SelectRandomFighters(arena.Heroes);
                round.Attacker = attacker.Id;
                round.Defender = defender.Id;

                round.AttackerType = attacker.Type;
                round.DefenderType = defender.Type;

                var initialHealth = (attacker.Health, defender.Health);
                HeroHelper.SimulateAttack(attacker, defender);

                round.AttackerHealthChange = initialHealth.Item1 - attacker.Health;
                round.DefenderHealthChange = initialHealth.Item2 - defender.Health;

                AddOrReplaceHeroes(eliminatedHeroes, attacker);
                AddOrReplaceHeroes(eliminatedHeroes, defender);

                if (attacker.Health <= 0) arena.Heroes.Remove(attacker);
                if (defender.Health <= 0) arena.Heroes.Remove(defender);

                // Increase health of heroes by 10 who are not involved in the current round
                foreach (var hero in arena.Heroes)
                {
                    if (hero.Id != attacker.Id && hero.Id != defender.Id)
                    {
                        hero.Health = Math.Min(hero.Health + 10, hero.InitialHealth);
                    }
                }

                history.Add(round);
            }

            if(arena.Heroes.Count == 1) AddOrReplaceHeroes(eliminatedHeroes, arena.Heroes[0]);

            arena.History = history; // Update arena history
            Arenas[arenaId] = arena; // Save the updated arena back to the dictionary

            var _arenaResult = new ArenaResult
            {
                ArenaId = arenaId,
                Heroes = eliminatedHeroes,
                History = history,
                LastHeroStanding = arena.Heroes.Count == 1 ? arena.Heroes[0].Id : (int?)null
            };

            ArenaResults.Add(_arenaResult);

            return Ok(_arenaResult);
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

        private static void AddOrReplaceHeroes(List<Hero> list, Hero newObject)
        {
            var existingObject = list.FirstOrDefault(o => o.Id == newObject.Id);
            if (existingObject is not null)
            {
                existingObject.Health = newObject.Health; // Update properties as needed
                existingObject.InitialHealth = newObject.InitialHealth; // Update properties as needed
            }
            else
            {
                list.Add(newObject);
            }
        }
    }
}