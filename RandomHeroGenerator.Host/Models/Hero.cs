namespace RandomHeroGenerator.Host.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Health { get; set; }
        public decimal InitialHealth { get; set; }
    }
}
