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

        /// <summary>
        /// There could be some chances where both will be die due to less than quater health in a battle
        /// </summary>
        public bool IsAttackerWon { get; set; } = true;
        public bool IsDefenderWon { get; set; } = true;
    }
}
