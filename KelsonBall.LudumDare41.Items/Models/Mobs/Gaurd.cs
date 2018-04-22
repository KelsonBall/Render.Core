namespace KelsonBall.LudumDare41.Items.Models
{
    public class Gaurd : GameItemModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VisionLength { get; set; }
        public double VisionArc { get; set; }
        public double ReactionTime { get; set; }
        public MobPath Path { get; set; }
    }
}
