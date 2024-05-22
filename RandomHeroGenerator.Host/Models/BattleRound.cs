﻿namespace RandomHeroGenerator.Host.Models
{
    public class BattleRound
    {
        public string? AttackerType { get; set; }
        public string? DefenderType { get; set; }
        public int Attacker { get; set; }
        public int Defender { get; set; }
        public int AttackerHealthChange { get; set; }
        public int DefenderHealthChange { get; set; }
    }
}
