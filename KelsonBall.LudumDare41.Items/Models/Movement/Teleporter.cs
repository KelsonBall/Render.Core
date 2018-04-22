namespace KelsonBall.LudumDare41.Items.Models
{
    public class Teleporter : GameItemModel
    {
        public double X { get; set; } public double Y { get; set; }
        public double Rotation { get; set; }
        public int TeleporterId { get; set; }
        public int CooldownMs { get; set; }
        public string Event { get; set; }
    }
}
