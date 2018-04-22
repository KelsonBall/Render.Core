namespace KelsonBall.LudumDare41.Items.Models
{
    public class Teleporter
    {
        public Point Location { get; set; }
        public double Rotation { get; set; }
        public int TeleporterId { get; set; }
        public int CooldownMs { get; set; }
        public string Event { get; set; }
    }
}
