namespace RandomHeroGenerator.Host.Models
{
    public class Arena
    {
        public List<Hero> Heroes { get; set; }
        public List<BattleRound> History { get; set; }

        public Arena()
        {
            Heroes = new List<Hero>();
            History = new List<BattleRound>();
        }
    }
}
