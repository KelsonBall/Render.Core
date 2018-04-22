namespace KelsonBall.LudumDare41.Items.Models
{
    public class CircleTerrain : GameItemModel
    {
        public double X { get; set; } public double Y { get; set; }
        public double Radius { get; set; }
        public string Image { get; set; }
        public double Bounce { get; set; }
        public double SoundLevel { get; set; }
        public string Event { get; set; }
    }
}
