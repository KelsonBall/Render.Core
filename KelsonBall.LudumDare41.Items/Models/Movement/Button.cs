namespace KelsonBall.LudumDare41.Items.Models.Movement
{
    public class Button : GameItemModel
    {
        public double X { get; set; } public double Y { get; set; }
        public string EventPressed { get; set; }
        public string EventRelease { get; set; }
    }
}
