namespace KelsonBall.LudumDare41.Items.Models
{
    public class PolygonTerrain
    {
        public Point Location { get; set; }
        public Point[] Points { get; set; }
        public double Bounce { get; set; }
        public double SoundLevel { get; set; }
        public string Event { get; set; }
    }
}
