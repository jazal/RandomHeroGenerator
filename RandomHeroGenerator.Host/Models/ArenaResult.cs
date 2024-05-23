namespace RandomHeroGenerator.Host.Models
{
    public class ArenaResult
    {
        public int ArenaId { get; set; }
        public List<Hero> Heroes { get; set; }
        public List<BattleRound> History { get; set; }
        public int? LastHeroStanding { get; set; }

        public ArenaResult()
        {
            Heroes = new List<Hero>();
            History = new List<BattleRound>();
        }
    }
}
