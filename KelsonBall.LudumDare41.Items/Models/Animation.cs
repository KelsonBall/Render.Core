namespace KelsonBall.LudumDare41.Items.Models
{
    public class Animation
    {
        public string Name { get; set; }
        public string SpriteSheet { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Padding { get; set; }
        public int[] Durations { get; set; }
    }
}
