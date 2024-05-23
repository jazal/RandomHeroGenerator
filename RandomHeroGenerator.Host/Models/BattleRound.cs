namespace RandomHeroGenerator.Host.Models
{
    public class BattleRound
    {
        public string? AttackerType { get; set; }
        public string? DefenderType { get; set; }
        public int Attacker { get; set; }
        public int Defender { get; set; }
        public decimal AttackerHealthChange { get; set; }
        public decimal DefenderHealthChange { get; set; }
    }
}
