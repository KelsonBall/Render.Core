namespace KelsonBall.LudumDare41.Items.Models
{
    public class Gaurd
    {
        public Point Start { get; set; }
        public double VisionLength { get; set; }
        public double VisionArc { get; set; }
        public double ReactionTime { get; set; }
        public MobPath Path { get; set; }
    }
}
