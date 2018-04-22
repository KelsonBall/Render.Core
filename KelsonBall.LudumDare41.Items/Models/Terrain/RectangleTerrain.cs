namespace KelsonBall.LudumDare41.Items.Models
{
    public class RectangleTerrain : GameItemModel
    {
        public double X { get; set; } public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Rotation { get; set; }
        public string Image { get; set; }
        public double Bounce { get; set; }
        public double SoundLevel { get; set; }
        public string Event { get; set; }
    }
}
